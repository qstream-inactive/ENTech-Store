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
    public class UploadAttachCommandBase : RepositoryBasedCommandBase<UploadAttachRequest, UploadAttachResponse>
    {


        public UploadAttachCommandBase(IRepository repository)
            : base(repository)
        {
        }


        public override UploadAttachResponse Execute(UploadAttachRequest request)
        {
            if (request.UploadId == 0)
                throw new ArgumentNullException("uploadId");

            var upload = Repository.GetById<Upload>(request.UploadId);
            
            //todo: adjust when userId is available
            //if (u.OwnerId != this.UserId)
            //    throw new Exception("Access denied");

            upload.AttachedEntityType = request.AttachingEntityType;
            upload.AttachedEntityFieldName = request.AttachingEntityFieldName;
            upload.AttachedEntityId = request.AttachingEntityId;
            upload.IsAttached = true;
            Repository.Update(upload.Id, upload);

            if (upload.IsUploaded)
                new UploadFinishCommandBase(this.Repository).Execute(new UploadFinishRequest
                {
                    UploadId = upload.Id,
                    CdnUrl = upload.CdnUrl,
                    AttachedEntityType = upload.AttachedEntityType,
                    AttachedEntityFieldName = upload.AttachedEntityFieldName,
                    AttachedEntityId = upload.AttachedEntityId
                });

            return new UploadAttachResponse
			{
				IsSuccess = true
			};
		}

    }
}



