using Degatech.Common;
using Degatech.Utilities.SQL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFMClasses
{
    #region FBODetailCustomFields
    /// <summary>
    /// This object represents the properties and methods of a FBODetailCustomFields.
    /// </summary>
    public class FBODetailCustomFields : BaseClass
    {
        #region Properties
        public int Id { get; set; }
        public string FieldType { get; set; } = String.Empty;
        public string Title { get; set; } = String.Empty;
        public string Content { get; set; } = String.Empty;
        public int AdminClientID { get; set; }
        public string FBO { get; set; } = String.Empty;
        public string ICAO { get; set; } = String.Empty;
        #endregion

        #region Constructors
        public FBODetailCustomFields()
        {
        }

        public FBODetailCustomFields(int id)
        {
            Id = id;
            Load();
        }

        public FBODetailCustomFields(SqlDataReader reader)
        {
            LoadFromReader(reader);
        }
        #endregion

        #region Public Methods
        public void Load()
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Id", Id));
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Select_FBODetailCustomField", parameters))
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
        //    DeleteFBODetailCustomField(Id);
        //}

        public void Update()
        {
            //ArrayList propertiesToOmit = new ArrayList();
            //propertiesToOmit.Add("IsEditable");
            //propertiesToOmit.Add("IsOmitted");
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_InsertUpdate_FBODetailCustomField", GetSQLParameters(/*propertiesToOmit*/)))
            {
                if (reader == null)
                    return;
                if (reader.Read())
                    SetProperties(reader);
            }
        }
        #endregion

        #region Static Methods
        public static FBODetailCustomFields GetFBODetailCustomField(int id)
        {
            return new FBODetailCustomFields(id);
        }

        public static FBODetailCustomFieldsCollection LoadList()
        {
            FBODetailCustomFieldsCollection collection = new FBODetailCustomFieldsCollection();
            collection.LoadAll();
            return collection;
        }

        public static FBODetailCustomFieldsCollection LoadListByAdminAndICAO(int adminID, string icao)
        {
            FBODetailCustomFieldsCollection collection = new FBODetailCustomFieldsCollection();
            collection.LoadByAdminIDAndICAO(adminID, icao);
            return collection;
        }

        //public static FBODetailCustomFields GetFBODetails(string icao, string fbo, int clientId)
        //{
        //    FBODetailCustomFields collection = new FBODetailCustomFields();
        //    collection.GetFBODetailsByICAO_FBO_Admin(icao, fbo, clientId);
        //    return collection;
        //}

        public static FBODetailCustomFieldsCollection GetFBODetailCustomFields(string fbo, string icao, int clientId)
        {
            FBODetailCustomFieldsCollection collection = new FBODetailCustomFieldsCollection();
            collection.GetCustomFields(fbo, icao, clientId);
            return collection;
        }

        public static FBODetailCustomFieldsCollection DeleteFBODetailCustomField(int id)
        {
            FBODetailCustomFieldsCollection collection = new FBODetailCustomFieldsCollection();
            collection.DeleteFBODetailCustomField(id);
            return collection;
        }
        #endregion
    }
    #endregion

    #region Collection
    public class FBODetailCustomFieldsCollection : List<FBODetailCustomFields>
    {
        public void LoadAll()
        {
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Select_FBODetailCustomFieldsAll"))
            {
                if (reader == null)
                    return;
                while (reader.Read())
                {
                    FBODetailCustomFields margin = new FBODetailCustomFields();
                    margin.SetProperties(reader);
                    Add(margin);
                }
            }
        }

        public void LoadByAdminIDAndICAO(int adminId, string icao)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@AdminClientID", adminId));
            parameters.Add(new SqlParameter("@ICAO", icao));
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Select_FBODetailCustomFieldsByAndAdminClientIDAndICAO", parameters))
            {
                if (reader == null)
                    return;
                while (reader.Read())
                {
                    FBODetailCustomFields margin = new FBODetailCustomFields();
                    margin.SetProperties(reader);
                    Add(margin);
                }
            }
        }

        public void GetCustomFields(string fbo, string icao, int clientId)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@FBO", fbo));
            parameters.Add(new SqlParameter("@ICAO", icao));
            parameters.Add(new SqlParameter("@AdminClientID", clientId));
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Select_FBODetailCustomFieldsByAndFBOAndICAOAndAdminClientID", parameters))
            {
                if (reader == null)
                    return;
                while (reader.Read())
                {
                    FBODetailCustomFields margin = new FBODetailCustomFields();
                    margin.SetProperties(reader);
                    Add(margin);
                }
            }
        }

        public void DeleteFBODetailCustomField(int id)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Id", id));
            ExecutionHelper.ExecuteNonQuery("up_Delete_FBODetailCustomField", parameters);
        }

        public FBODetailCustomFields GetId(int id)
        {
            return this.FirstOrDefault(margin => margin.Id == id);
        }

        public FBODetailCustomFields GetAdminClientID(int adminId)
        {
            return this.FirstOrDefault(margin => margin.AdminClientID == adminId);
        }

        public FBODetailCustomFields GetICAO(string icao)
        {
            return this.FirstOrDefault(margin => margin.ICAO == icao);
        }

        public FBODetailCustomFields GetFBO(string fbo)
        {
            return this.FirstOrDefault(margin => margin.FBO == fbo);
        }

        public FBODetailCustomFields GetFieldType(string type)
        {
            return this.FirstOrDefault(m => m.FieldType == type);
        }

        public FBODetailCustomFields GetTitle(string title)
        {
            return this.FirstOrDefault(m => m.Title == title);
        }

        public FBODetailCustomFields GetContent(string content)
        {
            return this.FirstOrDefault(m => m.Content == content);
        }
    }
    #endregion
}