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
    #region SupplierFuelsPrices
    /// <summary>
    /// This object represents the properties and methods of a SupplierFuelsPrices.
    /// </summary>
    public class SupplierFuelsPrices : BaseClass
    {
        #region Properties
        public int Id { get; set; }
        public string Vendor { get; set; } = String.Empty;
        public string IATA { get; set; } = String.Empty;
        public string ICAO { get; set; } = String.Empty;
        public string FBOName { get; set; } = String.Empty;
        public int Min { get; set; }
        public int Max { get; set; }
        public decimal TotalWithTax { get; set; }
        public DateTime Expires { get; set; }
        public string Product { get; set; } = String.Empty;
        public string Notes { get; set; } = String.Empty;
        public int AdminClientID { get; set; }
        public int SupplierID { get; set; }
        public DateTime DateUploaded { get; set; }
        public double PricingValidity { get;set; }
        #endregion

        #region Constructors
        public SupplierFuelsPrices()
        {
        }

        public SupplierFuelsPrices(int id)
        {
            Id = id;
            Load();
        }

        public SupplierFuelsPrices(SqlDataReader reader)
        {
            LoadFromReader(reader);
        }
        #endregion

        #region Public Methods
        public void Load()
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Id", Id));
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Select_SupplierFuelsPrice", parameters))
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

        //public void Delete()
        //{
        //    Delete(Id);
        //}

        public void Update()
        {
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_InsertUpdate_SupplierFuelsPrice", GetSQLParameters()))
            {
                if (reader == null)
                    return;
                if (reader.Read())
                    SetProperties(reader);
            }
        }
        #endregion

        #region Static Methods
        public static SupplierFuelsPricesCollection LoadList()
        {
            SupplierFuelsPricesCollection collection = new SupplierFuelsPricesCollection();
            collection.LoadAll();
            return collection;
        }

        public static SupplierFuelsPrices GetSupplierFuelsPrice(int id)
        {
            return new SupplierFuelsPrices(id);
        }

        //public static SupplierFuelsPricesCollection GetSupplierFuelsPriceByICAO(string icao)
        //{
        //    SupplierFuelsPricesCollection collection = new SupplierFuelsPricesCollection();
        //    collection.LoadByICAO(icao);
        //    return collection;
        //}

        public static SupplierFuelsPricesCollection GetSupplierFuelsPriceByAdmin(int adminId)
        {
            SupplierFuelsPricesCollection collection = new SupplierFuelsPricesCollection();
            collection.LoadStatus(adminId);
            return collection;
        }

        public static SupplierFuelsPricesCollection Delete(int adminId, int supplierId)
        {
            SupplierFuelsPricesCollection collection = new SupplierFuelsPricesCollection();
            collection.Delete(adminId, supplierId);
            return collection;
        }
        #endregion
    }
    #endregion

    #region Collection
    public class SupplierFuelsPricesCollection : List<SupplierFuelsPrices>
    {
        public void LoadAll()
        {
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Select_SupplierFuelsPricesAll"))
            {
                if (reader == null)
                    return;
                while (reader.Read())
                {
                    SupplierFuelsPrices supplier = new SupplierFuelsPrices();
                    supplier.SetProperties(reader);
                    Add(supplier);
                }
            }
        }

        public void LoadByAdminClientID(int adminClientId)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@AdminClientID", adminClientId));
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Select_SupplierFuelsPricesByAndAdminClientID", parameters))
            {
                if (reader == null)
                    return;
                while (reader.Read())
                {
                    SupplierFuelsPrices supplier = new SupplierFuelsPrices();
                    supplier.SetProperties(reader);
                    Add(supplier);
                }
            }
        }

        public void LoadByICAO(int adminClientId, string icao)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@AdminClientID", adminClientId));
            parameters.Add(new SqlParameter("@ICAO", icao));
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Select_SupplierFuelsPricesByAndICAO", parameters))
            {
                if (reader == null)
                    return;
                while (reader.Read())
                {
                    SupplierFuelsPrices supplier = new SupplierFuelsPrices();
                    supplier.SetProperties(reader);
                    Add(supplier);
                }
            }
        }

        public void LoadStatus(int adminClientId)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@AdminClientID", adminClientId));
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Select_SupplierFuelsPricesStatusByAdminClientID", parameters))
            {
                if (reader == null)
                    return;
                while (reader.Read())
                {
                    SupplierFuelsPrices supplier = new SupplierFuelsPrices();
                    supplier.SetProperties(reader);
                    Add(supplier);
                }
            }
        }

        public void LoadBySupplierID(int supplierID)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@SupplierID", supplierID));
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Select_SupplierFuelsPricesByAndSupplierID", parameters))
            {
                if (reader == null)
                    return;
                while (reader.Read())
                {
                    SupplierFuelsPrices supplier = new SupplierFuelsPrices();
                    supplier.SetProperties(reader);
                    Add(supplier);
                }
            }
        }

        public void Delete(int adminId, int supplierId)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@AdminClientID", adminId));
            parameters.Add(new SqlParameter("@SupplierID", supplierId));
            ExecutionHelper.ExecuteNonQuery("up_Delete_SupplierFuelsPricesByAndAdminClientIDAndSupplierID", parameters);
        }

        public SupplierFuelsPrices GetId(int id)
        {
            return this.FirstOrDefault(supplier => supplier.Id == id);
        }

        public SupplierFuelsPrices GetVendor(string vendor)
        {
            return this.FirstOrDefault(supplier => supplier.Vendor == vendor);
        }

        public SupplierFuelsPrices GetIATA(string iata)
        {
            return this.FirstOrDefault(supplier => supplier.IATA == iata);
        }

        public SupplierFuelsPrices GetICAO(string icao)
        {
            return this.FirstOrDefault(supplier => supplier.ICAO == icao);
        }

        public SupplierFuelsPrices GetFBO(string fbo)
        {
            return this.FirstOrDefault(supplier => supplier.FBOName == fbo);
        }

        public SupplierFuelsPrices GetMin(int min)
        {
            return this.FirstOrDefault(supplier => supplier.Min == min);
        }

        public SupplierFuelsPrices GetMax(int max)
        {
            return this.FirstOrDefault(supplier => supplier.Max == max);
        }

        public SupplierFuelsPrices GetTotalWithTax(decimal total)
        {
            return this.FirstOrDefault(supplier => supplier.TotalWithTax == total);
        }

        public SupplierFuelsPrices GetExpires(DateTime expires)
        {
            return this.FirstOrDefault(supplier => supplier.Expires == expires);
        }

        public SupplierFuelsPrices GetProduct(string product)
        {
            return this.FirstOrDefault(supplier => supplier.Product == product);
        }

        public SupplierFuelsPrices GetNotes(string notes)
        {
            return this.FirstOrDefault(supplier => supplier.Notes == notes);
        }

        public SupplierFuelsPrices GetAdminClientID(int adminId)
        {
            return this.FirstOrDefault(supplier => supplier.AdminClientID == adminId);
        }

        public SupplierFuelsPrices GetSupplierID(int supplierId)
        {
            return this.FirstOrDefault(supplier => supplier.SupplierID == supplierId);
        }

        public SupplierFuelsPrices GetDateUploaded(DateTime date)
        {
            return this.FirstOrDefault(supplier => supplier.DateUploaded == date);
        }
    }
    #endregion
}