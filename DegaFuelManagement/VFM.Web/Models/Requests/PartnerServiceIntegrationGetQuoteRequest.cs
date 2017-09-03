using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VFM.Web.Models.Requests
{
    public class PartnerServiceIntegrationGetQuoteRequest : GetQuoteForLocationRequest
    {
        public override int AdminClientID { get; set; }

        public override int CustClientID { get; set; }

        public override string TailNumber { get; set; }

        public Guid AccountId { get; set; }

        public string ProcessName { get; set; }
    }
}