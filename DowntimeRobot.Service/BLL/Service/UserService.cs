using DowntimeRobot.Entity.Entity;
using DowntimeRobot.Service.BLL.Dto;
using DowntimeRobot.Service.BLL.Repository;
using DowntimeRobot.Service.Extensions;
using System;
using System.Linq;

namespace DowntimeRobot.Service.BLL.Service
{
    public class UserService : DbFactory, IUserService
    {
        public ServiceResponse<User> GetById(int id)
        {
            var user = DbInstance.Users.Where(x => x.Id == id && !x.IsDeleted).FirstOrDefault();
            if (user == null)
                return new ServiceResponse<User> { IsSuccess = false, Message = MessageService.EntityNotFound, Data = null };
            //
            return new ServiceResponse<User> { IsSuccess = true, Message = MessageService.Success, Data = user };
        }
        public ServiceResponse<User> GetByEmailAddress(string emailAddress)
        {
            var user = DbInstance.Users.Where(x => x.EmailAddress == emailAddress && !x.IsDeleted).FirstOrDefault();
            if (user == null)
                return new ServiceResponse<User> { IsSuccess = false, Message = MessageService.EntityNotFound, Data = null };
            //
            return new ServiceResponse<User> { IsSuccess = true, Message = MessageService.Success, Data = user };
        }
        public ServiceResponseBase Login(LoginDto dto)
        {
            try
            {
                //get user by username 
                var user = DbInstance.Users.Where(x => x.EmailAddress == dto.EmailAddress).FirstOrDefault();
                if (user == null)
                    return new ServiceResponseBase() { IsSuccess = false, Message = MessageService.UserNotFound };
                //user password is correct
                if (!user.Password.Decrypt().Equals(dto.Password))
                    return new ServiceResponseBase() { IsSuccess = false, Message = MessageService.WrongPassword };

                return new ServiceResponseBase() { IsSuccess = true, Message = MessageService.Success };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ServiceResponseBase SignUp(UserDto dto)
        {
            try
            {
                //username if using already by another user
                if (DbInstance.Users.Any(x => x.EmailAddress == dto.EmailAddress))
                    return new ServiceResponseBase()
                    {
                        IsSuccess = false,
                        Message = "This email address already using by another users"
                    };

                //name is valid  
                if (!dto.Name.IsValidName())
                    return new ServiceResponseBase()
                    {
                        IsSuccess = false,
                        Message = $"Error: {MessageService.NotValidUsername} , Hint: name must be only letters "
                    };
                //surname is valid  
                if (!dto.Surname.IsValidName())
                    return new ServiceResponseBase()
                    {
                        IsSuccess = false,
                        Message = $"Error: {MessageService.NotValidUsername} , Hint: surname must be only letters"
                    };
                //password is valid 
                if (!dto.Password.IsValidPassword())
                    return new ServiceResponseBase()
                    {
                        IsSuccess = false,
                        Message = $"{MessageService.NotValidPassword } ,Hint: The passord can not be include special chars and also it must be letters or number additionally it can not be less than 6 characters or more than 32 characters"
                    };
                //username validation
                if (!Mail.IsValid(dto.EmailAddress))
                    return new ServiceResponseBase()
                    {
                        IsSuccess = false,
                        Message = "This email address is not valid"
                    };

                //create new user                 
                dto.Password = dto.Password.Encrypt();
                var user = UserConvertor.ToEntity(dto);
                //
                // add data to db 
                DbInstance.Users.Add(user);
                if (DbInstance.SaveChanges() > 0)
                {
                    return new ServiceResponseBase()
                    {
                        IsSuccess = true,
                        Message = MessageService.Success,
                        EntityId = user.Id,
                        TotalCount = 1
                    };
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return new ServiceResponseBase()
            {
                IsSuccess = false,
                Message = MessageService.Error
            };
        }
    }
}
