using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using VFMClasses;

namespace VFM.Web.Services
{
    /// <summary>
    /// Summary description for EmailRoutingService
    /// </summary>
    [WebService(Namespace = "http://www.fuelerlinx.com")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class EmailRoutingService : VFMWebService
    {
        #region Web Methods
        [WebMethod(EnableSession = true)]
        public static int UpdateEmail(object emailOBJ)
        {
            EmailRouting email = new EmailRouting();
            email.Clone(emailOBJ);
            email.Update();
            return email.Id;
        }

        [WebMethod(EnableSession = true)]
        public static EmailRouting GetEmail(int id)
        {
            return EmailRouting.GetEmailRouting(id);
        }

        [WebMethod(EnableSession = true)]
        public static List<EmailRouting> GetListOfEmails()
        {
            return EmailRouting.LoadList();
        }

        [WebMethod(EnableSession = true)]
        public static EmailRoutingCollection GetEmailsByAdminClient(int clientId)
        {
            EmailRoutingCollection email = new EmailRoutingCollection();
            email.LoadByAdminClientID(clientId);
            return email;
        }

        [WebMethod(EnableSession = true)]
        public static void DeleteEmail(int id)
        {
            EmailRouting.DeleteEmail(id);
        }
        #endregion
    }
}
