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
    [RoutePrefix("api/Registration")]
    public class RegistrationAPIController : ApiController
    {
        [Route, HttpPost]
        public HttpResponseMessage AddRegistration(AddRegistrationRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            
            if (RegistrationService.CheckUsername(0, model.Username))
            {
                string message = "This username is already being used.";
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, message);
            }

            ItemResponse<int> response = new ItemResponse<int>();
            response.Item = RegistrationService.UpdateRegistration(model);
            return Request.CreateResponse(response);
        }

        [Route("{id:int}"), HttpPut]
        public HttpResponseMessage UpdateRegistration(UpdateRegistrationRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            //if (RegistrationService.CheckUsername(model.Username))
            //{
            //    string message = "This username is already being used.";
            //    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, message);
            //}
            
            SuccessResponse response = new SuccessResponse();
            RegistrationService.UpdateRegistration(model);
            return Request.CreateResponse(response);
        }

        [Route("{id:int}"), HttpGet]
        public HttpResponseMessage GetRegistration(int id)
        {
            ItemResponse<Registration> response = new ItemResponse<Registration>();
            response.Item = RegistrationService.GetRegistration(id);
            return Request.CreateResponse(response);
        }

        [Route, HttpGet]
        public HttpResponseMessage GetListOfRegistration()
        {
            ItemsResponse<Registration> response = new ItemsResponse<Registration>();
            response.Items = RegistrationService.GetRegistrationList();
            return Request.CreateResponse(response);
        }

        [Route("{id:int}"), HttpDelete]
        public HttpResponseMessage DeleteRegistration(int id)
        {
            SuccessResponse response = new SuccessResponse();
            RegistrationService.DeleteRegistration(id);
            return Request.CreateResponse(response);
        }

        [Route("Detail/{id:int}"), HttpGet]
        public HttpResponseMessage GetDetailedRegInfo(int id)
        {
            ItemResponse<Registration> response = new ItemResponse<Registration>();
            response.Item = RegistrationService.GetDetailedRegInfo(id);
            return Request.CreateResponse(response);
        }

        [Route("CheckUsername"), HttpPost]
        public HttpResponseMessage CheckUsername(UpdateRegistrationRequest model)
        {
            SuccessResponse response = new SuccessResponse();
            response.IsDuplicateUsername = RegistrationService.CheckUsername(model.Id, model.Username);
            return Request.CreateResponse(response);
        }
    }
}
