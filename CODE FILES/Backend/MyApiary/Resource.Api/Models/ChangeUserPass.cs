using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Resource.Api.Models
{
    public class ChangeUserPass
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string Mail { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string Pass_old { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string Pass_new { get; set; }
    }
}
