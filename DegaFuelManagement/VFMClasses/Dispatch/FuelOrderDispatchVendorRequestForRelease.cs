using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Degatech.Utilities.Data;
using Degatech.Utilities.Exceptions;
using Degatech.Utilities.SQL;
using VFMClasses;

public class FuelOrderDispatchVendorRequestForRelease : EmailDispatches
{
    #region Members

    private FuelOrders _FuelOrder;
    private CustomerDetails _Customer;
    private Clients _Client;
    private string _SupplierEmail;

    #endregion

    #region Constructors

    public FuelOrderDispatchVendorRequestForRelease()
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
        _Customer = new CustomerDetails(_FuelOrder.CustClientID);

        GetSupplierEmail();
        if (_SupplierEmail != "" && !_Customer.IsDemoMode)
            SendRequestForRelease();
    }

    #endregion

    #region Private Methods

    private void GetSupplierEmail()
    {
        List<SqlParameter> parameters = new List<SqlParameter>();
        parameters.Add(new SqlParameter("@AdminClientID", _FuelOrder.AdminClientID));
        parameters.Add(new SqlParameter("@SupplierID", _FuelOrder.SupplierID));
        using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Load_SupplierEmail", parameters))
        {
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    _SupplierEmail = reader["SupplierEmail"].ToString();
                }
            }
        }

        if (_SupplierEmail == "")
        {
            parameters.Clear();
            parameters.Add(new SqlParameter("@AdminClientID", _FuelOrder.AdminClientID));
            parameters.Add(new SqlParameter("@SupplierID", _FuelOrder.SupplierID));
            parameters.Add(new SqlParameter("@ICAO", _FuelOrder.ICAO));
            parameters.Add(new SqlParameter("@FBO", _FuelOrder.FBO));
            parameters.Add(new SqlParameter("@Product", _FuelOrder.Product));
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Load_SupplierEmailFromSupplierFuelsPrices", parameters))
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        _SupplierEmail = reader["VendorEmail"].ToString();
                    }
                }
            }
        }
    }

    private void SendRequestForRelease()
    {
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
            CreateAndSendEmail();
        }
        catch (Exception e)
        {
            ErrorLog EL = new ErrorLog(e);
            EL.UserID = _FuelOrder.CustClientID;
            EL.URL = "SendFuelOrderVendorDispatchRequestForRelease, ID: " + _FuelOrder.Id.ToString();
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
            if (reader.HasRows)
            {
                EmailTemplate emailTemplate = GetEmailTemplate();
                while (reader.Read())
                {
                    SetProperties(reader);
                }
                emailTemplate.Content = Content;
                emailTemplate.SendEmail(this);
            }
        }
    }

    private SqlDataReader GetReader()
    {
        List<SqlParameter> parameters = new List<SqlParameter>();
        parameters.Add(new SqlParameter("@AdminClientID", _FuelOrder.AdminClientID));
        parameters.Add(new SqlParameter("@Purpose", Purpose.VendorRequestForRelease));
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

        emailTemplate.EmailTo = _SupplierEmail;
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
        emailTemplate.ReplacementValues.Add("%AIRCRAFTTYPE%", _FuelOrder.Aircraft.MakeAndModel.Make + " " + _FuelOrder.Aircraft.MakeAndModel.Model);
        emailTemplate.ReplacementValues.Add("%ID%", _FuelOrder.Id.ToString());

        return emailTemplate;
    }
    #endregion
}
