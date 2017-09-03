using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VFM.Web.Models.Responses;
using VFM.Web.Models.Requests;
using VFM.Web.Services;
using VFMClasses;

namespace VFM.Web.Controllers.API
{
    [RoutePrefix("api/Clients")]
    public class ClientsAPIController : ApiController
    {
        [Route, HttpPost]
        public HttpResponseMessage AddClient(AddClientRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemResponse<int> response = new ItemResponse<int>();
            response.Item = ClientsService.UpdateClient(model);
            return Request.CreateResponse(response);
        }

        [Route("{id:int}"), HttpPut]
        public HttpResponseMessage UpdateClient(UpdateClientRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            SuccessResponse response = new SuccessResponse();
            ClientsService.UpdateClient(model);
            return Request.CreateResponse(response);
        }

        [Route("{id:int}"), HttpGet]
        public HttpResponseMessage GetClient(int id)
        {
            ItemResponse<Clients> response = new ItemResponse<Clients>();
            response.Item = ClientsService.GetClient(id);
            return Request.CreateResponse(response);
        }


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

        [Route("info/{id:int}"), HttpGet]
        public HttpResponseMessage GetClientInfo(int id)
        {
            ItemResponse<Clients> response = new ItemResponse<Clients>();
            response.Item = ClientsService.GetDetailedClientInfo(id);
            return Request.CreateResponse(response);
        }
    }
}
