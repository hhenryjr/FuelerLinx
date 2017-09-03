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
    [RoutePrefix("api/CustomerDetailsCustomFields")]
    public class CustomerDetailsCustomFieldsAPIController : ApiController
    {
        [Route, HttpPost]
        public HttpResponseMessage AddCustomField(AddCustomerDetailsCustomFieldRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemResponse<int> response = new ItemResponse<int>();
            response.Item = CustomerDetailsCustomFieldsService.UpdateCustomerDetailsCustomField(model);
            //ItemsResponse<int> response = new ItemsResponse<int>();
            //foreach (AddCustomerDetailsCustomFieldRequest model in models)
            //{
            //    response.Items.Add(CustomerDetailsCustomFieldsService.UpdateCustomerDetailsCustomField(model));
            //}
            return Request.CreateResponse(response);
        }

        [Route("{id:int}"), HttpPut]
        public HttpResponseMessage UpdateCustomField(UpdateCustomerDetailsCustomFieldRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            SuccessResponse response = new SuccessResponse();
            CustomerDetailsCustomFieldsService.UpdateCustomerDetailsCustomField(model);
            return Request.CreateResponse(response);
        }

        [Route("{id:int}"), HttpGet]
        public HttpResponseMessage GetCustomField(int id)
        {
            ItemResponse<CustomerDetailsCustomFields> response = new ItemResponse<CustomerDetailsCustomFields>();
            response.Item = CustomerDetailsCustomFieldsService.GetCustomerDetailsCustomField(id);
            return Request.CreateResponse(response);
        }

        [Route("{adminId:int}/{custId:int}"), HttpGet]
        public HttpResponseMessage GetCustomerFields(int adminId, int custId)
        {
            ItemsResponse<CustomerDetailsCustomFields> response = new ItemsResponse<CustomerDetailsCustomFields>();
            response.Items = CustomerDetailsCustomFieldsService.GetCustomFields(adminId, custId);
            return Request.CreateResponse(response);
        }

        [Route("{id:int}"), HttpDelete]
        public HttpResponseMessage DeleteCustomField(int id)
        {
            SuccessResponse response = new SuccessResponse();
            CustomerDetailsCustomFieldsService.DeleteCustomerDetailsCustomField(id);
            return Request.CreateResponse(response);
        }
    }
}
