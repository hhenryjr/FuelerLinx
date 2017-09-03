using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using VFMClasses;

namespace VFM.Web.Services
{
    /// <summary>
    /// Summary description for AircraftPriceMarginsService
    /// </summary>
    [WebService(Namespace = "http://www.fuelerlinx.com")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class AircraftPriceMarginsService : VFMWebService
    {
        #region Web Methods
        [WebMethod(EnableSession = true)]
        public static int UpdateAircraftPriceMargin(object marginOBJ)
        {
            AircraftPriceMargins margin = new AircraftPriceMargins();
            margin.Clone(marginOBJ);
            margin.Update();
            return margin.AircraftID;
        }

        [WebMethod(EnableSession = true)]
        public static AircraftPriceMargins GetAircraftPriceMargin(int id)
        {
            return AircraftPriceMargins.GetAircraftPriceMargin(id);
        }

        [WebMethod(EnableSession = true)]
        public static AircraftPriceMarginsCollection GetAircraftPriceMarginByAircraftID(int id)
        {
            return AircraftPriceMargins.GetAircraftPriceMarginByAircraftID(id);
        }

        [WebMethod(EnableSession = true)]
        public static List<AircraftPriceMargins> GetListOfAircraftPriceMargins()
        {
            return AircraftPriceMargins.LoadList();
        }

        [WebMethod(EnableSession = true)]
        public static void DeleteAircraftPriceMargin(int id)
        {
            AircraftPriceMargins.Delete(id);
        }
        #endregion
    }
}
