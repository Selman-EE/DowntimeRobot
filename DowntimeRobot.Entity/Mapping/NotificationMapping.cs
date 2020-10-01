using DowntimeRobot.Entity.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DowntimeRobot.Entity.Mapping
{
    public class NotificationMapping : EntityTypeConfiguration<Notification>
    {
        public NotificationMapping()
        {
            ToTable("Notification");
            HasKey(s => s.Id);
            Property(s => s.Name).IsRequired().HasMaxLength(100);
            Property(s => s.Description).IsOptional();
        }
    }
}
