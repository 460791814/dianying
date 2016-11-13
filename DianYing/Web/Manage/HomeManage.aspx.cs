using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DianYing.Web.Tool;
using System.Configuration;
using DianYing.BLL;
using DianYing.Model;

namespace DianYing.Web.Manage
{
    public partial class HomeManage : System.Web.UI.Page
    {
        public string imgPath = "";
        public string path = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Request["method"] == "save")
            {

                T_Movie tMovie = new T_Movie();
                E_Movie eMovie = new E_Movie();

                int movieId =StringTool.GetInt(this.Request["MovieId"]);
                string imgName = this.Request["img"];
                eMovie.MovieId = movieId;
                eMovie.BigImagePath = imgName;
                if (movieId <= 0)
                {
                    Response.Write("请选择电影ID");
                    return;
                }
                if (string.IsNullOrEmpty(imgName))
                {
                   Response.Write("请上传电影图片");
                    return;
                }
                bool isSucces = tMovie.SetHome(eMovie);
             
                    Response.Write(tMovie.SetHome(eMovie));
                    Response.End();
            }
            else if (this.Request["method"] == "delete")
            {
                T_Movie tMovie = new T_Movie();
                E_Movie eMovie = new E_Movie();

                int movieId = StringTool.GetInt(this.Request["MovieId"]);
           
                eMovie.MovieId = movieId;
                eMovie.BigImagePath = "";
                if (movieId <= 0)
                {
                    Response.Write("请选择电影ID");
                    return;
                }
             
                bool isSucces = tMovie.SetHome(eMovie);

                Response.Write(tMovie.SetHome(eMovie));
                Response.End();
            }
            else
            {
                path = ConfigurationManager.AppSettings["ListPath"].ToString();
                imgPath = string.IsNullOrEmpty(this.Request["img"]) ? "/Images/ftp-img.jpg" : ConfigurationManager.AppSettings["ListPath"].ToString() + StringTool.SubStr(this.Request["img"], 2) + this.Request["img"] + ".jpg";
            }
        }
    }
}