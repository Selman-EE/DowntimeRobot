using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DowntimeRobot.Entity.Entity
{
    public class Application : EntityBaseExtended
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        public virtual ICollection<Job> Jobs { get; set; } = new HashSet<Job>();

    }
}
