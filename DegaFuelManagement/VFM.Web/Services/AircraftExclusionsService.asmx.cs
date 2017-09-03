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
    /// Summary description for AircraftExclusions
    /// </summary>
    [WebService(Namespace = "http://www.fuelerlinx.com")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    //[System.Web.Script.Services.ScriptService]
    public class AircraftExclusionsService : VFMWebService
    {
        #region Web Methods
        [WebMethod(EnableSession = true)]
        public static int UpdateAircraftExclusion(object aircraftOBJ)
        {
            AircraftExclusions aircraft = new AircraftExclusions();
            aircraft.Clone(aircraftOBJ);
            aircraft.Update();
            return aircraft.Id;
        }

        [WebMethod(EnableSession = true)]
        public static AircraftExclusions UpdateAircraftExclusions(object aircraftOBJ)
        {
            AircraftExclusions aircraft = new AircraftExclusions();
            aircraft.Clone(aircraftOBJ);
            aircraft.Update();
            return aircraft;
        }

        [WebMethod(EnableSession = true)]
        public static AircraftExclusions GetAircraftExclusion(int id)
        {
            return AircraftExclusions.GetAircraftExclusion(id);
        }

        [WebMethod(EnableSession = true)]
        public static List<AircraftExclusions> GetListOfAircraftExclusions()
        {
            return AircraftExclusions.LoadList();
        }

        [WebMethod(EnableSession = true)]
        public static List<AircraftExclusions> GetAircraftExclusionsByICAOAndFBOAndAdminClient(string icao, string fbo, int clientId)
        {
            return AircraftExclusions.LoadAircraftExclusionsByICAOAndFBOAndAdminClient(icao, fbo, clientId);
        }

        [WebMethod(EnableSession = true)]
        public static void DeleteAircraftExclusion(int id)
        {
            AircraftExclusions.DeleteAircraftExclusion(id);
        }
        #endregion
    }
}
