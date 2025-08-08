using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Resource.Api.Entities
{
    public class ETariff
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "int")]
        public int Max_apiaries { get; set; }

        [Required]
        [Column(TypeName = "int")]
        public int Max_beehives { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        public ICollection<EUser> Users { get; set; }
    }
}
