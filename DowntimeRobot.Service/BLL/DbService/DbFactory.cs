using DowntimeRobot.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DowntimeRobot.Service.BLL.Repository
{

    public class DbFactory
    {
        //connection string name in web config
        private const string DbContextKey = "DowntimeRobotDb";

        public static DownTimeRobotDbContext DbInstance
        {
            get
            {
                return InstanceContext();
            }
        }

        public static void RemoveContext()
        {
            if (HttpContext.Current != null && HttpContext.Current.Items[DbContextKey] != null)
            {
                ((ObjectContext)HttpContext.Current.Items[DbContextKey]).Dispose();
                HttpContext.Current.Items.Remove(DbContextKey);
            }
        }

        public static DownTimeRobotDbContext InstanceContext()
        {

            if (!System.Web.Hosting.HostingEnvironment.IsHosted)  //Console and windows applications use (!!İmportant after cache develops)
            {
                return SingletonContext.Instance;
            }


            if (!HttpContext.Current.Items.Contains(DbContextKey))
            {
                HttpContext.Current.Items.Add(DbContextKey, new DownTimeRobotDbContext());
            }

            return HttpContext.Current.Items[DbContextKey] as DownTimeRobotDbContext;
        }
    }


    public sealed class SingletonContext
    {
        private static volatile DownTimeRobotDbContext _instance;
        public static object SyncRoot { get; } = new object();
        private SingletonContext() { }

        public static DownTimeRobotDbContext Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (SyncRoot)
                    {
                        if (_instance == null)
                            _instance = new DownTimeRobotDbContext();
                    }
                }

                return _instance;
            }
        }
    }
}
