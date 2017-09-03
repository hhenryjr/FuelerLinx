namespace Degatech.Common.Calendar
{
    public class CalendarEventBase : BaseClass
    {
        #region Properties  
        //Necessary to use lower-case property names to work with the JS FullCalendar library
        public int OID { get; set; }
        public string title { get; set; } = string.Empty;
        public bool allDay { get; set; }
        public string note { get; set; } = string.Empty;
        public string url { get; set; } = string.Empty;
        public string color { get; set; } = string.Empty;
        #endregion

    }
}
