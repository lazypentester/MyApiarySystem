using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.Api.Models
{
    public class Account
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Phone { get; set; }

        public string Mail { get; set; }

        public bool Confirmed { get; set; }

        public int TariffId { get; set; }

        public string Role { get; set; }
    }
}
