
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VFM.Web.Models.Responses;
using VFM.Web.Services;
using VFMClasses;

namespace VFM.Web.Controllers.API
{
    [RoutePrefix("api/SiteSettings")]
    public class SiteSettingsAPIController : ApiController
    {
        [Route, HttpGet]
        public HttpResponseMessage GetAllSettings()
        {
            ItemResponse<SiteSettings> response = new ItemResponse<SiteSettings>();
            response.Item = SiteSettingsService.GetAllSettings();
            return Request.CreateResponse(response);
        }

        [Route("Map/{map:alpha}"), HttpGet]
        public HttpResponseMessage GetMapSettings(string map)
        {
            ItemResponse<SiteSettings> response = new ItemResponse<SiteSettings>();
            response.Item = SiteSettingsService.GetMap(map);
            return Request.CreateResponse(response);
        }
    }
}
