using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Resource.Api.Models
{
    public class AddNewApiary
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public int UserId { get; set; }
    }
}
