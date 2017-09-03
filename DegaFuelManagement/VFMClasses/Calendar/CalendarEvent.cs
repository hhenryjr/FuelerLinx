using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using Degatech.Utilities.SQL;

namespace VFMClasses.Calendar
{
    public class CalendarEvent : UserCalendarEvent
    {
        #region Properties  
        public DateTime StartDate { get; set; } = DatabaseDateTimeMinValue();
        public DateTime EndDate { get; set; } = DatabaseDateTimeMinValue();
        public bool IsComplete { get; set; }

        //Properties used to better set/get for javascript
        public string start
        {
            get { return StartDate.ToShortDateString() + " " + StartDate.ToShortTimeString(); }
            set { StartDate = DateTime.Parse(value); }
        }

        public string end
        {
            get { return EndDate.ToShortDateString() + " " + EndDate.ToShortTimeString(); }
            set { EndDate = DateTime.Parse(value); }
        }
        #endregion

        #region Public Methods
        public void Load()
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@OID", OID));
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Select_CalendarEvents"))
            {
                if (ConnectionHelper.IsReadingOneRowSuccessful(reader))
                    LoadFromReader(reader);
            }
        }

        public void LoadFromReader(SqlDataReader reader)
        {
            SetProperties(reader);
        }

        public void Update()
        {
            List<SqlParameter> parameters = GetSQLParameters(GetPropertiesToOmitForUpdate());
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_InsertUpdate_CalendarEvents", parameters))
            {
                if (ConnectionHelper.IsReadingOneRowSuccessful(reader))
                    SetProperties(reader);
            }
        }

        public void Delete(int eventId)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@EventID", eventId));
            ExecutionHelper.ExecuteNonQuery("up_Delete_CalendarEvents", parameters);
        }
        #endregion

        #region Private Methods

        private ArrayList GetPropertiesToOmitForUpdate()
        {
            ArrayList propertiesToOmit = new ArrayList();
            propertiesToOmit.Add("start");
            propertiesToOmit.Add("end");
            return propertiesToOmit;
        }
        #endregion
    }

    public class CalendarEventCollection : List<CalendarEvent>
    {
        #region Public Methods
        public void Load(int clientID, int userID)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ClientID", clientID));
            parameters.Add(new SqlParameter("@UserID", userID));
            LoadFromDatabase(parameters);
        }

        public void Load(int clientID, int userID, DateTime startDate, DateTime endDate)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ClientID", clientID));
            parameters.Add(new SqlParameter("@UserID", userID));
            parameters.Add(new SqlParameter("@StartDate", startDate));
            parameters.Add(new SqlParameter("@EndDate", endDate));
            LoadFromDatabase(parameters);
        }

        public void SortByStartDate()
        {
            Sort((x, y) => x.StartDate.CompareTo(y.StartDate));
        }
        #endregion

        #region Private Methods
        private void LoadFromDatabase(List<SqlParameter> parameters)
        {
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Select_CalendarEvents", parameters))
            {
                if (reader == null)
                    return;
                while (reader.Read())
                {
                    CalendarEvent calendarEvent = new CalendarEvent();
                    calendarEvent.LoadFromReader(reader);
                    Add(calendarEvent);
                }
            }
            SortByStartDate();
        }
        #endregion
    }
}