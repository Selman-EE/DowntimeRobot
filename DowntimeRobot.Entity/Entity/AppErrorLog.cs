using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DowntimeRobot.Entity.Entity
{
    public class AppErrorLog : EntityBase
    {
        public string Controller { get; set; }
        public string Action { get; set; }
        public string RequestUrl { get; set; }
        public string ExceptionMessage { get; set; }
        public string StackTrace { get; set; }
        public string Browser { get; set; }
        public string IpAddress { get; set; }
        public string UserId { get; set; }
        public bool IsMobileBrowser { get; set; }
    }
}
