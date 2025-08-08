using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Resource.Api.Models
{
    public class CreateSession
    {
        public int Device_id { get; set; }

        [Required]
        public string Device_Name { get; set; }

        public string Device_Mac_address { get; set; }

        [Required]
        public string Device_Fingerprint { get; set; }

        [Required]
        public string Device_Refresh_token { get; set; }

        public int Session_Id { get; set; }

        public string Session_Ip_address { get; set; }

        public string Session_Geolocation { get; set; }
    }
}
