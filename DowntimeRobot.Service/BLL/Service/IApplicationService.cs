using DowntimeRobot.Entity.Entity;
using DowntimeRobot.Service.BLL.Dto;
using System.Collections.Generic;

namespace DowntimeRobot.Service.BLL.Service
{
    public interface IApplicationService
    {
        ServiceResponseBase Add(Application app);
        ServiceResponseBase Delete(int id, int userId);
        ServiceResponse<Application> GetById(int id, int userId);
        ServiceResponse<List<Application>> GetList(int userId);
        ServiceResponseBase Update(Application app);
    }
}