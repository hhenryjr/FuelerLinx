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
    public class CustomersAutoCompleteDataSource
    {
        #region Private Members
        private static string _CacheKeyName = "CustomersAutoCompleteDataSource";
        #endregion

        #region Public Methods
        public DataTable GetCustomersDataSourceByAdminClinetID(int adminClientId)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@AdminClientID", adminClientId));
            DataSet dataSet = ExecutionHelper.ExecuteDataset("up_Select_CustomerDetailsByAndAdminClientID", parameters, ExecutionHelper.GetConnectionString());
            if (dataSet != null && dataSet.Tables.Count > 0)
                return dataSet.Tables[0];
            return new DataTable();
        }
        #endregion

        #region Static Methods
        public static DataTable GetCustomersDataSourceFromCache(int adminClientId)
        {
            DataTable result;
            if (!CacheHelper.Exists(_CacheKeyName))
            {
                CustomersAutoCompleteDataSource autoCompleteData = new CustomersAutoCompleteDataSource();
                result = autoCompleteData.GetCustomersDataSourceByAdminClinetID(adminClientId);
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
