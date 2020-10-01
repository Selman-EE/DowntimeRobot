using DowntimeRobot.Service.BLL.Service;
using DowntimeRobot.Service.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DowntimeRobot.Admin.Controllers
{
    public class HomeController : ControllerBase
    {
        private readonly IUserService _userService;
        public HomeController(IUserService userService)
        {
            _userService = userService;
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}