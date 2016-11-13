using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DianYing.Web.Base;
using DianYing.BLL;
using DianYing.Model;
using PageUI;

namespace DianYing.Web
{

    public partial class Play : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            PageHtml p = new PageHtml();
            p.GetPlay();

        }
    }
  
}