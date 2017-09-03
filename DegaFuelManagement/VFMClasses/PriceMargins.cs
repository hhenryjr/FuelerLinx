using Degatech.Common;
using Degatech.Utilities.SQL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFMClasses
{
    #region PriceMargins
    /// <summary>
    /// This object represents the properties and methods of a PriceMargins.
    /// </summary>
    public class PriceMargins : BaseClass
    {
        #region Properties
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public int AdminClientID { get; set; }
        public decimal PrimaryMargin { get; set; }
        public string Note { get; set; } = String.Empty;
        public DateTime DateAdded { get; set; }
        public PriceMarginTiersCollection PriceMarginTiers { get; set; }
        #endregion

        #region Constructors
        public PriceMargins()
        {
        }

        public PriceMargins(int id)
        {
            Id = id;
            Load();
        }

        public PriceMargins(SqlDataReader reader)
        {
            LoadFromReader(reader);
        }
        #endregion

        #region Public Methods
        public void Load()
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Id", Id));
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Select_PriceMargin", parameters))
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
            DeletePriceMargin(Id);
        }

        public void Update()
        {
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_InsertUpdate_PriceMargin", GetSQLParameters()))
            {
                if (reader == null)
                    return;
                if (reader.Read())
                    SetProperties(reader);
            }
        }
        #endregion

        #region Static Methods
        public static PriceMargins GetPriceMargin(int id)
        {
            return new PriceMargins(id);
        }

        public static PriceMarginsCollection LoadList()
        {
            PriceMarginsCollection collection = new PriceMarginsCollection();
            collection.LoadAll();
            return collection;
        }

        public static PriceMarginsCollection LoadListByAdmin(int adminID)
        {
            PriceMarginsCollection collection = new PriceMarginsCollection();
            collection.LoadByAdminID(adminID);
            return collection;
        }

        public static PriceMarginsCollection DeletePriceMargin(int id)
        {
            PriceMarginsCollection collection = new PriceMarginsCollection();
            collection.DeletePriceMargin(id);
            return collection;
        }
        #endregion
    }
    #endregion

    #region Collection
    public class PriceMarginsCollection : List<PriceMargins>
    {
        public void LoadAll()
        {
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Select_PriceMarginsAll"))
            {
                if (reader == null)
                    return;
                while (reader.Read())
                {
                    PriceMargins margin = new PriceMargins();
                    margin.SetProperties(reader);
                    Add(margin);
                }
            }
        }

        public void LoadByAdminID(int adminId)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@AdminClientID", adminId));
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Select_PriceMarginsByAndAdminClientID", parameters))
            {
                if (reader == null)
                    return;
                while (reader.Read())
                {
                    PriceMargins margin = new PriceMargins();
                    margin.SetProperties(reader);
                    margin.PriceMarginTiers = new PriceMarginTiersCollection();
                    margin.PriceMarginTiers.LoadByPriceMarginID(margin.Id);
                    Add(margin);
                }
            }
        }

        public void DeletePriceMargin(int id)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Id", id));
            ExecutionHelper.ExecuteNonQuery("up_Delete_PriceMargin", parameters);
        }

        public PriceMargins GetId(int id)
        {
            return this.FirstOrDefault(margin => margin.Id == id);
        }

        public PriceMargins GetName(string name)
        {
            return this.FirstOrDefault(margin => margin.Name == name);
        }

        public PriceMargins GetMargin(decimal primaryMargin)
        {
            return this.FirstOrDefault(margin => margin.PrimaryMargin == primaryMargin);
        }

        public PriceMargins GetAdminClientID(int adminId)
        {
            return this.FirstOrDefault(margin => margin.AdminClientID == adminId);
        }        

        public PriceMargins GetNotes(string notes)
        {
            return this.FirstOrDefault(margin => margin.Note == notes);
        }     
        
        public PriceMargins GetDateAdded(DateTime dateAdded)
        {
            return this.FirstOrDefault(margin => margin.DateAdded == dateAdded);
        }
    }
    #endregion
}