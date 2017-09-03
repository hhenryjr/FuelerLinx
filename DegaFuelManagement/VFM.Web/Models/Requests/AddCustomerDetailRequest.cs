using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VFM.Web.Models.Requests
{
    public class AddCustomerDetailRequest
    {
        [Required]
        public int AdminClientID { get; set; }

        [Required]
        public int CustClientID { get; set; }

        [Required]
        public string Name { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string BaseICAO { get; set; }

        public bool IsActive { get; set; }

        public string Note { get; set; }

        public string ParentName { get; set; }

        public string CertificateType { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string State { get; set; }

        public string ZipCode { get; set; }

        public bool IsDemoMode { get; set; }
    }
}