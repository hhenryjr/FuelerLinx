using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using VFMClasses;

namespace VFM.Web.Services
{
    /// <summary>
    /// Summary description for ContactNotesService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class ContactNotesService : VFMWebService
    {
        #region Web Methods
        [WebMethod(EnableSession = true)]
        public static int UpdateContactNote(object noteOBJ)
        {
            ContactNotes note = new ContactNotes();
            note.Clone(noteOBJ);
            note.Update();
            return note.Id;
        }

        [WebMethod(EnableSession = true)]
        public static ContactNotes GetContactNote(int id)
        {
            return ContactNotes.GetContactNote(id);
        }

        [WebMethod(EnableSession = true)]
        public static List<ContactNotes> GetListOfContactNotes()
        {
            return ContactNotes.LoadList();
        }

        [WebMethod(EnableSession = true)]
        public static void DeleteContactNote(int id)
        {
            ContactNotes.DeleteContactNote(id);
        }
        #endregion
    }
}
