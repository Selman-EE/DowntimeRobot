using DowntimeRobot.Admin.Helper;
using DowntimeRobot.Entity.Entity;
using DowntimeRobot.Service.BLL.Service;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DowntimeRobot.Admin.Filter
{

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class AuthAttribute : AuthorizeAttribute
    {

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            var user = CookieHelper.GetCookiesValue();
            if (user == null)
            {
                if (filterContext.RequestContext.HttpContext.Request.IsAjaxRequest())
                {
                    filterContext.Result = new JsonResult() { Data = "unauthorized user", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                    return;
                }
                else
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "Controller", "Account" }, { "Action", "Login" } });
                    return;
                }
            }
            else
            {
                var userService = DependencyResolver.Current.GetService<IUserService>();
                var result = userService.Login(user);
                var userData = (User)HttpContext.Current.Session["UserData"];
                if (!result.IsSuccess || userData == null)
                {
                    CookieHelper.ClearCookies();
                    //
                    if (filterContext.RequestContext.HttpContext.Request.IsAjaxRequest())
                    {
                        filterContext.Result = new JsonResult() { Data = $"unauthorized user ,{result.Message}", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                        return;
                    }
                    else
                    {
                        filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "Controller", "Account" }, { "Action", "Login" } });
                        return;
                    }
                }
            }
        }
    }

}