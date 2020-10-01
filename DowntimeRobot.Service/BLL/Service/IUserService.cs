using DowntimeRobot.Entity.Entity;
using DowntimeRobot.Service.BLL.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DowntimeRobot.Service.BLL.Service
{
    public interface IUserService
    {
        ServiceResponse<User> GetById(int id);
        ServiceResponse<User> GetByEmailAddress(string emailAddress);
        ServiceResponseBase Login(LoginDto dto);
        ServiceResponseBase SignUp(UserDto dto);
    }
}
