using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DianYing.BLL;
using DianYing.Model;
using DianYing.Web.Tool;
using System.IO;
using System.Configuration;

namespace DianYing.Web.Handler
{
    /// <summary>
    /// UploadHandler 的摘要说明
    /// </summary>
    public class UploadHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            HttpPostedFile file = context.Request.Files[0];

            string fileName = file.FileName;
            string imgName = StringTool.Md5(fileName);

            string sPath = ConfigurationManager.AppSettings["HomePath"] + "\\" + StringTool.SubStr(imgName, 2) + "\\";
            string _FileSavePath = HttpContext.Current.Server.MapPath(sPath);
            if (Directory.Exists(Path.GetDirectoryName(_FileSavePath)) == false)
            {
                Directory.CreateDirectory(Path.GetDirectoryName(_FileSavePath));
            }
            file.SaveAs(_FileSavePath + imgName + ".jpg");
            int movieId = StringTool.GetInt(context.Request.Form["MovieId"]);

            T_Movie tMovie = new T_Movie();
            E_Movie eMovie = new E_Movie();

            eMovie.MovieId = movieId;
            eMovie.BigImagePath = imgName;
            if (movieId <= 0) {
                context.Response.Write("请选择电影ID");
                return;
            }
            if (string.IsNullOrEmpty(imgName))
            {
                context.Response.Write("请上传电影图片");
                return;
            }
         bool isSucces=   tMovie.SetHome(eMovie);
         if (isSucces)
         {
             context.Response.Write("保存成功");
         }
         else {
             context.Response.Write("保存失败");
         }
        }
    
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}