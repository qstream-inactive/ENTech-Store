using ENTech.Store.Entities;
using ENTech.Store.Entities.StoreModule;
using ENTech.Store.Entities.UnitOfWork;
using ENTech.Store.Infrastructure.Repositories;
using ENTech.Store.Services.ProductModule.Requests;
using ENTech.Store.Services.ProductModule.Responses;
using ENTech.Store.Services.SharedModule.Commands;

namespace ENTech.Store.Services.ProductModule.Commands
{
    public class ProductCreateCommandBase : RepositoryBasedCommandBase<ProductCreateRequest, ProductCreateResponse>
	{
        public ProductCreateCommandBase(IRepository repository)
            : base(repository)
		{
		}

		public override ProductCreateResponse Execute(ProductCreateRequest request)
		{
		    //todo:
            Repository.Create<Product>(2, new Product
            {
                Name = request.Product.Name
            });

			return new ProductCreateResponse
			{
				IsSuccess = true,
                ProductId = 2//prodId
			};
		}
	}
}