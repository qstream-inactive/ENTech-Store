using ENTech.Store.Entities;
using ENTech.Store.Entities.UnitOfWork;
using ENTech.Store.Infrastructure.FileStorage;
using ENTech.Store.Services.SharedModule.Commands;
using ENTech.Store.Services.UploadModule.Requests;
using ENTech.Store.Services.UploadModule.Responses;
using System;
using ENTech.Store.Infrastructure.Repositories;
using ENTech.Store.Entities.UploadModule;

namespace ENTech.Store.Services.UploadModule.Commands
{
    public class UploadGetByIdCommandBase : RepositoryBasedCommandBase<UploadGetByIdRequest, UploadGetByIdResponse>
    {
        public UploadGetByIdCommandBase(IRepository repository)
            : base(repository)
		{
		}

        public override UploadGetByIdResponse Execute(UploadGetByIdRequest request)
        {
            var upload = Repository.GetById<Upload>(request.Id);
            //todo: rerieve from repository
            //request.Id
            return new UploadGetByIdResponse()
            {
                Id = upload.Id,
                OwnerId = upload.OwnerId,
                SizeBytes = upload.SizeBytes,
                UploadedBytes = upload.UploadedBytes,
                CreatedAt = upload.CreatedAt,
                LastUpdatedAt = upload.LastUpdatedAt
            };

        }
    }
}
