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
    #region AcukwikCountries
    /// <summary>
    /// This object represents the properties and methods of a AcukwikCountries.
    /// </summary>
    public class AcukwikCountries : BaseClass
    {
        #region Properties
        public float CountryID { get; set; }
        public string Country { get; set; } = String.Empty;
        public string FullCountryName { get; set; } = String.Empty;
        public string ISOCountryID { get; set; } = String.Empty;
        public string ISO_Name { get; set; } = String.Empty;
        public string ISOCode2 { get; set; } = String.Empty;
        public string ISOCode3 { get; set; } = String.Empty;
        public string Region { get; set; } = String.Empty;
        #endregion

        #region Constructors
        public AcukwikCountries()
        {
        }

        public AcukwikCountries(int id)
        {
            CountryID = id;
            Load();
        }

        public AcukwikCountries(SqlDataReader reader)
        {
            LoadFromReader(reader);
        }
        #endregion

        #region Public Methods
        public void Load()
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@CountryID", CountryID));
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
        public static AcukwikCountries GetAcukwikAirport(int id)
        {
            return new AcukwikCountries(id);
        }

        public static AcukwikCountriesCollection LoadList()
        {
            AcukwikCountriesCollection collection = new AcukwikCountriesCollection();
            collection.LoadAll();
            return collection;
        }

        #endregion
    }

    #region Collection
    public class AcukwikCountriesCollection : List<AcukwikCountries>
    {
        #region Public Methods


        public void LoadAll()
        {
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Select_AcukwikCountriesAll"))
            {
                if (reader == null)
                    return;
                while (reader.Read())
                {
                    AcukwikCountries country = new AcukwikCountries();
                    country.SetProperties(reader);
                    Add(country);
                }
            }
        }

        public AcukwikCountries GetCountryID(float countryId)
        {
            return this.FirstOrDefault(country => country.CountryID == countryId);
        }

        public AcukwikCountries GetCountry(string country)
        {
            return this.FirstOrDefault(c => c.Country == country);
        }

        public AcukwikCountries GetFullCountryName(string name)
        {
            return this.FirstOrDefault(country => country.FullCountryName == name);
        }

        public AcukwikCountries GetISOCountryID(string countryId)
        {
            return this.FirstOrDefault(country => country.ISOCountryID == countryId);
        }

        public AcukwikCountries GetISO_Name(string name)
        {
            return this.FirstOrDefault(country => country.ISO_Name == name);
        }

        public AcukwikCountries GetISOCode2(string iso)
        {
            return this.FirstOrDefault(country => country.ISOCode2 == iso);
        }

        public AcukwikCountries GetISOCode3(string iso)
        {
            return this.FirstOrDefault(country => country.ISOCode3 == iso);
        }

        public AcukwikCountries GetRegion(string region)
        {
            return this.FirstOrDefault(country => country.Region == region);
        }
        #endregion
    }
    #endregion

    #endregion
}