using System;
using System.ComponentModel.DataAnnotations;

namespace VFM.Web.Models.Requests
{
    public class GetFuelOrdersRequest
    {
        [Required]
        public int ClientID { get; set; }

        public DateTime StartDate { get; set; } = Degatech.Common.BaseClass.DatabaseDateTimeMinValue();

        public DateTime EndDate { get; set; } = Degatech.Common.BaseClass.DatabaseDateTimeMinValue();
    }
}