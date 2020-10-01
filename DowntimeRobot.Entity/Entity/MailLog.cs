using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DowntimeRobot.Entity.Entity
{
    public class MailLog : EntityBase
    {
        public int JobId { get; set; }
        public string IpAddress { get; set; }        
        public string MailFrom { get; set; }
        public string MailTo { get; set; }
        public string MailSubject { get; set; }
        public string MailBody { get; set; }
        public string Message { get; set; }
    }
}
