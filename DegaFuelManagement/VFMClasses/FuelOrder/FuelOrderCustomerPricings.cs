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
    #region FuelOrderCustomerPricings
    /// <summary>
    /// This object represents the properties and methods of a FuelOrderCustomerPricing.
    /// </summary>
    public class FuelOrderCustomerPricings : FuelOrderPricings
    {

        #region Constructors
        public FuelOrderCustomerPricings()
        {
        }

        public FuelOrderCustomerPricings(int id)
        {
            Id = id;
            Load();
        }

        public FuelOrderCustomerPricings(SqlDataReader reader)
        {
            LoadFromReader(reader);
        }
        #endregion

        #region Public Methods
        public void Load()
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Id", Id));
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Select_FuelOrderCustomerPricing", parameters))
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

        public void LoadCustomerInvoiced(int adminId, int supplierId, int fuelOrderId, int invoiceVolume, string fboName)
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

        public void Delete()
        {
            Delete(Id);
        }

        public void Update()
        {
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_InsertUpdate_FuelOrderCustomerPricing", GetSQLParameters()))
            {
                if (reader == null)
                    return;
                if (reader.Read())
                    SetProperties(reader);
            }
        }
        #endregion

        #region Static Methods
        public static FuelOrderCustomerPricingsCollection GetFuelOrderCustomerPricingByFuelOrderId(int fuelOrderId)
        {
            FuelOrderCustomerPricingsCollection result = new FuelOrderCustomerPricingsCollection();
            result.LoadByFuelOrderId(fuelOrderId);
            return result;
        }

        public static FuelOrderCustomerPricingsCollection LoadList()
        {
            FuelOrderCustomerPricingsCollection collection = new FuelOrderCustomerPricingsCollection();
            collection.LoadAll();
            return collection;
        }

        public static FuelOrderCustomerPricings GetFuelOrderCustomerPricing(int id)
        {
            return new FuelOrderCustomerPricings(id);
        }

        public static FuelOrderCustomerPricings GetCustomerInvoiced(int adminId, int supplierId, int fuelOrderId, int invoiceVolume, string fboName)
        {
            FuelOrderCustomerPricings pricing = new FuelOrderCustomerPricings();
            pricing.LoadCustomerInvoiced(adminId, supplierId, fuelOrderId, invoiceVolume, fboName);
            return pricing;
        }

        public static FuelOrderCustomerPricingsCollection Delete(int id)
        {
            FuelOrderCustomerPricingsCollection collection = new FuelOrderCustomerPricingsCollection();
            collection.Delete(id);
            return collection;
        }
        #endregion
    }
    #endregion

    #region Collection
    public class FuelOrderCustomerPricingsCollection : List<FuelOrderCustomerPricings>
    {
        public void LoadByFuelOrderId(int fuelOrderId)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@fuelOrderId", fuelOrderId));
            using (
                SqlDataReader reader =
                    Degatech.Utilities.SQL.ExecutionHelper.ExecuteReader(
                        "up_Select_FuelOrderCustomerPricingsByFuelOrderId", parameters))
            {
                LoadFromReader(reader);
            }
        }

        public void LoadAll()
        {
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Select_FuelOrderCustomerPricingsAll"))
            {
                if (reader == null)
                    return;
                while (reader.Read())
                {
                    FuelOrderCustomerPricings pricing = new FuelOrderCustomerPricings();
                    pricing.SetProperties(reader);
                    Add(pricing);
                }
            }
        }

        public void LoadByAdminClientID(int adminClientId)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@AdminClientID", adminClientId));
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Select_FuelOrderCustomerPricingsByAndAdminClientID", parameters))
            {
                if (reader == null)
                    return;
                while (reader.Read())
                {
                    FuelOrderCustomerPricings pricing = new FuelOrderCustomerPricings();
                    pricing.SetProperties(reader);
                    Add(pricing);
                }
            }
        }

        public void LoadBySupplierID(int supplierID)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@SupplierID", supplierID));
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Select_FuelOrderCustomerPricingsByAndSupplierID", parameters))
            {
                if (reader == null)
                    return;
                while (reader.Read())
                {
                    FuelOrderCustomerPricings pricing = new FuelOrderCustomerPricings();
                    pricing.SetProperties(reader);
                    Add(pricing);
                }
            }
        }

        public void Delete(int id)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Id", id));
            ExecutionHelper.ExecuteNonQuery("up_Delete_CustomerPriceMargin", parameters);
        }

        public FuelOrderCustomerPricings GetId(int id)
        {
            return this.FirstOrDefault(pricing => pricing.Id == id);
        }

        public FuelOrderCustomerPricings GetVendor(string vendor)
        {
            return this.FirstOrDefault(pricing => pricing.Vendor == vendor);
        }

        public FuelOrderCustomerPricings GetIATA(string iata)
        {
            return this.FirstOrDefault(pricing => pricing.IATA == iata);
        }

        public FuelOrderCustomerPricings GetICAO(string icao)
        {
            return this.FirstOrDefault(pricing => pricing.ICAO == icao);
        }

        public FuelOrderCustomerPricings GetFBO(string fbo)
        {
            return this.FirstOrDefault(pricing => pricing.FBOName == fbo);
        }

        public FuelOrderCustomerPricings GetMin(int min)
        {
            return this.FirstOrDefault(pricing => pricing.PriceTierMin == min);
        }

        public FuelOrderCustomerPricings GetMax(int max)
        {
            return this.FirstOrDefault(pricing => pricing.PriceTierMax == max);
        }

        public FuelOrderCustomerPricings GetTotalWithTax(decimal total)
        {
            return this.FirstOrDefault(pricing => pricing.SupplierTotalWithTax == total);
        }

        public FuelOrderCustomerPricings GetExpires(DateTime expires)
        {
            return this.FirstOrDefault(pricing => pricing.Expires == expires);
        }

        public FuelOrderCustomerPricings GetProduct(string product)
        {
            return this.FirstOrDefault(pricing => pricing.Product == product);
        }

        public FuelOrderCustomerPricings GetNotes(string notes)
        {
            return this.FirstOrDefault(pricing => pricing.Notes == notes);
        }

        public FuelOrderCustomerPricings GetAdminClientID(int adminId)
        {
            return this.FirstOrDefault(pricing => pricing.AdminClientID == adminId);
        }

        public FuelOrderCustomerPricings GetSupplierID(int pricingId)
        {
            return this.FirstOrDefault(pricing => pricing.SupplierID == pricingId);
        }

        #region Private Methods
        private void LoadFromReader(SqlDataReader reader)
        {
            while (reader.Read())
            {
                FuelOrderCustomerPricings pricing = new FuelOrderCustomerPricings();
                pricing.SetProperties(reader);
                Add(pricing);
            }
        }
        #endregion
    }
    #endregion
}
