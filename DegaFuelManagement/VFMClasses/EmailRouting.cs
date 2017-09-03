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
    #region EmailRouting
    /// <summary>
    /// This object represents the properties and methods of a EmailRouting.
    /// </summary>
    public class EmailRouting : BaseClass
    {
        #region Properties
        public int Id { get; set; }
        public int AdminClientID { get; set; }
        public string EmailType { get; set; }
        public string FromEmail { get; set; } = String.Empty;
        public string ToEmail { get; set; } = String.Empty;
        public string BCCEmail { get; set; } = String.Empty;
        public string DispatchContactEmail { get; set; } = String.Empty;
        public string Subject { get; set; } = String.Empty;
        public DateTime DateAdded { get; set; }
        #endregion

        #region Constructors
        public EmailRouting()
        {
        }

        public EmailRouting(int id)
        {
            Id = id;
            Load();
        }

        public EmailRouting(SqlDataReader reader)
        {
            LoadFromReader(reader);
        }
        #endregion

        #region Public Methods
        public void Load()
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Id", Id));
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Select_EmailRouting", parameters))
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
            DeleteEmail(Id);
        }

        public void Update()
        {
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_InsertUpdate_EmailRouting", GetSQLParameters()))
            {
                if (reader == null)
                    return;
                if (reader.Read())
                    SetProperties(reader);
            }
        }
        #endregion

        #region Static Methods
        public static EmailRoutingCollection LoadList()
        {
            EmailRoutingCollection collection = new EmailRoutingCollection();
            collection.LoadAll();
            return collection;
        }

        public static EmailRouting GetEmailRouting(int id)
        {
            return new EmailRouting(id);
        }

        public static EmailRoutingCollection GetEmailsByAdminClient(int clientId)
        {
            EmailRoutingCollection collection = new EmailRoutingCollection();
            collection.LoadByAdminClientID(clientId);
            return collection;
        }

        public static EmailRoutingCollection DeleteEmail(int id)
        {
            EmailRoutingCollection collection = new EmailRoutingCollection();
            collection.Delete(id);
            return collection;
        }
        #endregion
    }
    #endregion

    #region Collection
    public class EmailRoutingCollection : List<EmailRouting>
    {

        #region Public Methods
        public void LoadAll()
        {
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Select_EmailRoutingsAll"))
            {
                if (reader == null)
                    return;
                while (reader.Read())
                {
                    EmailRouting email = new EmailRouting();
                    email.SetProperties(reader);
                    Add(email);
                }
            }
        }

        public void LoadByAdminClientID(int adminId)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@AdminClientID", adminId));
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Select_EmailRoutingByAndAdminClientID", parameters))
            {
                if (reader == null)
                    return;
                while (reader.Read())
                {
                    EmailRouting email = new EmailRouting();
                    email.SetProperties(reader);
                    Add(email);
                }
            }
        }
        public void Delete(int id)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Id", id));
            ExecutionHelper.ExecuteNonQuery("up_Delete_EmailRouting", parameters);
        }

        public EmailRouting GetEmailByID(int id)
        {
            return this.FirstOrDefault(email => email.Id == id);
        }

        public EmailRouting GetEmailByAdminClientID(int id)
        {
            return this.FirstOrDefault(email => email.AdminClientID == id);
        }

        public EmailRouting GetEmailType(string type)
        {
            return this.FirstOrDefault(email => email.EmailType == type);
        }

        public EmailRouting GetToEmail(string toEmail)
        {
            return this.FirstOrDefault(email => email.ToEmail == toEmail);
        }

        public EmailRouting GetFromEmail(string fromEmail)
        {
            return this.FirstOrDefault(email => email.FromEmail == fromEmail);
        }

        public EmailRouting GetBCCEmail(string bccEmail)
        {
            return this.FirstOrDefault(email => email.BCCEmail == bccEmail);
        }

        public EmailRouting GetEmailByDateAdded(DateTime date)
        {
            return this.FirstOrDefault(email => email.DateAdded == date);
        }
        #endregion
    }
    #endregion
}