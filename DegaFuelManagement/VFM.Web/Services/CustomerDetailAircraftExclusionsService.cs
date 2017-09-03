using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using VFMClasses;

namespace VFM.Web.Services
{
    /// <summary>
    /// Summary description for CustomerDetailAircraftExclusionsService
    /// </summary>
    [WebService(Namespace = "http://www.fuelerlinx.com")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class CustomerDetailAircraftExclusionsService : VFMWebService
    {
        #region Web Methods
        [WebMethod(EnableSession = true)]
        public static int UpdateExclusion(object exclusionOBJ)
        {
            CustomerDetailAircraftExclusions exclusion = new CustomerDetailAircraftExclusions();
            exclusion.Clone(exclusionOBJ);
            exclusion.Update();
            return exclusion.Id;
        }

        [WebMethod(EnableSession = true)]
        public static CustomerDetailAircraftExclusions GetExclusion(int id)
        {
            return CustomerDetailAircraftExclusions.GetExclusion(id);
        }

        [WebMethod(EnableSession = true)]
        public static List<CustomerDetailAircraftExclusions> GetExclusions()
        {
            return CustomerDetailAircraftExclusions.LoadList();
        }

        [WebMethod(EnableSession = true)]
        public static List<CustomerDetailAircraftExclusions> GetExclusionsByCustClient(int custId, int adminId, int vendorId)
        {
            return CustomerDetailAircraftExclusions.GetAircaftExclusions(custId, adminId, vendorId);
        }

        [WebMethod(EnableSession = true)]
        public static void DeleteExclusion(int id)
        {
            CustomerDetailAircraftExclusions.Delete(id);
        }
        #endregion
    }
}