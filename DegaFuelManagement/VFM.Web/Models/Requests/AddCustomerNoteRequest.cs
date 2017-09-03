using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VFM.Web.Models.Requests
{
    public class AddCustomerNoteRequest
    {
        [Required]
        public int AdminClientID { get; set; }

        [Required]
        public int CustClientID { get; set; }

        [Required]
        public string Note { get; set; }

        [Required]
        public int AddedByUserID { get; set; }

        public int UpdatedByUserID { get; set; }
    }
}