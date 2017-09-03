using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Degatech.Common;
using Degatech.Utilities.SQL;
using System.Linq;
using System.Collections;

namespace VFMClasses
{
    #region AircraftExclusions
    /// <summary>
    /// This object represents the properties and methods of a AircraftExclusions.
    /// </summary>
    public class AircraftExclusions : BaseClass
    {
        #region Properties
        public int Id { get; set; }
        public int AdminClientID { get; set; }
        public int CustClientID { get; set; }
        public string ICAO { get; set; } = String.Empty;
        public string FBO { get; set; } = String.Empty;
        public int AircraftID { get; set; }
        public string TailNumbers { get; set; }
        public decimal Margin { get; set; }
        public bool IsExcluded { get; set; }
        public Clients Client { get; set; }
        public Aircrafts Aircraft { get; set; }
        public AircraftsCollection aircrafts { get; set; }

        public string[] TailNumberList
        {
            get
            {
                //if (TailNumbers == null)
                //    TailNumbers = String.Empty;
                return TailNumbers.Split(',');
            }
            set
            {
                TailNumbers = Degatech.Utilities.Data.Utilities.ArrayListToCommaDelimited(new ArrayList(value));
            }
        }
        #endregion

        #region Constructors
        public AircraftExclusions()
        {
        }

        public AircraftExclusions(int id)
        {
            Id = id;
            Load();
        }

        public AircraftExclusions(SqlDataReader reader)
        {
            LoadFromReader(reader);
        }
        #endregion

        #region Public Methods
        public void Load()
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Id", Id));
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Select_AircraftExclusion", parameters))
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
            DeleteAircraftExclusion(Id);
        }

        public void Update()
        {
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_InsertUpdate_AircraftExclusion", GetSQLParameters()))
            {
                if (reader == null)
                    return;
                if (reader.Read())
                    SetProperties(reader);
            }
        }
        #endregion

        #region Static Methods
        public static AircraftExclusions GetAircraftExclusion(int id)
        {
            return new AircraftExclusions(id);
        }

        public static AircraftExclusionsCollection LoadList()
        {
            AircraftExclusionsCollection collection = new AircraftExclusionsCollection();
            collection.LoadAll();
            return collection;
        }

        public static AircraftExclusionsCollection LoadAircraftExclusionsByICAOAndFBOAndAdminClient(string icao, string fbo, int clientId)
        {
            AircraftExclusionsCollection collection = new AircraftExclusionsCollection();
            collection.LoadByICAOAndFBOAndAdminClient(icao, fbo, clientId);
            return collection;
        }

        public static AircraftExclusionsCollection DeleteAircraftExclusion(int id)
        {
            AircraftExclusionsCollection collection = new AircraftExclusionsCollection();
            collection.DeleteAircraftExclusion(id);
            return collection;
        }
        #endregion
    }

    #region Collection
    public class AircraftExclusionsCollection : List<AircraftExclusions>
    {
        #region Public Methods


        public void LoadAll()
        {
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Select_AircraftExclusionsAll"))
            {
                if (reader == null)
                    return;
                while (reader.Read())
                {
                    AircraftExclusions exclusion = new AircraftExclusions();
                    exclusion.SetProperties(reader);
                    Add(exclusion);
                }
            }
        }

        public void LoadByICAOAndFBOAndAdminClient(string icao, string fbo, int clientId)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ICAO", icao));
            parameters.Add(new SqlParameter("@FBO", fbo));
            parameters.Add(new SqlParameter("@AdminClientID", clientId));
            using (
                SqlDataReader reader =
                    ExecutionHelper.ExecuteReader("up_Select_AircraftExclusionsByAndICAOAndFBOAndAdminClientID", parameters))
            {
                if (reader == null)
                    return;
                while (reader.Read())
                {
                    AircraftExclusions exclusion = new AircraftExclusions();
                    exclusion.SetProperties(reader);
                    exclusion.Client = new Clients();
                    exclusion.Client.SetProperties(reader);
                    exclusion.Aircraft = new Aircrafts();
                    exclusion.Aircraft.SetProperties(reader);
                    exclusion.Aircraft.MakeAndModel = new AircraftData();
                    exclusion.Aircraft.MakeAndModel.SetProperties(reader);
                    exclusion.aircrafts = new AircraftsCollection();
                    exclusion.aircrafts.LoadByCustClientID(exclusion.CustClientID);
                    Add(exclusion);
                }
            }
        }

        public void DeleteAircraftExclusion(int id)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Id", id));
            ExecutionHelper.ExecuteNonQuery("up_Delete_AircraftExclusion", parameters);
        }

        public AircraftExclusions GetId(int id)
        {
            return this.FirstOrDefault(exclusion => exclusion.Id == id);
        }

        public AircraftExclusions GetAdminClientID(int clientId)
        {
            return this.FirstOrDefault(exclusion => exclusion.AdminClientID == clientId);
        }

        public AircraftExclusions GetCustClientID(int clientId)
        {
            return this.FirstOrDefault(exclusion => exclusion.CustClientID == clientId);
        }

        public AircraftExclusions GetICAO(string icao)
        {
            return this.FirstOrDefault(exclusion => exclusion.ICAO == icao);
        }

        public AircraftExclusions GetFBO(string fbo)
        {
            return this.FirstOrDefault(exclusion => exclusion.FBO == fbo);
        }

        public AircraftExclusions GetAircraftID(int exclusionID)
        {
            return this.FirstOrDefault(exclusion => exclusion.AircraftID == exclusionID);
        }
        #endregion
    }
    #endregion

    #endregion
}


