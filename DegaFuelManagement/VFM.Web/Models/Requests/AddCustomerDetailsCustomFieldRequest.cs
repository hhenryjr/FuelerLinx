using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VFM.Web.Models.Requests
{
    public class AddCustomerDetailsCustomFieldRequest
    {
        [Required]
        public int AdminClientID { get; set; }

        [Required]
        public int CustClientID { get; set; }

        [Required]
        public string FieldType { get; set; } = String.Empty;

        [Required]
        public string Title { get; set; } = String.Empty;

        [Required]
        public string Content { get; set; } = String.Empty;
    }
}