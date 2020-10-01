using DowntimeRobot.Entity.Entity;
using DowntimeRobot.Service.BLL.Dto;
using DowntimeRobot.Service.BLL.Repository;
using DowntimeRobot.Service.Extensions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DowntimeRobot.Service.BLL.Service
{
    public class ApplicationService : DbFactory, IApplicationService
    {
        public ServiceResponse<Application> GetById(int id, int userId)
        {

            var app = DbInstance.Applications.Where(x => x.Id == id && x.UserId == userId && !x.IsDeleted).FirstOrDefault();
            if (app == null)
                return new ServiceResponse<Application> { IsSuccess = false, Message = MessageService.EntityNotFound, Data = null };
            //
            return new ServiceResponse<Application> { IsSuccess = true, Message = MessageService.Success, Data = app };
        }
        public ServiceResponse<List<Application>> GetList(int userId)
        {
            var appList = DbInstance.Applications.Where(x => x.UserId == userId && !x.IsDeleted).OrderByDescending(x => x.Id).ToList();
            if (appList.Count <= 0)
                return new ServiceResponse<List<Application>> { IsSuccess = false, Message = MessageService.NotRecord, Data = null };
            //
            return new ServiceResponse<List<Application>> { IsSuccess = true, Message = MessageService.Success, Data = appList };
        }

        public ServiceResponseBase Add(Application app)
        {
            try
            {
                if (!app.Url.IsValidUrl())
                    return new ServiceResponseBase() { IsSuccess = false, Message = $"{MessageService.Error} ,Url is not valid format" };
                //
                app.Code = app.Code.CleanSpecialChars().ToUpper();
                if (app.Code.Length < 3 || !app.Code.IsValidName())
                    return new ServiceResponseBase() { IsSuccess = false, Message = $"{MessageService.Error} ,App code must be higher than 2 letters and it can be only letters" };

                app.Name = app.Name.CleanSpecialChars();
                // add data to db 
                DbInstance.Applications.Add(app);
                if (DbInstance.SaveChanges() > 0)
                {
                    return new ServiceResponseBase()
                    {
                        IsSuccess = true,
                        Message = MessageService.Success,
                        EntityId = app.Id,
                        TotalCount = 1
                    };
                }

                return new ServiceResponseBase()
                {
                    IsSuccess = false,
                    Message = MessageService.Error,
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ServiceResponseBase Update(Application app)
        {
            try
            {
                var appEntity = DbInstance.Applications.Where(x => x.Id == app.Id).FirstOrDefault();
                if (appEntity == null)
                    return new ServiceResponseBase() { IsSuccess = false, Message = MessageService.EntityNotFound };
                //
                if (!app.Url.IsValidUrl())
                    return new ServiceResponseBase() { IsSuccess = false, Message = $"{MessageService.Error} ,Url is not valid format" };
                //
                appEntity.Code = app.Code.CleanSpecialChars().ToUpper();
                if (app.Code.Length < 3 || !app.Code.IsValidName())
                    return new ServiceResponseBase() { IsSuccess = false, Message = $"{MessageService.Error} ,App code must be higher than 2 letters and it can be only letters" };
                //
                appEntity.Url = app.Url;
                appEntity.Name = app.Name.CleanSpecialChars();
                appEntity.DateOfUpdate = DateTime.Now;
                //
                DbInstance.Applications.AddOrUpdate(appEntity);
                if (DbInstance.SaveChanges() > 0)
                {
                    return new ServiceResponseBase()
                    {
                        IsSuccess = true,
                        Message = MessageService.Success,
                        EntityId = app.Id,
                        TotalCount = 1
                    };
                }

                return new ServiceResponseBase()
                {
                    IsSuccess = false,
                    Message = MessageService.Error,
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ServiceResponseBase Delete(int id, int userId)
        {
            try
            {
                var appEntity = DbInstance.Applications.Where(x => x.Id == id && x.UserId == userId).FirstOrDefault();
                if (appEntity == null)
                    return new ServiceResponseBase() { IsSuccess = false, Message = MessageService.EntityNotFound };
                //                
                appEntity.DateOfDelete = DateTime.Now;
                appEntity.IsDeleted = true;

                DbInstance.Applications.AddOrUpdate(appEntity);
                if (DbInstance.SaveChanges() > 0)
                {
                    return new ServiceResponseBase()
                    {
                        IsSuccess = true,
                        Message = MessageService.Success,
                        EntityId = id,
                        TotalCount = 1
                    };
                }

                return new ServiceResponseBase()
                {
                    IsSuccess = false,
                    Message = MessageService.Error,
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
