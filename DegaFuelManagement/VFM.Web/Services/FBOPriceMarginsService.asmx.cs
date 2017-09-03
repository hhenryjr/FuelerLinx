using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using VFMClasses;

namespace VFM.Web.Services
{
    /// <summary>
    /// Summary description for FBOPriceMarginsService
    /// </summary>
    [WebService(Namespace = "http://www.fuelerlinx.com")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class FBOPriceMarginsService : VFMWebService
    {
        #region Web Methods
        [WebMethod(EnableSession = true)]
        public static int UpdateFBOPriceMargin(object marginOBJ)
        {
            FBOPriceMargins margin = new FBOPriceMargins();
            margin.Clone(marginOBJ);
            margin.Update();
            return margin.Id;
        }

        [WebMethod(EnableSession = true)]
        public static FBOPriceMargins GetFBOPriceMargin(int id)
        {
            return FBOPriceMargins.GetFBOPriceMargin(id);
        }

        [WebMethod(EnableSession = true)]
        public static FBOPriceMarginsCollection GetFBOPriceMarginsByAdminClientIDAndICAO(int adminId, string icao)
        {
            return FBOPriceMargins.LoadListByAdminAndICAO(adminId, icao);
        }

        [WebMethod(EnableSession = true)]
        public static FBOPriceMarginsCollection GetFBODetails(string icao, string fbo, int clientId)
        {
            //FBOPriceMargins margin = new FBOPriceMargins();
            //margin.GetFBODetailsByICAO_FBO_Admin(icao, fbo, clientId);
            //margin.CustomFields = new FBODetailCustomFieldsCollection();
            //margin.CustomFields.GetCustomFields(fbo, icao, clientId);
            //return margin;
            return FBOPriceMargins.GetFBODetails(icao, fbo, clientId);
        }

        [WebMethod(EnableSession = true)]
        public static FBOPriceMargins GetFBODetailsWithCustomFields(string icao, string fbo, int clientId)
        {
            //FBOPriceMargins margin = new FBOPriceMargins();
            //margin.GetFBODetailsByICAO_FBO_Admin(icao, fbo, clientId);
            //margin.CustomFields = new FBODetailCustomFieldsCollection();
            //margin.CustomFields.GetCustomFields(fbo, icao, clientId);
            //return margin;
            return FBOPriceMargins.GetFBODetailsWithCustomFields(icao, fbo, clientId);
        }

        [WebMethod(EnableSession = true)]
        public static List<FBOPriceMargins> GetListOfFBOPriceMargins()
        {
            return FBOPriceMargins.LoadList();
        }

        [WebMethod(EnableSession = true)]
        public static void DeleteFBOPriceMargin(string icao, string fbo, int clientId, int id)
        {
            FBOPriceMargins.DeleteFBOPriceMargin(icao, fbo, clientId, id);
        }
        #endregion
    }
}
