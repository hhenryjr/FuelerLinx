using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Instrumentation;
using System.Web;
using System.Web.Services;
using VFM.Web.Services;
using VFMClasses;

namespace VFM.Web.Services
{
    /// <summary>
    /// Summary description for Clients
    /// </summary>
    [WebService(Namespace = "http://www.fuelerlinx.com")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class ClientsService : VFMWebService
    {
        #region Web Methods
        [WebMethod(EnableSession = true)]
        public static int UpdateClient(object clientOBJ)
        {
            Clients client = new Clients();
            client.Clone(clientOBJ);
            client.Update();
            return client.Id;
        }

        [WebMethod(EnableSession = true)]
        public static Clients GetClient(int id)
        {
            return Clients.GetClient(id);
        }

        [WebMethod(EnableSession = true)]
        public static Clients GetDetailedClientInfo(int Id)
        {
            Clients client = new Clients(Id);
            //client.Contacts = new ContactsCollection();
            //client.Contacts.LoadByClientID(Id);
            //client.Users = new UsersCollection();
            //client.Users.LoadByClientID(Id);
            //client.ClientNotes = new ClientNotesCollection();
            //client.ClientNotes.LoadByClient(client);
            //client.ContactNotes = new ContactNotesCollection();
            //client.ContactNotes.LoadByClient(client);
            client.FuelOrders = new FuelOrdersCollection();
            client.FuelOrders.LoadByClient(client);
            client.Aircrafts = new AircraftsCollection();
            client.Aircrafts.LoadByClientID(Id);
            client.Customers = new CustomerDetailsCollection();
            client.Customers.LoadByAdminClientID(Id);
            client.PriceMargins = new PriceMarginsCollection();
            client.PriceMargins.LoadByAdminID(Id);
            return client;
        }

        //[WebMethod(EnableSession = true)]
        //public static Clients GetClientNotes(int adminClientId, int custClientId)
        //{
        //    Clients client = new Clients(adminClientId);
        //    //client.Contacts = new ContactsCollection();
        //    //client.Contacts.LoadByClientID(adminClientId);
        //    client.ClientNotes = new ClientNotesCollection();
        //    client.ClientNotes.LoadByAdminClientIDAndCustClientID(adminClientId, custClientId);
        //    //client.ContactNotes = new ContactNotesCollection();
        //    //client.ContactNotes.LoadByClient(client);
        //    return client;
        //}

        //[WebMethod(EnableSession = true)]
        //public static Clients GetContactNotes(int adminClientId, int custClientId, int contactId)
        //{
        //    Clients client = new Clients(adminClientId);
        //    //client.Contacts = new ContactsCollection();
        //    //client.Contacts.LoadByClientID(adminClientId);
        //    //client.ClientNotes = new ClientNotesCollection();
        //    //client.ClientNotes.LoadByAdminClientIDAndCustClientID(adminClientId, custClientId);
        //    client.ContactNotes = new ContactNotesCollection();
        //    client.ContactNotes.LoadByAdminClientAndCustClientAndContactID(adminClientId, custClientId, contactId);
        //    return client;
        //}
        
        [WebMethod(EnableSession = true)]
        public static List<Clients> GetListOfClients()
        {
            return Clients.LoadList();
        }

        [WebMethod(EnableSession = true)]
        public static void DeleteClient(int id)
        {
            Clients.DeleteClient(id);
        }
        #endregion
    }
}
