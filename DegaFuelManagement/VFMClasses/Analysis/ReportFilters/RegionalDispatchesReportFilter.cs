using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFMClasses.Analysis.ReportFilters
{
    public class RegionalDispatchesReportFilter : ReportFilterBase
    {
        #region Properties
        public string MapRegion { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        #endregion
    }
}
