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
    /// Summary description for Airports
    /// </summary>
    [WebService(Namespace = "http://www.fuelerlinx.com")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    //[System.Web.Script.Services.ScriptService]
    public class AirportsService : VFMWebService
    {
        #region Web Methods
        [WebMethod(EnableSession = true)]
        public static int UpdateAirport(object airportOBJ)
        {
            Airports airport = new Airports();
            airport.Clone(airportOBJ);
            airport.Update();
            return airport.Id;
        }

        [WebMethod(EnableSession = true)]
        public static Airports GetAirport(int id)
        {
            return Airports.GetAirport(id);
        }

        [WebMethod(EnableSession = true)]
        public static List<Airports> GetListOfAirports()
        {
            return Airports.LoadList();
        }

        [WebMethod(EnableSession = true)]
        public static string GetAirportAutoCompleteList()
        {
            return JsonConvert.SerializeObject(AirportAutoCompleteDataSource.GetAirportDataSourceFromCache());
        }
        #endregion
    }
}