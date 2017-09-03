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
    [RoutePrefix("api/PriceMarginTiers")]
    public class PriceMarginTiersAPIController : ApiController
    {
        [Route, HttpPost]
        public HttpResponseMessage AddPriceMarginTier(AddPriceMarginTierRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemResponse<int> response = new ItemResponse<int>();
            response.Item = PriceMarginTiersService.UpdatePriceMarginTier(model);
            return Request.CreateResponse(response);
        }

        [Route("List"), HttpPost]
        public HttpResponseMessage AddListOfTiers(UpdatePriceMarginTierRequest[] models)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemResponse<int> response = new ItemResponse<int>();
            foreach (AddPriceMarginTierRequest model in models)
            {
            response.Item = PriceMarginTiersService.UpdatePriceMarginTier(model);
            }
            return Request.CreateResponse(response);
        }

        [Route("{id:int}"), HttpPut]
        public HttpResponseMessage UpdatePriceMarginTier(UpdatePriceMarginTierRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            SuccessResponse response = new SuccessResponse();
            PriceMarginTiersService.UpdatePriceMarginTier(model);
            return Request.CreateResponse(response);
        }

        [Route("{id:int}"), HttpGet]
        public HttpResponseMessage GetPriceMarginTier(int id)
        {
            ItemResponse<PriceMarginTiers> response = new ItemResponse<PriceMarginTiers>();
            response.Item = PriceMarginTiersService.GetPriceMarginTier(id);
            return Request.CreateResponse(response);
        }

        [Route, HttpGet]
        public HttpResponseMessage GetListOfPriceMarginTiers()
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemsResponse<PriceMarginTiers> response = new ItemsResponse<PriceMarginTiers>();
            response.Items = PriceMarginTiersService.GetListOfPriceMarginTiers();
            return Request.CreateResponse(response);
        }

        [Route("Margin/{marginId:int}"), HttpGet]
        public HttpResponseMessage GetTierByPriceMarginID(int marginId)
        {
            ItemsResponse<PriceMarginTiers> response = new ItemsResponse<PriceMarginTiers>();
            response.Items = PriceMarginTiersService.GetTiersByPriceMargin(marginId);
            return Request.CreateResponse(response);
        }

        [Route("{id:int}"), HttpDelete]
        public HttpResponseMessage DeletePriceMarginTier(int id)
        {
            SuccessResponse response = new SuccessResponse();
            PriceMarginTiersService.DeletePriceMarginTier(id);
            return Request.CreateResponse(response);
        }
    }
}
