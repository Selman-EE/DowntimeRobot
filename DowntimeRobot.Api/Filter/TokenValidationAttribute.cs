using DowntimeRobot.Service.BLL.Service;
using DowntimeRobot.Service.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace DowntimeRobot.Api.Filter
{
    [AttributeUsageAttribute(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    internal sealed class TokenValidationAttribute : ActionFilterAttribute, IActionFilter, IFilter
    {
        private const string RequestHeaderApiKey = "ApiKey";
        private const string RequestHeaderSecretKey = "SecretKey";
        private const string RequestHeaderJtwKey = "Authenticate-Bearer";

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            try
            {
                //Api key controls
                if (!actionContext.Request.Headers.Contains(RequestHeaderApiKey))
                {
                    actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized)
                    {
                        Content = new StringContent(MessageService.ApiKeyNotFound)
                    };
                    return;
                }
                //
                var apiKey = actionContext.Request.Headers.GetValues(RequestHeaderApiKey).First();
                if (string.IsNullOrWhiteSpace(apiKey) || !apiKey.Equals("ApiKey".GetAppSetting()))
                {
                    actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized)
                    {
                        Content = new StringContent(MessageService.InvalidApiKey)
                    };
                    return;
                }
                var secretKey = actionContext.Request.Headers.GetValues(RequestHeaderSecretKey).First();
                if (string.IsNullOrWhiteSpace(secretKey) || !secretKey.Equals("SecretKey".GetAppSetting()))
                {
                    actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized)
                    {
                        Content = new StringContent(MessageService.InvalidSecretKey)
                    };
                    return;
                }

                //
                //Jwt token controls
                if (!actionContext.Request.Headers.Contains(RequestHeaderJtwKey))
                {
                    actionContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden)
                    {
                        Content = new StringContent(MessageService.MissingAuthorizationToken)
                    };
                    return;
                }
                //
                //check token is valid
                string token = actionContext.Request.Headers.GetValues(RequestHeaderJtwKey).First();
                if (string.IsNullOrWhiteSpace(token))
                {
                    actionContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden)
                    {
                        Content = new StringContent(MessageService.InvalidToken)
                    };
                    return;
                }

                if (!JwtManager.ValidateToken(token, out string _))
                {
                    actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized)
                    {
                        Content = new StringContent(MessageService.InvalidToken)
                    };
                    return;
                }
                //
                base.OnActionExecuting(actionContext);
            }
            catch (Exception ex)
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent($"{MessageService.ContactToManager} {Environment.NewLine} {ex.ErrorMessage()}")
                };
            }


        }
    }
}