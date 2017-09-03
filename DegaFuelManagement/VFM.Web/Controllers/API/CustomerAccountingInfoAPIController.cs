using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VFM.API.EntityServices;
using VFM.EDM;
using VFM.Web.Models.Requests;
using VFM.Web.Models.Responses;
using VFM.Web.Services;
using VFMClasses;

namespace VFM.Web.Controllers.API
{
    [RoutePrefix("api/CustomerAccountingInfo")]
    public class CustomerAccountingInfoAPIController : ApiController
    {
        [Route, HttpPost]
        public HttpResponseMessage AddInfo(AddCustomerAccountingInfoRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemResponse<CustomerAccountingInfo> response = new ItemResponse<CustomerAccountingInfo>();
            response.Item = CustomerAccountingInfoService.UpdateInfo(model);
            if (model.IsFuelerLinxCustomer)
                AddGUID(model);
            return Request.CreateResponse(response);
        }

        [Route("{adminId:int}/{custId:int}"), HttpGet]
        public HttpResponseMessage GetInfoByAdminAndCustClientID(int adminId, int custId)
        {
            ItemResponse<CustomerAccountingInfo> response = new ItemResponse<CustomerAccountingInfo>();
            response.Item = CustomerAccountingInfoService.GetInfoByAdminAndCustClientID(adminId, custId);
            return Request.CreateResponse(response);
        }

        #region Private Methods

        private void AddGUID(AddCustomerAccountingInfoRequest model)
        {
            PartnerServiceIntegration serviceIntegration = new PartnerServiceIntegration();
            serviceIntegration.ClientID = model.CustClientID;
            serviceIntegration.AdminClientID = model.AdminClientID;

            PartnerServiceIntegrationsAPIController integrationAPIController =
                new PartnerServiceIntegrationsAPIController();


            integrationAPIController.AddGUID(ref serviceIntegration);
            if (serviceIntegration.Id == 0)
            {
                PartnerServiceIntegrations integrations = new PartnerServiceIntegrations();
                integrations.Update(serviceIntegration);
            }
        }

        #endregion

        //[Route, HttpGet]
        //public HttpResponseMessage GetCustomerDetailList()
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        //    }

        //    ItemsResponse<CustomerAccountingInfo> response = new ItemsResponse<CustomerAccountingInfo>();
        //    response.Items = CustomerAccountingInfoService.GetListOfCustomerAccountingInfo();
        //    return Request.CreateResponse(response);
        //}

        //[Route("Admin/{clientId:int}"), HttpGet]
        //public HttpResponseMessage GetCustomersByAdminClient(int clientId)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        //    }

        //    ItemsResponse<CustomerAccountingInfo> response = new ItemsResponse<CustomerAccountingInfo>();
        //    response.Items = CustomerAccountingInfoService.GetListOfCustomersByAdminClient(clientId);
        //    return Request.CreateResponse(response);
        //}

        //[Route("{id:int}"), HttpDelete]
        //public HttpResponseMessage DeleteCustomerDetail(int id)
        //{
        //    SuccessResponse response = new SuccessResponse();
        //    CustomerAccountingInfoService.DeleteCustomerDetail(id);
        //    return Request.CreateResponse(response);
        //}
    }
}
