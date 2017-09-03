using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using VFMClasses;
using Degatech.Common;

namespace VFM.Web.Services
{
    /// <summary>
    /// Summary description for SupplierFuelsPricesService
    /// </summary>
    [WebService(Namespace = "http://www.fuelerlinx.com")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class SupplierFuelsPricesService : VFMWebService
    {
        #region Web Methods
        [WebMethod(EnableSession = true)]
        public static int UpdateSupplierFuelsPrice(object fuelOrderOBJ)
        {
            SupplierFuelsPrices fuelOrder = new SupplierFuelsPrices();
            fuelOrder.Clone(fuelOrderOBJ);
            fuelOrder.Update();
            return fuelOrder.Id;
        }

        [WebMethod(EnableSession = true)]
        public static SupplierFuelsPrices GetSupplierFuelsPrice(int id)
        {
            return SupplierFuelsPrices.GetSupplierFuelsPrice(id);
            //SupplierFuelsPrices order = new SupplierFuelsPrices(id);
            //return order;
        }

        [WebMethod(EnableSession = true)]
        public static List<SupplierFuelsPrices> GetListOfSupplierFuelsPrices()
        {
            return SupplierFuelsPrices.LoadList();
        }

        [WebMethod(EnableSession = true)]
        public static List<SupplierFuelsPrices> GetSupplierFuelsPricesByAdminClientID(int adminId)
        {
            return SupplierFuelsPrices.GetSupplierFuelsPriceByAdmin(adminId);
        }

        [WebMethod(EnableSession = true)]
        public static void DeleteSupplierFuelsPrice(int adminId, int supplierId)
        {
            SupplierFuelsPrices.Delete(adminId, supplierId);
        }

        [WebMethod(EnableSession = true)]
        public static SupplierFuelsPricesCollection GetSupplierFuelsPricesByICAO(int adminId, string icao)
        {
            SupplierFuelsPricesCollection result = new SupplierFuelsPricesCollection();
            result.LoadByICAO(adminId, icao);
            return result;
        }
        #endregion
    }
}
