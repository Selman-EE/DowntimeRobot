using DowntimeRobot.Entity.Entity;
using DowntimeRobot.Service.BLL.Dto;
using DowntimeRobot.Service.BLL.Service;
using DowntimeRobot.Service.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DowntimeRobot.Admin.Controllers
{
    public class ApplicationController : ControllerBase
    {
        private readonly IApplicationService _applicationService;

        public ApplicationController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        public ActionResult Index()
        {
            int userId = UserData.Id;
            var result = _applicationService.GetList(userId);
            if (result.IsSuccess)
                return View(result.Data);

            return View();
        }

        // GET: Application/Create
        public ActionResult Create()
        {
            return View("CreateOrUpdate", new Application());
        }

        // POST: Application/Create
        [HttpPost]
        public ActionResult Create(Application application)
        {
            try
            {
                var result = _applicationService.Add(application);
                if (result.IsSuccess)
                    return RedirectToAction("Index");

                return View("CreateOrUpdate", application);
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        // GET: Application/Edit/5
        public ActionResult Edit(int id)
        {
            int userId = UserData.Id;
            var result = _applicationService.GetById(id, userId);
            if (result.IsSuccess)
                return View("CreateOrUpdate", result.Data);

            return RedirectToAction("Index");
        }

        // POST: Application/Edit
        [HttpPost]
        public ActionResult Edit(Application application)
        {
            try
            {
                var result = _applicationService.Update(application);
                if (result.IsSuccess)
                    return RedirectToAction("Index");

                return View("CreateOrUpdate", application);
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }


        // POST: Application/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                int userId = UserData.Id;
                var result = _applicationService.Delete(id, userId);
                if (result.IsSuccess)
                    return Json(result);

                return Json(new ServiceResponseBase { IsSuccess = false, Message = MessageService.Error });
            }
            catch (Exception ex)
            {
                return Json(new ServiceResponseBase { IsSuccess = false, Message = ex.ErrorMessage() });
            }
        }
    }
}
