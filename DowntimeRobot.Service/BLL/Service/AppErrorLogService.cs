using DowntimeRobot.Entity.Entity;
using DowntimeRobot.Service.BLL.Dto;
using DowntimeRobot.Service.BLL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DowntimeRobot.Service.BLL.Service
{
    public class AppErrorLogService : DbFactory, IAppErrorLogService
    {
        public ServiceResponse<List<AppErrorLog>> GetList(int userId)
        {
            var list = DbInstance.AppErrorLogs.Where(x => x.UserId == userId.ToString()).OrderByDescending(x => x.Id).ToList();
            if (list.Count <= 0)
                return new ServiceResponse<List<AppErrorLog>> { IsSuccess = false, Message = MessageService.NotRecord, Data = null };
            //
            return new ServiceResponse<List<AppErrorLog>> { IsSuccess = true, Message = MessageService.Success, Data = list };
        }
        public ServiceResponse<AppErrorLog> GetlById(int id)
        {
            var data = DbInstance.AppErrorLogs.Where(x => x.Id == id).FirstOrDefault();
            if (data == null)
                return new ServiceResponse<AppErrorLog> { IsSuccess = false, Message = MessageService.NotRecord, Data = null };
            //
            return new ServiceResponse<AppErrorLog> { IsSuccess = true, Message = MessageService.Success, Data = data };
        }

        public void Add(AppErrorLog log)
        {
            try
            {
                DbInstance.AppErrorLogs.Add(log);
                DbInstance.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
