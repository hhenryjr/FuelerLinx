using Degatech.Utilities.Cache;
using Degatech.Utilities.SQL;
using System.Data;
using System.Data.SqlClient;
using Degatech.Common.DataSets.AirportsTableAdapters;

namespace Degatech.Common
{
    public class AirportAutoCompleteDataSource
    {
        #region Private Members
        private static string _CacheKeyName = "AirportAutoCompleteDataSource";
        #endregion

        #region Public Methods
        public DataTable GetAirportDataSource()
        {
            AirportAutoCompletionDataTableAdapter adapter = new AirportAutoCompletionDataTableAdapter();
            adapter.Connection = new SqlConnection(ExecutionHelper.GetConnectionString());
            return adapter.GetData();
        }
        #endregion

        #region Static Methods
        public static DataTable GetAirportDataSourceFromCache()
        {
            DataTable result;
            if (!CacheHelper.Exists(_CacheKeyName))
            {
                AirportAutoCompleteDataSource autoCompleteData = new AirportAutoCompleteDataSource();
                result = autoCompleteData.GetAirportDataSource();
                if (result != null && result.Rows.Count > 0)
                    CacheHelper.Add(result, _CacheKeyName, 1440);
            }
            else
                CacheHelper.Get(_CacheKeyName, out result);
            return result;
        }

        public static void ClearCache()
        {
            CacheHelper.Clear(_CacheKeyName);
        }
        #endregion
    }
}
