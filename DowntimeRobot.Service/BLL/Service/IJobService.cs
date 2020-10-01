using DowntimeRobot.Entity.Entity;
using DowntimeRobot.Service.BLL.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DowntimeRobot.Service.BLL.Service
{
    public interface IJobService
    {
        Task<bool> JobSchedulerBegin();
        ServiceResponseBase Add(Job app);
        ServiceResponseBase Delete(int id, int userId);
        ServiceResponse<Job> GetById(int id, int userId);
        ServiceResponse<List<Job>> GetList(int userId);
        ServiceResponseBase Update(Job job);
    }
}