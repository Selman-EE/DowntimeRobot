using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DowntimeRobot.Service.BLL.Dto
{
    public class UserNotificationDto
    {
        public string Entry { get; set; }
        public DateTime DateOfInsert { get; set; }
        public int UserId { get; set; }
        public int NotifyId { get; set; }
    }
}
