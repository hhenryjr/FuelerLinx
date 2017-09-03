using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using VFMClasses;

namespace VendorFuelManagement.Services
{
    /// <summary>
    /// Summary description for FuelOrdersService
    /// </summary>
    [WebService(Namespace = "http://www.fuelerlinx.com")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class FuelOrdersService : VFMWebService
    {
        #region Web Methods
        [WebMethod(EnableSession = true)]
        public int UpdateFuelOrder(object fuelOrderOBJ)
        {
            FuelOrders fuelOrder = new FuelOrders();
            fuelOrder.SetProperties((Dictionary<string, object>)fuelOrderOBJ);
            fuelOrder.Update();
            return fuelOrder.Id;
        }

        [WebMethod(EnableSession = true)]
        public FuelOrders GetFuelOrder(int id)
        {
            return FuelOrders.GetFuelOrder(id);
        }

        [WebMethod(EnableSession = true)]
        public List<FuelOrders> GetListOfFuelOrders()
        {
            return FuelOrders.LoadList();
        }

        [WebMethod(EnableSession = true)]
        public void DeleteFuelOrder(int id)
        {
            FuelOrders.DeleteFuelOrder(id);
        }
        #endregion
    }
}
