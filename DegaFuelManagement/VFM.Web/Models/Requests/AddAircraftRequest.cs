using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VFM.Web.Models.Requests
{
    public class AddAircraftRequest
    {
        public int MakeModelID { get; set; }

        [Required]
        public int AdminClientID { get; set; }

        [Required]
        public int CustClientID { get; set; }

        public string TailNumber { get; set; }

        public string AccountNumber { get; set; }

        public string CardNumber { get; set; }

        public bool IsMarginEnabled { get; set; }

        public double Margin { get; set; }
    }
}