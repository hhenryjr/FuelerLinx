using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using VFMClasses;

namespace VFM.Web.Services
{
    /// <summary>
    /// Summary description for CustomerDetailsCustomFieldsService
    /// </summary>
    [WebService(Namespace = "http://www.fuelerlinx.com")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class CustomerDetailsCustomFieldsService : VFMWebService
    {
        #region Web Methods
        [WebMethod(EnableSession = true)]
        public static int UpdateCustomerDetailsCustomField(object marginOBJ)
        {
            CustomerDetailsCustomFields margin = new CustomerDetailsCustomFields();
            margin.Clone(marginOBJ);
            margin.Update();
            return margin.Id;
        }

        [WebMethod(EnableSession = true)]
        public static CustomerDetailsCustomFields GetCustomerDetailsCustomField(int id)
        {
            return CustomerDetailsCustomFields.GetCustomerDetailsCustomField(id);
        }

        [WebMethod(EnableSession = true)]
        public static CustomerDetailsCustomFieldsCollection GetCustomFields(int adminId, int custId)
        {
            return CustomerDetailsCustomFields.GetCustomerDetailsCustomFields(adminId, custId);
        }

        [WebMethod(EnableSession = true)]
        public static void DeleteCustomerDetailsCustomField(int id)
        {
            CustomerDetailsCustomFields.DeleteCustomerDetailsCustomField(id);
        }
        #endregion
    }
}
