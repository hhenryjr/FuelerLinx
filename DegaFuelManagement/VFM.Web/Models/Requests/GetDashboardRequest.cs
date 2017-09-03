using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using VFMClasses.Analysis.ReportFilters;

namespace VFM.Web.Models.Requests
{
    public class GetDashboardRequest : ReportFilterBase
    {
        public string ClientName { get; set; }
    }
}