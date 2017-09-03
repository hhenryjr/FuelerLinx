using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Degatech.Common;
using Degatech.Utilities.Session;
using Degatech.Utilities.SQL;

namespace VFMClasses
{
    #region Login
    /// <summary>
    /// This object represents the properties and methods of a Login.
    /// </summary>
    public class Logins : BaseClass
    {
        #region Private Members
        private string _LoginAttemptsKey = "LoginAttempts";
        #endregion

        #region Properties
        public int Id { get; set; }
        public string Username { get; set; } = String.Empty;
        public string Password { get; set; } = String.Empty;
        public int Attempts { get; set; }
        public LoginResults LoginResult { get; set; }
        public DateTime DateLoggedIn { get; set; }
        #endregion

        #region Constructors
        public Logins()
        {
        }

        public Logins(int id)
        {
            Id = id;
            Load();
        }

        public Logins(SqlDataReader reader)
        {
            LoadFromReader(reader);
        }
        #endregion

        #region Public Methods
        public void AttemptLogin(string username, string password)
        {
            Username = username;
            Password = password;
            Attempts = 1;
            DateLoggedIn = DateTime.Now;
            Users.CurrentUser = AuthenticateUser();
            SetLoginResultForCurrentUser();
            LogAttempt();
        }

        public void Load()
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Id", Id));
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Select_Login", parameters))
            {
                if (reader == null)
                    return;
                if (reader.Read())
                    SetProperties(reader);
            }
        }

        //public void LoadList()
        //{
        //    using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Select_Login"))
        //    {
        //        if (reader == null)
        //            return;
        //        while (reader.Read())
        //        {
        //            Logins obj = new Logins();
        //            obj.SetProperties(reader);
        //        }
        //    }
        //}

        protected void LoadFromReader(SqlDataReader reader)
        {
            SetProperties(reader);
        }

        public void Delete()
        {
            Delete(Id);
        }

        //public void Update()
        //{
        //    using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_InsertUpdate_Login", GetSQLParameters()))
        //    {
        //        if (reader == null)
        //            return;
        //        if (reader.Read())
        //            SetProperties(reader);
        //    }
        //}
        #endregion

        #region Private Methods
        private Users AuthenticateUser()
        {
            Users result = new Users();
            List<SqlParameter> Params = new List<SqlParameter>();
            SqlParameter Param = new SqlParameter("@Username", Username);
            Params.Add(Param);
            Param = new SqlParameter("@Password", Password);
            Params.Add(Param);
            Param = new SqlParameter("@Domain", Degatech.Utilities.Data.Utilities.Domain());
            Params.Add(Param);
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Select_LoginByUsernameAndPassword", Params))
            {
                if (!ConnectionHelper.IsReadingOneRowSuccessful(reader))
                    return result;
                result.SetProperties(reader);
            }
            if (result.Id == 0)
                return result;
            result.Client = Clients.GetClient(result.ClientID);
            result.Registration = Registration.GetRegistration(result.RegistrationID);
            return result;
        }

        private void LogAttempt()
        {
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_InsertUpdate_Login", GetSQLParameters()))
            {
                if (ConnectionHelper.IsReadingOneRowSuccessful(reader))
                    SetProperties(reader);
            }
        }

        private void SetLoginResultForCurrentUser()
        {
            if (Users.CurrentUser.IsActive)
                LoginResult = LoginResults.Success;
            else if (Users.CurrentUser.ClientID == 0)
                LoginResult = LoginResults.InvalidUsernamePassword;
            else if (!Users.CurrentUser.IsActive)
                LoginResult = LoginResults.InactiveAccount;
        }
        #endregion

        #region Static Methods
        public static Logins GetLogin(int id)
        {
            return new Logins(id);
        }

        public static void Delete(int id)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Id", id));
            ExecutionHelper.ExecuteNonQuery("up_Delete_Login", parameters);
        }
        #endregion
    }
    #endregion
}


