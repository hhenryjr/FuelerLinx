using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VFM.Web.Models.Responses;
using VFMClasses.FuelOrder;
using VFM.Web.Services;

namespace VFM.Web.Controllers.API
{
    [RoutePrefix("api/FuelOrderInvoices")]
    public class FuelOrderInvoicesAPIController : ApiController
    {
        [Route("{fuelOrderId:int}"), HttpGet]
        public HttpResponseMessage GetInvoices(int fuelOrderId)
        {
            ItemsResponse<FuelOrderInvoices> response = new ItemsResponse<FuelOrderInvoices>();
            response.Items = FuelOrderInvoicesService.GetInvoicesByFuelOrder(fuelOrderId);
            return Request.CreateResponse(response);
        }

        [Route("{id:int}"), HttpDelete]
        public HttpResponseMessage DeleteInvoice(int id)
        {
            SuccessResponse response = new SuccessResponse();
            FuelOrderInvoicesService.DeleteInvoice(id);
            return Request.CreateResponse(response);
        }

        [Route("List/{fuelOrderId:int}"), HttpDelete]
        public HttpResponseMessage DeleteInvoices(int fuelOrderId)
        {
            SuccessResponse response = new SuccessResponse();
            FuelOrderInvoicesService.DeleteInvoicesByFuelOrder(fuelOrderId);
            return Request.CreateResponse(response);
        }

        [Route("Download/{id:int}"), HttpPost]
        public HttpResponseMessage DownloadInvoice(int id)
        {
            SuccessResponse response = new SuccessResponse();
            FuelOrderInvoicesService.DownloadInvoice(id);
            return Request.CreateResponse(response);
        }
    }
}
