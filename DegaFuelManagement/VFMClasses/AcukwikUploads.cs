using Degatech.Common;
using Degatech.Utilities.SQL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFMClasses
{
    public class AcukwikUploads : BaseClass
    {
        #region Properties
        public string Name { get; set; }
        public DateTime DateUploaded { get; set; }
        #endregion

        #region Constructors
        public AcukwikUploads()
        {
        }

        public AcukwikUploads(string name)
        {
            Name = name;
            Load();
        }

        public AcukwikUploads(SqlDataReader reader)
        {
            LoadFromReader(reader);
        }
        #endregion

        #region Public Methods
        public void Load()
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Name", Name));
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Select_AcukwikUploadsByName", parameters))
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

        public void Update()
        {
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_InsertUpdate_AcukwikUpload", GetSQLParameters()))
            {
                if (reader == null)
                    return;
                if (reader.Read())
                    SetProperties(reader);
            }
        }
        #endregion

        #region Static Methods
        public static AcukwikUploads GetUpload(string name)
        {
            return new AcukwikUploads(name);
        }
        #endregion
    }
}
