using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using ENTech.Store.Services.CommandService.Definition;
using ENTech.Store.Services.Misc;

using ENTech.Store.Services.UploadModule.Requests;
using ENTech.Store.Services.UploadModule.Responses;
using ENTech.Store.Services.UploadModule.Commands;



namespace ENTech.Store.Api.ForStoreAdmin.Controllers
{
    [RoutePrefix("1.0/store-admin-api/uploads")]
    public class UploadController
    {
        private readonly IExternalCommandService<BusinessAdminSecurityInformation> _businessAdminExternalCommandService;

        [HttpPost]
        public Task<int> CreateUpload()
        {
            //var createUploadRequest = new UploadCreateRequest();
            //var result = _businessAdminExternalCommandService.Execute<UploadCreateRequest, UploadCreateResponse, UploadCreateCommand>(createUploadRequest);
            //return//...
        }

        [HttpPost]
        public Task Save(int id)
        {
        }

        [HttpGet]
        public Task<object> GetUpload(int id)
        {}

    }
}
