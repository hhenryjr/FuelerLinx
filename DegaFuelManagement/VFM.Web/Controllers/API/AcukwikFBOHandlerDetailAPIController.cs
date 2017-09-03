using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VFM.Web.Models.Responses;
using VFM.Web.Services;
using VFMClasses;

namespace VFM.Web.Controllers.API
{
    [RoutePrefix("api/AcukwikFBOHandlerDetail")]
    public class AcukwikFBOHandlerDetailAPIController : ApiController
    {
        [Route("{id:int}"), HttpGet]
        public HttpResponseMessage GetDetail(int id)
        {
            ItemResponse<AcukwikFBOHandlerDetail> response = new ItemResponse<AcukwikFBOHandlerDetail>();
            response.Item = AcukwikFBOHandlerDetailService.GetDetail(id);
            return Request.CreateResponse(response);
        }

        [Route, HttpGet]
        public HttpResponseMessage GetListOfAcukwikFBOHandlerDetail()
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            //ItemResponse<string> response = new ItemResponse<string>();
            //response.Item = AcukwikFBOHandlerDetailService.GetAirportAutoCompleteList();
            ItemsResponse<AcukwikFBOHandlerDetail> response = new ItemsResponse<AcukwikFBOHandlerDetail>();
            response.Items = AcukwikFBOHandlerDetailService.GetListOfAcukwikFBOHandlerDetail();
            return Request.CreateResponse(response);
        }
    }
}
