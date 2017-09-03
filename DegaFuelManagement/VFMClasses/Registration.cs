using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Degatech.Common;

namespace VFMClasses
{
    #region Registration
    /// <summary>
    /// This object represents the properties and methods of a Registration.
    /// </summary>
    public class Registration : BaseClass
    {
        #region Properties
        public int Id { get; set; }
        public string FirstName { get; set; } = String.Empty;
        public string LastName { get; set; } = String.Empty;
        public string Company { get; set; } = String.Empty;
        public string Username { get; set; } = String.Empty;
        public string Email { get; set; } = String.Empty;
        public string Password { get; set; } = String.Empty;
        public bool IsDuplicateUsername { get; set; } = false;
        public DateTime DateAdded { get; set; }
        public UsersCollection Users { get; set; }
        #endregion

        #region Constructors
        public Registration()
        {
        }

        public Registration(int id)
        {
            Id = id;
            Load();
        }

        public Registration(SqlDataReader reader)
        {
            LoadFromReader(reader);
        }
        #endregion

        #region Public Methods
        public void Load()
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Id", Id));
            using (SqlDataReader reader = Degatech.Utilities.SQL.ExecutionHelper.ExecuteReader("up_Select_Registration", parameters))
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
            DeleteRegistration(Id);
        }

        public void Update()
        {
            ArrayList propertiesToOmit = new ArrayList();
            propertiesToOmit.Add("IsDuplicateUsername");
            using (SqlDataReader reader = Degatech.Utilities.SQL.ExecutionHelper.ExecuteReader("up_InsertUpdate_Registration", GetSQLParameters(propertiesToOmit)))
            {
                if (reader == null)
                    return;
                if (reader.Read())
                    SetProperties(reader);
            }
        }

        public void CheckUsername()
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Id", Id));
            parameters.Add(new SqlParameter("@Username", Username));
            parameters.Add(new SqlParameter("@AdminClientID", VFMClasses.Users.CurrentUser.Client.Id));
            using (SqlDataReader reader = Degatech.Utilities.SQL.ExecutionHelper.ExecuteReader("up_CheckUsername", parameters))
            {
                if (reader == null)
                    return;
                if (reader.Read())
                    SetProperties(reader);
            }
        }
        #endregion

        #region Static Methods
        public static RegistrationCollection LoadList()
        {
            RegistrationCollection collection = new RegistrationCollection();
            collection.LoadAll();
            return collection;
        }

        public static Registration GetRegistration(int id)
        {
            return new Registration(id);
        }

        public static RegistrationCollection DeleteRegistration(int id)
        {
            RegistrationCollection collection = new RegistrationCollection();
            collection.Delete(id);
            return collection;
        }
        #endregion


    }
    #endregion

    #region Collection
    public class RegistrationCollection : List<Registration>
    {
        #region Public Methods
        public void LoadAll()
        {
            using (SqlDataReader reader = Degatech.Utilities.SQL.ExecutionHelper.ExecuteReader("up_Select_RegistrationsAll"))
            {
                if (reader == null)
                    return;
                while (reader.Read())
                {
                    Registration reg = new Registration();
                    reg.SetProperties(reader);
                    Add(reg);
                }
            }
        }

        public void Delete(int id)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Id", id));
            Degatech.Utilities.SQL.ExecutionHelper.ExecuteNonQuery("up_Delete_Registration", parameters);
        }

        public Registration GetId(int id)
        {
            return this.FirstOrDefault(reg => reg.Id == id);
        }

        public Registration GetFirstName(string firstName)
        {
            return this.FirstOrDefault(reg => reg.FirstName == firstName);
        }

        public Registration GetLastName(string lastName)
        {
            return this.FirstOrDefault(reg => reg.LastName == lastName);
        }

        public Registration GetCompany(string company)
        {
            return this.FirstOrDefault(reg => reg.Company == company);
        }

        public Registration GetUsername(string username)
        {
            return this.FirstOrDefault(reg => reg.Username == username);
        }

        public Registration GetEmail(string email)
        {
            return this.FirstOrDefault(reg => reg.Email == email);
        }

        public Registration GetPassword(string password)
        {
            return this.FirstOrDefault(reg => reg.Password == password);
        }

        public Registration GetDateAdded(DateTime date)
        {
            return this.FirstOrDefault(reg => reg.DateAdded == date);
        }
        #endregion
    }
    #endregion
}


