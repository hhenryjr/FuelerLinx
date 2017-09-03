using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Degatech.Common;

namespace VFMClasses
{
    #region ContactUs
    /// <summary>
    /// This object represents the properties and methods of a ContactUs.
    /// </summary>
    public class ContactUs : BaseClass
    {
        #region Properties
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public string Email { get; set; } = String.Empty;
        public string Subject { get; set; } = String.Empty;
        public string Message { get; set; } = String.Empty;
        public DateTime DateAdded { get; set; }
        #endregion

        #region Constructors
        public ContactUs()
        {
        }

        public ContactUs(int id)
        {
            Id = id;
            Load();
        }

        public ContactUs(SqlDataReader reader)
        {
            LoadFromReader(reader);
        }
        #endregion

        #region Public Methods
        public void Load()
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Id", Id));
            using (SqlDataReader reader = Degatech.Utilities.SQL.ExecutionHelper.ExecuteReader("up_Select_ContactUs", parameters))
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
            DeleteContactUs(Id);
        }

        public void Update()
        {
            using (SqlDataReader reader = Degatech.Utilities.SQL.ExecutionHelper.ExecuteReader("up_InsertUpdate_ContactUs", GetSQLParameters()))
            {
                if (reader == null)
                    return;
                if (reader.Read())
                    SetProperties(reader);
            }
        }
        #endregion

        #region Static Methods
        public static ContactUsCollection LoadList()
        {
            ContactUsCollection collection = new ContactUsCollection();
            collection.LoadAll();
            return collection;
        }

        public static ContactUs GetContactUs(int id)
        {
            return new ContactUs(id);
        }

        public static ContactUsCollection DeleteContactUs(int id)
        {
            ContactUsCollection collection = new ContactUsCollection();
            collection.Delete(id);
            return collection;
        }
        #endregion

    }
    #endregion

    #region Collection
    public class ContactUsCollection : List<ContactUs>
    {
        public void LoadAll()
        {
            using (SqlDataReader reader = Degatech.Utilities.SQL.ExecutionHelper.ExecuteReader("up_Select_ContactUsAll"))
            {
                if (reader == null)
                    return;
                while (reader.Read())
                {
                    ContactUs contactUs = new ContactUs();
                    contactUs.SetProperties(reader);
                    Add(contactUs);
                }
            }
        }

        public void Delete(int id)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Id", id));
            Degatech.Utilities.SQL.ExecutionHelper.ExecuteNonQuery("up_Delete_ContactUs", parameters);
        }

        public ContactUs GetId(int id)
        {
            return this.FirstOrDefault(contactUs => contactUs.Id == id);
        }

        public ContactUs GetName(string name)
        {
            return this.FirstOrDefault(contactUs => contactUs.Name == name);
        }

        public ContactUs GetEmail(string email)
        {
            return this.FirstOrDefault(contactUs => contactUs.Email == email);
        }

        public ContactUs GetSubject(string subject)
        {
            return this.FirstOrDefault(contactUs => contactUs.Subject == subject);
        }

        public ContactUs GetMessage(string message)
        {
            return this.FirstOrDefault(contactUs => contactUs.Message == message);
        }

        public ContactUs GetDateAdded(DateTime date)
        {
            return this.FirstOrDefault(contactUs => contactUs.DateAdded == date);
        }
    }
    #endregion

}
