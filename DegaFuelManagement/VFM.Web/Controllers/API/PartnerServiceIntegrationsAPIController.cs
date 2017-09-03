using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web.Http;
using VFM.API.EntityServices;
using VFM.EDM;
using VFM.Web.Models.Requests;
using VFM.Web.Models.Responses;
using VFM.Web.Services;
using VFMClasses;
using VFMClasses.FuelOrder;
using MailMessage = Degatech.Utilities.Mail.MailMessage;

namespace VFM.Web.Controllers.API
{
    [RoutePrefix("api/integrations")]
    public class PartnerServiceIntegrationsAPIController : ApiController
    {
        string badResponse = "The FuelerLinx account number does not match our records.";

        [Route("account"), HttpPost]
        public HttpResponseMessage CreateAccount(PartnerServiceIntegration serviceIntegration)
        {
            AddGUID(ref serviceIntegration);

            ItemResponse<PartnerServiceIntegration> response = new ItemResponse<PartnerServiceIntegration>();
            PartnerServiceIntegrations integrations = new PartnerServiceIntegrations();
            response.Item = integrations.Update(serviceIntegration);
            return Request.CreateResponse(response);
        }

        [Route("FuelQuote"), HttpPost]
        public HttpResponseMessage GetFuelQuote(PartnerServiceIntegrationGetQuoteRequest model)
        {
            if (!CheckAccount(model))
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, badResponse);

            FuelOrderPricingsCollection quotes = new FuelOrderPricingsCollection();

            foreach (string ICAO in model.ICAO.Split(','))
            {
                FuelOrderPricingsCollection quote = new FuelOrderPricingsCollection();

                if (ICAO != "")
                    quotes.AddRange(FuelOrderPricingsService.GetQuoteForLocation(model.AdminClientID, model.CustClientID,
                        ICAO, model.TailNumber));
            }

            ItemsResponse<FuelOrderPricings> response = new ItemsResponse<FuelOrderPricings>();
            response.Items = quotes;
            return Request.CreateResponse(response);
        }

        [Route("AddFuelOrder"), HttpPost]
        public HttpResponseMessage CreateFuelOrder(PartnerServiceIntegrationAddFuelOrderRequest model)
        {
            if (!CheckAccount(model))
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, badResponse);

            AircraftsCollection aircrafts = new AircraftsCollection();
            aircrafts.LoadByAdminAndCustClientID(model.AdminClientID, model.CustClientID);
            Aircrafts aircraft = aircrafts.GetTailNumber(model.TailNumber);
            if (aircraft != null)
                model.AircraftID = aircraft.Id;

            model.IsFromFuelerLinx = true;
            ItemResponse<int> response = new ItemResponse<int>();
            response.Item = FuelOrdersService.UpdateFuelOrder(model);
            return Request.CreateResponse(response);
        }

        [Route("UpdateFuelOrder"), HttpPost]
        public HttpResponseMessage UpdateFuelOrder(UpdateFuelOrderRequest model)
        {
            if (!CheckAccount(model))
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, badResponse);

            FuelOrders fuelOrder = new FuelOrders(model.Id);
            fuelOrder.IsFromFuelerLinx = true;
            fuelOrder.TailNumber = model.TailNumber;
            fuelOrder.AdminStatus = model.AdminStatus;
            fuelOrder.CustStatus = model.CustStatus;
            fuelOrder.QuotedPPG = model.QuotedPPG;
            fuelOrder.FuelingDateTime = model.FuelingDateTime;
            fuelOrder.FuelOn = model.FuelOn;
            fuelOrder.CertificateType = model.CertificateType;
            fuelOrder.FuelingDateTime = model.FuelingDateTime;
            fuelOrder.InvoicedPPG = model.InvoicedPPG;
            fuelOrder.InvoicedVolume = model.InvoicedVolume;
            fuelOrder.BasePPG = model.BasePPG;
            fuelOrder.TripID = model.TripID;
            fuelOrder.PostedRetail = model.PostedRetail;
            fuelOrder.PlattsPrice = model.PlattsPrice;
            fuelOrder.AdminNotes = model.AdminNotes;
            fuelOrder.RampFee = model.RampFee;
            fuelOrder.RampFeeWaivedAt = model.RampFeeWaivedAt;
            fuelOrder.InvoiceNumber = model.InvoiceNumber;
            fuelOrder.InvoiceStatus = model.InvoiceStatus;
            fuelOrder.NoFuel = model.NoFuel;
            ItemResponse<int> response = new ItemResponse<int>();
            response.Item = FuelOrdersService.UpdateFuelOrder(fuelOrder);
            return Request.CreateResponse(response);
        }

        [Route("GetFuelOrder"), HttpPost]
        public HttpResponseMessage GetFuelOrder(UpdateFuelOrderRequest model)
        {
            if (!CheckAccount(model))
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, badResponse);

            ItemResponse<FuelOrders> response = new ItemResponse<FuelOrders>();
            response.Item = FuelOrdersService.GetFuelOrder(model.Id);
            return Request.CreateResponse(response);
        }

        [Route("AddInvoice"), HttpPost]
        public HttpResponseMessage AddInvoice(PartnerServiceFuelOrderInvoiceRequest model)
        {
            if (!CheckAccount(model))
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, badResponse);

            FuelOrderInvoices invoice = new FuelOrderInvoices();
            invoice.FuelOrderID = model.FuelOrderID;
            invoice.InvoiceName = model.InvoiceName;
            invoice.ContentType = model.ContentType;
            invoice.InvoiceData = model.InvoiceData;
            invoice.Update();

            ItemResponse<bool> response = new ItemResponse<bool>();
            response.Item = true;
            return Request.CreateResponse(response);
        }

        [Route("DeleteInvoice"), HttpPost]
        public HttpResponseMessage DeleteInvoice(PartnerServiceFuelOrderInvoiceRequest model)
        {
            if (!CheckAccount(model))
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, badResponse);

            FuelOrderInvoices invoice = new FuelOrderInvoices();
            invoice.FuelOrderID = model.FuelOrderID;
            invoice.InvoiceName = model.InvoiceName;
            invoice.DeleteFromDatabase();

            ItemResponse<bool> response = new ItemResponse<bool>();
            response.Item = true;
            return Request.CreateResponse(response);
        }

        [Route("GetInvoices"), HttpPost]
        public HttpResponseMessage GetInvoices(UpdateFuelOrderRequest model)
        {
            if (!CheckAccount(model))
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, badResponse);

            ItemsResponse<FuelOrderInvoices> response = new ItemsResponse<FuelOrderInvoices>();
            response.Items = FuelOrderInvoicesService.GetInvoicesByFuelOrder(model.Id);
            return Request.CreateResponse(response);
        }

        #region Public Methods

        public void AddGUID(ref PartnerServiceIntegration serviceIntegration)
        {
            if (serviceIntegration.AccountNumber == Guid.Empty)
            {
                var partnerService = new PartnerServiceIntegrations();
                var Integration = partnerService.GetByClientID(serviceIntegration.ClientID, serviceIntegration.AdminClientID);
                if (Integration != null)
                {
                    serviceIntegration.AccountNumber = Integration.AccountNumber;
                    serviceIntegration.Id = Integration.Id;
                }
            }

            if (serviceIntegration.AccountNumber == Guid.Empty)
            {
                serviceIntegration.AccountNumber = Guid.NewGuid();

                if (serviceIntegration.AdminClientID == 0)
                    serviceIntegration.AdminClientID = Users.CurrentUser.ClientID;

                //Send an email notification to support@fuelerlinx.com containing the customer client's name
                EmailRoutingCollection emailsCollection = EmailRouting.GetEmailsByAdminClient(serviceIntegration.AdminClientID);
                EmailRouting emailRouting = new EmailRouting();
                foreach (EmailRouting emails in emailsCollection)
                {
                    if (emails.EmailType == "New Client Activated")
                    {
                        emailRouting = emails;
                        break;
                    }
                }

                Clients client = new Clients(serviceIntegration.ClientID);

                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(emailRouting.FromEmail);
                mailMessage.To.Add(new MailAddress("support@fuelerlinx.com"));

                mailMessage.IsBodyHtml = true;
                mailMessage.Priority = MailPriority.Normal;
                mailMessage.Body = client.Name + " was activated from " + Users.CurrentUser.Client.Name +
                                   ". Their account number is: " + serviceIntegration.AccountNumber.ToString();

                try
                {
                    Degatech.Utilities.Data.Utilities.SendEmail(mailMessage);
                }
                catch (Exception ex)
                {

                }
            }
        }
        #endregion

        #region Private Methods
        private bool CheckAccount(PartnerServiceIntegrationAddFuelOrderRequest model)
        {
            PartnerServiceIntegrations integration = new PartnerServiceIntegrations();
            var partner = integration.GetByAccountID(model.AccountId);
            if (partner == null)
                return false;
            var AdminClient = new Clients(partner.AdminClientID);
            if (!AdminClient.Name.ToLower().Contains(model.ProcessName))
                return false;
            model.AdminClientID = partner.AdminClientID;
            model.CustClientID = partner.ClientID;
            return true;
        }

        private bool CheckAccount(UpdateFuelOrderRequest model)
        {
            PartnerServiceIntegrations integration = new PartnerServiceIntegrations();
            var partner = integration.GetByAccountID(model.AccountId);
            if (partner == null)
                return false;
            var AdminClient = new Clients(partner.AdminClientID);
            if (!AdminClient.Name.ToLower().Contains(model.ProcessName))
                return false;
            model.AdminClientID = partner.AdminClientID;
            model.CustClientID = partner.ClientID;
            return true;
        }

        private bool CheckAccount(PartnerServiceFuelOrderInvoiceRequest model)
        {
            PartnerServiceIntegrations integration = new PartnerServiceIntegrations();
            var partner = integration.GetByAccountID(model.AccountId);
            if (partner == null)
                return false;
            var AdminClient = new Clients(partner.AdminClientID);
            if (!AdminClient.Name.ToLower().Contains(model.ProcessName))
                return false;
            return true;
        }

        private bool CheckAccount(PartnerServiceIntegrationGetQuoteRequest model)
        {
            PartnerServiceIntegrations integration = new PartnerServiceIntegrations();
            var partner = integration.GetByAccountID(model.AccountId);
            if (partner == null)
                return false;
            var AdminClient = new Clients(partner.AdminClientID);
            if (!AdminClient.Name.ToLower().Contains(model.ProcessName))
                return false;
            model.AdminClientID = partner.AdminClientID;
            model.CustClientID = partner.ClientID;
            return true;
        }
        #endregion
    }
}
