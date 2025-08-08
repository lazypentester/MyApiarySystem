using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resource.Api.Models
{
    public class NotofocationsResponceByApiary
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime Created_date { get; set; }

        public bool Readed { get; set; }

        public int ApiaryId { get; set; }
    }
}
