using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DianYing.BLL;
using DianYing.Model;

namespace DianYing.Web.Handler
{
    /// <summary>
    /// IdentityHandler 的摘要说明
    /// </summary>
    public class IdentityHandler : IHttpHandler
    {
        public E_User UserInfo
        {
            get;
            set;
        }
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            UserInfo = new E_User();
            UserInfo.Id = 0;
            HttpCookie cookie = context.Request.Cookies["diyibk"];
            if (cookie != null)
            {

                UserInfo.Id = Convert.ToInt32(cookie.Values["userId"]);
                UserInfo.UserName = cookie.Values["userName"].ToString();
                UserInfo.PassWord = cookie.Values["passWord"].ToString();
            }
            if (UserInfo.Id > 0)
            {
                string method = context.Request["method"];

                int movieId = context.Request["movieid"] == null ? 0 : Convert.ToInt32(context.Request["movieid"]);
              //  bool isLike = context.Request["islike"] == null ? false : Convert.ToBoolean(context.Request["islike"]);
             //   bool isplan = context.Request["isplan"] == null ? false : Convert.ToBoolean(context.Request["isplan"]);
                bool status = context.Request["status"] == null ? false : Convert.ToBoolean(context.Request["status"]);

                B_Identify bal = new B_Identify();
                E_Identify e = new E_Identify();
                e.UserId = UserInfo.Id;
                e.MovieId = movieId;
                if (method == "like")
                {
                    e.IsLike = status;
                    if (bal.IsExist(e))
                    {
                        bal.UpdateLike(e);
                    }
                    else
                    {
                        bal.Insert(e);
                    }
                }
                if (method == "seen")
                {
                    e.IsRead = status;
                    e.IsPlan = false;
                    if (bal.IsExist(e))
                    {
                        bal.UpdateRead(e);
                    }
                    else
                    {
                        bal.Insert(e);
                    }
                }
                if (method == "plan")
                {
                    e.IsPlan = status;
                    e.IsRead = false;
                    if (bal.IsExist(e))
                    {
                        bal.UpdateRead(e);
                    }
                    else
                    {
                        bal.Insert(e);

                    }
                }
            }
            
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}