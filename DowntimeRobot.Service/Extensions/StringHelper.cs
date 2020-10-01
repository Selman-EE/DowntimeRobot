using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DowntimeRobot.Service.Extensions
{
    public static class StringHelper
    {
        public static string GetAppSetting(this string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        public static bool IsValidUrl(this string input)
        {
            return Uri.TryCreate(input, UriKind.Absolute, out Uri uri)
                && (uri.Scheme == Uri.UriSchemeHttp
                 || uri.Scheme == Uri.UriSchemeHttps
                 || uri.Scheme == Uri.UriSchemeFtp
                 || uri.Scheme == Uri.UriSchemeMailto);
        }

        public static string CleanSpecialChars(this string input)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(input))
                    return "";

                input = input.Trim().Replace(" ", string.Empty);
                input = Regex.Replace(input, "[*'\",_&#*!%^$@]", "");
                string invalidRegStr = string.Format(@"([{0}]*\.+$)|([{0}]+)", Regex.Escape(new string(System.IO.Path.GetInvalidFileNameChars())));
                input = Regex.Replace(input, invalidRegStr, "_");
                invalidRegStr = string.Format(@"([{0}]*\.+$)|([{0}]+)", Regex.Escape(new string(System.IO.Path.GetInvalidPathChars())));

                return input;
            }
            catch
            {
                return input;
            }
        }
        public static bool IsValidName(this string input)
        {
            try
            {
                input = input.Replace(" ", string.Empty);
                return input.All(char.IsLetter);
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static bool IsValidPassword(this string password)
        {
            Regex passwordPolicyExpression = new Regex(@"((?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#!$%]).{6,32})");
            return passwordPolicyExpression.IsMatch(password);
        }
    }
}
