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
    public class UploadUpdateCommandBase:RepositoryBasedCommandBase<UploadUpdateRequest, UploadUpdateResponse>
    {
        public UploadUpdateCommandBase(IRepository repository)
            : base(repository)
		{
		}

        public override UploadUpdateResponse Execute(UploadUpdateRequest request)
        {
            const int mb = 1024 * 1024;

            //if (!_repository.Exists(id))
            //    CreateUpload(id);

            var upload = Repository.GetById<Upload>(request.Id);
            
            //todo: update wwhen userId is available
            //if (u.OwnerId != this.UserId) throw new Exception("Access denied");

            var limit = GetLimit(upload.Role);

            if ((request.Stream.Length / mb) > limit)
                throw new Exception("File is too big");

            upload.FileExtension = request.Extension;
            upload.SizeBytes = (int)request.Stream.Length;
            upload.LastUpdatedAt= DateTime.UtcNow;

            Repository.Update(upload.Id, upload);

            var fileStorage = new LocalFileStorage();

            var url = fileStorage.Save(upload.Id + request.Extension, request.Stream, pos =>
            {
                //update status
                upload.UploadedBytes = (int)request.Stream.Position;
                Repository.Update(upload.Id, upload);
            });

            upload.CdnUrl = url;
            upload.IsUploaded = true;
            Repository.Update(upload.Id, upload);

            if (upload.IsAttached)
                new UploadFinishCommandBase(this.Repository).Execute(new UploadFinishRequest
                {
                    UploadId = upload.Id,
                    CdnUrl = upload.CdnUrl,
                    AttachedEntityType = upload.AttachedEntityType,
                    AttachedEntityFieldName = upload.AttachedEntityFieldName,
                    AttachedEntityId = upload.AttachedEntityId
                });
            
            //todo: UploadUpdateRequest upload

            return new UploadUpdateResponse();
        }

        private int GetLimit(string role)
        {
            return 10;
        }

    }
}



