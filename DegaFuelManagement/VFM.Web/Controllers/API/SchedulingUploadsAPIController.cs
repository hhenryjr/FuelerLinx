using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VFM.Web.Models.Responses;
using VFM.Web.Services;
using VFMClasses;
using VFMClasses.SchedulingSheets;

namespace VFM.Web.Controllers.API
{
    [RoutePrefix("api/SchedulingUploads")]
    public class SchedulingUploadsAPIController : ApiController
    {
        [Route("{name:alpha}/{custId:int}"), HttpGet]
        public HttpResponseMessage GetDetail(string name, int custId)
        {
            ItemResponse<SchedulingUploads> response = new ItemResponse<SchedulingUploads>();
            response.Item = SchedulingUploadsService.GetSchedulingUpload(name, custId);
            return Request.CreateResponse(response);
        }

        [Route("{schedulingId:int}/{custClientId:int}"), HttpGet]
        public HttpResponseMessage generateFuelOrders(int schedulingId, int custClientId)
        {
            SchedulingGenerateFuelOrders generateFuelOrders = new SchedulingGenerateFuelOrders();
            generateFuelOrders.SchedulingPlatform = schedulingId;
            generateFuelOrders.AdminClientID = Users.CurrentUser.ClientID;
            generateFuelOrders.CustClientID = custClientId;
            
            ItemResponse<SchedulingUploads> response = new ItemResponse<SchedulingUploads>();
            response.Message = generateFuelOrders.GetTrips().ToString();
            return Request.CreateResponse(response);
        }
        
    }
}
