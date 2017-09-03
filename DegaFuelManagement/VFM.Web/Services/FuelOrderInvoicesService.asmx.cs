using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using VFMClasses.FuelOrder;

namespace VFM.Web.Services
{
    /// <summary>
    /// Summary description for FuelOrderInvoicesService
    /// </summary>
    [WebService(Namespace = "http://www.fuelerlinx.com")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class FuelOrderInvoicesService : VFMWebService
    {
        [WebMethod(EnableSession = true)]
        public static void DownloadInvoice(int id)
        {
            FuelOrderInvoices invoice = new FuelOrderInvoices();
            invoice.DownloadAttachment(HttpContext.Current, id);
        }

        [WebMethod(EnableSession = true)]
        public static List<FuelOrderInvoices> GetInvoicesByFuelOrder(int fuelOrderId)
        {
            return FuelOrderInvoices.GetInvoicesByFuelOrder(fuelOrderId);
        }

        [WebMethod(EnableSession = true)]
        public static List<FuelOrderInvoices> DeleteInvoicesByFuelOrder(int fuelOrderId)
        {
            return FuelOrderInvoices.DeleteInvoicesByFuelOrder(fuelOrderId);
        }

        [WebMethod(EnableSession = true)]
        public static List<FuelOrderInvoices> DeleteInvoice(int id)
        {
            return FuelOrderInvoices.DeleteInvoice(id);
        }
    }
}
