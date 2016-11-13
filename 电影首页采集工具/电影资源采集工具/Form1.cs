using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using Tool;
using System.Text.RegularExpressions;
using DianYing.Model;
using System.Data.SqlClient;
using Maticsoft.DBUtility;
using System.Threading;
using TorrentTool.Tool;
using System.IO;
using System.Configuration;

namespace 电影资源采集工具
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Thread thread = null;
        private void btnStrat_Click(object sender, EventArgs e)
        {
            this.btnStrat.Enabled = false;
            thread = new Thread(new ParameterizedThreadStart(GetList));
            thread.IsBackground = true;
            thread.Start(this.txtPage.Text);


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
        }
        CookieContainer cookie = new CookieContainer();
        string jiyi = "http://dianying.fm/kankan?cmd=next";
        public void GetList(object obj)
        {


            //try
            //{

            this.webBrowser1.Url = new Uri(jiyi);
            this.webBrowser1.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(webBrowser1_DocumentCompleted);



            #region 不用的代码
            // int page = Convert.ToInt32(obj);
            //  List<string> list = new List<string>();

            //    string h = HttpTool.GetHtml("http://dianying.fm/kankan?cmd=next", new CookieContainer());
            //   string str = HttpTool.GetHtml("http://dianying.fm/kankan?cmd=next", cookie);

            //if (!string.IsNullOrEmpty(str))
            //{

            //    string xx = "<a.*?href=\"(.*?)\".*?>";

            //    Regex r = new Regex(xx);
            //    if (r.IsMatch(str))
            //    {
            //        var ec = r.Matches(str);
            //        foreach (Match item in ec)
            //        {
            //            if (item.Groups[1].Value.Contains("one_key_play?"))
            //            {
            //                if (!list.Contains("http://dianying.fm" + item.Groups[1].Value))
            //                {
            //                    list.Add("http://dianying.fm" + item.Groups[1].Value);
            //                }
            //            }

            //        }
            //    }
            //    for (int i = 0; i < list.Count; i++)
            //    {
            //        string bofangUrl = list[i];
            //        string bofangId = bofangUrl.Replace("http://dianying.fm/one_key_play?id=", "");
            //        string bigPath = ConfigurationManager.AppSettings["bigpath"] + "\\" + bofangId.Substring(0, 2) + "\\";
            //        if (Directory.Exists(bigPath) == false)
            //        {
            //            Directory.CreateDirectory(bigPath);
            //        }
            //        //下载大图
            //        HttpTool.DownLoadImg("http://poster.dianying.fm/movie/" + bofangId + "/bdmt/720", bigPath + bofangId + ".jpg");
            //        string info = HttpTool.GetHtml(list[i], new CookieContainer());
            //        E_BigImg model = new E_BigImg();
            //        model.Img = bofangId;

            //        GetPlay(list[i], model);
            //    }
            //}
            //if (this.listLog.Items.Count > 100)
            //{
            //    //清空日记
            //    this.listLog.Items.Clear();

            //}
            // GetList(page);
            #endregion



            //  }
            //catch (Exception e)
            //{
            //    ChongQi();

            //    WriteLog.PrintLn("GetList" + e.Message);
            //}

        }
        string name = "";
        string oldname = "";
        void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            try
            {
              

                var str = this.webBrowser1.Document.GetElementsByTagName("a");
                E_BigImg model = new E_BigImg();
                foreach (HtmlElement item in str)
                {
                  
                    string u = item.GetAttribute("href");
                    if (u.Contains("one_key_play?"))
                    {

                        string bofangUrl = u;
                        string bofangId = bofangUrl.Replace("http://dianying.fm/one_key_play?id=", "");
                        string bigPath = ConfigurationManager.AppSettings["bigpath"] + "\\" + bofangId.Substring(0, 2) + "\\";
                        if (Directory.Exists(bigPath) == false)
                        {
                            Directory.CreateDirectory(bigPath);
                        }
                        //下载大图
                        HttpTool.DownLoadImg("http://poster.dianying.fm/movie/" + bofangId + "/bdmt/720", bigPath + bofangId + ".jpg");
                        string info = HttpTool.GetHtml(u, new CookieContainer());

                        if (!string.IsNullOrEmpty(bofangId))
                        {
                            model.Img = bofangId;
                        }

                      //  GetPlay(u, model);

                    }
                    if (u.Contains("/movie/"))
                    {
                       name=  item.InnerText;
                    }
                
                 
                }
                if (oldname != name && !string.IsNullOrEmpty(name))
                {
                    oldname = name;

                    int mid = GetMovieIdExists(name);
                    string strSql = " select top 1 *  from [dbo].[T_Source] where MovieId=" + mid;

                    using (SqlDataReader reader = DbHelperSQL.ExecuteReader(strSql))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {

                                model.MovieId = mid;
                                model.Source = reader["Site"].ToString();
                                model.SourceUrl = reader["MovieURL"].ToString();
                                model.IsUse = true;
                                if (!ExistsBigImg(mid))
                                {
                                    AddToBigImg(model);
                                    Msg(name + "首页库添加成功");
                                }
                            }

                        }
                    }
                }
                if (this.listLog.Items.Count > 100)
                {
                    //清空日记
                    this.listLog.Items.Clear();

                }
                this.webBrowser1.Url = new Uri("http://dianying.fm/kankan?cmd=next");
                // this.webBrowser1.Document.GetElementById("show_next").InvokeMember("Click");
            }
            catch (Exception ex)
            {

                Msg(ex.Message);
            }

        }

        public int GetMovieIdExists(string MovieName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select MovieId from T_Movie");
            strSql.Append(" where MovieName=@MovieName and ChannelID=1");
            SqlParameter[] parameters = {
					new SqlParameter("@MovieName", MovieName)
			};
            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }


        }
        public void GetPlay(string url, E_BigImg model)
        {
            url = url.Replace("\n", "");
            url = url.Replace("&nbsp;", "");
            string strHtml = HttpTool.GetHtml(url, new CookieContainer());
            string soure = "<a href=\"(.*?)\" rel=\"nofollow\"><span class=\"x-pl-sourcename\">(.*?)</span></a>";
            string soureUrl = GetByReg(strHtml, soure, 1);
            string soureName = GetByReg(strHtml, soure, 2);

            string info = "<a href=\"/movie/(.*?)\">(.*?)</a>";
            string infoUrl = GetByReg(strHtml, info, 1);

            model.Source = soureName;
            model.SourceUrl = soureUrl;
            if (!string.IsNullOrEmpty(infoUrl))
            {
                GetInfo("http://dianying.fm/movie/" + infoUrl, model);
            }

        }
        public void GetInfo(string url, E_BigImg model)
        {
            //try
            //{


            string strHtml = HttpTool.GetHtml(url, new CookieContainer());
            string dianYingName = "";
            if (!string.IsNullOrEmpty(strHtml))
            {
                string title = "<title> 《(.*?)》(.*?)</title>";

                Regex r = new Regex(title);
                if (r.IsMatch(strHtml))
                {
                    Match m = r.Match(strHtml);
                    dianYingName = m.Groups[1].Value;
                }
                string imgPath = GetImgPath(strHtml);

                strHtml = strHtml.Replace("\n", "");
                strHtml = strHtml.Replace("&nbsp;", "");

                string RegTable = "<tr><td class=\"span2\"><span class=\"x-m-label\">导演</span></td><!-- <td>(.*?)</td>--><td>(.*?)</td></tr>";
                string daoyan = GetByReg(strHtml, RegTable, 1);
                string zhuYanReg = "<tr><td class=\"span2\"><span class=\"x-m-label\">主演</span></td><!-- <td>(.*?)</td>--><td>(.*?)</td></tr>";
                string zhuyan = GetByReg(strHtml, zhuYanReg, 1);
                string leiXingReg = "<tr><td class=\"span2\"><span class=\"x-m-label\">类型</span></td><!-- <td>(.*?)</td>--><td>(.*?)</td></tr>";
                string leixing = GetByReg(strHtml, leiXingReg, 1);
                string diquReg = "<tr><td class=\"span2\"><span class=\"x-m-label\">地区</span></td><!-- <td>(.*?)</td>--><td>(.*?)</td></tr>";
                string diqu = GetByReg(strHtml, diquReg, 1);
                string shangyinReg = "<tr><td class=\"span2\"><span class=\"x-m-label\">上映时间</span></td><!-- <td>(.*?)</td>--><td>(.*?)</td></tr>";
                string shangyin = GetByReg(strHtml, shangyinReg, 1);

                string pianChangReg = "<tr><td class=\"span2\"><span class=\"x-m-label\">片长</span></td><!-- <td>(.*?)</td>--><td>(.*?)</td></tr>";
                string pianChang = GetByReg(strHtml, pianChangReg, 1);
                string bieMingReg = "<tr><td class=\"span2\"><span class=\"x-m-label\">别名</span></td><!-- <td>(.*?)</td>--><td>(.*?)</td></tr>";
                string bieMing = GetByReg(strHtml, bieMingReg, 1);
                string pingFenReg = "<tr class=\"x-m-rating\"><td class=\"span2\"><span class=\"x-m-label\">评分</span></td><td>(.*?)</td></tr>";
                string pingFen = GetByReg(strHtml, pingFenReg, 1);

                string dbReg = "豆瓣：<a .*? href=\"(.*?)\" .*?><span .*?>(.*?)</span></a>";
                string dbPingFen = GetByReg(pingFen, dbReg, 2);
                string dbUrl = GetByReg(pingFen, dbReg, 1);

                string IMDBReg = "IMDB：<a .*? href=\"(.*?)\" .*?><span .*?>(.*?)</span></a>";
                string IMDBPingFen = GetByReg(pingFen, IMDBReg, 2);
                string IMDBUrl = GetByReg(pingFen, IMDBReg, 1);

                string introReg = "<div class=\"x-m-summary\"><p>(.*?)</p></div>";
                string intro = GetByReg(strHtml, introReg, 1);

                string sPath = ConfigurationManager.AppSettings["path"] + "\\" + imgPath.Substring(0, 2) + "\\";
                string msPath = ConfigurationManager.AppSettings["mpath"] + "\\" + imgPath.Substring(0, 2) + "\\";
                if (Directory.Exists(sPath) == false)
                {
                    Directory.CreateDirectory(sPath);
                }
                if (Directory.Exists(msPath) == false)
                {
                    Directory.CreateDirectory(msPath);
                }
                HttpTool.DownLoadImg("http://dianying.fm/poster/l/" + imgPath, sPath + imgPath + ".jpg");
                HttpTool.DownLoadImg("http://dianying.fm/poster/m/" + imgPath, msPath + imgPath + ".jpg");
                T_Movie movie = new T_Movie();
                movie.Actor = zhuyan;
                movie.AnotherName = bieMing;
                movie.Area = diqu;
                movie.DBScore = dbPingFen == "" ? 0 : Convert.ToDecimal(dbPingFen);
                movie.DBUrl = dbUrl;
                movie.Director = daoyan;
                movie.Hit = 1;
                movie.ImagePath = imgPath;
                movie.IMDBScore = IMDBPingFen == "" ? 0 : Convert.ToDecimal(IMDBPingFen);
                movie.IMDBUrl = IMDBUrl;
                movie.Intro = intro;
                movie.Length = pianChang;
                movie.MovieName = dianYingName;
                movie.Type = leixing;

                movie.Year = StrSub(shangyin, 5) == "" ? 0 : Convert.ToInt32(StrSub(shangyin, 5));

                movie.ReleaseTime = shangyin;
                movie.UpdateTime = DateTime.Now;
                movie.ChannelID = 1;
                if (!Exists(dianYingName))
                {
                    int movieId = Add(movie);//获取插入后的ID
                    Msg(dianYingName + "入库成功");

                    Getlink(strHtml, movieId);
                    GetHashLinks(strHtml, movieId);
                    model.MovieId = movieId;
                    AddToBigImg(model);
                }
                else
                {
                    int movieId = GetMovieId(dianYingName);
                    if (movieId > 0)
                    {
                        if (!ExistsBigImg(movieId))
                        {
                            model.MovieId = movieId;
                            AddToBigImg(model);
                            Msg(dianYingName + "首页库添加成功");
                        }
                    }
                }
            }
            else
            {
                WriteLog.PrintLn(url + "访问失败");
            }
            //}
            //catch (Exception e)
            //{

            //    WriteLog.PrintLn("GetInfo" + e.Message);
            //}
        }
        /// <summary>
        /// 获取海报
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string GetImgPath(string str)
        {
            string reg = "<img alt=\"(.*?)\" src=\"/poster/l/(.*?)\">";
            string result = "";
            Regex r = new Regex(reg);
            if (r.IsMatch(str))
            {
                Match m = r.Match(str);
                result = m.Groups[2].Value;
            }
            return result;
        }

        public string GetByReg(string html, string reg, int i)
        {

            string result = "";
            Regex r = new Regex(reg);
            if (r.IsMatch(html))
            {
                Match m = r.Match(html);
                result = m.Groups[i].Value;
            }
            return result;
        }

        public void Getlink(string html, int movieId)
        {
            html = html.Replace("display:none;", "");
            string reg = "<tr style=\"\">(.*?)</tr>";
            Regex r = new Regex(reg);
            if (r.IsMatch(html))
            {
                var ec = r.Matches(html);
                foreach (Match item in ec)
                {
                    string tr = item.Groups[1].Value;
                    GetLinkInfo(tr, movieId);

                }
            }
        }
        public void GetLinkInfo(string str, int movieId)
        {
            string name = "";
            string url = "";
            string reg = "<td>(.*?)<span class=\"muted\"> (.*?) </span></td>";
            Regex r = new Regex(reg);
            if (r.IsMatch(str))
            {
                Match m = r.Match(str);
                name = m.Groups[1].Value;
            }
            string xx = "<a.*?href=\"(.*?)\".*?>";

            Regex r1 = new Regex(xx);
            if (r1.IsMatch(str))
            {
                var ec = r1.Matches(str);
                foreach (Match item in ec)
                {
                    if (item.Groups[1].Value.Contains("playlink?"))
                    {
                        url = item.Groups[1].Value;
                    }

                }
            }
            url = url.Replace("/playlink?url=", "");
            T_M eM = new T_M();
            eM.MovieId = movieId;
            eM.Name = name;
            eM.URL = Sub(url);
            if (url.Contains("m1905.com"))
            {
                AddToDb(eM, "T_M");
            }
            else if (url.Contains("letv.com"))
            {
                AddToDb(eM, "T_LE");
            }
            else if (url.Contains("iqiyi.com"))
            {
                AddToDb(eM, "T_QiYi");
            }
            else if (url.Contains("sohu.com"))
            {
                AddToDb(eM, "T_SouHu");
            }
            else if (url.Contains("kankan.com"))
            {
                AddToDb(eM, "T_XunLeiKanKan");
            }
            else if (url.Contains("wasu.cn"))
            {

                AddToDb(eM, "T_WaSu");
            }
            else if (url.Contains("qq.com"))
            {
                AddToDb(eM, "T_TenXun");
            }
            else if (url.Contains("pps.tv"))
            {
                AddToDb(eM, "T_PPS");
            }
            else if (url.Contains("tudou.com"))
            {
                AddToDb(eM, "T_TuDou");
            }
            else if (url.Contains("yugaopian.com"))
            {
                AddToDb(eM, "T_YuGaoPian");
            }
            else if (url.Contains("funshion.com"))
            {
                AddToDb(eM, "T_FunShion");
            }
            else if (url.Contains("youku.com"))
            {
                AddToDb(eM, "T_YouKu");
            }
            else if (url.Contains("pptv.com"))
            {
                AddToDb(eM, "T_PPTV");
            }
            else if (url.Contains("56.com"))
            {
                AddToDb(eM, "T_56");
            }
            else if (url.Contains("baofeng.com"))
            {
                AddToDb(eM, "T_BaoFeng");
            }
            else if (url.Contains("baidu.com"))
            {
                AddToDb(eM, "T_BaiDu");
            }
            else
            {
                AddToDb(eM, "T_QiTa");
            }
        }

        public void GetHashLinks(string html, int movieId)
        {
            html = html.Replace("display:none;", "");
            string reg = "<tr class=\"resources\" style=\"\">(.*?)</tr>";
            Regex r = new Regex(reg);
            if (r.IsMatch(html))
            {
                var ec = r.Matches(html);
                foreach (Match item in ec)
                {
                    string tr = item.Groups[1].Value;
                    GethashLinkInfo(tr, movieId);
                }
            }
        }
        public void GethashLinkInfo(string str, int movieId)
        {
            string name = "";
            string url = "";
            string reg = "<td style=\"word-break: break-all;\">(.*?)</td>";
            Regex r = new Regex(reg);
            if (r.IsMatch(str))
            {
                Match m = r.Match(str);
                name = m.Groups[1].Value;
            }
            string xx = "<a.*?href=\"(.*?)\".*?>";

            Regex r1 = new Regex(xx);
            if (r1.IsMatch(str))
            {
                var ec = r1.Matches(str);
                foreach (Match item in ec)
                {
                    if (item.Groups[1].Value.Contains("magnet:?"))
                    {
                        url = item.Groups[1].Value;

                    }

                }
            }
            url = url.Replace("/playlink?url=", "");
            T_M eM = new T_M();
            eM.MovieId = movieId;
            eM.Name = name;
            eM.URL = Sub(url);
            int o = AddToDb(eM, "T_Hash");
            if (o == 0)
            {

            }
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string MovieName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from T_Movie");
            strSql.Append(" where MovieName=@MovieName");
            SqlParameter[] parameters = {
					new SqlParameter("@MovieName", MovieName)
			};


            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool ExistsBigImg(int MovieId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from T_BigImg");
            strSql.Append(" where MovieId=@MovieId");
            SqlParameter[] parameters = {
					new SqlParameter("@MovieId", MovieId)
			};


            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public int GetMovieId(string MovieName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select MovieId from T_Movie");
            strSql.Append(" where MovieName=@MovieName");
            SqlParameter[] parameters = {
					new SqlParameter("@MovieName", MovieName)
			};


            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(DianYing.Model.T_Movie model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_Movie(");
            strSql.Append("MovieName,Director,Actor,Type,Area,Length,AnotherName,DBScore,IMDBScore,ImagePath,DBUrl,IMDBUrl,Intro,Hit,ReleaseTime,Year,UpdateTime,ChannelID)");
            strSql.Append(" values (");
            strSql.Append("@MovieName,@Director,@Actor,@Type,@Area,@Length,@AnotherName,@DBScore,@IMDBScore,@ImagePath,@DBUrl,@IMDBUrl,@Intro,@Hit,@ReleaseTime,@Year,@UpdateTime,@ChannelID)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@MovieName", SqlDbType.NVarChar,150),
					new SqlParameter("@Director", SqlDbType.NVarChar,50),
					new SqlParameter("@Actor", SqlDbType.NVarChar,150),
					new SqlParameter("@Type", SqlDbType.NVarChar,50),
					new SqlParameter("@Area", SqlDbType.NVarChar,50),
					new SqlParameter("@Length", SqlDbType.NVarChar,50),
					new SqlParameter("@AnotherName", SqlDbType.NVarChar,150),
					new SqlParameter("@DBScore", SqlDbType.Decimal,9),
					new SqlParameter("@IMDBScore", SqlDbType.Float,8),
					new SqlParameter("@ImagePath", SqlDbType.NVarChar,150),
					new SqlParameter("@DBUrl", SqlDbType.NVarChar,150),
					new SqlParameter("@IMDBUrl", SqlDbType.NVarChar,150),
					new SqlParameter("@Intro", SqlDbType.NVarChar,-1),
					new SqlParameter("@Hit", SqlDbType.Int,4),
					new SqlParameter("@ReleaseTime", SqlDbType.NVarChar,50),
					new SqlParameter("@Year", SqlDbType.Int,4),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@ChannelID", SqlDbType.Int,4)};
            parameters[0].Value = model.MovieName;
            parameters[1].Value = model.Director;
            parameters[2].Value = model.Actor;
            parameters[3].Value = model.Type;
            parameters[4].Value = model.Area;
            parameters[5].Value = model.Length;
            parameters[6].Value = model.AnotherName;
            parameters[7].Value = model.DBScore;
            parameters[8].Value = model.IMDBScore;
            parameters[9].Value = model.ImagePath;
            parameters[10].Value = model.DBUrl;
            parameters[11].Value = model.IMDBUrl;
            parameters[12].Value = model.Intro;
            parameters[13].Value = model.Hit;
            parameters[14].Value = model.ReleaseTime;
            parameters[15].Value = model.Year;
            parameters[16].Value = model.UpdateTime;
            parameters[17].Value = model.ChannelID;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int AddToDb(T_M model, string dbName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into " + dbName + "(");
            strSql.Append("MovieId,Name,URL)");
            strSql.Append(" values (");
            strSql.Append("@MovieId,@Name,@URL)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@MovieId", SqlDbType.Int,4),
					new SqlParameter("@Name", SqlDbType.NVarChar,250),
					new SqlParameter("@URL", SqlDbType.NVarChar,350)};
            parameters[0].Value = model.MovieId;
            parameters[1].Value = model.Name;
            parameters[2].Value = model.URL;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int AddToBigImg(E_BigImg model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_BigImg(");
            strSql.Append("MovieId,Img,Source,SourceUrl,IsUse)");
            strSql.Append(" values (");
            strSql.Append("@MovieId,@Img,@Source,@SourceUrl,@IsUse)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@MovieId", SqlDbType.Int,4),
					new SqlParameter("@Img", SqlDbType.NVarChar,150),
                    	new SqlParameter("@Source", SqlDbType.NVarChar,50),
                        	new SqlParameter("@SourceUrl", SqlDbType.NVarChar,250),
					new SqlParameter("@IsUse", SqlDbType.Bit,1)};
            parameters[0].Value = model.MovieId;
            parameters[1].Value = model.Img;
            parameters[2].Value = model.Source;
            parameters[3].Value = model.SourceUrl;
            parameters[4].Value = true;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_M ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
			};
            parameters[0].Value = Id;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 日记
        /// </summary>
        /// <param name="str"></param>
        public void Msg(string str)
        {
            this.listLog.Items.Add(DateTime.Now.ToString() + ":" + str);
        }
        public string StrSub(string str, int i)
        {
            if (string.IsNullOrEmpty(str))
            {
                return "";
            }
            else
            {
                if (str.Length > i)
                {
                    return str.Substring(0, i);
                }
                else
                {
                    return str;
                }
            }
        }
        public static string Sub(string str)
        {
            string s = "";

            if (!string.IsNullOrEmpty(str))
            {
                if (str.Contains("&id="))
                {
                    s = str.Substring(0, str.IndexOf("&id="));
                }
                else
                {
                    s = str;
                }
            }
            return s;
        }

        public void ChongQi()
        {
            try
            {



                System.Timers.Timer t = new System.Timers.Timer(Convert.ToInt32(this.textBox1.Text) * 1000);   //实例化Timer类，设置间隔时间为10000毫秒；   
                t.Elapsed += new System.Timers.ElapsedEventHandler(btnStrat_Click); //到达时间的时候执行事件；   
                t.AutoReset = false;   //设置是执行一次（false）还是一直执行(true)；   
                t.Enabled = true;     //是否执行System.Timers.Timer.Elapsed事件；   

                Msg("3600后重启");
            }
            catch (Exception e)
            {

                Msg(e.Message);
            }
        }
    }
}
