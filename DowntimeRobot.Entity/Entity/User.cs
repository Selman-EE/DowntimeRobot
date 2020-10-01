using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DowntimeRobot.Entity.Entity
{
    public class User : EntityBaseExtended
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public virtual ICollection<Application> Applications { get; set; } = new HashSet<Application>();
        public virtual ICollection<UserNotification> UserNotifications { get; set; } = new HashSet<UserNotification>();
    }
}
