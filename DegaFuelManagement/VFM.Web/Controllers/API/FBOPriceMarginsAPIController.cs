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
    [RoutePrefix("api/FBOPriceMargins")]
    public class FBOPriceMarginsAPIController : ApiController
    {
        [Route, HttpPost]
        public HttpResponseMessage AddPriceMargin(AddFBOPriceMarginRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemResponse<int> response = new ItemResponse<int>();
            response.Item = FBOPriceMarginsService.UpdateFBOPriceMargin(model);
            //ItemsResponse<int> response = new ItemsResponse<int>();
            //foreach (AddFBOPriceMarginRequest model in models)
            //{
            //    response.Items.Add(FBOPriceMarginsService.UpdateFBOPriceMargin(model));
            //}
            return Request.CreateResponse(response);
        }

        [Route("{id:int}"), HttpPut]
        public HttpResponseMessage UpdatePriceMargin(UpdateFBOPriceMarginRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            SuccessResponse response = new SuccessResponse();
            FBOPriceMarginsService.UpdateFBOPriceMargin(model);
            return Request.CreateResponse(response);
        }

        [Route("{id:int}"), HttpGet]
        public HttpResponseMessage GetPriceMargin(int id)
        {
            ItemResponse<FBOPriceMargins> response = new ItemResponse<FBOPriceMargins>();
            response.Item = FBOPriceMarginsService.GetFBOPriceMargin(id);
            return Request.CreateResponse(response);
        }

        [Route, HttpGet]
        public HttpResponseMessage GetListOfFBOPriceMargins()
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemsResponse<FBOPriceMargins> response = new ItemsResponse<FBOPriceMargins>();
            response.Items = FBOPriceMarginsService.GetListOfFBOPriceMargins();
            return Request.CreateResponse(response);
        }

        [Route("{adminId:int}/{icao:alpha}"), HttpGet]
        public HttpResponseMessage GetFBOPriceMarginsByAdminClientIDAndICAO(int adminId, string icao)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemsResponse<FBOPriceMargins> response = new ItemsResponse<FBOPriceMargins>();
            response.Items = FBOPriceMarginsService.GetFBOPriceMarginsByAdminClientIDAndICAO(adminId, icao);
            return Request.CreateResponse(response);
        }

        [Route("Details"), HttpPost]
        public HttpResponseMessage GetFBODetails(GetFBODetailsRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            //ItemResponse<FBOPriceMargins> response = new ItemResponse<FBOPriceMargins>();
            //response.Item = FBOPriceMarginsService.GetFBODetails(model.ICAO, model.FBO, model.AdminClientID);
            ItemResponse<FBOPriceMarginsCollection> response = new ItemResponse<FBOPriceMarginsCollection>();
            response.Item = FBOPriceMarginsService.GetFBODetails(model.ICAO, model.FBO, model.AdminClientID);
            return Request.CreateResponse(response);
        }

        [Route, HttpDelete]
        public HttpResponseMessage DeletePriceMargin(AddFBOPriceMarginRequest model)
        {
            SuccessResponse response = new SuccessResponse();
            FBOPriceMarginsService.DeleteFBOPriceMargin(model.ICAO, model.FBO, model.AdminClientID, model.Id);
            return Request.CreateResponse(response);
        }
    }
}
