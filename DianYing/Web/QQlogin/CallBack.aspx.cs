using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using XKCommon.QQConnect;
using Newtonsoft.Json;
using DianYing.Web.Tool;
using DianYing.Model;
using DianYing.BLL;

namespace DianYing.Web.QQlogin
{
    public partial class CallBack : System.Web.UI.Page
    {
        private string Code { get { return Request.QueryString["code"]; } }

        private string State { get { return Request.QueryString["state"]; } }

        private string ComeUrl = "/";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;


            if (string.IsNullOrEmpty(Code) || string.IsNullOrEmpty(State))
            {
                Response.Write("error:null");
                Response.End();
            }

            if (Request.Cookies["QQLoginSN"] == null)
            {
                Response.Write("error:exist");
                Response.End();
            }
            ComeUrl = Request.Cookies["QQLoginSN"].Value;
            Response.Cookies["QQLoginSN"].Value = null;

            if (StringTool.Md5(ComeUrl) != State)
            {
                Response.Write("error:state;");
                Response.End();
            }

            string RequestStr;

            //获取access_token
            RequestStr = StringTool.SendWebRequest(QQApiUrl.AccessToken(Code), null, "GET", Encoding.UTF8, Encoding.UTF8).Replace(" ", "");
            if (RequestStr.Substring(0, 12) != "access_token")
            {
                Response.Write("error:access_token");
                Response.End();
            }
            string accessToken = RequestStr.Substring(13, RequestStr.IndexOf("&") - 13);
            //获取OpenID
            RequestStr = StringTool.SendWebRequest(QQApiUrl.OpenID(accessToken), null, "GET", Encoding.UTF8, Encoding.UTF8);
            if (RequestStr.IndexOf("callback") > -1)
            {
                int iStart = RequestStr.IndexOf("(") + 1;
                RequestStr = RequestStr.Substring(iStart, RequestStr.IndexOf(")") - iStart);
            }
            QQUser QUser = JsonConvert.DeserializeObject<QQUser>(RequestStr);
            if (QUser == null)
            {
                Response.Write("error:openid");
                return;
            }
            if (QUser.ret != 0)
            {
                Response.Write("error:" + QUser.msg);
                return;
            }
            string openId = QUser.openid;

            //获取UserInfo
            RequestStr = StringTool.SendWebRequest(QQApiUrl.Get_User_Info(accessToken, openId), null, "GET", Encoding.UTF8, Encoding.UTF8);

            if (RequestStr.IndexOf("callback") > -1)
            {
                int iStart = RequestStr.IndexOf("(") + 1;
                RequestStr = RequestStr.Substring(iStart, RequestStr.IndexOf(")") - iStart);
            }
            QUser = JsonConvert.DeserializeObject<QQUser>(RequestStr);
            if (QUser == null)
            {
                Response.Write("error:info");
                return;
            }
            if (QUser.ret != 0)
            {
                Response.Write("error:" + QUser.msg);
                return;
            }
            QUser.openid = openId;
            QUser.accesstoken = accessToken;
            doLogin(QUser);
           // Response.Write("openid:" + openId + "--nickenanme:" + QUser.nickname);

         
        }
        public void doLogin(QQUser QUser)
        {
            B_User bll = new B_User();
            int userID = 0;
            bool toInsert = false;
            E_User eUser = new E_User();

            eUser.UserName = QUser.openid;
            eUser.NickName = QUser.nickname;
            eUser.PassWord = StringTool.GetMd5Hash(QUser.openid + "diyibk");
            eUser.AccessToken = QUser.accesstoken;
            eUser.Figureurl = QUser.figureurl;
            eUser.Figureurl_1 = QUser.figureurl_1;
            eUser.Figureurl_2 = QUser.figureurl_2;
            eUser.LastTime = DateTime.Now.ToString();
            eUser.Type = 1;
            userID = bll.IsExist(eUser);
            if (userID <= 0)
            {
                toInsert = true;
            }

            if (toInsert)
            {
                userID = bll.Add(eUser);
            }
            else
            {
                bll.Update(eUser);
            }
            //登陆成功
            HttpCookie cookie = new HttpCookie("diyibk");//初使化并设置Cookie的名称

            DateTime dt = DateTime.Now;

            TimeSpan ts = new TimeSpan(365, 0, 0, 0, 0);//过期时间为1分钟

            cookie.Expires = dt.Add(ts);//设置过期时间

            cookie.Values.Add("userId", userID.ToString());

            cookie.Values.Add("userName", eUser.UserName);
            cookie.Values.Add("nickName", eUser.NickName);
            cookie.Values.Add("passWord", eUser.PassWord);

            Response.AppendCookie(cookie);
            Response.Redirect(Server.UrlDecode(ComeUrl));

        }


    }
}