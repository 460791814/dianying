using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data;
using System.Text.RegularExpressions;
using System.Configuration;
using System.IO;

namespace DianYing.Web.Tool
{
    public class ManageTemplate
    {
        public ManageTemplate(string sPath)
        {
            Load(sPath);
        }
        public string KeyWord;
        /// <summary>
        /// 获取或设置模版HTML代码
        /// </summary>
        public string HTML { get { return _html.ToString(); } set { _html = new StringBuilder(value); } }
        private StringBuilder _html;
        /// <summary>
        /// 读取文件流
        /// </summary>
        /// <param name="sPath"></param>
        private void Load(string sPath)
        {
            string tempContent = "";
            string filePath = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["ManageDir"] + sPath);
            try
            {
                //打开文件
                StreamReader reader = new StreamReader(filePath, Encoding.GetEncoding("gb2312"));
                //读取流
                tempContent = reader.ReadToEnd();
                reader.Dispose();
                //  Utils.Cache(Config.TemplateDir + sPath, tempContent, new System.Web.Caching.CacheDependency(filePath), DateTime.Now.AddDays(2), System.Web.Caching.Cache.NoSlidingExpiration);
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write("Template.Err:" + sPath);
                HttpContext.Current.Response.Write("<br />Message:" + ex.Message);
                HttpContext.Current.Response.End();
            }
            _html = new StringBuilder(tempContent + "<!-- Read -->");

            //关闭流
        }
        /// <summary>
        /// 模板替换
        /// </summary>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        public void Replace(string oldValue, string newValue)
        {
            _html.Replace(oldValue, newValue);
        }
        /// <summary>
        /// 获取模版中某部分模版
        /// </summary>
        /// <param name="BStr">开始标记</param>
        /// <param name="EStr">结束标记</param>
        /// <returns>获取的模版</returns>
        public string GetPartContent(string BStr, string EStr)
        {
            string PartStr = string.Empty;
            if (_html.ToString().IndexOf(BStr) > 0)
            {
                PartStr = _html.ToString().Substring(_html.ToString().IndexOf(BStr) + BStr.Length);
                PartStr = PartStr.Substring(0, PartStr.IndexOf(EStr));
            
            }
            return PartStr;
        }
        /// <summary>
        /// 获取已经处理过的LoopContent标签内容
        /// </summary>
        /// <param name="dt">数据源(DataTable)</param>
        /// <param name="LoopTempate">模版内容</param>
        /// <returns></returns>
        public string GetLoopContent(DataTable dt, string LoopTempate)
        {
            if (dt == null)
            { return "当前无匹配记录！"; }
            LoopTempate = LoopTempate.Replace("{$", "{");
            StringBuilder LoopContent = new StringBuilder();
            foreach (DataRow dRow in dt.Rows)
            {
                Regex Re = new Regex(@"{(.[^{}]+)}");
                MatchCollection Mats = Re.Matches(LoopTempate);
                StringBuilder LoopItem = new StringBuilder(LoopTempate);
                foreach (Match Mat in Mats)
                {
                    LoopItem.Replace(Mat.Value, GetRowsHTML(dRow, Re.Replace(Mat.Value, "$1")));
                }
                LoopContent.Append(LoopItem.ToString());
                LoopItem = null; Re = null; Mats = null;
            }
            //LoopContent.Append("\n<!--<TemplateBegin>" + LoopTempate + "<TemplateEnd>-->\n");
            return LoopContent.ToString();
        }
        private string GetRowsHTML(DataRow dRow, string ParaStr)
        {
            ParaStr = ParaStr.Replace("(", ",");
            ParaStr = ParaStr.Replace(")", "");
            string[] Paras = ParaStr.Split(',');
            string labelName = Paras[0].ToLower();
            string DataStr = string.Empty;
            switch (labelName)
            {
                case "id":
                    return dRow["Id"].ToString();
                case "name":
                    return dRow["Name"].ToString();
                case "details":
                    return dRow["Details"].ToString();
              
                  
                default:
                    return dRow[labelName].ToString();// "默认的";
            }
        }






        ///// <summary>
        ///// 替换所有标签
        ///// </summary>
        //public void ReplaceAllFlag()
        //{
        //    _html.Replace("{$showheader}", Utils.ReadTemplate("_Header.htm"));
        //    _html.Replace("{$showfooter}", Utils.ReadTemplate("_Footer.htm"));
        //}


        public void Dispose()
        {
            _html = null;
        }
    }
}