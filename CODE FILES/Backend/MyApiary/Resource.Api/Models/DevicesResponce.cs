using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resource.Api.Models
{
    public class DevicesResponce
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Mac_address { get; set; }

        public string Fingerprint { get; set; }
    }
}
