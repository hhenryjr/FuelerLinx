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
    [RoutePrefix("api/Distribution")]
    public class DistributionAPIController : ApiController
    {
        [Route("Company/{adminId:int}/{clientId:int}"), HttpGet]
        public HttpResponseMessage DistributeCompany(int adminId, int clientId)
        {
            bool response = DistributionService.DistributeCompany(adminId, clientId);
            return Request.CreateResponse(response);
        }

        [Route("Contact/{adminId:int}/{clientId:int}"), HttpGet]
        public HttpResponseMessage DistributeContact(int adminId, int clientId)
        {
            bool response = DistributionService.DistributeContact(adminId, clientId);
            return Request.CreateResponse(response);
        }

        [Route("Margins/{adminId:int}"), HttpGet]
        public HttpResponseMessage DistributeAllMargins(int adminId)
        {
            SuccessResponse response = new SuccessResponse();
            DistributionService.DistributeAllMargins(adminId);
            return Request.CreateResponse(response);
        }

        [Route("Margins/{adminId:int}/{clientId:int}"), HttpGet]
        public HttpResponseMessage DistributeMargin(int adminId, int clientId)
        {
            SuccessResponse response = new SuccessResponse();
            DistributionService.DistributeMargin(adminId, clientId);
            return Request.CreateResponse(response);
        }
    }
}
