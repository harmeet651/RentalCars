using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectApp.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required] //not null
        [StringLength(250)]
        public string Name { get; set; }

        public String MembershipType { get; set; }

        [Display(Name = "Membership Type")]
        public int MembershipTypeId { get; set; }

        public bool IsSubscribedToNewsLetter { get; set; }

        public String Owns { get; set; }
    }
}