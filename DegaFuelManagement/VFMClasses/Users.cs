using System;
using System.Web;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Degatech.Common;
using Degatech.Utilities.Session;
using VFM.API.EntityServices;
using VFM.EDM;

namespace VFMClasses
{
    #region User
    /// <summary>
    /// This object represents the properties and methods of a User.
    /// </summary>
    public class Users : BaseClass
    {
        #region Private Members
        //private Clients _Client;
        //private Registration _Registration;
        //private FuelOrdersCollection _FuelOrders;
        #endregion

        #region Properties
        public int Id { get; set; }
        public int RegistrationID { get; set; }
        public int ClientID { get; set; }
        public bool IsActive { get; set; }
        public DateTime DateAdded { get; set; }
        public CustomerDetails CustomerDetail { get; set; } = new CustomerDetails();
        public Clients Client { get; set; } = new Clients();
        public Registration Registration { get; set; } = new Registration();
        public LoginPermission Permission { get; set; } = new LoginPermission();
        public string Domain { get { return Clients.GetClientName(); } }
        #endregion

        #region Constructors
        public Users()
        {
        }

        public Users(int id)
        {
            Id = id;
            Load();
        }

        public Users(SqlDataReader reader)
        {
            LoadFromReader(reader);
        }
        #endregion

        #region Public Methods
        public void Load()
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Id", Id));
            using (SqlDataReader reader = Degatech.Utilities.SQL.ExecutionHelper.ExecuteReader("up_Select_User", parameters))
            {
                if (reader == null)
                    return;
                if (reader.Read())
                    SetProperties(reader);
            }
            Client = new Clients(ClientID);
            Registration = new Registration(RegistrationID);
            CustomerDetail = new CustomerDetails(ClientID);
            LoginPermissions Permissions = new LoginPermissions();
            Permission = Permissions.GetByUserID(Id);
        }

        public void LoadByClientID(int clientId)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ClientID", clientId));
            using (SqlDataReader reader = Degatech.Utilities.SQL.ExecutionHelper.ExecuteReader("up_Select_UsersByAndClientID", parameters))
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
            DeleteUserByRegID(Id);
        }

        public void Update()
        {
            using (SqlDataReader reader = Degatech.Utilities.SQL.ExecutionHelper.ExecuteReader("up_InsertUpdate_User", GetSQLParameters()))
            {
                if (reader == null)
                    return;
                if (reader.Read())
                    SetProperties(reader);
            }
        }
        #endregion

        #region Private Methods 
        //private void LoadClientIfNull()
        //{
        //    if (_Client == null && ClientID > 0)
        //        _Client = new Clients(ClientID);
        //}

        //private void LoadRegistrationIfNull()
        //{
        //    if (_Registration == null && RegistrationID > 0)
        //        _Registration = new Registration(RegistrationID);
        //}

        //private void LoadFuelOrdersIfNull()
        //{
        //    if (_FuelOrders == null && Id > 0)
        //    {
        //        _FuelOrders = new FuelOrdersCollection();
        //        _FuelOrders.LoadByUserID(Id);
        //    }
        //}
        #endregion

        #region Static Methods
        public static Users CurrentUser
        {
            get
            {
                if (SessionHelper.Exists("CurrentUser"))
                    return SessionHelper.Get<Users>("CurrentUser");
                return new Users();
            }

            set
            {
                SessionHelper.Add(value, "CurrentUser");
            }
        }

        public static void Logout()
        {
            SessionHelper.Abandon();
        }

        public static UsersCollection LoadList()
        {
            UsersCollection collection = new UsersCollection();
            collection.LoadAll();
            return collection;
        }

        public static UsersCollection LoadListByClient(int clientID)
        {
            UsersCollection collection = new UsersCollection();
            collection.LoadByClientID(clientID);
            return collection;
        }

        public static Users LoadByClient(int clientID)
        {
            Users collection = new Users();
            collection.LoadByClientID(clientID);
            return collection;
        }

        public static UsersCollection LoadListByReg(int regID)
        {
            UsersCollection collection = new UsersCollection();
            collection.LoadByRegID(regID);
            return collection;
        }

        public static Users GetUser(int id)
        {
            return new Users(id);
        }

        public static void DeleteUserByRegID(int regID)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@RegistrationID", regID));
            Degatech.Utilities.SQL.ExecutionHelper.ExecuteNonQuery("up_Delete_UsersByAndRegistrationID", parameters);
        }
        #endregion
    }
    #endregion

    #region Collection
    public class UsersCollection : List<Users>
    {
        #region Public Methods
        public void LoadAll()
        {
            using (SqlDataReader reader = Degatech.Utilities.SQL.ExecutionHelper.ExecuteReader("up_Select_UsersAll"))
            {
                if (reader == null)
                    return;
                while (reader.Read())
                {
                    Users user = new Users();
                    user.SetProperties(reader);
                    Add(user);
                }
            }
        }

        public void LoadByClientID(int clientId)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ClientID", clientId));
            using (SqlDataReader reader = Degatech.Utilities.SQL.ExecutionHelper.ExecuteReader("up_Select_AdminUsersByAndClientID", parameters))
            {
                if (reader == null)
                    return;
                while (reader.Read())
                {
                    Users user = new Users();
                    user.SetProperties(reader);
                    user.Registration = new Registration();
                    user.Registration.SetProperties(reader);
                    Add(user);
                }
            }
        }

        public void LoadByClient(Clients client)
        {
            LoadByClientID(client.Id);
        }

        public void LoadByRegID(int regId)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@RegistrationID", regId));
            using (
                SqlDataReader reader =
                    Degatech.Utilities.SQL.ExecutionHelper.ExecuteReader("up_Select_UsersByAndRegistrationID", parameters))
            {
                if (reader == null)
                    return;
                while (reader.Read())
                {
                    Users user = new Users();
                    user.SetProperties(reader);
                    Add(user);
                }
            }
        }

        public void LoadByReg(Registration reg)
        {
            LoadByRegID(reg.Id);
        }

        public Users GetId(int id)
        {
            return this.FirstOrDefault(user => user.Id == id);
        }

        public Users GetRegID(int regId)
        {
            return this.FirstOrDefault(user => user.RegistrationID == regId);
        }

        public Users GetClientID(int clientId)
        {
            return this.FirstOrDefault(user => user.ClientID == clientId);
        }

        public Users IsActive(bool active)
        {
            return this.FirstOrDefault(user => user.IsActive == active);
        }

        public Users GetDateAdded(DateTime date)
        {
            return this.FirstOrDefault(user => user.DateAdded == date);
        }
        #endregion
    }
    #endregion
}