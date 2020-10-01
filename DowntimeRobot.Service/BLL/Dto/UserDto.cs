using DowntimeRobot.Entity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DowntimeRobot.Service.BLL.Dto
{
    public class UserDto : DtoBaseExtended
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public List<ApplicationDto> Applications { get; set; } = new List<ApplicationDto>();
        public List<UserNotificationDto> UserNotifications { get; set; } = new List<UserNotificationDto>();

    }


    public static class UserConvertor
    {
        public static User ToEntity(UserDto dto)
        {
            if (dto == null) return null;
            return new User
            {
                Name = dto.Name,
                Surname = dto.Surname,
                Password = dto.Password,
                EmailAddress = dto.EmailAddress,
                DateOfInsert = dto.DateOfInsert,
                DateOfUpdate = dto.DateOfUpdate,                
            };
        }
        public static UserDto ToDto(User entity)
        {
            if (entity == null) return null;
            return new UserDto
            {
                Name = entity.Name,
                Surname = entity.Surname,
                Password = entity.Password,
                EmailAddress = entity.EmailAddress,
                DateOfInsert = entity.DateOfInsert,
                DateOfUpdate = entity.DateOfUpdate,                
            };
        }
    }
}
