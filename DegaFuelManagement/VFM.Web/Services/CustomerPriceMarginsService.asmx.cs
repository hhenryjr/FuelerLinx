using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Degatech.Common;
using VFMClasses;

namespace VFM.Web.Services
{
    /// <summary>
    /// Summary description for CustomerPriceMarginsService
    /// </summary>
    [WebService(Namespace = "http://www.fuelerlinx.com")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class CustomerPriceMarginsService : VFMWebService
    {
        #region Web Methods
        [WebMethod(EnableSession = true)]
        public static int UpdateCustomerPriceMargin(object marginOBJ)
        {
            CustomerPriceMargins margin = new CustomerPriceMargins();
            margin.Clone(marginOBJ);
            margin.Update();
            return margin.Id;
        }

        [WebMethod(EnableSession = true)]
        public static CustomerPriceMargins GetCustomerPriceMargin(int id)
        {
            return CustomerPriceMargins.GetCustomerPriceMargin(id);
        }

        [WebMethod(EnableSession = true)]
        public static CustomerPriceMargins GetCustomerPriceMarginByCustClientID(int custClientId)
        {
            return CustomerPriceMargins.GetCustomerPriceMarginByCustClientID(custClientId);
        }

        //[WebMethod(EnableSession = true)]
        //public static CustomerPriceMarginsCollection GetCustomerPriceMarginByCustClientIDAndICAO(int customerClientID, string icao)
        //{
        //    return CustomerPriceMargins.GetCustomerPriceMarginByCustClientIDAndICAO(customerClientID, icao);
        //}

        [WebMethod(EnableSession = true)]
        public static CustomerPriceMarginsCollection GetCustomerPriceMarginByClientIDsAndICAO(int customerClientID, int adminClientID, string icao)
        {
            return CustomerPriceMargins.GetCustomerPriceMarginByCustClientIDAndICAO(customerClientID, adminClientID, icao);
        }

        [WebMethod(EnableSession = true)]
        public static CustomerPriceMarginsCollection GetCustomerPriceMarginForAllVendors(int customerClientID, int adminClientID, string icao)
        {
            return CustomerPriceMargins.GetCustomerPriceMarginForAllVendors(customerClientID, adminClientID, icao);
        }

        [WebMethod(EnableSession = true)]
        public static List<CustomerPriceMargins> GetListOfCustomerPriceMargins()
        {
            return CustomerPriceMargins.LoadList();
        }

        [WebMethod(EnableSession = true)]
        public static CustomerPriceMargins UpdatePriceMarginGetHighestMargin(int id, int custClientId, int priceMarginId)
        {
            CustomerPriceMargins margin = new CustomerPriceMargins();
            margin.UpdatePriceMarginAndLoadHighestMargin(id, custClientId, priceMarginId);
            return margin;
        }

        [WebMethod(EnableSession = true)]
        public static void DeleteCustomerPriceMargin(int id)
        {
            CustomerPriceMargins.Delete(id);
        }
        #endregion
    }
}
