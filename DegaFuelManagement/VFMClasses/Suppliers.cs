using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Degatech.Common;
using Degatech.Utilities.SQL;
using System.Linq;

namespace VFMClasses
{
    #region Suppliers
    /// <summary>
    /// This object represents the properties and methods of a Suppliers.
    /// </summary>
    public class Suppliers : BaseClass
    {
        #region Properties
        public int Id { get; set; }
        public int AdminClientID { get; set; }
        public string SupplierName { get; set; } = String.Empty;
        public DateTime LastUpdate { get; set; } = DatabaseDateTimeMinValue();
        public int ValidPricingPercentage { get; set; }
        public string SupplierEmail { get; set; }
        public string SupplierPhone { get; set; }
        #endregion

        #region Readonly
        public string LastUpdateDisplayText
        {
            get
            {
                return IsDateNothing(LastUpdate) ? "N/A" : LastUpdate.ToShortDateString();
            }
        }
        #endregion

        #region Constructors
        public Suppliers()
        {
        }

        public Suppliers(int id)
        {
            Id = id;
            Load();
        }

        public Suppliers(SqlDataReader reader)
        {
            LoadFromReader(reader);
        }
        #endregion

        #region Public Methods
        public void Load()
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Id", Id));
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Select_Supplier", parameters))
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
            propertiesToOmit.Add("AdminClientID");
            propertiesToOmit.Add("SupplierName");
            propertiesToOmit.Add("LastUpdate");
            propertiesToOmit.Add("ValidPricingPercentage");
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_InsertUpdate_Supplier", GetSQLParameters(propertiesToOmit)))
            {
                if (reader == null)
                    return;
                if (reader.Read())
                    SetProperties(reader);
            }
        }
        #endregion

        #region Static Methods
        public static SuppliersCollection LoadList()
        {
            SuppliersCollection collection = new SuppliersCollection();
            collection.LoadAll();
            return collection;
        }

        public static SuppliersCollection LoadByAdminClient(int adminId)
        {
            SuppliersCollection collection = new SuppliersCollection();
            collection.LoadByAdmin(adminId);
            return collection;
        }

        public static Suppliers GetSupplier(int id)
        {
            return new Suppliers(id);
        }

        public static SuppliersCollection Delete(int id)
        {
            SuppliersCollection collection = new SuppliersCollection();
            collection.Delete(id);
            return collection;
        }
        #endregion
    }
    #endregion

    #region Collection
    public class SuppliersCollection : List<Suppliers>
    {
        public void LoadAll()
        {
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Select_SuppliersAll"))
            {
                if (reader == null)
                    return;
                while (reader.Read())
                {
                    Suppliers supplier = new Suppliers();
                    supplier.SetProperties(reader);
                    Add(supplier);
                }
            }
        }

        public void LoadByAdmin(int adminId)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@AdminClientID", adminId));
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Select_SuppliersByAdminClientID", parameters))
            {
                if (reader == null)
                    return;
                while (reader.Read())
                {
                    Suppliers supplier = new Suppliers();
                    supplier.SetProperties(reader);
                    Add(supplier);
                }
            }
        }

        public void Delete(int id)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Id", id));
            ExecutionHelper.ExecuteNonQuery("up_Delete_SupplierFuelsPrice", parameters);
        }

        public Suppliers GetId(int id)
        {
            return this.FirstOrDefault(supplier => supplier.Id == id);
        }

        public Suppliers GetName(string vendor)
        {
            return this.FirstOrDefault(supplier => supplier.SupplierName == vendor);
        }
    }
    #endregion

}


