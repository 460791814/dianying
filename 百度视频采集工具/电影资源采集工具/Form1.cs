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
using System.Web;
using Maticsoft.Model;
using DianYing.Web.Tool;
using System.Web.Script.Serialization;

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
            thread = new Thread(new ParameterizedThreadStart(XUNHUAN));
            thread.IsBackground = true;
            thread.Start(this.txtPage.Text);


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            this.Cb_Channel.Items.AddRange(Data.Channel);
            this.Cb_Channel.SelectedIndex = 0;

            this.CB_Type.Items.AddRange(Data.dyYypes);
            this.CB_Type.SelectedIndex = 0;

            this.CB_Area.Items.AddRange(Data.areas);
            this.CB_Area.SelectedIndex = 0;

            this.CB_Year.Items.AddRange(Data.years);
            this.CB_Year.SelectedIndex = 0;
           
        }
        int jilu = 0;
        public void XUNHUAN(object obj)
        {
            for (int i = 0; i < Data.dyYypes.Length; i++)
            {
                WriteLog.PrintLn("电影类型：" + Data.dyYypes[i]);
                Msg("电影类型：" + Data.dyYypes[i]);
                for (int j = 0; j < Data.areas.Length; j++)
                {
                  
                    for (int k = 0; k < Data.years.Length; k++)
                    {
                        jilu = 0;
                        GetList(Data.dyYypes[i], Data.areas[j],Data.years[k],1);
                    }
                 
                }
            }
        
            Msg("电影采集完毕");
            this.btnStrat.Enabled = true;
        }
        public void GetList(string t, string a,string y, int page)
        {
            try
            {

                List<string> list = new List<string>();
                //%E6%AD%A3%E7%89%87
                string str = HttpTool.GetHtml("http://v.baidu.com/commonapi/movie2level/?callback=jQuery19107936704258900136_1401507550406&filter=true&type=" + t + "&area=" + a + "&actor=&start=" + y + "&complete=%E6%AD%A3%E7%89%87&order=pubtime&pn=" + page + "&rating=&prop=&_=1401507550407", new CookieContainer());

                if (!string.IsNullOrEmpty(str))
                {
                    page++;
                    this.txtPage.Text = page.ToString();
                    string xx = "\"id\":\"(.*?)\",\"url\"";

                    Regex r = new Regex(xx);
                    if (r.IsMatch(str))
                    {
                        var ec = r.Matches(str);
                        foreach (Match item in ec)
                        {

                            if (!list.Contains(item.Groups[1].Value))
                            {
                                list.Add(item.Groups[1].Value);
                            }


                        }
                    }
                    else
                    {
                        jilu++;
                    }

                    for (int i = 0; i < list.Count; i++)
                    {
                        GetInfo(list[i]);
                    }

                }

                if (this.listLog.Items.Count > 100)
                {
                    //清空日记
                    this.listLog.Items.Clear();

                }

                WriteLog.PrintLn("page:" + page);
                Msg("开始抓取page" + page);
                if (jilu < 6)
                {
                    if (page <= Convert.ToInt32(this.txtEndPage.Text))
                    {
                        GetList(t, a,y, page);
                    }
                }

            }
            catch (Exception e)
            {
                //   ChongQi();

                //  WriteLog.PrintLn("GetList" + e.Message);
            }

        }

        public void GetInfo(string bdMovieId)
        {
            try
            {

                string url = "http://v.baidu.com/movie/" + bdMovieId + ".htm?frp=browse";


                string strHtml = HttpTool.GetHtml(url, new CookieContainer());
                string dianYingName = "";

                if (!string.IsNullOrEmpty(strHtml))
                {
                    string title = "<title>(.*?)-电影-百度视频</title>";

                    Regex r = new Regex(title);
                    if (r.IsMatch(strHtml))
                    {
                        Match m = r.Match(strHtml);
                        dianYingName = m.Groups[1].Value;
                    }


                    strHtml = strHtml.Replace("\n", "");
                    // strHtml = strHtml.Replace("&nbsp;", "");
                    string imgPath = GetImgPath(strHtml);
                    if (imgPath == "")
                    {
                        string regIMG = "<imgsrc=\"(.*?)\"width=\"225\"height=\"300\"alt=\"(.*?)\"/>";

                        Regex regI = new Regex(regIMG);
                        if (regI.IsMatch(strHtml))
                        {
                            Match m = regI.Match(strHtml);
                            imgPath = m.Groups[1].Value;
                        }
                    }

                    string introReg = "<meta name=\"description\" content=\"(.*?)\" />";
                    string intro = GetByReg(strHtml, introReg, 1);
                    string divInfoReg = "<div class=\"desc-wrapper\">(.*?)</div>";
                    string divinfo = GetByReg(strHtml, divInfoReg, 1);
                    strHtml = divinfo;
                    strHtml = strHtml.Replace("\t", "");
                    strHtml = strHtml.Replace(" ", "");
                    string RegTable = "<li><spanclass=\"desc-title\">导演：</span>(.*?)</li>";
                    string daoyan = GetByReg(strHtml, RegTable, 1).Replace("&nbsp;", "/");
                    string zhuYanReg = "<li><spanclass=\"desc-title\">演员：</span>(.*?)</li>";
                    string zhuyan = GetByReg(strHtml, zhuYanReg, 1).Replace("&nbsp;", "/");
                    if (string.IsNullOrEmpty(zhuyan))
                    {
                        string zhuYanReg1 = "<li><spanclass=\"desc-title\">主演：</span>(.*?)</li>";
                        zhuyan = GetByReg(strHtml, zhuYanReg1, 1).Replace("&nbsp;", "/");
                    }
                    string leiXingReg = "<ulclass=\"aside-highlight\"><liclass=\"desc-title\">看点：</li>(.*?)</ul>";
                    string leixing = GetByReg(strHtml, leiXingReg, 1).Replace("</li><li>", "/").Replace("</li>", "").Replace("<li>", "");
                    string diquReg = "<li><spanclass=\"desc-title\">地区：</span>(.*?)<spanclass=\"desc-titlepl30\">年代：</span>(.*?)<spanclass=\"desc-titlepl30\">片长：</span>(.*?)</li>";
                    string diqu = GetByReg(strHtml, diquReg, 1).Replace("&nbsp;", "");

                    string shangyin = GetByReg(strHtml, diquReg, 2).Replace("&nbsp;", "");


                    string pianChang = GetByReg(strHtml, diquReg, 3).Replace("&nbsp;", "");
                    string bieMingReg = "<spanclass=\"desc-title\">别名：</span><aid=\"bieming\">(.*?)</a>";
                    string bieMing = GetByReg(strHtml, bieMingReg, 1);



                    string dbReg = "<spanclass=\"desc-titlepl30\">豆瓣评分：</span><spanclass=\"rating-score\">(.*?)</span>";
                    string dbPingFen = GetByReg(strHtml, dbReg, 1).Replace("&nbsp;", "");
                    //  string dbUrl = GetByReg(dbReg, dbReg, 1);



                    string imgName = Sub(imgPath);

                    string sPath = ConfigurationManager.AppSettings["path"] + "\\" + StringTool.SubStr(imgName, 2) + "\\";

                    if (Directory.Exists(sPath) == false)
                    {
                        Directory.CreateDirectory(sPath);
                    }

                    HttpTool.DownLoadImg(imgPath, sPath + imgName + ".jpg");

                    T_Movie movie = new T_Movie();
                    movie.Actor = zhuyan;
                    movie.AnotherName = bieMing;
                    movie.Area = diqu;
                    movie.DBScore = dbPingFen == "" ? 0 : Convert.ToDecimal(dbPingFen);
                    movie.DBUrl = "";
                    movie.Director = daoyan;
                    movie.Hit = 1;
                    movie.ImagePath = imgName;
                    movie.IMDBScore = 0; //IMDBPingFen == "" ? 0 : Convert.ToDecimal(IMDBPingFen);
                    movie.IMDBUrl = "";// IMDBUrl;
                    movie.Intro = intro;
                    movie.Length = pianChang;
                    movie.MovieName = dianYingName;
                    movie.Type = leixing;

                    movie.Year = shangyin == "" ? 0 : Convert.ToInt32(shangyin);

                    movie.ReleaseTime = shangyin;
                    movie.UpdateTime = DateTime.Now;
                    movie.ChannelID = 1;

                    movie.IsFinish = true;

                    movie.SourceID = bdMovieId;
                    if (!string.IsNullOrEmpty(dianYingName))
                    {
                        int mid = GetMovieIdExists(dianYingName,movie.Year.ToString());
                        if (mid > 0)
                        {

                            Getlink("http://v.baidu.com/movie_intro/?dtype=playUrl&service=json&id=" + bdMovieId , mid, dianYingName);
                            Msg(dianYingName + "录入播放库成功");
                        }
                        else
                        {
                            int movieId = Add(movie);//获取插入后的ID
                            Msg(dianYingName + "入库成功");
                            Getlink("http://v.baidu.com/movie_intro/?dtype=playUrl&service=json&id=" + bdMovieId , movieId, dianYingName);
                            Msg(dianYingName + "录入播放库成功");
                        }
                    }
                }
                else
                {
                    //  WriteLog.PrintLn(url + "访问失败");
                }
            }
            catch (Exception e)
            {

                WriteLog.PrintLn("movieid:" + bdMovieId + "-访问失败");
            }
        }




        /// <summary>
        /// 获取海报
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string GetImgPath(string str)
        {
            string reg = "<div class=\"poster-sec\">(.*?)</div>";
            string result = "";
            string xx = "";
            Regex r = new Regex(reg);
            if (r.IsMatch(str))
            {
                Match m = r.Match(str);
                xx = m.Groups[1].Value;
            }
            xx = xx.Replace("\t", "");
            xx = xx.Replace(" ", "");
            string reg1 = "<imgsrc=\"(.*?)\"width=\"225\"height=\"300\"alt=\"(.*?)\">";

            Regex r1 = new Regex(reg1);
            if (r1.IsMatch(xx))
            {
                Match m = r1.Match(xx);
                result = m.Groups[1].Value;
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

        public void Getlink(string html, int movieId, string name)
        {
            string strHtml = HttpTool.GetHtml(html, new CookieContainer());


            List<M> list = new List<M>();

            System.Runtime.Serialization.Json.DataContractJsonSerializer json = new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(List<M>));
            using (MemoryStream stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(strHtml)))
            {
                list = (List<M>)json.ReadObject(stream);

            }
            if (list != null && list.Count > 0)
            {
                bool movieUpdate = false;
                for (int i = 0; i < list.Count; i++)
                {
                    //GetLinkInfo(list[i].link, movieId,name);
                    T_Source es = new T_Source();
                    es.MovieId = movieId;
                    es.MovieName = name;
                    es.MovieURL = list[i].link;
                    es.Site = list[i].site;
                    es.Episode = 1;
                    if (!ExistsUrl(es))
                    {
                        movieUpdate = true;
                        AddToSource(es);
                        Msg(name + ":新加(" + es.MovieName + ")");
                    }
                }
                if (movieUpdate) {
                    string sql = "update T_Movie set UpdateTime=@UpdateTime where MovieId=" + movieId;
                    DbHelperSQL.GetSingle(sql, new SqlParameter("@UpdateTime", DateTime.Now));
                }
                string sql1 = "update T_Movie set IsFinish=1 where MovieId=" + movieId;
                DbHelperSQL.GetSingle(sql1);
            }
            else
            {
                string sql = "update T_Movie set IsFinish=0 where MovieId=" + movieId;
                DbHelperSQL.GetSingle(sql);
            }
        }
        public class M
        {
            public string link;
            public string site;
            public string name;
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
        public int GetMovieIdExists(string MovieName, string Year)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select MovieId from T_Movie");
            strSql.Append(" where MovieName=@MovieName and Year=@Year and ChannelID=1");
            SqlParameter[] parameters = {
					new SqlParameter("@MovieName", MovieName),
                    new SqlParameter("@Year", Year)
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
        public int GetMovieIdExists2(string MovieName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select MovieId from T_Movie");
            strSql.Append(" where MovieName=@MovieName and ChannelID=2");
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
        /// 是否存在该记录
        /// </summary>
        public bool ExistsUrl(T_Source model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from T_Source ");
            strSql.Append(" where MovieURL=@MovieURL and MovieId=@MovieId");
            SqlParameter[] parameters = {
					new SqlParameter("@MovieURL", model.MovieURL),
                    	new SqlParameter("@MovieId", model.MovieId)
			};


            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(T_Movie model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_Movie(");
            strSql.Append("MovieName,Director,Actor,Type,Area,Length,AnotherName,DBScore,IMDBScore,ImagePath,DBUrl,IMDBUrl,Intro,Hit,ReleaseTime,Year,UpdateTime,ChannelID,SourceID,IsFinish)");
            strSql.Append(" values (");
            strSql.Append("@MovieName,@Director,@Actor,@Type,@Area,@Length,@AnotherName,@DBScore,@IMDBScore,@ImagePath,@DBUrl,@IMDBUrl,@Intro,@Hit,@ReleaseTime,@Year,@UpdateTime,@ChannelID,@SourceID,@IsFinish)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@MovieName", SqlDbType.NVarChar,150),
					new SqlParameter("@Director", SqlDbType.NVarChar,50),
					new SqlParameter("@Actor", SqlDbType.NVarChar,150),
					new SqlParameter("@Type", SqlDbType.NVarChar,50),
					new SqlParameter("@Area", SqlDbType.NVarChar,50),
					new SqlParameter("@Length", SqlDbType.NVarChar,50),
					new SqlParameter("@AnotherName", SqlDbType.NVarChar,150),
					new SqlParameter("@DBScore", SqlDbType.Float,8),
					new SqlParameter("@IMDBScore", SqlDbType.Float,8),
					new SqlParameter("@ImagePath", SqlDbType.NVarChar,150),
					new SqlParameter("@DBUrl", SqlDbType.NVarChar,150),
					new SqlParameter("@IMDBUrl", SqlDbType.NVarChar,150),
					new SqlParameter("@Intro", SqlDbType.NVarChar,-1),
					new SqlParameter("@Hit", SqlDbType.Int,4),
					new SqlParameter("@ReleaseTime", SqlDbType.NVarChar,50),
					new SqlParameter("@Year", SqlDbType.Int,4),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@ChannelID", SqlDbType.Int,4),
					new SqlParameter("@SourceID", SqlDbType.NVarChar,50),
					new SqlParameter("@IsFinish", SqlDbType.Bit,1)};
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
            parameters[18].Value = model.SourceID;
            parameters[19].Value = model.IsFinish;

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
        /// 删除一条数据
        /// </summary>
        public bool Delete(T_M model, string dbName)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from  " + dbName);
            strSql.Append(" where MovieId=@MovieId");
            SqlParameter[] parameters = {
					new SqlParameter("@MovieId", SqlDbType.Int,4)
			};
            parameters[0].Value = model.MovieId;

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
                if (str.Contains("u="))
                {
                    s = str.Substring(str.IndexOf("u=")).Replace("u=", "").Replace("&fm=20", "");
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


                // this.txtPage.Text = jilu.ToString();
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
        #region 电视剧采集
        public void GetTVList(string t, string a,string y, int page)
        {
            //try
            //{
          
            List<string> list = new List<string>();
            string str = HttpTool.GetHtml("http://v.baidu.com/commonapi/tvplay2level/?callback=jQuery1910954512323718518_1401597014307&filter=false&type=" + t + "&area=" + a + "&actor=&start="+y+"&complete=&order=pubtime&pn=" + page + "&rating=&prop=&_=1401597014311", new CookieContainer());

            if (!string.IsNullOrEmpty(str))
            {
                page++;
                this.txtPage.Text = page.ToString();
                string xx = "\"id\":\"(.*?)\",\"site\"";

                Regex r = new Regex(xx);
                if (r.IsMatch(str))
                {
                    var ec = r.Matches(str);
                    foreach (Match item in ec)
                    {

                        if (!list.Contains(item.Groups[1].Value))
                        {
                            list.Add(item.Groups[1].Value);
                        }


                    }
                }
                else
                {
                    jilu++;
                }

                for (int i = 0; i < list.Count; i++)
                {
                    GetTVInfo(list[i]);
                }

            }

            if (this.listLog.Items.Count > 100)
            {
                //清空日记
                this.listLog.Items.Clear();

            }

            //  WriteLog.PrintLn("page:" + page);
            Msg("开始抓取page" + page);
            if (jilu < 6)
            {
                if (page <= Convert.ToInt32(this.txtEndPage.Text))
                {
                    GetTVList(t, a,y, page);
                }
            }

            //}
            //catch (Exception e)
            //{
            //    ChongQi();

            //    WriteLog.PrintLn("GetList" + e.Message);
            //}

        }

        public void GetTVInfo(string bdMovieId)
        {
            try
            {

                string url = "http://v.baidu.com/tv/" + bdMovieId + ".htm?frp=browse";


                string strHtml = HttpTool.GetHtml(url, new CookieContainer());
                string dianYingName = "";
                string dyStatus = "";
                if (!string.IsNullOrEmpty(strHtml))
                {
                    string title = "<title>(.*?)（(.*?)）-电视剧-百度视频</title>";

                    Regex r = new Regex(title);
                    if (r.IsMatch(strHtml))
                    {
                        Match m = r.Match(strHtml);
                        dianYingName = m.Groups[1].Value;
                        dyStatus = m.Groups[2].Value;
                    }


                    strHtml = strHtml.Replace("\n", "");
                    // strHtml = strHtml.Replace("&nbsp;", "");
                    string imgPath = GetImgPath(strHtml);
                    string aReg = "<a.*?>(.*?)</a>";

                    string introReg = "<meta name=\"description\" content=\"(.*?)\" />";
                    string intro = GetByReg(strHtml, introReg, 1);
                    string divInfoReg = "<div class=\"desc-wrapper\">(.*?)</div>";
                    string divinfo = GetByReg(strHtml, divInfoReg, 1);
                    strHtml = divinfo;
                    strHtml = strHtml.Replace("\t", "");
                    strHtml = strHtml.Replace(" ", "");
                    string RegTable = "<li><spanclass=\"desc-title\">导演：</span>(.*?)</li>";

                    string daoyan = GetByReg(strHtml, RegTable, 1).Replace("&nbsp;", "/");
                    daoyan = GetByReg(daoyan, aReg, 1);

                    string zhuYanReg = "<li><spanclass=\"desc-title\">主演：</span>(.*?)</li>";
                    string zhuyan = GetByReg(strHtml, zhuYanReg, 1).Replace("&nbsp;", "/");
                    string zhuyan_r = ""; ;
                    Regex zy = new Regex(aReg);
                    if (zy.IsMatch(zhuyan))
                    {
                        var ec_zy = zy.Matches(zhuyan);
                        foreach (Match item in ec_zy)
                        {

                            zhuyan_r = zhuyan_r + item.Groups[1].Value + "/";


                        }
                    }

                    string leiXingReg = "<ulclass=\"desc-highlight\">(.*?)</ul>";
                    string leixing = GetByReg(strHtml, leiXingReg, 1).Replace("</li><li>", "/").Replace("</li>", "").Replace("<li>", "");
                    leixing = leixing.Replace("<iclass=\"highlight-left\"></i>", "").Replace("<iclass=\"highlight-right\"></i>", "");
                    string leixing_r = ""; ;
                    Regex lx = new Regex(aReg);
                    if (lx.IsMatch(zhuyan))
                    {
                        var ec_lx = lx.Matches(leixing);
                        foreach (Match item in ec_lx)
                        {

                            leixing_r = leixing_r + item.Groups[1].Value + "/";


                        }
                    }

                    string diquReg = "<li><spanclass=\"desc-title\">地区：</span>(.*?)</li>";
                    string diqu = GetByReg(strHtml, diquReg, 1).Replace("&nbsp;", "");

                    string shangyin = GetByReg(strHtml, diquReg, 2).Replace("&nbsp;", "");


                    string pianChang = GetByReg(strHtml, diquReg, 3).Replace("&nbsp;", "");
                    string bieMingReg = "<li><spanclass=\"desc-title\">别名：</span>(.*?)</li>";
                    string bieMing = GetByReg(strHtml, bieMingReg, 1).Replace("&nbsp;", "");



                    string dbReg = "<spanclass=\"desc-titlepl30\">豆瓣评分：</span><spanclass=\"rating-score\">(.*?)</span>";
                    string dbPingFen = GetByReg(strHtml, dbReg, 1).Replace("&nbsp;", "");
                    //  string dbUrl = GetByReg(dbReg, dbReg, 1);



                    string imgName = Sub(imgPath);

                    string sPath = ConfigurationManager.AppSettings["path"] + "\\" + StringTool.SubStr(imgName, 2) + "\\";

                    if (Directory.Exists(sPath) == false)
                    {
                        Directory.CreateDirectory(sPath);
                    }

                    HttpTool.DownLoadImg(imgPath, sPath + imgName + ".jpg");

                    T_Movie movie = new T_Movie();
                    movie.Actor = zhuyan_r;
                    movie.AnotherName = bieMing;
                    movie.Area = diqu;
                    movie.DBScore = dbPingFen == "" ? 0 : Convert.ToDecimal(dbPingFen);
                    movie.DBUrl = "";
                    movie.Director = daoyan;
                    movie.Hit = 1;
                    movie.ImagePath = imgName;
                    movie.IMDBScore = 0; //IMDBPingFen == "" ? 0 : Convert.ToDecimal(IMDBPingFen);
                    movie.IMDBUrl = "";// IMDBUrl;
                    movie.Intro = intro;
                    movie.Length = pianChang;
                    movie.MovieName = dianYingName;
                    movie.Type = leixing_r;

                    movie.Year = 0;// shangyin == "" ? 0 : Convert.ToInt32(shangyin);

                    movie.ReleaseTime = shangyin;
                    movie.UpdateTime = DateTime.Now;

                    movie.ChannelID = 2;
                    if (dyStatus.Contains("更新"))
                    {
                        movie.IsFinish = false;
                    }
                    else
                    {
                        movie.IsFinish = true;
                    }
                    movie.SourceID = bdMovieId;
                    if (!string.IsNullOrEmpty(dianYingName))
                    {
                        int mid = GetMovieIdExists2(dianYingName);
                        if (mid > 0)
                        {
                            string sql = "update T_Movie set IsFinish=@IsFinish  where MovieId=" + mid;
                            DbHelperSQL.GetSingle(sql, new SqlParameter("@IsFinish", movie.IsFinish));
                            GetTVlink("http://v.baidu.com/tv_intro/?dtype=tvPlayUrl&service=json&id=" + bdMovieId + "&callback=bd__cbs__d4uc03", mid, dianYingName);
                            Msg(dianYingName + "录入播放库成功");
                        }
                        else
                        {
                            int movieId = Add(movie);//获取插入后的ID
                            Msg(dianYingName + "入库成功");
                            GetTVlink("http://v.baidu.com/tv_intro/?dtype=tvPlayUrl&service=json&id=" + bdMovieId + "&callback=bd__cbs__d4uc03", movieId, dianYingName);
                            Msg(dianYingName + "录入播放库成功");
                        }
                    }
                }
                else
                {
                    //  WriteLog.PrintLn(url + "访问失败");
                }
            }
            catch (Exception e)
            {

                WriteLog.PrintLn("movieid：" + bdMovieId + "-访问失败");
            }
        }

        private void btnTV_Click(object sender, EventArgs e)
        {
            this.btnTV.Enabled = false;
            thread = new Thread(new ParameterizedThreadStart(TV));
            thread.IsBackground = true;
            thread.Start(this.txtPage.Text);
        }
        public void TV(object OBJ)
        {
            //for (int i = 0; i < Data.TVTypes.Length; i++)
            //{
            //    WriteLog.PrintLn("电视剧类型：" + Data.TVTypes[i]);
            //    for (int j = 0; j < Data.TVAreas.Length; j++)
            //    {
            //        jilu = 0;
            //        GetTVList(Data.TVTypes[i], Data.TVAreas[j], 1);
            //    }
            //}
            jilu = 0;
            GetTVList("", "","", 1);
            Msg("电视剧采集完毕");
            this.btnTV.Enabled = true;
        }
        public void GetTVlink(string html, int movieId, string name)
        {
            string strHtml = HttpTool.GetHtml(html, new CookieContainer());

            strHtml = strHtml.Replace("bd__cbs__d4uc03(", "").Replace(")", "").Replace("/**/", "");


            List<S> list = new List<S>();
            System.Runtime.Serialization.Json.DataContractJsonSerializer json = new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(List<S>));
            using (MemoryStream stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(strHtml)))
            {
                list = (List<S>)json.ReadObject(stream);

            }
            if (list != null && list.Count > 0)
            {
                bool isUpdate = false;
                for (int i = 0; i < list.Count; i++)
                {
                    for (int j = 0; j < list[i].episodes.Count; j++)
                    {
                        T_Source es = new T_Source();
                        es.MovieId = movieId;
                        es.MovieName = list[i].episodes[j].single_title;
                        es.MovieURL = list[i].episodes[j].url;
                        es.Site = list[i].episodes[j].site_url;
                        es.Episode = StringTool.GetInt(list[i].episodes[j].episode);
                        if (!ExistsUrl(es))
                        {
                            isUpdate = true;
                            AddToSource(es);

                            Msg(name + ":新加(" + es.MovieName + ")");
                        }
                    }

                }
                if (isUpdate) {
                    //是否有更新
                    string sql = "update T_Movie set UpdateTime=@UpdateTime where MovieId=" + movieId;
                    DbHelperSQL.GetSingle(sql,  new SqlParameter("@UpdateTime", DateTime.Now));
                }


            }
            else
            {
                string sql = "update T_Movie set IsFinish=0 where MovieId=" + movieId;
                DbHelperSQL.GetSingle(sql);
            }
        }
        public List<T> JSONStringToList<T>(string JsonStr)
        {

            JavaScriptSerializer Serializer = new JavaScriptSerializer();

            List<T> objs = Serializer.Deserialize<List<T>>(JsonStr);

            return objs;

        }
        public class J
        {
            public List<S> s;
            public J()
            {
                s = new List<S>();
            }
        }
        public class Site_info
        {
            public string site;
        }
        public class S
        {
            public List<Episodes> episodes;
            //    public Site_info site_info;
            public S()
            {
                episodes = new List<Episodes>();
            }
        }
        /// <summary>
        /// 电视剧Json参数
        /// </summary>
        public class Episodes
        {
            public string single_title;
            public string url;
            public string episode;
            public string site_url;
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool AddToSource(T_Source model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_Source(");
            strSql.Append("MovieId,MovieName,MovieURL,Episode,Site)");
            strSql.Append(" values (");
            strSql.Append("@MovieId,@MovieName,@MovieURL,@Episode,@Site)");
            SqlParameter[] parameters = {
				
					new SqlParameter("@MovieId", SqlDbType.Int,4),
					new SqlParameter("@MovieName", SqlDbType.NVarChar,250),
					new SqlParameter("@MovieURL", SqlDbType.NVarChar,350),
					new SqlParameter("@Episode", SqlDbType.Int,4),
					new SqlParameter("@Site", SqlDbType.NVarChar,50)};

            parameters[0].Value = model.MovieId;
            parameters[1].Value = model.MovieName;
            parameters[2].Value = model.MovieURL;
            parameters[3].Value = model.Episode;
            parameters[4].Value = model.Site;

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
        #endregion
        public List<int> SelectMovieList()
        {

            string strSql = " select MovieId FROM T_Movie WHERE ChannelID=2 AND IsFinish=0  ";
            List<int> list = new List<int>();
            using (SqlDataReader reader = DbHelperSQL.ExecuteReader(strSql))
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        list.Add(Convert.ToInt32(reader["MovieId"]));

                    }

                }
            }
            return list;

        }


        public List<T_Movie> SelectMovieListForNull(string str)
        {

            string strSql = str;
            List<T_Movie> list = new List<T_Movie>();
            using (SqlDataReader reader = DbHelperSQL.ExecuteReader(strSql))
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        T_Movie m = new T_Movie();
                        m.SourceID = reader["SourceID"].ToString();
                        m.MovieId = Convert.ToInt32(reader["MovieId"]);
                        list.Add(m);

                    }

                }
            }
            return list;

        }
        #region 定时采集更新

        public void dyUpdate(string status)
        {
            try
            {
                int page = 1;
                List<string> list = new List<string>();
                //%E6%AD%A3%E7%89%87
                string str = HttpTool.GetHtml("http://v.baidu.com/commonapi/movie2level/?callback=jQuery19107936704258900136_1401507550406&filter=true&type=&area=&actor=&start=&complete=&order=pubtime&pn=" + page + "&rating=&prop=&_=1401507550407", new CookieContainer());

                if (!string.IsNullOrEmpty(str))
                {
                    page++;
                    this.txtPage.Text = page.ToString();
                    string xx = "\"id\":\"(.*?)\",\"url\"";

                    Regex r = new Regex(xx);
                    if (r.IsMatch(str))
                    {
                        var ec = r.Matches(str);
                        foreach (Match item in ec)
                        {

                            if (!list.Contains(item.Groups[1].Value))
                            {
                                list.Add(item.Groups[1].Value);
                            }


                        }
                    }
                    else
                    {
                        jilu++;
                    }

                    for (int i = 0; i < list.Count; i++)
                    {
                        GetInfo(list[i]);
                    }

                }

                if (this.listLog.Items.Count > 100)
                {
                    //清空日记
                    this.listLog.Items.Clear();

                }




            }
            catch (Exception e)
            {
                //   ChongQi();

                WriteLog.PrintLn("dyupdate：" + e.Message);
            }
        }
        public void dsjUpdate()
        {
            try
            {
                int page = 1;
                List<string> list = new List<string>();
                string str = HttpTool.GetHtml("http://v.baidu.com/commonapi/tvplay2level/?callback=jQuery1910954512323718518_1401597014307&filter=false&type=&area=&actor=&start=&complete=&order=pubtime&pn=" + page + "&rating=&prop=&_=1401597014311", new CookieContainer());

                if (!string.IsNullOrEmpty(str))
                {
                    page++;
                    this.txtPage.Text = page.ToString();
                    string xx = "\"id\":\"(.*?)\",\"site\"";

                    Regex r = new Regex(xx);
                    if (r.IsMatch(str))
                    {
                        var ec = r.Matches(str);
                        foreach (Match item in ec)
                        {

                            if (!list.Contains(item.Groups[1].Value))
                            {
                                list.Add(item.Groups[1].Value);
                            }


                        }
                    }
                    else
                    {
                        jilu++;
                    }

                    for (int i = 0; i < list.Count; i++)
                    {
                        GetTVInfo(list[i]);
                    }

                }

                if (this.listLog.Items.Count > 100)
                {
                    //清空日记
                    this.listLog.Items.Clear();

                }



            }
            catch (Exception e)
            {
                ChongQi();

                WriteLog.PrintLn("GetList" + e.Message);
            }
        }
        /// <summary>
        /// 更新已有的电影
        /// </summary>
        public void updatedy()
        {

        }
        public void updatedsj()
        {
            List<int> list = SelectMovieList();
            for (int i = 0; i < list.Count; i++)
            {
                GetTVInfo(list[i].ToString());
            }
        }
        #endregion

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            this.btnUpdate.Enabled = false;
            thread = new Thread(new ParameterizedThreadStart(UpdateFM));
            thread.IsBackground = true;
            thread.Start(this.txtPage.Text);


         


        }
        public void UpdateFM(object OBJ)
        {
            List<T_Movie> list = SelectMovieListForNull("select MovieId,SourceID  from [dbo].[T_Movie] where ChannelID=1 AND IsFinish=0");
            for (int i = 0; i < list.Count; i++)
            {
                try
                {
              

                    GetInfo(list[i].SourceID);
                }
                catch (Exception e1)
                {

                    WriteLog.PrintLn("SourceID:" + list[i].SourceID);
                }

               
            }
            Msg("处理完毕");
            this.btnUpdate.Enabled = true;
        }
        #region 更新图片
        private void button1_Click(object sender, EventArgs e)
        {
            thread = new Thread(new ParameterizedThreadStart(ccccccccccc));
            thread.IsBackground = true;
            thread.Start(this.txtPage.Text);

        }
        public void ccccccccccc(object obj)
        {
            List<int> ALL = SelectMovieIdList();
            try
            {


                for (int i = 0; i < ALL.Count; i++)
                {

                    string url = "http://v.baidu.com/movie/" + ALL[i] + ".htm?frp=browse";


                    string strHtml = HttpTool.GetHtml(url, new CookieContainer());


                    strHtml = strHtml.Replace("\n", "");

                    string reg = "<div class=\"poster-sec\">(.*?)</div>";
                    string result = "";
                    string xx = "";
                    Regex r = new Regex(reg);
                    if (r.IsMatch(strHtml))
                    {
                        Match m = r.Match(strHtml);
                        xx = m.Groups[1].Value;
                    }
                    xx = xx.Replace("\t", "");
                    xx = xx.Replace(" ", "");
                    string reg1 = "<imgsrc=\"(.*?)\"width=\"225\"height=\"300\"alt=\"(.*?)\"/>";

                    Regex r1 = new Regex(reg1);
                    if (r1.IsMatch(xx))
                    {
                        Match m = r1.Match(xx);
                        result = m.Groups[1].Value;
                        string imgName = Sub(result);

                        string sPath = ConfigurationManager.AppSettings["path"] + "\\" + StringTool.SubStr(imgName, 2) + "\\";

                        if (Directory.Exists(sPath) == false)
                        {
                            Directory.CreateDirectory(sPath);
                        }

                        HttpTool.DownLoadImg(result, sPath + imgName + ".jpg");
                        string sql = "update T_Movie set ImagePath='" + imgName + "' where ChannelID=1 and SourceID=" + ALL[i];
                        DbHelperSQL.GetSingle(sql);
                        Msg(ALL[i] + "更新成功！");
                    }
                }
            }
            catch (Exception d)
            {
                Msg(d.Message);
            }
            Msg("处理完毕");
        }
        public List<int> SelectMovieIdList()
        {

            string strSql = " select SourceID FROM T_Movie WHERE ChannelID=1 AND ImagePath=''  ";
            List<int> list = new List<int>();
            using (SqlDataReader reader = DbHelperSQL.ExecuteReader(strSql))
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        list.Add(Convert.ToInt32(reader["SourceID"]));

                    }

                }
            }
            return list;

        }
        #endregion

        private void Cb_Channel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Cb_Channel.Text == "电影")
            {
                this.CB_Type.Items.Clear();
                this.CB_Type.Items.AddRange(Data.dyYypes);
                this.CB_Type.SelectedIndex = 0;

                this.CB_Area.Items.Clear();
                this.CB_Area.Items.AddRange(Data.areas);
                this.CB_Area.SelectedIndex = 0;

                this.CB_Year.Items.Clear();
                this.CB_Year.Items.AddRange(Data.years);
                this.CB_Year.SelectedIndex = 0;
            }
            else {
                this.CB_Type.Items.Clear();
                this.CB_Type.Items.AddRange(Data.TVTypes);
                this.CB_Type.SelectedIndex = 0;

                this.CB_Area.Items.Clear();
                this.CB_Area.Items.AddRange(Data.TVAreas);
                this.CB_Area.SelectedIndex = 0;

                this.CB_Year.Items.Clear();
                this.CB_Year.Items.AddRange(Data.TVYears);
                this.CB_Year.SelectedIndex = 0;
            
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            jilu = 0;
            if (this.Cb_Channel.Text == "电影")
            {
                GetList(this.CB_Type.Text, this.CB_Area.Text, this.CB_Year.Text, Convert.ToInt32(this.txtPage.Text));
            }
            if (this.Cb_Channel.Text == "电视剧")
            {
                GetTVList(this.CB_Type.Text, this.CB_Area.Text, this.CB_Year.Text, Convert.ToInt32(this.txtPage.Text));
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (this.Cb_Channel.Text == "电影")
            {
                GetInfo(this.txtbdMovieId.Text);
            }
            if (this.Cb_Channel.Text == "电视剧")
            {
                GetTVInfo(this.txtbdMovieId.Text);
            }
        }


    }

}
