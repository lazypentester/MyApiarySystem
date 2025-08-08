using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Resource.Api.Models
{
    public class RegisterValidation
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string secretCode { get; set; }
    }
}
