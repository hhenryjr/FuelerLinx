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
    #region Vendors
    /// <summary>
    /// This object represents the properties and methods of a Vendors.
    /// </summary>
    public class Vendors : BaseClass
    {
        #region Properties
        public int Id { get; set; }
        public int AdminClientID { get; set; }
        public string Name { get; set; } = String.Empty;
        public bool IsDeactivated { get; set; }
        public bool IsExcluded { get; set; }
        public double Margin { get; set; }
        public string Note { get; set; } = String.Empty;

        #endregion

        #region Constructors
        public Vendors()
        {
        }

        public Vendors(int id)
        {
            Id = id;
            Load();
        }

        public Vendors(SqlDataReader reader)
        {
            LoadFromReader(reader);
        }
        #endregion

        #region Public Methods
        public void Load()
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Id", Id));
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Select_Vendor", parameters))
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
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_InsertUpdate_Vendor", GetSQLParameters()))
            {
                if (reader == null)
                    return;
                if (reader.Read())
                    SetProperties(reader);
            }
        }
        #endregion

        #region Static Methods
        public static VendorsCollection LoadList()
        {
            VendorsCollection collection = new VendorsCollection();
            collection.LoadAll();
            return collection;
        }

        public static Vendors GetVendor(int id)
        {
            return new Vendors(id);
        }

        public static VendorsCollection GetVendorsByAdminClient(int adminId)
        {
            VendorsCollection collection = new VendorsCollection();
            collection.LoadByAdminClient(adminId);
            return collection;
        }

        public static VendorsCollection Delete(int id)
        {
            VendorsCollection collection = new VendorsCollection();
            collection.Delete(id);
            return collection;
        }
        #endregion
    }
    #endregion

    #region Collection
    public class VendorsCollection : List<Vendors>
    {
        public void LoadAll()
        {
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Select_VendorsAll"))
            {
                if (reader == null)
                    return;
                while (reader.Read())
                {
                    Vendors Vendor = new Vendors();
                    Vendor.SetProperties(reader);
                    Add(Vendor);
                }
            }
        }

        public void LoadByAdminClient(int adminId)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@AdminClientID", adminId));
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Select_VendorsByAdminClientID", parameters))
            {
                if (reader == null)
                    return;
                while (reader.Read())
                {
                    Vendors Vendor = new Vendors();
                    Vendor.SetProperties(reader);
                    Add(Vendor);
                }
            }
        }

        public void Delete(int id)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Id", id));
            ExecutionHelper.ExecuteNonQuery("up_Delete_VendorFuelsPrice", parameters);
        }

        public Vendors GetId(int id)
        {
            return this.FirstOrDefault(Vendor => Vendor.Id == id);
        }

        public Vendors GetName(string vendor)
        {
            return this.FirstOrDefault(Vendor => Vendor.Name == vendor);
        }

        public Vendors GetState(bool vendor)
        {
            return this.FirstOrDefault(Vendor => Vendor.IsDeactivated == vendor);
        }

        public Vendors GetStatus(bool vendor)
        {
            return this.FirstOrDefault(Vendor => Vendor.IsExcluded == vendor);
        }

        public Vendors GetMargin(double vendor)
        {
            return this.FirstOrDefault(Vendor => Vendor.Margin == vendor);
        }

        public Vendors GetNote(string vendor)
        {
            return this.FirstOrDefault(Vendor => Vendor.Note == vendor);
        }
    }
    #endregion

}