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
    #region CustomerDetailsCustomFields
    /// <summary>
    /// This object represents the properties and methods of a CustomerDetailsCustomFields.
    /// </summary>
    public class CustomerDetailsCustomFields : BaseClass
    {
        #region Properties
        public int Id { get; set; }
        public string FieldType { get; set; } = String.Empty;
        public string Title { get; set; } = String.Empty;
        public string Content { get; set; } = String.Empty;
        public int AdminClientID { get; set; }
        public int CustClientID { get; set; }
        #endregion

        #region Constructors
        public CustomerDetailsCustomFields()
        {
        }

        public CustomerDetailsCustomFields(int id)
        {
            Id = id;
            Load();
        }

        public CustomerDetailsCustomFields(SqlDataReader reader)
        {
            LoadFromReader(reader);
        }
        #endregion

        #region Public Methods
        public void Load()
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Id", Id));
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Select_CustomerDetailsCustomField", parameters))
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
        //    DeleteCustomerDetailsCustomField(Id);
        //}

        public void Update()
        {
            //ArrayList propertiesToOmit = new ArrayList();
            //propertiesToOmit.Add("IsEditable");
            //propertiesToOmit.Add("IsOmitted");
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_InsertUpdate_CustomerDetailsCustomField", GetSQLParameters(/*propertiesToOmit*/)))
            {
                if (reader == null)
                    return;
                if (reader.Read())
                    SetProperties(reader);
            }
        }
        #endregion

        #region Static Methods
        public static CustomerDetailsCustomFields GetCustomerDetailsCustomField(int id)
        {
            return new CustomerDetailsCustomFields(id);
        }

        public static CustomerDetailsCustomFieldsCollection LoadList()
        {
            CustomerDetailsCustomFieldsCollection collection = new CustomerDetailsCustomFieldsCollection();
            collection.LoadAll();
            return collection;
        }

        public static CustomerDetailsCustomFieldsCollection LoadListByAdminAndICAO(int adminID, string icao)
        {
            CustomerDetailsCustomFieldsCollection collection = new CustomerDetailsCustomFieldsCollection();
            collection.LoadByAdminIDAndICAO(adminID, icao);
            return collection;
        }

        //public static CustomerDetailsCustomFields GetCustomerDetailss(string icao, string fbo, int clientId)
        //{
        //    CustomerDetailsCustomFields collection = new CustomerDetailsCustomFields();
        //    collection.GetCustomerDetailssByICAO_FBO_Admin(icao, fbo, clientId);
        //    return collection;
        //}

        public static CustomerDetailsCustomFieldsCollection GetCustomerDetailsCustomFields(int adminId, int custId)
        {
            CustomerDetailsCustomFieldsCollection collection = new CustomerDetailsCustomFieldsCollection();
            collection.GetCustomFields(adminId, custId);
            return collection;
        }

        public static CustomerDetailsCustomFieldsCollection DeleteCustomerDetailsCustomField(int id)
        {
            CustomerDetailsCustomFieldsCollection collection = new CustomerDetailsCustomFieldsCollection();
            collection.DeleteCustomerDetailsCustomField(id);
            return collection;
        }
        #endregion
    }
    #endregion

    #region Collection
    public class CustomerDetailsCustomFieldsCollection : List<CustomerDetailsCustomFields>
    {
        public void LoadAll()
        {
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Select_CustomerDetailsCustomFieldsAll"))
            {
                if (reader == null)
                    return;
                while (reader.Read())
                {
                    CustomerDetailsCustomFields margin = new CustomerDetailsCustomFields();
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
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Select_CustomerDetailsCustomFieldsByAndAdminClientIDAndICAO", parameters))
            {
                if (reader == null)
                    return;
                while (reader.Read())
                {
                    CustomerDetailsCustomFields margin = new CustomerDetailsCustomFields();
                    margin.SetProperties(reader);
                    Add(margin);
                }
            }
        }

        public void GetCustomFields(int adminId, int custId)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@AdminClientID", adminId));
            parameters.Add(new SqlParameter("@CustClientID", custId));
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Select_CustomerDetailsCustomFieldsByAndAdminClientIDAndCustClientID", parameters))
            {
                if (reader == null)
                    return;
                while (reader.Read())
                {
                    CustomerDetailsCustomFields margin = new CustomerDetailsCustomFields();
                    margin.SetProperties(reader);
                    Add(margin);
                }
            }
        }

        public void DeleteCustomerDetailsCustomField(int id)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Id", id));
            ExecutionHelper.ExecuteNonQuery("up_Delete_CustomerDetailsCustomField", parameters);
        }

        public CustomerDetailsCustomFields GetId(int id)
        {
            return this.FirstOrDefault(margin => margin.Id == id);
        }

        public CustomerDetailsCustomFields GetAdminClientID(int adminId)
        {
            return this.FirstOrDefault(margin => margin.AdminClientID == adminId);
        }

        public CustomerDetailsCustomFields GetCustClientID(int clientId)
        {
            return this.FirstOrDefault(margin => margin.CustClientID == clientId);
        }

        public CustomerDetailsCustomFields GetFieldType(string type)
        {
            return this.FirstOrDefault(m => m.FieldType == type);
        }

        public CustomerDetailsCustomFields GetTitle(string title)
        {
            return this.FirstOrDefault(m => m.Title == title);
        }

        public CustomerDetailsCustomFields GetContent(string content)
        {
            return this.FirstOrDefault(m => m.Content == content);
        }
    }
    #endregion
}