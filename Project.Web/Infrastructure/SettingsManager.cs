using System;
using System.Configuration;
using System.Dynamic;

namespace Project.Web.Infrastructure
{
    public static class SettingsManager
    {
        public static bool UserLockoutEnabledByDefault
        {
            get { return Convert.ToBoolean(ConfigurationManager.AppSettings["UserLockoutEnabledByDefault"]); }
        }

        public static TimeSpan DefaultAccountLockoutTimeSpan
        {
            get
            {
                return
                    TimeSpan.FromMinutes(Double.Parse(ConfigurationManager.AppSettings["DefaultAccountLockoutTimeSpan"]));
            }
        }

        public static int MaxFailedAccessAttemptsBeforeLockout
        {
            get { return Convert.ToInt32(ConfigurationManager.AppSettings["MaxFailedAccessAttemptsBeforeLockout"]); }
        }
    }
}