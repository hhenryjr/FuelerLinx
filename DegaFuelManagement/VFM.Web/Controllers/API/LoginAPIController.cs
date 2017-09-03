using Degatech.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Degatech.Utilities.Exceptions;
using VFM.Web.Models;
using VFM.Web.Models.Requests;
using VFM.Web.Models.Responses;
using VFM.Web.Services;
using VFMClasses;

namespace VFM.Web.Controllers.API
{
    [RoutePrefix("api/login")]
    public class LoginAPIController : ApiController
    {
        [Route, HttpPost]
        public HttpResponseMessage Login(LoginRequest model)
        {
            HttpResponseMessage responseMessage = null;

            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            Logins login = LoginsService.Login(model.Username, model.Password);

            if (login.LoginResult == BaseClass.LoginResults.InvalidUsernamePassword)
            {
                BaseResponse response = new ErrorResponse("Login failed! Please check if you typed in the correct Username and Password.");
                responseMessage = Request.CreateResponse(HttpStatusCode.BadRequest, response);
            }
            if (login.LoginResult == BaseClass.LoginResults.InactiveAccount)
            {
                BaseResponse response = new ErrorResponse("Login failed! Your account has been deactivated.");
                responseMessage = Request.CreateResponse(HttpStatusCode.BadRequest, response);
            }
            if (login.LoginResult == BaseClass.LoginResults.Success)
            {
                ItemResponse<Users> response = new ItemResponse<Users>();
                response.Item = Users.CurrentUser;
                responseMessage = Request.CreateResponse(response);
            }

            return responseMessage;
        }

        [Route, HttpGet]
        public HttpResponseMessage Logout()
        {
            SuccessResponse response = new SuccessResponse();
            LoginsService.Logout();
            return Request.CreateResponse(response);
        }

        [Route("GetClientName"), HttpGet]
        public HttpResponseMessage GetClientName()
        {
            ItemResponse<string> response = new ItemResponse<string>();
            response.Item = Clients.GetClientName().Replace(" ", "");
            return Request.CreateResponse(response);
        }
    }
}
