using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VFM.API.EntityServices;
using VFM.EDM;
using VFM.Web.Models.Responses;

namespace VFM.Web.Controllers.API
{
    [RoutePrefix("api/permissions")]
    public class LoginPermissionsAPIController : ApiController
    {
        [Route, HttpPost]
        public HttpResponseMessage Add(LoginPermission permission)
        {
            ItemResponse<LoginPermission> response = new ItemResponse<LoginPermission>();
            LoginPermissions permissions = new LoginPermissions();
            response.Item = permissions.Update(permission);
            return Request.CreateResponse(response);
        }

        [Route("{id:int}"), HttpPut]
        public HttpResponseMessage Update(LoginPermission permission)
        {
            ItemResponse<LoginPermission> response = new ItemResponse<LoginPermission>();
            LoginPermissions permissions = new LoginPermissions();
            response.Item = permissions.Update(permission);
            return Request.CreateResponse(response);
        }

        [Route("{id:int}"), HttpGet]
        public HttpResponseMessage GetByID(int id)
        {
            ItemResponse<LoginPermission> response = new ItemResponse<LoginPermission>();
            LoginPermissions permissions = new LoginPermissions();
            response.Item = permissions.GetById(id);
            return Request.CreateResponse(response);
        }

        [Route("user/{userId:int}"), HttpGet]
        public HttpResponseMessage GetByUserID(int userId)
        {
            ItemResponse<LoginPermission> response = new ItemResponse<LoginPermission>();
            LoginPermissions permissions = new LoginPermissions();
            response.Item = permissions.GetByUserID(userId);
            return Request.CreateResponse(response);
        }

        [Route, HttpDelete]
        public void Delete(LoginPermission permission)
        {
            LoginPermissions permissions = new LoginPermissions();
            permissions.Delete(permission);
        }
    }
}
