using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using VFMClasses;

namespace VFM.Web.Services
{
    /// <summary>
    /// Summary description for FBODetailVendorExclusionsService
    /// </summary>
    [WebService(Namespace = "http://www.fuelerlinx.com")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class FBODetailVendorExclusionsService : VFMWebService
    {
        #region Web Methods
        [WebMethod(EnableSession = true)]
        public static int UpdateExclusion(object fuelOrderOBJ)
        {
            FBODetailVendorExclusions fuelOrder = new FBODetailVendorExclusions();
            fuelOrder.Clone(fuelOrderOBJ);
            fuelOrder.Update();
            return fuelOrder.Id;
        }

        [WebMethod(EnableSession = true)]
        public static FBODetailVendorExclusions GetExclusion(int id)
        {
            return FBODetailVendorExclusions.GetExclusion(id);
        }

        [WebMethod(EnableSession = true)]
        public static List<FBODetailVendorExclusions> GetExclusions()
        {
            return FBODetailVendorExclusions.LoadList();
        }

        [WebMethod(EnableSession = true)]
        public static List<FBODetailVendorExclusions> GetExclusionsByCustClient(int custId, int adminId)
        {
            return FBODetailVendorExclusions.GetExclusionsByFBO(custId, adminId);
        }

        [WebMethod(EnableSession = true)]
        public static void DeleteExclusion(int id)
        {
            FBODetailVendorExclusions.Delete(id);
        }
        #endregion
    }
}