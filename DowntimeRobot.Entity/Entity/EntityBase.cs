using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DowntimeRobot.Entity.Entity
{
    public class EntityBase
    {
        public int Id { get; set; }
        public DateTime DateOfInsert { get; set; } = DateTime.Now;
        
    }
}
