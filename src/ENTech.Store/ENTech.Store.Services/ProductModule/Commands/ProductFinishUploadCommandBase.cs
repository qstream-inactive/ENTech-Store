using ENTech.Store.Entities;
using ENTech.Store.Entities.StoreModule;
using ENTech.Store.Entities.UnitOfWork;
using ENTech.Store.Infrastructure.Repositories;
using ENTech.Store.Services.ProductModule.Requests;
using ENTech.Store.Services.ProductModule.Responses;
using ENTech.Store.Services.SharedModule.Commands;
using ENTech.Store.Services.SharedModule.Requests;
using ENTech.Store.Services.SharedModule.Responses;

namespace ENTech.Store.Services.ProductModule.Commands
{
    public class ProductFinishUploadCommandBase : RepositoryBasedCommandBase<EntityFinishUploadRequest, EntityFinishUploadResponse>
	{
        public ProductFinishUploadCommandBase(IRepository repository)
            : base(repository)
		{
		}

        public override EntityFinishUploadResponse Execute(EntityFinishUploadRequest request)
		{
            //todo: update product with repo

            if (request.EntityFieldName.ToLower() == "photourl")
            {
                var p = Repository.GetById<Product>(request.EntityId);

                if (p.PhotoUploadId == request.UploadId)
                {
                    p.PhotoUrl = request.CdnUrl;
                    p.PhotoUploadId = 0;
                    Repository.Update(p.Id, p);
                }//else - the upload is not actual (was canceled before)
            }

            return new EntityFinishUploadResponse
			{
				IsSuccess = true
			};
		}
	}
}