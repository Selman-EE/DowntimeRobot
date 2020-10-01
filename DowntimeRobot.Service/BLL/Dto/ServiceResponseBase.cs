using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DowntimeRobot.Service.BLL.Dto
{
    public class ServiceResponseBase
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public int EntityId { get; set; }
        public int TotalCount { get; set; }
    }
}
