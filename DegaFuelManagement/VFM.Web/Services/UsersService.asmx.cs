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
    /// Summary description for UsersService
    /// </summary>
    [WebService(Namespace = "http://www.fuelerlinx.com")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class UsersService : VFMWebService
    {
        #region Web Methods

        [WebMethod(EnableSession = true)]
        public static int UpdateUser(object userOBJ)
        {
            Users user = new Users();
            user.Clone(userOBJ);
            user.Update();
            return user.Id;
        }

        [WebMethod(EnableSession = true)]
        public static Users GetUser(int id)
        {
            return Users.GetUser(id);
        }

        [WebMethod(EnableSession = true)]
        public static List<Users> GetListOfUsers()
        {
            return Users.LoadList();
        }

        [WebMethod(EnableSession = true)]
        public static List<Users> GetUsersByClient(int clientId)
        {
            return Users.LoadListByClient(clientId);
        }

        [WebMethod(EnableSession = true)]
        public static void DeleteUser(int id)
        {
            Users.DeleteUserByRegID(id);
        }
        #endregion
    }
}
