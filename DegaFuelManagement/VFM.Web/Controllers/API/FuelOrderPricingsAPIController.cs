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
    [RoutePrefix("api/FuelOrderPricings")]
    public class FuelOrderPricingsAPIController : ApiController
    {
        [Route, HttpPost]
        public HttpResponseMessage AddFuelOrderPricing(AddFuelOrderPricingRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemResponse<int> response = new ItemResponse<int>();
            response.Item = FuelOrderPricingsService.UpdateFuelOrderPricing(model);
            return Request.CreateResponse(response);
        }

        [Route("List"), HttpPost]
        public HttpResponseMessage AddFuelOrderPricings(AddFuelOrderPricingRequest[] models)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemsResponse<int> response = new ItemsResponse<int>();
            foreach (AddFuelOrderPricingRequest model in models)
            {
            response.Items.Add(FuelOrderPricingsService.UpdateFuelOrderPricing(model));
            }
            return Request.CreateResponse(response);
        }

        [Route("{id:int}"), HttpPut]
        public HttpResponseMessage UpdateFuelOrderPricing(UpdateFuelOrderPricingRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            SuccessResponse response = new SuccessResponse();
            FuelOrderPricingsService.UpdateFuelOrderPricing(model);
            return Request.CreateResponse(response);
        }

        [Route("{id:int}"), HttpGet]
        public HttpResponseMessage GetFuelOrderPricing(int id)
        {
            ItemResponse<FuelOrderPricings> response = new ItemResponse<FuelOrderPricings>();
            response.Item = FuelOrderPricingsService.GetFuelOrderPricing(id);
            return Request.CreateResponse(response);
        }

        [Route("{invoiceVolume:int}"), HttpPost]
        public HttpResponseMessage GetFuelOrderPricing(AddFuelOrderPricingRequest model, int invoiceVolume)
        {
            ItemResponse<FuelOrderPricings> response = new ItemResponse<FuelOrderPricings>();
            response.Item = FuelOrderPricingsService.GetFuelOrderPricingByInvoiced(model.AdminClientID, model.SupplierID, model.FuelOrderID, invoiceVolume, model.FBOName);
            return Request.CreateResponse(response);
        }

        [Route, HttpGet]
        public HttpResponseMessage GetListOfFuelOrderPricings()
        {
            ItemsResponse<FuelOrderPricings> response = new ItemsResponse<FuelOrderPricings>();
            response.Items = FuelOrderPricingsService.GetListOfFuelOrderPricings();
            return Request.CreateResponse(response);
        }

        [Route("{id:int}"), HttpDelete]
        public HttpResponseMessage DeleteFuelOrderPricing(int id)
        {
            SuccessResponse response = new SuccessResponse();
            FuelOrderPricingsService.DeleteFuelOrderPricing(id);
            return Request.CreateResponse(response);
        }

        [Route("GetQuoteForLocation"), HttpPost]
        public HttpResponseMessage GetQuoteForLocation(GetQuoteForLocationRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            ItemsResponse<FuelOrderPricings> response = new ItemsResponse<FuelOrderPricings>();
            response.Items = FuelOrderPricingsService.GetQuoteForLocation(model.AdminClientID, model.CustClientID, model.ICAO, model.TailNumber);
            return Request.CreateResponse(response);
        }

        [Route("GetQuoteForLocationForAllVendors"), HttpPost]
        public HttpResponseMessage GetQuoteForLocationFromAllVendors(GetQuoteForLocationRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            ItemsResponse<FuelOrderPricings> response = new ItemsResponse<FuelOrderPricings>();
            response.Items = FuelOrderPricingsService.GetQuoteForLocationForAllVendors(model.AdminClientID, model.CustClientID, model.ICAO, model.TailNumber);
            return Request.CreateResponse(response);
        }
    }
}
