using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DianYing.Web.Tool;
using DianYing.BLL;
using DianYing.Model;
using System.Text;
using Tool;
using System.Configuration;
using System.IO;
using DianYing.Web.Base;

namespace DianYing.Web.Manage
{
    public partial class MovieInfo : ManagePage
    {
        public E_Movie em=new E_Movie();
        public string method="save";
        protected void Page_Load(object sender, EventArgs e)
        {
            T_Movie tMovie = new T_Movie();
            string method = this.Request["method"];
            int movieId = StringTool.GetInt(this.Request["MovieId"]);
            if (method == "save")
            {
                
                E_Movie eMovie = new E_Movie();
                eMovie.MovieId = StringTool.GetInt(this.Request["MovieId"]);
                eMovie.ChannelID = StringTool.GetInt(this.Request["ChannelID"]);
                eMovie.MovieName = this.Request["MovieName"];
                eMovie.Director = this.Request["Director"];
                eMovie.Actor = this.Request["Actor"];
                eMovie.Type = this.Request["Type"];
                eMovie.Area = this.Request["Area"];
                eMovie.Length = this.Request["Length"];
                eMovie.AnotherName = this.Request["AnotherName"];
                eMovie.DBScore = StringTool.GetDecimal(this.Request["DBScore"]);
                eMovie.IMDBScore = StringTool.GetDecimal(this.Request["IMDBScore"]);
                    eMovie.ImagePath = Request["ImagePath"];

                    eMovie.Hit = 1;
                eMovie.Intro = this.Request["Intro"];
                eMovie.ReleaseTime = this.Request["ReleaseTime"];
                eMovie.Year =StringTool.GetInt(this.Request["Year"]);
                eMovie.IsFinish = Convert.ToBoolean(this.Request["IsFinish"]);
                eMovie.UpdateTime = DateTime.Now;
                if (movieId > 0)
                {
                    tMovie.Update(eMovie);
                    Response.Redirect("/Manage/PlayLinkInfo.aspx?MovieId=" + movieId);
                }
                else {
                 int id=   tMovie.Add(eMovie);
                 Response.Redirect("/Manage/PlayLinkInfo.aspx?MovieId="+id);
                }
            }
            if (method == "delete")
            {
              Response.Write(  tMovie.Delete(movieId));
              Response.End();
            }
            else
            {

                if (movieId > 0)
                {
                    //修改

                    E_Movie eMovie = new E_Movie();

                    eMovie.MovieId = movieId;
                  
                    em = tMovie.SelectMovie(eMovie);
                    //   this.upload.Attributes.Add("style", "display:none");
                }
                else
                {
                    //  this.img.Attributes.Add("style", "display:none");
                }
            }
        }
        public string GetChannel(int? channelId)
        {
            StringBuilder channelStr = new StringBuilder();

            for (int i = 0; i < Data.ChannelList.Count; i++)
            {
                if (Data.ChannelList[i].ChannelID == channelId)
                {
                    channelStr.Append(" <option selected=\"selected\"  value=\"" + Data.ChannelList[i].ChannelID + "\">" + Data.ChannelList[i].ChannelName + "</option>");
                }
                else
                {
                    channelStr.Append(" <option value=\"" + Data.ChannelList[i].ChannelID + "\">" + Data.ChannelList[i].ChannelName + "</option>");
                }
            }
            return channelStr.ToString();
        }
    }
}