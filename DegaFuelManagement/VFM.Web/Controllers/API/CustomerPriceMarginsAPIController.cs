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
    [RoutePrefix("api/CustomerPriceMargins")]
    public class CustomerPriceMarginsAPIController : ApiController
    {
        [Route, HttpPost]
        public HttpResponseMessage AddCustomerPriceMargin(AddCustomerPriceMarginRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemResponse<int> response = new ItemResponse<int>();
            response.Item = CustomerPriceMarginsService.UpdateCustomerPriceMargin(model);
            return Request.CreateResponse(response);
        }

        [Route("List"), HttpPost]
        public HttpResponseMessage AddCustomerPriceMargins(AddCustomerPriceMarginRequest[] models)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemsResponse<int> response = new ItemsResponse<int>();
            foreach (AddCustomerPriceMarginRequest model in models)
            { 
            response.Items.Add(CustomerPriceMarginsService.UpdateCustomerPriceMargin(model));
            }
            return Request.CreateResponse(response);
        }

        [Route("{id:int}"), HttpPut]
        public HttpResponseMessage UpdateCustomerPriceMargin(UpdateCustomerPriceMarginRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            SuccessResponse response = new SuccessResponse();
            CustomerPriceMarginsService.UpdateCustomerPriceMargin(model);
            return Request.CreateResponse(response);
        }

        [Route("{id:int}"), HttpGet]
        public HttpResponseMessage GetCustomerPriceMargin(int id)
        {
            ItemResponse<CustomerPriceMargins> response = new ItemResponse<CustomerPriceMargins>();
            response.Item = CustomerPriceMarginsService.GetCustomerPriceMarginByCustClientID(id);
            return Request.CreateResponse(response);
        }

        [Route("{custId:int}/{adminId:int}/{icao:alpha}"), HttpGet]
        public HttpResponseMessage GetQuote(int custId, int adminId, string icao)
        {
            ItemResponse<CustomerPriceMarginsCollection> response = new ItemResponse<CustomerPriceMarginsCollection>();
            response.Item = CustomerPriceMarginsService.GetCustomerPriceMarginByClientIDsAndICAO(custId, adminId, icao);
            return Request.CreateResponse(response);
        }

        [Route("AllVendors/{custId:int}/{adminId:int}/{icao:alpha}"), HttpGet]
        public HttpResponseMessage GetQuoteFromAllVendors(int custId, int adminId, string icao)
        {
            ItemResponse<CustomerPriceMarginsCollection> response = new ItemResponse<CustomerPriceMarginsCollection>();
            response.Item = CustomerPriceMarginsService.GetCustomerPriceMarginForAllVendors(custId, adminId, icao);
            return Request.CreateResponse(response);
        }

        [Route, HttpGet]
        public HttpResponseMessage GetListOfCustomerPriceMargins()
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemsResponse<CustomerPriceMargins> response = new ItemsResponse<CustomerPriceMargins>();
            response.Items = CustomerPriceMarginsService.GetListOfCustomerPriceMargins();
            return Request.CreateResponse(response);
        }

        [Route("Highest/{id:int}/{custClientId:int}/{priceMarginID:int}"), HttpGet]
        public HttpResponseMessage GetHighestMargin(int id, int custClientId, int priceMarginId)
        {
            ItemResponse<CustomerPriceMargins> response = new ItemResponse<CustomerPriceMargins>();
            response.Item = CustomerPriceMarginsService.UpdatePriceMarginGetHighestMargin(id, custClientId, priceMarginId);
            return Request.CreateResponse(response);
        }

        [Route("{id:int}"), HttpDelete]
        public HttpResponseMessage DeleteCustomerPriceMargin(int id)
        {
            SuccessResponse response = new SuccessResponse();
            CustomerPriceMarginsService.DeleteCustomerPriceMargin(id);
            return Request.CreateResponse(response);
        }

    }
}
