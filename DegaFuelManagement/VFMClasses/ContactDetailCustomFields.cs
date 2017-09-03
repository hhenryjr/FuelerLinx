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
    #region ContactDetailCustomFields
    /// <summary>
    /// This object represents the properties and methods of a ContactDetailCustomFields.
    /// </summary>
    public class ContactDetailCustomFields : BaseClass
    {
        #region Properties
        public int Id { get; set; }
        public string FieldType { get; set; } = String.Empty;
        public string Title { get; set; } = String.Empty;
        public string Content { get; set; } = String.Empty;
        public int ContactID { get; set; }
        #endregion

        #region Constructors
        public ContactDetailCustomFields()
        {
        }

        public ContactDetailCustomFields(int id)
        {
            Id = id;
            Load();
        }

        public ContactDetailCustomFields(SqlDataReader reader)
        {
            LoadFromReader(reader);
        }
        #endregion

        #region Public Methods
        public void Load()
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Id", Id));
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Select_ContactDetailCustomField", parameters))
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
        //    DeleteContactDetailCustomField(Id);
        //}

        public void Update()
        {
            //ArrayList propertiesToOmit = new ArrayList();
            //propertiesToOmit.Add("IsEditable");
            //propertiesToOmit.Add("IsOmitted");
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_InsertUpdate_ContactDetailCustomField", GetSQLParameters(/*propertiesToOmit*/)))
            {
                if (reader == null)
                    return;
                if (reader.Read())
                    SetProperties(reader);
            }
        }
        #endregion

        #region Static Methods
        public static ContactDetailCustomFields GetContactDetailCustomField(int id)
        {
            return new ContactDetailCustomFields(id);
        }

        public static ContactDetailCustomFieldsCollection LoadList()
        {
            ContactDetailCustomFieldsCollection collection = new ContactDetailCustomFieldsCollection();
            collection.LoadAll();
            return collection;
        }

        public static ContactDetailCustomFieldsCollection LoadListByAdminAndICAO(int adminID, string icao)
        {
            ContactDetailCustomFieldsCollection collection = new ContactDetailCustomFieldsCollection();
            collection.LoadByAdminIDAndICAO(adminID, icao);
            return collection;
        }

        //public static ContactDetailCustomFields GetContactDetails(string icao, string fbo, int clientId)
        //{
        //    ContactDetailCustomFields collection = new ContactDetailCustomFields();
        //    collection.GetContactDetailsByICAO_FBO_Admin(icao, fbo, clientId);
        //    return collection;
        //}

        public static ContactDetailCustomFieldsCollection GetContactDetailCustomFields(int contactId)
        {
            ContactDetailCustomFieldsCollection collection = new ContactDetailCustomFieldsCollection();
            collection.GetCustomFields(contactId);
            return collection;
        }

        public static ContactDetailCustomFieldsCollection DeleteContactDetailCustomField(int id)
        {
            ContactDetailCustomFieldsCollection collection = new ContactDetailCustomFieldsCollection();
            collection.DeleteContactDetailCustomField(id);
            return collection;
        }
        #endregion
    }
    #endregion

    #region Collection
    public class ContactDetailCustomFieldsCollection : List<ContactDetailCustomFields>
    {
        public void LoadAll()
        {
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Select_ContactDetailCustomFieldsAll"))
            {
                if (reader == null)
                    return;
                while (reader.Read())
                {
                    ContactDetailCustomFields margin = new ContactDetailCustomFields();
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
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Select_ContactDetailCustomFieldsByAndAdminClientIDAndICAO", parameters))
            {
                if (reader == null)
                    return;
                while (reader.Read())
                {
                    ContactDetailCustomFields margin = new ContactDetailCustomFields();
                    margin.SetProperties(reader);
                    Add(margin);
                }
            }
        }

        public void GetCustomFields(int contactId)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ContactID", contactId));
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Select_ContactDetailCustomFieldsByAndContactID", parameters))
            {
                if (reader == null)
                    return;
                while (reader.Read())
                {
                    ContactDetailCustomFields margin = new ContactDetailCustomFields();
                    margin.SetProperties(reader);
                    Add(margin);
                }
            }
        }

        public void DeleteContactDetailCustomField(int id)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Id", id));
            ExecutionHelper.ExecuteNonQuery("up_Delete_ContactDetailCustomField", parameters);
        }

        public ContactDetailCustomFields GetId(int id)
        {
            return this.FirstOrDefault(margin => margin.Id == id);
        }

        public ContactDetailCustomFields GetAdminClientID(int adminId)
        {
            return this.FirstOrDefault(margin => margin.ContactID == adminId);
        }

        public ContactDetailCustomFields GetFieldType(string type)
        {
            return this.FirstOrDefault(m => m.FieldType == type);
        }

        public ContactDetailCustomFields GetTitle(string title)
        {
            return this.FirstOrDefault(m => m.Title == title);
        }

        public ContactDetailCustomFields GetContent(string content)
        {
            return this.FirstOrDefault(m => m.Content == content);
        }
    }
    #endregion
}