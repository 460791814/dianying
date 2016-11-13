using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DianYing.Model;

using DianYing.Web.Tool;
using System.Configuration;

namespace DianYing.Web.Base
{
    public class ManagePage : System.Web.UI.Page
    {
        public E_User UserInfo
        {
            get;
            set;
        }
        protected override void OnUnload(EventArgs e)
        {

            UserInfo = null;
            base.OnUnload(e);
        }
        protected override void OnLoad(EventArgs e)
        {
         

                UserInfo = new E_User();
                UserInfo.Id = 0;
                HttpCookie cookie = Request.Cookies["managediyibk"];
                if (cookie != null)
                {

                    UserInfo.UserName = cookie.Values["userName"].ToString();
                    UserInfo.PassWord = cookie.Values["passWord"].ToString();

                    if (UserInfo.UserName == ConfigurationManager.AppSettings["AdminName"] && UserInfo.PassWord == ConfigurationManager.AppSettings["AdminPassword"]) { }
                    else
                    {
                        Login();
                    }

                }
                else
                {
                    Login();
                }
                base.OnLoad(e);
        }

        public void Login()
        {
            Response.Redirect("/Manage/AdminLogin.aspx");
        }
        public ManageTemplate head = new ManageTemplate("header.htm");
        public string Sub(string str, int i)
        {
            return StringTool.SubStr(str, i);
        }
    }
}