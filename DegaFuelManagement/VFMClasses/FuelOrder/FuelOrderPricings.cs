using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Degatech.Common;
using Degatech.Utilities.SQL;
using System.Linq;

namespace VFMClasses
{
    #region FuelOrderPricings
    /// <summary>
    /// This object represents the properties and methods of a FuelOrderPricing.
    /// </summary>
    public class FuelOrderPricings : BaseClass
    {
        #region Properties
        public int Id { get; set; }
        public string Vendor { get; set; } = String.Empty;
        public string IATA { get; set; } = String.Empty;
        public string ICAO { get; set; } = String.Empty;
        public string FBOName { get; set; } = String.Empty;
        public int SupplierMin { get; set; }
        public int SupplierMax { get; set; }
        public decimal SupplierTotalWithTax { get; set; }
        public DateTime Expires { get; set; }
        public string Product { get; set; } = String.Empty;
        public string Notes { get; set; } = String.Empty;
        public int AdminClientID { get; set; }
        public int SupplierID { get; set; }
        public int CustClientID { get; set; }
        public decimal PriceTierMargin { get; set; }
        public decimal FBOMargin { get; set; }
        public decimal Price { get; set; }
        public string TailNumber { get; set; } = String.Empty;
        public int PriceMarginID { get; set; }
        public int PriceTierMin { get; set; }
        public int PriceTierMax { get; set; }
        public decimal PrimaryPriceMargin { get; set; }
        public DateTime EffectiveDate { get; set; }
        public int FuelOrderID { get; set; }
        #endregion

        #region Constructors
        public FuelOrderPricings()
        {
        }

        public FuelOrderPricings(int id)
        {
            Id = id;
            Load();
        }

        public FuelOrderPricings(SqlDataReader reader)
        {
            LoadFromReader(reader);
        }
        #endregion

        #region Public Methods
        public void Load()
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Id", Id));
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Select_FuelOrderPricing", parameters))
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
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_InsertUpdate_FuelOrderPricing", GetSQLParameters()))
            {
                if (reader == null)
                    return;
                if (reader.Read())
                    SetProperties(reader);
            }
        }

        public void LoadWholesaleInvoiced(int adminId, int supplierId, int fuelOrderId, int invoiceVolume, string fboName)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@AdminClientID", adminId));
            parameters.Add(new SqlParameter("@SupplierID", supplierId));
            parameters.Add(new SqlParameter("@FuelOrderID", fuelOrderId));
            parameters.Add(new SqlParameter("@InvoiceVolume", invoiceVolume));
            parameters.Add(new SqlParameter("@FBOName", fboName));
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Select_FuelOrderPricingsByInvoiceVolume", parameters))
            {
                if (reader == null)
                    return;
                if (reader.Read())
                    SetProperties(reader);
            }

        }
        #endregion

        #region Static Methods
        public static FuelOrderPricingsCollection LoadList()
        {
            FuelOrderPricingsCollection collection = new FuelOrderPricingsCollection();
            collection.LoadAll();
            return collection;
        }

        public static FuelOrderPricings GetFuelOrderPricing(int id)
        {
            return new FuelOrderPricings(id);
        }

        public static FuelOrderPricings GetWholesaleInvoiced(int adminId, int supplierId, int fuelOrderId, int invoiceVolume, string fboName)
        {
            FuelOrderPricings pricing = new FuelOrderPricings();
            pricing.LoadWholesaleInvoiced(adminId, supplierId, fuelOrderId, invoiceVolume, fboName);
            return pricing;
        }

        public static FuelOrderPricingsCollection Delete(int id)
        {
            FuelOrderPricingsCollection collection = new FuelOrderPricingsCollection();
            collection.Delete(id);
            return collection;
        }
        #endregion
    }
    #endregion

    #region Collection
    public class FuelOrderPricingsCollection : List<FuelOrderPricings>
    {
        #region Public Methods
        public void LoadAll()
        {
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Select_FuelOrderPricingsAll"))
            {
                if (reader == null)
                    return;
                while (reader.Read())
                {
                    FuelOrderPricings pricing = new FuelOrderPricings();
                    pricing.SetProperties(reader);
                    Add(pricing);
                }
            }
        }

        public void LoadByAdminClientID(int adminClientId)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@AdminClientID", adminClientId));
            using (
                SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Select_FuelOrderPricingsByAndAdminClientID",
                    parameters))
            {
                if (reader == null)
                    return;
                while (reader.Read())
                {
                    FuelOrderPricings pricing = new FuelOrderPricings();
                    pricing.SetProperties(reader);
                    Add(pricing);
                }
            }
        }

        public void LoadBySupplierID(int supplierID)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@SupplierID", supplierID));
            using (
                SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Select_FuelOrderPricingsByAndSupplierID",
                    parameters))
            {
                if (reader == null)
                    return;
                while (reader.Read())
                {
                    FuelOrderPricings pricing = new FuelOrderPricings();
                    pricing.SetProperties(reader);
                    Add(pricing);
                }
            }
        }

        public void GetQuoteByLocation(int adminClientID, int custClientID, string icao, string tailNumber)
        {
            using (
                SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Load_Pricing",
                    GetParametersForPriceLoading(custClientID, adminClientID, icao, tailNumber)))
            {
                    AddPricingFromReader(reader);
            }
        }

        public void GetQuoteByLocationForAllVendors(int adminClientID, int custClientID, string icao, string tailNumber)
        {
            using (
                SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Load_PricingForAllVendors",
                    GetParametersForPriceLoading(custClientID, adminClientID, icao, tailNumber)))
            {
                AddPricingFromReader(reader);
            }
        }

        public void Delete(int id)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Id", id));
            ExecutionHelper.ExecuteNonQuery("up_Delete_CustomerPriceMargin", parameters);
        }

        public FuelOrderPricings GetId(int id)
        {
            return this.FirstOrDefault(pricing => pricing.Id == id);
        }

        public FuelOrderPricings GetVendor(string vendor)
        {
            return this.FirstOrDefault(pricing => pricing.Vendor == vendor);
        }

        public FuelOrderPricings GetIATA(string iata)
        {
            return this.FirstOrDefault(pricing => pricing.IATA == iata);
        }

        public FuelOrderPricings GetICAO(string icao)
        {
            return this.FirstOrDefault(pricing => pricing.ICAO == icao);
        }

        public FuelOrderPricings GetFBO(string fbo)
        {
            return this.FirstOrDefault(pricing => pricing.FBOName == fbo);
        }

        public FuelOrderPricings GetMin(int min)
        {
            return this.FirstOrDefault(pricing => pricing.PriceTierMin == min);
        }

        public FuelOrderPricings GetMax(int max)
        {
            return this.FirstOrDefault(pricing => pricing.PriceTierMax == max);
        }

        public FuelOrderPricings GetTotalWithTax(decimal total)
        {
            return this.FirstOrDefault(pricing => pricing.SupplierTotalWithTax == total);
        }

        public FuelOrderPricings GetExpires(DateTime expires)
        {
            return this.FirstOrDefault(pricing => pricing.Expires == expires);
        }

        public FuelOrderPricings GetProduct(string product)
        {
            return this.FirstOrDefault(pricing => pricing.Product == product);
        }

        public FuelOrderPricings GetNotes(string notes)
        {
            return this.FirstOrDefault(pricing => pricing.Notes == notes);
        }

        public FuelOrderPricings GetAdminClientID(int adminId)
        {
            return this.FirstOrDefault(pricing => pricing.AdminClientID == adminId);
        }

        public FuelOrderPricings GetSupplierID(int pricingId)
        {
            return this.FirstOrDefault(pricing => pricing.SupplierID == pricingId);
        }
        #endregion

        #region Private Methods
        private void AddPricingFromReader(SqlDataReader reader)
        {
            while (reader.Read())
            {
                FuelOrderPricings pricing = new FuelOrderPricings();
                pricing.SetProperties(reader);
                Add(pricing);
            }
        }

        private List<SqlParameter> GetParametersForPriceLoading(int custClientID, int adminClientID, string icao, string tailNumber)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@CustClientID", custClientID));
            parameters.Add(new SqlParameter("@AdminClientID", adminClientID));
            parameters.Add(new SqlParameter("@ICAO", icao));
            parameters.Add(new SqlParameter("@TailNumber", tailNumber));
            return parameters;
        }
        #endregion
    }
    #endregion
}
