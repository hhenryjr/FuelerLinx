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
    [RoutePrefix("api/ContactUs")]
    public class ContactUsAPIController : ApiController
    {
        [Route, HttpPost]
        public HttpResponseMessage AddContactUs(AddContactUsRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemResponse<int> response = new ItemResponse<int>();
            response.Item = ContactUsService.UpdateContactUs(model);
            return Request.CreateResponse(response);
        }

        [Route("{id:int}"), HttpPut]
        public HttpResponseMessage UpdateContactUs(UpdateContactUsRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            SuccessResponse response = new SuccessResponse();
            ContactUsService.UpdateContactUs(model);
            return Request.CreateResponse(response);
        }

        [Route("{id:int}"), HttpGet]
        public HttpResponseMessage GetContactUs(int id)
        {
            ItemResponse<ContactUs> response = new ItemResponse<ContactUs>();
            response.Item = ContactUsService.GetContactUs(id);
            return Request.CreateResponse(response);
        }

        [Route, HttpGet]
        public HttpResponseMessage GetListOfContactUs()
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemsResponse<ContactUs> response = new ItemsResponse<ContactUs>();
            response.Items = ContactUsService.GetListOfContactUs();
            return Request.CreateResponse(response);
        }

        [Route("{id:int}"), HttpDelete]
        public HttpResponseMessage DeleteContactUs(int id)
        {
            SuccessResponse response = new SuccessResponse();
            ContactUsService.DeleteContactUs(id);
            return Request.CreateResponse(response);
        }
    }
}
