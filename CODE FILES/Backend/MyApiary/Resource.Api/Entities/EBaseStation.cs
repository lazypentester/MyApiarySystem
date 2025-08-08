using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Resource.Api.Entities
{
    public class EBaseStation
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "bit")]
        public bool Is_working { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(max)")]
        public string Serial_number { get; set; }

        public ICollection<ESensor> Sensors { get; set; }
    }
}
