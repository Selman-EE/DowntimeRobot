namespace DowntimeRobot.Entity.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DowntimeRobot.Entity.DownTimeRobotDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(DowntimeRobot.Entity.DownTimeRobotDbContext context)
        {
            //  This method will be called after migrating to the latest version.
            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            context.Notifications.AddOrUpdate(new Entity.Notification
            {
                Id = 1,
                Name = "EMAIL",
                DateOfInsert = DateTime.Now
            });
            context.Notifications.AddOrUpdate(new Entity.Notification
            {
                Id = 2,
                Name = "SMS",
                DateOfInsert = DateTime.Now
            });
            //
            context.Users.AddOrUpdate(new Entity.User
            {
                Id = 1,
                Name = "Selman",
                Surname = "Ekici",
                EmailAddress = "test",
                Password = "6/H2E3KnUX0=", //123
                DateOfInsert = DateTime.Now,
            });
            //
            context.Applications.AddOrUpdate(new Entity.Application
            {
                Id = 1,
                UserId = 1,
                Code = "NSA",
                Name = "NetSparker",
                Url = "https://www.netsparker.com/",
                DateOfInsert = DateTime.Now,
            });
            //
            context.UserNotifications.AddOrUpdate(new Entity.UserNotification
            {
                UserId = 1,
                NotifyId = 1,
                Entry = "selmanerhanekici@gmail.com",                
                DateOfInsert = DateTime.Now
            });
            context.EventLogs.AddOrUpdate(new Entity.EventLog
            {
                Id = 1,
                UserId = 1,
                UserEmailAddress = "selmanerhanekici@gmail.com",
                UserFullName = "Selman Ekici",
                AppId = 1,
                AppCode = "NSA",
                AppName = "NetSparker",
                AppUrl = "https://www.netsparker.com/",
                HttpStatusCode = 200,
                Status = true,
                ReqMaxLimit = 1,
                Result = "test",
                DateOfInsert = DateTime.Now,
                StartDate = DateTime.Now.AddHours(1),
                EndDate = DateTime.Now.AddHours(1).AddSeconds(5),
                JobId = 1,
            });

            context.Jobs.AddOrUpdate(new Entity.Job
            {
                Id = 1,
                Status = true,
                AppId = 1,
                DateOfInsert = DateTime.Now,
                StartDate = DateTime.Now.AddMinutes(-10),
                ReqMaxLimit = 10,
                RangeType = 4, // MonitoringRangeType.cs
                RangeValue = 1
            });
            context.SaveChanges();

        }
    }
}
