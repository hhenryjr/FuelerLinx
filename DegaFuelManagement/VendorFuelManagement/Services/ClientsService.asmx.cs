using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using VFMClasses;

namespace VendorFuelManagement.Services
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
        public int UpdateClient(object clientOBJ)
        {
            Clients client = new Clients();
            client.SetProperties((Dictionary<string, object>) clientOBJ);
            client.Update();
            return client.Id;
        }

        [WebMethod(EnableSession = true)]
        public Clients GetClient(int id)
        {
            return Clients.GetClient(id);
        }

        [WebMethod(EnableSession = true)]
        public static Clients GetDetailedClientInfo(int Id)
        {
            Clients client = new Clients(Id);
            client.Contacts = new ContactsCollection();
            //client.Contacts.LoadByClientID(Id);
            client.Users = new UsersCollection();
            client.Users.LoadByClientID(Id);
            return client;
        }

        [WebMethod(EnableSession = true)]
        public static List<Clients> GetListOfClients()
        {
            return Clients.LoadList();
        }

        [WebMethod(EnableSession = true)]
        public void DeleteClient(int id)
        {
            Clients.DeleteClient(id);
        }
        #endregion
    }
}
