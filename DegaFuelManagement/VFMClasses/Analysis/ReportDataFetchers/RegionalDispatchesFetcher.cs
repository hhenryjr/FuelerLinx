using System.Data.SqlClient;
using Degatech.Common.Analysis;
using Degatech.Common.Analysis.HighCharts;
using VFMClasses.Analysis.ReportData;

namespace VFMClasses.Analysis.ReportDataFetchers
{
    public class RegionalDispatchesFetcher : IReportDataFetcher
    {
        #region Public Methods
        public SeriesResult GetReportDataSeriesResult(IReportFilter filter)
        {
            SeriesResult result = new SeriesResult();
            result.Name = "Regional Dispatches";
            RegionalDispatchesCollection collection = GetRegionalDispatchesCollection(filter);
            foreach (RegionalDispatches dispatches in collection)
            {
                result.AddDataValueFromReportObject(dispatches, dispatches.Value, dispatches.Label);
            }
            return result;
        }
        #endregion

        private RegionalDispatchesCollection GetRegionalDispatchesCollection(IReportFilter filter)
        {
            RegionalDispatchesCollection collection = new RegionalDispatchesCollection();
            using (
                SqlDataReader reader = Degatech.Utilities.SQL.ExecutionHelper.ExecuteReader("up_Select_Analysis_RegionalDispatches",
                    filter.GetSQLParameters()))
            {
                if (reader == null)
                    return collection;
                while (reader.Read())
                {
                    RegionalDispatches regionalDispatches = new RegionalDispatches();
                    regionalDispatches.SetProperties(reader);
                    collection.Add(regionalDispatches);
                }
            }
            return collection;
        }
    }
}
