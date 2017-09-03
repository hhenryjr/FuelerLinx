using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VFM.Web.Models.Requests
{
    public class AddCustomerAccountingInfoRequest
    {
        [Required]
        public int AdminClientID { get; set; }

        [Required]
        public int CustClientID { get; set; }

        public DateTime StartDate { get; set; }

        public string AccountRep { get; set; } = String.Empty;

        public string AccountingCode { get; set; } = String.Empty;

        public bool IsFuelerLinxCustomer { get; set; }

        public string BillingRep { get; set; } = String.Empty;

        public string SchedulingSystem { get; set; } = String.Empty;

        public string CreditCardFee { get; set; } = String.Empty;

        public string PaymentTerms { get; set; } = String.Empty;

        public string BillToSetup { get; set; } = String.Empty;
    }
}