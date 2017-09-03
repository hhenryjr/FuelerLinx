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
    [RoutePrefix("api/Calendar")]
    public class CalendarEventAPIController : ApiController
    {
        [Route, HttpGet]
        public HttpResponseMessage Load()
        {
            //ItemsResponse<CalendarEvent> response = new ItemsResponse<CalendarEvent>();
            //response.Items = CalendarEventService.LoadCalendar();
            ItemResponse<CalendarEvent> response = new ItemResponse<CalendarEvent>();
            return Request.CreateResponse(response);
        }

        [Route, HttpPost]
        public HttpResponseMessage UpdateCalendar(UpdateCalendarEventRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemResponse<CalendarEvent> response = new ItemResponse<CalendarEvent>();
            response.Item = CalendarEventService.UpdateCalendar(model);
            return Request.CreateResponse(response);
        }

        [Route("{clientID:int}/{userID:int}"), HttpGet]
        public HttpResponseMessage Load(int clientID, int userID)
        {
            ItemsResponse<CalendarEvent> response = new ItemsResponse<CalendarEvent>();
            response.Items = CalendarEventService.Load(clientID, userID);
            return Request.CreateResponse(response);
        }

        [Route("{clientID:int}/{userID:int}/{startDate:datetime}/{endDate:datetime}"), HttpGet]
        public HttpResponseMessage Load(int clientID, int userID, DateTime startDate, DateTime endDate)
        {
            ItemsResponse<CalendarEvent> response = new ItemsResponse<CalendarEvent>();
            response.Items = CalendarEventService.Load(clientID, userID, startDate, endDate);
            return Request.CreateResponse(response);
        }

        [Route("Sort"), HttpGet]
        public HttpResponseMessage SortByDate()
        {
            ItemsResponse<CalendarEvent> response = new ItemsResponse<CalendarEvent>();
            response.Items = CalendarEventService.SortByDate();
            return Request.CreateResponse(response);
        }

        [Route("{eventId:int}"), HttpDelete]
        public HttpResponseMessage DeleteCalendar(int eventId)
        {
            SuccessResponse response = new SuccessResponse();
            CalendarEventService.Delete(eventId);
            return Request.CreateResponse(response);
        }
    }
}
