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
    [RoutePrefix("api/Vendors")]
    public class VendorsAPIController : ApiController
    {
        [Route, HttpPost]
        public HttpResponseMessage AddVendor(AddVendorRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemResponse<int> response = new ItemResponse<int>();
            response.Item = VendorsService.UpdateVendor(model);
            return Request.CreateResponse(response);
        }

        [Route("{id:int}"), HttpPut]
        public HttpResponseMessage UpdateVendor(UpdateVendorRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            SuccessResponse response = new SuccessResponse();
            VendorsService.UpdateVendor(model);
            return Request.CreateResponse(response);
        }

        [Route("{adminId:int}"), HttpGet]
        public HttpResponseMessage GetVendor(int adminId)
        {
            ItemsResponse<Vendors> response = new ItemsResponse<Vendors>();
            response.Items = VendorsService.GeVendorsByAdminClient(adminId);
            return Request.CreateResponse(response);
        }

        [Route, HttpGet]
        public HttpResponseMessage GetListOfVendors()
        {
            ItemsResponse<Vendors> response = new ItemsResponse<Vendors>();
            response.Items = VendorsService.GetListOfVendors();
            return Request.CreateResponse(response);
        }

        [Route("{id:int}"), HttpDelete]
        public HttpResponseMessage DeleteVendor(int id)
        {
            SuccessResponse response = new SuccessResponse();
            VendorsService.DeleteVendor(id);
            return Request.CreateResponse(response);
        }

    }
}