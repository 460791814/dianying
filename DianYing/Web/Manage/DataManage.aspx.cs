using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using DianYing.BLL;
using DianYing.Model;
using Tool;
using DianYing.Web.Tool;
using DianYing.Web.Base;

namespace DianYing.Web.Manage
{
    public partial class DataManage : ManagePage
    {
        public string PageString;
        public string sLinkUrl;
        public string userInfo;

        public E_Movie em;
        public string Y, A, T, O, K;
        public string KeyName = "";
        protected void Page_Load(object sender, EventArgs e)
        {

          //  string strWhere = this.Request["where"] == null ? "" : this.Request["where"];

            E_Movie eMovie = new E_Movie();
             eMovie.MovieName = this.Request["MovieName"];
             eMovie.BigImagePath = this.Request["BigImagePath"];
             if (!string.IsNullOrEmpty(eMovie.BigImagePath)) {
                 BigImagePath.Checked = true;
             }
            eMovie.ChannelID = StringTool.GetInt(this.Request["ChannelID"]);
          
            eMovie.Year = 0;
            eMovie.CurrentPage = StringTool.GetInt(this.Request["page"]); ;
            eMovie.OrderBy = "UpdateTime";
            em = eMovie;
            if (!IsPostBack)
            {

                TaskListBind(this.RepList, eMovie);
            }

        }
        public Model.E_Movie GetWhere(string strWhere)
        {
            DianYing.Model.E_Movie eMovie = new Model.E_Movie();
            eMovie.ChannelID = 0;
            eMovie.Year = 0;
            eMovie.CurrentPage = 0;
            eMovie.OrderBy = "UpdateTime";
            if (!string.IsNullOrEmpty(strWhere))
            {
                if (strWhere.Contains('~'))
                {
                    string[] ws = strWhere.Split('~');
                    for (int i = 0; i < ws.Length; i++)
                    {
                        GetCase(ws[i], eMovie);
                    }
                }
                else
                {
                    GetCase(strWhere, eMovie);
                }
            }
            em = eMovie;
            return eMovie;
        }
        public void GetCase(string strWhere, Model.E_Movie eMovie)
        {
            StringBuilder canShu = new StringBuilder("");
            string[] s = strWhere.Split('_');
            switch (s[0])
            {
                case "page":
                    eMovie.CurrentPage = Convert.ToInt32(s[1]);

                    break;
                case "channelid":
                    eMovie.ChannelID = Convert.ToInt32(s[1]);

                    break;
                case "year":
                    eMovie.Year = Convert.ToInt32(s[1]);

                    break;
                case "region":
                    eMovie.Area = s[1];

                    break;
                case "genre":
                    eMovie.Type = s[1];

                    break;
                case "orderby":
                    eMovie.OrderBy = s[1];

                    break;
                case "name":
                    eMovie.MovieName = s[1];
                  //  KeyWord = s[1];

                    break;
                case "key":
                    eMovie.Actor = s[1];
                    KeyName = s[1];
                    break;
                default:
                    break;
            }
        }
        public string GetLinks(E_Movie eMovie, string action)
        {

            StringBuilder link = new StringBuilder("DataManage.aspx?");
            if (eMovie.ChannelID != 0)
            {
                link.Append("&channelid=" + eMovie.ChannelID);
            }
            //if (!string.IsNullOrEmpty(eMovie.OrderBy))
            //{
            //    if (action != "orderby")
            //    {
            //        link.Append("~orderby_" + eMovie.OrderBy);
            //    }
            //}

            //if (eMovie.Year != 0)
            //{
            //    if (action != "year")
            //    {
            //        link.Append("~year_" + eMovie.Year);
            //    }
            //}
            //if (!string.IsNullOrEmpty(eMovie.Area))
            //{
            //    if (action != "region")
            //    {
            //        link.Append("~region_" + eMovie.Area);
            //    }
            //}
            //if (!string.IsNullOrEmpty(eMovie.Type))
            //{
            //    if (action != "genre")
            //    {
            //        link.Append("~genre_" + eMovie.Type);
            //    }
            //}
            //if (!string.IsNullOrEmpty(eMovie.Actor))
            //{
            //    if (action != "key")
            //    {
            //        link.Append("~key_" + eMovie.Actor);
            //    }
            //}

            if (!string.IsNullOrEmpty(eMovie.MovieName))
            {

                link.Append("&MovieName=" + eMovie.MovieName);
                
            }
            return link.ToString();

        }
        /// <summary>
        /// 绑定工作任务列表
        /// </summary>
        /// <param name="rpt"></param>
        private void TaskListBind(Repeater rpt, Model.E_Movie eMovie)
        {
            if (rpt != null)
            {
                T_Movie tMovie = new T_Movie();
               // eMovie.UserId = UserInfo.Id;
                eMovie.PageSize = 15;
                if (eMovie.CurrentPage <= 0) eMovie.CurrentPage = 1;
                int count = 0;

                rpt.DataSource = tMovie.SelectMovieList(eMovie, ref count);

                rpt.DataBind();
                int iPageCount = count / eMovie.PageSize;
                if ((count % eMovie.PageSize) > 0) iPageCount++;
                if (eMovie.CurrentPage > iPageCount) eMovie.CurrentPage = iPageCount;
             
                PageString = ShowPageV(iPageCount, eMovie.PageSize, eMovie.CurrentPage, GetLinks(eMovie, ""));

            }
        }

        /// <summary>
        /// 生成翻页代码
        /// </summary>
        /// <param name="totalPageCount">总页数</param>
        /// <param name="pageSize">每页数据量</param>
        /// <param name="currentPageIndex">当前页</param>
        /// <param name="sLinkUrl">跳转地址</param>
        /// <returns></returns>
        public static string ShowPageV(int totalPageCount, int pageSize, int currentPageIndex, string sLinkUrl)
        {
            int proPage = currentPageIndex - 1;//上一页
            int nextPage = proPage + 2;//下一页
            if (proPage < 1)
            {
                proPage = 1;
            }
            if (nextPage > totalPageCount)
            {
                nextPage = totalPageCount;
            }
            if (totalPageCount < 1 || pageSize < 1)
            {
                return "";
            }
            int start = currentPageIndex - (int)(Math.Ceiling(Convert.ToDouble(pageSize / 2)) - 1);
            if (pageSize < totalPageCount)
            {
                if (start < 1)
                {
                    start = 1;
                }
                else if (start + pageSize > totalPageCount)
                {
                    start = totalPageCount - pageSize + 1;
                }
            }
            else
            {
                start = 1;
            }
            int end = start + pageSize - 1;
            //int end = start + 10;
            if (end > totalPageCount)
            {
                end = totalPageCount;
            }
            StringBuilder newNumberStr = new StringBuilder();
            if (currentPageIndex <= 1)
            {
                newNumberStr.Append("<a href=\"javascript:void(0);\" disabled=\"disabled\" class=\"disabled\">首页</a><a href=\"javascript:void(0);\" disabled=\"disabled\" class=\"disabled\">上一页</a>");
            }
            else
            {
                newNumberStr.AppendFormat("<a href=\"{0}&page=1\" >首页</a><a href=\"{0}&page={1}\" >上一页</a>", sLinkUrl, proPage);
            }
            for (var i = start; i <= end; i++)
            {

                if (i == currentPageIndex)
                {
                    newNumberStr.AppendFormat("<a title='" + i + "' href=\"javascript:void(0);\" class=\"current\">" + i + "</a>");
                }
                else
                {
                    newNumberStr.AppendFormat("<a title='" + i + "' href=\"{0}&page={1}\">" + i + "</a>", sLinkUrl, i);
                }
            }
            if (currentPageIndex == totalPageCount)
            {
                newNumberStr.Append("<a href=\"javascript:void(0);\" disabled=\"disabled\" class=\"disabled\">下一页</a><a href=\"javascript:void(0);\" disabled=\"disabled\" class=\"disabled\">末页</a>");
            }
            else
            {
                newNumberStr.AppendFormat("<a href=\"{0}&page={1}\" >下一页</a><a href=\"{0}&page={2}\" >末页</a>", sLinkUrl, nextPage, totalPageCount);
            }
            newNumberStr.AppendFormat("<a href=\"javascript:void(0);\" class=\"disabled\">共{0}页</a>", totalPageCount);
            if (totalPageCount > 1)
            {
                return newNumberStr.ToString();
            }
            else
            {
                return "";
            }
        }

        public string GetType(string str)
        {
            string t = "";
            StringBuilder typeStr = new StringBuilder();
            if (!string.IsNullOrEmpty(str))
            {
                str = str.TrimEnd('/');
                if (str.Contains('/'))
                {
                    string[] ts = str.Split('/');
                    for (int i = 0; i < ts.Length; i++)
                    {
                        if (Data.type.Contains("/" + ts[i] + "/"))
                        {
                            typeStr.Append(ts[i] + " ");
                        }
                    }
                }
                else
                {

                    t = str;
                }
            }
            return typeStr.ToString();
        }

        public string QuChu(string str)
        {

            StringBuilder typeStr = new StringBuilder();
            if (!string.IsNullOrEmpty(str))
            {
                str = str.TrimEnd('/');
                if (str.Contains('/'))
                {
                    string[] ts = str.Split('/');
                    for (int i = 0; i < ts.Length; i++)
                    {

                        typeStr.Append(ts[i] + " ");

                    }
                }
                else
                {

                    typeStr.Append(str);
                }
            }
            return typeStr.ToString();

        }

        public string GetChannel(int? channelId)
        {
            StringBuilder channelStr = new StringBuilder();

            for (int i = 0; i < Data.ChannelList.Count; i++)
            {
                if (Data.ChannelList[i].ChannelID == channelId)
                {
                    channelStr.Append(" <option selected=\"selected\"  value=\""+Data.ChannelList[i].ChannelID+"\">"+Data.ChannelList[i].ChannelName+"</option>");
                }
                else {
                    channelStr.Append(" <option value=\"" + Data.ChannelList[i].ChannelID + "\">" + Data.ChannelList[i].ChannelName + "</option>");
                }
            }
            return channelStr.ToString();
        }
        public string GetTuiJian(object obj,string movieid)
        {
            if (obj!=null)
            {
                if (string.IsNullOrEmpty(obj.ToString()))
                {
                    return "<a href=\"javascript:void(0)\"  class=\"btn-mini mark btn x-tooltip\" title=\"推荐到首页\" data-toggle=\"tooltip\" data-type=\"plan\" onclick=\"SetHomeImg('" + movieid + "')\"><span>设为首页 </span></a>";
                }
                else {
                    return "<a href=\"javascript:void(0)\"  class=\"btn-mini mark btn x-tooltip\" title=\"从首页移除\" data-toggle=\"tooltip\" data-type=\"plan\" onclick=\"deletefromhome('" + movieid + "')\"><span>移除首页 </span></a>";
                }
              
            }
            else {
                return "<a href=\"javascript:void(0)\"  class=\"btn-mini mark btn x-tooltip\" title=\"推荐到首页\" data-toggle=\"tooltip\" data-type=\"plan\" onclick=\"SetHomeImg('" + movieid + "')\"><span>设为首页 </span></a>";
             }
        }
    
    }
}