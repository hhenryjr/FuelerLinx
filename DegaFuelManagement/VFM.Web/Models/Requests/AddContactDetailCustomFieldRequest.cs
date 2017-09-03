using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VFM.Web.Models.Requests
{
    public class AddContactDetailCustomFieldRequest
    {
        [Required]
        public int ContactID { get; set; }

        [Required]
        public string FieldType { get; set; } = String.Empty;

        [Required]
        public string Title { get; set; } = String.Empty;

        [Required]
        public string Content { get; set; } = String.Empty;
    }
}