using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Degatech.Common;

namespace VFMClasses.Calendar
{
    public class UserCalendarEvent : CalendarEventBase
    {
        #region Properties
        public int ClientID { get; set; }
        public int UserID { get; set; }
        #endregion
    }
}
