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
    [RoutePrefix("api/FBODetailCustomFields")]
    public class FBODetailCustomFieldsAPIController : ApiController
    {
        [Route, HttpPost]
        public HttpResponseMessage AddPriceMargin(AddFBODetailCustomFieldRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemResponse<int> response = new ItemResponse<int>();
            response.Item = FBODetailCustomFieldsService.UpdateFBODetailCustomField(model);
            //ItemsResponse<int> response = new ItemsResponse<int>();
            //foreach (AddFBODetailCustomFieldRequest model in models)
            //{
            //    response.Items.Add(FBODetailCustomFieldsService.UpdateFBODetailCustomField(model));
            //}
            return Request.CreateResponse(response);
        }

        [Route("{id:int}"), HttpPut]
        public HttpResponseMessage UpdatePriceMargin(UpdateFBODetailCustomFieldRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            SuccessResponse response = new SuccessResponse();
            FBODetailCustomFieldsService.UpdateFBODetailCustomField(model);
            return Request.CreateResponse(response);
        }

        [Route("{id:int}"), HttpGet]
        public HttpResponseMessage GetPriceMargin(int id)
        {
            ItemResponse<FBODetailCustomFields> response = new ItemResponse<FBODetailCustomFields>();
            response.Item = FBODetailCustomFieldsService.GetFBODetailCustomField(id);
            return Request.CreateResponse(response);
        }

        [Route("Details"), HttpPost]
        public HttpResponseMessage GetFBODetails(GetFBODetailsRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemsResponse<FBODetailCustomFields> response = new ItemsResponse<FBODetailCustomFields>();
            response.Items = FBODetailCustomFieldsService.GetCustomFields(model.FBO, model.ICAO, model.AdminClientID);
            return Request.CreateResponse(response);
        }

        [Route("{id:int}"), HttpDelete]
        public HttpResponseMessage DeletePriceMargin(int id)
        {
            SuccessResponse response = new SuccessResponse();
            FBODetailCustomFieldsService.DeleteFBODetailCustomField(id);
            return Request.CreateResponse(response);
        }
    }
}
