﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DowntimeRobot.Service.Extensions
{
    public static class TypeExtension
    {
        public static T To<T>(this object input)
        {
            try
            {
                return (T)Convert.ChangeType(input, typeof(T));
            }
            catch
            {
                throw new Exception("Variable is not converting");
            }
        }
    }
}
