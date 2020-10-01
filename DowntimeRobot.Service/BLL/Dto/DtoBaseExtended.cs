using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DowntimeRobot.Service.BLL.Dto
{
    public class DtoBaseExtended : DtoBase
    {
        public DateTime? DateOfUpdate { get; set; }
        public DateTime? DateOfDelete { get; set; }
        public bool IsDeleted { get; set; }
    }
}
