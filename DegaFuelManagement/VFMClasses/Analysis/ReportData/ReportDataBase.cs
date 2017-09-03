using Degatech.Common;
using Degatech.Common.Analysis.HighCharts;

namespace VFMClasses.Analysis.ReportData
{
    public class ReportDataPoint : BaseClass, IDataPoint
    {
        #region Properties
        public int Day { get; set; }
        public int Week { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public double Value { get; set; }
        public string Label { get; set; }
        #endregion

    }
}
