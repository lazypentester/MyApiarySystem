using MyApiaryAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApiaryAdmin.Storage
{
    public static class Storage
    {
        public static string token_short { get; set; }

        public static string token_long { get; set; }

        public static int user_id { get; set; }

        public static List<SensorType> sensorTypes { get; set; }

        public static List<Beehive> beehives { get; set; }

        public static List<BaseStation> baseStations { get; set; }
    }
}
