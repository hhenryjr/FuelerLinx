using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Degatech.Common;
using Degatech.Utilities.SQL;

namespace VFMClasses
{
    public class FuelOrderFees : BaseClass
    {
        //private int id;
        #region Enums
        #endregion

        #region Properties
        public int Id { get; set; }
        public int FuelOrderID { get; set; } = 0;
        public string FeeDesc { get; set; } = "";
        public double FeeAmount { get; set; } = 0.0;
        public string FeeNameAsKey { get; set; } = "";
        public bool IsStored { get; set; }

        #endregion

        #region Constructors
        public FuelOrderFees()
        {
        }

        public FuelOrderFees(int id)
        {
            Id = id;
        }
        #endregion

        #region Public Methods
        public void Update()
        {
            ArrayList propertiesToOmit = new ArrayList();
            propertiesToOmit.Add("FeeNameAsKey");
            propertiesToOmit.Add("IsStored");
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_InsertUpdate_FuelOrderFees", GetSQLParameters(propertiesToOmit)))
            {
                if (reader == null)
                    return;
                if (reader.Read())
                    SetProperties(reader);
            }
        }
        
        public string GetFeeDescAsKey()
        {
            if (string.IsNullOrEmpty(FeeNameAsKey))
                FeeNameAsKey = FeeDesc.ToLower().Replace("fee", "").Trim();
            return FeeNameAsKey;
        }

        public bool HasPricingData()
        {
            return (FeeAmount > 0);
        }
        #endregion

        #region Static Methods
        public static void DeleteAll(int FuelOrderID)
        {
            List<SqlParameter> Params = new List<SqlParameter>();
            SqlParameter Param = new SqlParameter("@FuelOrderID", SqlDbType.Int);
            Param.Value = FuelOrderID;
            Params.Add(Param);
            ExecutionHelper.ExecuteNonQuery("up_Delete_FuelOrderFees", Params);
        }

        public static FuelOrderFeesCollection DeleteFuelOrderFee(int id)
        {
            FuelOrderFeesCollection collection = new FuelOrderFeesCollection();
            collection.Delete(id);
            return collection;
        }

        public static FuelOrderFeesCollection LoadList(int FuelOrderID)
        {
            FuelOrderFeesCollection collection = new FuelOrderFeesCollection();
            collection.LoadAllFees(FuelOrderID);
            return collection;
        }
        #endregion
    }

    public class FuelOrderFeesCollection : List<FuelOrderFees>
    {
        #region Public Methods
        /// <summary>
        /// Populates the collection with the company's default fees
        /// </summary>
        /// <param name="clientID">The ID of the company to use</param>
        public void LoadClientFees(int clientID)
        {
            ArrayList defaultFees = ClientFees.GetList(clientID);
            if (defaultFees == null)
                return;
            foreach (string defaultFee in defaultFees)
            {
                if (string.IsNullOrEmpty(defaultFee))
                    continue;
                var fee = GetFeeByDescription(defaultFee);
                if (fee == null)
                {
                    fee = new FuelOrderFees() { FeeDesc = defaultFee };
                    this.Add(fee);
                }
            }
            this.SortByStored();
        }

        public void LoadAllFees(int FuelOrderID)
        {
            List<SqlParameter> Params = new List<SqlParameter>();
            SqlParameter Param = new SqlParameter("@FuelOrderID", SqlDbType.Int);
            Param.Value = FuelOrderID;
            Params.Add(Param);
            using (SqlDataReader rdr = ExecutionHelper.ExecuteReader("up_Load_FuelOrderFees", Params))
            {
                while (rdr.Read())
                {
                    FuelOrderFees fuelOrderFee = new FuelOrderFees();
                    fuelOrderFee.SetProperties(rdr);
                    Add(fuelOrderFee);
                }
            }
        }

        public FuelOrderFees GetFeeByDescription(string description)
        {
            description = description.ToLower().Trim();
            return this.FirstOrDefault(fee => fee.FeeDesc.ToLower().Trim().Equals(description));
        }

        public void SortByName()
        {
            Sort((x, y) => x.FeeDesc.CompareTo(y.FeeDesc));
        }

        public void SortByStored()
        {
            Sort((x, y) => x.IsStored.CompareTo(y.IsStored));
        }

        public FuelOrderFeesCollection GetAllWithPricingData()
        {
            FuelOrderFeesCollection result = new FuelOrderFeesCollection();
            foreach (FuelOrderFees fee in this)
            {
                if (!fee.HasPricingData())
                    continue;
                result.Add(fee);
            }
            return result;
        }

        public void UpdateAll(int fuelOrderID)
        {
            FuelOrderFees.DeleteAll(fuelOrderID);
            foreach (FuelOrderFees fee in this)
                fee.FuelOrderID = fuelOrderID;
            ExecutionHelper.BulkInsert(this.GetAllWithPricingData().AsDataTable(), "dbo.FuelOrderFees", ExecutionHelper.GetConnectionString());
        }

        public DataTable AsDataTable()
        {
            DataTable result = GetAsDatabaseTable();
            foreach (FuelOrderFees fee in this)
            {
                DataRow row = result.NewRow();
                row["fuelOrderID"] = fee.FuelOrderID;
                row["feeDesc"] = fee.FeeDesc;
                row["feeAmount"] = fee.FeeAmount;
                result.Rows.Add(row);
            }
            return result;
        }

        public void Delete(int id)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Id", id));
            ExecutionHelper.ExecuteNonQuery("up_Delete_FuelOrderFees", parameters);
        }
        #endregion

        #region Private Methods
        private DataTable GetAsDatabaseTable()
        {
            DataTable result = new DataTable();
            result.Columns.Add("fuelOrderID", Type.GetType("System.Int32"));
            result.Columns.Add("feeDesc", Type.GetType("System.String"));
            result.Columns.Add("feeAmount", Type.GetType("System.Double"));
            return result;
        }
        #endregion
    }
}
