using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using VFM.Web.Services;
using VFMClasses;
using Degatech.Common;

namespace VFM.Web.Services
{
    /// <summary>
    /// Summary description for AcukwikAirports
    /// </summary>
    [WebService(Namespace = "http://www.fuelerlinx.com")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    //[System.Web.Script.Services.ScriptService]
    public class AcukwikAirportsService : VFMWebService
    {
        #region Web Methods
        //[WebMethod(EnableSession = true)]
        //public static int UpdateAirport(object airportOBJ)
        //{
        //    AcukwikAirports airport = new AcukwikAirports();
        //    airport.Clone(airportOBJ);
        //    airport.Update();
        //    return airport.Id;
        //}

        [WebMethod(EnableSession = true)]
        public static AcukwikAirports GetAirport(int id)
        {
            return AcukwikAirports.GetAcukwikAirport(id);
        }

        [WebMethod(EnableSession = true)]
        public static List<AcukwikAirports> GetListOfAcukwikAirports()
        {
            return AcukwikAirports.LoadList();
        }

        [WebMethod(EnableSession = true)]
        public static List<AcukwikAirports> GetAcukwikAirportsAndMarginsByAdminClientID(int adminId)
        {
            return AcukwikAirports.LoadAirportsAndMarginsByAdminClientID(adminId);
        }

        [WebMethod(EnableSession = true)]
        public static string GetAirportAutoCompleteListByAdminClientID(int adminId)
        {
            return JsonConvert.SerializeObject(AcukwikAirportAutoCompleteDataSource.GetAirportDataSourceFromCache(adminId));
        }
        #endregion
    }
}