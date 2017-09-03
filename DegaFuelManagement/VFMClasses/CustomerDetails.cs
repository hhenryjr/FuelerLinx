using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Degatech.Common;
using Degatech.Utilities.SQL;
using System.Linq;
using System.Collections;
using VFM.EDM;
using VFM.API.EntityServices;

namespace VFMClasses
{
    #region CustomerDetail
    /// <summary>
    /// This object represents the properties and methods of a CustomerDetail.
    /// </summary>
    public class CustomerDetails : BaseClass
    {
        #region Properties
        public int Id { get; set; }
        public int AdminClientID { get; set; }
        public int CustClientID { get; set; }
        public string Name { get; set; } = String.Empty;
        public string Phone { get; set; } = String.Empty;
        public string Email { get; set; } = String.Empty;
        public string City { get; set; } = String.Empty;
        public string Country { get; set; } = String.Empty;
        public string BaseICAO { get; set; } = String.Empty;
        public bool IsActive { get; set; }
        public string Note { get; set; } = String.Empty;
        public DateTime DateAdded { get; set; }
        public string ParentName { get; set; }
        public string CertificateType { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public ContactsCollection Contacts { get; set; }
        public CustomerNotesCollection CustomerNotes { get; set; }
        public Users User { get; set; }
        public Registration Registration { get; set; }
        public AircraftsCollection Aircrafts { get; set; }
        public PriceMargins PriceMargin { get; set; }
        public CustomerPriceMargins CustomerPriceMargin { get; set; }
        public PriceMarginTiers PriceMarginTier { get; set; }
        public CustomerAccountingInfo CustomerAccountingInfo { get; set; }
        public CustomerDetailsCustomFieldsCollection CustomFields { get; set; }
        public PartnerServiceIntegration Integration { get; set; }
        public string PrimaryContact { get; set; }
        public int CustomerPriceMarginID { get; set; }
        public int MarginSetting { get; set; }
        public double MarginResult { get; set; }
        public int FleetSize { get; set; }
        public bool IsDemoMode { get; set; } = false;
        #endregion

        #region Constructors
        public CustomerDetails()
        {
        }

        public CustomerDetails(int id)
        {
            //Id = id;
            CustClientID = id;
            Load();
        }

        public CustomerDetails(SqlDataReader reader)
        {
            LoadFromReader(reader);
        }
        #endregion

        #region Public Methods
        public void Load()
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            //parameters.Add(new SqlParameter("@Id", Id));
            //using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Select_CustomerDetail", parameters))
            parameters.Add(new SqlParameter("@Id", CustClientID));
            if (Users.CurrentUser.Client.ClientType == ClientTypes.Admin)
                parameters.Add(new SqlParameter("@AdminClientId", Users.CurrentUser.ClientID));
            else
                parameters.Add(new SqlParameter("@Domain", Degatech.Utilities.Data.Utilities.Domain()));
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Select_CustomerDetail", parameters))
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
            DeleteCustomerDetail(Id);
        }

        public void Update()
        {
            ArrayList propertiesToOmit = new ArrayList();
            propertiesToOmit.Add("Contacts");
            propertiesToOmit.Add("CustomerNotes");
            propertiesToOmit.Add("Users");
            propertiesToOmit.Add("Aircrafts");
            propertiesToOmit.Add("PriceMargin");
            propertiesToOmit.Add("CustomerPriceMargin");
            propertiesToOmit.Add("PriceMarginTier");
            propertiesToOmit.Add("PrimaryContact");
            propertiesToOmit.Add("CustomerPriceMarginID");
            propertiesToOmit.Add("MarginSetting");
            propertiesToOmit.Add("MarginResult");
            propertiesToOmit.Add("FleetSize");
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_InsertUpdate_CustomerDetail", GetSQLParameters(propertiesToOmit)))
            {
                if (reader == null)
                    return;
                if (reader.Read())
                    SetProperties(reader);
            }
        }
        #endregion

        #region Static Methods
        public static CustomerDetails GetCustomerDetail(int id)
        {
            return new CustomerDetails(id);
        }

        public static CustomerDetailsCollection LoadList()
        {
            CustomerDetailsCollection collection = new CustomerDetailsCollection();
            collection.LoadAll();
            return collection;
        }

        public static CustomerDetailsCollection LoadListByAdminClient(int clientId)
        {
            CustomerDetailsCollection collection = new CustomerDetailsCollection();
            collection.LoadByAdminClientID(clientId);
            return collection;
        }

        public static CustomerDetailsCollection DeleteCustomerDetail(int id)
        {
            CustomerDetailsCollection collection = new CustomerDetailsCollection();
            collection.DeleteCustomerDetail(id);
            return collection;
        }

        #endregion
    }

    #region Collection
    public class CustomerDetailsCollection : List<CustomerDetails>
    {
        #region Public Methods
        public void LoadAll()
        {
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Select_CustomerDetailsAll"))
            {
                if (reader == null)
                    return;
                while (reader.Read())
                {
                    CustomerDetails detail = new CustomerDetails();
                    detail.SetProperties(reader);
                    Add(detail);
                }
            }
        }

        public void LoadByAdminClientID(int clientId)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@AdminClientID", clientId));
            using (
                SqlDataReader reader =
                    ExecutionHelper.ExecuteReader("up_Select_CustomerDetailsByAndAdminClientID", parameters))
            {
                if (reader == null)
                    return;
                while (reader.Read())
                {
                    CustomerDetails detail = new CustomerDetails();
                    detail.SetProperties(reader);
                    //detail.Aircrafts = new AircraftsCollection();
                    //detail.Aircrafts.LoadByCustClientID(detail.CustClientID);
                    detail.CustomerPriceMargin = new CustomerPriceMargins();
                    detail.CustomerPriceMargin.SetProperties(reader);
                    detail.PriceMargin = new PriceMargins();
                    detail.PriceMargin.SetProperties(reader);
                    detail.PriceMarginTier = new PriceMarginTiers();
                    detail.PriceMarginTier.SetProperties(reader);
                    Add(detail);
                }
            }
        }

        public static DataTable GetCustomersByAdminClientID(int clientId)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@AdminClientID", clientId));
            DataSet resultSet = ExecutionHelper.ExecuteDataset("up_Select_CustomerDetailsByAndAdminClientID", parameters);
            if (resultSet.Tables.Count > 0)
                return resultSet.Tables[0];
            return null;
        }

        public void LoadByCustClientID(int clientId)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@CustClientID", clientId));
            using (
                SqlDataReader reader =
                    ExecutionHelper.ExecuteReader("up_Select_CustomerDetailsByAndCustClientID", parameters))
            {
                if (reader == null)
                    return;
                while (reader.Read())
                {
                    CustomerDetails detail = new CustomerDetails();
                    detail.SetProperties(reader);
                    Add(detail);
                }
            }
        }

        public void DeleteCustomerDetail(int id)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@CustClientID", id));
            ExecutionHelper.ExecuteNonQuery("up_Delete_CustomerDetail", parameters);
        }

        public CustomerDetails GetId(int id)
        {
            return this.FirstOrDefault(detail => detail.Id == id);
        }

        public CustomerDetails GetAdminClientID(int clientId)
        {
            return this.FirstOrDefault(detail => detail.AdminClientID == clientId);
        }

        public CustomerDetails GetCustClientID(int clientId)
        {
            return this.FirstOrDefault(detail => detail.CustClientID == clientId);
        }

        public CustomerDetails GetName(string name)
        {
            return this.FirstOrDefault(detail => detail.Name == name);
        }

        public CustomerDetails GetPhone(string phone)
        {
            return this.FirstOrDefault(detail => detail.Phone == phone);
        }

        public CustomerDetails GetEmail(string email)
        {
            return this.FirstOrDefault(detail => detail.Email == email);
        }

        public CustomerDetails GetCity(string city)
        {
            return this.FirstOrDefault(detail => detail.City == city);
        }

        public CustomerDetails GetCountry(string country)
        {
            return this.FirstOrDefault(detail => detail.Country == country);
        }

        public CustomerDetails GetBaseICAO(string icao)
        {
            return this.FirstOrDefault(detail => detail.BaseICAO == icao);
        }

        public CustomerDetails GetIsActive(bool isActive)
        {
            return this.FirstOrDefault(detail => detail.IsActive == isActive);
        }

        public CustomerDetails GetDateAdded(DateTime date)
        {
            return this.FirstOrDefault(detail => detail.DateAdded == date);
        }

        public CustomerDetails PrimaryContact(string id)
        {
            return this.FirstOrDefault(detail => detail.PrimaryContact == id);
        }

        public CustomerDetails CustomerPriceMarginID(int id)
        {
            return this.FirstOrDefault(detail => detail.CustomerPriceMarginID == id);
        }

        public CustomerDetails MarginSetting(int id)
        {
            return this.FirstOrDefault(detail => detail.MarginSetting == id);
        }

        public CustomerDetails MarginResult(int id)
        {
            return this.FirstOrDefault(detail => detail.MarginResult == id);
        }

        public CustomerDetails FleetSize(int id)
        {
            return this.FirstOrDefault(detail => detail.FleetSize == id);
        }
        #endregion
    }
    #endregion

    #endregion
}


