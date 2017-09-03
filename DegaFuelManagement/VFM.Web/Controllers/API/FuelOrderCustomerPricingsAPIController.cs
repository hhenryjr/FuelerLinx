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
using Degatech.Utilities.IEnumerable;
namespace VFM.Web.Controllers.API
{
    [RoutePrefix("api/FuelOrderCustomerPricings")]
    public class FuelOrderCustomerPricingsAPIController : ApiController
    {
        [Route, HttpPost]
        public HttpResponseMessage AddFuelOrderCustomerPricing(AddFuelOrderCustomerPricingRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemResponse<int> response = new ItemResponse<int>();
            response.Item = FuelOrderCustomerPricingsService.UpdateFuelOrderCustomerPricing(model);
            return Request.CreateResponse(response);
        }

        [Route("List"), HttpPost]
        public HttpResponseMessage AddFuelOrderCustomerPricings(List<AddFuelOrderCustomerPricingRequest> models)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemsResponse<int> response = new ItemsResponse<int>();
            Degatech.Utilities.SQL.ExecutionHelper.BulkInsert(models.AsDataTable(), "FuelOrderCustomerPricings", Degatech.Utilities.SQL.ExecutionHelper.GetConnectionString());
            return Request.CreateResponse(response);
        }

        [Route("{id:int}"), HttpPut]
        public HttpResponseMessage UpdateFuelOrderCustomerPricing(UpdateFuelOrderCustomerPricingRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            SuccessResponse response = new SuccessResponse();
            FuelOrderCustomerPricingsService.UpdateFuelOrderCustomerPricing(model);
            return Request.CreateResponse(response);
        }

        [Route("{fuelOrderId:int}"), HttpGet]
        public HttpResponseMessage GetFuelOrderCustomerPricing(int fuelOrderId)
        {
            ItemsResponse<FuelOrderCustomerPricings> response = new ItemsResponse<FuelOrderCustomerPricings>();
            response.Items = FuelOrderCustomerPricingsService.GetFuelOrderCustomerPricingByFuelOrderId(fuelOrderId);
            return Request.CreateResponse(response);
        }

        [Route("{invoiceVolume:int}"), HttpPost]
        public HttpResponseMessage GetFuelOrderPricing(AddFuelOrderPricingRequest model, int invoiceVolume)
        {
            ItemResponse<FuelOrderCustomerPricings> response = new ItemResponse<FuelOrderCustomerPricings>();
            response.Item = FuelOrderCustomerPricingsService.GetCustomerInvoiced(model.AdminClientID, model.SupplierID, model.FuelOrderID, invoiceVolume, model.FBOName);
            return Request.CreateResponse(response);
        }

        [Route, HttpGet]
        public HttpResponseMessage GetListOfFuelOrderCustomerPricings()
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemsResponse<FuelOrderCustomerPricings> response = new ItemsResponse<FuelOrderCustomerPricings>();
            response.Items = FuelOrderCustomerPricingsService.GetListOfFuelOrderCustomerPricings();
            return Request.CreateResponse(response);
        }

        [Route("{id:int}"), HttpDelete]
        public HttpResponseMessage DeleteFuelOrderCustomerPricing(int id)
        {
            SuccessResponse response = new SuccessResponse();
            FuelOrderCustomerPricingsService.DeleteFuelOrderCustomerPricing(id);
            return Request.CreateResponse(response);
        }

    }
}
