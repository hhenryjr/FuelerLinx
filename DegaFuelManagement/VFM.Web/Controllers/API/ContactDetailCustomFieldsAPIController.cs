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
    [RoutePrefix("api/ContactDetailCustomFields")]
    public class ContactDetailCustomFieldsAPIController : ApiController
    {
        [Route, HttpPost]
        public HttpResponseMessage AddCustomField(AddContactDetailCustomFieldRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemResponse<int> response = new ItemResponse<int>();
            response.Item = ContactDetailCustomFieldsService.UpdateContactDetailCustomField(model);
            //ItemsResponse<int> response = new ItemsResponse<int>();
            //foreach (AddContactDetailCustomFieldRequest model in models)
            //{
            //    response.Items.Add(ContactDetailCustomFieldsService.UpdateContactDetailCustomField(model));
            //}
            return Request.CreateResponse(response);
        }

        [Route("{id:int}"), HttpPut]
        public HttpResponseMessage UpdateCustomField(UpdateContactDetailCustomFieldRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            SuccessResponse response = new SuccessResponse();
            ContactDetailCustomFieldsService.UpdateContactDetailCustomField(model);
            return Request.CreateResponse(response);
        }

        //[Route("{id:int}"), HttpGet]
        //public HttpResponseMessage GetCustomField(int id)
        //{
        //    ItemResponse<ContactDetailCustomFields> response = new ItemResponse<ContactDetailCustomFields>();
        //    response.Item = ContactDetailCustomFieldsService.GetContactDetailCustomField(id);
        //    return Request.CreateResponse(response);
        //}

        [Route("{contactId:int}"), HttpGet]
        public HttpResponseMessage GetCustomerFields(int contactId)
        {
            ItemsResponse<ContactDetailCustomFields> response = new ItemsResponse<ContactDetailCustomFields>();
            response.Items = ContactDetailCustomFieldsService.GetCustomFields(contactId);
            return Request.CreateResponse(response);
        }

        [Route("{id:int}"), HttpDelete]
        public HttpResponseMessage DeleteCustomField(int id)
        {
            SuccessResponse response = new SuccessResponse();
            ContactDetailCustomFieldsService.DeleteContactDetailCustomField(id);
            return Request.CreateResponse(response);
        }
    }
}
