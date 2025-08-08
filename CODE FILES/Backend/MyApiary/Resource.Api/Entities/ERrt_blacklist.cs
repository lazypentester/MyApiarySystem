using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Resource.Api.Entities
{
    public class ERrt_blacklist
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(max)")]
        public string Refresh_token { get; set; }

        [Required]
        [Column(TypeName = "int")]
        [ForeignKey("EDevice")]
        public int DeviceId { get; set; }
        public EDevice Device { get; set; }
    }
}
