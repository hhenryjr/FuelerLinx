using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Degatech.Common;
using Degatech.Utilities.Exceptions;
using Degatech.Utilities.SQL;

namespace VFMClasses
{
    #region FuelOrders
    /// <summary>
    /// This object represents the properties and methods of a FuelOrder.
    /// </summary>
    public class FuelOrders : BaseClass
    {
        #region Enum
        public enum InvoiceStatuses
        {
            NotSpecified = 0,
            New = 1,
            Modify = 2,
            PostVerify = 3,
            MarginAnalysis = 4,
            Discrepancy = 5,
            Accounting = 6,
            Pending = 7,
            Reconciled = 8,
            Cancelled = 9,
            NoFuel = 10
        }
        #endregion

        #region Properties
        public int Id { get; set; }
        public int AdminClientID { get; set; }
        public int CustClientID { get; set; }
        public int OrderedByUserID { get; set; }
        public string ICAO { get; set; } = String.Empty;
        public string FBO { get; set; } = String.Empty;
        public int AircraftID { get; set; }
        public string TailNumber { get; set; } = String.Empty;
        public DateTime DateRequested { get; set; }
        public AdminStatuses AdminStatus { get; set; }
        public CustStatuses CustStatus { get; set; }
        public bool IsArchived { get; set; }
        public bool IsDirectlyEntered { get; set; }
        public decimal QuotedPPG { get; set; }
        public decimal InvoicedPPG { get; set; }
        public decimal QuotedVolume { get; set; }
        public decimal InvoicedVolume { get; set; }
        public DateTime FuelingDateTime { get; set; }
        public DateTime DateAdded { get; set; }
        //public FuelOrderNotesCollection FuelOrderNotes { get; set; }
        public Aircrafts Aircraft { get; set; }
        public Clients Client { get; set; }
        public FuelOrderFeesCollection FuelOrderFees { get; set; } = new FuelOrderFeesCollection();
        public FuelOrderTaxesCollection FuelOrderTaxes { get; set; } = new FuelOrderTaxesCollection();
        public string FBOPhone { get; set; } = String.Empty;
        public double WholesaleQuoted { get; set; }
        public double WholesaleInvoiced { get; set; }
        public string InvoiceNumber { get; set; } = String.Empty;
        public double BasePPG { get; set; }
        public bool NoFuel { get; set; }
        public int TripID { get; set; }
        public int LegNumber { get; set; }
        public string CertificateType { get; set; } = String.Empty;
        public string FuelOn { get; set; } = String.Empty;
        public double PostedRetail { get; set; }
        public double RampFee { get; set; }
        public double RampFeeWaivedAt { get; set; }
        public string RequestedBy { get; set; } = String.Empty;
        public InvoiceStatuses InvoiceStatus { get; set; }
        public int SupplierID { get; set; }
        public double PlattsPrice { get; set; }
        public string AdminNotes { get; set; } = String.Empty;
        public string CustNotes { get; set; } = String.Empty;
        public string Vendor { get; set; } = String.Empty;
        public string Product { get; set; } = String.Empty;
        public bool HasAttachments { get; set; }
        public double PPG { get; set; }
        public decimal Total { get; set; }
        public string InvoiceStatusLabel {
            get {
                return Enum.GetName(typeof(InvoiceStatuses), InvoiceStatus);
            } }

        public bool IsFromFuelerLinx { get; set; } = false;
        #endregion

        #region Readonly Properties

        public string FuelingDateString
        {
            get { return FuelingDateTime.ToString("MM/dd/yy"); }
        }

        public string FuelingTimeString
        {
            get { return FuelingDateTime.ToString("HH:mm"); }
        }
        #endregion

        #region Constructors
        public FuelOrders()
        {
        }

        public FuelOrders(int id)
        {
            Id = id;
            Load();
            FuelOrderFees.LoadAllFees(id);
            FuelOrderTaxes.LoadAllTaxes(id);
            FuelOrderFees.LoadClientFees(CustClientID);
            FuelOrderTaxes.LoadClientTaxes(CustClientID);
        }

        public FuelOrders(SqlDataReader reader)
        {
            LoadFromReader(reader);
        }
        #endregion

        #region Public Methods
        public void Load()
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Id", Id));
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Select_FuelOrder", parameters))
            {
                if (reader == null)
                    return;
                if (reader.Read())                
                    SetProperties(reader);                
            }
        }

        protected void LoadFromReader(SqlDataReader reader)
        {
            SetProperties(reader);
        }

        public void Delete()
        {
            DeleteFuelOrder(Id);
        }

        public void Update()
        {
            ArrayList propertiesToOmit = new ArrayList();
            propertiesToOmit.Add("FBOPhone");
            propertiesToOmit.Add("HasAttachments");
            propertiesToOmit.Add("PPG");
            propertiesToOmit.Add("DateAdded");
            propertiesToOmit.Add("IsFromFuelerLinx");
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_InsertUpdate_FuelOrder", GetSQLParameters(propertiesToOmit)))
            {
                if (reader == null)
                    return;
                if (reader.Read())
                    SetProperties(reader);
            }

            if (!IsFromFuelerLinx)
                SyncDataWithFuelerLinx();
        }

        public void Dispatch()
        {
            //FuelDispatch.
        }
        #endregion

        #region Private Methods

        private void SyncDataWithFuelerLinx()
        {
            try
            {
                FuelerLinxDegaIntegration.DegaIntegration fuelerlinxIntegration =
                    new FuelerLinxDegaIntegration.DegaIntegration();
                SiteSettings settings = new SiteSettings();
                fuelerlinxIntegration.Url = settings.FuelerLinxServiceURL;
                FuelerLinxDegaIntegration.DegaObject fuelerLinxTransaction = fuelerlinxIntegration.GetTransaction(Id);
                fuelerLinxTransaction.ActualPPG = double.Parse(InvoicedPPG.ToString());
                fuelerLinxTransaction.ActualVolume = double.Parse(InvoicedVolume.ToString());
                fuelerLinxTransaction.basePPG = BasePPG;
                fuelerLinxTransaction.TripID = TripID;
                fuelerLinxTransaction.tail_number = TailNumber;
                fuelerLinxTransaction.TransactionDetails.FlightType = CertificateType;
                fuelerLinxTransaction.TransactionDetails.FuelOn = FuelOn;
                if (FuelOn == "Departure")
                    fuelerLinxTransaction.etd = FuelingDateTime;
                else
                    fuelerLinxTransaction.eta = FuelingDateTime;
                fuelerLinxTransaction.postedRetail = PostedRetail;
                fuelerLinxTransaction.PlattsPrice = PlattsPrice;
                fuelerLinxTransaction.fbo_notes = AdminNotes;
                fuelerLinxTransaction.ReqFees.RampFee = RampFee;
                fuelerLinxTransaction.ReqFees.RampFeeWaivedAt = RampFeeWaivedAt;
                fuelerLinxTransaction.ReqFees.FuelerInvoiceNumber = InvoiceNumber;
                if (InvoiceStatus == InvoiceStatuses.Reconciled)
                    fuelerLinxTransaction.reconciled = true;
                else if (InvoiceStatus == InvoiceStatuses.Discrepancy)
                    fuelerLinxTransaction.discrepancy = true;
                else if (InvoiceStatus == InvoiceStatuses.Cancelled)
                    fuelerLinxTransaction.cancelled = true;
                fuelerLinxTransaction.TransactionDetails.NoFuel = NoFuel;
                fuelerlinxIntegration.UpdateTransaction(fuelerLinxTransaction);
            }
            catch (System.Exception exception)
            {
                ErrorLog.LogAndEmailError(exception, "SyncDataWithFuelerLinx", Users.CurrentUser.Id);
            }
        }
        #endregion

        #region Static Methods
        public static FuelOrdersCollection LoadList()
        {
            FuelOrdersCollection collection = new FuelOrdersCollection();
            collection.LoadAll();
            return collection;
        }

        public static FuelOrders GetFuelOrder(int id)
        {
            return new FuelOrders(id);
        }

        public static FuelOrdersCollection LoadListByAdminClient(int clientId, DateTime startDate, DateTime endDate)
        {
            FuelOrdersCollection collection = new FuelOrdersCollection();
            collection.LoadByAdminClientID(clientId, startDate, endDate);
            return collection;
        }

        public static FuelOrdersCollection LoadListByCustClient(int clientId, DateTime startDate, DateTime endDate)
        {
            FuelOrdersCollection collection = new FuelOrdersCollection();
            collection.LoadByCustClientID(clientId, startDate, endDate);
            return collection;
        }

        public static FuelOrdersCollection DeleteFuelOrder(int id)
        {
            FuelOrdersCollection collection = new FuelOrdersCollection();
            collection.Delete(id);
            return collection;
        }
        #endregion

    }
    #endregion

    #region Collection
    public class FuelOrdersCollection : List<FuelOrders>
    {
        public void LoadAll()
        {
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Select_FuelOrdersAll"))
            {
                if (reader == null)
                    return;
                while (reader.Read())
                {
                    FuelOrders fuelOrder = new FuelOrders();
                    fuelOrder.SetProperties(reader);
                    fuelOrder.Aircraft = new Aircrafts();
                    fuelOrder.Aircraft.SetProperties(reader);
                    Add(fuelOrder);
                }
            }
        }

        public void LoadFutureOrders(int adminClientID)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@OrderedByUserID", adminClientID));
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Select_FutureFuelOrders", parameters))
            {
                if (reader == null)
                    return;
                while (reader.Read())
                {
                    FuelOrders fuelOrder = new FuelOrders();
                    fuelOrder.SetProperties(reader);
                    fuelOrder.Aircraft = new Aircrafts();
                    fuelOrder.Aircraft.SetProperties(reader);
                    Add(fuelOrder);
                }
            }
        }

        public void LoadByUserID(int userId)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@OrderedByUserID", userId));
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Select_FuelOrdersByAndOrderedByUserID", parameters))
            {
                if (reader == null)
                    return;
                while (reader.Read())
                {
                    FuelOrders fuelOrder = new FuelOrders();
                    fuelOrder.SetProperties(reader);
                    fuelOrder.Aircraft = new Aircrafts();
                    fuelOrder.Aircraft.SetProperties(reader);
                    Add(fuelOrder);
                }
            }
        }

        public void LoadByAdminClientID(int adminClientId)
        {
            LoadByAdminClientID(adminClientId, BaseClass.DatabaseDateTimeMinValue(), BaseClass.DatabaseDateTimeMinValue());
        }

        public void LoadByAdminClientID(int adminClientId, DateTime startDate, DateTime endDate)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@AdminClientID", adminClientId));
            parameters.Add(new SqlParameter("@StartDate", startDate));
            parameters.Add(new SqlParameter("@EndDate", endDate));

            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Select_FuelOrdersByAndAdminClientID", parameters))
            {
                if (reader == null)
                    return;
                while (reader.Read())
                {
                    FuelOrders fuelOrder = new FuelOrders();
                    fuelOrder.SetProperties(reader);
                    fuelOrder.Aircraft = new Aircrafts();
                    fuelOrder.Aircraft.SetProperties(reader);
                    fuelOrder.Client = new Clients();
                    fuelOrder.Client.SetProperties(reader);
                    Add(fuelOrder);
                }
            }
        }

        public void LoadByCustClientID(int custClientId)
        {
            LoadByCustClientID(custClientId, BaseClass.DatabaseDateTimeMinValue(), BaseClass.DatabaseDateTimeMinValue());
        }

        public void LoadByCustClientID(int custClientId, DateTime startDate, DateTime endDate)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@CustClientID", custClientId));
            parameters.Add(new SqlParameter("@StartDate", startDate));
            parameters.Add(new SqlParameter("@EndDate", endDate));
            parameters.Add(new SqlParameter("@Domain", Degatech.Utilities.Data.Utilities.Domain()));
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Select_FuelOrdersByAndCustClientID", parameters))
            {
                if (reader == null)
                    return;
                while (reader.Read())
                {
                    FuelOrders fuelOrder = new FuelOrders();
                    fuelOrder.SetProperties(reader);
                    fuelOrder.Aircraft = new Aircrafts();
                    fuelOrder.Aircraft.SetProperties(reader);
                    Add(fuelOrder);
                }
            }
        }

        public void LoadByClient(Clients client)
        {
            if (client.ClientType == BaseClass.ClientTypes.Admin)
            {
                LoadByAdminClientID(client.Id);
            }
            else
            {
                LoadByCustClientID(client.Id);
            }
        }

        public int GetHighestTransactionID(int clientID, string clientType)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ClientID", clientID));
            if (clientType != "")
                parameters.Add(new SqlParameter("@Domain", Degatech.Utilities.Data.Utilities.Domain()));
            return int.Parse(ExecutionHelper.ExecuteScalar("up_Select_HighestTransactionID", parameters).ToString());
        }

        public void Delete(int id)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Id", id));
            ExecutionHelper.ExecuteNonQuery("up_Delete_FuelOrder", parameters);
        }

        public FuelOrders GetId(int id)
        {
            return this.FirstOrDefault(fuelOrder => fuelOrder.Id == id);
        }

        public FuelOrders GetAdminClientID(int adminId)
        {
            return this.FirstOrDefault(fuelOrder => fuelOrder.AdminClientID == adminId);
        }

        public FuelOrders GetCustClientID(int custId)
        {
            return this.FirstOrDefault(fuelOrder => fuelOrder.CustClientID == custId);
        }

        public FuelOrders GetUserID(int userId)
        {
            return this.FirstOrDefault(fuelOrder => fuelOrder.OrderedByUserID == userId);
        }

        public FuelOrders GetICAO(string icao)
        {
            return this.FirstOrDefault(fuelOrder => fuelOrder.ICAO == icao);
        }

        public FuelOrders GetFBO(string fbo)
        {
            return this.FirstOrDefault(fuelOrder => fuelOrder.FBO == fbo);
        }

        public FuelOrders GetAircraft(int aircraft)
        {
            return this.FirstOrDefault(fuelOrder => fuelOrder.AircraftID == aircraft);
        }

        public FuelOrders GetDateRequested(DateTime dateRequested)
        {
            return this.FirstOrDefault(fuelOrder => fuelOrder.DateRequested == dateRequested);
        }

        public FuelOrders GetAdminStatus(BaseClass.AdminStatuses adminStatus)
        {
            return this.FirstOrDefault(fuelOrder => fuelOrder.AdminStatus == adminStatus);
        }

        public FuelOrders GetCustStatus(BaseClass.CustStatuses custStatus)
        {
            return this.FirstOrDefault(fuelOrder => fuelOrder.CustStatus == custStatus);
        }

        public FuelOrders GetIsArchived(bool isArchived)
        {
            return this.FirstOrDefault(fuelOrder => fuelOrder.IsArchived == isArchived);
        }

        public FuelOrders GetIsDirectlyEntered(bool isDirectlyEntered)
        {
            return this.FirstOrDefault(fuelOrder => fuelOrder.IsDirectlyEntered == isDirectlyEntered);
        }

        public FuelOrders GetQuotedPPG(decimal quotedPpg)
        {
            return this.FirstOrDefault(fuelOrder => fuelOrder.QuotedPPG == quotedPpg);
        }

        public FuelOrders GetInvoicedPPG(decimal invoicedPpg)
        {
            return this.FirstOrDefault(fuelOrder => fuelOrder.InvoicedPPG == invoicedPpg);
        }

        public FuelOrders GetQuotedVolume(decimal quotedVolume)
        {
            return this.FirstOrDefault(fuelOrder => fuelOrder.QuotedVolume == quotedVolume);
        }

        public FuelOrders GetInvoicedVolume(decimal invoicedVolume)
        {
            return this.FirstOrDefault(fuelOrder => fuelOrder.InvoicedVolume == invoicedVolume);
        }

        public FuelOrders GetFuelingDateTime(DateTime fuelPricingDate)
        {
            return this.FirstOrDefault(fuelOrder => fuelOrder.FuelingDateTime == fuelPricingDate);
        }

        public FuelOrders GetDateAdded(DateTime dateAdded)
        {
            return this.FirstOrDefault(fuelOrder => fuelOrder.DateAdded == dateAdded);
        }
    }
    #endregion

}


