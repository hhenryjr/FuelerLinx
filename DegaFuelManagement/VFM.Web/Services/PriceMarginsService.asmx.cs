using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using VFMClasses;

namespace VFM.Web.Services
{
    /// <summary>
    /// Summary description for PriceMarginsService
    /// </summary>
    [WebService(Namespace = "http://www.fuelerlinx.com")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class PriceMarginsService : VFMWebService
    {
        #region Web Methods
        [WebMethod(EnableSession = true)]
        public static int UpdatePriceMargin(object marginOBJ)
        {
            PriceMargins margin = new PriceMargins();
            margin.Clone(marginOBJ);
            margin.Update();
            return margin.Id;
        }

        [WebMethod(EnableSession = true)]
        public static PriceMargins GetPriceMargin(int id)
        {
            PriceMargins margin = new PriceMargins(id);
            margin.PriceMarginTiers = new PriceMarginTiersCollection();
            margin.PriceMarginTiers.LoadByPriceMarginID(id);
            return margin;
        }

        [WebMethod(EnableSession = true)]
        public static List<PriceMargins> GetListOfPriceMargins()
        {
            return PriceMargins.LoadList();
        }

        [WebMethod(EnableSession = true)]
        public static List<PriceMargins> GetPriceMarginsByAdmin(int adminId)
        {
            return PriceMargins.LoadListByAdmin(adminId);
        }

        [WebMethod(EnableSession = true)]
        public static void DeletePriceMargin(int id)
        {
            PriceMargins.DeletePriceMargin(id);
        }
        #endregion
    }
}
