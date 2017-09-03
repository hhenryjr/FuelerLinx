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
    /// Summary description for ContactUsService
    /// </summary>
    [WebService(Namespace = "http://www.fuelerlinx.com")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class ContactUsService : VFMWebService
    {
        #region Web Methods
        [WebMethod(EnableSession = true)]
        public static int UpdateContactUs(object contactUsOBJ)
        {
            ContactUs contactUs = new ContactUs();
            contactUs.SetProperties((Dictionary<string, object>)contactUsOBJ);
            contactUs.Update();
            return contactUs.Id;
        }

        [WebMethod(EnableSession = true)]
        public static ContactUs GetContactUs(int id)
        {
            return ContactUs.GetContactUs(id);
        }

        [WebMethod(EnableSession = true)]
        public static List<ContactUs> GetListOfContactUs()
        {
            return ContactUs.LoadList();
        }

        [WebMethod(EnableSession = true)]
        public static void DeleteContactUs(int id)
        {
            ContactUs.DeleteContactUs(id);
        }
        #endregion
    }
}
