using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Resource.Api.Entities
{
    public class ESession
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "Datetime")]
        public DateTime Start_date { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string Ip_address { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string Geolocation { get; set; }

        [Required]
        [Column(TypeName = "int")]
        [ForeignKey("EDevice")]
        public int DeviceId { get; set; }
        public EDevice Device { get; set; }
    }
}
