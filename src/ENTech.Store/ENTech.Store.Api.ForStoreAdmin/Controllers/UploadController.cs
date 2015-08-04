using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using ENTech.Store.Services.CommandService.Definition;
using ENTech.Store.Services.Misc;

using ENTech.Store.Services.UploadModule.Requests;
using ENTech.Store.Services.UploadModule.Responses;
using ENTech.Store.Services.UploadModule.Commands;
using System.IO;



namespace ENTech.Store.Api.ForStoreAdmin.Controllers
{
    [RoutePrefix("1.0/store-admin-api/uploads")]
    public class UploadController : ApiController
    {

        //POST uploads - create
        //PUT uploads/{id} 

        private readonly IExternalCommandService<BusinessAdminSecurityInformation> _businessAdminExternalCommandService;

        [HttpPost]
        [Route("1.0/uploads")]
        [ResponseType(typeof(UploadCreateResponse))]
        public async Task<UploadCreateResponse> Create(UploadCreateRequest createUploadRequest)
        {
            var result = _businessAdminExternalCommandService.Execute<UploadCreateRequest, UploadCreateResponse, UploadCreateCommandBase>(createUploadRequest);
            return result;
        }

        [HttpPut]
        [Route("1.0/uploads/{id}")]
        [ResponseType(typeof(UploadUpdateResponse))]
        public async Task PutFile(int id)
        {
            var files = System.Web.HttpContext.Current.Request.Files;

            if (files.Count == 0)
                throw new Exception("No file in http request");

            if (files.Count > 1)
                throw new Exception("Uploading more than 2 files in one http request - forbidden");

            using (var s = await this.Request.Content.ReadAsStreamAsync())
            {
                var ext = Path.GetExtension(files[0].FileName);
                var saveUploadResponse = new UploadUpdateRequest
                {
                    Id = id,
                    Extension = ext,
                    Stream = s
                };

                _businessAdminExternalCommandService.Execute<UploadUpdateRequest, UploadUpdateResponse, UploadUpdateCommandBase>(saveUploadResponse);
            }
        }

        [HttpGet]
        [Route("1.0/uploads/{id}")]
        [ResponseType(typeof(UploadGetByIdResponse))]
        public async Task<object> GetById(int id)
        {
            var getUploadbyIdRequest = new UploadGetByIdRequest
            {
                Id = id
            };

            var upload = _businessAdminExternalCommandService.Execute<UploadGetByIdRequest, UploadGetByIdResponse, UploadGetByIdCommandBase>(getUploadbyIdRequest);
            return upload;
        }
    }
}
