using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VFM.Web.Models.Requests
{
    public class AddFuelOrderNoteRequest
    {
        [Required]
        public int ClientID { get; set; }

        [Required]
        public int FuelOrderID { get; set; }

        [Required]
        public string Note { get; set; }

        [Required]
        public int AddedByUserID { get; set; }

        public int UpdatedByUserID { get; set; }
    }
}