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
    #region CustomerDetailAircraftExclusions
    /// <summary>
    /// This object represents the properties and methods of a CustomerDetailAircraftExclusions.
    /// </summary>
    public class CustomerDetailAircraftExclusions : BaseClass
    {
        #region Properties
        public int Id { get; set; }
        public int AdminClientID { get; set; }
        public int CustClientID { get; set; }
        public int VendorID { get; set; }
        public string TailNumber { get; set; }

        #endregion

        #region Constructors
        public CustomerDetailAircraftExclusions()
        {
        }

        public CustomerDetailAircraftExclusions(int id)
        {
            Id = id;
            Load();
        }

        public CustomerDetailAircraftExclusions(SqlDataReader reader)
        {
            LoadFromReader(reader);
        }
        #endregion

        #region Public Methods
        public void Load()
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Id", Id));
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Select_CustomerDetailAircraftExclusion", parameters))
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
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_InsertUpdate_CustomerDetailAircraftExclusion", GetSQLParameters()))
            {
                if (reader == null)
                    return;
                if (reader.Read())
                    SetProperties(reader);
            }
        }
        #endregion

        #region Static Methods
        public static CustomerDetailAircraftExclusionsCollection LoadList()
        {
            CustomerDetailAircraftExclusionsCollection collection = new CustomerDetailAircraftExclusionsCollection();
            collection.LoadAll();
            return collection;
        }

        public static CustomerDetailAircraftExclusions GetExclusion(int id)
        {
            return new CustomerDetailAircraftExclusions(id);
        }

        public static CustomerDetailAircraftExclusionsCollection GetAircaftExclusions(int custId, int adminId, int vendorId)
        {
            CustomerDetailAircraftExclusionsCollection collection = new CustomerDetailAircraftExclusionsCollection();
            collection.LoadByAdminCustAndVendor(custId, adminId, vendorId);
            return collection;
        }

        public static CustomerDetailAircraftExclusionsCollection Delete(int id)
        {
            CustomerDetailAircraftExclusionsCollection collection = new CustomerDetailAircraftExclusionsCollection();
            collection.Delete(id);
            return collection;
        }
        #endregion
    }
    #endregion

    #region Collection
    public class CustomerDetailAircraftExclusionsCollection : List<CustomerDetailAircraftExclusions>
    {
        public void LoadAll()
        {
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Select_CustomerDetailAircraftExclusionsAll"))
            {
                if (reader == null)
                    return;
                while (reader.Read())
                {
                    CustomerDetailAircraftExclusions CustomerDetailAircraftExclusion = new CustomerDetailAircraftExclusions();
                    CustomerDetailAircraftExclusion.SetProperties(reader);
                    Add(CustomerDetailAircraftExclusion);
                }
            }
        }

        public void LoadByAdminCustAndVendor(int custId, int adminId, int vendorId)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@AdminClientID", adminId));
            parameters.Add(new SqlParameter("@CustClientID", custId));
            parameters.Add(new SqlParameter("@VendorID", vendorId));
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Select_CustomerDetailAircraftExclusionsByAdminCustAndVendorID", parameters))
            {
                if (reader == null)
                    return;
                while (reader.Read())
                {
                    CustomerDetailAircraftExclusions CustomerDetailAircraftExclusion = new CustomerDetailAircraftExclusions();
                    CustomerDetailAircraftExclusion.SetProperties(reader);
                    Add(CustomerDetailAircraftExclusion);
                }
            }
        }

        public void Delete(int id)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Id", id));
            ExecutionHelper.ExecuteNonQuery("up_Delete_CustomerDetailAircraftExclusion", parameters);
        }

        public CustomerDetailAircraftExclusions GetId(int id)
        {
            return this.FirstOrDefault(CustomerDetailAircraftExclusion => CustomerDetailAircraftExclusion.Id == id);
        }

        public CustomerDetailAircraftExclusions GetAircraftId(int id)
        {
            return this.FirstOrDefault(CustomerDetailAircraftExclusion => CustomerDetailAircraftExclusion.AdminClientID == id);
        }

        public CustomerDetailAircraftExclusions GetClientId(int id)
        {
            return this.FirstOrDefault(CustomerDetailAircraftExclusion => CustomerDetailAircraftExclusion.CustClientID == id);
        }
    }
    #endregion

}