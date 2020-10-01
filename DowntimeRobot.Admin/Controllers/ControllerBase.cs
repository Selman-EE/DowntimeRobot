using DowntimeRobot.Admin.Filter;
using DowntimeRobot.Entity.Entity;
using System.Web.Mvc;

namespace DowntimeRobot.Admin.Controllers
{
    [CustomExceptionHandler]
    [Auth]
    public class ControllerBase : Controller
    {
        public User UserData { get; set; }
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var session = Session["UserData"];
            if (session != null)
                UserData = (User)session;

            base.OnActionExecuting(filterContext);
        }
    }

}