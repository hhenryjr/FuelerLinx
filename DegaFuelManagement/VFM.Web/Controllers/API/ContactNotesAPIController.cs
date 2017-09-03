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
    [RoutePrefix("api/ContactNotes")]
    public class ContactNotesAPIController : ApiController
    {
        [Route, HttpPost]
        public HttpResponseMessage AddContactNote(AddContactNoteRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemResponse<int> response = new ItemResponse<int>();
            response.Item = ContactNotesService.UpdateContactNote(model);
            return Request.CreateResponse(response);
        }

        [Route("{id:int}"), HttpPut]
        public HttpResponseMessage UpdateContactNote(UpdateContactNoteRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            SuccessResponse response = new SuccessResponse();
            ContactNotesService.UpdateContactNote(model);
            return Request.CreateResponse(response);
        }

        [Route("{id:int}"), HttpGet]
        public HttpResponseMessage GetContactNote(int id)
        {
            ItemResponse<ContactNotes> response = new ItemResponse<ContactNotes>();
            response.Item = ContactNotesService.GetContactNote(id);
            return Request.CreateResponse(response);
        }

        [Route, HttpGet]
        public HttpResponseMessage GetListOfContactNotes()
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemsResponse<ContactNotes> response = new ItemsResponse<ContactNotes>();
            response.Items = ContactNotesService.GetListOfContactNotes();
            return Request.CreateResponse(response);
        }

        [Route("{id:int}"), HttpDelete]
        public HttpResponseMessage DeleteContactNote(int id)
        {
            SuccessResponse response = new SuccessResponse();
            ContactNotesService.DeleteContactNote(id);
            return Request.CreateResponse(response);
        }

    }
}
