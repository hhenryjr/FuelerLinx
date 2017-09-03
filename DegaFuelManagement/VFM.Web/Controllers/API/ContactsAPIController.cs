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
using VFMClasses.Exports;
using Degatech.Common;
using VFMClasses.DataSets.CompaniesTableAdapters;
using System.Data.SqlClient;

namespace VFM.Web.Controllers.API
{
    [RoutePrefix("api/Contacts")]
    public class ContactsAPIController : ApiController
    {
        [Route, HttpPost]
        public HttpResponseMessage AddContact(AddContactRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemResponse<int> response = new ItemResponse<int>();
            response.Item = ContactsService.UpdateContact(model);
            return Request.CreateResponse(response);
        }

        [Route("{id:int}"), HttpPut]
        public HttpResponseMessage UpdateContact(UpdateContactRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            SuccessResponse response = new SuccessResponse();
            ContactsService.UpdateContact(model);
            return Request.CreateResponse(response);
        }

        [Route("{id:int}"), HttpGet]
        public HttpResponseMessage GetContact(int id)
        {
            ItemResponse<Contacts> response = new ItemResponse<Contacts>();
            response.Item = ContactsService.GetContactInfo(id);
            return Request.CreateResponse(response);
        }

        [Route, HttpGet]
        public HttpResponseMessage GetListOfContacts()
        {
            ItemsResponse<Contacts> response = new ItemsResponse<Contacts>();
            response.Items = ContactsService.GetListOfContacts();
            return Request.CreateResponse(response);
        }

        [Route("Admin/{clientId:int}"), HttpGet]
        public HttpResponseMessage GetContactsByAdminClientID(int clientId)
        {
            ItemsResponse<Contacts> response = new ItemsResponse<Contacts>();
            response.Items = ContactsService.GetContactsByAdminClientID(clientId);
            return Request.CreateResponse(response);
        }

        [Route("Cust/{clientId:int}"), HttpGet]
        public HttpResponseMessage GetContactsByCustClientID(int clientId)
        {
            ItemsResponse<Contacts> response = new ItemsResponse<Contacts>();
            response.Items = ContactsService.GetContactsByCustClientID(clientId);
            return Request.CreateResponse(response);
        }

        [Route("Export"), HttpPost]
        public string ExportContacts(ExportDataRequest model)
        {
            var table = GetContacts(model.ClientID, model.ListOfIDs);
            string filePath = ExportHelper.CreateFilePath("Contacts.csv", model.ClientName);
            CSVGenerator.GenerateCSV(table, filePath);
            return ExportHelper.GetFilePathFromRoot(filePath);
        }

        [Route("{id:int}"), HttpDelete]
        public HttpResponseMessage DeleteContact(int id)
        {
            SuccessResponse response = new SuccessResponse();
            ContactsService.DeleteContact(id);
            return Request.CreateResponse(response);
        }

        #region Private Methods
        private VFMClasses.DataSets.Companies.ContactsDataTable GetContacts(int adminID, string companyIDs)
        {
            ContactsTableAdapter adapter = new ContactsTableAdapter();
            adapter.Connection = new SqlConnection(Degatech.Utilities.SQL.ConnectionHelper.GetConnectionString("SQLFLString"));
            VFMClasses.DataSets.Companies.ContactsDataTable table = adapter.GetData(adminID, companyIDs);
            return table;
        }
        #endregion
    }
}
