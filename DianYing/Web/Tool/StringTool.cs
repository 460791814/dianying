using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using System.Text;
using System.Security.Cryptography;

namespace DianYing.Web.Tool
{
    /// <summary>
    /// 字符串工具
    /// </summary>
    public static class StringTool
    {
        /// <summary>
        /// 将字符串转换为数字
        /// </summary>
        /// <param name="Str">待转换字符串</param>
        /// <returns>转换后的数字</returns>
        public static int GetInt(string Str)
        {
            if (String.IsNullOrEmpty(Str)) return 0;
            try
            {
                if (Str.IndexOf('.') > -1)
                {
                    return Convert.ToInt32(Convert.ToSingle(Str));
                }
                else
                {
                    return Convert.ToInt32(Str);
                }
            }
            catch { return 0; }
        }
        /// <summary>
        /// 判断是否为数字(包含小数点)
        /// </summary>
        /// <param name="Str"></param>
        /// <returns></returns>
        public static bool IsNumeric(string Str)
        {
            if (String.IsNullOrEmpty(Str)) return false;
            for (int i = 0; i < Str.Length; i++)
            {
                if (Char.IsNumber(Str[i]) == false)
                {
                    if (Str[i] != '.')
                        return false;
                }
            }
            return true;
        }
        /// <summary>
        /// 将字符串转换为双精度数字
        /// </summary>
        /// <param name="Str">待转换字符串</param>
        /// <returns>转换后的双精度数字</returns>
        public static Double GetDouble(string Str)
        {
            if (IsNumeric(Str))
                return Convert.ToDouble(Str);
            else
                return 0;
        }
        /// <summary>
        /// 将字符串转换为十进制数字
        /// </summary>
        /// <param name="Str"></param>
        /// <returns></returns>
        public static Decimal GetDecimal(string Str)
        {
            if (IsNumeric(Str))
                return Convert.ToDecimal(Str);
            else
                return 0;
        }
        /// <summary>
        /// 字符串截取
        /// </summary>
        /// <param name="str"></param>
        /// <param name="sum"></param>
        /// <returns></returns>
        public static string SubStr(string str,int sum)
        {
            string s = "";
            if (!string.IsNullOrEmpty(str))
            {
                if (str.Length > sum)
                {
                    s = str.Substring(0, sum);
                }
            }
            return s;
        }
        /// <summary>
        /// 获取32位Md5加密字符串
        /// </summary>
        /// <param name="dataStr">等加密字符串</param>
        /// <returns>32位Md5字符串</returns>
        public static string Md5(string dataStr)
        {
            return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(dataStr, "MD5").ToLower();
        }
        /// <summary>
        /// 取得输入字符串的MD5哈希值
        /// </summary>
        /// <param name="argInput">输入字符串</param>
        /// <returns>MD5哈希值</returns>
        public static string GetMd5Hash(string str)
        {
            byte[] by = System.Text.Encoding.UTF8.GetBytes(str);
            MD5 md5 = MD5.Create();
            byte[] md5by = md5.ComputeHash(by);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < md5by.Length; i++)
            {
                sb.Append(md5by[i].ToString("x2"));
            }
            return sb.ToString();
        }
        /// <summary>
        /// 发送一个WebRequest请求，并返回页面代码
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="data">请求数据</param>
        /// <param name="method">请求方式(GET/POST)</param>
        /// <param name="wEnc">发送数据编码(Encoding.UTF8)</param>
        /// <param name="rEnc">接收数据编码(Encoding.UTF8)</param>
        /// <returns>返回接收到的页面内容</returns>
        public static string SendWebRequest(string url, string data, string method, Encoding wEnc, Encoding rEnc)
        {
            WebResponse WRes;
            WebRequest WReq = WebRequest.Create(url);
            WReq.Method = string.Compare(method, "GET", true) == 0 ? "GET" : "POST";
            WReq.ContentType = "application/x-www-form-urlencoded";
            //WReq.ContentType = "text/html";
            WReq.Credentials = CredentialCache.DefaultCredentials;
            WReq.Timeout = 10000;//10秒
            if (string.IsNullOrEmpty(data) == false)
            {
                byte[] dataByte = wEnc.GetBytes(data);
                WReq.ContentLength = dataByte.Length;
                Stream writer = WReq.GetRequestStream();
                writer.Write(dataByte, 0, dataByte.Length);
                writer.Close();
            }
            try { WRes = WReq.GetResponse(); }
            catch (WebException WExc)
            {
                #region 获取错误信息流-艾延路（便于获取新浪微博错误码）
                if (WExc.Response != null)
                {
                    using (StreamReader reader = new StreamReader(WExc.Response.GetResponseStream()))
                    {
                        string errorInfo = reader.ReadToEnd();
                        reader.Close();
                    }
                }
                #endregion

                throw new Exception(string.Format("message:{0};status:{1};url:{2};data:{3}", WExc.Message, WExc.Status, url, data));
                //WRes = WExc.Response;
            }
            StreamReader SRea = new StreamReader(WRes.GetResponseStream(), rEnc);
            string reString = SRea.ReadToEnd();
            WRes.Close(); SRea.Dispose();
            return reString;
        }

        /// <summary>
        /// 生成翻页代码
        /// </summary>
        /// <param name="totalPageCount">总页数</param>
        /// <param name="pageSize">每页数据量</param>
        /// <param name="currentPageIndex">当前页</param>
        /// <param name="sLinkUrl">跳转地址</param>
        /// <returns></returns>
        public static string ShowPageV(int totalPageCount, int pageSize, int currentPageIndex, string sLinkUrl)
        {
            int proPage = currentPageIndex - 1;//上一页
            int nextPage = proPage + 2;//下一页
            if (proPage < 1)
            {
                proPage = 1;
            }
            if (nextPage > totalPageCount)
            {
                nextPage = totalPageCount;
            }
            if (totalPageCount < 1 || pageSize < 1)
            {
                return "";
            }
            int start = currentPageIndex - (int)(Math.Ceiling(Convert.ToDouble(pageSize / 2)) - 1);
            if (pageSize < totalPageCount)
            {
                if (start < 1)
                {
                    start = 1;
                }
                else if (start + pageSize > totalPageCount)
                {
                    start = totalPageCount - pageSize + 1;
                }
            }
            else
            {
                start = 1;
            }
            int end = start + pageSize - 1;
            //int end = start + 10;
            if (end > totalPageCount)
            {
                end = totalPageCount;
            }
            StringBuilder newNumberStr = new StringBuilder();
            if (currentPageIndex <= 1)
            {
                newNumberStr.Append("<a href=\"javascript:void(0);\" disabled=\"disabled\" class=\"disabled\">首页</a><a href=\"javascript:void(0);\" disabled=\"disabled\" class=\"disabled\">上一页</a>");
            }
            else
            {
                newNumberStr.AppendFormat("<a href=\"{0}~page_1\" >首页</a><a href=\"{0}~page_{1}\" >上一页</a>", sLinkUrl, proPage);
            }
            for (var i = start; i <= end; i++)
            {

                if (i == currentPageIndex)
                {
                    newNumberStr.AppendFormat("<a title='" + i + "' href=\"javascript:void(0);\" class=\"current\">" + i + "</a>");
                }
                else
                {
                    newNumberStr.AppendFormat("<a title='" + i + "' href=\"{0}~page_{1}\">" + i + "</a>", sLinkUrl, i);
                }
            }
            if (currentPageIndex == totalPageCount)
            {
                newNumberStr.Append("<a href=\"javascript:void(0);\" disabled=\"disabled\" class=\"disabled\">下一页</a><a href=\"javascript:void(0);\" disabled=\"disabled\" class=\"disabled\">末页</a>");
            }
            else
            {
                newNumberStr.AppendFormat("<a href=\"{0}&page_{1}\" >下一页</a><a href=\"{0}~page_{2}\" >末页</a>", sLinkUrl, nextPage, totalPageCount);
            }
            newNumberStr.AppendFormat("<a href=\"javascript:void(0);\" class=\"disabled\">共{0}页</a>", totalPageCount);
            if (totalPageCount > 1)
            {
                return newNumberStr.ToString();
            }
            else
            {
                return "";
            }
        }
    }
}