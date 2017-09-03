using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net;
using Degatech.Utilities.Data;
using Degatech.Utilities.Exceptions;
using Degatech.Utilities.SQL;
using VFMClasses;

public class FuelOrderDispatchRequestForRelease : EmailDispatches
{
    #region Members

    private FuelOrders _FuelOrder;
    private CustomerDetails _Customer;
    private FuelOrderCustomerPricingsCollection _VolumeDiscounts;
    private Clients _Client;

    #endregion

    #region Constructors

    public FuelOrderDispatchRequestForRelease()
    {
        
    }
    #endregion


    #region Public Methods

    public void SendFuelRequestForRelease(FuelOrders fuelOrder)
    {
        _FuelOrder = fuelOrder;
        SendRequestForRelease();
    }

    public void SendFuelRequestForRelease(int fuelOrderId)
    {
       _FuelOrder = new FuelOrders(fuelOrderId);
       SendRequestForRelease();
    }

    #endregion

    #region Private Methods

    private void SendRequestForRelease()
    {
        _Customer = new CustomerDetails(_FuelOrder.CustClientID);
        _Client = new Clients(_FuelOrder.AdminClientID);
        _Customer.Contacts = new ContactsCollection();
        _Customer.Contacts.LoadByCustClientID(_Customer.CustClientID);


        try
        {
            if (_Customer.Contacts.Count > 0)
            {
                if (!string.IsNullOrEmpty(_Customer.Email) && !_Customer.Email.EndsWith(";"))
                    _Customer.Email += ";";
                foreach (Contacts contact in _Customer.Contacts)
                {
                    if (!contact.Distribute || string.IsNullOrEmpty(contact.Email))
                        continue;
                    _Customer.Email += contact.Email + ";";
                }
            }
            _FuelOrder.Aircraft = Aircrafts.GetAircraft(_FuelOrder.AircraftID);
            _FuelOrder.Aircraft.MakeAndModel = AircraftData.GetAircraftData(_FuelOrder.Aircraft.MakeModelID);
            _VolumeDiscounts = new FuelOrderCustomerPricingsCollection();
            _VolumeDiscounts.LoadByFuelOrderId(_FuelOrder.Id);
            CreateAndSendEmail();
        }
        catch (Exception e)
        {
            ErrorLog EL = new ErrorLog(e);
            EL.UserID = _FuelOrder.CustClientID;
            EL.URL = "SendFuelOrderDispatchRequestForRelease, ID: " + _FuelOrder.Id.ToString();
            EL.LogError();
        }
    }

    private bool IsOKToSend()
    {
        if (Utilities.SiteMode() == SiteModes.Local)
            return false;
        return true;
    }

    private void CreateAndSendEmail()
    {
        using (SqlDataReader reader = GetReader())
        {
            EmailTemplate emailTemplate = GetEmailTemplate();
            while (reader.Read())
            {
                SetProperties(reader);
            }
            if (!string.IsNullOrEmpty(_Customer.Email))
                emailTemplate.EmailCC = _Customer.Email;
            emailTemplate.Content = Content;
            emailTemplate.SendEmail(this);
        }
    }

    private SqlDataReader GetReader()
    {
        List<SqlParameter> parameters = new List<SqlParameter>();
        parameters.Add(new SqlParameter("@AdminClientID", _FuelOrder.AdminClientID));
        parameters.Add(new SqlParameter("@Purpose", Purpose.RequestForRelease));
        return ExecutionHelper.ExecuteReader("up_Load_EmailDispatches", parameters);
    }

    private EmailTemplate GetEmailTemplate()
    {
        EmailTemplate emailTemplate = new EmailTemplate();

        EmailRoutingCollection emailsCollection = EmailRouting.GetEmailsByAdminClient(_FuelOrder.AdminClientID);
        EmailRouting emailRouting = new EmailRouting();
        foreach (EmailRouting emails in emailsCollection)
        {
            if (emails.EmailType == "Fuel Order")
            {
                emailRouting = emails;
                break;
            }
        }

        emailTemplate.EmailTo = emailRouting.ToEmail;
        emailTemplate.ReplacementValues.Add("%FROMEMAIL%", emailRouting.FromEmail);
        emailTemplate.ReplacementValues.Add("%COMPANY%", _Customer.Name);
        emailTemplate.ReplacementValues.Add("%ICAO%", _FuelOrder.ICAO);
        emailTemplate.ReplacementValues.Add("%CUSTOMEREMAIL%", _Customer.Email);
        emailTemplate.ReplacementValues.Add("%CUSTOMERPHONE%", _Customer.Phone);
        emailTemplate.ReplacementValues.Add("%VENDOR%", _Client.Name.Replace("Largent Fuel", "LARGENT Fuels"));
        emailTemplate.ReplacementValues.Add("%VENDOREMAIL%", emailRouting.DispatchContactEmail);
        emailTemplate.ReplacementValues.Add("%REQUESTEDUPLIFT%", Convert.ToInt32(_FuelOrder.QuotedVolume).ToString());
        emailTemplate.ReplacementValues.Add("%QUOTEDPRICE%", _FuelOrder.QuotedPPG.ToString("C"));
        emailTemplate.ReplacementValues.Add("%NOTES%", _FuelOrder.AdminNotes);
        emailTemplate.ReplacementValues.Add("%LISTINGNAME%", _FuelOrder.FBO);
        emailTemplate.ReplacementValues.Add("%FLIGHTTYPE%", _Customer.CertificateType);
        emailTemplate.ReplacementValues.Add("%FUELINGDATE%", _FuelOrder.FuelingDateString + " at " + _FuelOrder.FuelingTimeString);
        emailTemplate.ReplacementValues.Add("%TAILNUMBER%", _FuelOrder.Aircraft.TailNumber);
        emailTemplate.ReplacementValues.Add("%AIRCRAFTTYPE%", _FuelOrder.Aircraft.MakeAndModel.Make +  " " + _FuelOrder.Aircraft.MakeAndModel.Model);
        emailTemplate.ReplacementValues.Add("%ID%", _FuelOrder.Id.ToString());
        emailTemplate.ReplacementValues.Add("%VOLUMEDISCOUNTOPTIONS%", GetVolumeDiscounts());

        return emailTemplate;
    }

    private string GetVolumeDiscounts()
    {
        string volumeDiscounts = "";

        try
        {
            foreach (FuelOrderCustomerPricings price in _VolumeDiscounts)
            {
                if (price.Vendor.ToLower() == _FuelOrder.Vendor.ToLower() &&
                    price.FBOName.ToLower() == _FuelOrder.FBO.ToLower())
                    volumeDiscounts += "<div>" + price.PriceTierMin + "+: " + price.Price.ToString("C") + " at " +
                                       price.FBOName + " " + price.Product + "</div>";
            }
        }
        catch (System.Exception exception)
        {
            //Do nothing
        }

        return volumeDiscounts;
    }
    #endregion
}