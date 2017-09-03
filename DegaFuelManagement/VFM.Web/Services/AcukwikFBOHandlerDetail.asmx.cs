using Degatech.Common;
using Newtonsoft.Json;
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
    /// Summary description for AcukwikFBOHandlerDetail
    /// </summary>
    [WebService(Namespace = "http://www.fuelerlinx.com")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    //[System.Web.Script.Services.ScriptService]
    public class AcukwikFBOHandlerDetailService : VFMWebService
    {
        #region Web Methods
        //[WebMethod(EnableSession = true)]
        //public static int UpdateDetail(object airportOBJ)
        //{
        //    AcukwikFBOHandlerDetail airport = new AcukwikFBOHandlerDetail();
        //    airport.Clone(airportOBJ);
        //    airport.Update();
        //    return airport.Id;
        //}

        [WebMethod(EnableSession = true)]
        public static AcukwikFBOHandlerDetail GetDetail(int id)
        {
            return AcukwikFBOHandlerDetail.GetAcukwikFBOHandlerDetail(id);
        }

        [WebMethod(EnableSession = true)]
        public static List<AcukwikFBOHandlerDetail> GetListOfAcukwikFBOHandlerDetail()
        {
            return AcukwikFBOHandlerDetail.LoadList();
        }
        #endregion
    }
}