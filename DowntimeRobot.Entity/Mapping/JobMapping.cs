using DowntimeRobot.Entity.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DowntimeRobot.Entity.Mapping
{
    public class JobMapping : EntityTypeConfiguration<Job>
    {
        public JobMapping()
        {
            ToTable("Job");
            HasKey(s => s.Id);
            Property(s => s.RangeType).IsRequired();
            Property(s => s.RangeValue).IsRequired();
            Property(s => s.DateOfInsert).IsOptional();
            Property(s => s.DateOfUpdate).IsOptional();
        }
    }
}
