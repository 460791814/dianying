using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using XKCommon.QQConnect;
using DianYing.Web.Tool;

namespace DianYing.Web.QQlogin
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;

            string comeUrl = "http://v.diyibk.com/";//HttpContext.Current.Request["ComeUrl"];
            if (comeUrl.Length > 280)
                comeUrl = comeUrl.Substring(0, 280);

            Response.Cookies["QQLoginSN"].Value = comeUrl;
            //Session["ExistSN"] = comeUrl;

            Response.Redirect(QQApiUrl.AuthorizationCode(StringTool.Md5(comeUrl)));
        }

    }
}