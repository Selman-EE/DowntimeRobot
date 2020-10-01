using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DowntimeRobot.Entity.Entity
{
    public class EventLog : EntityBase
    {
        public int JobId { get; set; }
        public int AppId { get; set; }
        public string AppCode { get; set; }
        public string AppName { get; set; }
        public string AppUrl { get; set; }
        public int UserId { get; set; }
        public string UserFullName { get; set; }
        public string UserEmailAddress { get; set; }
        public int HttpStatusCode { get; set; }
        public int ReqMaxLimit { get; set; }
        public bool Status { get; set; }
        public string Result { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
