using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resource.Api.Models
{
    public class SessionsResponce
    {
        public int Id { get; set; }

        public DateTime Start_date { get; set; }

        public string Ip_address { get; set; }

        public string Geolocation { get; set; }

        public int DeviceId { get; set; }
    }
}
