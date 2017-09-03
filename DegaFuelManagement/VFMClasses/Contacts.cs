using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Degatech.Common;
using Degatech.Utilities.SQL;
using System.Collections;

namespace VFMClasses
{

    #region Contacts
    /// <summary>
    /// This object represents the properties and methods of a Contact.
    /// </summary>
    public class Contacts : BaseClass
    {
        #region Properties
        public int Id { get; set; }
        public string ContactType { get; set; }
        public int AdminClientID { get; set; }
        public int CustClientID { get; set; }
        public string FirstName { get; set; } = String.Empty;
        public string LastName { get; set; } = String.Empty;
        public string Title { get; set; } = String.Empty;
        public string CompanyName { get; set; }
        public string MobilePhone { get; set; } = String.Empty;
        public string Phone { get; set; } = String.Empty;
        public string Email { get; set; } = String.Empty;
        public string Address { get; set; } = String.Empty;
        public string City { get; set; } = String.Empty;
        public string State { get; set; } = String.Empty;
        public string ZipCode { get; set; } = String.Empty;
        public string Country { get; set; } = String.Empty;
        public bool Distribute { get; set; }
        public string Note { get; set; }
        public DateTime DateAdded { get; set; }
        public ContactNotesCollection Notes { get; set; }
        public ContactDetailCustomFieldsCollection CustomFields { get; set; }
        public Registration Registration { get; set; }
        public string TailNumbers { get; set; }
        public string AircraftType { get; set; }
        #endregion

        #region Constructors
        public Contacts()
        {
        }

        public Contacts(int id)
        {
            Id = id;
            Load();
        }

        public Contacts(SqlDataReader reader)
        {
            LoadFromReader(reader);
        }
        #endregion

        #region Public Methods
        public void Load()
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Id", Id));
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Select_Contact", parameters))
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
            DeleteContact(Id);
        }

        public void Update()
        {
            ArrayList propertiesToOmit = new ArrayList();
            propertiesToOmit.Add("CompanyName");
            propertiesToOmit.Add("DateAdded");
            propertiesToOmit.Add("TailNumbers");
            propertiesToOmit.Add("AircraftType");
            using (
                SqlDataReader reader = ExecutionHelper.ExecuteReader("up_InsertUpdate_Contact", GetSQLParameters(propertiesToOmit)))
            {
                if (reader == null)
                    return;
                if (reader.Read())
                    SetProperties(reader);
            }
        }
        #endregion

        #region Static Methods
        public static Contacts GetContact(int id)
        {
            return new Contacts(id);
        }

        public static ContactsCollection LoadList()
        {
            ContactsCollection collection = new ContactsCollection();
            collection.LoadAll();
            return collection;
        }

        public static ContactsCollection LoadListByCustClient(int clientID)
        {
            ContactsCollection collection = new ContactsCollection();
            collection.LoadByCustClientID(clientID);
            return collection;
        }

        public static ContactsCollection LoadListByAdminClient(int clientID)
        {
            ContactsCollection collection = new ContactsCollection();
            collection.LoadByAdminClientID(clientID);
            return collection;
        }

        public static ContactsCollection DeleteContact(int id)
        {
            ContactsCollection collection = new ContactsCollection();
            collection.DeleteContact(id);
            return collection;
        }
        #endregion
    }

    #region Collection
    public class ContactsCollection : List<Contacts>
    {
        #region Public Methods
        public void LoadAll()
        {
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Select_ContactsAll"))
            {
                if (reader == null)
                    return;
                while (reader.Read())
                {
                    Contacts contact = new Contacts();
                    contact.SetProperties(reader);
                    Add(contact);
                }
            }
        }

        public void LoadByCustClientID(int clientId)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@CustClientID", clientId));
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Select_ContactsByAndCustClientID", parameters))
            {
                if (reader == null)
                    return;
                while (reader.Read())
                {
                    Contacts contact = new Contacts();
                    contact.SetProperties(reader);
                    contact.Registration = new Registration();
                    contact.Registration.SetProperties(reader);
                    Add(contact);
                }
            }
        }

        public void LoadByAdminClientID(int clientId)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@AdminClientID", clientId));
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Select_ContactsByAndAdminClientID", parameters))
            {
                if (reader == null)
                    return;
                while (reader.Read())
                {
                    Contacts contact = new Contacts();
                    contact.SetProperties(reader);
                    contact.Registration = new Registration();
                    contact.Registration.SetProperties(reader);
                    Add(contact);
                }
            }
        }

        public static DataTable GetContactsByAdminClientID(int clientId)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@AdminClientID", clientId));
            DataSet resultSet = ExecutionHelper.ExecuteDataset("up_Select_ContactsByAndAdminClientID", parameters);
            if (resultSet.Tables.Count > 0)
                return resultSet.Tables[0];
            return null;
        }

        public void DeleteContact(int id)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Id", id));
            ExecutionHelper.ExecuteNonQuery("up_Delete_Contact", parameters);
        }

        public Contacts GetId(int id)
        {
            return this.FirstOrDefault(contact => contact.Id == id);
        }

        public Contacts GetContactType(string type)
        {
            return this.FirstOrDefault(contact => contact.ContactType == type);
        }

        public Contacts GetAdminClientId(int clientId)
        {
            return this.FirstOrDefault(contact => contact.AdminClientID == clientId);
        }

        public Contacts GetCustClientId(int clientId)
        {
            return this.FirstOrDefault(contact => contact.CustClientID == clientId);
        }

        public Contacts GetFirstName(string name)
        {
            return this.FirstOrDefault(contact => contact.FirstName == name);
        }

        public Contacts GetLastName(string name)
        {
            return this.FirstOrDefault(contact => contact.LastName == name);
        }

        public Contacts GetTitle(string title)
        {
            return this.FirstOrDefault(contact => contact.Title == title);
        }

        public Contacts GetCompanyName(string name)
        {
            return this.FirstOrDefault(contact => contact.CompanyName == name);
        }

        public Contacts GetMobilePhone(string phone)
        {
            return this.FirstOrDefault(contact => contact.MobilePhone == phone);
        }

        public Contacts GetPhone(string phone)
        {
            return this.FirstOrDefault(contact => contact.Phone == phone);
        }

        public Contacts GetEmail(string email)
        {
            return this.FirstOrDefault(contact => contact.Email == email);
        }

        public Contacts GetAddress(string address)
        {
            return this.FirstOrDefault(contact => contact.Address == address);
        }

        public Contacts GetCity(string city)
        {
            return this.FirstOrDefault(contact => contact.City == city);
        }

        public Contacts GetState(string state)
        {
            return this.FirstOrDefault(contact => contact.State == state);
        }

        public Contacts GetZipCode(string zipCode)
        {
            return this.FirstOrDefault(contact => contact.ZipCode == zipCode);
        }

        public Contacts GetCountry(string country)
        {
            return this.FirstOrDefault(contact => contact.Country == country);
        }

        public Contacts GetDistribute(bool dist)
        {
            return this.FirstOrDefault(contact => contact.Distribute == dist);
        }

        public Contacts GetNote(string note)
        {
            return this.FirstOrDefault(contact => contact.Note == note);
        }

        public Contacts GetDateAdded(DateTime date)
        {
            return this.FirstOrDefault(contact => contact.DateAdded == date);
        }
        #endregion
    }
    #endregion

    #endregion
}
