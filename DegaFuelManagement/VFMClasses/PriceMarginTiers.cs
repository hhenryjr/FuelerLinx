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
    #region PriceMarginTiers
    /// <summary>
    /// This object represents the properties and methods of a PriceMarginTiers.
    /// </summary>
    public class PriceMarginTiers : BaseClass
    {
        #region Properties
        public int Id { get; set; }
        public int PriceMarginID { get; set; }
        public int MinGallon { get; set; }
        public int MaxGallon { get; set; }
        public decimal Margin { get; set; }
        public DateTime DateAdded { get; set; }
        #endregion

        #region Constructors
        public PriceMarginTiers()
        {
        }

        public PriceMarginTiers(int id)
        {
            Id = id;
            Load();
        }

        public PriceMarginTiers(SqlDataReader reader)
        {
            LoadFromReader(reader);
        }
        #endregion

        #region Public Methods
        public void Load()
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@PriceMarginID", Id));
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Select_PriceMarginTiersByAndPriceMarginID", parameters))
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
            DeletePriceMarginTier(Id);
        }

        public void Update()
        {
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_InsertUpdate_PriceMarginTier", GetSQLParameters()))
            {
                if (reader == null)
                    return;
                if (reader.Read())
                    SetProperties(reader);
            }
        }
        #endregion

        #region Static Methods
        public static PriceMarginTiers GetPriceMarginTier(int id)
        {
            return new PriceMarginTiers(id);
        }

        public static PriceMarginTiersCollection LoadList()
        {
            PriceMarginTiersCollection collection = new PriceMarginTiersCollection();
            collection.LoadAll();
            return collection;
        }

        public static PriceMarginTiersCollection LoadByPriceMarginID(int adminID)
        {
            PriceMarginTiersCollection collection = new PriceMarginTiersCollection();
            collection.LoadByPriceMarginID(adminID);
            return collection;
        }

        public static PriceMarginTiersCollection DeletePriceMarginTier(int id)
        {
            PriceMarginTiersCollection collection = new PriceMarginTiersCollection();
            collection.DeletePriceMarginTier(id);
            return collection;
        }
        #endregion
    }
    #endregion

    #region Collection
    public class PriceMarginTiersCollection : List<PriceMarginTiers>
    {
        public void LoadAll()
        {
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Select_PriceMarginTiersAll"))
            {
                if (reader == null)
                    return;
                while (reader.Read())
                {
                    PriceMarginTiers margin = new PriceMarginTiers();
                    margin.SetProperties(reader);
                    Add(margin);
                }
            }
        }

        public void LoadByPriceMarginID(int adminId)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@PriceMarginID", adminId));
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Select_PriceMarginTiersByAndPriceMarginID", parameters))
            {
                if (reader == null)
                    return;
                while (reader.Read())
                {
                    PriceMarginTiers margin = new PriceMarginTiers();
                    margin.SetProperties(reader);
                    Add(margin);
                }
            }
        }

        public void DeletePriceMarginTier(int id)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Id", id));
            ExecutionHelper.ExecuteNonQuery("up_Delete_PriceMarginTier", parameters);
        }

        public PriceMarginTiers GetId(int id)
        {
            return this.FirstOrDefault(tier => tier.Id == id);
        }

        public PriceMarginTiers GetPriceMarginID(int marginID)
        {
            return this.FirstOrDefault(tier => tier.PriceMarginID == marginID);
        }

        public PriceMarginTiers GetMaxGallon(int maxGal)
        {
            return this.FirstOrDefault(tier => tier.MaxGallon == maxGal);
        }

        public PriceMarginTiers GetMinGallon(int minGal)
        {
            return this.FirstOrDefault(tier => tier.MinGallon == minGal);
        }

        public PriceMarginTiers GetMargin(decimal margin)
        {
            return this.FirstOrDefault(tier => tier.Margin == margin);
        }

        public PriceMarginTiers GetDateAdded(DateTime dateAdded)
        {
            return this.FirstOrDefault(tier => tier.DateAdded == dateAdded);
        }
    }
    #endregion
}