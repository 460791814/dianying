using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tool
{
    public class Validate
    {
        /// <summary>
        /// 验证邮箱是否合法
        /// </summary>
        /// <param name="strEmail"></param>
        /// <returns>True(合法)/False</returns>
        public static bool IsEmail(string strEmail)
        {
            if (string.IsNullOrEmpty(strEmail)) return false;
            if ((strEmail.IndexOf("@") < 0)) return false;
            string TempWords = "abcdefghijklmnopqrstuvwxyz_-.0123456789@";
            char[] Chars = strEmail.ToLower().ToCharArray();
            foreach (char sChar in Chars)
            {
                if (TempWords.IndexOf(sChar) < 0)
                {
                    return false;
                }
            }
            return true;
        }
    }
}