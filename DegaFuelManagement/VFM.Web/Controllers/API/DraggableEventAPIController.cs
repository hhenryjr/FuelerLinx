using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VFM.Web.Models.Requests;
using VFM.Web.Models.Responses;
using VFM.Web.Services;
using VFMClasses.Calendar;

namespace VFM.Web.Controllers.API
{
    [RoutePrefix("api/Draggable")]
    public class DraggableEventAPIController : ApiController
    {
        [Route, HttpGet]
        public HttpResponseMessage Load()
        {
            //ItemsResponse<DraggableEvent> response = new ItemsResponse<DraggableEvent>();
            //response.Items = DraggableEventService.LoadDraggable();
            ItemResponse<DraggableEvent> response = new ItemResponse<DraggableEvent>();
            return Request.CreateResponse(response);
        }

        [Route, HttpPost]
        public HttpResponseMessage UpdateDraggable(AddDraggableEventRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemResponse<DraggableEvent> response = new ItemResponse<DraggableEvent>();
            response.Item = DraggableEventService.UpdateEvent(model);
            return Request.CreateResponse(response);
        }

        [Route("{clientID:int}/{userID:int}"), HttpGet]
        public HttpResponseMessage Load(int clientID, int userID)
        {
            ItemsResponse<DraggableEvent> response = new ItemsResponse<DraggableEvent>();
            response.Items = DraggableEventService.Load(clientID, userID);
            return Request.CreateResponse(response);
        }

        [Route("{eventId:int}"), HttpDelete]
        public HttpResponseMessage DeleteDraggable(int eventId)
        {
            SuccessResponse response = new SuccessResponse();
            DraggableEventService.Delete(eventId);
            return Request.CreateResponse(response);
        }
    }
}
