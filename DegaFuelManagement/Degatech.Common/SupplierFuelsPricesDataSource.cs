using Degatech.Utilities.Cache;
using Degatech.Utilities.SQL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Degatech.Common
{
    public class SupplierFuelsPricesDataSource
    {
        #region Private Members
        private static string _CacheKeyName = "SupplierFuelsPricesDataSource";
        #endregion

        #region Public Methods
        public DataTable GetSupplierFuelsPricesByAdminClinetID(int adminClientId)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@AdminClientID", adminClientId));
            DataSet dataSet = ExecutionHelper.ExecuteDataset("up_Select_SupplierFuelsPricesByAndAdminClientID", parameters, ExecutionHelper.GetConnectionString());
            if (dataSet != null && dataSet.Tables.Count > 0)
                return dataSet.Tables[0];
            return new DataTable();
        }
        #endregion

        #region Static Methods
        public static DataTable GetSupplierFuelsPricesDataSourceFromCache(int adminClientId)
        {
            DataTable result;
            if (!CacheHelper.Exists(_CacheKeyName))
            {
                SupplierFuelsPricesDataSource autoCompleteData = new SupplierFuelsPricesDataSource();
                result = autoCompleteData.GetSupplierFuelsPricesByAdminClinetID(adminClientId);
                if (result != null && result.Rows.Count > 0)
                    CacheHelper.Add(result, _CacheKeyName, 1440);
            }
            else
                CacheHelper.Get(_CacheKeyName, out result);
            ClearCache();
            return result;
        }

        public static void ClearCache()
        {
            CacheHelper.Clear(_CacheKeyName);
        }
        #endregion
    }
}
