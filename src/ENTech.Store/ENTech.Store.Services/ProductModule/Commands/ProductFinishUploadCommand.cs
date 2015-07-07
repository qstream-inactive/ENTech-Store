using ENTech.Store.Entities;
using ENTech.Store.Entities.UnitOfWork;
using ENTech.Store.Services.ProductModule.Requests;
using ENTech.Store.Services.ProductModule.Responses;
using ENTech.Store.Services.SharedModule.Commands;
using ENTech.Store.Services.SharedModule.Requests;
using ENTech.Store.Services.SharedModule.Responses;

namespace ENTech.Store.Services.ProductModule.Commands
{
    public class ProductFinishUploadCommand : DbContextCommandBase<EntityFinishUploadRequest, EnitutyFinishUploadResponse>
	{
        public ProductFinishUploadCommand(IUnitOfWork unitOfWork)
            : base(unitOfWork.DbContext, false)
		{
		}

        public override EnitutyFinishUploadResponse Execute(EntityFinishUploadRequest request)
		{
            //todo: update product with repo

            if (request.EntityFieldName.ToLower() == "logo")
            {
                var p = GetById(request.EntityId);

                if (p.LogoUploadId == request.UploadId)
                {
                    p.LogoUrl = url;
                    p.LogoUploadId = "";
                    Update(p);
                }//else - the upload is not actual (was canceled before)
            }

            return new EnitutyFinishUploadResponse
			{
				IsSuccess = true
			};
		}
	}
}