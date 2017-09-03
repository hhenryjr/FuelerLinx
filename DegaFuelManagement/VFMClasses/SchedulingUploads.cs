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
    public class SchedulingUploads : BaseClass
    {
        #region Properties
        public string Name { get; set; }
        public int CustClientID { get; set; }
        public DateTime DateUploaded { get; set; }
        public int AdminClientID { get; set; }
        #endregion

        #region Constructors
        public SchedulingUploads()
        {
        }

        public SchedulingUploads(string name, int custId, int adminClientID)
        {
            Name = name;
            CustClientID = custId;
            AdminClientID = adminClientID;
            Load();
        }

        public SchedulingUploads(SqlDataReader reader)
        {
            LoadFromReader(reader);
        }
        #endregion

        #region Public Methods
        public void Load()
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Name", Name));
            parameters.Add(new SqlParameter("@CustClientID", CustClientID));
            parameters.Add(new SqlParameter("@AdminClientID", AdminClientID));
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Select_SchedulingUploadsByName", parameters))
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
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_InsertUpdate_SchedulingUpload", GetSQLParameters()))
            {
                if (reader == null)
                    return;
                if (reader.Read())
                    SetProperties(reader);
            }
        }
        #endregion

        #region Static Methods
        public static SchedulingUploads GetUpload(string name, int custId, int adminClientID)
        {
            return new SchedulingUploads(name, custId, adminClientID);
        }
        #endregion
    }
}
