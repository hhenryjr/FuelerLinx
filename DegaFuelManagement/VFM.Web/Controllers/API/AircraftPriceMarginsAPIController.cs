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
    [RoutePrefix("api/AircraftPriceMargins")]
    public class AircraftPriceMarginsAPIController : ApiController
    {
        [Route, HttpPost]
        public HttpResponseMessage AddAircraftPriceMargin(AddAircraftPriceMarginRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemResponse<int> response = new ItemResponse<int>();
            response.Item = AircraftPriceMarginsService.UpdateAircraftPriceMargin(model);
            return Request.CreateResponse(response);
        }

        [Route("{id:int}"), HttpPut]
        public HttpResponseMessage UpdateAircraftPriceMargin(UpdateAircraftPriceMarginRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            SuccessResponse response = new SuccessResponse();
            AircraftPriceMarginsService.UpdateAircraftPriceMargin(model);
            return Request.CreateResponse(response);
        }

        [Route("{id:int}"), HttpGet]
        public HttpResponseMessage GetAircraftPriceMargin(int id)
        {
            ItemResponse<AircraftPriceMarginsCollection> response = new ItemResponse<AircraftPriceMarginsCollection>();
            response.Item = AircraftPriceMarginsService.GetAircraftPriceMarginByAircraftID(id);
            return Request.CreateResponse(response);
        }

        [Route, HttpGet]
        public HttpResponseMessage GetListOfAircraftPriceMargins()
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemsResponse<AircraftPriceMargins> response = new ItemsResponse<AircraftPriceMargins>();
            response.Items = AircraftPriceMarginsService.GetListOfAircraftPriceMargins();
            return Request.CreateResponse(response);
        }

        [Route("{id:int}"), HttpDelete]
        public HttpResponseMessage DeleteAircraftPriceMargin(int id)
        {
            SuccessResponse response = new SuccessResponse();
            AircraftPriceMarginsService.DeleteAircraftPriceMargin(id);
            return Request.CreateResponse(response);
        }

    }
}
