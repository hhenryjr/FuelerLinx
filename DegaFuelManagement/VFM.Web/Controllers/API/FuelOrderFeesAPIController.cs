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
    [RoutePrefix("api/FuelOrderFees")]
    public class FuelOrderFeesAPIController : ApiController
    {
        [Route, HttpPost]
        public HttpResponseMessage AddFuelOrder(AddFuelOrderFeeRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemResponse<FuelOrderFees> response = new ItemResponse<FuelOrderFees>();
            response.Item = FuelOrderFeesService.UpdateFee(model);
            return Request.CreateResponse(response);
        }

        [Route("List"), HttpPost]
        public HttpResponseMessage AddList(UpdateFuelOrderFeeRequest[] models)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemsResponse<FuelOrderFees> response = new ItemsResponse<FuelOrderFees>();
            foreach (AddFuelOrderFeeRequest model in models)
            {
                response.Items.Add(FuelOrderFeesService.UpdateFee(model));
            }
            return Request.CreateResponse(response);
        }

        [Route("{id:int}"), HttpPut]
        public HttpResponseMessage UpdateFuelOrder(UpdateFuelOrderFeeRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            SuccessResponse response = new SuccessResponse();
            FuelOrderFeesService.UpdateFee(model);
            return Request.CreateResponse(response);
        }

        [Route("{fuelOrderId:int}"), HttpGet]
        public HttpResponseMessage GetFuelOrder(int fuelOrderId)
        {
            ItemsResponse<FuelOrderFees> response = new ItemsResponse<FuelOrderFees>();
            response.Items = FuelOrderFeesService.GetListOfFuelOrderFees(fuelOrderId);
            return Request.CreateResponse(response);
        }

        [Route, HttpGet]
        public HttpResponseMessage GetListOfFuelOrderFees(int fuelOrderId)
        {
            ItemsResponse<FuelOrderFees> response = new ItemsResponse<FuelOrderFees>();
            response.Items = FuelOrderFeesService.GetListOfFuelOrderFees(fuelOrderId);
            return Request.CreateResponse(response);
        }

        //[Route("Admin/{clientId:int}"), HttpGet]
        //public HttpResponseMessage GetFuelOrderFeesByAdminClient(int clientId)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        //    }

        //    ItemsResponse<FuelOrderFees> response = new ItemsResponse<FuelOrderFees>();
        //    response.Items = FuelOrderFeesService.GetFuelOrderFeesByAdminClient(clientId);
        //    return Request.CreateResponse(response);
        //}

        [Route("{feeID:int}"), HttpDelete]
        public HttpResponseMessage DeleteAllFees(int feeID)
        {
            SuccessResponse response = new SuccessResponse();
            FuelOrderFeesService.DeleteAllFees(feeID);
            return Request.CreateResponse(response);
        }
    }
}
