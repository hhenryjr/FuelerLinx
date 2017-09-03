using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using VFMClasses;

namespace VendorFuelManagement.Services
{
    /// <summary>
    /// Summary description for Contacts
    /// </summary>
    [WebService(Namespace = "http://www.fuelerlinx.com")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class ContactsService : VFMWebService
    {
        #region Web Methods
        [WebMethod(EnableSession = true)]
        public int UpdateContact(object contactOBJ)
        {
            Contacts contact = new Contacts();
            contact.SetProperties((Dictionary<string, object>)contactOBJ);
            contact.Update();
            return contact.Id;
        }

        [WebMethod(EnableSession = true)]
        public Contacts GetContact(int id)
        {
            return Contacts.GetContact(id);
        }

        [WebMethod(EnableSession = true)]
        public List<Contacts> GetListOfContacts()
        {
            return Contacts.LoadList();
        }

        [WebMethod(EnableSession = true)]
        public void DeleteContact(int id)
        {
            Contacts.DeleteContact(id);
        }
        #endregion
    }
}
