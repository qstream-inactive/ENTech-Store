using ENTech.Store.Entities;
using ENTech.Store.Entities.UnitOfWork;
using ENTech.Store.Entities.UploadModule;
using ENTech.Store.Services.SharedModule.Commands;
using ENTech.Store.Services.UploadModule.Requests;
using ENTech.Store.Services.UploadModule.Responses;
using System;
using ENTech.Store.Infrastructure.Repositories;

namespace ENTech.Store.Services.UploadModule.Commands
{
    public class UploadDetachCommandBase : RepositoryBasedCommandBase<UploadDetachRequest, UploadDetachResponse>
    {
        public UploadDetachCommandBase(IRepository repository)
            : base(repository)
		{
		}

        public override UploadDetachResponse Execute(UploadDetachRequest request)
        {
            if (request.UploadId > 0)
                throw new ArgumentNullException("uploadId");

            var u = Repository.GetById<Upload>(request.UploadId);
            
            //todo: update when userId is available
            //if (u.OwnerId != this.UserId)
            //    throw new Exception("Access denied");

            u.AttachedEntityType = null;
            u.AttachedEntityFieldName = null;
            u.AttachedEntityId = 0;
            u.IsAttached = false;
            Repository.Update(u.Id, u);

            return new UploadDetachResponse
			{
				IsSuccess = true
			};
		}

    }
}



