using DowntimeRobot.Entity.Entity;
using System.Data.Entity.ModelConfiguration;

namespace DowntimeRobot.Entity.Mapping
{
    public class UserMapping : EntityTypeConfiguration<User>
    {
        public UserMapping()
        {
            ToTable("User");
            HasKey(s => s.Id);
            Property(s => s.Name).IsRequired().HasMaxLength(100);
            Property(s => s.Surname).IsRequired().HasMaxLength(100);
            Property(s => s.Password).IsRequired().HasMaxLength(300);
            Property(s => s.DateOfInsert).IsRequired();
            Property(s => s.DateOfUpdate).IsOptional();
            Property(s => s.DateOfDelete).IsOptional();
        }
    }
}
