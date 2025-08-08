using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resource.Api.Models
{
    public class UserResponce
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Phone { get; set; }

        public string Mail { get; set; }

        public string Tariff { get; set; }
    }
}
