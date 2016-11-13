using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using DianYing.Web.Tool;

namespace DianYing.Web.Manage
{
    public partial class UploadImg : System.Web.UI.Page
    {
        public string imgPath = "";
        public string path = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            path = ConfigurationManager.AppSettings["ListPath"].ToString();
            imgPath =string.IsNullOrEmpty( this.Request["img"])? "/Images/ftp-img.jpg" : ConfigurationManager.AppSettings["ListPath"].ToString() + StringTool.SubStr(this.Request["img"], 2) + this.Request["img"] + ".jpg";
        }
    }
}