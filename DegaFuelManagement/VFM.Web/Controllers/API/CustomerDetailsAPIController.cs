using Degatech.Common;
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
using VFMClasses.Exports;
using VFMClasses.DataSets.CompaniesTableAdapters;
using System.Data.SqlClient;

namespace VFM.Web.Controllers.API
{
    [RoutePrefix("api/details")]
    public class CustomerDetailsAPIController : ApiController
    {
        [Route, HttpPost]
        public HttpResponseMessage AddCustomerDetail(AddCustomerDetailRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemResponse<int> response = new ItemResponse<int>();
            response.Item = CustomerDetailsService.UpdateCustomerDetail(model);
            return Request.CreateResponse(response);
        }

        [Route("{id:int}"), HttpPut]
        public HttpResponseMessage UpdateCustomerDetail(UpdateCustomerDetailRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            SuccessResponse response = new SuccessResponse();
            CustomerDetailsService.UpdateCustomerDetail(model);
            return Request.CreateResponse(response);
        }

        [Route("{id:int}"), HttpGet]
        public HttpResponseMessage GetCustomerDetail(int id)
        {
            ItemResponse<CustomerDetails> response = new ItemResponse<CustomerDetails>();
            response.Item = CustomerDetailsService.GetCustomerDetailInfo(id);
            return Request.CreateResponse(response);
        }

        [Route, HttpGet]
        public HttpResponseMessage GetCustomerDetailList()
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemsResponse<CustomerDetails> response = new ItemsResponse<CustomerDetails>();
            response.Items = CustomerDetailsService.GetListOfCustomerDetails();
            return Request.CreateResponse(response);
        }

        [Route("Admin/{clientId:int}"), HttpGet]
        public HttpResponseMessage GetCustomersByAdminClient(int clientId)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemResponse<string> response = new ItemResponse<string>();
            response.Item = CustomerDetailsService.GetCustomersAutoCompleteListByAdminClientID(clientId);
            //ItemsResponse<CustomerDetails> response = new ItemsResponse<CustomerDetails>();
            //response.Items = CustomerDetailsService.GetListOfCustomersByAdminClient(clientId);
            return Request.CreateResponse(response);
        }

        [Route("Export"), HttpPost]
        public string ExportContacts(ExportDataRequest model)
        {
            var table = GetCompanies(model.ClientID, model.ListOfIDs);
            string filePath = ExportHelper.CreateFilePath("Companies.csv", model.ClientName);
            CSVGenerator.GenerateCSV(table, filePath);
            return ExportHelper.GetFilePathFromRoot(filePath);
        }

        [Route("{id:int}"), HttpDelete]
        public HttpResponseMessage DeleteCustomerDetail(int id)
        {
            SuccessResponse response = new SuccessResponse();
            CustomerDetailsService.DeleteCustomerDetail(id);
            return Request.CreateResponse(response);
        }

        #region Private Methods
        private VFMClasses.DataSets.Companies.CompaniesDataTable GetCompanies(int adminId, string companyIDs)
        {
            CompaniesTableAdapter adapter = new CompaniesTableAdapter();
            adapter.Connection = new SqlConnection(Degatech.Utilities.SQL.ConnectionHelper.GetConnectionString("SQLFLString"));
            VFMClasses.DataSets.Companies.CompaniesDataTable table = adapter.GetData(adminId, companyIDs);
            return table;
        }
        #endregion
    }
}
