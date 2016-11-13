using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DianYing.BLL;
using DianYing.Web.Base;
using DianYing.Web.Tool;
using DianYing.Model;

namespace DianYing.Web.User
{
    public partial class Identify : BasePage
    {
        public string PageString;
        public int TotalCount;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Run();
                E_Identify eIdentify = new E_Identify();
                string action = this.Request["action"];
                switch (action)
                {
                    case "like":
                        eIdentify.StrWhere = "IsLike";
                        break;
                    case "plan":
                        eIdentify.StrWhere = "IsPlan";
                        break;
                    case "seen":
                        eIdentify.StrWhere = "IsRead";
                        break;
                    default:
                        eIdentify.StrWhere = "IsLike";
                        break;
                }

                TaskListBind(this.RepList, eIdentify);
            }

        }
        /// <summary>
        /// 绑定工作任务列表
        /// </summary>
        /// <param name="rpt"></param>
        private void TaskListBind(Repeater rpt, E_Identify eIdentify)
        {
            if (rpt != null)
            {
                B_Identify bal = new B_Identify();
                eIdentify.UserId = UserInfo.Id;
                eIdentify.PageSize = 15;
                if (eIdentify.CurrentPage <= 0) eIdentify.CurrentPage = 1;
                int count = 0;

                rpt.DataSource = bal.GetList(eIdentify, ref count);
                TotalCount = count;
                rpt.DataBind();
                int iPageCount = count / eIdentify.PageSize;
                if ((count % eIdentify.PageSize) > 0) iPageCount++;
                if (eIdentify.CurrentPage > iPageCount) eIdentify.CurrentPage = iPageCount;

                PageString = StringTool.ShowPageV(iPageCount, eIdentify.PageSize, eIdentify.CurrentPage, "");

            }
        }
    }
}