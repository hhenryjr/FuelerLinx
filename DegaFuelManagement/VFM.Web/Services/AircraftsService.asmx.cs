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
    /// Summary description for Aircrafts
    /// </summary>
    [WebService(Namespace = "http://www.fuelerlinx.com")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    //[System.Web.Script.Services.ScriptService]
    public class AircraftsService : VFMWebService
    {
        #region Web Methods
        [WebMethod(EnableSession = true)]
        public static int UpdateAircraft(object aircraftOBJ)
        {
            Aircrafts aircraft = new Aircrafts();
            aircraft.Clone(aircraftOBJ);
            aircraft.Update();
            return aircraft.Id;
        }

        [WebMethod(EnableSession = true)]
        public static Aircrafts GetAircraft(int id)
        {
            return Aircrafts.GetAircraft(id);
        }

        [WebMethod(EnableSession = true)]
        public static List<Aircrafts> GetListOfAircrafts()
        {
            return Aircrafts.LoadList();
        }

        [WebMethod(EnableSession = true)]
        public static List<Aircrafts> GetAircraftsByClientID(int id)
        {
            return Aircrafts.LoadAircraftsByClient(id);
        }

        [WebMethod(EnableSession = true)]
        public static List<Aircrafts> GetAircraftsByCustClientID(int id)
        {
            return Aircrafts.LoadAircraftsByCustClient(id);
        }

        [WebMethod(EnableSession = true)]
        public static List<Aircrafts> GetAircraftsByAdminAndClientID(int adminId, int clientId)
        {
            return Aircrafts.LoadAircraftsByAdminAndClient(adminId, clientId);
        }

        [WebMethod(EnableSession = true)]
        public static List<Aircrafts> GetRemainingAircrafts(int adminId, int clientId, string fbo, string icao)
        {
            AircraftsCollection aircrafts = new AircraftsCollection();
            aircrafts.LoadRemainingByClientID(adminId, clientId, fbo, icao);
            return aircrafts;
        }

        [WebMethod(EnableSession = true)]
        public static void DeleteAircraft(int id)
        {
            Aircrafts.DeleteAircraft(id);
        }
        #endregion
    }
}
