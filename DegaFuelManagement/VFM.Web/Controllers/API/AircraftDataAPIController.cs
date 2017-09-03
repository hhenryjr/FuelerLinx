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
    [RoutePrefix("api/AircraftData")]
    public class AircraftDataAPIController : ApiController
    {
        //[Route, HttpPost]
        //public HttpResponseMessage AddAircraftData(AddAircraftDataRequest model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        //    }

        //    ItemResponse<int> response = new ItemResponse<int>();
        //    response.Item = AircraftDataService.UpdateAircraftData(model);
        //    return Request.CreateResponse(response);
        //}

        //[Route("{id:int}"), HttpPut]
        //public HttpResponseMessage UpdateAircraftData(UpdateAircraftDataRequest model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        //    }

        //    SuccessResponse response = new SuccessResponse();
        //    AircraftDataService.UpdateAircraftData(model);
        //    return Request.CreateResponse(response);
        //}

        [Route("{id:int}"), HttpGet]
        public HttpResponseMessage GetAircraftData(int id)
        {
            ItemResponse<AircraftData> response = new ItemResponse<AircraftData>();
            response.Item = AircraftDataService.GetAircraftData(id);
            return Request.CreateResponse(response);
        }

        [Route, HttpGet]
        public HttpResponseMessage GetListOfAircraftData()
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemResponse<string> response = new ItemResponse<string>();
            response.Item = AircraftDataService.GetListOfAircraftData();
            return Request.CreateResponse(response);
        }

        [Route("{id:int}"), HttpDelete]
        public HttpResponseMessage DeleteAircraftData(int id)
        {
            SuccessResponse response = new SuccessResponse();
            AircraftDataService.DeleteAircraftData(id);
            return Request.CreateResponse(response);
        }

    }
}