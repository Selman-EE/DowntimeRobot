using DowntimeRobot.Entity.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DowntimeRobot.Entity.Mapping
{
    public class UserNotificationMapping : EntityTypeConfiguration<UserNotification>
    {
        public UserNotificationMapping()
        {
            ToTable("UserNotification");            
            Property(s => s.Entry).IsRequired();
        }
    }
}
