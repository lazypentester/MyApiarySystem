using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Resource.Api.Entities
{
    public class EDevice
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string Mac_address { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(max)")]
        public string Fingerprint { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(max)")]
        public string Refresh_token { get; set; }

        [Required]
        [Column(TypeName = "int")]
        [ForeignKey("EUser")]
        public int UserId { get; set; }
        public EUser User { get; set; }

        public ICollection<ESession> Sessions { get; set; }
        public ICollection<ERrt_blacklist> Rrt_Blacklists { get; set; }
    }
}
