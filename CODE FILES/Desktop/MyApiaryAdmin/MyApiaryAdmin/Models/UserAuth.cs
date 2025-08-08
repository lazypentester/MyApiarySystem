using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApiaryAdmin.Models
{
    public class UserAuth
    {
        public string access_token_short { get; set; }
        public string access_token_long { get; set; }
        public int user_id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string phone { get; set; }
        public string mail { get; set; }
        public int tariffId { get; set; }
        public string tariff_name { get; set; }
        public int max_apiaries { get; set; }
        public int max_beehives { get; set; }
        public double price { get; set; }
        public bool confirmed { get; set; }
    }
}
