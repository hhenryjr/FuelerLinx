using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VFM.Web.Models.Requests
{
    public class PartnerServiceFuelOrderInvoiceRequest
    {
        public Guid AccountId { get; set; }
        public int FuelOrderID { get; set; }
        public byte[] InvoiceData { get; set; }
        public string InvoiceName { get; set; }
        public string ContentType { get; set; }
        public string ProcessName { get; set; }
    }
}