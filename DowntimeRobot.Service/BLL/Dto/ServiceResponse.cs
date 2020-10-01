using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DowntimeRobot.Service.BLL.Dto
{
    public class ServiceResponse<T> : ServiceResponseBase
    {
        public T Data { get; set; }
    }
}
