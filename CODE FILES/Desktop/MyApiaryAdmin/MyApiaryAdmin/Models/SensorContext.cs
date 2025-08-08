using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApiaryAdmin.Models
{
    public class SensorContext
    {
        public int Id { get; set; }

        public float Min_value { get; set; }

        public float Max_value { get; set; }

        public float Value { get; set; }

        public bool Is_working { get; set; }

        public string Serial_number { get; set; }

        public int SensorTypeId { get; set; }

        public int BeehiveId { get; set; }

        public int BaseStationId { get; set; }
    }
}
