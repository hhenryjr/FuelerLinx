﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using VFMClasses;

namespace VFM.Web.Services
{
    /// <summary>
    /// Summary description for RegistrationService
    /// </summary>
    [WebService(Namespace = "http://www.fuelerlinx.com")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class SiteSettingsService : VFMWebService
    {
        [WebMethod(EnableSession = true)]
        public static SiteSettings GetMap(string map)
        {
            return SiteSettings.GetMapSettings(map);
        }

        [WebMethod(EnableSession = true)]
        public static SiteSettings GetAllSettings()
        {
            return SiteSettings.GetAllSettings();
        }
    }
}
