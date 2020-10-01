using DowntimeRobot.Entity.Entity;
using DowntimeRobot.Service.BLL.Dto;
using DowntimeRobot.Service.BLL.Repository;
using DowntimeRobot.Service.Enums;
using DowntimeRobot.Service.Extensions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DowntimeRobot.Service.BLL.Service
{
    public class JobService : DbFactory, IJobService
    {
        private readonly IMailService _mailService;
        public JobService(IMailService mailService)
        {
            _mailService = mailService;
        }
        public async Task<bool> JobSchedulerBegin()
        {
            var jobs = DbInstance.Jobs.Where(x => x.RangeType == (int)MonitoringRangeType.Daily && x.StartDate <= DateTime.Now && x.Status).ToList();
            if (jobs.Count < 1)
                return false;

            foreach (var job in jobs)
            {
                //
                var reqCount = await DbInstance.EventLogs.CountAsync(x => x.JobId == job.Id && DbFunctions.DiffDays(x.DateOfInsert, job.DateOfInsert) == 0);
                if (reqCount >= job.ReqMaxLimit)
                    continue;
                //
                var lastTry = DbInstance.EventLogs.Where(x => x.JobId == job.Id && DbFunctions.DiffDays(x.DateOfInsert, job.DateOfInsert) == 0)
                    .OrderByDescending(x => x.Id)
                    .FirstOrDefault();
                if (lastTry != null && DateTime.Now > lastTry.DateOfInsert.AddHours(job.RangeValue))
                    continue;
                //
                var reqUrl = job.Application.Url;
                if (string.IsNullOrWhiteSpace(reqUrl))
                    continue;

                await Task.Run(async () =>
                {
                    //
                    var cts = new CancellationTokenSource();
                    cts.CancelAfter(TimeSpan.FromMilliseconds(10000));
                    DateTime startDate = DateTime.Now;
                    DateTime endDate;
                    try
                    {
                        using (var client = new HttpClient())
                        {
                            client.DefaultRequestHeaders.Accept.Clear();
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                            HttpResponseMessage response = await client.GetAsync(reqUrl, cts.Token);
                            //
                            //If the response status was ok
                            if (!response.IsSuccessStatusCode)
                            {
                                endDate = DateTime.Now;
                                var result = response.Content.ReadAsStringAsync().Result;
                                var httpStatusCode = response.StatusCode.To<int>();
                                //
                                var html = $"Request Uri: {reqUrl} <br>"
                                + $"Date: {DateTime.Now} <br>"
                                + $"(HttpStatusCode: {httpStatusCode}) - {response.StatusCode} <br>"
                                + $"Response: {result}";
                                //
                                var subject = $"Error:{job.Application.Name}";
                                //buraya ne geliyor diye bak
                                var mailTo = job.Application.User.UserNotifications.Select(x => x.Entry).ToArray();
                                await _mailService.SendMail(mailTo, subject, html, job.Id);
                                //
                                DbInstance.EventLogs.Add(new EventLog
                                {
                                    AppId = job.AppId,
                                    AppCode = job.Application.Code,
                                    AppName = job.Application.Name,
                                    AppUrl = job.Application.Url,
                                    Result = result,
                                    ReqMaxLimit = job.ReqMaxLimit,
                                    JobId = job.Id,
                                    HttpStatusCode = httpStatusCode,
                                    UserId = job.Application.UserId,
                                    UserFullName = $"{job.Application.User.Name} {job.Application.User.Surname}",
                                    UserEmailAddress = job.Application.User.EmailAddress,
                                    StartDate = startDate,
                                    EndDate = endDate,
                                    Status = false,
                                });
                                await DbInstance.SaveChangesAsync();
                            }
                            else
                            {
                                endDate = DateTime.Now;
                                //
                                DbInstance.EventLogs.Add(new EventLog
                                {
                                    AppId = job.AppId,
                                    AppCode = job.Application.Code,
                                    AppName = job.Application.Name,
                                    AppUrl = job.Application.Url,
                                    ReqMaxLimit = job.ReqMaxLimit,
                                    JobId = job.Id,
                                    HttpStatusCode = (int)response.StatusCode,
                                    UserId = job.Application.UserId,
                                    UserFullName = $"{job.Application.User.Name} {job.Application.User.Surname}",
                                    UserEmailAddress = job.Application.User.EmailAddress,
                                    StartDate = startDate,
                                    EndDate = endDate,
                                    Status = true
                                });
                                await DbInstance.SaveChangesAsync();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        endDate = DateTime.Now;
                        DbInstance.EventLogs.Add(new EventLog
                        {
                            AppId = job.AppId,
                            AppCode = job.Application.Code,
                            AppName = job.Application.Name,
                            AppUrl = job.Application.Url,
                            ReqMaxLimit = job.ReqMaxLimit,
                            JobId = job.Id,
                            UserId = job.Application.UserId,
                            UserFullName = $"{job.Application.User.Name} {job.Application.User.Surname}",
                            UserEmailAddress = job.Application.User.EmailAddress,
                            StartDate = startDate,
                            EndDate = endDate,
                            Status = false,
                            Result = ex.Message
                        });
                        await DbInstance.SaveChangesAsync();
                    }
                    finally
                    {
                        cts.Cancel();
                        cts.Dispose();
                    }

                });
            }

            return true;
        }

        public ServiceResponse<Job> GetById(int id, int userId)
        {

            var app = DbInstance.Jobs.Where(x => x.Id == id && x.Application.UserId == userId && !x.IsDeleted).FirstOrDefault();
            if (app == null)
                return new ServiceResponse<Job> { IsSuccess = false, Message = MessageService.EntityNotFound, Data = null };
            //
            return new ServiceResponse<Job> { IsSuccess = true, Message = MessageService.Success, Data = app };
        }
        public ServiceResponse<List<Job>> GetList(int userId)
        {
            var appList = DbInstance.Jobs.Where(x => x.Application.UserId == userId && !x.IsDeleted).OrderByDescending(x => x.Id).ToList();
            if (appList.Count <= 0)
                return new ServiceResponse<List<Job>> { IsSuccess = false, Message = MessageService.NotRecord, Data = null };
            //
            return new ServiceResponse<List<Job>> { IsSuccess = true, Message = MessageService.Success, Data = appList };
        }

        public ServiceResponseBase Add(Job app)
        {
            try
            {
                // add data to db 
                DbInstance.Jobs.Add(app);
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
        public ServiceResponseBase Update(Job job)
        {
            try
            {
                var jobEntity = DbInstance.Jobs.Where(x => x.Id == job.Id && !x.Status && !x.IsDeleted).FirstOrDefault();
                if (jobEntity == null)
                    return new ServiceResponseBase() { IsSuccess = false, Message = MessageService.EntityNotFound };
                //                
                //
                jobEntity.Status = job.Status;
                jobEntity.RangeType = job.RangeType;
                jobEntity.RangeValue = job.RangeValue;
                jobEntity.ReqMaxLimit = job.ReqMaxLimit;
                jobEntity.AppId = job.AppId;
                jobEntity.StartDate = job.StartDate;
                jobEntity.DateOfUpdate = DateTime.Now;
                //
                DbInstance.Entry(jobEntity).State = EntityState.Modified;
                if (DbInstance.SaveChanges() > 0)
                {
                    return new ServiceResponseBase()
                    {
                        IsSuccess = true,
                        Message = MessageService.Success,
                        EntityId = job.Id,
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
                var jobEntity = DbInstance.Jobs.Where(x => x.Id == id && x.Application.UserId == userId && !x.IsDeleted).FirstOrDefault();
                if (jobEntity == null)
                    return new ServiceResponseBase() { IsSuccess = false, Message = MessageService.EntityNotFound };
                //                
                jobEntity.DateOfDelete = DateTime.Now;
                jobEntity.IsDeleted = true;
                //
                DbInstance.Entry(jobEntity).State = EntityState.Modified;
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
