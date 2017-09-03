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
    /// Summary description for FuelOrderNotesService
    /// </summary>
    [WebService(Namespace = "http://www.fuelerlinx.com")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class FuelOrderNotesService : VFMWebService
    {
        #region Web Methods
        [WebMethod(EnableSession = true)]
        public static int UpdateFuelOrderNote(object noteOBJ)
        {
            FuelOrderNotes note = new FuelOrderNotes();
            note.Clone(noteOBJ);
            if (note.AddedByUserID == 0)
            {
                note.AddedByUserID = Users.CurrentUser.Id;
            }
            note.Update();
            return note.Id;
        }

        [WebMethod(EnableSession = true)]
        public static FuelOrderNotes GetFuelOrderNote(int id)
        {
            return FuelOrderNotes.GetFuelOrderNote(id);
        }

        [WebMethod(EnableSession = true)]
        public static List<FuelOrderNotes> GetListOfFuelOrderNotes()
        {
            return FuelOrderNotes.LoadList();
        }

        [WebMethod(EnableSession = true)]
        public static void DeleteFuelOrderNote(int id)
        {
            FuelOrderNotes.DeleteFuelOrderNote(id);
        }
        #endregion
    }
}
