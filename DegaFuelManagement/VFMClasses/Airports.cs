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
    #region Airports
    /// <summary>
    /// This object represents the properties and methods of a Airports.
    /// </summary>
    public class Airports : BaseClass
    {
        #region Properties
        public int Id { get; set; }
        public string ICAO { get; set; } = String.Empty;
        public string IATA { get; set; } = String.Empty;
        public string AirportCity { get; set; } = String.Empty;
        public string AirportName { get; set; } = String.Empty;
        public string FullAirportName { get; set; } = String.Empty;
        public string StateProvAbbv2 { get; set; } = String.Empty;
        public string Country { get; set; } = String.Empty;
        public string FuelType { get; set; } = String.Empty;
        public string RunwayID { get; set; } = String.Empty;
        public string Customs { get; set; } = String.Empty;
        public int RunwayLength { get; set; }
        public int RunwayWidth { get; set; }
        public string AirportOfEntry { get; set; } = String.Empty;
        public int Elevation { get; set; }
        public string Latitude { get; set; } = String.Empty;
        public string Longitude { get; set; } = String.Empty;
        public string IntlTimeZone { get; set; } = String.Empty;
        public string DaylightSavingsYN { get; set; } = String.Empty;
        public string DaylightSavingTime { get; set; } = String.Empty;
        public string TimeZone { get; set; } = String.Empty;
        public string Open24Hours { get; set; } = String.Empty;
        public string ControlTowerHours { get; set; } = String.Empty;
        public string ApproachList { get; set; } = String.Empty;
        public string HandlingMandatory { get; set; } = String.Empty;
        public string SlotsRequired { get; set; } = String.Empty;
        public string Lighting { get; set; } = String.Empty;
        public string Variation { get; set; } = String.Empty;
        public string TowerPhone { get; set; } = String.Empty;
        public double latitude_number { get; set; }
        public double longitude_number { get; set; }
        public double lat { get; set; }
        public double lng { get; set; }
        public string AirportHours { get; set; } = String.Empty;
        #endregion

        #region Constructors
        public Airports()
        {
        }

        public Airports(int id)
        {
            Id = id;
            Load();
        }

        public Airports(SqlDataReader reader)
        {
            LoadFromReader(reader);
        }
        #endregion

        #region Public Methods
        public void Load()
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Id", Id));
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Select_Airport", parameters))
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

        public void Update()
        {
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_InsertUpdate_Airport", GetSQLParameters()))
            {
                if (reader == null)
                    return;
                if (reader.Read())
                    SetProperties(reader);
            }
        }
        #endregion

        #region Static Methods
        public static Airports GetAirport(int id)
        {
            return new Airports(id);
        }

        public static AirportsCollection LoadList()
        {
            AirportsCollection collection = new AirportsCollection();
            collection.LoadAll();
            return collection;
        }

        #endregion
    }

    #region Collection
    public class AirportsCollection : List<Airports>
    {
        #region Public Methods


        public void LoadAll()
        {
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Select_AirportsAll"))
            {
                if (reader == null)
                    return;
                while (reader.Read())
                {
                    Airports airport = new Airports();
                    airport.SetProperties(reader);
                    Add(airport);
                }
            }
        }

        public Airports GetId(int id)
        {
            return this.FirstOrDefault(airport => airport.Id == id);
        }

        public Airports GetICAO(string icao)
        {
            return this.FirstOrDefault(airport => airport.ICAO == icao);
        }

        public Airports GetIATA(string iata)
        {
            return this.FirstOrDefault(airport => airport.IATA == iata);
        }

        public Airports GetAirportCity(string airportCity)
        {
            return this.FirstOrDefault(airport => airport.AirportCity == airportCity);
        }

        public Airports GetAirportName(string airportName)
        {
            return this.FirstOrDefault(airport => airport.AirportName == airportName);
        }

        public Airports GetFullAirportName(string fullAirportName)
        {
            return this.FirstOrDefault(airport => airport.FullAirportName == fullAirportName);
        }

        public Airports GetStateProvAbbv2(string stateProvAbbv2)
        {
            return this.FirstOrDefault(airport => airport.StateProvAbbv2 == stateProvAbbv2);
        }

        public Airports GetCountry(string country)
        {
            return this.FirstOrDefault(airport => airport.Country == country);
        }

        public Airports GetFuelType(string fuelType)
        {
            return this.FirstOrDefault(airport => airport.FuelType == fuelType);
        }

        public Airports GetRunwayID(string runwayID)
        {
            return this.FirstOrDefault(airport => airport.RunwayID == runwayID);
        }

        public Airports GetCustoms(string customs)
        {
            return this.FirstOrDefault(airport => airport.Customs == customs);
        }

        public Airports GetRunwayLength(int runwayLength)
        {
            return this.FirstOrDefault(airport => airport.RunwayLength == runwayLength);
        }

        public Airports GetRunwayWidth(int runwayWidth)
        {
            return this.FirstOrDefault(airport => airport.RunwayWidth == runwayWidth);
        }

        public Airports GetAirportOfEntry(string airportOfEntry)
        {
            return this.FirstOrDefault(airport => airport.AirportOfEntry == airportOfEntry);
        }

        public Airports GetElevation(int elevation)
        {
            return this.FirstOrDefault(airport => airport.Elevation == elevation);
        }

        public Airports GetLatitude(string latitude)
        {
            return this.FirstOrDefault(airport => airport.Latitude == latitude);
        }

        public Airports GetLongitude(string longitude)
        {
            return this.FirstOrDefault(airport => airport.Longitude == longitude);
        }

        public Airports GetIntlTimeZone(string intlTimeZone)
        {
            return this.FirstOrDefault(airport => airport.IntlTimeZone == intlTimeZone);
        }

        public Airports GetDaylightSavingsYN(string daylightSavingsYN)
        {
            return this.FirstOrDefault(airport => airport.DaylightSavingsYN == daylightSavingsYN);
        }

        public Airports GetDaylightSavingTime(string daylightSavingTime)
        {
            return this.FirstOrDefault(airport => airport.DaylightSavingTime == daylightSavingTime);
        }

        public Airports GetTimeZone(string timeZone)
        {
            return this.FirstOrDefault(airport => airport.TimeZone == timeZone);
        }

        public Airports GetOpen24Hours(string open24Hours)
        {
            return this.FirstOrDefault(airport => airport.Open24Hours == open24Hours);
        }

        public Airports GetControlTowerHours(string controlTowerHours)
        {
            return this.FirstOrDefault(airport => airport.ControlTowerHours == controlTowerHours);
        }

        public Airports GetApproachList(string approachList)
        {
            return this.FirstOrDefault(airport => airport.ApproachList == approachList);
        }

        public Airports GetHandlingMandatory(string handlingMandatory)
        {
            return this.FirstOrDefault(airport => airport.HandlingMandatory == handlingMandatory);
        }

        public Airports GetSlotsRequired(string slotsRequired)
        {
            return this.FirstOrDefault(airport => airport.SlotsRequired == slotsRequired);
        }

        public Airports GetLighting(string lighting)
        {
            return this.FirstOrDefault(airport => airport.Lighting == lighting);
        }

        public Airports GetVariation(string variation)
        {
            return this.FirstOrDefault(airport => airport.Variation == variation);
        }

        public Airports GetTowerPhone(string towerPhone)
        {
            return this.FirstOrDefault(airport => airport.TowerPhone == towerPhone);
        }

        public Airports GetLatNumber(float latnumber)
        {
            return this.FirstOrDefault(airport => airport.latitude_number == latnumber);
        }

        public Airports GetLongNumber(float longNumber)
        {
            return this.FirstOrDefault(airport => airport.longitude_number == longNumber);
        }

        public Airports GetLat(float lat)
        {
            return this.FirstOrDefault(airport => airport.lat == lat);
        }

        public Airports GetLong(string lng)
        {
            return this.FirstOrDefault(airport => airport.AirportCity == lng);
        }

        public Airports GetAirportHours(string airportHours)
        {
            return this.FirstOrDefault(airport => airport.AirportHours == airportHours);
        }
        #endregion
    }
    #endregion

    #endregion
}