using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VFM.Web.Models.Requests;
using VFM.Web.Models.Responses;
using VFM.Web.Services;
using VFMClasses;

namespace VFM.Web.Controllers.API
{
    [RoutePrefix("api/PriceMargins")]
    public class PriceMarginsAPIController : ApiController
    {
        [Route, HttpPost]
        public HttpResponseMessage AddPriceMargin(AddPriceMarginRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemResponse<int> response = new ItemResponse<int>();
            response.Item = PriceMarginsService.UpdatePriceMargin(model);
            return Request.CreateResponse(response);
        }

        [Route("{id:int}"), HttpPut]
        public HttpResponseMessage UpdatePriceMargin(UpdatePriceMarginRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            SuccessResponse response = new SuccessResponse();
            PriceMarginsService.UpdatePriceMargin(model);
            return Request.CreateResponse(response);
        }

        [Route("{id:int}"), HttpGet]
        public HttpResponseMessage GetPriceMargin(int id)
        {
            ItemResponse<PriceMargins> response = new ItemResponse<PriceMargins>();
            response.Item = PriceMarginsService.GetPriceMargin(id);
            return Request.CreateResponse(response);
        }

        [Route, HttpGet]
        public HttpResponseMessage GetListOfPriceMargins()
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemsResponse<PriceMargins> response = new ItemsResponse<PriceMargins>();
            response.Items = PriceMarginsService.GetListOfPriceMargins();
            return Request.CreateResponse(response);
        }

        [Route("Admin/{clientId:int}"), HttpGet]
        public HttpResponseMessage GetListOfPriceMargins(int clientId)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemsResponse<PriceMargins> response = new ItemsResponse<PriceMargins>();
            response.Items = PriceMarginsService.GetPriceMarginsByAdmin(clientId);
            return Request.CreateResponse(response);
        }

        [Route("{id:int}"), HttpDelete]
        public HttpResponseMessage DeletePriceMargin(int id)
        {
            SuccessResponse response = new SuccessResponse();
            PriceMarginsService.DeletePriceMargin(id);
            return Request.CreateResponse(response);
        }
    }
}
