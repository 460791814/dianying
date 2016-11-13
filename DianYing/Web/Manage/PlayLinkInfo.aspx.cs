using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DianYing.BLL;
using DianYing.Model;
using System.Text;
using System.Data;
using Tool;
using DianYing.Web.Base;

namespace DianYing.Web.Manage
{
    public partial class PlayLinkInfo : ManagePage
    {
        public E_Movie em;
        public string Labels;
        public string Links;
        public string FirstURL;
        public int Mid;
        public string SourceID;
        public string ChannelID;
        public string tvStr;
        protected void Page_Load(object sender, EventArgs e)
        {
          //  Run();
            int movieId = this.Request["MovieId"] == null ? 1 : Convert.ToInt32(this.Request["MovieId"]);
            Mid = movieId;
            T_Movie tMovie = new T_Movie();
            E_Movie eMovie = new E_Movie();
           // eMovie.UserId = UserInfo.Id;
            eMovie.MovieId = movieId;
            em = tMovie.SelectMovie(eMovie);
            SourceID = em.SourceID;
            ChannelID = em.ChannelID.ToString();
            StringBuilder label = new StringBuilder();
            StringBuilder urls = new StringBuilder();
            StringBuilder tvPlay = new StringBuilder();
            bool isLast = true;
            bool firstLinks = true;
            bool isFistTv = true;
            string bofang = "";
            // string[] tabs = new string[] { "m1905.com", "letv.com", "iqiyi.com", "sohu.com", "kankan.com", "wasu.cn", "baofeng.com", "pps.tv", "pptv.com", "56.com", "fun.tv", "cntv.cn", "kumi.cn", "61.com", "hunantv.com", "tudou.com", "youku.com" };
         //   string[] tabs = tMovie.SelectSiteList(movieId).ToArray();
            string[] tabs = Data.DomainName;
            for (int i = 0; i < tabs.Length; i++)
            {

                DataRow[] list = tMovie.SelectSourceList(movieId).Select("site='" + tabs[i] + "'", "Episode");


              //  if (list.Length > 0)
             //   {
                    string img = tabs[i];
                    string labName = Data.CaseName(img);
                    if (isLast)
                    {
                        label.Append("<li class='active' style=''><a href='#tab_" + i + "' data-toggle='tab'><img style='width:16px;height:16px;' src='/picture/" + img + ".ico' alt='" + labName + "'>" + labName + "</a></li>");
                        urls.Append("<div class='tab-pane active' id='tab_" + i + "'><table class='table table-condensed table-striped table-hover'>");
                    }
                    else
                    {
                        label.Append("<li class='' style=''><a href='#tab_" + i + "' data-toggle='tab'><img style='width:16px;height:16px;' src='/picture/" + img + ".ico' alt='" + labName + "'>" + labName + "</a></li>");
                        urls.Append("<div class='tab-pane' id='tab_" + i + "'><table class='table table-condensed table-striped table-hover'>");
                    }
                    isLast = false;
                    for (int j = 0; j < list.Length; j++)
                    {

                        string codeUrl = "/play?url=" + list[j]["MovieURL"].ToString().Replace("#from_baidu", "") + "&mid=" + movieId + "&f=" + list[j]["Site"] + "&name=" + list[j]["MovieName"];

                        if (isFistTv && ChannelID == "2")
                        {
                            tvPlay.Append(" <li><a href='" + codeUrl + "' title='" + list[j]["MovieName"] + "'  target=\"_blank\" >" + list[j]["Episode"] + "</a></li>");
                        }
                        if (firstLinks)
                        {
                            bofang = codeUrl;
                        }
                        firstLinks = false;
                        urls.Append(@"<tr style=''><td>" + list[j]["MovieName"] + "</td><td class='span3' style='width:30%;'><a class='btn btn-primary btn-small' target='_blank' rel='nofollow' href='" + codeUrl + "'><i class='icon-white icon-play'></i>立即播放</a>&nbsp&nbsp&nbsp&nbsp<a  href=\"javascript:void(0);\" onclick=\"update('" + list[j]["Id"] + "')\" class=\"x-plan-remove btn btn-mini\"><i class=\"icon-check\"></i>修改</a>&nbsp&nbsp&nbsp&nbsp<a  href=\"javascript:void(0);\" onclick=\"del('" + list[j]["Id"] + "')\" class=\"x-plan-remove btn btn-mini\"> <i class=\"icon-remove\"></i> 移除</a></td></tr>");
                    }
                    urls.Append(@"<tr style=''><td><a class='btn btn-primary btn-small' target='_blank' rel='nofollow' onclick=add('" + movieId + "','" + img + "') href='javascript:void(0)'><i class='icon-white icon-play'></i>添加播放连接</a></td></tr>");
                    isFistTv = false;
                    tvStr = tvPlay.ToString();
                    urls.Append("</table></div>");
              //  }
            }


            //List<E_M> Hashlist = tMovie.SelectURLList(movieId, "T_Hash");

            //if (Hashlist != null && Hashlist.Count > 0)
            //{
            //    if (isLast)
            //    {
            //        label.Append("<li class='active'><a href='#tab_11' data-toggle='tab'><i class='icon-magnet'></i> 磁力链接</a></li>");
            //        urls.Append("<div class='tab-pane active' id='tab_11'><table class='table table-condensed table-striped table-hover'>");
            //    }
            //    else
            //    {
            //        label.Append("<li class=''><a href='#tab_11' data-toggle='tab'><i class='icon-magnet'></i> 磁力链接</a></li>");
            //        urls.Append("<div class='tab-pane' id='tab_11'><table class='table table-condensed table-striped table-hover'>");
            //    }

            //    for (int j = 0; j < Hashlist.Count; j++)
            //    {
            //        urls.Append(@"<tr class='resources' style=''><td style='word-break: break-all;'>" + Hashlist[j].Name + "</td><td class='span3'><a data-message='magnet' class='btn btn-mini x-tooltip' rel='nofollow' href='" + Hashlist[j].URL + "' alt=''><i class='icon-magnet'></i> </a><a class='btn btn-mini  XL_CLOUD_VOD_BUTTON_btn_b XL_CLOUD_VOD_BUTTON_yunyulan_btn_small' style='padding:0px;' tclass='' sclass='small' name='TD_CLOUD_VOD_BUTTON' href='javascript:void(0);' gcid='" + Hashlist[j].Name + "' filename='" + Hashlist[j].Name + "' url='" + Hashlist[j].URL + "' from='un_131661'></a></td></tr>");
            //    }
            //    urls.Append("</table></div>");
            //}
            Labels = label.ToString();
            Links = urls.ToString();
            FirstURL = bofang;
        }

        public string ALink(string url, string str)
        {
            StringBuilder link = new StringBuilder();
            if (!string.IsNullOrEmpty(str))
            {
                if (str.Contains("/"))
                {
                    string[] strs = str.Split('/');
                    for (int i = 0; i < strs.Length; i++)
                    {
                        link.Append("<a href='" + url + strs[i] + "'>" + strs[i] + "</a>  ");
                    }

                }
                else
                {
                    link.Append("<a href='" + url + str + "'>" + str + "</a> ");
                }
            }
            return link.ToString().TrimEnd('/');
        }
    }
}