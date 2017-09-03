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
    [RoutePrefix("api/Suppliers")]
    public class SuppliersAPIController : ApiController
    {
        [Route, HttpPost]
        public HttpResponseMessage AddSupplier(AddSupplierRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemResponse<int> response = new ItemResponse<int>();
            response.Item = SuppliersService.UpdateSupplier(model);
            return Request.CreateResponse(response);
        }

        [Route("{id:int}"), HttpPut]
        public HttpResponseMessage UpdateSupplier(UpdateSupplierRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            SuccessResponse response = new SuccessResponse();
            SuppliersService.UpdateSupplier(model);
            return Request.CreateResponse(response);
        }

        [Route("{id:int}"), HttpGet]
        public HttpResponseMessage GetSupplier(int id)
        {
            ItemResponse<Suppliers> response = new ItemResponse<Suppliers>();
            response.Item = SuppliersService.GetSupplier(id);
            return Request.CreateResponse(response);
        }

        [Route("Admin/{adminId:int}"), HttpGet]
        public HttpResponseMessage GetSuppliersByAdmin(int adminId)
        {
            ItemsResponse<Suppliers> response = new ItemsResponse<Suppliers>();
            response.Items = SuppliersService.GetSuppliersByAdmin(adminId);
            return Request.CreateResponse(response);
        }

        [Route, HttpGet]
        public HttpResponseMessage GetListOfSuppliers()
        {
            ItemsResponse<Suppliers> response = new ItemsResponse<Suppliers>();
            response.Items = SuppliersService.GetListOfSuppliers();
            return Request.CreateResponse(response);
        }

        [Route("{id:int}"), HttpDelete]
        public HttpResponseMessage DeleteSupplier(int id)
        {
            SuccessResponse response = new SuccessResponse();
            SuppliersService.DeleteSupplier(id);
            return Request.CreateResponse(response);
        }

    }
}
