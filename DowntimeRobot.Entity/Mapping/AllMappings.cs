using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DowntimeRobot.Entity.Mapping
{
    public class AllMappings
    {
        public static void SetMaps(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserMapping());
            modelBuilder.Configurations.Add(new JobMapping());
            modelBuilder.Configurations.Add(new ApplicationMapping());
            modelBuilder.Configurations.Add(new UserNotificationMapping());
            modelBuilder.Configurations.Add(new NotificationMapping());
        }
    }
}
