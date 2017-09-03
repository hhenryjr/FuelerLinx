using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VFM.Web.Models.Responses;
using VFM.Web.Services;
using VFMClasses;

namespace VFM.Web.Controllers.API
{
    [RoutePrefix("api/AcukwikAirports")]
    public class AcukwikAirportsAPIController : ApiController
    {
        [Route("{id:int}"), HttpGet]
        public HttpResponseMessage GetAirport(int id)
        {
            ItemResponse<AcukwikAirports> response = new ItemResponse<AcukwikAirports>();
            response.Item = AcukwikAirportsService.GetAirport(id);
            return Request.CreateResponse(response);
        }

        [Route, HttpGet]
        public HttpResponseMessage GetListOfAcukwikAirports()
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            //ItemResponse<string> response = new ItemResponse<string>();
            //response.Item = AcukwikAirportsService.GetAirportAutoCompleteList();
            ItemsResponse<AcukwikAirports> response = new ItemsResponse<AcukwikAirports>();
            response.Items = AcukwikAirportsService.GetListOfAcukwikAirports();
            return Request.CreateResponse(response);
        }

        [Route("Margins/{adminId:int}"), HttpGet]
        public HttpResponseMessage GetAcukwikAirportsAndMarginsByAdminClientID(int adminId)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemResponse<string> response = new ItemResponse<string>();
            response.Item = AcukwikAirportsService.GetAirportAutoCompleteListByAdminClientID(adminId);
            //ItemsResponse<AcukwikAirports> response = new ItemsResponse<AcukwikAirports>();
            //response.Items = AcukwikAirportsService.GetAirportAutoCompleteListByAdminClientID(adminId);
            return Request.CreateResponse(response);
        }
    }
}
