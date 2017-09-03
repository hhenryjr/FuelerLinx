using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using VFMClasses;

namespace VFM.Web.Services
{
    /// <summary>
    /// Summary description for VendorsService
    /// </summary>
    [WebService(Namespace = "http://www.fuelerlinx.com")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class VendorsService : VFMWebService
    {
        #region Web Methods
        [WebMethod(EnableSession = true)]
        public static int UpdateVendor(object fuelOrderOBJ)
        {
            Vendors fuelOrder = new Vendors();
            fuelOrder.Clone(fuelOrderOBJ);
            fuelOrder.Update();
            return fuelOrder.Id;
        }

        [WebMethod(EnableSession = true)]
        public static Vendors GetVendor(int id)
        {
            return Vendors.GetVendor(id);
        }

        [WebMethod(EnableSession = true)]
        public static List<Vendors> GetListOfVendors()
        {
            return Vendors.LoadList();
        }

        [WebMethod(EnableSession = true)]
        public static List<Vendors> GeVendorsByAdminClient(int adminId)
        {
            return Vendors.GetVendorsByAdminClient(adminId);
        }

        [WebMethod(EnableSession = true)]
        public static void DeleteVendor(int id)
        {
            Vendors.Delete(id);
        }
        #endregion
    }
}