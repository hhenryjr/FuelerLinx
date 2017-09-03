using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using VFMClasses;

namespace VFM.Web.Services
{
    /// <summary>
    /// Summary description for CustomerDetailVendorExclusionsService
    /// </summary>
    [WebService(Namespace = "http://www.fuelerlinx.com")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class CustomerDetailVendorExclusionsService : VFMWebService
    {
        #region Web Methods
        [WebMethod(EnableSession = true)]
        public static int UpdateExclusion(object fuelOrderOBJ)
        {
            CustomerDetailVendorExclusions fuelOrder = new CustomerDetailVendorExclusions();
            fuelOrder.Clone(fuelOrderOBJ);
            fuelOrder.Update();
            return fuelOrder.Id;
        }

        [WebMethod(EnableSession = true)]
        public static CustomerDetailVendorExclusions GetExclusion(int id)
        {
            return CustomerDetailVendorExclusions.GetExclusion(id);
        }

        [WebMethod(EnableSession = true)]
        public static List<CustomerDetailVendorExclusions> GetExclusions()
        {
            return CustomerDetailVendorExclusions.LoadList();
        }

        [WebMethod(EnableSession = true)]
        public static List<CustomerDetailVendorExclusions> GetExclusionsByCustClient(int custId, int adminId)
        {
            return CustomerDetailVendorExclusions.GetExclusionsByCustClient(custId, adminId);
        }

        [WebMethod(EnableSession = true)]
        public static void DeleteExclusion(int id)
        {
            CustomerDetailVendorExclusions.Delete(id);
        }
        #endregion
    }
}