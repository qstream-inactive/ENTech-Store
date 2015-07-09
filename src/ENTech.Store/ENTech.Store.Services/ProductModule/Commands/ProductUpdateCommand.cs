using ENTech.Store.Entities;
using ENTech.Store.Entities.StoreModule;
using ENTech.Store.Entities.UnitOfWork;
using ENTech.Store.Services.ProductModule.Requests;
using ENTech.Store.Services.ProductModule.Responses;
using ENTech.Store.Services.SharedModule.Commands;
using ENTech.Store.Services.UploadModule.Requests;

namespace ENTech.Store.Services.ProductModule.Commands
{
	public class ProductUpdateCommand : DbContextCommandBase<ProductUpdateRequest, ProductUpdateResponse>
	{
		public ProductUpdateCommand(IUnitOfWork unitOfWork)
			: base(unitOfWork.DbContext, false)
		{
		}

		public override ProductUpdateResponse Execute(ProductUpdateRequest request)
		{
		    Product product = null;// todo:get from the repo

		    HandlePhotoUpload(request, product);

		    return new ProductUpdateResponse
			{
				IsSuccess = true
			};
		}

	    private static void HandlePhotoUpload(ProductUpdateRequest request, Product product)
	    {
	        if (product.PhotoUploadId != request.Product.PhotoUploadId)
	        {
	            if (product.PhotoUploadId != 0)
	            {
	                var detachRequest = new UploadDetachRequest
	                {
	                    UploadId = product.PhotoUploadId
	                };
	                //todo: exec command
	            }


	            //todo get product,
	            if (request.Product.PhotoUploadId != 0)
	            {
	                var attachRequest = new UploadAttachRequest
	                {
	                    UploadId = request.Product.PhotoUploadId,
	                    AttachingEntityType = "Product",
	                    AttachingEntityId = request.Id,
	                    AttachingEntityFieldName = "Photo"
	                };

	                //todo: execute request
	            }
	        }
	    }
	}
}