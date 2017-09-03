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
    #region FBODetailVendorExclusions
    /// <summary>
    /// This object represents the properties and methods of a FBODetailVendorExclusions.
    /// </summary>
    public class FBODetailVendorExclusions : BaseClass
    {
        #region Properties
        public int Id { get; set; }
        public int VendorID { get; set; }
        public int FBOID { get; set; }
        public Vendors Vendor { get; set; }

        #endregion

        #region Constructors
        public FBODetailVendorExclusions()
        {
        }

        public FBODetailVendorExclusions(int id)
        {
            Id = id;
            Load();
        }

        public FBODetailVendorExclusions(SqlDataReader reader)
        {
            LoadFromReader(reader);
        }
        #endregion

        #region Public Methods
        public void Load()
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Id", Id));
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Select_FBODetailVendorExclusion", parameters))
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
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_InsertUpdate_FBODetailVendorExclusion", GetSQLParameters()))
            {
                if (reader == null)
                    return;
                if (reader.Read())
                    SetProperties(reader);
            }
        }
        #endregion

        #region Static Methods
        public static FBODetailVendorExclusionsCollection LoadList()
        {
            FBODetailVendorExclusionsCollection collection = new FBODetailVendorExclusionsCollection();
            collection.LoadAll();
            return collection;
        }

        public static FBODetailVendorExclusions GetExclusion(int id)
        {
            return new FBODetailVendorExclusions(id);
        }

        public static FBODetailVendorExclusionsCollection GetExclusionsByFBO(int custId, int adminId)
        {
            FBODetailVendorExclusionsCollection collection = new FBODetailVendorExclusionsCollection();
            collection.LoadByFBO(custId, adminId);
            return collection;
        }

        public static FBODetailVendorExclusionsCollection Delete(int id)
        {
            FBODetailVendorExclusionsCollection collection = new FBODetailVendorExclusionsCollection();
            collection.Delete(id);
            return collection;
        }
        #endregion
    }
    #endregion

    #region Collection
    public class FBODetailVendorExclusionsCollection : List<FBODetailVendorExclusions>
    {
        public void LoadAll()
        {
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Select_FBODetailVendorExclusionsAll"))
            {
                if (reader == null)
                    return;
                while (reader.Read())
                {
                    FBODetailVendorExclusions FBODetailVendorExclusion = new FBODetailVendorExclusions();
                    FBODetailVendorExclusion.SetProperties(reader);
                    Add(FBODetailVendorExclusion);
                }
            }
        }

        public void LoadByFBO(int fboId, int adminId)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@FBOID", fboId));
            parameters.Add(new SqlParameter("@AdminClientID", adminId));
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Select_FBODetailVendorExclusionsByFBOID", parameters))
            {
                if (reader == null)
                    return;
                while (reader.Read())
                {
                    FBODetailVendorExclusions FBODetailVendorExclusion = new FBODetailVendorExclusions();
                    FBODetailVendorExclusion.SetProperties(reader);
                    FBODetailVendorExclusion.Vendor = new Vendors();
                    FBODetailVendorExclusion.Vendor.SetProperties(reader);
                    Add(FBODetailVendorExclusion);
                }
            }
        }

        public void Delete(int id)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Id", id));
            ExecutionHelper.ExecuteNonQuery("up_Delete_FBODetailVendorExclusion", parameters);
        }

        public FBODetailVendorExclusions GetId(int id)
        {
            return this.FirstOrDefault(FBODetailVendorExclusion => FBODetailVendorExclusion.Id == id);
        }

        public FBODetailVendorExclusions GetVendorId(int id)
        {
            return this.FirstOrDefault(FBODetailVendorExclusion => FBODetailVendorExclusion.VendorID == id);
        }

        public FBODetailVendorExclusions GetClientId(int id)
        {
            return this.FirstOrDefault(FBODetailVendorExclusion => FBODetailVendorExclusion.FBOID == id);
        }
    }
    #endregion

}