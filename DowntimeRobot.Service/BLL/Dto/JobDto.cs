using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DowntimeRobot.Service.BLL.Dto
{
    public class JobDto : DtoBase
    {
        public int ReqMaxLimit { get; set; } // max 5
        public int RangeType { get; set; }
        public int RangeValue { get; set; }
        public bool Status { get; set; }
        public DateTime DateOfUpdate { get; set; }
        public int AppId { get; set; }
    }
}
