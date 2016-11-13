using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DianYing.Model;
using DianYing.BLL;
using System.Web;
using System.Web.Caching;
using System.Data;
using Tool;

namespace PageUI
{
    public class PageHtml
    {
        public E_User UserInfo
        {
            get;
            set;
        }
        public Template head = new Template("web", "header.htm");
        public Template foot = new Template("web", "footer.htm");
        public PageHtml()
        {
            try
            {
         
                    UserInfo = new E_User();
                    UserInfo.Id = 0;
                    HttpCookie cookie = HttpContext.Current.Request.Cookies["diyibk"];
                    if (cookie != null)
                    {

                        UserInfo.Id = Convert.ToInt32(cookie.Values["userId"]);
                        UserInfo.UserName = cookie.Values["userName"].ToString();
                        UserInfo.NickName = cookie.Values["nickName"].ToString();
                        UserInfo.PassWord = cookie.Values["passWord"].ToString();
                    }
                    head.Replace("{$logininfo}", GetLoginInfo(UserInfo));
                    head.Replace("{$keyword}", "");
             

            }
            catch (Exception)
            {
                //防止修改cookie时报System.NullReferenceException错误
                HttpContext.Current.Response.Redirect("/Login.aspx?method=login_out&url=" + HttpContext.Current.Request.Url);
            }
          
        }
        /// <summary>
        /// 首页
        /// </summary>
        public void Default()
        {
            Safe.SafePrint();
            Cache cache = HttpRuntime.Cache;
            List<int> list = null;
            T_Movie tMovie = new T_Movie();
            if (cache["BigImg"] == null)
            {
                T_BigImg big = new T_BigImg();

                List<int> l = tMovie.SelectHomeMovieList();
                list = l;
                cache.Insert("BigImg", l, null, DateTime.Now.AddSeconds(86400), TimeSpan.Zero);
            }


            list = (List<int>)cache["BigImg"];


            Random R = new Random();
            int rId = R.Next(1, list.Count);
            E_Movie eMovie = new E_Movie();
            eMovie.MovieId = list[rId];
            eMovie.UserId = UserInfo.Id;

            eMovie = tMovie.SelectMovie(eMovie);


            //<%=em.IsRead?"revision-btn-seen x-tooltip unmark btn-success":"revision-btn-seen x-tooltip mark btn-normal" %>
            Template Tmplate = new Template("web", "default.htm");

            string seenStr = eMovie.IsRead ? "revision-btn-seen x-tooltip unmark btn-success" : "revision-btn-seen x-tooltip mark btn-normal";
            string seen = "<a href=\"#\" data-container=\"body\" class=\"" + seenStr + "\"title=\"标记为看过的电影\" data-title=\"取消标记为看过的电影\" data-toggle=\"tooltip\" data-id=\"" + eMovie.MovieId + "\" data-type=\"seen\"><i class=\"icon-check icon-white\" style=\"margin-top:26px;\"></i> <span></span></a>";

            string likeStr = eMovie.IsLike ? "revision-btn-like x-tooltip unmark btn-danger" : "revision-btn-like x-tooltip mark btn-normal";
            string like = "<a href=\"#\" data-container=\"body\" data-placement=\"top\" class=\" " + likeStr + "\" style=\"text-decoration:none;\" data-title=\"取消喜欢 ( 快捷键 L )\" title=\"喜欢该电影 ( 快捷键 L )\"data-toggle=\"tooltip\" data-id=\"" + eMovie.MovieId + "\" data-type=\"like\"><i class=\"icon-heart icon-white\" style=\"margin-top:26px;\"></i></a>";
            string planStr = eMovie.IsPlan ? "revision-btn-plan x-tooltip unmark btn-warning" : "revision-btn-plan x-tooltip mark btn-normal";

            string plan = "<a href=\"#\" data-container=\"body\" class=\"" + planStr + " \" data-title=\"取消计划观看该电影\"title=\"计划观看该电影\" data-title=\"取消计划观看该电影\" data-toggle=\"tooltip\" data-id=\"" + eMovie.MovieId + "\" data-type=\"plan\"><i class=\"icon-time icon-white\" style=\"margin-top:26px;\"></i> <span></span></a>";

            Tmplate.Replace("{MovieId}", eMovie.MovieId.ToString());
            Tmplate.Replace("{MovieName}", eMovie.MovieName);
            Tmplate.Replace("{DBScore}", eMovie.DBScore > 0 ? eMovie.DBScore.ToString() : "");
            Tmplate.Replace("{Type}", QuChu(eMovie.Type));
            Tmplate.Replace("{Actor}", QuChu(eMovie.Actor));
            Tmplate.Replace("{ImagePath}", "dyImg/bigImg/" + StringTool.SubStr(StringTool.GetString(eMovie.BigImagePath), 2) + "/" + eMovie.BigImagePath + ".jpg");


            Tmplate.Replace("{Like}", like);
            Tmplate.Replace("{Seen}", seen);
            Tmplate.Replace("{Plan}", plan);

            Tmplate.Replace("Link", GetUrl(eMovie.MovieId));

            Tmplate.Replace("{SubIntro}", StringTool.SubStr(eMovie.Intro, 100));
            Tmplate.Replace("{Intro}", eMovie.Intro);

            Tmplate.Replace("{header}", head.HTML);
            Tmplate.Replace("{footer}", foot.HTML);
            Utils.Write(Tmplate.HTML);
            Tmplate.Dispose();



        }
        /// <summary>
        /// 列表页
        /// </summary>
        public void GetList()
        {
            Safe.SafePrint();
            string strWhere = HttpContext.Current.Request["where"] == null ? "" : HttpContext.Current.Request["where"];
            E_Movie eMovie = new E_Movie();
            eMovie.CurrentPage = StringTool.GetInt(HttpContext.Current.Request["page"]);
            eMovie.ChannelID = StringTool.GetInt(HttpContext.Current.Request["channelid"]);
            eMovie.Year = StringTool.GetInt(HttpContext.Current.Request["year"]);
            eMovie.Area = HttpContext.Current.Server.UrlDecode(HttpContext.Current.Request["region"]);
            eMovie.Type = HttpContext.Current.Server.UrlDecode(HttpContext.Current.Request["genre"]);
            eMovie.OrderBy = HttpContext.Current.Request["orderby"] == null ? "UpdateTime" : HttpContext.Current.Request["orderby"];
            eMovie.Actor = HttpContext.Current.Request["key"];
            eMovie.MovieName = HttpContext.Current.Server.UrlDecode(HttpContext.Current.Request["keyword"]);
            T_Movie tMovie = new T_Movie();
            eMovie.UserId = UserInfo.Id;
            eMovie.PageSize = 15;
            if (eMovie.CurrentPage <= 0) eMovie.CurrentPage = 1;
            int count = 0;
            List<E_Movie> list = tMovie.SelectMovieList(eMovie, ref count);

            int iPageCount = count / eMovie.PageSize;
            if ((count % eMovie.PageSize) > 0) iPageCount++;
            if (eMovie.CurrentPage > iPageCount) eMovie.CurrentPage = iPageCount;
            //Y = GetLinks(eMovie, "year");
            //A = GetLinks(eMovie, "region");
            //O = GetLinks(eMovie, "orderby");
            //T = GetLinks(eMovie, "genre");
            //K = GetLinks(eMovie, "key");
            string PageString = ShowPageV(iPageCount, eMovie.PageSize, eMovie.CurrentPage, "?" + GetLinks(eMovie, ""));
            Template Tmplate = new Template("web", "list.htm");
            string movieListTmplate = Tmplate.GetPartContent("<!--<MovieListBegin>", "<MovieListEnd>-->");

            string content = Tmplate.GetLoopContent(list, movieListTmplate);
            Tmplate.Replace("{Channel}", GetChannelById(eMovie.ChannelID));
            Tmplate.Replace("{ChannelList}", GetChannels());
            Tmplate.Replace("{Year}", eMovie.Year > 0 ? eMovie.Year.ToString() : "不限");
            Tmplate.Replace("{YearList}", GetYear(GetLinks(eMovie, "year")));

            Tmplate.Replace("{Area}", string.IsNullOrEmpty(eMovie.Area) ? "不限" :eMovie.Area.ToString());
            Tmplate.Replace("{AreaList}", GetArea(GetLinks(eMovie, "region")));

            Tmplate.Replace("{Type}", string.IsNullOrEmpty(eMovie.Type) ? "不限" : eMovie.Area.ToString());
            Tmplate.Replace("{TypeList}", GetType(GetLinks(eMovie, "genre")));
            Tmplate.Replace("{KeyWord}", eMovie.MovieName);//{KeyWord}
            Tmplate.Replace("{OrderBy}", GetCurrentOrder(eMovie));
            Tmplate.Replace("{OrderByList}", GetOrder(GetLinks(eMovie, "orderby")));
            Tmplate.Replace("{KeyName}", GetKeyName(eMovie.Actor, GetLinks(eMovie, "key")));
            Tmplate.Replace("{PageString}", PageString);
            Tmplate.Replace("{MovieList}", content);
            Tmplate.Replace("{header}", head.HTML);
            Tmplate.Replace("{footer}", foot.HTML);
            Utils.Write(Tmplate.HTML);
            Tmplate.Dispose();
        }
        /// <summary>
        /// 获取当前排序对象
        /// </summary>
        /// <param name="em"></param>
        /// <returns></returns>
        public string GetCurrentOrder(E_Movie em)
        {
            string str = "";
            if (em.OrderBy == "IMDBScore")
            {
                str = "按IMDB评分排序";
            }
            else if (em.OrderBy == "DBScore")
            {
                str = "按豆瓣评分排序";
            }
            else if (em.OrderBy == "year")
            {
                str = "按年份排序";
            }
            else
            {
                str = "按更新时间排序";
            }
            return str;
        }
        /// <summary>
        /// 分类列表
        /// </summary>
        /// <returns></returns>
        public string GetChannels()
        {
            StringBuilder c = new StringBuilder();
            List<E_Channel> channel = Data.ChannelList;
            for (int i = 0; i < channel.Count(); i++)
            {
                c.Append("<li><a tabindex=\"-1\" href=\"/category/?channelid=" + channel[i].ChannelID + "\">" + channel[i].ChannelName + "</a></li>");
            }
            return c.ToString();
        }
        /// <summary>
        /// 根据ID获取分类名称
        /// </summary>
        /// <returns></returns>
        public string GetChannelById(int? channelid)
        {

            List<E_Channel> channel = Data.ChannelList;
            for (int i = 0; i < channel.Count(); i++)
            {
                if (channel[i].ChannelID == channelid)
                {
                    return channel[i].ChannelName;
                }
            }
            return "所有频道";
        }
        /// <summary>
        /// 年份列表
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string GetYear(string str)
        {
            StringBuilder c = new StringBuilder("<li><a tabindex=\"-1\" href=\"/category/?year=" + str + "\">不限</a></li>");

            for (int i = DateTime.Now.Year; i >= 1990; i--)
            {
                c.Append(" <li><a tabindex=\"-1\" href=\"/category/?year=" + i + "" + str + "\">" + i + "</a></li>");
            }
                        c.Append("<li><a tabindex=\"-1\" href=\"/category/?year=80"+str+"\">80年代</a></li>");
                        c.Append("<li><a tabindex=\"-1\" href=\"/category/?year=70"+str+"\">70年代</a></li>");
                        c.Append("<li><a tabindex=\"-1\" href=\"/category/?year=60"+str+"\">60年代</a></li>");
                        c.Append("<li><a tabindex=\"-1\" href=\"/category/?year=50"+str+"\">50年代</a></li>");
                        c.Append("<li><a tabindex=\"-1\" href=\"/category/?year=40"+str+"\">40年代</a></li>");
                       c.Append(" <li><a tabindex=\"-1\" href=\"/category/?year=30"+str+"\">30年代</a></li>");
                       c.Append(" <li><a tabindex=\"-1\" href=\"/category/?year=20"+str+"\">20年代</a></li>");

        
            return c.ToString();
        }
        /// <summary>
        /// 地区列表
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string GetArea(string str)
        {
            StringBuilder c = new StringBuilder("<li><a tabindex=\"-1\" href=\"/category/?region=" + str + "\">不限</a></li>");
            string[] a = Data.MovieArea;
            for (int i = 0; i < a.Length; i++)
            {
                c.Append(" <li><a tabindex=\"-1\" href=\"/category/?region=" + HttpContext.Current.Server.UrlEncode(a[i]) + str + "\">" + a[i] + "</a></li>");
            }
            return c.ToString();
        }
        /// <summary>
        /// 类型
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string GetType(string str)
        {
            StringBuilder c = new StringBuilder("<li><a tabindex=\"-1\" href=\"/category/?genre=" + str + "\">不限</a></li>");
            string[] t = Data.MovieType;
            for (int i = 0; i < t.Length; i++)
            {
                c.Append(" <li><a tabindex=\"-1\" href=\"/category/?genre=" + HttpContext.Current.Server.UrlEncode(t[i]) + str + "\">" + t[i] + "</a></li>");
            }
            return c.ToString();
        }

        /// <summary>
        /// 排序列表
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string GetOrder(string str)
        {
            StringBuilder c = new StringBuilder();
            c.Append("<li><a tabindex=\"-1\" href=\"/category/?orderby=UpdateTime" + str + "\">按更新时间排序</a></li>");
            c.Append("<li><a tabindex=\"-1\" href=\"/category/?orderby=DBScore" + str + "\">按豆瓣评分排序</a></li>");
            c.Append("<li><a tabindex=\"-1\" href=\"/category/?orderby=year" + str + "\">按年份排序</a></li>");
            return c.ToString();
        }
        /// <summary>
        /// 关键字查询
        /// </summary>
        /// <param name="key"></param>
        /// <param name="str"></param>
        /// <returns></returns>
        public string GetKeyName(string key, string str)
        {
            if (!string.IsNullOrEmpty(key))
            {

                return "<li><a class=\"x-cat-tag\" title=\"移除关键字\" href=\"/category/?" + str + "\">移除\"" + key + "\"</a></li>";
            }
            return "";
        }
        public string GetLinks(E_Movie eMovie, string action)
        {

            StringBuilder link = new StringBuilder("");
            if (eMovie.ChannelID != 0)
            {
                link.Append("&channelid=" + eMovie.ChannelID);
            }
            if (!string.IsNullOrEmpty(eMovie.OrderBy))
            {
                if (action != "orderby")
                {
                    link.Append("&orderby=" + eMovie.OrderBy);
                }
            }

            if (eMovie.Year != 0)
            {
                if (action != "year")
                {
                    link.Append("&year=" + eMovie.Year);
                }
            }
            if (!string.IsNullOrEmpty(eMovie.Area))
            {
                if (action != "region")
                {
                    link.Append("&region=" + eMovie.Area);
                }
            }
            if (!string.IsNullOrEmpty(eMovie.Type))
            {
                if (action != "genre")
                {
                    link.Append("&genre=" + eMovie.Type);
                }
            }
            if (!string.IsNullOrEmpty(eMovie.Actor))
            {
                if (action != "key")
                {
                    link.Append("&key=" + eMovie.Actor);
                }
            }
            return link.ToString();

        }
        /// <summary>
        /// 详情页
        /// </summary>
        public void GetInfo()
        {
            Safe.SafePrint();
            int movieId = HttpContext.Current.Request["mid"] == null ? 1 : Convert.ToInt32(HttpContext.Current.Request["mid"]);
            T_Movie tMovie = new T_Movie();
            E_Movie eMovie = new E_Movie();
            eMovie.UserId = UserInfo.Id;
            eMovie.MovieId = movieId;
            eMovie = tMovie.SelectMovie(eMovie);
            Template Tmplate = new Template("web", "info.htm");
            StringBuilder label = new StringBuilder();
            StringBuilder urls = new StringBuilder();
            StringBuilder tvPlay = new StringBuilder();

            bool isLast = true;
            bool firstLinks = true;
            bool isFistTv = true;
            string bofang = "";
            // string[] tabs = new string[] { "m1905.com", "letv.com", "iqiyi.com", "sohu.com", "kankan.com", "wasu.cn", "baofeng.com", "pps.tv", "pptv.com", "56.com", "fun.tv", "cntv.cn", "kumi.cn", "61.com", "hunantv.com", "tudou.com", "youku.com" };
            string[] tabs = tMovie.SelectSiteList(movieId).ToArray();
            for (int i = 0; i < tabs.Length; i++)
            {

                DataRow[] list = tMovie.SelectSourceList(movieId).Select("site='" + tabs[i] + "'", "Episode");


                if (list.Length > 0)
                {
                    string img = tabs[i];
                    string labName = CaseName(img);
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

                        if (isFistTv && eMovie.ChannelID == 2)
                        {
                            tvPlay.Append(" <li><a href='" + codeUrl + "' title='" + list[j]["MovieName"] + "'  target=\"_blank\" >" + list[j]["Episode"] + "</a></li>");
                        }
                        if (firstLinks)
                        {
                            bofang = codeUrl;
                        }
                        firstLinks = false;
                        urls.Append(@"<tr style=''><td>" + list[j]["MovieName"] + "</td><td class='span3' style='width:23%;'><a class='btn btn-primary btn-small' target='_blank' rel='nofollow' href='" + codeUrl + "'><i class='icon-white icon-play'></i>立即播放</a></td></tr>");
                    }
                    isFistTv = false;
              
                    urls.Append("</table></div>");
                }
            }

            #region 是否喜欢
            string likeStr = eMovie.IsLike ? "btn x-tooltip unmark btn-danger" : "mark btn x-tooltip";
            string like = "<a href=\"#\"  class=\" " + likeStr + "\" style=\"text-decoration:none;\"  title=\"喜欢该电影 \" data-toggle=\"tooltip\" data-id=\"" + eMovie.MovieId + "\" data-type=\"like\"><i class=\"icon-heart\" ></i><span>喜欢</span></a>";

            string seenStr = eMovie.IsRead ? "unmark btn btn-success x-tooltip" : "mark btn x-tooltip";
            string seen = "<a href=\"#\" data-container=\"body\" class=\"" + seenStr + "\"title=\"标记为看过的电影\"  data-toggle=\"tooltip\" data-id=\"" + eMovie.MovieId + "\" data-type=\"seen\"><i class=\"icon-check\" ></i> <span>已看</span></a>";


            string planStr = eMovie.IsPlan ? "unmark btn btn-warning x-tooltip" : "mark btn x-tooltip";
            string plan = "<a href=\"#\" data-container=\"body\" class=\"" + planStr + " \"  title=\"计划观看该电影\" data-title=\"取消计划观看该电影\" data-toggle=\"tooltip\" data-id=\"" + eMovie.MovieId + "\" data-type=\"plan\"><i class=\"icon-time\" ></i> <span>计划</span></a>";
            Tmplate.Replace("{Like}", like);
            Tmplate.Replace("{Seen}", seen);
            Tmplate.Replace("{Plan}", plan);
            #endregion
            #region 猜你喜欢
            string random = Tmplate.GetPartContent("<!--<RandomBegin>", "<RandomEnd>-->");
            Tmplate.Replace("{Random}", Tmplate.GetLoopContent(tMovie.GetList(7, "", " newid()").Tables[0], random));
            #endregion
            Tmplate.Replace("{tvStr}", tvPlay.ToString());
            Tmplate.Replace("{MovieId}", eMovie.MovieId.ToString());
            Tmplate.Replace("{IsFinish}", eMovie.IsFinish ? "已完结" : "更新中");
            Tmplate.Replace("{Director}", eMovie.Director.TrimEnd('/'));
            Tmplate.Replace("{Actor}", ALink("/category/?key=", eMovie.Actor));
            Tmplate.Replace("{Type}", ALink("/category/?genre=", eMovie.Type));
            Tmplate.Replace("{Area}", ALink("/category/?region=", eMovie.Area));
            Tmplate.Replace("{ReleaseTime}", eMovie.ReleaseTime);
            Tmplate.Replace("{Length}", eMovie.Length);
            Tmplate.Replace("{AnotherName}", eMovie.AnotherName);
            Tmplate.Replace("{DBScore}", eMovie.DBScore.ToString());
            Tmplate.Replace("{SourceID}", eMovie.SourceID.ToString());
            Tmplate.Replace("{ChannelID}", eMovie.ChannelID.ToString());

            Tmplate.Replace("{ImagePath}", "/dyImg/Img/" + StringTool.SubStr(StringTool.GetString(eMovie.ImagePath), 2) + "/" + eMovie.ImagePath + ".jpg");
            Tmplate.Replace("{Intro}", eMovie.Intro);
            Tmplate.Replace("{FirstURL}", bofang);
            Tmplate.Replace("{MovieName}", eMovie.MovieName);
            Tmplate.Replace("{Labels}", label.ToString());
            Tmplate.Replace("{Links}", urls.ToString());

            Tmplate.Replace("{header}", head.HTML);
            Tmplate.Replace("{footer}", foot.HTML);
            Utils.Write(Tmplate.HTML);
            Tmplate.Dispose();
        }
        /// <summary>
        /// 播放页
        /// </summary>
        public void GetPlay()
        {
            Safe.SafePrint();
            int movieId = HttpContext.Current.Request["mid"] == null ? 1 : Convert.ToInt32(HttpContext.Current.Request["mid"]);
            int MovieId = movieId;
            string url = HttpContext.Current.Request["url"].Replace("/Play.aspx", "");
            string site = CaseName(HttpContext.Current.Request["f"]);
            string playName = HttpContext.Current.Server.UrlDecode(HttpContext.Current.Request["name"]);
            string playUrl = "";
            T_Movie tMovie = new T_Movie();
            T_Source tSource = new T_Source();
            E_Movie eMovie = new E_Movie();
            eMovie.UserId = UserInfo.Id;
            eMovie.MovieId = movieId;

            if (!string.IsNullOrEmpty(url))
            {
                if (movieId > 0)
                {


                    eMovie = tMovie.SelectMovie(eMovie);
                    playUrl = HttpContext.Current.Server.UrlDecode(url);
                }
            }
            else
            {
                if (movieId > 0)
                {


                    eMovie = tMovie.SelectMovie(eMovie);
                    E_Source eSource = tSource.GetModelByMovieId(movieId);
                    site = CaseName(eSource.Site);
                    playName = HttpContext.Current.Server.UrlDecode(eSource.MovieName);
                    playUrl = HttpContext.Current.Server.UrlDecode(eSource.MovieURL);
                }
            }
            Template Tmplate = new Template("web", "play.htm");

            #region 是否喜欢
            string likeStr = eMovie.IsLike ? "btn x-tooltip unmark btn-danger" : "mark btn x-tooltip";
            string like = "<a href=\"#\"  class=\" " + likeStr + "\" style=\"text-decoration:none;\"  title=\"喜欢该电影 \" data-toggle=\"tooltip\" data-id=\"" + eMovie.MovieId + "\" data-type=\"like\"><i class=\"icon-heart\" ></i><span></span></a>";

            string seenStr = eMovie.IsRead ? "unmark btn btn-success x-tooltip" : "mark btn x-tooltip";
            string seen = "<a href=\"#\" data-container=\"body\" class=\"" + seenStr + "\"title=\"标记为看过的电影\"  data-toggle=\"tooltip\" data-id=\"" + eMovie.MovieId + "\" data-type=\"seen\"><i class=\"icon-check\" ></i> <span></span></a>";


            string planStr = eMovie.IsPlan ? "unmark btn btn-warning x-tooltip" : "mark btn x-tooltip";
            string plan = "<a href=\"#\" data-container=\"body\" class=\"" + planStr + " \"  title=\"计划观看该电影\" data-title=\"取消计划观看该电影\" data-toggle=\"tooltip\" data-id=\"" + eMovie.MovieId + "\" data-type=\"plan\"><i class=\"icon-time\" ></i> <span></span></a>";
            Tmplate.Replace("{Like}", like);
            Tmplate.Replace("{Seen}", seen);
            Tmplate.Replace("{Plan}", plan);
            #endregion
            Tmplate.Replace("{MovieId}", eMovie.MovieId.ToString());
            Tmplate.Replace("{Director}", eMovie.Director.TrimEnd('/'));
            Tmplate.Replace("{Actor}", eMovie.Actor);

            Tmplate.Replace("{ImagePath}", "/dyImg/Img/" + StringTool.SubStr(StringTool.GetString(eMovie.ImagePath), 2) + "/" + eMovie.ImagePath + ".jpg");
            Tmplate.Replace("{MovieName}", eMovie.MovieName);
            Tmplate.Replace("{PlayUrl}", playUrl);
            Tmplate.Replace("{PlayName}", playName);
            Tmplate.Replace("{Site}", site);


            Tmplate.Replace("{header}", head.HTML);
            Tmplate.Replace("{footer}", foot.HTML);
            Utils.Write(Tmplate.HTML);
            Tmplate.Dispose();
        }
        /// <summary>
        /// 获取最新
        /// </summary>
        public void GetNew()
        {
          
            T_Movie tMovie = new T_Movie();


            Template Tmplate = new Template("web", "new.htm");

            string MovieTop = Tmplate.GetPartContent("<!--<MovieNewBegin>", "<MovieNewEnd>-->");

            Tmplate.Replace("{MovieNew}", Tmplate.GetLoopContent(tMovie.GetList(50, "", "UpdateTime desc").Tables[0], MovieTop));
            Tmplate.Replace("{header}", head.HTML);
            Tmplate.Replace("{footer}", foot.HTML);
            Utils.Write(Tmplate.HTML);
            Tmplate.Dispose();
        }
        /// <summary>
        /// 推荐
        /// </summary>
        public void TuiJian()
        {
           
            T_Movie tMovie = new T_Movie();
            Template Tmplate = new Template("web", "tuijian.htm");
            string MovieTop = Tmplate.GetPartContent("<!--<MovieTopBegin>", "<MovieTopEnd>-->");
            string TvTop = Tmplate.GetPartContent("<!--<TvTopBegin>", "<TvTopEnd>-->");
            Tmplate.Replace("{MovieTop}", Tmplate.GetLoopContent(tMovie.GetList(7, "ChannelID=1", "UpdateTime desc").Tables[0], MovieTop));
            Tmplate.Replace("{TvTop}", Tmplate.GetLoopContent(tMovie.GetList(7, "ChannelID=2", "UpdateTime desc").Tables[0], TvTop));
            Tmplate.Replace("{header}", head.HTML);
            Tmplate.Replace("{footer}", foot.HTML);
            Utils.Write(Tmplate.HTML);
            Tmplate.Dispose();
        }
        public string CaseName(string str)
        {
            string name = "";
            switch (str)
            {
                case "m1905.com":
                    name = "电影网";
                    break;
                case "fun.tv":
                    name = "风行网";
                    break;
                case "letv.com":
                    name = "乐视";
                    break;
                case "iqiyi.com":
                    name = "爱奇艺";
                    break;
                case "sohu.com":
                    name = "搜狐";
                    break;
                case "kankan.com ":
                    name = "迅雷看看";
                    break;
                case "wasu.cn":
                    name = "华数";
                    break;
                case "pps.tv":
                    name = "PPS";
                    break;
                case "pptv.com":
                    name = "PPTV";
                    break;
                case "baofeng.com":
                    name = "暴风";
                    break;
                case "56.com":
                    name = "56";
                    break;
                case "cntv.cn":
                    name = "央视网";
                    break;
                case "61.com":
                    name = "淘米网";
                    break;
                case "kumi.cn":
                    name = "酷米";
                    break;
                case "hunantv.com":
                    name = "芒果TV";
                    break;
                case "tudou.com":
                    name = "土豆";
                    break;
                case "youku.com":
                    name = "优酷";
                    break;
                default:
                    name = str;
                    break;
            }
            return name;
        }
        /// <summary>
        /// 生成播放地址
        /// </summary>
        /// <param name="url"></param>
        /// <param name="movieId"></param>
        /// <param name="source"></param>
        /// <param name="movieName"></param>
        /// <returns></returns>
        public string GetUrl(int movieId)
        {
            return "/play?mid=" + movieId;
        }
        public string GetLoginInfo(E_User UserInfo)
        {
            StringBuilder loginStr = new StringBuilder();

            if (UserInfo.Id > 0)
            {
                loginStr.Append("<ul class=\"nav pull-right\"><li class=\"x-li-nickname\"><a rel=\"popover\" id=\"msg_ct_popover\" data-toggle=\"popover\" data-popover-show=\"0\" href=\"/User/UserCenter.aspx\" data-msg-ct=\"0\" data-original-title=\"\" title=\"\">");
                loginStr.Append("<i class=\"icon-user icon-white\"></i><font id=\"nav_nickname\">" + UserInfo.NickName + "</font></a></li>");
                loginStr.Append("<li id=\"nav_setting\" class=\"dropdown\"><a href=\"#\" class=\"dropdown-toggle\" data-toggle=\"dropdown\"><span>设置</span><span class=\"caret\"></span></a>");
                loginStr.Append("<ul class=\"dropdown-menu\"><li><a tabindex=\"-1\" href=\"/User/UserCenter.aspx\">个人信息</a></li><li class=\"divider\"></li><li><a tabindex=\"-1\" href=\"/Login.aspx?method=login_out&url=" + HttpContext.Current.Request.Url + "\">注销</a></li></ul></li></ul>");

            }
            else
            {
                loginStr.Append("<ul class=\"nav pull-right\"><li class=\"x-li-nickname\"><a id=\"login\" class=\"login-register-dialog\" style=\"cursor: pointer;\"><i class=\"icon-user icon-white\"></i><font id=\"nav_nickname\">登录/注册 </font></a></li></ul>");
            }

            return loginStr.ToString();
        }
        public string QuChu(string str)
        {

            StringBuilder typeStr = new StringBuilder();
            if (!string.IsNullOrEmpty(str))
            {
                str = str.TrimEnd('/');
                if (str.Contains("/"))
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
        /// <summary>
        /// 详情页拼接链接
        /// </summary>
        /// <param name="url"></param>
        /// <param name="str"></param>
        /// <returns></returns>
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
                newNumberStr.AppendFormat("<a href=\"{0}~page=1\" >首页</a><a href=\"{0}&page={1}\" >上一页</a>", sLinkUrl, proPage);
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

    }
}
