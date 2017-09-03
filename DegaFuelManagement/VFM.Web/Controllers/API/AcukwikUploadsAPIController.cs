using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VFM.Web.Models.Responses;
using VFM.Web.Services;
using VFMClasses;

namespace VFM.Web.Controllers.API
{
    [RoutePrefix("api/AcukwikUploads")]
    public class AcukwikUploadsAPIController : ApiController
    {
        [Route("{name:alpha}"), HttpGet]
        public HttpResponseMessage GetDetail(string name)
        {
            ItemResponse<AcukwikUploads> response = new ItemResponse<AcukwikUploads>();
            response.Item = AcukwikUploadsService.GetAcukwikUpload(name);
            return Request.CreateResponse(response);
        }
    }
}
