using ENTech.Store.Entities.StoreModule;
using ENTech.Store.Entities.UnitOfWork;
using ENTech.Store.Infrastructure.Repositories;
using ENTech.Store.Services.ProductModule.Queries;
using ENTech.Store.Services.ProductModule.Requests;
using ENTech.Store.Services.ProductModule.Responses;
using ENTech.Store.Services.SharedModule.Commands;
using ENTech.Store.Services.SharedModule.Dtos;

namespace ENTech.Store.Services.ProductModule.Commands
{
    public class ProductGetByIdCommandBase : RepositoryBasedCommandBase<ProductGetByIdRequest, ProductGetByIdResponse>
	{
        public ProductGetByIdCommandBase(IRepository repository)
            : base(repository)
		{
		}


		public override ProductGetByIdResponse Execute(ProductGetByIdRequest request)
		{
			//var query = new ProductGetByIdQuery();
            var product = Repository.GetById<Product>(request.Id.Value);
           
            var productDto = new Dtos.ProductDto
		    {
                Id = product.Id,
                Name = product.Name
		    };

            if (product.PhotoUploadId != 0)
            {
                var upload = Repository.GetById<ENTech.Store.Entities.UploadModule.Upload>(product.PhotoUploadId);
                productDto.Photo = new UploadReferenceDto(upload.Id, upload.UploadedBytes, upload.SizeBytes);
            }
            else if (!string.IsNullOrEmpty(product.PhotoUrl))
                productDto.Photo = new UrlDto(product.PhotoUrl);
            else
                productDto.Photo = null;


            return new ProductGetByIdResponse
			{
				IsSuccess = true,
                Item = productDto
			};
		}
	}
}