using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Degatech.Utilities.SQL;
using System.Data.SqlClient;
using System.Collections;

namespace VFMClasses.Calendar
{
    public class DraggableEvent : UserCalendarEvent
    {
        #region Public Methods
        public void Load()
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@OID", OID));
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Select_DraggableEvents"))
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
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_InsertUpdate_DraggableEvents", parameters))
            {
                if (ConnectionHelper.IsReadingOneRowSuccessful(reader))
                    SetProperties(reader);
            }
        }

        public void Delete(int eventId)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@EventID", eventId));
            ExecutionHelper.ExecuteNonQuery("up_Delete_DraggableEvents", parameters);
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

    public class DraggableEventCollection : List<DraggableEvent>
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
        #endregion

        #region Private Methods
        private void LoadFromDatabase(List<SqlParameter> parameters)
        {
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Select_DraggableEvents", parameters))
            {
                if (reader == null)
                    return;
                while (reader.Read())
                {
                    DraggableEvent calendarEvent = new DraggableEvent();
                    calendarEvent.LoadFromReader(reader);
                    Add(calendarEvent);
                }
            }
        }
        #endregion
    }

}
