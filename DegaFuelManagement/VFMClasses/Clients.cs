using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Degatech.Common;
using Degatech.Utilities.SQL;

namespace VFMClasses
{
    #region Clients
    /// <summary>
    /// This object represents the properties and methods of a Client.
    /// </summary>
    public class Clients : BaseClass
    {
        #region Properties
        public int Id { get; set; }
        public string Name { get; set; }
        public ClientTypes ClientType { get; set; }
        public Guid Credentials { get; set; } = Guid.Empty;
        public DateTime DateAdded { get; set; }
        public ContactsCollection Contacts { get; set; }
        public UsersCollection Users { get; set; }
        //public ClientNotesCollection ClientNotes { get; set; }
        //public ContactNotesCollection ContactNotes { get; set; }
        public AircraftsCollection Aircrafts { get; set; }
        public FuelOrdersCollection FuelOrders { get; set; }
        public CustomerDetailsCollection Customers { get; set; }
        public PriceMarginsCollection PriceMargins { get; set; }
        #endregion

        #region Constructors
        public Clients()
        {
        }

        public Clients(int id)
        {
            Id = id;
            Load();
        }

        public Clients(SqlDataReader reader)
        {
            LoadFromReader(reader);
        }

        #endregion

        #region Public Methods
        public void Load()
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Id", Id));
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Select_Client", parameters))
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
            DeleteClient(Id);
        }

        public void Update()
        {
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_InsertUpdate_Client", GetSQLParameters()))
            {
                if (reader == null)
                    return;
                if (reader.Read())
                    SetProperties(reader);
            }
        }

        #endregion

        #region Static Methods
        public static ClientsCollection LoadList()
        {
            ClientsCollection collection = new ClientsCollection();
            collection.LoadAll();
            return collection;
        }

        public static Clients GetClient(int id)
        {
            return new Clients(id);
        }

        public static ClientsCollection DeleteClient(int id)
        {
            ClientsCollection collection = new ClientsCollection();
            collection.Delete(id);
            return collection;
        }

        public static string GetClientName()
        {
            Clients client = new Clients();
            List<SqlParameter> Params = new List<SqlParameter>();
            SqlParameter Param = new SqlParameter("@Domain", Degatech.Utilities.Data.Utilities.Domain());
            Params.Add(Param);
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Select_ClientNameByDomain", Params))
            {
                if (!ConnectionHelper.IsReadingOneRowSuccessful(reader))
                    return "";
                client.SetProperties(reader);
            }
            return client.Name;
        }
        #endregion
    }
    #endregion

    #region Collection
    public class ClientsCollection : List<Clients>
    {

        #region Public Methods
        public void LoadAll()
        {
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Select_ClientsAll"))
            {
                if (reader == null)
                    return;
                while (reader.Read())
                {
                    Clients client = new Clients();
                    client.SetProperties(reader);
                    Add(client);
                }
            }
        }

        public void Delete(int id)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Id", id));
            ExecutionHelper.ExecuteNonQuery("up_Delete_Client", parameters);
        }

        public Clients GetClientByID(int id)
        {
            return this.FirstOrDefault(client => client.Id == id);
        }

        public Clients GetClientType(BaseClass.ClientTypes type)
        {
            return this.FirstOrDefault(client => client.ClientType == type);
        }

        public Clients GetClientByCredentials(Guid cred)
        {
            return this.FirstOrDefault(client => client.Credentials == cred);
        }

        public Clients GetClientByDateAdded(DateTime date)
        {
            return this.FirstOrDefault(client => client.DateAdded == date);
        }
        #endregion
    }
    #endregion
}
