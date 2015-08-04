using ENTech.Store.Entities;
using ENTech.Store.Entities.UnitOfWork;
using ENTech.Store.Services.SharedModule.Commands;
using ENTech.Store.Services.SharedModule.Requests;
using ENTech.Store.Services.UploadModule.Requests;
using ENTech.Store.Services.UploadModule.Responses;
using System;
using ENTech.Store.Infrastructure.Repositories;
using ENTech.Store.Services.SharedModule.Responses;
using ENTech.Store.Entities.UploadModule;

namespace ENTech.Store.Services.UploadModule.Commands
{
    public class UploadFinishCommandBase : RepositoryBasedCommandBase<UploadFinishRequest, UploadFinishResponse>
    {
        public UploadFinishCommandBase(IRepository repository)
            : base(repository)
		{
		}

        public override UploadFinishResponse Execute(UploadFinishRequest request)
        {
            Repository.Delete<Upload>(request.UploadId);

            var r = new EntityFinishUploadRequest
            {
                EntityId = request.AttachedEntityId,
                EntityFieldName = request.AttachedEntityFieldName,
                CdnUrl = request.CdnUrl
            }; 
            
            var cmd = GetCommand(request.AttachedEntityType);

            var result = new UploadFinishResponse
            {
                IsSuccess = false
            };

            if (cmd != null )
            {
                var cmdResult = cmd.Execute(r);
                result.IsSuccess = cmdResult.IsSuccess;
                result.Error = cmdResult.Error;
                result.ArgumentErrors = cmdResult.ArgumentErrors;
            }

            return result;
        }

        private RepositoryBasedCommandBase<EntityFinishUploadRequest, EntityFinishUploadResponse> GetCommand(string entityTypeName)
        {
            
            if (entityTypeName == "Product")
            {
                //todo: use command service
                return new ProductModule.Commands.ProductFinishUploadCommandBase(this.Repository);
            }

            return null;
        }
    }
}



