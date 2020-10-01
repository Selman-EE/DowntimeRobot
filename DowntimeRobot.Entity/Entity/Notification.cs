using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DowntimeRobot.Entity.Entity
{
    public class Notification : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<UserNotification> UserNotifications { get; set; } = new HashSet<UserNotification>();
    }
}
