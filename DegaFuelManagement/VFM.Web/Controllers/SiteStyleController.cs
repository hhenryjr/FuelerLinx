using System.Web;
using System.Web.Mvc;

namespace VFM.Web.Controllers
{
    public class SiteStyleController : Controller
    {
        public string CustomSiteCSS()
        {
            if (string.IsNullOrEmpty(VFMClasses.Users.CurrentUser.Client.Name))
                return "";
            string customSiteResult = Server.MapPath("/Content/CustomSites/" + HttpUtility.UrlEncode(VFMClasses.Users.CurrentUser.Client.Name.Replace(" ", "") + ".css"));
            if (!System.IO.File.Exists(customSiteResult))
                customSiteResult = Server.MapPath("/Content/CustomSites/Default.css");
            Response.ContentType = "text/css";
            return System.IO.File.ReadAllText(customSiteResult);
        }
    }
}