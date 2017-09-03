using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using VFMClasses;

namespace VFM.Web.Services
{
    /// <summary>
    /// Summary description for PriceMarginTiersService
    /// </summary>
    [WebService(Namespace = "http://www.fuelerlinx.com")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class PriceMarginTiersService : VFMWebService
    {
        #region Web Methods
        [WebMethod(EnableSession = true)]
        public static int UpdatePriceMarginTier(object marginOBJ)
        {
            PriceMarginTiers margin = new PriceMarginTiers();
            margin.Clone(marginOBJ);
            margin.Update();
            return margin.Id;
        }

        [WebMethod(EnableSession = true)]
        public static PriceMarginTiers GetPriceMarginTier(int id)
        {
            return PriceMarginTiers.GetPriceMarginTier(id);
        }

        [WebMethod(EnableSession = true)]
        public static List<PriceMarginTiers> GetListOfPriceMarginTiers()
        {
            return PriceMarginTiers.LoadList();
        }

        [WebMethod(EnableSession = true)]
        public static PriceMarginTiersCollection GetTiersByPriceMargin(int marginId)
        {
            return PriceMarginTiers.LoadByPriceMarginID(marginId);
        }

        [WebMethod(EnableSession = true)]
        public static void DeletePriceMarginTier(int id)
        {
            PriceMarginTiers.DeletePriceMarginTier(id);
        }
        #endregion
    }
}
