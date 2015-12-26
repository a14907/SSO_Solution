using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Server.Models
{
    public class CommonHelper
    {
        private static Random random = new Random();
        internal static string GetPwd(string pwd)
        {
            MD5 md = MD5.Create();
            var buff = md.ComputeHash(Encoding.UTF8.GetBytes(pwd));
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < buff.Length; i++)
            {
                sb.Append(buff[i].ToString("X2"));
            }
            return sb.ToString();
        }

        public static string GetValueBykey(string key)
        {
            return System.Configuration.ConfigurationManager.AppSettings[key];
        }

        public static string LoginCookieName
        {
            get
            {
                return GetValueBykey("LoginCookieName");
            }
        }

    }
}