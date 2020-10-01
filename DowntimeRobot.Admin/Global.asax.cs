using DowntimeRobot.Admin.Filter;
using System.Web.Mvc;
using System.Web.Routing;

namespace DowntimeRobot.Admin
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);            
            //
            //String inputs will be trim 
            ModelBinders.Binders.DefaultBinder = new TrimModelBinder();
        }
    }
}
