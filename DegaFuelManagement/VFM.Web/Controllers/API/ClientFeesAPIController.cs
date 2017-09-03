using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VFM.Web.Models.Requests;
using VFM.Web.Models.Responses;
using VFM.Web.Services;
using VFMClasses;

namespace VFM.Web.Controllers.API
{
    [RoutePrefix("api/ClientFees")]
    public class ClientFeesAPIController : ApiController
    {
        [Route, HttpPost]
        public HttpResponseMessage AddClientFee(AddClientFeeRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemResponse<ClientFees> response = new ItemResponse<ClientFees>();
            response.Item = ClientFeesService.UpdateFee(model);
            return Request.CreateResponse(response);
        }

        [Route("{id:int}"), HttpPut]
        public HttpResponseMessage UpdateClientFee(UpdateClientFeeRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            SuccessResponse response = new SuccessResponse();
            ClientFeesService.UpdateFee(model);
            return Request.CreateResponse(response);
        }

        [Route("{clientID:int}/{taxDesc}"), HttpDelete]
        public HttpResponseMessage DeleteClientFee(int clientID, string taxDesc)
        {
            SuccessResponse response = new SuccessResponse();
            ClientFeesService.DeleteClientFee(clientID, taxDesc);
            return Request.CreateResponse(response);
        }
    }
}
