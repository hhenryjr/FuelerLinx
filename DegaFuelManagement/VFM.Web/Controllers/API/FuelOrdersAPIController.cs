using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Degatech.Common;
using VFM.Web.Models.Responses;
using VFM.Web.Models.Requests;
using VFM.Web.Services;
using VFMClasses;
using VFMClasses.DataSets.FuelOrdersTableAdapters;
using VFMClasses.Exports;
using System.Data;

namespace VFM.Web.Controllers.API
{
    [RoutePrefix("api/FuelOrders")]
    public class FuelOrdersAPIController : ApiController
    {
        [Route, HttpPost]
        public HttpResponseMessage AddFuelOrder(AddFuelOrdersRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemResponse<int> response = new ItemResponse<int>();
            response.Item = FuelOrdersService.UpdateFuelOrder(model);
            return Request.CreateResponse(response);
        }

        [Route("List"), HttpPost]
        public HttpResponseMessage AddList(AddFuelOrdersRequest[] models)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemsResponse<int> response = new ItemsResponse<int>();
            foreach (AddFuelOrdersRequest model in models)
            {
                response.Items.Add(FuelOrdersService.UpdateFuelOrder(model));
            }
            return Request.CreateResponse(response);
        }

        [Route("{id:int}"), HttpPut]
        public HttpResponseMessage UpdateFuelOrder(UpdateFuelOrderRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            SuccessResponse response = new SuccessResponse();
            FuelOrdersService.UpdateFuelOrder(model);
            return Request.CreateResponse(response);
        }

        [Route("List"), HttpPut]
        public HttpResponseMessage UpdateList(UpdateFuelOrderRequest[] models)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            SuccessResponse response = new SuccessResponse();
            foreach (AddFuelOrdersRequest model in models)
            {
                FuelOrdersService.UpdateFuelOrder(model);
            }
            return Request.CreateResponse(response);
        }

        [Route("{id:int}"), HttpGet]
        public HttpResponseMessage GetFuelOrder(int id)
        {
            ItemResponse<FuelOrders> response = new ItemResponse<FuelOrders>();
            response.Item = FuelOrdersService.GetFuelOrder(id);
            return Request.CreateResponse(response);
        }

        [Route, HttpGet]
        public HttpResponseMessage GetListOfFuelOrders()
        {
            ItemsResponse<FuelOrders> response = new ItemsResponse<FuelOrders>();
            response.Items = FuelOrdersService.GetListOfFuelOrders();
            return Request.CreateResponse(response);
        }

        [Route("Admin"), HttpPost]
        public HttpResponseMessage GetFuelOrdersByAdminClient(GetFuelOrdersRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemsResponse<FuelOrders> response = new ItemsResponse<FuelOrders>();
            response.Items = FuelOrdersService.GetFuelOrdersByAdminClient(model.ClientID, model.StartDate, model.EndDate);
            return Request.CreateResponse(response);
        }

        [Route("Cust"), HttpPost]
        public HttpResponseMessage GetFuelOrdersByCustClient(GetFuelOrdersRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemsResponse<FuelOrders> response = new ItemsResponse<FuelOrders>();
            response.Items = FuelOrdersService.GetFuelOrdersByCustClient(model.ClientID, model.StartDate, model.EndDate);
            return Request.CreateResponse(response);
        }

        [Route("{id:int}"), HttpDelete]
        public HttpResponseMessage DeleteFuelOrder(int id)
        {
            SuccessResponse response = new SuccessResponse();
            FuelOrdersService.DeleteFuelOrder(id);
            return Request.CreateResponse(response);
        }

        [Route("List"), HttpDelete]
        public HttpResponseMessage DeleteList(UpdateFuelOrderRequest[] models)
        {
            SuccessResponse response = new SuccessResponse();                        
            foreach (UpdateFuelOrderRequest model in models)
            {
                FuelOrdersService.DeleteFuelOrder(model.Id);
            }
            return Request.CreateResponse(response);
        }

        [Route("GetRanking"), HttpPost]
        public string GetCustomerRankingData(GetDashboardRequest model)
        {
            var table = GetCustomerRankingDataTable(model.ClientID, model.StartDateFilter.ToShortDateString(), model.EndDateFilter.ToShortDateString());
            return Newtonsoft.Json.JsonConvert.SerializeObject(table);
        }

        [Route("ExportRanking"), HttpPost]
        public string ExportCustomerRankingData(GetDashboardRequest model)
        {
            var table = GetCustomerRankingDataTable(model.ClientID, model.StartDateFilter.ToShortDateString(), model.EndDateFilter.ToShortDateString());
            string filePath = ExportHelper.CreateFilePath("FuelOrdersCustomerRanking.csv", model.ClientName);
            CSVGenerator.GenerateCSV(table, filePath);
            return ExportHelper.GetFilePathFromRoot(filePath);
        }

        [Route("Analysis"), HttpPost]
        public string GetFuelOrderHistory(GetDashboardRequest model)
        {
            var table = GetTransactionHistory(model.ClientID, model.StartDateFilter.ToShortDateString(), model.EndDateFilter.ToShortDateString());
            return Newtonsoft.Json.JsonConvert.SerializeObject(table);
        }

        [Route("GetTotal/Admin"), HttpPost]
        public string GetTotalByStatusData(GetDashboardRequest model)
        {
            var table = GetTotalFuelOrdersByStatusDataTable(model.ClientID, model.StartDateFilter.ToShortDateString(), model.EndDateFilter.ToShortDateString());
            return Newtonsoft.Json.JsonConvert.SerializeObject(table);
        }

        [Route("GetTotal/Cust"), HttpPost]
        public string GetCustTotalByStatusData(GetDashboardRequest model)
        {
            var table = GetTotalCustFuelOrdersByStatusDataTable(model.ClientID, model.StartDateFilter.ToShortDateString(), model.EndDateFilter.ToShortDateString());
            return Newtonsoft.Json.JsonConvert.SerializeObject(table);
        }

        [Route("Export/Admin"), HttpPost]
        public string ExportFuelOrdersByAdminClient(ExportDataRequest model)
        {
            var table = ExportFuelOrders(model.ClientID, model.ListOfIDs, model.StartDate.ToShortDateString(), model.EndDate.ToShortDateString());
            string filePath = ExportHelper.CreateFilePath("FuelOrderTransactions.csv", model.ClientName);
            CSVGenerator.GenerateCSV(table, filePath);
            return ExportHelper.GetFilePathFromRoot(filePath);
        }

        [Route("Export/Cust"), HttpPost]
        public string ExportFuelOrdersByCustClient(ExportDataRequest model)
        {
            var table = ExportFuelOrdersByCust(model.ClientID, model.ListOfIDs, model.StartDate.ToShortDateString(), model.EndDate.ToShortDateString());
            DataTable copyDataTable;
            copyDataTable = table.Copy();
            copyDataTable.PrimaryKey = null;
            copyDataTable.Columns["Name"].SetOrdinal(0);
            copyDataTable.Columns["RequestedBy"].SetOrdinal(1);
            copyDataTable.Columns["TailNumber"].SetOrdinal(4);
            copyDataTable.Columns["Product"].SetOrdinal(5);
            copyDataTable.Columns["PPG"].SetOrdinal(6);
            copyDataTable.Columns["DateRequested"].SetOrdinal(7);
            copyDataTable.Columns["FuelingDateTime"].SetOrdinal(8);
            copyDataTable.Columns["PostedRetail"].SetOrdinal(9);
            copyDataTable.Columns["QuotedPPG"].SetOrdinal(10);
            copyDataTable.Columns["InvoicedPPG"].SetOrdinal(11);
            copyDataTable.Columns["QuotedVolume"].SetOrdinal(12);
            copyDataTable.Columns["InvoicedVolume"].SetOrdinal(13);
            copyDataTable.Columns["RampFee"].SetOrdinal(14);
            copyDataTable.Columns["RampFeeWaivedAt"].SetOrdinal(15);
            copyDataTable.Columns["InvoiceStatus"].SetOrdinal(16);
            copyDataTable.Columns["CustNotes"].SetOrdinal(17);
            string filePath = ExportHelper.CreateFilePath("FuelOrderTransactions.csv", model.ClientName);
            CSVGenerator.GenerateCSV(copyDataTable, filePath);
            return ExportHelper.GetFilePathFromRoot(filePath);
        }

        [Route("ExportTotal/Admin"), HttpPost]
        public string ExportTotalByStatusData(GetDashboardRequest model)
        {
            var table = GetTotalFuelOrdersByStatusDataTable(model.ClientID, model.StartDateFilter.ToShortDateString(), model.EndDateFilter.ToShortDateString());
            string filePath = ExportHelper.CreateFilePath("FuelOrdersTotalByStatus.csv", model.ClientName);
            CSVGenerator.GenerateCSV(table, filePath);
            return ExportHelper.GetFilePathFromRoot(filePath);
        }

        [Route("ExportTotal/Cust"), HttpPost]
        public string ExportCustTotalByStatusData(GetDashboardRequest model)
        {
            var table = GetTotalCustFuelOrdersByStatusDataTable(model.ClientID, model.StartDateFilter.ToShortDateString(), model.EndDateFilter.ToShortDateString());
            string filePath = ExportHelper.CreateFilePath("FuelOrdersTotalByStatus.csv", model.ClientName);
            CSVGenerator.GenerateCSV(table, filePath);
            return ExportHelper.GetFilePathFromRoot(filePath);
        }

        [Route("Summary"), HttpPost]
        public string GetGeneralSummaryData(GetDashboardRequest model)
        {
            var table = GetFuelOrdersGeneralSummaryDataTable(model.ClientID, model.StartDateFilter.ToShortDateString(), model.EndDateFilter.ToShortDateString());
            return Newtonsoft.Json.JsonConvert.SerializeObject(table);
        }

        [Route("DispatchEmails"), HttpPost]
        public HttpResponseMessage DispatchEmails(DispatchEmailsRequest model)
        {
            SuccessResponse response = new SuccessResponse();
            FuelOrderDispatchRequestForRelease fuelOrderDispatchConfirmation = new FuelOrderDispatchRequestForRelease();
            fuelOrderDispatchConfirmation.SendFuelRequestForRelease(model.fuelOrderId);
            FuelOrderDispatchVendorRequestForRelease fuelOrderVendorDispatchConfirmation =
                new FuelOrderDispatchVendorRequestForRelease();
            fuelOrderVendorDispatchConfirmation.SendFuelRequestForRelease(model.fuelOrderId);
            return Request.CreateResponse(response);
        }

        [Route("SendRequestConfirmation"), HttpPost]
        public HttpResponseMessage sendRequestConfirmation(DispatchEmailsRequest model)
        {
            SuccessResponse response = new SuccessResponse();
            FuelOrderRequestConfirmation fuelOrderRequestConfirmation = new FuelOrderRequestConfirmation();
            fuelOrderRequestConfirmation.SendFuelRequestConfirmation(model.fuelOrderId);
            return Request.CreateResponse(response);
        }

        [Route("FutureOrders"), HttpPost]
        public FuelOrdersCollection GetFutureOrders(FutureOrdersRequest model)
        {
            FuelOrdersCollection futureFuelOrders = new FuelOrdersCollection();
            futureFuelOrders.LoadFutureOrders(model.AdminClientId);
            return futureFuelOrders;
        }

        [Route("HighestTransactionIDFromDatabase/Admin"), HttpPost]
        public int HighestAdminTransactionIDFromDatabase(RequestBase request)
        {
            var HighestTransactionIDFromDatabase = GetHighestTransactionIDFromDatabase(request.ClientID, "");
            return HighestTransactionIDFromDatabase;
        }

        [Route("HighestTransactionIDFromDatabase/Cust"), HttpPost]
        public int HighestCustTransactionIDFromDatabase(RequestBase request)
        {
            var HighestTransactionIDFromDatabase = GetHighestTransactionIDFromDatabase(request.ClientID, "cust");
            return HighestTransactionIDFromDatabase;
        }

        #region Private Methods
        private VFMClasses.DataSets.FuelOrders.CustomerRankingDataTable GetCustomerRankingDataTable(int adminClientId,
            string startDate, string endDate)
        {
            CustomerRankingTableAdapter adapter = new CustomerRankingTableAdapter();
            adapter.Connection =
                new SqlConnection(Degatech.Utilities.SQL.ConnectionHelper.GetConnectionString("SQLFLString"));
            VFMClasses.DataSets.FuelOrders.CustomerRankingDataTable table = adapter.GetData(DateTime.Parse(startDate),
                DateTime.Parse(endDate), adminClientId);
            return table;
        }

        private VFMClasses.DataSets.FuelOrders.TotalByStatusDataTable GetTotalFuelOrdersByStatusDataTable(int adminClientId,
            string startDate, string endDate)
        {
            TotalByStatusTableAdapter adapter = new TotalByStatusTableAdapter();
            adapter.Connection =
                new SqlConnection(Degatech.Utilities.SQL.ConnectionHelper.GetConnectionString("SQLFLString"));
            VFMClasses.DataSets.FuelOrders.TotalByStatusDataTable table = adapter.GetData(DateTime.Parse(startDate),
                DateTime.Parse(endDate), adminClientId);
            return table;
        }

        private VFMClasses.DataSets.FuelOrders.TotalByStatusDataTable GetTransactionHistory(int adminClientId,
            string startDate, string endDate)
        {
            TotalByStatusTableAdapter adapter = new TotalByStatusTableAdapter();
            adapter.Connection =
                new SqlConnection(Degatech.Utilities.SQL.ConnectionHelper.GetConnectionString("SQLFLString"));
            VFMClasses.DataSets.FuelOrders.TotalByStatusDataTable table = adapter.GetAnalysis(DateTime.Parse(startDate),
                DateTime.Parse(endDate), adminClientId);
            return table;
        }

        private VFMClasses.DataSets.FuelOrders.TotalByStatusDataTable GetTotalCustFuelOrdersByStatusDataTable(int custClientId,
            string startDate, string endDate)
        {
            TotalByStatusTableAdapter adapter = new TotalByStatusTableAdapter();
            adapter.Connection =
                new SqlConnection(Degatech.Utilities.SQL.ConnectionHelper.GetConnectionString("SQLFLString"));
            VFMClasses.DataSets.FuelOrders.TotalByStatusDataTable table = adapter.GetCustData(DateTime.Parse(startDate),
                DateTime.Parse(endDate), custClientId, Degatech.Utilities.Data.Utilities.Domain());
            return table;
        }

        private VFMClasses.DataSets.FuelOrders.GeneralSummaryDataTable GetFuelOrdersGeneralSummaryDataTable(int adminClientId,
            string startDate, string endDate)
        {
            GeneralSummaryTableAdapter adapter = new GeneralSummaryTableAdapter();
            adapter.Connection =
                new SqlConnection(Degatech.Utilities.SQL.ConnectionHelper.GetConnectionString("SQLFLString"));
            VFMClasses.DataSets.FuelOrders.GeneralSummaryDataTable table = adapter.GetData(DateTime.Parse(startDate),
                DateTime.Parse(endDate), adminClientId);
            return table;
        }

        private VFMClasses.DataSets.FuelOrders.TransactionsDataTable ExportFuelOrders(int adminClientId, string fuelOrderIDs,
            string startDate, string endDate)
        {
            TransactionsTableAdapter adapter = new TransactionsTableAdapter();
            adapter.Connection =
                new SqlConnection(Degatech.Utilities.SQL.ConnectionHelper.GetConnectionString("SQLFLString"));
            VFMClasses.DataSets.FuelOrders.TransactionsDataTable table = adapter.ExportDataByAdminID(adminClientId, fuelOrderIDs,
                DateTime.Parse(startDate), DateTime.Parse(endDate));
            return table;
        }

        private VFMClasses.DataSets.FuelOrders.TransactionsDataTable ExportFuelOrdersByCust(int custClientId, string fuelOrderIDs,
            string startDate, string endDate)
        {
            TransactionsTableAdapter adapter = new TransactionsTableAdapter();
            adapter.Connection =
                new SqlConnection(Degatech.Utilities.SQL.ConnectionHelper.GetConnectionString("SQLFLString"));
            VFMClasses.DataSets.FuelOrders.TransactionsDataTable table = adapter.ExportDataByCust(custClientId, fuelOrderIDs, DateTime.Parse(startDate),
                DateTime.Parse(endDate));
            return table;
        }

        private VFMClasses.DataSets.FuelOrders.TransactionsDataTable GetTransactionsByAdmin(int adminClientId,
            string startDate, string endDate)
        {
            TransactionsTableAdapter adapter = new TransactionsTableAdapter();
            adapter.Connection =
                new SqlConnection(Degatech.Utilities.SQL.ConnectionHelper.GetConnectionString("SQLFLString"));
            VFMClasses.DataSets.FuelOrders.TransactionsDataTable table = adapter.GetDataByAdmin(adminClientId, DateTime.Parse(startDate),
                DateTime.Parse(endDate));
            return table;
        }

        private VFMClasses.DataSets.FuelOrders.TransactionsDataTable GetTransactionsByCust(int custClientId, 
            string startDate, string endDate)
        {
            TransactionsTableAdapter adapter = new TransactionsTableAdapter();
            adapter.Connection =
                new SqlConnection(Degatech.Utilities.SQL.ConnectionHelper.GetConnectionString("SQLFLString"));
            VFMClasses.DataSets.FuelOrders.TransactionsDataTable table = adapter.GetDataByCust(custClientId, DateTime.Parse(startDate),
                DateTime.Parse(endDate));
            return table;
        }

        private int GetHighestTransactionIDFromDatabase(int clientID, string clientType)
        {
            FuelOrdersCollection fuelOrderCollection = new FuelOrdersCollection();
            return fuelOrderCollection.GetHighestTransactionID(clientID, clientType);
        }
        #endregion
    }
}
