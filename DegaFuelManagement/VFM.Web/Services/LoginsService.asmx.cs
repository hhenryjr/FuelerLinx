using System;
using System.Collections.Generic;
using System.EnterpriseServices.Internal;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Description;
using VFM.Web.Services;
using VFMClasses;

namespace VFM.Web.Services
{
    /// <summary>
    /// Summary description for LoginsService
    /// </summary>
    [WebService(Namespace = "http://www.fuelerlinx.com")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class LoginsService : VFMWebService
    {
        #region Web Methods
        //[WebMethod(EnableSession = true)]
        //public int UpdateLogin(object loginOBJ)
        //{
        //    Logins login = new Logins();
        //    login.SetProperties((Dictionary<string, object>)loginOBJ);
        //    //login.Update();
        //    return login.Id;
        //}

        [WebMethod(EnableSession = true)]
        public static Logins Login(string username, string password)
        {
            //string IPAddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            //if (string.IsNullOrEmpty(IPAddress))
            //{
            //    IPAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            //}

            Logins logins = new Logins();
            logins.AttemptLogin(username, password);            
            //if (logins.LoginResult == InvalidUsernamePassword)
            //if (logins.Attempts == 5)
            //{
            //UserVars.CurrentUser = null;
            //SessionManager.ClearSessionVariables();
            //}
            return logins;
        }

        [WebMethod(EnableSession = true)]
        public static void Logout()
        {
            Users.Logout();
        }

        [WebMethod(EnableSession = true)]
        public static Clients GetCurrentUserClient()
        {
            return Users.CurrentUser.Client;
        }

        [WebMethod(EnableSession = true)]
        public static Logins GetLogin(int id)
        {
            return Logins.GetLogin(id);
        }

        //[WebMethod(EnableSession = true)]
        //public static bool SendContactUsEmail(Dictionary<string, object> contactUsInfoObject)
        //{
        //    ContactUs contactInfo = new ContactUs();
        //    contactInfo.SetProperties(contactUsInfoObject);
        //    //EmailTemplate emailTemplate = new EmailTemplate(new UserVars(), "ContactUs.xml");
        //    //emailTemplate.EmailTo = "support@fuelerlinx.com";
        //    //emailTemplate.SendEmail(contactInfo);
        //    return true;
        //}

        //[WebMethod(EnableSession = true)]
        //public static Logins GetDetailedLoginsInformation(int Id)
        //{
        //    Logins login = new Logins(Id);
        //    login.Users = new UsersCollection();
        //    login.Users.LoadByloginID(Id);
        //    return login;
        //}

        //[WebMethod(EnableSession = true)]
        //public static List<Logins> GetListOfLogins()
        //{
        //    return Logins.LoadList();
        //}

        //[WebMethod(EnableSession = true)]
        //public static void DeleteLogin(int id)
        //{
        //    Logins.DeleteLogin(id);
        //}
        #endregion
    }
}
