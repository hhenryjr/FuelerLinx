using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using VFMClasses;

namespace VFM.Web.Services
{
    /// <summary>
    /// Summary description for FuelOrderPricingsService
    /// </summary>
    [WebService(Namespace = "http://www.fuelerlinx.com")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class FuelOrderPricingsService : VFMWebService
    {
        #region Web Methods
        [WebMethod(EnableSession = true)]
        public static int UpdateFuelOrderPricing(object fuelOrderOBJ)
        {
            FuelOrderPricings fuelOrder = new FuelOrderPricings();
            fuelOrder.Clone(fuelOrderOBJ);
            fuelOrder.Update();
            return fuelOrder.Id;
        }

        [WebMethod(EnableSession = true)]
        public static FuelOrderPricings GetFuelOrderPricing(int id)
        {
            return FuelOrderPricings.GetFuelOrderPricing(id);
        }

        [WebMethod(EnableSession = true)]
        public static FuelOrderPricings GetFuelOrderPricingByInvoiced(int adminId, int supplierId, int fuelOrderId, int invoiceVolume, string fboName)
        {
            return FuelOrderPricings.GetWholesaleInvoiced(adminId, supplierId, fuelOrderId, invoiceVolume, fboName);
        }

        [WebMethod(EnableSession = true)]
        public static List<FuelOrderPricings> GetListOfFuelOrderPricings()
        {
            return FuelOrderPricings.LoadList();
        }

        [WebMethod(EnableSession = true)]
        public static void DeleteFuelOrderPricing(int id)
        {
            FuelOrderPricings.Delete(id);
        }

        [WebMethod(EnableSession = true)]
        public static FuelOrderPricingsCollection GetQuoteForLocation(int adminId, int custClientId, string icao,
            string tailNumber)
        {
            FuelOrderPricingsCollection result = new FuelOrderPricingsCollection();
            result.GetQuoteByLocation(adminId, custClientId, icao, tailNumber);
            return result;
        }

        [WebMethod(EnableSession = true)]
        public static FuelOrderPricingsCollection GetQuoteForLocationForAllVendors(int adminId, int custClientId, string icao,
            string tailNumber)
        {
            FuelOrderPricingsCollection result = new FuelOrderPricingsCollection();
            result.GetQuoteByLocationForAllVendors(adminId, custClientId, icao, tailNumber);
            return result;
        }
        #endregion
    }
}
