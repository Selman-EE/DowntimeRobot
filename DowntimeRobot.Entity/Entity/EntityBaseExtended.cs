using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DowntimeRobot.Entity.Entity
{
    public class EntityBaseExtended : EntityBase
    {
        public DateTime? DateOfUpdate { get; set; }
        public DateTime? DateOfDelete { get; set; }
        public bool IsDeleted { get; set; }
    }
}
