using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PageUI;

namespace DianYing.Web
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
         // PageHtml p = new PageHtml();
          //  p.GetPlay();
            string str = HttpContext.Current.Request.Url.Host.ToString();
        }
    }
}