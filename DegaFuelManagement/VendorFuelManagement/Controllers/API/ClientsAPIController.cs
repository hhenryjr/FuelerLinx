using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VendorFuelManagement.Models.Responses;
using VendorFuelManagement.Services;
using VFMClasses;

namespace VendorFuelManagement.Controllers.API
{
    [RoutePrefix("api/Clients")]
    public class ClientsAPIController : ApiController
    {
        [Route, HttpGet]
        public HttpResponseMessage GetClientList()
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemsResponse<Clients> response = new ItemsResponse<Clients>();
            response.Items = ClientsService.GetListOfClients();
            return Request.CreateResponse(response);
        }

        [Route("{id:int}"), HttpGet]
        public HttpResponseMessage GetClientInfo(int id)
        {
            ItemResponse<Clients> response = new ItemResponse<Clients>();
            response.Item = ClientsService.GetDetailedClientInfo(id);
            return Request.CreateResponse(response);
        }
    }
}
