using System.Web.Services;
using Degatech.Common.Analysis.HighCharts;
using VFMClasses.Analysis.ReportDataFetchers;
using VFMClasses.Analysis.ReportFilters;

namespace VFM.Web.Services
{
    /// <summary>
    /// Summary description for AnalysisService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class AnalysisService : VFMWebService
    {
        #region Web Methods
        [WebMethod (EnableSession = true)]
        public static SeriesResult GetRegionalDispatches(RegionalDispatchesReportFilter filter)
        {
            RegionalDispatchesFetcher dataFetcher = new RegionalDispatchesFetcher();
            return dataFetcher.GetReportDataSeriesResult(filter);
        }
        #endregion
    }
}
