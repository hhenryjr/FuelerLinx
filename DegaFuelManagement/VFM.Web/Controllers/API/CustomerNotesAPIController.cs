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
    [RoutePrefix("api/CustomerNotes")]
    public class CustomerNotesAPIController : ApiController
    {
        [Route, HttpPost]
        public HttpResponseMessage AddCustomerNote(AddCustomerNoteRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemResponse<int> response = new ItemResponse<int>();
            response.Item = CustomerNotesService.UpdateCustomerNote(model);
            return Request.CreateResponse(response);
        }

        [Route("{id:int}"), HttpPut]
        public HttpResponseMessage UpdateCustomerNote(UpdateCustomerNoteRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            SuccessResponse response = new SuccessResponse();
            CustomerNotesService.UpdateCustomerNote(model);
            return Request.CreateResponse(response);
        }

        [Route("{id:int}"), HttpGet]
        public HttpResponseMessage GetCustomerNote(int id)
        {
            ItemResponse<CustomerNotes> response = new ItemResponse<CustomerNotes>();
            response.Item = CustomerNotesService.GetCustomerNote(id);
            return Request.CreateResponse(response);
        }

        [Route, HttpGet]
        public HttpResponseMessage GetListOfCustomerNotes()
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemsResponse<CustomerNotes> response = new ItemsResponse<CustomerNotes>();
            response.Items = CustomerNotesService.GetListOfCustomerNotes();
            return Request.CreateResponse(response);
        }

        [Route("{id:int}"), HttpDelete]
        public HttpResponseMessage DeleteCustomerNote(int id)
        {
            SuccessResponse response = new SuccessResponse();
            CustomerNotesService.DeleteCustomerNote(id);
            return Request.CreateResponse(response);
        }

    }
}
