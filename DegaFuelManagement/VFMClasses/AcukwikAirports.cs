using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Degatech.Common;
using System.Data.SqlClient;
using Degatech.Utilities.SQL;

namespace VFMClasses
{
    #region AcukwikAirports
    /// <summary>
    /// This object represents the properties and methods of a AcukwikAirports.
    /// </summary>
    public class AcukwikAirports : BaseClass
    {
        #region Properties
        public float AirportID { get; set; }
        public string ICAO { get; set; } = String.Empty;
        public string IATA { get; set; } = String.Empty;
        public string FAA { get; set; } = String.Empty;
        public string FullAirportName { get; set; } = String.Empty;
        public string AirportCity { get; set; } = String.Empty;
        public string StateSubdivision { get; set; } = String.Empty;
        public string Country { get; set; } = String.Empty;
        public string AirportType { get; set; } = String.Empty;
        public string DistanceFromCity { get; set; } = String.Empty;
        public string Latitude { get; set; } = String.Empty;
        public string Longitude { get; set; } = String.Empty;
        public float Elevation { get; set; }
        public string Variation { get; set; } = String.Empty;
        public string IntlTimeZone { get; set; } = String.Empty;
        public string DaylightSavingsYN { get; set; } = String.Empty;
        public string FuelType { get; set; } = String.Empty;
        public string AirportOfEntry { get; set; } = String.Empty;
        public string Customs { get; set; } = String.Empty;
        public string HandlingMandatory { get; set; } = String.Empty;
        public string SlotsRequired { get; set; } = String.Empty;
        public string Open24Hours { get; set; } = String.Empty;
        public string ControlTowerHours { get; set; } = String.Empty;
        public string ApproachList { get; set; } = String.Empty;
        public string PrimaryRunwayID { get; set; } = String.Empty;
        public float RunwayLength { get; set; }
        public float RunwayWidth { get; set; }
        public string Lighting { get; set; } = String.Empty;
        public string AirportNameShort { get; set; } = String.Empty;
        public AcukwikCountries Countries { get; set; }
        public AirportPriceMargins AirportPriceMargins { get; set; }
        public int AdminClientID { get; set; }
        public string AirportMargin { get; set; }
        //public FBOPriceMargins FBOPriceMargins { get; set; }
        #endregion

        #region Constructors
        public AcukwikAirports()
        {
        }

        public AcukwikAirports(int id)
        {
            AirportID = id;
            Load();
        }

        public AcukwikAirports(SqlDataReader reader)
        {
            LoadFromReader(reader);
        }
        #endregion

        #region Public Methods
        public void Load()
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@AirportID", AirportID));
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Select_AcukwikAirport", parameters))
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
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_InsertUpdate_AcukwikAirport", GetSQLParameters()))
            {
                if (reader == null)
                    return;
                if (reader.Read())
                    SetProperties(reader);
            }
        }
        #endregion

        #region Static Methods
        public static AcukwikAirports GetAcukwikAirport(int id)
        {
            return new AcukwikAirports(id);
        }

        public static AcukwikAirportsCollection LoadList()
        {
            AcukwikAirportsCollection collection = new AcukwikAirportsCollection();
            collection.LoadAll();
            return collection;
        }

        public static AcukwikAirportsCollection LoadAirportsAndMarginsByAdminClientID(int adminId)
        {
            AcukwikAirportsCollection collection = new AcukwikAirportsCollection();
            collection.LoadByAiportsAndMarginsByAdminClientID(adminId);
            return collection;
        }
        #endregion
    }

    #region Collection
    public class AcukwikAirportsCollection : List<AcukwikAirports>
    {
        #region Public Methods


        public void LoadAll()
        {
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Select_AcukwikAirportsAll"))
            {
                if (reader == null)
                    return;
                while (reader.Read())
                {
                    AcukwikAirports airport = new AcukwikAirports();
                    airport.SetProperties(reader);
                    Add(airport);
                }
            }
        }

        public void LoadByAiportsAndMarginsByAdminClientID(int adminClientId)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@AdminClientID", adminClientId));
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Select_AcukwikAirportsAndMarginsByAndAdminClientID", parameters))
            {
                if (reader == null)
                    return;
                while (reader.Read())
                {
                    AcukwikAirports margin = new AcukwikAirports();
                    margin.SetProperties(reader);
                    margin.Countries = new AcukwikCountries();
                    margin.Countries.SetProperties(reader);
                    margin.AirportPriceMargins = new AirportPriceMargins();
                    margin.AirportPriceMargins.SetProperties(reader);
                    //margin.FBOPriceMargins = new FBOPriceMargins();
                    //margin.FBOPriceMargins.SetProperties(reader);
                    Add(margin);
                }
            }
        }


        public AcukwikAirports GetAirportID(float airportId)
        {
            return this.FirstOrDefault(airport => airport.AirportID == airportId);
        }

        public AcukwikAirports GetICAO(string icao)
        {
            return this.FirstOrDefault(airport => airport.ICAO == icao);
        }

        public AcukwikAirports GetIATA(string iata)
        {
            return this.FirstOrDefault(airport => airport.IATA == iata);
        }

        public AcukwikAirports GetFAA(string faa)
        {
            return this.FirstOrDefault(airport => airport.FAA == faa);
        }

        public AcukwikAirports GetFullAirportName(string fullAirportName)
        {
            return this.FirstOrDefault(airport => airport.FullAirportName == fullAirportName);
        }

        public AcukwikAirports GetAirportCity(string airportCity)
        {
            return this.FirstOrDefault(airport => airport.AirportCity == airportCity);
        }

        public AcukwikAirports GetStateSubdivision(string stateSubdivision)
        {
            return this.FirstOrDefault(airport => airport.StateSubdivision == stateSubdivision);
        }

        public AcukwikAirports GetCountry(string country)
        {
            return this.FirstOrDefault(airport => airport.Country == country);
        }

        public AcukwikAirports GetAirportType(string airportType)
        {
            return this.FirstOrDefault(airport => airport.AirportType == airportType);
        }

        public AcukwikAirports GetDistanceFromCity(string distance)
        {
            return this.FirstOrDefault(airport => airport.DistanceFromCity == distance);
        }

        public AcukwikAirports GetLatitude(string latitude)
        {
            return this.FirstOrDefault(airport => airport.Latitude == latitude);
        }

        public AcukwikAirports GetLongitude(string longitude)
        {
            return this.FirstOrDefault(airport => airport.Longitude == longitude);
        }

        public AcukwikAirports GetElevation(int elevation)
        {
            return this.FirstOrDefault(airport => airport.Elevation == elevation);
        }

        public AcukwikAirports GetVariation(string variation)
        {
            return this.FirstOrDefault(airport => airport.Variation == variation);
        }

        public AcukwikAirports GetIntlTimeZone(string intlTimeZone)
        {
            return this.FirstOrDefault(airport => airport.IntlTimeZone == intlTimeZone);
        }

        public AcukwikAirports GetDaylightSavingsYN(string daylightSavingsYN)
        {
            return this.FirstOrDefault(airport => airport.DaylightSavingsYN == daylightSavingsYN);
        }

        public AcukwikAirports GetFuelType(string fuelType)
        {
            return this.FirstOrDefault(airport => airport.FuelType == fuelType);
        }

        public AcukwikAirports GetAirportOfEntry(string entry)
        {
            return this.FirstOrDefault(airport => airport.AirportOfEntry == entry);
        }

        public AcukwikAirports GetCustoms(string customs)
        {
            return this.FirstOrDefault(airport => airport.Customs == customs);
        }

        public AcukwikAirports GetHandlingMandatory(string handlingMandatory)
        {
            return this.FirstOrDefault(airport => airport.HandlingMandatory == handlingMandatory);
        }

        public AcukwikAirports GetSlotsRequired(string slotsRequired)
        {
            return this.FirstOrDefault(airport => airport.SlotsRequired == slotsRequired);
        }

        public AcukwikAirports GetOpen24Hours(string open24Hours)
        {
            return this.FirstOrDefault(airport => airport.Open24Hours == open24Hours);
        }

        public AcukwikAirports GetControlTowerHours(string controlTowerHours)
        {
            return this.FirstOrDefault(airport => airport.ControlTowerHours == controlTowerHours);
        }

        public AcukwikAirports GetApproachList(string approachList)
        {
            return this.FirstOrDefault(airport => airport.ApproachList == approachList);
        }

        public AcukwikAirports GetPrimaryRunwayID(string primaryRunwayID)
        {
            return this.FirstOrDefault(airport => airport.PrimaryRunwayID == primaryRunwayID);
        }

        public AcukwikAirports GetRunwayLength(int runwayLength)
        {
            return this.FirstOrDefault(airport => airport.RunwayLength == runwayLength);
        }

        public AcukwikAirports GetRunwayWidth(int runwayWidth)
        {
            return this.FirstOrDefault(airport => airport.RunwayWidth == runwayWidth);
        }

        public AcukwikAirports GetLighting(string lighting)
        {
            return this.FirstOrDefault(airport => airport.Lighting == lighting);
        }

        public AcukwikAirports GetAirportNameShort(string airportName)
        {
            return this.FirstOrDefault(airport => airport.AirportNameShort == airportName);
        }

        public AcukwikAirports GetAdminClient(int clientId)
        {
            return this.FirstOrDefault(airport => airport.AdminClientID == clientId);
        }

        public AcukwikAirports GetAirportMargin(string margin)
        {
            return this.FirstOrDefault(airport => airport.AirportMargin == margin);
        }
        #endregion
    }
    #endregion

    #endregion
}