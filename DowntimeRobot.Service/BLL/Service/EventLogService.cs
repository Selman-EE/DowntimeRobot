using DowntimeRobot.Entity.Entity;
using DowntimeRobot.Service.BLL.Dto;
using DowntimeRobot.Service.BLL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace DowntimeRobot.Service.BLL.Service
{
    public class EventLogService : DbFactory, IEventLogService
    {
        //this will be ajax
        public ServiceResponse<List<EventLog>> GetList(int userId)
        {
            var list = DbInstance.EventLogs.Where(x => x.UserId == userId).OrderByDescending(x => x.Id).ToList();
            if (list.Count <= 0)
                return new ServiceResponse<List<EventLog>> { IsSuccess = false, Message = MessageService.NotRecord, Data = null };
            //
            return new ServiceResponse<List<EventLog>> { IsSuccess = true, Message = MessageService.Success, Data = list };
        }

        public ServiceResponse<List<EventLog>> GetList(string searchText, string sortColumn, int start, int length, int userId)
        {
            var list = DbInstance.EventLogs.Where(x => x.UserId == userId).AsQueryable();

            if (!string.IsNullOrEmpty(searchText))
                list = list.Where(
                        s => s.UserFullName.ToString().ToLower().Contains(searchText.ToLower()) ||
                             s.UserEmailAddress.ToString().ToLower().Contains(searchText.ToLower()) ||
                             s.AppUrl.ToString().ToLower().Contains(searchText.ToLower()) ||
                             s.AppName.ToString().ToLower().Contains(searchText.ToLower())).AsQueryable();
            //
            if (!string.IsNullOrEmpty(sortColumn))
                list = list.SortBy(sortColumn).AsQueryable();
            //
            int count = list.Count();
            var data = list.Skip(start).Take(length).ToList();
            return new ServiceResponse<List<EventLog>> { IsSuccess = true, Message = MessageService.Success, Data = data, TotalCount = count };
        }
    }
}
