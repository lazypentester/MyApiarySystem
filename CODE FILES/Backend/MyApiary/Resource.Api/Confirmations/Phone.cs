using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace Resource.Api.Confirmations
{
    public static class Phone
    {
        public static bool SendSms(string phone_number, string mess)
        {
            // Find your Account SID and Auth Token at twilio.com/console
            // and set the environment variables. See http://twil.io/secure
            string accountSid = "";
            string authToken = "";

            try
            {
                TwilioClient.Init(accountSid, authToken);

                var message = MessageResource.Create(
                    body: $"{mess}",
                    from: new Twilio.Types.PhoneNumber("+17152604446"),
                    //to: new Twilio.Types.PhoneNumber(phone_number)
                    to: new Twilio.Types.PhoneNumber(phone_number)
                );

                Console.WriteLine(message.Sid);
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
