using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resource.Api.Entities
{
    public class SensorsGetResponce
    {
        public int Id { get; set; }

        public float MinValue { get; set; }

        public float MaxValue { get; set; }

        public bool isWorking { get; set; }

        public string Type { get; set; }

        public string SerialNumber { get; set; }

        public int BeehiveId { get; set; }

        public string BaseStationName { get; set; }

        public string BaseStationSerialNumber { get; set; }
    }
}
