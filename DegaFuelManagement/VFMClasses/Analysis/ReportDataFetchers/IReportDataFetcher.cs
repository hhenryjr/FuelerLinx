using Degatech.Common.Analysis;
using Degatech.Common.Analysis.HighCharts;

namespace VFMClasses.Analysis.ReportDataFetchers
{
    public interface IReportDataFetcher
    {
        SeriesResult GetReportDataSeriesResult(IReportFilter filter);
    }
}
