using DowntimeRobot.Entity.Entity;
using DowntimeRobot.Entity.Mapping;
using DowntimeRobot.Entity.Migrations;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DowntimeRobot.Entity
{
    public class DownTimeRobotDbContext : DbContext
    {
        public DownTimeRobotDbContext() : base("DowntimeRobotDb")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DownTimeRobotDbContext, Configuration>());
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Application> Applications { get; set; }
        public DbSet<UserNotification> UserNotifications { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<AppErrorLog> AppErrorLogs { get; set; }
        public DbSet<EventLog> EventLogs { get; set; }
        public DbSet<MailLog> MailLogs { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            AllMappings.SetMaps(modelBuilder);
            //
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            //
            //modelBuilder.Entity<UserNotification>().HasKey(un => new { un.UserId, un.NotifyId });
            //modelBuilder.Entity<User>()
            //    .HasMany(x => x.UserNotifications)
            //    .WithOptional()
            //    .HasForeignKey(x => x.UserId);

            //modelBuilder.Entity<Notification>()
            //    .HasMany(x => x.UserNotifications)
            //    .WithOptional()
            //    .HasForeignKey(x => x.NotifyId);
        }
    }
}
