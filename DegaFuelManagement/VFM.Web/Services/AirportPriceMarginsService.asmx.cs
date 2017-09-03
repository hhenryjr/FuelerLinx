using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using VFMClasses;

namespace VFM.Web.Services
{
    /// <summary>
    /// Summary description for AirportPriceMarginService
    /// </summary>
    [WebService(Namespace = "http://www.fuelerlinx.com")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class AirportPriceMarginsService : VFMWebService
    {
        #region Web Methods
        [WebMethod(EnableSession = true)]
        public static int UpdateAirportPriceMargin(object marginOBJ)
        {
            AirportPriceMargins margin = new AirportPriceMargins();
            margin.Clone(marginOBJ);
            margin.Update();
            return margin.AdminClientID;
        }

        [WebMethod(EnableSession = true)]
        public static AirportPriceMargins GetAirportPriceMargin(int id)
        {
            return AirportPriceMargins.GetAirportPriceMargin(id);
        }

        [WebMethod(EnableSession = true)]
        public static AirportPriceMarginCollection GetAirportPriceMarginsByAdminClientID(int id)
        {
            return AirportPriceMargins.LoadListByAdmin(id);
        }

        [WebMethod(EnableSession = true)]
        public static List<AirportPriceMargins> GetListOfAirportPriceMargins()
        {
            return AirportPriceMargins.LoadList();
        }

        [WebMethod(EnableSession = true)]
        public static void DeleteAirportPriceMargin(int id)
        {
            AirportPriceMargins.DeleteAirportPriceMargin(id);
        }
        #endregion
    }
}
