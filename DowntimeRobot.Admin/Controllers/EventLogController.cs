using DowntimeRobot.Service.BLL.Model;
using DowntimeRobot.Service.BLL.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace DowntimeRobot.Admin.Controllers
{
    public class EventLogController : ControllerBase
    {
        private readonly IEventLogService _eventLogService;

        public EventLogController(IEventLogService eventLogService)
        {
            _eventLogService = eventLogService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult DataList(DataTableParams param)
        {
            var start = (param.StartIndex / param.DisplayLength);
            param.StartIndex = start == 0 ? 0 : start + 1;
            var result = _eventLogService.GetList(param.SearchValue, param.SortColumn, param.StartIndex, param.DisplayLength, UserData.Id);
            //
            var rootData = new DataTableResponse
            {
                recordsTotal = result.TotalCount
            };
            if (result != null && result.Data.Count > 0)
            {
                var listString = new List<List<string>>();
                result.Data.ForEach(z =>
                {
                    var listObject = new List<string>
                    {
                        z.UserFullName,
                        z.UserEmailAddress,
                        $"{z.AppCode} - {z.AppName}",
                        z.AppUrl,
                        $"{z.HttpStatusCode} - {(HttpStatusCode)z.HttpStatusCode}",
                        z.Status ? "Success" : "Failed",
                        z.DateOfInsert.ToString("dd.MM.yy hh:mm:ss"),
                    };
                    listString.Add(listObject);
                });
                //
                rootData.recordsFiltered = result.Data.Count;
                rootData.aaData = listString;
            }
            else
                rootData.aaData = new List<List<string>>();


            return Json(rootData, JsonRequestBehavior.AllowGet);
        }
    }
}