using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DianYing.Web.Base;
using DianYing.BLL;

namespace DianYing.Web.User
{
    public partial class UserCenter : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Run();
            B_Identify bal = new B_Identify();
        }
    }
}