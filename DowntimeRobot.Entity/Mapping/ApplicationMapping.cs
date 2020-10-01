using DowntimeRobot.Entity.Entity;
using System.Data.Entity.ModelConfiguration;

namespace DowntimeRobot.Entity.Mapping
{
    public class ApplicationMapping : EntityTypeConfiguration<Application>
    {
        public ApplicationMapping()
        {
            ToTable("Application");
            HasKey(s => s.Id);
            Property(s => s.Code).IsRequired().HasMaxLength(100);
            Property(s => s.Name).IsRequired().HasMaxLength(100);
            Property(s => s.Url).IsRequired().HasMaxLength(3000);
            Property(s => s.DateOfInsert).IsRequired();
            Property(s => s.DateOfUpdate).IsOptional();
            Property(s => s.DateOfDelete).IsOptional();
        }
    }
}
