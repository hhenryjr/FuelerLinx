using Degatech.Common;
using Degatech.Utilities.SQL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFMClasses
{
    #region FBOPriceMargins
    /// <summary>
    /// This object represents the properties and methods of a FBOPriceMargins.
    /// </summary>
    public class FBOPriceMargins : BaseClass
    {
        #region Properties
        public int Id { get; set; }
        public int AdminClientID { get; set; }
        public string ICAO { get; set; } = String.Empty;
        public string FBO { get; set; } = String.Empty;
        public double Margin { get; set; }
        public bool IsEditable { get; set; }
        public bool IsOmitted { get; set; }
        public string Note { get; set; }
        public AcukwikFBOHandlerDetail AcukwikFBO { get; set; }
        public AcukwikAirports Airports { get; set; }
        public FBODetailCustomFieldsCollection CustomFields { get; set; }
        #endregion

        #region Constructors
        public FBOPriceMargins()
        {
        }

        public FBOPriceMargins(int id)
        {
            Id = id;
            Load();
        }

        public FBOPriceMargins(SqlDataReader reader)
        {
            LoadFromReader(reader);
        }
        #endregion

        #region Public Methods
        public void Load()
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Id", Id));
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Select_FBOPriceMargin", parameters))
            {
                if (reader == null)
                    return;
                if (reader.Read())
                    SetProperties(reader);
            }
        }

        public void GetFBODetailsByICAO_FBO_Admin(string icao, string fbo, int clientId)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ICAO", icao));
            parameters.Add(new SqlParameter("@FBO", fbo));
            parameters.Add(new SqlParameter("@AdminClientID", clientId));
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Select_FBODetailsByAndICAOAndFBOAndAdminClientID", parameters))
            {
                if (reader == null)
                    return;
                if (reader.Read())
                {
                    //SetProperties(reader);
                    //AcukwikFBO = new AcukwikFBOHandlerDetail();
                    //AcukwikFBO.SetProperties(reader);
                    //Airports = new AcukwikAirports();
                    //CustomFields = new FBODetailCustomFieldsCollection();
                    //CustomFields.GetCustomFields(fbo, icao, clientId);
                    FBOPriceMargins margin = new FBOPriceMargins();
                    margin.SetProperties(reader);
                    margin.AcukwikFBO = new AcukwikFBOHandlerDetail();
                    margin.AcukwikFBO.SetProperties(reader);
                    margin.Airports = new AcukwikAirports();
                    margin.Airports.SetProperties(reader);
                    margin.CustomFields = new FBODetailCustomFieldsCollection();
                    margin.CustomFields.GetCustomFields(fbo, icao, clientId);
                }
            }
        }

        protected void LoadFromReader(SqlDataReader reader)
        {
            SetProperties(reader);
        }

        //public void Delete()
        //{
        //    DeleteFBOPriceMargin(Id);
        //}

        public void Update()
        {
            ArrayList propertiesToOmit = new ArrayList();
            //propertiesToOmit.Add("Note");
            //propertiesToOmit.Add("IsOmitted");
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_InsertUpdate_FBOPriceMargin", GetSQLParameters(propertiesToOmit)))
            {
                if (reader == null)
                    return;
                if (reader.Read())
                    SetProperties(reader);
            }
        }
        #endregion

        #region Static Methods
        public static FBOPriceMargins GetFBOPriceMargin(int id)
        {
            return new FBOPriceMargins(id);
        }

        public static FBOPriceMarginsCollection LoadList()
        {
            FBOPriceMarginsCollection collection = new FBOPriceMarginsCollection();
            collection.LoadAll();
            return collection;
        }

        public static FBOPriceMarginsCollection LoadListByAdminAndICAO(int adminID, string icao)
        {
            FBOPriceMarginsCollection collection = new FBOPriceMarginsCollection();
            collection.LoadByAdminIDAndICAO(adminID, icao);
            return collection;
        }

        public static FBOPriceMargins GetFBODetailsWithCustomFields(string icao, string fbo, int clientId)
        {
            FBOPriceMargins collection = new FBOPriceMargins();
            collection.GetFBODetailsByICAO_FBO_Admin(icao, fbo, clientId);
            return collection;
        }

        public static FBOPriceMarginsCollection GetFBODetails(string icao, string fbo, int clientId)
        {
            //FBOPriceMargins margin = new FBOPriceMargins();
            //margin.GetFBODetailsByICAO_FBO_Admin(icao, fbo, clientId);
            //return margin;
            FBOPriceMarginsCollection collection = new FBOPriceMarginsCollection();
            collection.LoadFBODetails(icao, fbo, clientId);
            return collection;
        }

        public static FBOPriceMarginsCollection DeleteFBOPriceMargin(string icao, string fbo, int clientId, int id)
        {
            FBOPriceMarginsCollection collection = new FBOPriceMarginsCollection();
            collection.DeleteFBOPriceMargin(icao, fbo, clientId, id);
            return collection;
        }
        #endregion
    }
    #endregion

    #region Collection
    public class FBOPriceMarginsCollection : List<FBOPriceMargins>
    {
        public void LoadAll()
        {
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Select_FBOPriceMarginsAll"))
            {
                if (reader == null)
                    return;
                while (reader.Read())
                {
                    FBOPriceMargins margin = new FBOPriceMargins();
                    margin.SetProperties(reader);
                    Add(margin);
                }
            }
        }

        public void LoadByAdminIDAndICAO(int adminId, string icao)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@AdminClientID", adminId));
            parameters.Add(new SqlParameter("@ICAO", icao));
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Select_FBOPriceMarginsByAndAdminClientIDAndICAO", parameters))
            {
                if (reader == null)
                    return;
                while (reader.Read())
                {
                    FBOPriceMargins margin = new FBOPriceMargins();
                    margin.SetProperties(reader);
                    Add(margin);
                }
            }
        }

        public void LoadFBODetails(string icao, string fbo, int clientId)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ICAO", icao));
            parameters.Add(new SqlParameter("@FBO", fbo));
            parameters.Add(new SqlParameter("@AdminClientID", clientId));
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Select_FBODetailsByAndICAOAndFBOAndAdminClientID", parameters))
            {
                if (reader == null)
                    return;
                if (reader.Read())
                {
                    FBOPriceMargins margin = new FBOPriceMargins();
                    margin.SetProperties(reader);
                    margin.AcukwikFBO = new AcukwikFBOHandlerDetail();
                    margin.AcukwikFBO.SetProperties(reader);
                    margin.Airports = new AcukwikAirports();
                    margin.Airports.SetProperties(reader);
                    margin.CustomFields = new FBODetailCustomFieldsCollection();
                    margin.CustomFields.GetCustomFields(fbo, icao, clientId);
                    Add(margin);
                }
            }
        }

        public void DeleteFBOPriceMargin(string icao, string fbo, int clientId, int id)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ICAO", icao));
            parameters.Add(new SqlParameter("@FBO", fbo));
            parameters.Add(new SqlParameter("@AdminClientID", clientId));
            parameters.Add(new SqlParameter("@Id", id));
            ExecutionHelper.ExecuteNonQuery("up_Delete_FBOPriceMargin", parameters);
        }

        public FBOPriceMargins GetId(int id)
        {
            return this.FirstOrDefault(margin => margin.Id == id);
        }

        public FBOPriceMargins GetAdminClientID(int adminId)
        {
            return this.FirstOrDefault(margin => margin.AdminClientID == adminId);
        }

        public FBOPriceMargins GetICAO(string icao)
        {
            return this.FirstOrDefault(margin => margin.ICAO == icao);
        }

        public FBOPriceMargins GetFBO(string fbo)
        {
            return this.FirstOrDefault(margin => margin.FBO == fbo);
        }

        public FBOPriceMargins GetMargin(double margin)
        {
            return this.FirstOrDefault(m => m.Margin == margin);
        }

        public FBOPriceMargins GetEditable(bool isEditable)
        {
            return this.FirstOrDefault(m => m.IsEditable == isEditable);
        }

        public FBOPriceMargins GetOmitted(bool isOmitted)
        {
            return this.FirstOrDefault(m => m.IsOmitted == isOmitted);
        }

        public FBOPriceMargins GetNote(string note)
        {
            return this.FirstOrDefault(margin => margin.Note == note);
        }
    }
    #endregion
}