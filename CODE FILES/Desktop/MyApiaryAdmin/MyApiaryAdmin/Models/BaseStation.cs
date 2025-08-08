using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApiaryAdmin.Models
{
    public class BaseStation
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool Is_working { get; set; }

        public string Serial_number { get; set; }
    }
}
