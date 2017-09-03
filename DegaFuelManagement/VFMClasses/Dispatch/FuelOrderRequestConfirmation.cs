using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Degatech.Utilities.Data;
using Degatech.Utilities.Exceptions;
using Degatech.Utilities.SQL;
using VFMClasses;

public class FuelOrderRequestConfirmation : EmailDispatches
{
    #region Members

    private FuelOrders _FuelOrder;
    private CustomerDetails _Customer;
   
    #endregion

    #region Constructors

    public FuelOrderRequestConfirmation()
    {

    }
    #endregion


    #region Public Methods

    public void SendFuelRequestConfirmation(int fuelOrderId)
    {
        _FuelOrder = new FuelOrders(fuelOrderId);
        _Customer = new CustomerDetails(_FuelOrder.CustClientID);

        try
        {
            _Customer.Contacts = new ContactsCollection();
            _Customer.Contacts.LoadByCustClientID(_Customer.CustClientID);
            _Customer.Email = "";
            foreach (Contacts contact in _Customer.Contacts)
            {
                if (contact.Distribute)
                    _Customer.Email += contact.Email + ";";
            }
            _FuelOrder.Aircraft = Aircrafts.GetAircraft(_FuelOrder.AircraftID);
            _FuelOrder.Aircraft.MakeAndModel = AircraftData.GetAircraftData(_FuelOrder.Aircraft.MakeModelID);
            CreateAndSendEmail();
            _FuelOrder.AdminStatus = AdminStatuses.Confirmed;
            _FuelOrder.Update();
        }
        catch (Exception e)
        {
            ErrorLog EL = new ErrorLog(e);
            EL.UserID = _FuelOrder.CustClientID;
            EL.URL = "SendFuelRequestConfirmation, ID: " + _FuelOrder.Id.ToString();
            EL.LogError();
        }
    }

    #endregion

    #region Private Methods

    private void CreateAndSendEmail()
    {
        using (SqlDataReader reader = GetReader())
        {
            EmailTemplate emailTemplate = GetEmailTemplate();
            while (reader.Read())
            {
                SetProperties(reader);
            }
            emailTemplate.Content = WebUtility.HtmlDecode(Content);
            emailTemplate.SendEmail(this);
        }
    }

    private SqlDataReader GetReader()
    {
        List<SqlParameter> parameters = new List<SqlParameter>();
        parameters.Add(new SqlParameter("@AdminClientID", _FuelOrder.AdminClientID));
        parameters.Add(new SqlParameter("@Purpose", Purpose.FuelReleaseConfirmation));
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

        emailTemplate.EmailTo = _Customer.Email;
        emailTemplate.ReplacementValues.Add("%FROMEMAIL%", emailRouting.FromEmail);
        emailTemplate.ReplacementValues.Add("%CUSTOMEREMAIL%", _Customer.Email);
        emailTemplate.ReplacementValues.Add("%COMPANY%", _Customer.Name);
        emailTemplate.ReplacementValues.Add("%TAILNUMBER%", _FuelOrder.Aircraft.TailNumber);
        emailTemplate.ReplacementValues.Add("%CURRENTDATE%",DateTime.Now.ToString("f"));
        emailTemplate.ReplacementValues.Add("%TRIPNUMBER%", _FuelOrder.TripID.ToString());
        emailTemplate.ReplacementValues.Add("%ICAO%", _FuelOrder.ICAO);
        emailTemplate.ReplacementValues.Add("%LISTINGNAME%", _FuelOrder.FBO);
        emailTemplate.ReplacementValues.Add("%REQUESTEDUPLIFT%", (Convert.ToInt32(_FuelOrder.QuotedVolume) == 1 ? "1+" : Convert.ToInt32(_FuelOrder.QuotedVolume).ToString()));
        emailTemplate.ReplacementValues.Add("%QUOTEDPRICE%", _FuelOrder.QuotedPPG.ToString("C"));
        emailTemplate.ReplacementValues.Add("%RAMPFEE%", _FuelOrder.RampFee.ToString("C"));
        emailTemplate.ReplacementValues.Add("%RAMPFEEWAIVED%", _FuelOrder.RampFeeWaivedAt.ToString());
        emailTemplate.ReplacementValues.Add("%NOTES%", _FuelOrder.AdminNotes);
        emailTemplate.ReplacementValues.Add("%ID%", _FuelOrder.Id.ToString());
        emailTemplate.ReplacementValues.Add("%FUELINGDATE%", _FuelOrder.FuelingDateString + " at " + _FuelOrder.FuelingTimeString);
        emailTemplate.ReplacementValues.Add("%PRODUCT%", _FuelOrder.Product);
        emailTemplate.ReplacementValues.Add("%AIRCRAFTTYPE%", _FuelOrder.Aircraft.MakeAndModel.Make + " " + _FuelOrder.Aircraft.MakeAndModel.Model);

        return emailTemplate;
    }
    #endregion
}