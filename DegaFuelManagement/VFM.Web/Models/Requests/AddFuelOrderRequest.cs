using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Degatech.Common;
using VFMClasses;

namespace VFM.Web.Models.Requests
{
    public class AddFuelOrdersRequest 
    {
        [Required]
        public virtual int AdminClientID { get; set; }

        [Required]
        public virtual int CustClientID { get; set; }

        public int OrderedByUserID { get; set; }

        [Required]
        public string ICAO { get; set; } = String.Empty;

        [Required]
        public string FBO { get; set; } = String.Empty;

        [Required]
        public virtual int AircraftID { get; set; }

        [Required]
        public string TailNumber { get; set; }

        [Required]
        public DateTime DateRequested { get; set; }

        [Required]
        public BaseClass.AdminStatuses AdminStatus { get; set; }

        [Required]
        public BaseClass.CustStatuses CustStatus { get; set; }

        public bool IsArchived { get; set; }

        public bool IsDirectlyEntered { get; set; }

        [Required]
        public decimal QuotedPPG { get; set; }

        public decimal InvoicedPPG { get; set; }

        [Required]
        public decimal QuotedVolume { get; set; }

        public decimal InvoicedVolume { get; set; }

        [Required]
        public DateTime FuelingDateTime { get; set; }
        public string FBOPhone { get; set; } = String.Empty;
        public double WholesaleQuoted { get; set; }
        public double WholesaleInvoiced { get; set; }
        public string InvoiceNumber { get; set; } = String.Empty;
        public double BasePPG { get; set; }
        public bool NoFuel { get; set; }
        public int TripID { get; set; }
        public int LegNumber { get; set; }
        public string CertificateType { get; set; } = String.Empty;
        public string FuelOn { get; set; } = String.Empty;
        public double PostedRetail { get; set; }
        public double RampFee { get; set; }
        public double RampFeeWaivedAt { get; set; }
        public string RequestedBy { get; set; } = String.Empty;
        public FuelOrders.InvoiceStatuses InvoiceStatus { get; set; }
        public int SupplierID { get; set; }
        public double PlattsPrice { get; set; }
        public string AdminNotes { get; set; } = String.Empty;
        public string CustNotes { get; set; } = String.Empty;
        public string Vendor { get; set; } = String.Empty;
        public string Product { get; set; } = String.Empty;
        public bool IsFromFuelerLinx { get; set; } = false;
        public decimal Total { get; set; }
    }
}