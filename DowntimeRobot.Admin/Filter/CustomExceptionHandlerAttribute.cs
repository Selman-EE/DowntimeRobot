using DowntimeRobot.Admin.Helper;
using DowntimeRobot.Entity.Entity;
using DowntimeRobot.Service.BLL.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DowntimeRobot.Admin.Filter
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class CustomExceptionHandlerAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled || filterContext.HttpContext.IsCustomErrorEnabled)
            {
                return;
            }

            var errorService = DependencyResolver.Current.GetService<IAppErrorLogService>();
            //
            //add mvc error to DB            
            errorService.Add(new Entity.Entity.AppErrorLog
            {
                Action = filterContext.RouteData.Values["action"].ToString(),
                Controller = filterContext.RouteData.Values["controller"].ToString(),
                ExceptionMessage = filterContext?.Exception?.Message ?? "",
                StackTrace = filterContext.Exception?.StackTrace ?? "",
                DateOfInsert = filterContext.RequestContext.HttpContext.Timestamp,
                RequestUrl = filterContext.HttpContext.Request.Url.ToString(),
                Browser = $"{filterContext.HttpContext.Request.Browser.Browser} - Version:{filterContext.HttpContext.Request.Browser.Version}",
                IpAddress = System.Web.HttpContext.Current.Request.UserHostAddress,
                UserId = ((User)HttpContext.Current.Session["UserData"])?.Id.ToString() ?? string.Empty,
                IsMobileBrowser = filterContext.HttpContext.Request.IsMobileBrowser()
            });


            //Make sure that we mark the exception as handled
            filterContext.ExceptionHandled = true;
            filterContext.Result = new ViewResult()
            {
                ViewName = "Error"
            };


        }
    }

}