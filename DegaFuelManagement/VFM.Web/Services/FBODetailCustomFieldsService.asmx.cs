using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using VFMClasses;

namespace VFM.Web.Services
{
    /// <summary>
    /// Summary description for FBODetailCustomFieldsService
    /// </summary>
    [WebService(Namespace = "http://www.fuelerlinx.com")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class FBODetailCustomFieldsService : VFMWebService
    {
        #region Web Methods
        [WebMethod(EnableSession = true)]
        public static int UpdateFBODetailCustomField(object marginOBJ)
        {
            FBODetailCustomFields margin = new FBODetailCustomFields();
            margin.Clone(marginOBJ);
            margin.Update();
            return margin.Id;
        }

        [WebMethod(EnableSession = true)]
        public static FBODetailCustomFields GetFBODetailCustomField(int id)
        {
            return FBODetailCustomFields.GetFBODetailCustomField(id);
        }

        [WebMethod(EnableSession = true)]
        public static FBODetailCustomFieldsCollection GetCustomFields(string fbo, string icao, int clientId)
        {
            return FBODetailCustomFields.GetFBODetailCustomFields(fbo, icao, clientId);
        }

        [WebMethod(EnableSession = true)]
        public static void DeleteFBODetailCustomField(int id)
        {
            FBODetailCustomFields.DeleteFBODetailCustomField(id);
        }
        #endregion
    }
}
