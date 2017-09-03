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
using VFMClasses.DataSets.AirportsTableAdapters;
using System.Data.SqlClient;

namespace VFM.Web.Controllers.API
{
    [RoutePrefix("api/AirportPriceMargins")]
    public class AirportPriceMarginsAPIController : ApiController
    {
        [Route, HttpPost]
        public HttpResponseMessage AddPriceMargin(AddAirportPriceMarginRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemResponse<int> response = new ItemResponse<int>();
            response.Item = AirportPriceMarginsService.UpdateAirportPriceMargin(model);
            //ItemsResponse<int> response = new ItemsResponse<int>();
            //foreach (AddAirportPriceMarginRequest model in models)
            //{
            //    response.Items.Add(AirportPriceMarginsService.UpdateAirportPriceMargin(model));
            //}
            return Request.CreateResponse(response);
        }

        [Route, HttpPut]
        public HttpResponseMessage UpdatePriceMargin(AddAirportPriceMarginRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            SuccessResponse response = new SuccessResponse();
            AirportPriceMarginsService.UpdateAirportPriceMargin(model);
            return Request.CreateResponse(response);
        }

        [Route("{id:int}"), HttpGet]
        public HttpResponseMessage GetPriceMargin(int id)
        {
            ItemResponse<AirportPriceMargins> response = new ItemResponse<AirportPriceMargins>();
            response.Item = AirportPriceMarginsService.GetAirportPriceMargin(id);
            return Request.CreateResponse(response);
        }

        [Route, HttpGet]
        public HttpResponseMessage GetListOfAirportPriceMargins()
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemsResponse<AirportPriceMargins> response = new ItemsResponse<AirportPriceMargins>();
            response.Items = AirportPriceMarginsService.GetListOfAirportPriceMargins();
            return Request.CreateResponse(response);
        }

        [Route("Export"), HttpPost]
        public string ExportByClientID(ExportDataRequest model)
        {
            if (!ModelState.IsValid)
            {
                return (Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState)).ToString();
            }
            var table = GetAirports(model.ClientID, model.ListOfIDs);
            string filePath = ExportHelper.CreateFilePath("Airports.csv", model.ClientName);
            CSVGenerator.GenerateCSV(table, filePath);
            return ExportHelper.GetFilePathFromRoot(filePath);
        }

        [Route("{id:int}"), HttpDelete]
        public HttpResponseMessage DeletePriceMargin(int id)
        {
            SuccessResponse response = new SuccessResponse();
            AirportPriceMarginsService.DeleteAirportPriceMargin(id);
            return Request.CreateResponse(response);
        }

        #region Private Methods
        private VFMClasses.DataSets.Airports.AcukwikAirportsAndMarginsDataTable GetAirports(int adminId, string airportIDs)
        {
            AcukwikAirportsAndMarginsAdapter adapter = new AcukwikAirportsAndMarginsAdapter();
            adapter.Connection = new SqlConnection(Degatech.Utilities.SQL.ConnectionHelper.GetConnectionString("SQLFLString"));
            VFMClasses.DataSets.Airports.AcukwikAirportsAndMarginsDataTable table = adapter.GetDataBy(adminId, airportIDs);
            return table;
        }
        #endregion
    }
}
