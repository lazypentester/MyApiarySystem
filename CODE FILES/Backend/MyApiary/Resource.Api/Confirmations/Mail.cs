using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Resource.Api.Confirmations
{
    public class Mail
    {
        public static bool sendEmail(string mail, string text)
        {
            // отправитель - устанавливаем адрес и отображаемое в письме имя
            MailAddress from = new MailAddress("hlib.holubov@nure.ua", "Bee Company");
            // кому отправляем
            //MailAddress to = new MailAddress(mail);
            MailAddress to = new MailAddress(mail);
            // создаем объект сообщения
            MailMessage m = new MailMessage(from, to);
            // тема письма
            m.Subject = "Код підтвердження";
            // текст письма
            m.Body = $"<h2>{text}</h2>";
            // письмо представляет код html
            m.IsBodyHtml = true;
            // адрес smtp-сервера и порт, с которого будем отправлять письмо
            //SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 25);
            // логин и пароль
            smtp.Credentials = new NetworkCredential("hlib.holubov@nure.ua", "Gleb2309");
            smtp.EnableSsl = true;
            
            try
            {
                smtp.Send(m);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message.ToString());
                return false;
            }

            return true;
        }
    }
}
