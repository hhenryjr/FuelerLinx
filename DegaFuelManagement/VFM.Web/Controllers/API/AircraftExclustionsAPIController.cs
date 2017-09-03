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
    [RoutePrefix("api/AircraftExclusions")]
    public class AircraftExclusionsAPIController : ApiController
    {
        [Route("Add"), HttpPost]
        public HttpResponseMessage AddAircraftExclusion(AddAircraftExclusionRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemResponse<int> response = new ItemResponse<int>();
            response.Item = AircraftExclusionsService.UpdateAircraftExclusion(model);
            return Request.CreateResponse(response);
        }

        [Route("AddList"), HttpPost]
        public HttpResponseMessage AddListOfExclusions(AddAircraftExclusionRequest[] models)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemsResponse<AircraftExclusions> response = new ItemsResponse<AircraftExclusions>();
            foreach (AddAircraftExclusionRequest model in models)
            {
                response.Items.Add(AircraftExclusionsService.UpdateAircraftExclusions(model));
            }
            return Request.CreateResponse(response);
        }

        [Route("{id:int}"), HttpPut]
        public HttpResponseMessage UpdateAircraftExclusion(AddAircraftExclusionRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            SuccessResponse response = new SuccessResponse();
            AircraftExclusionsService.UpdateAircraftExclusion(model);
            return Request.CreateResponse(response);
        }

        //[Route("{id:int}"), HttpGet]
        //public HttpResponseMessage GetAircraftExclusion(int id)
        //{
        //    ItemResponse<AircraftExclusions> response = new ItemResponse<AircraftExclusions>();
        //    response.Item = AircraftExclusionsService.GetAircraftExclusion(id);
        //    return Request.CreateResponse(response);
        //}

        //[Route, HttpGet]
        //public HttpResponseMessage GetListOfAircraftExclusions()
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        //    }

        //    ItemsResponse<AircraftExclusions> response = new ItemsResponse<AircraftExclusions>();
        //    response.Items = AircraftExclusionsService.GetListOfAircraftExclusions();
        //    return Request.CreateResponse(response);
        //}

        [Route, HttpPost]
        public HttpResponseMessage GetAircraftExclusions(GetAircraftExclusionsRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemsResponse<AircraftExclusions> response = new ItemsResponse<AircraftExclusions>();
            response.Items = AircraftExclusionsService.GetAircraftExclusionsByICAOAndFBOAndAdminClient(model.ICAO, model.FBO, model.AdminClientID);
            return Request.CreateResponse(response);
        }

        [Route("{id:int}"), HttpDelete]
        public HttpResponseMessage DeleteAircraftExclusion(int id)
        {
            SuccessResponse response = new SuccessResponse();
            AircraftExclusionsService.DeleteAircraftExclusion(id);
            return Request.CreateResponse(response);
        }

    }
}