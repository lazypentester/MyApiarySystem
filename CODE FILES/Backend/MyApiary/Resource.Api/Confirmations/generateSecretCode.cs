using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resource.Api.Confirmations
{
    public static class generateSecretCode
    {
        private static char[] numbers = "0123456789".ToCharArray();

        public static string Generate(int length)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();

            for (int i = 0; i < length; i++)
            {
                builder.Append(numbers[random.Next(0, 9)]);
            }

            return builder.ToString();
        }
    }
}
