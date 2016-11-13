using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace XKCommon.QQConnect
{
    /// <summary>
    /// 
    /// </summary>
    public class QQConfig
    {
        /// <summary>
        /// 应用ID
        /// </summary>
        public static string AppID
        {
            get
            {
                if (ConfigurationManager.AppSettings["QQAppID"] != null)
                    return ConfigurationManager.AppSettings["QQAppID"];
                return "101099769";
            }
        }

        /// <summary>
        /// 应用Key
        /// </summary>
        public static string AppKey
        {
            get
            {
                if (ConfigurationManager.AppSettings["QQAppKey"] != null)
                    return ConfigurationManager.AppSettings["QQAppKey"];
                return "1a614e003bb9fecc25847c26dd85e06b";
            }

        }
        /// <summary>
        /// 返回地址
        /// </summary>
        public static string RedirectUrl
        {
            get
            {
                if (ConfigurationManager.AppSettings["QQRedirectUrl"] != null)
                    return ConfigurationManager.AppSettings["QQRedirectUrl"];
                return "http://v.diyibk.com/QQlogin/CallBack.aspx";
            }
        }
    }
}
