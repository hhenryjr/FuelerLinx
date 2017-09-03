using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VFM.Web.Models.Responses;
using VFM.Web.Models.Requests;
using VFM.Web.Services;
using VFMClasses;
using VFMClasses.DataSets.SupplierFuelPricesDataSetTableAdapters;
using System.Data.SqlClient;
using Degatech.Common;
using VFMClasses.Exports;

namespace VFM.Web.Controllers.API
{
    [RoutePrefix("api/SupplierFuelsPrices")]
    public class SupplierFuelsPricesAPIController : ApiController
    {
        [Route, HttpPost]
        public HttpResponseMessage AddSupplierFuelsPrice(AddSupplierFuelsPriceRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemResponse<int> response = new ItemResponse<int>();
            response.Item = SupplierFuelsPricesService.UpdateSupplierFuelsPrice(model);
            return Request.CreateResponse(response);
        }

        [Route("{id:int}"), HttpPut]
        public HttpResponseMessage UpdateSupplierFuelsPrice(UpdateSupplierFuelsPriceRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            SuccessResponse response = new SuccessResponse();
            SupplierFuelsPricesService.UpdateSupplierFuelsPrice(model);
            return Request.CreateResponse(response);
        }

        [Route("{id:int}"), HttpGet]
        public HttpResponseMessage GetSupplierFuelsPrice(int id)
        {
            ItemResponse<SupplierFuelsPrices> response = new ItemResponse<SupplierFuelsPrices>();
            response.Item = SupplierFuelsPricesService.GetSupplierFuelsPrice(id);
            return Request.CreateResponse(response);
        }

        [Route, HttpGet]
        public HttpResponseMessage GetListOfSupplierFuelsPrices()
        {
            ItemsResponse<SupplierFuelsPrices> response = new ItemsResponse<SupplierFuelsPrices>();
            response.Items = SupplierFuelsPricesService.GetListOfSupplierFuelsPrices();
            return Request.CreateResponse(response);
        }

        [Route("List/{adminId:int}"), HttpGet]
        public HttpResponseMessage GetSupplierFuelsPricesByAdmin(int adminId)
        {
            ItemsResponse<SupplierFuelsPrices> response = new ItemsResponse<SupplierFuelsPrices>();
            response.Items = SupplierFuelsPricesService.GetSupplierFuelsPricesByAdminClientID(adminId);
            return Request.CreateResponse(response);
        }

        [Route("{adminId:int}/{supplierId:int}"), HttpDelete]
        public HttpResponseMessage DeleteSupplierFuelsPrice(int adminId, int supplierId)
        {
            SuccessResponse response = new SuccessResponse();
            SupplierFuelsPricesService.DeleteSupplierFuelsPrice(adminId, supplierId);
            return Request.CreateResponse(response);
        }

        [Route("Export"), HttpPost]
        public string ExportPrices(ExportDataRequest model/*int adminId, string adminName*/)
        {
            var table = GetSupplierFuelsPricesDataTable(model.ClientID);
            DataTable copyDataTable;
            copyDataTable = table.Copy();
            copyDataTable.PrimaryKey = null;
            copyDataTable.Columns.Remove("Id");
            copyDataTable.Columns.Remove("AdminClientID");
            copyDataTable.Columns.Remove("SupplierID");
            copyDataTable.Columns.Remove("DateUploaded");
            copyDataTable.Columns["EffectiveDate"].SetOrdinal(7);
            string filePath = ExportHelper.CreateFilePath("VendorFuelsPrices.csv", model.ClientName);
            CSVGenerator.GenerateCSV(copyDataTable, filePath);
            return ExportHelper.GetFilePathFromRoot(filePath);
        }

        [Route("{adminId:int}/{icao:alpha}"), HttpGet]
        public HttpResponseMessage GetSupplierFuelPricesByICAO(int adminId, string icao)
        {
            ItemsResponse<SupplierFuelsPrices> response = new ItemsResponse<SupplierFuelsPrices>();
            response.Items = SupplierFuelsPricesService.GetSupplierFuelsPricesByICAO(adminId, icao);
            return Request.CreateResponse(response);
        }

        #region Private Methods
        private VFMClasses.DataSets.SupplierFuelPricesDataSet.SupplierFuelsPricesDataTable GetSupplierFuelsPricesDataTable(int adminId)
        {
            SupplierFuelsPricesTableAdapter adapter = new SupplierFuelsPricesTableAdapter();
            adapter.Connection = new SqlConnection(Degatech.Utilities.SQL.ConnectionHelper.GetConnectionString("SQLFLString"));
            VFMClasses.DataSets.SupplierFuelPricesDataSet.SupplierFuelsPricesDataTable table = adapter.GetData(adminId);
            return table;
        }
        #endregion

    }
}
