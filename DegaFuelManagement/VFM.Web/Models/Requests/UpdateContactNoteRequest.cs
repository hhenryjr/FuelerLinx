using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VFM.Web.Models.Requests
{
    public class UpdateContactNoteRequest : AddContactNoteRequest
    {
        [Required]
        public int Id { get; set; }
    }
}