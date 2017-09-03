using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VFM.Web.Models.Responses;
using VFM.Web.Services;

namespace VFM.Web.Controllers.API
{
    [RoutePrefix("api/master")]
    public class MasterAPIController : ApiController
    {
        [Route("check"), HttpPost]
        public bool IsUserStillLoggedIn()
        {
            bool response;
            return response = MasterSvc.IsUserStillLoggedIn();
            //ItemResponse<bool> response = new ItemResponse<bool>();
            //return Request.CreateErrorResponse(response);
        }
    }
}
