using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using VFMClasses;

namespace VFM.Web.Services
{
    /// <summary>
    /// Summary description for FuelOrderCustomerPricingsService
    /// </summary>
    [WebService(Namespace = "http://www.fuelerlinx.com")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class FuelOrderCustomerPricingsService : VFMWebService
    {
        #region Web Methods
        [WebMethod(EnableSession = true)]
        public static int UpdateFuelOrderCustomerPricing(object fuelOrderOBJ)
        {
            FuelOrderCustomerPricings fuelOrder = new FuelOrderCustomerPricings();
            fuelOrder.Clone(fuelOrderOBJ);
            fuelOrder.Update();
            return fuelOrder.Id;
        }

        [WebMethod(EnableSession = true)]
        public static FuelOrderCustomerPricingsCollection GetFuelOrderCustomerPricingByFuelOrderId(int fuelOrderId)
        {
            return FuelOrderCustomerPricings.GetFuelOrderCustomerPricingByFuelOrderId(fuelOrderId);
        }

        [WebMethod(EnableSession = true)]
        public static FuelOrderCustomerPricings GetCustomerInvoiced(int adminId, int supplierId, int fuelOrderId, int invoiceVolume, string fboName)
        {
            return FuelOrderCustomerPricings.GetCustomerInvoiced(adminId, supplierId, fuelOrderId, invoiceVolume, fboName);
        }

        [WebMethod(EnableSession = true)]
        public static List<FuelOrderCustomerPricings> GetListOfFuelOrderCustomerPricings()
        {
            return FuelOrderCustomerPricings.LoadList();
        }

        [WebMethod(EnableSession = true)]
        public static void DeleteFuelOrderCustomerPricing(int id)
        {
            FuelOrderCustomerPricings.Delete(id);
        }
        #endregion
    }
}
