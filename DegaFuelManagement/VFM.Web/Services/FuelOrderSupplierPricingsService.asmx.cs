using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using VFMClasses;
using VFMClasses.FuelOrder;

namespace VFM.Web.Services
{
    /// <summary>
    /// Summary description for FuelOrderSupplierPricingsService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class FuelOrderSupplierPricingsService : System.Web.Services.WebService
    {
        [WebMethod(EnableSession = true)]
        public static FuelOrderSupplierPricingsCollection GetFuelOrderSupplierPricingByFuelOrderId(int fuelOrderid)
        {
            FuelOrderSupplierPricingsCollection pricings = new FuelOrderSupplierPricingsCollection();
            pricings.LoadByFuelOrderId(fuelOrderid);
            return pricings;
        }
    }
}
