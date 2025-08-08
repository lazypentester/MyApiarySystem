using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Resource.Api.Models
{
    public class SensorsUpdateRequest
    {
        [Required]
        public int sensor_id { get; set; }

        public float minValue { get; set; }

        public float maxValue { get; set; }

        public bool isWorking { get; set; }

        [Required]
        public int beehiveId { get; set; }
    }
}
