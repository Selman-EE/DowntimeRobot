using DowntimeRobot.Service.BLL.Dto;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace DowntimeRobot.Admin.Helper
{
    public class CookieHelper
    {
        public const string CookieName = "_DTRBT";

        //clear cookie
        public static void ClearCookies()
        {
            if (HttpContext.Current.Request.Cookies[CookieName] != null)
            {
                HttpCookie myCookie = new HttpCookie(CookieName);
                myCookie.Expires = DateTime.Now.AddDays(-1d);
                HttpContext.Current.Response.Cookies.Add(myCookie);
            }
        }
        public static LoginDto GetCookiesValue()
        {
            return GetLoginCookie();
        }
        public static void SetCookiesValue(LoginDto model)
        {
            SetLoginCookie(model);
        }


        //get value of the login info
        private static LoginDto GetLoginCookie()
        {
            if (HttpContext.Current.Request.Cookies.Get(CookieName) == null)
                return null;

            try
            {
                var data = FormsAuthentication.Decrypt(HttpContext.Current.Request.Cookies[CookieName].Value);
                return JsonConvert.DeserializeObject<LoginDto>(data.UserData);

            }
            catch
            {
                FormsAuthentication.SignOut();
                HttpContext.Current.Request.Cookies.Set(new HttpCookie(CookieName, ""));
                return null;
            }
        }

        //create cookie for login info
        private static void SetLoginCookie(LoginDto model)
        {
            ClearCookies();

            var userDataJson = JsonConvert.SerializeObject(model);

            //forms auth model
            var authTicket = new FormsAuthenticationTicket(3, FormsAuthentication.FormsCookieName, DateTime.Now, DateTime.Now.AddMonths(1), true, userDataJson, "/");

            //encrypt the ticket and add it to a cookie
            HttpCookie cookie = new HttpCookie(CookieName, FormsAuthentication.Encrypt(authTicket));

            //set response cookie
            HttpContext.Current.Response.SetCookie(cookie);
        }
    }
}