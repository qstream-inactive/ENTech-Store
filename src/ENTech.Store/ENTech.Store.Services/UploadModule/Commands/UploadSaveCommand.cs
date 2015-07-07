using ENTech.Store.Entities;
using ENTech.Store.Entities.UnitOfWork;
using ENTech.Store.Services.SharedModule.Commands;
using ENTech.Store.Services.UploadModule.Requests;
using ENTech.Store.Services.UploadModule.Responses;
using System;

namespace ENTech.Store.Services.UploadModule.Commands
{
    public class UploadSaveCommand:DbContextCommandBase<UploadSaveRequest, UploadSaveResponse>
    {
        public UploadSaveCommand(IUnitOfWork unitOfWork)
            : base(unitOfWork.DbContext, false)
		{
		}

        public override UploadSaveResponse Execute(UploadSaveRequest request)
        {
            const int mb = 1024 * 1024;

            if ((s.Length / mb) > (int)limit)
                throw new Exception("File is too big");

            if (!_repository.Exists(id))
                CreateUpload(id);

            var u = _repository.GetById(id);
            if (u.OwnerId != this.UserId) throw new Exception("Access denied");

            u.FileExtension = fileExtension;
            u.Size = (int)s.Length;
            u.UpdatedAt = DateTime.UtcNow;

            _repository.Update(u.Id, u);

            var url = await _fileStorage.Save(u.Id + fileExtension, s, pos =>
            {
                //update status
                u.Uploaded = s.Position;
                _repository.Update(u.Id, u);
            });

            u.URL = url;
            u.IsUploaded = true;
            _repository.Update(u.Id, u);

            if (u.IsAttached)
                Finish(u);

            var upload = this.DbContext.Uploads.Create();
            upload.CreatedAt = DateTime.UtcNow;
            upload.LastUpdatedAt = upload.CreatedAt;
            upload.OwnerId = userId;
            
            //todo: UploadSaveRequest upload

            return new UploadCreateResponse
			{
				IsSuccess = true,
                Id = upload.Id
			};
		}

    }
}



