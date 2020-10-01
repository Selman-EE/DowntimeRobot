using DowntimeRobot.Entity.Entity;
using DowntimeRobot.Service.BLL.Dto;
using System.Collections.Generic;

namespace DowntimeRobot.Service.BLL.Service
{
    public interface IEventLogService
    {
        ServiceResponse<List<EventLog>> GetList(int userId);
        ServiceResponse<List<EventLog>> GetList(string searchText, string sortColumn, int start, int length, int userId);
    }
}