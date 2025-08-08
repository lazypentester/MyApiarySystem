using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Resource.Api.Entities
{
    public class ENotification
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(max)")]
        public string Description { get; set; }

        [Required]
        [Column(TypeName = "Datetime")]
        public DateTime Created_date { get; set; }

        [Required]
        [Column(TypeName = "Datetime")]
        public DateTime Read_date { get; set; }

        [Required]
        [Column(TypeName = "bit")]
        public bool Readed { get; set; }

        [Required]
        [Column(TypeName = "int")]
        [ForeignKey("EApiary")]
        public int ApiaryId { get; set; }
        public EApiary Apiary { get; set; }
    }
}
