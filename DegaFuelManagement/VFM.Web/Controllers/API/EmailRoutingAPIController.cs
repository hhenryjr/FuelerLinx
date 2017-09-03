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
    [RoutePrefix("api/EmailRouting")]
    public class EmailRoutingAPIController : ApiController
    {
        [Route, HttpPost]
        public HttpResponseMessage AddEmail(AddEmailRoutingRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemResponse<int> response = new ItemResponse<int>();
            response.Item = EmailRoutingService.UpdateEmail(model);
            return Request.CreateResponse(response);
        }

        [Route("{id:int}"), HttpPut]
        public HttpResponseMessage UpdateEmail(UpdateEmailRoutingRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            SuccessResponse response = new SuccessResponse();
            EmailRoutingService.UpdateEmail(model);
            return Request.CreateResponse(response);
        }

        [Route("{id:int}"), HttpGet]
        public HttpResponseMessage GetEmail(int id)
        {
            ItemResponse<EmailRouting> response = new ItemResponse<EmailRouting>();
            response.Item = EmailRoutingService.GetEmail(id);
            return Request.CreateResponse(response);
        }

        [Route, HttpGet]
        public HttpResponseMessage GetEmailList()
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemsResponse<EmailRouting> response = new ItemsResponse<EmailRouting>();
            response.Items = EmailRoutingService.GetListOfEmails();
            return Request.CreateResponse(response);
        }

        [Route("Admin/{clientId:int}"), HttpGet]
        public HttpResponseMessage GetEmailList(int clientId)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemsResponse<EmailRouting> response = new ItemsResponse<EmailRouting>();
            response.Items = EmailRoutingService.GetEmailsByAdminClient(clientId);
            return Request.CreateResponse(response);
        }

        [Route("{id:int}"), HttpDelete]
        public HttpResponseMessage DeleteEmail(int id)
        {
            SuccessResponse response = new SuccessResponse();
            EmailRoutingService.DeleteEmail(id);
            return Request.CreateResponse(response);
        }

    }
}
