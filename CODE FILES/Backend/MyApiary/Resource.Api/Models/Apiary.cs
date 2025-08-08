using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Resource.Api.Models
{
    public class Apiary
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public bool ConnectionWire { get; set; }
        [Required]
        public bool ConnectionWireless { get; set; }

        [Required]
        public int CountOfBeehives { get; set; }

        [Required]
        public bool Notifications { get; set; }
    }
}
