using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using VFM.Web.Services;
using VFMClasses;

namespace VFM.Web.Services
{
    /// <summary>
    /// Summary description for FuelOrderTaxesService
    /// </summary>
    [WebService(Namespace = "http://www.fuelerlinx.com")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class FuelOrderTaxesService : VFMWebService
    {
        #region Web Methods
        [WebMethod(EnableSession = true)]
        public static FuelOrderTaxes UpdateTax(object taxOBJ)
        {
            FuelOrderTaxes tax = new FuelOrderTaxes();
            tax.Clone(taxOBJ);
            tax.Update();
            return tax;
        }

        [WebMethod(EnableSession = true)]
        public static FuelOrderTaxes GetTax(int id)
        {
            return new FuelOrderTaxes(id);
        }

        [WebMethod(EnableSession = true)]
        public static List<FuelOrderTaxes> GetListOfFuelOrderTaxes(int fuelOrderId)
        {
            return FuelOrderTaxes.LoadList(fuelOrderId);
        }

        //[WebMethod(EnableSession = true)]
        //public static List<FuelOrderTaxes> GetFuelOrderTaxesByAdminClient(int clientId)
        //{
        //    return FuelOrderTaxes.LoadListByAdminClient(clientId);
        //}

        [WebMethod(EnableSession = true)]
        public static void DeleteFuelOrder(int id)
        {
            FuelOrderTaxes.DeleteFuelOrderTax(id);
        }
        #endregion
    }
}
