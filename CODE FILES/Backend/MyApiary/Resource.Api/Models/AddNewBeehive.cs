using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Resource.Api.Models
{
    public class AddNewBeehive
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public bool Alarm { get; set; }

        [Required]
        public int ApiaryId { get; set; }
    }
}
