using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using VFM.Web.Services;
using VFMClasses;

namespace VFM.Web.Services
{
    /// <summary>
    /// Summary description for Contacts
    /// </summary>
    [WebService(Namespace = "http://www.fuelerlinx.com")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class ContactsService : VFMWebService
    {
        #region Web Methods
        [WebMethod(EnableSession = true)]
        public static int UpdateContact(object contactOBJ)
        {
            Contacts contact = new Contacts();
            contact.Clone(contactOBJ);
            contact.Update();
            return contact.Id;
        }

        [WebMethod(EnableSession = true)]
        public static Contacts GetContact(int id)
        {
            return Contacts.GetContact(id);
        }

        [WebMethod(EnableSession = true)]
        public static Contacts GetContactInfo(int id)
        {
            Contacts contact = new Contacts(id);
            contact.Notes = new ContactNotesCollection();
            contact.Notes.LoadByContactID(id);
            contact.CustomFields = new ContactDetailCustomFieldsCollection();
            contact.CustomFields.GetCustomFields(id);
            return contact;
        }

        [WebMethod(EnableSession = true)]
        public static List<Contacts> GetListOfContacts()
        {
            return Contacts.LoadList();
        }

        [WebMethod(EnableSession = true)]
        public static ContactsCollection GetContactsByAdminClientID(int clientId)
        {
            ContactsCollection contact = new ContactsCollection();
            contact.LoadByAdminClientID(clientId);
            return contact;
        }

        [WebMethod(EnableSession = true)]
        public static ContactsCollection GetContactsByCustClientID(int clientId)
        {
            ContactsCollection contact = new ContactsCollection();
            contact.LoadByCustClientID(clientId);
            return contact;
        }

        [WebMethod(EnableSession = true)]
        public static void DeleteContact(int id)
        {
            Contacts.DeleteContact(id);
        }
        #endregion
    }
}
