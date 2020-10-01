using DowntimeRobot.Api.Model;
using DowntimeRobot.Service.BLL.Dto;
using DowntimeRobot.Service.BLL.Service;
using DowntimeRobot.Service.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace DowntimeRobot.Api.Controllers
{
    public class TokenController : ApiController
    {
        /// <summary>
        /// Create token for execute to jobs
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ResponseType(typeof(ServiceResponse<string>))]
        [AllowAnonymous]
        [Route("api/create-token")]
        [HttpPost]
        public IHttpActionResult CreateToken(ReqCreateToken request)
        {
            if (CheckToken(request.ApiKey, request.SecretKey))
                return Ok(new ServiceResponse<string> { Message = MessageService.Success, Data = JwtManager.GenerateToken(request.ApiKey), IsSuccess = true });

            return Ok(new ServiceResponse<string> { Message = $"{MessageService.InvalidApiKey} or {MessageService.InvalidSecretKey}", Data = null, IsSuccess = false });
        }

        private bool CheckToken(string apikey, string secretkey)
        {
            try
            {
                return apikey == "ApiKey".GetAppSetting() && secretkey == "SecretKey".GetAppSetting();
            }
            catch
            {
                return false;
            }
        }
    }
}
