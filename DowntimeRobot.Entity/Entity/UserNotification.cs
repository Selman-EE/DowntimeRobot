using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DowntimeRobot.Entity.Entity
{
    public class UserNotification
    {
        public string Entry { get; set; }
        public DateTime DateOfInsert { get; set; } = DateTime.Now;
        [Key]
        [Column(Order = 0)]
        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual User User { get; set; }
        [Key]
        [Column(Order = 1)]
        [ForeignKey("Notification")]
        public int NotifyId { get; set; }
        public virtual Notification Notification { get; set; }

    }
}
