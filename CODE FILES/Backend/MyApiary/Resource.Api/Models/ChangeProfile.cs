using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Resource.Api.Models
{
    public class ChangeProfile
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string Surname { get; set; }
    }
}
