using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DianYing.Model;
using System.Text;
using DianYing.Web.Tool;
using Tool;

namespace DianYing.Web.Base
{
    public class BasePage : System.Web.UI.Page
    {

        public E_User UserInfo
        {
            get;
            set;
        }
        protected override void OnLoad(EventArgs e)
        {
            try
            {


                UserInfo = new E_User();
                UserInfo.Id = 0;
                HttpCookie cookie = Request.Cookies["diyibk"];
                if (cookie != null)
                {

                    UserInfo.Id = Convert.ToInt32(cookie.Values["userId"]);
                    UserInfo.UserName = cookie.Values["userName"].ToString();
                    UserInfo.NickName = cookie.Values["nickName"].ToString();
                    UserInfo.PassWord = cookie.Values["passWord"].ToString();
                }
            }
            catch (Exception)
            {
                //防止修改cookie时报System.NullReferenceException错误
                Response.Redirect("/Login.aspx?method=login_out&url=" + Request.Url);
            }
            base.OnLoad(e);
        }
        protected override void OnUnload(EventArgs e)
        {

            UserInfo = null;
            base.OnUnload(e);
        }
        public string GetLoginInfo()
        {
            StringBuilder loginStr = new StringBuilder();

            if (UserInfo.Id > 0)
            {
                loginStr.Append("<ul class=\"nav pull-right\"><li class=\"x-li-nickname\"><a rel=\"popover\" id=\"msg_ct_popover\" data-toggle=\"popover\" data-popover-show=\"0\" href=\"/User/UserCenter.aspx\" data-msg-ct=\"0\" data-original-title=\"\" title=\"\">");
                loginStr.Append("<i class=\"icon-user icon-white\"></i><font id=\"nav_nickname\">" + UserInfo.NickName + "</font></a></li>");
                loginStr.Append("<li id=\"nav_setting\" class=\"dropdown\"><a href=\"#\" class=\"dropdown-toggle\" data-toggle=\"dropdown\"><span>设置</span><span class=\"caret\"></span></a>");
                loginStr.Append("<ul class=\"dropdown-menu\"><li><a tabindex=\"-1\" href=\"/User/UserCenter.aspx\">个人信息</a></li><li class=\"divider\"></li><li><a tabindex=\"-1\" href=\"/Login.aspx?method=login_out&url=" + Request.Url + "\">注销</a></li></ul></li></ul>");

            }
            else
            {
                loginStr.Append("<ul class=\"nav pull-right\"><li class=\"x-li-nickname\"><a id=\"login\" class=\"login-register-dialog\" style=\"cursor: pointer;\"><i class=\"icon-user icon-white\"></i><font id=\"nav_nickname\">登录/注册 </font></a></li></ul>");
            }

            return loginStr.ToString();
        }
        public string CaseName(string str)
        {
            string name = "";
            switch (str)
            {
                case "m1905.com":
                    name = "电影网";
                    break;
                case "fun.tv":
                    name = "风行网";
                    break;
                case "letv.com":
                    name = "乐视";
                    break;
                case "iqiyi.com":
                    name = "爱奇艺";
                    break;
                case "sohu.com":
                    name = "搜狐";
                    break;
                case "kankan.com ":
                    name = "迅雷看看";
                    break;
                case "wasu.cn":
                    name = "华数";
                    break;
                case "pps.tv":
                    name = "PPS";
                    break;
                case "pptv.com":
                    name = "PPTV";
                    break;
                case "baofeng.com":
                    name = "暴风";
                    break;
                case "56.com":
                    name = "56";
                    break;
                case "cntv.cn":
                    name = "央视网";
                    break;
                case "61.com":
                    name = "淘米网";
                    break;
                case "kumi.cn":
                    name = "酷米";
                    break;
                case "hunantv.com":
                    name = "芒果TV";
                    break;
                case "tudou.com":
                    name = "土豆";
                    break;
                case "youku.com":
                    name = "优酷";
                    break;
                default:
                    name = str;
                    break;
            }
            return name;
        }
        /// <summary>
        /// 生成播放地址
        /// </summary>
        /// <param name="url"></param>
        /// <param name="movieId"></param>
        /// <param name="source"></param>
        /// <param name="movieName"></param>
        /// <returns></returns>
        public string GetUrl(string url, int movieId, string source, string movieName)
        {
            return "/play?url=" + url + "&mid=" + movieId + "&f=" + source + "&name=" + movieName;
        }
        /// <summary>
        /// 生成播放地址
        /// </summary>
        /// <param name="url"></param>
        /// <param name="movieId"></param>
        /// <param name="source"></param>
        /// <param name="movieName"></param>
        /// <returns></returns>
        public string GetUrl( int movieId)
        {
            return "/play?mid=" + movieId ;
        }
        public string Sub(string str, int i)
        {
            return StringTool.SubStr(str, i);
        }
        public Template head = new Template("header.htm");
        public Template foot = new Template("footer.htm");
        public string KeyWord = "";
        public void Run()
        {
           
            GetLoginInfo();

            head.Replace("{$logininfo}", GetLoginInfo());
            head.Replace("{$keyword}", KeyWord);
        }
    }
}