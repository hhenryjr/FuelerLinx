using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Degatech.Common;
using Degatech.Utilities.SQL;


namespace VFMClasses.SchedulingSheets
{
    public class SchedulingGenerateFuelOrders : BaseClass
    {
        public int ID { get; set; }
        public int SchedulingPlatform { get; set; }
        public int AdminClientID { get; set; }
        public int CustClientID { get; set; }
        public string TripNumber { get; set; }
        public string TailNumber { get; set; }
        public string ICAO { get; set; }
        public string FBO { get; set; }
        public DateTime Arrival { get; set; }
        public DateTime ImportDate { get; set; }

        public bool GetTrips()
        {
            SchedulingGenerateFuelOrdersCollection schedulingImportsCollection =
                new SchedulingGenerateFuelOrdersCollection();
            schedulingImportsCollection.LoadAll(SchedulingPlatform, AdminClientID, CustClientID);

            AircraftsCollection aircrafts = new AircraftsCollection();
            aircrafts.LoadByAdminAndCustClientID(AdminClientID, CustClientID);

            foreach (SchedulingGenerateFuelOrders schedulingImport in schedulingImportsCollection)
            {
                if (aircrafts.GetTailNumber(schedulingImport.TailNumber) != null)
                {
                    FuelOrders fuelOrder = new FuelOrders();
                    fuelOrder.ICAO = schedulingImport.ICAO;
                    fuelOrder.FBO = schedulingImport.FBO;
                    fuelOrder.AdminClientID = schedulingImport.AdminClientID;
                    fuelOrder.CustClientID = schedulingImport.CustClientID;
                    fuelOrder.TailNumber = schedulingImport.TailNumber;
                    fuelOrder.AircraftID = aircrafts.GetTailNumber(fuelOrder.TailNumber).Id;
                    fuelOrder.FuelingDateTime = schedulingImport.Arrival;
                    fuelOrder.QuotedVolume = 1;
                    fuelOrder.DateRequested = DateTime.Now; //aircraftid relationship in databases aren't matching
                    fuelOrder.OrderedByUserID = Users.CurrentUser.Id;

                    FuelOrderPricingsCollection prices = GetQuote(AdminClientID, CustClientID, fuelOrder.ICAO,
                        fuelOrder.TailNumber);
                    if (prices.Count > 0)
                    {
                        fuelOrder.QuotedPPG = prices[0].Price;
                        fuelOrder.Vendor = prices[0].Vendor;
                        fuelOrder.SupplierID = prices[0].SupplierID;
                        fuelOrder.Product = prices[0].Product;
                    }
                    fuelOrder.Update();

                    //FuelOrderDispatchRequestForRelease fuelOrderDispatchConfirmation = new FuelOrderDispatchRequestForRelease();
                    //fuelOrderDispatchConfirmation.SendFuelRequestForRelease(fuelOrder);
                }
            }
            return true;
        }

        private FuelOrderPricingsCollection GetQuote(int adminId, int custClientId, string icao, string tailNumber)
        {
            FuelOrderPricingsCollection result = new FuelOrderPricingsCollection();
            result.GetQuoteByLocation(adminId, custClientId, icao, tailNumber);
            return result;
        }
    }

    public class SchedulingGenerateFuelOrdersCollection : List<SchedulingGenerateFuelOrders>
    {
        #region Public Methods
        public void LoadAll(int SchedulingPlatform, int AdminClientID, int CustClientID)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@SchedulingPlatform", SchedulingPlatform));
            parameters.Add(new SqlParameter("@AdminClientID", AdminClientID));
            parameters.Add(new SqlParameter("@CustClientID", CustClientID));
            using (SqlDataReader reader = ExecutionHelper.ExecuteReader("up_Select_SchedulingImports", parameters))
            {
                if (reader == null)
                    return;
                while (reader.Read())
                {
                    SchedulingGenerateFuelOrders schedulingImports = new SchedulingGenerateFuelOrders();
                    schedulingImports.SetProperties(reader);
                    Add(schedulingImports);
                }
            }
        }

    #endregion
    }
}
