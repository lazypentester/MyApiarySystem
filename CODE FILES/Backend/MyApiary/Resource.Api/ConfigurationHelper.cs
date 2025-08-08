using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resource.Api
{
    public class ConfigurationHelper
    {
        public static IConfiguration config;

        public static void Initialize(IConfiguration Configuration)
        {
            config = Configuration;
        }
    }
}
