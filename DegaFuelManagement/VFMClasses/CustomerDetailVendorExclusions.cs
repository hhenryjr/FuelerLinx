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
    #region CustomerDetailVendorExclusions
    /// <summary>
    /// This object represents the properties and methods of a CustomerDetailVendorExclusions.
    /// </summary>
    public class CustomerDetailVendorExclusions : BaseClass
    {
        #region Properties
        public int Id { get; set; }
        public int VendorID { get; set; }
        public int CustClientID { get; set; }
        public bool IsVendorExcluded { get; set; }
        public string TailNumbers { get; set; } = String.Empty;
        public Vendors Vendor { get; set; }

        public string[] TailNumberList
        {
            get
            {
                //if (TailNumbers == null)
                //    TailNumbers = String.Empty;
                return TailNumbers.Split(',');
            }
            set
            {
                TailNumbers = Degatech.Utilities.Data.Utilities.ArrayListToCommaDelimited(new ArrayList(value));
            }
        }

        #endregion

        #region Constructors
        public CustomerDetailVendorExclusions()
        {
        }

        public CustomerDetailVendorExclusions(int id)
        {
            Id = id;
            Load();
        }

        public CustomerDetailVendorExclusions(SqlDataReader reader)
        {
            LoadFromReader(reader);
        }
        #endregion

        #region Public Methods
        public void Load()
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Id", Id));
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Select_CustomerDetailVendorExclusion", parameters))
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
            propertiesToOmit.Add("IsVendorExcluded");
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_InsertUpdate_CustomerDetailVendorExclusion", GetSQLParameters(propertiesToOmit)))
            {
                if (reader == null)
                    return;
                if (reader.Read())
                    SetProperties(reader);
            }
        }
        #endregion

        #region Static Methods
        public static CustomerDetailVendorExclusionsCollection LoadList()
        {
            CustomerDetailVendorExclusionsCollection collection = new CustomerDetailVendorExclusionsCollection();
            collection.LoadAll();
            return collection;
        }

        public static CustomerDetailVendorExclusions GetExclusion(int id)
        {
            return new CustomerDetailVendorExclusions(id);
        }

        public static CustomerDetailVendorExclusionsCollection GetExclusionsByCustClient(int custId, int adminId)
        {
            CustomerDetailVendorExclusionsCollection collection = new CustomerDetailVendorExclusionsCollection();
            collection.LoadByCustClient(custId, adminId);
            return collection;
        }

        public static CustomerDetailVendorExclusionsCollection Delete(int id)
        {
            CustomerDetailVendorExclusionsCollection collection = new CustomerDetailVendorExclusionsCollection();
            collection.Delete(id);
            return collection;
        }
        #endregion
    }
    #endregion

    #region Collection
    public class CustomerDetailVendorExclusionsCollection : List<CustomerDetailVendorExclusions>
    {
        public void LoadAll()
        {
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Select_CustomerDetailVendorExclusionsAll"))
            {
                if (reader == null)
                    return;
                while (reader.Read())
                {
                    CustomerDetailVendorExclusions CustomerDetailVendorExclusion = new CustomerDetailVendorExclusions();
                    CustomerDetailVendorExclusion.SetProperties(reader);
                    Add(CustomerDetailVendorExclusion);
                }
            }
        }

        public void LoadByCustClient(int custId, int adminId)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@CustClientID", custId));
            parameters.Add(new SqlParameter("@AdminClientID", adminId));
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Select_CustomerDetailVendorExclusionsByCustClientID", parameters))
            {
                if (reader == null)
                    return;
                while (reader.Read())
                {
                    CustomerDetailVendorExclusions CustomerDetailVendorExclusion = new CustomerDetailVendorExclusions();
                    CustomerDetailVendorExclusion.SetProperties(reader);
                    CustomerDetailVendorExclusion.Vendor = new Vendors();
                    CustomerDetailVendorExclusion.Vendor.SetProperties(reader);
                    Add(CustomerDetailVendorExclusion);
                }
            }
        }

        public void Delete(int id)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Id", id));
            ExecutionHelper.ExecuteNonQuery("up_Delete_CustomerDetailVendorExclusion", parameters);
        }

        public CustomerDetailVendorExclusions GetId(int id)
        {
            return this.FirstOrDefault(CustomerDetailVendorExclusion => CustomerDetailVendorExclusion.Id == id);
        }

        public CustomerDetailVendorExclusions GetVendorId(int id)
        {
            return this.FirstOrDefault(CustomerDetailVendorExclusion => CustomerDetailVendorExclusion.VendorID == id);
        }

        public CustomerDetailVendorExclusions GetClientId(int id)
        {
            return this.FirstOrDefault(CustomerDetailVendorExclusion => CustomerDetailVendorExclusion.CustClientID == id);
        }
    }
    #endregion

}