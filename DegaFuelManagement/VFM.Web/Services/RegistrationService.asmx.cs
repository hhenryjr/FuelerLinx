using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using VFM.Web.Services;
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
    public class RegistrationService : VFMWebService
    {
        #region Web Methods
        [WebMethod(EnableSession = true)]
        public static int UpdateRegistration(object regOBJ)
        {
            Registration reg = new Registration();
            reg.Clone(regOBJ);
            reg.Update();
            return reg.Id;
        }

        [WebMethod(EnableSession = true)]
        public static bool CheckUsername(int id, string username)
        {
            Registration reg = new Registration();
            reg.Id = id;
            reg.Username = username;
            reg.CheckUsername();
            return reg.IsDuplicateUsername;
        }

        [WebMethod(EnableSession = true)]
        public static Registration GetRegistration(int id)
        {
            return Registration.GetRegistration(id);
        }

        [WebMethod(EnableSession = true)]
        public static Registration GetDetailedRegInfo(int Id)
        {
            Registration reg = new Registration(Id);
            reg.Users = new UsersCollection();
            reg.Users.LoadByRegID(Id);
            return reg;
        }

        [WebMethod(EnableSession = true)]
        public static List<Registration> GetRegistrationList()
        {
            return Registration.LoadList();
        }

        [WebMethod(EnableSession = true)]
        public static void DeleteRegistration(int id)
        {
            Registration.DeleteRegistration(id);
        }
        #endregion
    }
}
