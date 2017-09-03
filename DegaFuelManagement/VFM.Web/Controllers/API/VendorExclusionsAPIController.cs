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
    [RoutePrefix("api/VendorExclusions")]
    public class VendorExclusionsAPIController : ApiController
    {
        /*CUSTOMER DETAIL*/
        [Route("CustomerDetail"), HttpPost]
        public HttpResponseMessage AddVendorExclusion(AddCustomerDetailVendorExclusionRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemResponse<int> response = new ItemResponse<int>();
            response.Item = CustomerDetailVendorExclusionsService.UpdateExclusion(model);
            return Request.CreateResponse(response);
        }

        [Route("CustomerDetail/{id:int}"), HttpPut]
        public HttpResponseMessage UpdateVendorExclusion(UpdateCustomerDetailVendorExclusionRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            SuccessResponse response = new SuccessResponse();
            CustomerDetailVendorExclusionsService.UpdateExclusion(model);
            return Request.CreateResponse(response);
        }

        [Route("CustomerDetail/{custId:int}/{adminId:int}"), HttpGet]
        public HttpResponseMessage GetVendorExclusion(int custId, int adminId)
        {
            ItemsResponse<CustomerDetailVendorExclusions> response = new ItemsResponse<CustomerDetailVendorExclusions>();
            response.Items = CustomerDetailVendorExclusionsService.GetExclusionsByCustClient(custId, adminId);
            return Request.CreateResponse(response);
        }

        [Route("CustomerDetail/{id:int}"), HttpDelete]
        public HttpResponseMessage DeleteVendorExclusion(int id)
        {
            SuccessResponse response = new SuccessResponse();
            CustomerDetailVendorExclusionsService.DeleteExclusion(id);
            return Request.CreateResponse(response);
        }

        [Route("Aircrafts"), HttpPost]
        public HttpResponseMessage AddExcludedAircraft(AddCustomerDetailAircraftExclusionRequest model)
        {
            ItemResponse<int> response = new ItemResponse<int>();
            response.Item = CustomerDetailAircraftExclusionsService.UpdateExclusion(model);
            return Request.CreateResponse(response);
        }

        /*FBO DETAIL*/
        [Route("FBODetail"), HttpPost]
        public HttpResponseMessage AddVendorExclusion(AddFBODetailVendorExclusionRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemResponse<int> response = new ItemResponse<int>();
            response.Item = FBODetailVendorExclusionsService.UpdateExclusion(model);
            return Request.CreateResponse(response);
        }

        [Route("FBODetail/{id:int}"), HttpPut]
        public HttpResponseMessage UpdateVendorExclusion(UpdateFBODetailVendorExclusionRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            SuccessResponse response = new SuccessResponse();
            FBODetailVendorExclusionsService.UpdateExclusion(model);
            return Request.CreateResponse(response);
        }

        [Route("FBODetail/{custId:int}/{fboId:int}"), HttpGet]
        public HttpResponseMessage GetFBOVendorExclusion(int custId, int fboId)
        {
            ItemsResponse<FBODetailVendorExclusions> response = new ItemsResponse<FBODetailVendorExclusions>();
            response.Items = FBODetailVendorExclusionsService.GetExclusionsByCustClient(custId, fboId);
            return Request.CreateResponse(response);
        }

        [Route("FBODetail/{id:int}"), HttpDelete]
        public HttpResponseMessage DeleteFBOVendorExclusion(int id)
        {
            SuccessResponse response = new SuccessResponse();
            FBODetailVendorExclusionsService.DeleteExclusion(id);
            return Request.CreateResponse(response);
        }
    }
}