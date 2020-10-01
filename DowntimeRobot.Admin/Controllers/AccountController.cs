using DowntimeRobot.Admin.Helper;
using DowntimeRobot.Entity.Entity;
using DowntimeRobot.Service.BLL.Dto;
using DowntimeRobot.Service.BLL.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace DowntimeRobot.Admin.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        //================================
        // Business Logic
        //================================

        /*
        *  SignUp
        */
        [AllowAnonymous]
        public ActionResult Signup()
        {
            return View();
        }
        /*
         *  SignUp post 
         */
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Signup(UserDto model)
        {
            //sing out all froms auth
            FormsAuthentication.SignOut();
            //
            var result = _userService.SignUp(model);
            if (!result.IsSuccess)
            {
                //add error message
                ModelState.AddModelError("ErrorMessage", result.Message);
                return View("Login", model);
            }

            var user = _userService.GetById(result.EntityId);
            //	
            //user id 
            Session.Timeout = 1440; // one day as min
            Session["UserData"] = new User
            {
                Id = user.Data.Id,
                Name = user.Data.Name,
                Surname = user.Data.Surname,
                EmailAddress = user.Data.EmailAddress
            };
            //return main page
            return RedirectToAction("Index", "Home");
        }

        /*
        *  Login 
        */
        [AllowAnonymous]
        public ActionResult Login()
        {
            var user = CookieHelper.GetCookiesValue();
            if (user == null)
                return View(new LoginDto());

            return View(user);
        }

        /*
        *  Login post 
        */
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginDto model)
        {
            //sing out all froms auth
            FormsAuthentication.SignOut();
            //
            var result = _userService.Login(model);
            if (!result.IsSuccess)
            {
                //add error message
                ModelState.AddModelError("ErrorMessage", result.Message);
                return View("Login", model);
            }

            //	
            //save login data on cookie
            CookieHelper.SetCookiesValue(model);
            var user = _userService.GetByEmailAddress(model.EmailAddress);
            //
            //user id 
            Session["UserData"] = new User
            {
                Id = user.Data.Id,
                Name = user.Data.Name,
                Surname = user.Data.Surname,
                EmailAddress = user.Data.EmailAddress
            };
            //return main page
            return RedirectToAction("Index", "Application");
        }

        /*
        *  Login out
        */
        public ActionResult Logout()
        {
            //clear cookie
            CookieHelper.ClearCookies();
            //sing out all froms auth
            FormsAuthentication.SignOut();
            return View("Login", new LoginDto());
        }
    }
}