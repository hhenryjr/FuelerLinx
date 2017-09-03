using System;
using Degatech.Common;
using Degatech.Common.Analysis;

namespace VFMClasses.Analysis.ReportFilters
{
    public class ReportFilterBase : BaseClass, IReportFilter
    {
        #region Properties
        public DateTime StartDateFilter { get; set; } = DatabaseDateTimeMinValue();
        public DateTime EndDateFilter { get; set; } = DatabaseDateTimeMinValue();
        public int ClientID { get; set; }
        #endregion
    }
}