using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Resource.Api.Entities
{
    public class EApiary
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(max)")]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(max)")]
        public string Address { get; set; }

        [Required]
        [Column(TypeName = "int")]
        [ForeignKey("EUser")]
        public int UserId { get; set; }
        public EUser User { get; set; }
        public ICollection<EBeehive> Beehives { get; set; }
        public ICollection<ENotification> Notifications { get; set; }
    }
}
