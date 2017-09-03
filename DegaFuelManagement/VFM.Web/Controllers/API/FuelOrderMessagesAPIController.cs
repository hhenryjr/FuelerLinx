using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VFM.API.EntityServices.FuelOrder;
using VFM.EDM;
using VFM.Web.Models.Requests;
using VFM.Web.Models.Responses;

namespace VFM.Web.Controllers.API
{
    [RoutePrefix("api/FuelOrderMessages")]
    public class FuelOrderMessagesAPIController : ApiController
    {
        [Route, HttpPost]
        public HttpResponseMessage AddMessage(FuelOrderMessage model)
        {
            ItemResponse<FuelOrderMessage> response = new ItemResponse<FuelOrderMessage>();
            FuelOrderMessages message = new FuelOrderMessages();
            response.Item = message.Update(model);
            return Request.CreateResponse(response);
        }

        [Route("{fuelOrderId:int}"), HttpGet]
        public HttpResponseMessage GetListByFuelOrder(int fuelOrderId)
        {
            ItemsResponse<FuelOrderMessage> response = new ItemsResponse<FuelOrderMessage>();
            FuelOrderMessages message = new FuelOrderMessages();
            response.Items = message.GetListByFuelOrderSerializable(fuelOrderId);
            return Request.CreateResponse(response);
        }
    }
}
