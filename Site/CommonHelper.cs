using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site
{
    public class CommonHelper
    {
        public static string GetValueBykey(string key)
        {
            return System.Configuration.ConfigurationManager.AppSettings[key];
        }

        public static string SsoUrl
        {
            get
            {
                return GetValueBykey("ssourl");
            }
        }
        public static string SsoLogin
        {
            get
            {
                return GetValueBykey("ssologin");
            }
        }

    }
}
