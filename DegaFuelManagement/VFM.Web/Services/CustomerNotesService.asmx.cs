using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using VFMClasses;

namespace VFM.Web.Services
{
    /// <summary>
    /// Summary description for CustomerNotesService
    /// </summary>
    [WebService(Namespace = "http://www.fuelerlinx.com")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class CustomerNotesService : VFMWebService
    {
        #region Web Methods
        [WebMethod(EnableSession = true)]
        public static int UpdateCustomerNote(object noteOBJ)
        {
            CustomerNotes note = new CustomerNotes();
            note.Clone(noteOBJ);
            note.Update();
            return note.Id;
        }

        [WebMethod(EnableSession = true)]
        public static CustomerNotes GetCustomerNote(int id)
        {
            return CustomerNotes.GetCustomerNote(id);
        }

        [WebMethod(EnableSession = true)]
        public static List<CustomerNotes> GetListOfCustomerNotes()
        {
            return CustomerNotes.LoadList();
        }

        [WebMethod(EnableSession = true)]
        public static void DeleteCustomerNote(int id)
        {
            CustomerNotes.DeleteCustomerNote(id);
        }
        #endregion
    }
}
