using DowntimeRobot.Service.BLL.Service;
using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DowntimeRobot.Admin.Controllers
{
    public class AppErrorLogController : ControllerBase
    {
        private readonly IAppErrorLogService _appErrorLogService;

        public AppErrorLogController(IAppErrorLogService appErrorLogService)
        {
            _appErrorLogService = appErrorLogService;
        }

        public ActionResult Index()
        {
            var result = _appErrorLogService.GetList(UserData.Id);
            if (result.IsSuccess)
                return View(result.Data);

            return View();
        }

        public ActionResult Detail(int id)
        {
            return View(_appErrorLogService.GetlById(id).Data);
        }
    }
}