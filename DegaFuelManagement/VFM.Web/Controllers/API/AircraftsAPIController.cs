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
    [RoutePrefix("api/Aircrafts")]
    public class AircraftsAPIController : ApiController
    {
        [Route, HttpPost]
        public HttpResponseMessage AddAircraft(AddAircraftRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemResponse<int> response = new ItemResponse<int>();
            response.Item = AircraftsService.UpdateAircraft(model);
            return Request.CreateResponse(response);
        }

        [Route("AddList"), HttpPost]
        public HttpResponseMessage AddListOfAircrafts(AddAircraftRequest[] models)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemResponse<int> response = new ItemResponse<int>();
            foreach (AddAircraftRequest model in models)
            {
            response.Item = AircraftsService.UpdateAircraft(model);
            }
            return Request.CreateResponse(response);
        }

        [Route("{id:int}"), HttpPut]
        public HttpResponseMessage UpdateAircraft(UpdateAircraftRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            SuccessResponse response = new SuccessResponse();
            AircraftsService.UpdateAircraft(model);
            return Request.CreateResponse(response);
        }

        [Route("{id:int}"), HttpGet]
        public HttpResponseMessage GetAircraft(int id)
        {
            ItemsResponse<Aircrafts> response = new ItemsResponse<Aircrafts>();
            response.Items = AircraftsService.GetAircraftsByCustClientID(id);
            return Request.CreateResponse(response);
        }

        [Route, HttpGet]
        public HttpResponseMessage GetListOfAircrafts()
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemsResponse<Aircrafts> response = new ItemsResponse<Aircrafts>();
            response.Items = AircraftsService.GetListOfAircrafts();
            return Request.CreateResponse(response);
        }

        [Route("{adminId:int}/{custId:int}"), HttpGet]
        public HttpResponseMessage GetAircraftsByAdminAndCustID(int adminId, int custId)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemsResponse<Aircrafts> response = new ItemsResponse<Aircrafts>();
            response.Items = AircraftsService.GetAircraftsByAdminAndClientID(adminId, custId);
            return Request.CreateResponse(response);
        }

        [Route("TailNumbers/{id:int}"), HttpGet]
        public HttpResponseMessage GetAircrafts(int id)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemsResponse<Aircrafts> response = new ItemsResponse<Aircrafts>();
            response.Items = AircraftsService.GetAircraftsByClientID(id);
            return Request.CreateResponse(response);
        }

        [Route("Remaining/{adminId:int}/{clientId:int}"), HttpPost]
        public HttpResponseMessage GetRemainingAircrafts(int adminId, int clientId, GetFBODetailsRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemsResponse<Aircrafts> response = new ItemsResponse<Aircrafts>();
            response.Items = AircraftsService.GetRemainingAircrafts(adminId, clientId, model.FBO, model.ICAO);
            return Request.CreateResponse(response);
        }

        [Route, HttpDelete]
        public HttpResponseMessage DeleteAircraft(UpdateAircraftRequest[] models)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            SuccessResponse response = new SuccessResponse();
            foreach (UpdateAircraftRequest model in models)
            {
            AircraftsService.DeleteAircraft(model.Id);
            }
            return Request.CreateResponse(response);
        }

    }
}