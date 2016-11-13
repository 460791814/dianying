using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Web;
using System.Web.Caching;
using System.Text.RegularExpressions;

namespace PageUI
{
    /// <summary>
    /// 验证
    /// </summary>
    public static class Safe
    {
        /// <summary>
        /// 是否通过验证
        /// </summary>
        /// <param name="KeyName"></param>
        /// <param name="WebSite">域名</param>
        /// <returns></returns>
        public static bool IsPass(string KeyName)
        {

            string host = HttpContext.Current.Request.Url.Host.ToString();
            if (host == "localhost" || IsIP(host))
            {
                //ip访问
                return true;
            }
            else
            {
                //绑定了域名
                string sql = "SELECT  Count(1)  FROM [dbo].[T_Key] where IsUse=1 and  KeyName=@KeyName  and  WebSite=@WebSite";
                SqlParameter[] para = new SqlParameter[]{
              new SqlParameter("@KeyName",KeyName),
              new SqlParameter("@WebSite",host)
             };
                object obj = ExecuteScalar(sql, para);
                if (Convert.ToInt32(obj) > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

        }
        /// <summary>
        /// 是否通过验证
        /// </summary>
        /// <param name="WebSite">域名</param>
        /// <returns></returns>
        public static bool IsPass()
        {

            string host = HttpContext.Current.Request.Url.Host.ToString();
            if (host == "localhost" || IsIP(host))
            {
                //ip访问
                return true;
            }
            else
            {
                //绑定了域名
                string sql = "SELECT  Count(1)  FROM [dbo].[T_Key] where IsUse=1 and    WebSite=@WebSite";
                SqlParameter[] para = new SqlParameter[]{
          
              new SqlParameter("@WebSite",host)
             };
                object obj = ExecuteScalar(sql, para);
                if (Convert.ToInt32(obj) > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

        }
        public static void SafePrint()
        {
           
            Cache cache = HttpRuntime.Cache;
            if (cache["safe"] == null)
            {
                bool bc = Safe.IsPass();
                cache.Insert("safe", bc, null, DateTime.Now.AddSeconds(86400), TimeSpan.Zero);
            }
            bool b = Convert.ToBoolean( cache["safe"]);
            if (b)
            {
            }
            else
            {
                HttpContext.Current.Response.Write("不要慌！联系源码作者激活域名即可正常使用，联系QQ：460791814");
                HttpContext.Current.Response.End();
            }
        }
        /// <summary>
        /// 是否为ip
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static bool IsIP(string ip)
        {
            if (string.IsNullOrEmpty(ip)) return false;
            return Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        }

        public static object ExecuteScalar(string sql, params SqlParameter[] pms)
        {
            using (SqlConnection conn = new SqlConnection("Data Source=115.28.213.122;Initial Catalog=Safe;User ID=safe;Password=asd456fgh"))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddRange(pms);
                    conn.Open();
                    return cmd.ExecuteScalar();
                }
            }
        }
    }
}
