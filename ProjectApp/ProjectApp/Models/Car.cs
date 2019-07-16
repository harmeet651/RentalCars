using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectApp.Models
{
    public class Car
    {
        public int Id { get; set; }
        [Required] //not null
        [StringLength(250)]
        public string Name { get; set; }
        [Required]
        [StringLength(250)]
        public String Color { get; set; }
        [Required]
        [StringLength(250)]
        public String ReleaseYear { get; set; }
    }
}