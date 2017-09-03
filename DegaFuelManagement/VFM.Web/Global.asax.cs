using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.SessionState;
using Degatech.Utilities.Exceptions;

namespace VFM.Web
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            ErrorLog errorLog = new ErrorLog();
            try
            {
                System.Exception ex = HttpContext.Current.Server.GetLastError();
                errorLog.Ex = ex;
                if (ex.Message.ToLower().Contains("does not exist"))
                    errorLog.LogErrorInDatabase();
                else
                    errorLog.LogError();
            }
            catch (System.Exception exception)
            {
                errorLog.Ex = exception;
                errorLog.URL = "Issue logging error in Global.asax";
                errorLog.UserID = 0;
                errorLog.SendErrorEmail();
            }
        }

        protected void Application_PostAuthorizeRequest()
        {
            if (IsWebApiRequest())
            {
                HttpContext.Current.SetSessionStateBehavior(SessionStateBehavior.Required);
            }
        }

        private bool IsWebApiRequest()
        {
            return HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath.StartsWith(WebApiConfig.UrlPrefixRelative);
        }
    }
}
