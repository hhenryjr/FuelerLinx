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
    public class AircraftDataAutoCompleteDataSource
    {
        #region Private Members
        private static string _CacheKeyName = "AircraftDataAutoCompleteDataSource";
        #endregion

        #region Public Methods
        public DataTable GetAircraftData()
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            DataSet dataSet = ExecutionHelper.ExecuteDataset("up_Select_AircraftDatasAll", parameters, ExecutionHelper.GetConnectionString());
            if (dataSet != null && dataSet.Tables.Count > 0)
                return dataSet.Tables[0];
            return new DataTable();
        }
        #endregion

        #region Static Methods
        public static DataTable GetAircraftDataSourceFromCache()
        {
            DataTable result;
            if (!CacheHelper.Exists(_CacheKeyName))
            {
                AircraftDataAutoCompleteDataSource autoCompleteData = new AircraftDataAutoCompleteDataSource();
                result = autoCompleteData.GetAircraftData();
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
