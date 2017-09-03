using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace VFM.Web.Services
{
    public class VFMWebService : WebService
    {
        public static void RunPreServiceAuthenticationCheck()
        {
            //if (!UserVars.CurrentUser.IsLoggedIn() && !UserVars.CurrentUser.Admin)
            //    throw new Exception("This service is only available to logged in users.");
        }

        public VFMWebService()
        {
            if (Degatech.Utilities.Session.SessionHelper.IsSessionAccessible())
                RunPreServiceAuthenticationCheck();
        }
    }
}