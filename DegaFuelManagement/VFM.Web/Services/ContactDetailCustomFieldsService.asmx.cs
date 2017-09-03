using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using VFMClasses;

namespace VFM.Web.Services
{
    /// <summary>
    /// Summary description for ContactDetailCustomFieldsService
    /// </summary>
    [WebService(Namespace = "http://www.fuelerlinx.com")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class ContactDetailCustomFieldsService : VFMWebService
    {
        #region Web Methods
        [WebMethod(EnableSession = true)]
        public static int UpdateContactDetailCustomField(object marginOBJ)
        {
            ContactDetailCustomFields margin = new ContactDetailCustomFields();
            margin.Clone(marginOBJ);
            margin.Update();
            return margin.Id;
        }

        [WebMethod(EnableSession = true)]
        public static ContactDetailCustomFields GetContactDetailCustomField(int id)
        {
            return ContactDetailCustomFields.GetContactDetailCustomField(id);
        }

        [WebMethod(EnableSession = true)]
        public static ContactDetailCustomFieldsCollection GetCustomFields(int contactId)
        {
            return ContactDetailCustomFields.GetContactDetailCustomFields(contactId);
        }

        [WebMethod(EnableSession = true)]
        public static void DeleteContactDetailCustomField(int id)
        {
            ContactDetailCustomFields.DeleteContactDetailCustomField(id);
        }
        #endregion
    }
}
