using DowntimeRobot.Service.BLL.Repository;
using DowntimeRobot.Service.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DowntimeRobot.Service.BLL.Service
{
    public class MailService : DbFactory, IMailService
    {
        //https://mailtrap.io/inboxes/1053359/messages
        //Auth: PLAIN, LOGIN and CRAM-MD5
        //TLS: Optional(STARTTLS on all ports)
        private const string MailFrom = "no-reply@downtimerobot.com";
        private const string SmtpServer = "smtp.mailtrap.io";
        private const string SmtpUser = "219c0703259cd9";
        private const string SmtpPassword = "47fb1f217c53e6";
        private const int SmtpPort = 587; //25 or 465 or 587 or 2525

        public async Task SendMail(string[] to, string subject, string content, int jobId)
        {
            var mailTo = string.Join(",", to);
            new Mail().SendMail(string.Empty, MailFrom, mailTo, subject, content, SmtpServer, SmtpUser, SmtpPassword, SmtpPort);
            //
            //add for log
            DbInstance.MailLogs.Add(new Entity.Entity.MailLog
            {
                JobId = jobId,
                MailBody = content,
                MailFrom = MailFrom,
                MailTo = mailTo,
                MailSubject = subject,
                IpAddress = string.Empty,
                Message = string.Empty,
            });
            await DbInstance.SaveChangesAsync();
        }
        public bool SendMail(string[] to, string subject, string content)
        {
            var mailTo = string.Join(",", to);
            return new Mail().SendMail(string.Empty, MailFrom, mailTo, subject, content, SmtpServer, SmtpUser, SmtpPassword, SmtpPort);
        }

        public bool SendMail(string from, string[] to, string subject, string content, string smtpServer, string smtpUser, string smtpPassword, string smtpPort)
        {
            try
            {

                string mailTo = string.Empty;
                foreach (var item in to)
                {
                    mailTo += item + ",";
                }

                mailTo = mailTo.Substring(0, mailTo.Length - 1);


                var mail = new Mail();
                var send = mail.SendMail(string.Empty,
                    from,
                    mailTo,
                    content,
                    subject,
                    smtpServer,
                    smtpUser,
                    smtpPassword,
                    smtpPort.To<int>());

                return send;

            }
            catch
            {
                return false;
            }
        }
    }
}
