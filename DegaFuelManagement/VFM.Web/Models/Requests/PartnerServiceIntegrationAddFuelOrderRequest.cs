using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VFM.Web.Models.Requests
{
    public class PartnerServiceIntegrationAddFuelOrderRequest : AddFuelOrdersRequest
    {
        public override int AdminClientID { get; set; }

        public override int CustClientID { get; set; }

        public override int AircraftID { get; set; }

        public Guid AccountId { get; set; }

        public string ProcessName { get; set; }
    }
}