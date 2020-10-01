using DowntimeRobot.Entity.Entity;
using DowntimeRobot.Service.BLL.Dto;
using DowntimeRobot.Service.BLL.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DowntimeRobot.Admin.Controllers
{
    public class JobController : ControllerBase
    {
        private readonly IJobService _jobService;
        private readonly IApplicationService _applicationService;

        public JobController(IJobService jobService, IApplicationService applicationService)
        {
            _jobService = jobService;
            _applicationService = applicationService;
        }
        public ActionResult Index()
        {
            int userId = UserData.Id;
            var result = _jobService.GetList(userId);
            if (result.IsSuccess)
                return View(result.Data);

            return View();
        }

        // GET: Job/Create
        public ActionResult Create()
        {
            ViewBag.AppList = _applicationService.GetList(UserData.Id).Data;
            return View("CreateOrUpdate", new Job());
        }

        // POST: Job/Create
        [HttpPost]
        public ActionResult Create(Job job)
        {
            try
            {
                var result = _jobService.Add(job);
                if (result.IsSuccess)
                    return RedirectToAction("Index");

                return View("CreateOrUpdate", job);
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        #region Job Edit Process
        //// GET: Job/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    int userId = UserData.Id;
        //    var result = _jobService.GetById(id, userId);
        //    if (result.IsSuccess)
        //        return View("CreateOrUpdate", result.Data);

        //    return RedirectToAction("Index");
        //}

        //// POST: Job/Edit
        //[HttpPost]
        //public ActionResult Edit(Job job)
        //{
        //    try
        //    {
        //        var result = _jobService.Update(job);
        //        if (result.IsSuccess)
        //            return RedirectToAction("Index");

        //        return View("CreateOrUpdate", job);
        //    }
        //    catch
        //    {
        //        return RedirectToAction("Index");
        //    }
        //}

        #endregion

        // POST: Job/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                int userId = UserData.Id;
                var result = _jobService.Delete(id, userId);
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
