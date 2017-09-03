using System;
using System.Web.Services;
using VFM.Web.Services;
using VFMClasses;

/// <summary>
/// Summary description for MasterSvc
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class MasterSvc : VFMWebService
{
    [WebMethod(EnableSession = true)]
    public static bool IsUserStillLoggedIn()
    {
        if (Users.CurrentUser.Id > 0)
            return true;
        return false;
    }
}

