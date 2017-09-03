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
    #region AircraftPriceMargins
    /// <summary>
    /// This object represents the properties and methods of a AircraftPriceMargins.
    /// </summary>
    public class AircraftPriceMargins : BaseClass
    {
        #region Properties
        public int AircraftID { get; set; }
        public int PriceMarginID { get; set; }
        #endregion

        #region Constructors
        public AircraftPriceMargins()
        {
        }

        public AircraftPriceMargins(int id)
        {
            AircraftID = id;
            Load();
        }

        public AircraftPriceMargins(SqlDataReader reader)
        {
            LoadFromReader(reader);
        }
        #endregion

        #region Public Methods
        public void Load()
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@AircraftID", AircraftID));
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Select_AircraftPriceMargin", parameters))
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
            Delete(AircraftID);
        }

        public void Update()
        {
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_InsertUpdate_AircraftPriceMargin", GetSQLParameters()))
            {
                if (reader == null)
                    return;
                if (reader.Read())
                    SetProperties(reader);
            }
        }
        #endregion

        #region Static Methods
        public static AircraftPriceMarginsCollection LoadList()
        {
            AircraftPriceMarginsCollection collection = new AircraftPriceMarginsCollection();
            collection.LoadAll();
            return collection;
        }

        public static AircraftPriceMargins GetAircraftPriceMargin(int id)
        {
            return new AircraftPriceMargins(id);
        }

        public static AircraftPriceMarginsCollection GetAircraftPriceMarginByAircraftID(int id)
        {
            AircraftPriceMarginsCollection collection = new AircraftPriceMarginsCollection();
            collection.LoadByAircraftID(id);
            return collection;
        }

        public static AircraftPriceMarginsCollection Delete(int id)
        {
            AircraftPriceMarginsCollection collection = new AircraftPriceMarginsCollection();
            collection.Delete(id);
            return collection;
        }
        #endregion
    }
    #endregion

    #region Collection
    public class AircraftPriceMarginsCollection : List<AircraftPriceMargins>
    {
        public void LoadAll()
        {
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Select_AircraftPriceMarginsAll"))
            {
                if (reader == null)
                    return;
                while (reader.Read())
                {
                    AircraftPriceMargins margin = new AircraftPriceMargins();
                    margin.SetProperties(reader);
                    Add(margin);
                }
            }
        }

        public void LoadByAircraftID(int aircraftID)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@AircraftID", aircraftID));
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Select_AircraftPriceMarginsByAndAircraftID", parameters))
            {
                if (reader == null)
                    return;
                while (reader.Read())
                {
                    AircraftPriceMargins margin = new AircraftPriceMargins();
                    margin.SetProperties(reader);
                    Add(margin);
                }
            }
        }

        public void LoadByPriceMarginID(int marginID)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@PriceMarginID", marginID));
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Select_AircraftPriceMarginsByAndPriceMarginID", parameters))
            {
                if (reader == null)
                    return;
                while (reader.Read())
                {
                    AircraftPriceMargins margin = new AircraftPriceMargins();
                    margin.SetProperties(reader);
                    Add(margin);
                }
            }
        }

        public void Delete(int id)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@AircraftID", id));
            ExecutionHelper.ExecuteNonQuery("up_Delete_AircraftPriceMargin", parameters);
        }

        public AircraftPriceMargins GetAircraftID(int clientId)
        {
            return this.FirstOrDefault(margin => margin.AircraftID == clientId);
        }

        public AircraftPriceMargins GetPriceMarginID(int marginId)
        {
            return this.FirstOrDefault(margin => margin.PriceMarginID == marginId);
        }
    }
    #endregion
}