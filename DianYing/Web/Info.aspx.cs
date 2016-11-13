using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DianYing.Model;
using DianYing.BLL;
using System.Text;
using DianYing.Web.Base;
using DianYing.Web.Tool;
using System.Data;
using PageUI;

namespace DianYing.Web
{
    public partial class Info : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            PageHtml p = new PageHtml();
            p.GetInfo();

        }
     
    }
}