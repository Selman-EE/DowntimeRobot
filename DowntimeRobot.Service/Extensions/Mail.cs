using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace DowntimeRobot.Service.Extensions
{
    public class Mail
    {
        public bool SendMail(string displayName, string from, string to, string html, string title, string smtpServer, string smtpUser, string smtpPassword, int smtpPort)
        {
            displayName = string.IsNullOrWhiteSpace(displayName) ? "DownTime Robot" : displayName;
            //
            var message = new MailMessage();
            var address = new MailAddress(from, displayName);
            message.To.Add(to);
            message.From = address;
            message.Subject = title;
            message.Body = html;
            message.IsBodyHtml = true;
            message.BodyEncoding = Encoding.UTF8;
            try
            {
                var client = new SmtpClient
                {
                    Port = smtpPort,
                    Host = smtpServer,
                    EnableSsl = true,
                    Timeout = 20000,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new System.Net.NetworkCredential(smtpUser, smtpPassword)
                };

                client.Send(message);
                return true;
            }
            catch
            {
                return false;
            }

        }

        public static bool IsValid(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
