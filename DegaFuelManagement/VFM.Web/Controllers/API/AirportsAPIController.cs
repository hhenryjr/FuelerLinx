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
    [RoutePrefix("api/Airports")]
    public class AirportsAPIController : ApiController
    {
        [Route("{id:int}"), HttpGet]
        public HttpResponseMessage GetAirport(int id)
        {
            ItemResponse<Airports> response = new ItemResponse<Airports>();
            response.Item = AirportsService.GetAirport(id);
            return Request.CreateResponse(response);
        }

        [Route, HttpGet]
        public HttpResponseMessage GetListOfAirports()
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemResponse<string> response = new ItemResponse<string>();
            response.Item = AirportsService.GetAirportAutoCompleteList();
            //response.Items = AirportsService.GetListOfAirports();
            return Request.CreateResponse(response);
        }
    }
}
