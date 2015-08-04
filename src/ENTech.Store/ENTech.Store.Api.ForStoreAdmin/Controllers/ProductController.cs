using System;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ENTech.Store.Services.CommandService.Definition;
using ENTech.Store.Services.Misc;
using ENTech.Store.Services.ProductModule.Commands;
using ENTech.Store.Services.ProductModule.Requests;
using ENTech.Store.Services.ProductModule.Responses;

namespace ENTech.Store.Api.ForStoreAdmin.Controllers
{
	[RoutePrefix("1.0/store-admin-api/products")]
	public class ProductController : ApiController
	{
		private readonly IExternalCommandService<BusinessAdminSecurityInformation> _businessAdminExternalCommandService;

		public ProductController(IExternalCommandService<BusinessAdminSecurityInformation> businessAdminExternalCommandService)
		{
			_businessAdminExternalCommandService = businessAdminExternalCommandService;
		}

        [HttpPost]
        [Route("TestOffSet")]
        public DateTimeOffset TestOffSet([FromBody]DateTimeOffset? d)
        {
            if(d.HasValue) 
                return d.Value.AddDays(1);
            return DateTimeOffset.UtcNow;
        }

        [HttpPost]
        [Route("TestDate")]
        public DateTime TestDate([FromBody]DateTime? d)
        {
            if (d.HasValue)
                return d.Value.AddDays(1);
            //d.Value.ToUniversalTime
            return DateTime.UtcNow;
        }

        [HttpPost]
        [Route("Now")]
        public DateTime Testnow([FromBody]DateTime? d)
        {
            return DateTime.Now;
        }



		[HttpPost]
		[ResponseType(typeof(ProductCreateResponse))]
		[Route("create")]
		public HttpResponseMessage Create([FromBody]ProductCreateRequest request)
		{
			var response = _businessAdminExternalCommandService.Execute<ProductCreateRequest, ProductCreateResponse, ProductCreateCommandBase>(request);
			return Request.CreateResponse(response);
		}

		[HttpPut]
		[ResponseType(typeof(ProductUpdateResponse))]
		[Route("{Id:int}")]
		public HttpResponseMessage Update(int id, [FromBody]ProductUpdateRequest request)
		{
			var response = _businessAdminExternalCommandService.Execute<ProductUpdateRequest, ProductUpdateResponse, ProductUpdateCommand>(request);
			return Request.CreateResponse(response);
		}

		[HttpPost]
		[ResponseType(typeof(ProductDeleteResponse))]
		[Route("{Id:int}/delete")]
		public HttpResponseMessage Delete([FromBody]ProductDeleteRequest request)
		{
			var response = _businessAdminExternalCommandService.Execute<ProductDeleteRequest, ProductDeleteResponse, ProductDeleteCommand>(request);
			return Request.CreateResponse(response);
		}

		[HttpGet]
		[ResponseType(typeof(ProductGetByIdResponse))]
		[Route("{Id:int}")]
		public HttpResponseMessage GetById([FromUri]ProductGetByIdRequest request)
		{
			var response = _businessAdminExternalCommandService.Execute<ProductGetByIdRequest, ProductGetByIdResponse, ProductGetByIdCommandBase>(request);
			return Request.CreateResponse(response);
		}

		[HttpGet]
		[ResponseType(typeof(ProductFindResponse))]
		[Route("")]
		public HttpResponseMessage Find([FromUri]ProductFindRequest request)
		{
			var response = _businessAdminExternalCommandService.Execute<ProductFindRequest, ProductFindResponse, ProductFindCommand>(request);
			return Request.CreateResponse(response);
		}
	}
}