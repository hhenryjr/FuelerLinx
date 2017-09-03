using System.Net;
using System.Net.Http;
using System.Web.Http;
using Degatech.Common.Analysis.HighCharts;
using VFM.Web.Models.Responses;
using VFM.Web.Services;
using VFMClasses.Analysis.ReportFilters;

namespace VFM.Web.Controllers.API
{
    [RoutePrefix("api/analysis")]
    public class AnalysisAPIController : ApiController
    {
        [Route("RegionalDispatches"), HttpPost]
        public string GetRegionalDispatchData(RegionalDispatchesReportFilter model)
        {
            if (!ModelState.IsValid)
            {
                return "";
            }
            
            return Newtonsoft.Json.JsonConvert.SerializeObject(AnalysisService.GetRegionalDispatches(model));
        }
    }
}