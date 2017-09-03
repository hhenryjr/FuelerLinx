using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Degatech.Common;
using Degatech.Utilities.SQL;
using System.Linq;
using System.Collections;

namespace VFMClasses
{
    #region CustomerPriceMargins
    /// <summary>
    /// This object represents the properties and methods of a CustomerPriceMargins.
    /// </summary>
    public class CustomerPriceMargins : BaseClass
    {
        #region Properties
        public int Id { get; set; }
        public int CustClientID { get; set; }
        public int PriceMarginID { get; set; }
        public SupplierFuelsPrices SupplierPrices { get; set; }
        public PriceMarginTiers PriceMarginTiers { get; set; }
        public decimal Price { get; set; }
        public float HighestMargin { get; set; }
        public string TailNumber { get; set; } = string.Empty;
        #endregion

        #region Constructors
        public CustomerPriceMargins()
        {
        }

        public CustomerPriceMargins(int id)
        {
            Id = id;
            Load();
        }

        public CustomerPriceMargins(SqlDataReader reader)
        {
            LoadFromReader(reader);
        }
        #endregion

        #region Public Methods
        public void Load()
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Id", Id));
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Select_CustomerPriceMargin", parameters))
            {
                if (reader == null)
                    return;
                if (reader.Read())
                    SetProperties(reader);
            }
        }

        public void LoadByCustClientID(int custClientID)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@CustClientID", custClientID));
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Select_CustomerPriceMarginsByAndCustClientID", parameters))
            {
                if (reader == null)
                    return;
                if (reader.Read())
                    SetProperties(reader);
            }
        }

        public void UpdatePriceMarginAndLoadHighestMargin(int id, int custClientID, int priceMarginID)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Id", id));
            parameters.Add(new SqlParameter("@CustClientID", custClientID));
            parameters.Add(new SqlParameter("@PriceMarginID", priceMarginID));
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Update_CustomerPriceMargin_Select_HighestMarginByPriceMarginID", parameters))
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
            Delete(Id);
        }

        public void Update()
        {
            ArrayList propertiesToOmit = new ArrayList();
            propertiesToOmit.Add("SupplierPrices");
            propertiesToOmit.Add("PriceMarginTiers");
            propertiesToOmit.Add("Price");
            propertiesToOmit.Add("HighestMargin");
            propertiesToOmit.Add("TailNumber");
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_InsertUpdate_CustomerPriceMargin", GetSQLParameters(propertiesToOmit)))
            {
                if (reader == null)
                    return;
                if (reader.Read())
                    SetProperties(reader);
            }
        }
        #endregion

        #region Static Methods
        public static CustomerPriceMarginsCollection LoadList()
        {
            CustomerPriceMarginsCollection collection = new CustomerPriceMarginsCollection();
            collection.LoadAll();
            return collection;
        }

        public static CustomerPriceMargins GetCustomerPriceMargin(int id)
        {
            return new CustomerPriceMargins(id);
        }

        public static CustomerPriceMargins GetCustomerPriceMarginByCustClientID(int custClientID)
        {
            CustomerPriceMargins margin = new CustomerPriceMargins();
            margin.LoadByCustClientID(custClientID);
            return margin;
        }

        //public static CustomerPriceMarginsCollection GetCustomerPriceMarginByCustClientIDAndICAO(int customerClientId, string icao)
        //{
        //    return GetCustomerPriceMarginByCustClientIDAndICAO(customerClientId, 0, icao);
        //}

        public static CustomerPriceMarginsCollection GetCustomerPriceMarginByCustClientIDAndICAO(int custClientId, int adminClientId, string icao)
        {
            CustomerPriceMarginsCollection collection = new CustomerPriceMarginsCollection();
            collection.LoadByCustClientIDAndICAO(custClientId, adminClientId, icao);
            return collection;
        }

        public static CustomerPriceMarginsCollection GetCustomerPriceMarginForAllVendors(int customerClientId, int adminClientId, string icao)
        {
            CustomerPriceMarginsCollection collection = new CustomerPriceMarginsCollection();
            collection.LoadByCustClientIDForAllVendors(customerClientId, adminClientId, icao);
            return collection;
        }

        public static CustomerPriceMargins UpdateMarginAndGetHighestMargin(int id, int custClientId, int priceMarginId)
        {
            CustomerPriceMargins margin = new CustomerPriceMargins();
            margin.UpdatePriceMarginAndLoadHighestMargin(id, custClientId, priceMarginId);
            return margin;
        }

        public static CustomerPriceMarginsCollection Delete(int id)
        {
            CustomerPriceMarginsCollection collection = new CustomerPriceMarginsCollection();
            collection.Delete(id);
            return collection;
        }
        #endregion
    }
    #endregion

    #region Collection
    public class CustomerPriceMarginsCollection : List<CustomerPriceMargins>
    {
        #region Public Methods
        public void LoadAll()
        {
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Select_CustomerPriceMarginsAll"))
            {
                if (reader == null)
                    return;
                while (reader.Read())
                {
                    CustomerPriceMargins margin = new CustomerPriceMargins();
                    margin.SetProperties(reader);
                    Add(margin);
                }
            }
        }

        public void LoadByCustClientID(int custClientID)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@CustClientID", custClientID));
            using (
                SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Select_CustomerPriceMarginsByAndCustClientID",
                    parameters))
            {
                if (reader == null)
                    return;
                while (reader.Read())
                {
                    CustomerPriceMargins margin = new CustomerPriceMargins();
                    margin.SetProperties(reader);
                    Add(margin);
                }
            }
        }

        public void LoadByPriceMarginID(int marginID)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@PriceMarginID", marginID));
            using (
                SqlDataReader reader = ExecutionHelper.ExecuteReader(
                    "up_Select_CustomerPriceMarginsByAndPriceMarginID", parameters))
            {
                if (reader == null)
                    return;
                while (reader.Read())
                {
                    CustomerPriceMargins margin = new CustomerPriceMargins();
                    margin.SetProperties(reader);
                    Add(margin);
                }
            }
        }

        public void LoadByCustClientIDAndICAO(int custClientID, int adminClientID, string icao)
        {
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Load_Pricing", GetParametersForPriceLoading(custClientID, adminClientID, icao)))
            {
                AddPriceMarginsFromReader(reader);
            }
        }

        public void LoadByCustClientIDForAllVendors(int custClientID, int adminClientID, string icao)
        {
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Load_PricingForAllVendors", GetParametersForPriceLoading(custClientID, adminClientID, icao)))
            {
                AddPriceMarginsFromReader(reader);
            }
        }

        public void LoadHighestMargin(int priceMarginID)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@PriceMarginID", priceMarginID));
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Select_HighestMarginByPriceMarginID", parameters))
            {
                if (reader == null)
                    return;
                while (reader.Read())
                {
                    CustomerPriceMargins margin = new CustomerPriceMargins();
                    margin.SetProperties(reader);
                    Add(margin);
                }
            }
        }

        public void Delete(int id)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Id", id));
            ExecutionHelper.ExecuteNonQuery("up_Delete_CustomerPriceMargin", parameters);
        }

        public CustomerPriceMargins GetId(int id)
        {
            return this.FirstOrDefault(margin => margin.Id == id);
        }

        public CustomerPriceMargins GetCustClientID(int clientId)
        {
            return this.FirstOrDefault(margin => margin.CustClientID == clientId);
        }

        public CustomerPriceMargins GetPriceMarginID(int marginId)
        {
            return this.FirstOrDefault(margin => margin.PriceMarginID == marginId);
        }

        public CustomerPriceMargins GetTotalCost(decimal total)
        {
            return this.FirstOrDefault(margin => margin.Price == total);
        }

        public CustomerPriceMargins GetHighestMargin(float highest)
        {
            return this.FirstOrDefault(margin => margin.HighestMargin == highest);
        }
        #endregion

        #region Private Methods
        private List<SqlParameter> GetParametersForPriceLoading(int custClientID, int adminClientID, string icao)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@CustClientID", custClientID));
            parameters.Add(new SqlParameter("@AdminClientID", adminClientID));
            parameters.Add(new SqlParameter("@ICAO", icao));
            return parameters;
        }

        private void AddPriceMarginsFromReader(SqlDataReader reader)
        {
            if (reader == null)
                return;
            while (reader.Read())
            {
                CustomerPriceMargins margin = new CustomerPriceMargins();
                margin.SetProperties(reader);
                margin.PriceMarginTiers = new PriceMarginTiers();
                margin.PriceMarginTiers.SetProperties(reader);
                margin.SupplierPrices = new SupplierFuelsPrices();
                margin.SupplierPrices.SetProperties(reader);
                Add(margin);
            }
        }
        #endregion
    }
    #endregion
}