using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using VFMClasses.Calendar;

namespace VFM.Web.Services
{
    /// <summary>
    /// Summary description for CalendarEventService
    /// </summary>
    [WebService(Namespace = "http://www.fuelerlinx.com")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class CalendarEventService : VFMWebService
    {
        #region Web Methods

        [WebMethod(EnableSession = true)]
        //public static CalendarEventCollection LoadCalendar()
        //{
        //    CalendarEventCollection cal = new CalendarEventCollection();
        //    cal.Load();
        //    return cal;
        //}
        public static CalendarEvent LoadCalendar()
        {
            CalendarEvent cal = new CalendarEvent();
            cal.Load();
            return cal;
        }

        [WebMethod(EnableSession = true)]
        public static CalendarEvent UpdateCalendar(object calendar)
        {
            CalendarEvent cal = new CalendarEvent();
            cal.Clone(calendar);
            cal.Update();
            return cal;
        }

        [WebMethod(EnableSession = true)]
        public static CalendarEventCollection Load(int clientID, int userID)
        {
            CalendarEventCollection cal = new CalendarEventCollection();
            cal.Load(clientID, userID);
            return cal;
        }

        [WebMethod(EnableSession = true)]
        public static CalendarEventCollection Load(int clientID, int userID, DateTime startDate, DateTime endDate)
        {
            CalendarEventCollection cal = new CalendarEventCollection();
            cal.Load(clientID, userID, startDate, endDate);
            return cal;
        }

        [WebMethod(EnableSession = true)]
        public static CalendarEventCollection SortByDate()
        {
            CalendarEventCollection cal = new CalendarEventCollection();
            cal.SortByStartDate();
            return cal;
        }

        [WebMethod(EnableSession = true)]
        public static void Delete(int eventId)
        {
            CalendarEvent cal = new CalendarEvent();
            cal.Delete(eventId);
        }
        #endregion
    }
}
