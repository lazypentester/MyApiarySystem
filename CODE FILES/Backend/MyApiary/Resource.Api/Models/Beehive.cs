using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Resource.Api.Models
{
    public class Beehive
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public bool Alarm { get; set; }

        [Required]
        public float SensorTemperature { get; set; }

        [Required]
        public float SensorHumidity { get; set; }

        [Required]
        public float SensorNoise { get; set; }

    }
}
