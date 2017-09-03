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
    #region AircraftData
    /// <summary>
    /// This object represents the properties and methods of a AircraftData.
    /// </summary>
    public class AircraftData : BaseClass
    {
        #region Properties
        public int AircraftID { get; set; }
        public string Make { get; set; } = String.Empty;
        public string Model { get; set; } = String.Empty;
        public AircraftSizes Size { get; set; }
        public string SizeName
        {
            get { return (Enum.GetName(typeof(AircraftSizes), (int)Size)); }
        }
        #endregion

        #region Constructors
        public AircraftData()
        {
        }

        public AircraftData(int id)
        {
            AircraftID = id;
            Load();
        }

        public AircraftData(SqlDataReader reader)
        {
            LoadFromReader(reader);
        }
        #endregion

        #region Public Methods
        public void Load()
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@AircraftID", AircraftID));
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Select_AircraftDatum", parameters))
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
            DeleteAircraftData(AircraftID);
        }

        public void Update()
        {
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_InsertUpdate_AircraftDatum", GetSQLParameters()))
            {
                if (reader == null)
                    return;
                if (reader.Read())
                    SetProperties(reader);
            }
        }
        #endregion

        #region Static Methods
        public static AircraftDataCollection LoadList()
        {
            AircraftDataCollection collection = new AircraftDataCollection();
            collection.LoadAll();
            return collection;
        }
        public static AircraftData GetAircraftData(int id)
        {
            return new AircraftData(id);
        }

        public static AircraftDataCollection DeleteAircraftData(int id)
        {
            AircraftDataCollection collection = new AircraftDataCollection();
            collection.Delete(id);
            return collection;
        }
        #endregion
    }
    #endregion

    #region Collection
    public class AircraftDataCollection : List<AircraftData>
    {
        public void LoadAll()
        {
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Select_AircraftDatasAll"))
            {
                if (reader == null)
                    return;
                while (reader.Read())
                {
                    AircraftData aircraft = new AircraftData();
                    aircraft.SetProperties(reader);
                    Add(aircraft);
                }
            }
        }

        public void Delete(int id)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@AircraftID", id));
            ExecutionHelper.ExecuteNonQuery("up_Delete_AircraftDatum", parameters);
        }

        public AircraftData GetAircraftID(int id)
        {
            return this.FirstOrDefault(aircraft => aircraft.AircraftID == id);
        }

        public AircraftData GetMake(string make)
        {
            return this.FirstOrDefault(aircraft => aircraft.Make == make);
        }

        public AircraftData GetModel(string model)
        {
            return this.FirstOrDefault(aircraft => aircraft.Model == model);
        }

        public AircraftData GetSize(BaseClass.AircraftSizes size)
        {
            return this.FirstOrDefault(aircraft => aircraft.Size == size);
        }

        public AircraftData GetSizeName(string size)
        {
            return this.FirstOrDefault(aircraft => aircraft.SizeName == size);
        }
    }
    #endregion
}


