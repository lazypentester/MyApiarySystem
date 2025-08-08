using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.Api.Models
{
    public class Tariff
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Max_apiaries { get; set; }

        public int Max_beehives { get; set; }

        public decimal Price { get; set; }
    }
}
