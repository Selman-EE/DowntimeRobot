using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DowntimeRobot.Api.Model
{
    public class ReqCreateToken
    {
        public string ApiKey { get; set; }
        public string SecretKey { get; set; }
    }
}