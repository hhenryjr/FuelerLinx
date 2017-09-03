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
    /// Summary description for FuelOrderFeesService
    /// </summary>
    [WebService(Namespace = "http://www.fuelerlinx.com")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class FuelOrderFeesService : VFMWebService
    {
        #region Web Methods
        [WebMethod(EnableSession = true)]
        public static FuelOrderFees UpdateFee(object feeOBJ)
        {
            FuelOrderFees fee = new FuelOrderFees();
            fee.Clone(feeOBJ);
            fee.Update();
            return fee;
        }

        [WebMethod(EnableSession = true)]
        public static FuelOrderFees GetFee(int fuelOrderId)
        {
            return new FuelOrderFees(fuelOrderId);
        }

        [WebMethod(EnableSession = true)]
        public static List<FuelOrderFees> GetListOfFuelOrderFees(int fuelOrderId)
        {
            return FuelOrderFees.LoadList(fuelOrderId);
        }

        //[WebMethod(EnableSession = true)]
        //public static List<FuelOrderFees> GetFuelOrderFeesByAdminClient(int clientId)
        //{
        //    return FuelOrderFees.LoadListByAdminClient(clientId);
        //}

        [WebMethod(EnableSession = true)]
        public static void DeleteAllFees(int fuelOrderId)
        {
            FuelOrderFees.DeleteFuelOrderFee(fuelOrderId);
        }
        #endregion
    }
}
