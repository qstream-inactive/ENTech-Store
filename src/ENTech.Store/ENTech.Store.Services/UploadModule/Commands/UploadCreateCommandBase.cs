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
    public class UploadCreateCommandBase:RepositoryBasedCommandBase<UploadCreateRequest, UploadCreateResponse>
    {
        public UploadCreateCommandBase(IRepository repository)
            : base(repository)
		{
		}


        public override UploadCreateResponse Execute(UploadCreateRequest request)
        {
            var upload = new Upload();
            upload.Id = 1;
            upload.CreatedAt = DateTime.UtcNow;
            upload.LastUpdatedAt = upload.CreatedAt;
            upload.Role = request.Role;
            //todo:update when userID is available
            //upload.OwnerId = userId; 

            Repository.Create(1, upload);

            return new UploadCreateResponse
			{
				IsSuccess = true,
                Id = upload.Id
			};
		}

    }
}



