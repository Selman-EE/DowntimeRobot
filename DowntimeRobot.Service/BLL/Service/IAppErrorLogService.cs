using DowntimeRobot.Entity.Entity;
using DowntimeRobot.Service.BLL.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DowntimeRobot.Service.BLL.Service
{
    public interface IAppErrorLogService
    {
        ServiceResponse<List<AppErrorLog>> GetList(int userId);
        ServiceResponse<AppErrorLog> GetlById(int id);
        void Add(AppErrorLog log);
    }
}
