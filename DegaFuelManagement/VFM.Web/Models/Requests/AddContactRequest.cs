using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VFM.Web.Models.Requests
{
    public class AddContactRequest
    {
        public string ContactType { get; set; }

        [Required]
        public int AdminClientID { get; set; }

        [Required]
        public int CustClientID { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        public string Title { get; set; }
        public string MobilePhone { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public bool Distribute { get; set; }
        public string Note { get; set; }
    }
}