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
    /// Summary description for FuelOrdersService
    /// </summary>
    [WebService(Namespace = "http://www.fuelerlinx.com")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class FuelOrdersService : VFMWebService
    {
        #region Web Methods
        [WebMethod(EnableSession = true)]
        public static int UpdateFuelOrder(object fuelOrderOBJ)
        {
            FuelOrders fuelOrder = new FuelOrders();
            fuelOrder.Clone(fuelOrderOBJ);
            fuelOrder.Update();
            return fuelOrder.Id;
        }

        [WebMethod(EnableSession = true)]
        public static FuelOrders GetFuelOrder(int id)
        {
            FuelOrders order = new FuelOrders(id);
            //order.FuelOrderTaxes = new FuelOrderTaxesCollection();
            //order.FuelOrderNotes = new FuelOrderNotesCollection();
            //order.FuelOrderNotes.LoadByFuelOrder(id);
            order.Aircraft = new Aircrafts(order.AircraftID);
            return order;
        }

        [WebMethod(EnableSession = true)]
        public static List<FuelOrders> GetListOfFuelOrders()
        {
            return FuelOrders.LoadList();
        }

        [WebMethod(EnableSession = true)]
        public static List<FuelOrders> GetFuelOrdersByAdminClient(int clientId, DateTime startDate, DateTime endDate)
        {
            return FuelOrders.LoadListByAdminClient(clientId, startDate, endDate);
        }

        [WebMethod(EnableSession = true)]
        public static List<FuelOrders> GetFuelOrdersByCustClient(int clientId, DateTime startDate, DateTime endDate)
        {
            return FuelOrders.LoadListByCustClient(clientId, startDate, endDate);
        }

        [WebMethod(EnableSession = true)]
        public static void DeleteFuelOrder(int id)
        {
            FuelOrders.DeleteFuelOrder(id);
        }
        #endregion
    }
}
