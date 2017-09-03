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
    [RoutePrefix("api/ClientTaxes")]
    public class ClientTaxesAPIController : ApiController
    {
        [Route, HttpPost]
        public HttpResponseMessage AddClientTax(AddClientTaxRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemResponse<ClientTaxes> response = new ItemResponse<ClientTaxes>();
            response.Item = ClientTaxesService.UpdateTax(model);
            return Request.CreateResponse(response);
        }

        [Route("{id:int}"), HttpPut]
        public HttpResponseMessage UpdateClientTax(UpdateClientTaxRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            SuccessResponse response = new SuccessResponse();
            ClientTaxesService.UpdateTax(model);
            return Request.CreateResponse(response);
        }

        [Route("{clientID:int}/{taxDesc}"), HttpDelete]
        public HttpResponseMessage DeleteClientTax(int clientID, string taxDesc)
        {
            SuccessResponse response = new SuccessResponse();
            ClientTaxesService.DeleteClientTax(clientID, taxDesc);
            return Request.CreateResponse(response);
        }
    }
}
