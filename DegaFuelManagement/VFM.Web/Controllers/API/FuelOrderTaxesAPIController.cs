using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Degatech.Common;
using VFM.Web.Models.Responses;
using VFM.Web.Models.Requests;
using VFM.Web.Services;
using VFMClasses;
using VFMClasses.Exports;

namespace VFM.Web.Controllers.API
{
    [RoutePrefix("api/FuelOrderTaxes")]
    public class FuelOrderTaxesAPIController : ApiController
    {
        [Route, HttpPost]
        public HttpResponseMessage AddFuelOrderTax(AddFuelOrderTaxRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemResponse<FuelOrderTaxes> response = new ItemResponse<FuelOrderTaxes>();
            response.Item = FuelOrderTaxesService.UpdateTax(model);
            return Request.CreateResponse(response);
        }

        [Route("List"), HttpPost]
        public HttpResponseMessage AddList(UpdateFuelOrderTaxRequest[] models)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemsResponse<FuelOrderTaxes> response = new ItemsResponse<FuelOrderTaxes>();
            foreach (AddFuelOrderTaxRequest model in models)
            {
                response.Items.Add(FuelOrderTaxesService.UpdateTax(model));
            }
            return Request.CreateResponse(response);
        }

        [Route("{id:int}"), HttpPut]
        public HttpResponseMessage UpdateFuelOrderTax(UpdateFuelOrderTaxRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            SuccessResponse response = new SuccessResponse();
            FuelOrderTaxesService.UpdateTax(model);
            return Request.CreateResponse(response);
        }

        [Route("{fuelOrderId:int}"), HttpGet]
        public HttpResponseMessage GetFuelOrderTaxes(int fuelOrderId)
        {
            ItemsResponse<FuelOrderTaxes> response = new ItemsResponse<FuelOrderTaxes>();
            response.Items = FuelOrderTaxesService.GetListOfFuelOrderTaxes(fuelOrderId);
            return Request.CreateResponse(response);
        }

        [Route, HttpGet]
        public HttpResponseMessage GetListOfFuelOrderTaxes(int fuelOrderId)
        {
            ItemsResponse<FuelOrderTaxes> response = new ItemsResponse<FuelOrderTaxes>();
            response.Items = FuelOrderTaxesService.GetListOfFuelOrderTaxes(fuelOrderId);
            return Request.CreateResponse(response);
        }

        //[Route("Admin/{clientId:int}"), HttpGet]
        //public HttpResponseMessage GetFuelOrderTaxesByAdminClient(int clientId)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        //    }

        //    ItemsResponse<FuelOrderTaxes> response = new ItemsResponse<FuelOrderTaxes>();
        //    response.Items = FuelOrderTaxesService.GetFuelOrderTaxesByAdminClient(clientId);
        //    return Request.CreateResponse(response);
        //}

        [Route("{id:int}"), HttpDelete]
        public HttpResponseMessage DeleteFuelOrderTax(int id)
        {
            SuccessResponse response = new SuccessResponse();
            FuelOrderTaxesService.DeleteFuelOrder(id);
            return Request.CreateResponse(response);
        }
    }
}
