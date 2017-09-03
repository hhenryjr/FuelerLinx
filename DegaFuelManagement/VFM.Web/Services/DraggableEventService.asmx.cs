using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using VFMClasses.Calendar;

namespace VFM.Web.Services
{
    /// <summary>
    /// Summary description for DraggableEventService
    /// </summary>
    [WebService(Namespace = "http://www.fuelerlinx.com")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class DraggableEventService : VFMWebService
    {
        #region Web Methods

        [WebMethod(EnableSession = true)]
        //public static DraggableEventCollection LoadDraggable()
        //{
        //    DraggableEventCollection cal = new DraggableEventCollection();
        //    cal.Load();
        //    return cal;
        //}
        public static DraggableEvent LoadEvent()
        {
            DraggableEvent cal = new DraggableEvent();
            cal.Load();
            return cal;
        }

        [WebMethod(EnableSession = true)]
        public static DraggableEvent UpdateEvent(object calendar)
        {
            DraggableEvent cal = new DraggableEvent();
            cal.Clone(calendar);
            cal.Update();
            return cal;
        }

        [WebMethod(EnableSession = true)]
        public static DraggableEventCollection Load(int clientID, int userID)
        {
            DraggableEventCollection cal = new DraggableEventCollection();
            cal.Load(clientID, userID);
            return cal;
        }

        [WebMethod(EnableSession = true)]
        public static void Delete(int eventId)
        {
            DraggableEvent cal = new DraggableEvent();
            cal.Delete(eventId);
        }
        #endregion
    }
}
