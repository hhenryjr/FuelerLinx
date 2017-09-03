using Degatech.Common;
using Degatech.Utilities.SQL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFMClasses
{
    #region AirportPriceMargin
    /// <summary>
    /// This object represents the properties and methods of a AirportPriceMargin.
    /// </summary>
    public class AirportPriceMargins : BaseClass
    {
        #region Properties
        public int AdminClientID { get; set; }
        public string ICAO { get; set; } = String.Empty;
        //public double Margin { get; set; }
        public bool IsInactive { get; set; }
        //public bool IsDisabled { get; set; }
        #endregion

        #region Constructors
        public AirportPriceMargins()
        {
        }

        public AirportPriceMargins(int id)
        {
            AdminClientID = id;
            Load();
        }

        public AirportPriceMargins(SqlDataReader reader)
        {
            LoadFromReader(reader);
        }
        #endregion

        #region Public Methods
        public void Load()
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@AdminClientID", AdminClientID));
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Select_AirportPriceMargin", parameters))
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
            DeleteAirportPriceMargin(AdminClientID);
        }

        public void Update()
        {
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_InsertUpdate_AirportPriceMargin", GetSQLParameters()))
            {
                if (reader == null)
                    return;
                if (reader.Read())
                    SetProperties(reader);
            }
        }
        #endregion

        #region Static Methods
        public static AirportPriceMargins GetAirportPriceMargin(int id)
        {
            return new AirportPriceMargins(id);
        }

        public static AirportPriceMarginCollection LoadList()
        {
            AirportPriceMarginCollection collection = new AirportPriceMarginCollection();
            collection.LoadAll();
            return collection;
        }

        public static AirportPriceMarginCollection LoadListByAdmin(int adminID)
        {
            AirportPriceMarginCollection collection = new AirportPriceMarginCollection();
            collection.LoadByAdminID(adminID);
            return collection;
        }

        public static AirportPriceMarginCollection DeleteAirportPriceMargin(int id)
        {
            AirportPriceMarginCollection collection = new AirportPriceMarginCollection();
            collection.DeleteAirportPriceMargin(id);
            return collection;
        }
        #endregion
    }
    #endregion

    #region Collection
    public class AirportPriceMarginCollection : List<AirportPriceMargins>
    {
        public void LoadAll()
        {
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Select_AirportPriceMarginsAll"))
            {
                if (reader == null)
                    return;
                while (reader.Read())
                {
                    AirportPriceMargins margin = new AirportPriceMargins();
                    margin.SetProperties(reader);
                    Add(margin);
                }
            }
        }

        public void LoadByAdminID(int adminId)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@AdminClientID", adminId));
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Select_AirportPriceMarginsByAndAdminClientID", parameters))
            {
                if (reader == null)
                    return;
                while (reader.Read())
                {
                    AirportPriceMargins margin = new AirportPriceMargins();
                    margin.SetProperties(reader);
                    Add(margin);
                }
            }
        }

        public static DataTable GetAirportsByAdminClientID(int clientId)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@AdminClientID", clientId));
            DataSet resultSet = ExecutionHelper.ExecuteDataset("up_Select_AirportPriceMarginsByAdminClientID", parameters);
            if (resultSet.Tables.Count > 0)
                return resultSet.Tables[0];
            return null;
        }

        public void DeleteAirportPriceMargin(int id)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@AdminClientID", id));
            ExecutionHelper.ExecuteNonQuery("up_Delete_AirportPriceMargin", parameters);
        }

        public AirportPriceMargins GetAdminClientID(int adminId)
        {
            return this.FirstOrDefault(margin => margin.AdminClientID == adminId);
        }

        public AirportPriceMargins GetICAO(string icao)
        {
            return this.FirstOrDefault(margin => margin.ICAO == icao);
        }

        //public AirportPriceMargins GetMargin(double margin)
        //{
        //    return this.FirstOrDefault(m => m.Margin == margin);
        //}

        public AirportPriceMargins GetIsInactive(bool isInactive)
        {
            return this.FirstOrDefault(m => m.IsInactive == isInactive);
        }

        //public AirportPriceMargins GetIsDisabled(bool isDisabled)
        //{
        //    return this.FirstOrDefault(m => m.IsDisabled == isDisabled);
        //}
    }
    #endregion
}