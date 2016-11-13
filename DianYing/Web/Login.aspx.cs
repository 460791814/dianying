using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DianYing.BLL;
using DianYing.Model;
using System.Security.Cryptography;
using System.Text;
using DianYing.Web.Tool;
using System.Configuration;

namespace DianYing.Web
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string userName = this.Request["account"];
            string passWord = this.Request["password"];
            string method = this.Request["method"];
            string url = this.Request["url"];
            string rePassword = this.Request["re_password"];
            string yzm = this.Request["yzm"];
            B_User bal = new B_User();
            E_User eUser = new E_User();
            if (method == "same")
            {
                eUser.UserName = userName;
                if (!string.IsNullOrEmpty(userName))
                {
                    Response.Write(bal.IsExistName(eUser.UserName));
                    Response.End();

                }
            }
            if (method == "register")
            {
                if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(passWord))
                {
                    if (passWord == rePassword)
                    {
                       if(!string.IsNullOrEmpty(yzm)){
                           if (Session["code"].ToString().ToLower() == yzm.ToLower())
                           {
                               eUser.UserName = userName;
                               eUser.PassWord = StringTool.GetMd5Hash(passWord + "diyibk");
                               int userId = bal.Insert(eUser);
                               if (userId > 0)
                               {
                                   //登陆成功
                                   HttpCookie cookie = new HttpCookie("diyibk");//初使化并设置Cookie的名称

                                   DateTime dt = DateTime.Now;

                                   TimeSpan ts = new TimeSpan(365, 0, 0, 0, 0);//过期时间为1分钟

                                   cookie.Expires = dt.Add(ts);//设置过期时间

                                   cookie.Values.Add("userId", userId.ToString());

                                   cookie.Values.Add("userName", eUser.UserName);
                                   cookie.Values.Add("nickName", eUser.UserName);

                                   cookie.Values.Add("passWord", eUser.PassWord);

                                   Response.AppendCookie(cookie);
                                   Response.Write("success");
                                   Response.End();

                               }
                               else
                               {
                                   Response.Write("注册失败！");
                                   Response.End();


                               }
                           }
                           else
                           {
                               Response.Write("验证码错误！");
                               Response.End();
                           }
                        }
                       
                    }
                }

            }
            if (method == "login")
            {
                eUser.PassWord =StringTool.GetMd5Hash(passWord + "diyibk");
                eUser.UserName = userName;
                int userId = bal.IsExist(eUser);
                if (userId > 0)
                {
                    //登陆成功
                    HttpCookie cookie = new HttpCookie("diyibk");//初使化并设置Cookie的名称

                    DateTime dt = DateTime.Now;

                    TimeSpan ts = new TimeSpan(365, 0, 0, 0, 0);//过期时间为1分钟

                    cookie.Expires = dt.Add(ts);//设置过期时间

                    cookie.Values.Add("userId", userId.ToString());

                    cookie.Values.Add("userName", eUser.UserName);
                    cookie.Values.Add("nickName", eUser.NickName);
                    cookie.Values.Add("passWord", eUser.PassWord);

                    Response.AppendCookie(cookie);
                    //Response.Redirect(Server.UrlDecode(url));
                    Response.Write("success");
                    Response.End();
                }
                else
                {
                    Response.Write("false");
                    Response.End();
                }

            }
            if (method == "login_out")
            {

                //登陆成功
                HttpCookie cookie = Request.Cookies["diyibk"];

                if (cookie != null)
                {
                    cookie.Expires = DateTime.Now.AddDays(-1);//设置过期时间
                    Response.Cookies.Add(cookie);
                }
                Response.Redirect(url);
              //  Response.End();
            }

            if (method == "managelogin")
            { 
               //后台登陆
                if (userName == ConfigurationManager.AppSettings["AdminName"] && passWord == ConfigurationManager.AppSettings["AdminPassword"])
                { 
             
              
                    //登陆成功
                    HttpCookie cookie = new HttpCookie("managediyibk");//初使化并设置Cookie的名称

                    DateTime dt = DateTime.Now;



                    cookie.Values.Add("userName", userName);

                    cookie.Values.Add("passWord", passWord);

                    Response.AppendCookie(cookie);
               
                    Response.Write("success");
                    Response.End();

                }
                else
                {
               
                    Response.Write("false");
                    Response.End();
                }

            }

        }


    }
}