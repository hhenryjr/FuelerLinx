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
    [RoutePrefix("api/FuelOrderNotes")]
    public class FuelOrderNotesAPIController : ApiController
    {
        [Route, HttpPost]
        public HttpResponseMessage AddFuelOrderNote(AddFuelOrderNoteRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemResponse<int> response = new ItemResponse<int>();
            response.Item = FuelOrderNotesService.UpdateFuelOrderNote(model);
            return Request.CreateResponse(response);
        }

        [Route("{id:int}"), HttpPut]
        public HttpResponseMessage UpdateFuelOrderNote(UpdateFuelOrderNoteRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            SuccessResponse response = new SuccessResponse();
            FuelOrderNotesService.UpdateFuelOrderNote(model);
            return Request.CreateResponse(response);
        }

        [Route("{id:int}"), HttpGet]
        public HttpResponseMessage GetFuelOrderNote(int id)
        {
            ItemResponse<FuelOrderNotes> response = new ItemResponse<FuelOrderNotes>();
            response.Item = FuelOrderNotesService.GetFuelOrderNote(id);
            return Request.CreateResponse(response);
        }

        [Route, HttpGet]
        public HttpResponseMessage GetListOfFuelOrderNotes()
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemsResponse<FuelOrderNotes> response = new ItemsResponse<FuelOrderNotes>();
            response.Items = FuelOrderNotesService.GetListOfFuelOrderNotes();
            return Request.CreateResponse(response);
        }

        [Route("{id:int}"), HttpDelete]
        public HttpResponseMessage DeleteFuelOrderNote(int id)
        {
            SuccessResponse response = new SuccessResponse();
            FuelOrderNotesService.DeleteFuelOrderNote(id);
            return Request.CreateResponse(response);
        }

    }
}
