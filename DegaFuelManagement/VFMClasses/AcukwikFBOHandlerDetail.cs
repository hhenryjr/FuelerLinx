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
    #region AcukwikFBOHandlerDetail
    /// <summary>
    /// This object represents the properties and methods of a AcukwikFBOHandlerDetail.
    /// </summary>
    public class AcukwikFBOHandlerDetail : BaseClass
    {
        #region Properties
        public float AirportID { get; set; }
        public float HandlerID { get; set; }
        public string HandlerLongName { get; set; } = String.Empty;
        public string HandlerType { get; set; } = String.Empty;
        public string HandlerTelephone { get; set; } = String.Empty;
        public string HandlerFax { get; set; } = String.Empty;
        public string HandlerTollFree { get; set; } = String.Empty;
        public float HandlerFreq { get; set; }
        public string HandlerFuelBrand { get; set; } = String.Empty;
        public string HandlerFuelBrand2 { get; set; } = String.Empty;
        public string HandlerFuelSupply { get; set; } = String.Empty;
        public string HandlerLocationOnField { get; set; } = String.Empty;
        public string MultiService { get; set; } = String.Empty;
        public string Avcard { get; set; } = String.Empty;
        public float AcukwikID { get; set; }
        #endregion

        #region Constructors
        public AcukwikFBOHandlerDetail()
        {
        }

        public AcukwikFBOHandlerDetail(int id)
        {
            HandlerID = id;
            Load();
        }

        public AcukwikFBOHandlerDetail(SqlDataReader reader)
        {
            LoadFromReader(reader);
        }
        #endregion

        #region Public Methods
        public void Load()
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@HandlerID", HandlerID));
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Select_AcukwikFBOHandlerDetail", parameters))
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
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_InsertUpdate_AcukwikFBOHandlerDetail", GetSQLParameters()))
            {
                if (reader == null)
                    return;
                if (reader.Read())
                    SetProperties(reader);
            }
        }
        #endregion

        #region Static Methods
        public static AcukwikFBOHandlerDetail GetAcukwikFBOHandlerDetail(int id)
        {
            return new AcukwikFBOHandlerDetail(id);
        }

        public static AcukwikFBOHandlerDetailCollection LoadList()
        {
            AcukwikFBOHandlerDetailCollection collection = new AcukwikFBOHandlerDetailCollection();
            collection.LoadAll();
            return collection;
        }

        #endregion
    }

    #region Collection
    public class AcukwikFBOHandlerDetailCollection : List<AcukwikFBOHandlerDetail>
    {
        #region Public Methods


        public void LoadAll()
        {
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Select_AcukwikFBOHandlerDetailsAll"))
            {
                if (reader == null)
                    return;
                while (reader.Read())
                {
                    AcukwikFBOHandlerDetail airport = new AcukwikFBOHandlerDetail();
                    airport.SetProperties(reader);
                    Add(airport);
                }
            }
        }

        public AcukwikFBOHandlerDetail GetAirportID(float airportId)
        {
            return this.FirstOrDefault(airport => airport.AirportID == airportId);
        }

        public AcukwikFBOHandlerDetail GetHandlerID(float id)
        {
            return this.FirstOrDefault(airport => airport.HandlerID == id);
        }

        public AcukwikFBOHandlerDetail GetHandlerLongName(string name)
        {
            return this.FirstOrDefault(airport => airport.HandlerLongName == name);
        }

        public AcukwikFBOHandlerDetail GetHandlerType(string type)
        {
            return this.FirstOrDefault(airport => airport.HandlerType == type);
        }

        public AcukwikFBOHandlerDetail GetHandlerTelephone(string telephone)
        {
            return this.FirstOrDefault(airport => airport.HandlerTelephone == telephone);
        }

        public AcukwikFBOHandlerDetail GetHandlerFax(string fax)
        {
            return this.FirstOrDefault(airport => airport.HandlerFax == fax);
        }

        public AcukwikFBOHandlerDetail GetHandlerTollFree(string toll)
        {
            return this.FirstOrDefault(airport => airport.HandlerTollFree == toll);
        }

        public AcukwikFBOHandlerDetail GetHandlerFreq(float freq)
        {
            return this.FirstOrDefault(airport => airport.HandlerFreq == freq);
        }

        public AcukwikFBOHandlerDetail GetHandlerFuelBrand(string brand)
        {
            return this.FirstOrDefault(airport => airport.HandlerFuelBrand == brand);
        }

        public AcukwikFBOHandlerDetail GetHandlerFuelBrand2(string brand)
        {
            return this.FirstOrDefault(airport => airport.HandlerFuelBrand2 == brand);
        }

        public AcukwikFBOHandlerDetail GetHandlerFuelSupply(string supply)
        {
            return this.FirstOrDefault(airport => airport.HandlerFuelSupply == supply);
        }

        public AcukwikFBOHandlerDetail GetHandlerLocationOnField(string location)
        {
            return this.FirstOrDefault(airport => airport.HandlerLocationOnField == location);
        }

        public AcukwikFBOHandlerDetail GetMultiService(string multi)
        {
            return this.FirstOrDefault(airport => airport.MultiService == multi);
        }

        public AcukwikFBOHandlerDetail GetAvcard(string avcard)
        {
            return this.FirstOrDefault(airport => airport.Avcard == avcard);
        }

        public AcukwikFBOHandlerDetail GetAcukwikID(float id)
        {
            return this.FirstOrDefault(airport => airport.AcukwikID == id);
        }
        #endregion
    }
    #endregion

    #endregion
}