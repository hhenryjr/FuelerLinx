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
    [RoutePrefix("api/Users")]
    public class UsersAPIController : ApiController
    {
        [Route, HttpPost]
        public HttpResponseMessage AddUser(AddUserRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemResponse<int> response = new ItemResponse<int>();
            response.Item = UsersService.UpdateUser(model);
            return Request.CreateResponse(response);
        }

        [Route("{id:int}"), HttpPut]
        public HttpResponseMessage UpdateUser(UpdateUserRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            SuccessResponse response = new SuccessResponse();
            UsersService.UpdateUser(model);
            return Request.CreateResponse(response);
        }

        [Route("{id:int}"), HttpGet]
        public HttpResponseMessage GetUser(int id)
        {
            ItemResponse<Users> response = new ItemResponse<Users>();
            response.Item = UsersService.GetUser(id);
            return Request.CreateResponse(response);
        }

        [Route, HttpGet]
        public HttpResponseMessage GetListOfUsers()
        {
            ItemsResponse<Users> response = new ItemsResponse<Users>();
            response.Items = UsersService.GetListOfUsers();
            return Request.CreateResponse(response);
        }

        [Route("Client/{id:int}"), HttpGet]
        public HttpResponseMessage GetListOfUsers(int id)
        {
            ItemsResponse<Users> response = new ItemsResponse<Users>();
            response.Items = UsersService.GetUsersByClient(id);
            return Request.CreateResponse(response);
        }

        [Route("{id:int}"), HttpDelete]
        public HttpResponseMessage DeleteUser(int id)
        {
            SuccessResponse response = new SuccessResponse();
            UsersService.DeleteUser(id);
            return Request.CreateResponse(response);
        }

    }
}
