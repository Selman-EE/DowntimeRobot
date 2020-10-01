using DowntimeRobot.Api.Filter;
using DowntimeRobot.Api.Model;
using DowntimeRobot.Service.BLL.Dto;
using DowntimeRobot.Service.BLL.Service;
using DowntimeRobot.Service.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace DowntimeRobot.Api.Controllers
{
    [TokenValidation]
    public class JobExecuterController : ApiController
    {
        private readonly IJobService _jobService;
        public JobExecuterController(IJobService jobService)
        {
            _jobService = jobService;
        }

        // <summary>
        /// Begin jobs
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ResponseType(typeof(ServiceResponseBase))]
        [Route("api/job-executer")]
        [HttpGet]
        public async Task<IHttpActionResult> JobExecuter()
        {
            try
            {
                await _jobService.JobSchedulerBegin();
                return Ok(new ServiceResponseBase { Message = MessageService.Success, IsSuccess = true });
            }
            catch (Exception ex)
            {
                return Ok(new ServiceResponseBase { Message = ex.ErrorMessage(), IsSuccess = false });
            }
        }
    }
}
