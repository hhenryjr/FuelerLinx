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
    /// Summary description for AircraftDataService
    /// </summary>
    [WebService(Namespace = "http://www.fuelerlinx.com")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class AircraftDataService : VFMWebService
    {
        #region Web Methods
        //[WebMethod(EnableSession = true)]
        //public static int UpdateAircraft(object aircraftOBJ)
        //{
        //    AircraftData aircraft = new AircraftData();
        //    aircraft.Clone(aircraftOBJ);
        //    aircraft.Update();
        //    return aircraft.AircraftID;
        //}

        [WebMethod(EnableSession = true)]
        public static AircraftData GetAircraftData(int id)
        {
            return AircraftData.GetAircraftData(id);
        }

        [WebMethod(EnableSession = true)]
        public static string GetListOfAircraftData()
        {
            return JsonConvert.SerializeObject(AircraftDataAutoCompleteDataSource.GetAircraftDataSourceFromCache());
        }

        [WebMethod(EnableSession = true)]
        public static void DeleteAircraftData(int id)
        {
            AircraftData.DeleteAircraftData(id);
        }
        #endregion
    }
}