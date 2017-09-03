using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Degatech.Common;
using Degatech.Utilities.SQL;
using System.Linq;

namespace VFMClasses
{
    #region Aircrafts
    /// <summary>
    /// This object represents the properties and methods of a Aircrafts.
    /// </summary>
    public class Aircrafts : BaseClass
    {
        #region Properties
        public int Id { get; set; }
        public int MakeModelID { get; set; }
        public int AdminClientID { get; set; }
        public int CustClientID { get; set; }
        public string TailNumber { get; set; } = String.Empty;
        public string AccountNumber { get; set; } = String.Empty;
        public string CardNumber { get; set; } = String.Empty;
        public bool IsMarginEnabled { get; set; }
        public double Margin { get; set; }
        public DateTime DateAdded { get; set; }
        public AircraftData MakeAndModel { get; set; }
        #endregion

        #region Constructors
        public Aircrafts()
        {
        }

        public Aircrafts(int id)
        {
            Id = id;
            Load();
        }

        public Aircrafts(SqlDataReader reader)
        {
            LoadFromReader(reader);
        }
        #endregion

        #region Public Methods
        public void Load()
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Id", Id));
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Select_Aircraft", parameters))
            {
                if (reader == null)
                    return;
                if (reader.Read())
                    SetProperties(reader);
            }
        }

        protected void LoadFromReader(SqlDataReader reader)
        {
            SetProperties(reader);
        }

        public void Delete()
        {
            DeleteAircraft(Id);
        }

        public void Update()
        {
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_InsertUpdate_Aircraft", GetSQLParameters()))
            {
                if (reader == null)
                    return;
                if (reader.Read())
                    SetProperties(reader);
            }
        }
        #endregion

        #region Static Methods
        public static Aircrafts GetAircraft(int id)
        {
            return new Aircrafts(id);
        }

        public static AircraftsCollection LoadList()
        {
            AircraftsCollection collection = new AircraftsCollection();
            collection.LoadAll();
            return collection;
        }

        public static AircraftsCollection LoadAircraftsByClient(int id)
        {
            AircraftsCollection collection = new AircraftsCollection();
            collection.LoadByClientID(id);
            return collection;
        }

        public static AircraftsCollection LoadAircraftsByAdminAndClient(int adminId, int custId)
        {
            AircraftsCollection collection = new AircraftsCollection();
            collection.LoadByAdminAndCustClientID(adminId, custId);
            return collection;
        }

        public static AircraftsCollection LoadAircraftsByCustClient(int custId)
        {
            AircraftsCollection collection = new AircraftsCollection();
            collection.LoadByCustClientID(custId);
            return collection;
        }

        public static AircraftsCollection DeleteAircraft(int id)
        {
            AircraftsCollection collection = new AircraftsCollection();
            collection.DeleteAircraft(id);
            return collection;
        }
        #endregion
    }

    #region Collection
    public class AircraftsCollection : List<Aircrafts>
    {
        #region Public Methods


        public void LoadAll()
        {
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Select_AircraftsAll"))
            {
                if (reader == null)
                    return;
                while (reader.Read())
                {
                    Aircrafts aircraft = new Aircrafts();
                    aircraft.SetProperties(reader);
                    Add(aircraft);
                }
            }
        }

        public void LoadByAdminClientID(int clientId)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@AdminClientID", clientId));
            using (
                SqlDataReader reader =
                    ExecutionHelper.ExecuteReader("up_Select_AircraftsByAndAdminClientID", parameters))
            {
                if (reader == null)
                    return;
                while (reader.Read())
                {
                    Aircrafts aircraft = new Aircrafts();
                    aircraft.SetProperties(reader);
                    Add(aircraft);
                }
            }
        }

        public void LoadByCustClientID(int clientId)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@CustClientID", clientId));
            using (
                SqlDataReader reader =
                    ExecutionHelper.ExecuteReader("up_Select_AircraftsByAndCustClientID", parameters))
            {
                if (reader == null)
                    return;
                while (reader.Read())
                {
                    Aircrafts aircraft = new Aircrafts();
                    aircraft.SetProperties(reader);
                    aircraft.MakeAndModel = new AircraftData();
                    aircraft.MakeAndModel.SetProperties(reader);
                    Add(aircraft);
                }
            }
        }

        public void LoadByAdminAndCustClientID(int adminId, int custId)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@AdminClientID", adminId));
            parameters.Add(new SqlParameter("@CustClientID", custId));
            using (
                SqlDataReader reader =
                    ExecutionHelper.ExecuteReader("up_Select_AircraftsByAndAdminAndCustClientID", parameters))
            {
                if (reader == null)
                    return;
                while (reader.Read())
                {
                    Aircrafts aircraft = new Aircrafts();
                    aircraft.SetProperties(reader);
                    Add(aircraft);
                }
            }
        }

        public void LoadByClientID(int clientId)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@CustClientID", clientId));
            using (
                SqlDataReader reader =
                    ExecutionHelper.ExecuteReader("up_Select_AircraftAndAircraftDataByClientID", parameters))
            {
                if (reader == null)
                    return;
                while (reader.Read())
                {
                    Aircrafts aircraft = new Aircrafts();
                    aircraft.SetProperties(reader);
                    aircraft.MakeAndModel = new AircraftData(/*aircraft.MakeModelID*/);
                    aircraft.MakeAndModel.SetProperties(reader);
                    Add(aircraft);
                }
            }
        }

        public void LoadRemainingByClientID(int adminId, int clientId, string fbo, string icao)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ICAO", icao));
            parameters.Add(new SqlParameter("@FBO", fbo));
            parameters.Add(new SqlParameter("@AdminClientID", adminId));
            parameters.Add(new SqlParameter("@CustClientID", clientId));
            using (
                SqlDataReader reader =
                    ExecutionHelper.ExecuteReader("up_Select_Aircrafts_RemainingByCustClientID", parameters))
            {
                if (reader == null)
                    return;
                while (reader.Read())
                {
                    Aircrafts aircraft = new Aircrafts();
                    aircraft.SetProperties(reader);
                    aircraft.MakeAndModel = new AircraftData(/*aircraft.MakeModelID*/);
                    aircraft.MakeAndModel.SetProperties(reader);
                    Add(aircraft);
                }
            }
        }

        public void LoadByMakeModelID(int makeModelId)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@MakeModelID", makeModelId));
            using (
                SqlDataReader reader =
                    ExecutionHelper.ExecuteReader("up_Select_AircraftsByAndMakeModelID", parameters))
            {
                if (reader == null)
                    return;
                while (reader.Read())
                {
                    Aircrafts aircraft = new Aircrafts();
                    aircraft.SetProperties(reader);
                    Add(aircraft);
                }
            }
        }

        public void DeleteAircraft(int id)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Id", id));
            ExecutionHelper.ExecuteNonQuery("up_Delete_Aircraft", parameters);
        }

        public Aircrafts GetId(int id)
        {
            return this.FirstOrDefault(aircraft => aircraft.Id == id);
        }

        public Aircrafts GetMakeModelID(int makeModelID)
        {
            return this.FirstOrDefault(aircraft => aircraft.MakeModelID == makeModelID);
        }

        public Aircrafts GetAdminClientID(int clientId)
        {
            return this.FirstOrDefault(aircraft => aircraft.AdminClientID == clientId);
        }

        public Aircrafts GetCustClientID(int clientId)
        {
            return this.FirstOrDefault(aircraft => aircraft.CustClientID == clientId);
        }

        public Aircrafts GetTailNumber(string tailNumber)
        {
            return this.FirstOrDefault(aircraft => aircraft.TailNumber == tailNumber);
        }

        public Aircrafts GetAccountNumber(string number)
        {
            return this.FirstOrDefault(aircraft => aircraft.AccountNumber == number);
        }

        public Aircrafts GetCardNumber(string number)
        {
            return this.FirstOrDefault(aircraft => aircraft.CardNumber == number);
        }

        public Aircrafts GetIsMarginEnabled(bool enabled)
        {
            return this.FirstOrDefault(aircraft => aircraft.IsMarginEnabled == enabled);
        }

        public Aircrafts GetMargin(double margin)
        {
            return this.FirstOrDefault(aircraft => aircraft.Margin == margin);
        }

        public Aircrafts GetDateAdded(DateTime date)
        {
            return this.FirstOrDefault(aircraft => aircraft.DateAdded == date);
        }
        #endregion
    }
    #endregion

    #endregion
}


