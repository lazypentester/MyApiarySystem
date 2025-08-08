using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Resource.Api.Entities
{
    public class ESensor
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "real")]
        public float Min_value { get; set; }

        [Required]
        [Column(TypeName = "real")]
        public float Max_value { get; set; }

        [Required]
        [Column(TypeName = "real")]
        public float Value { get; set; }

        [Required]
        [Column(TypeName = "bit")]
        public bool Is_working { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string Serial_number { get; set; }

        [Required]
        [Column(TypeName = "int")]
        [ForeignKey("ESensorType")]
        public int SensorTypeId { get; set; }
        public ESensorType SensorType { get; set; }

        [Required]
        [Column(TypeName = "int")]
        [ForeignKey("EBeehive")]
        public int BeehiveId { get; set; }
        public EBeehive Beehive { get; set; }

        [Required]
        [Column(TypeName = "int")]
        [ForeignKey("EBaseStation")]
        public int BaseStationId { get; set; }
        public EBaseStation BaseStation { get; set; }
    }
}
