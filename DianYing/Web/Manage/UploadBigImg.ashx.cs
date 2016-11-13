using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DianYing.Web.Tool;
using System.Configuration;
using System.IO;

namespace DianYing.Web.Manage
{
    /// <summary>
    /// UploadBigImg 的摘要说明
    /// </summary>
    public class UploadBigImg : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
           
            context.Response.ContentType = "text/html";
            HttpPostedFile oFile = context.Request.Files["fileinput"];




            if (oFile.ContentLength <= 2048 * 1024)
            { //图片大小小于2M
                string imgName = StringTool.Md5(oFile.FileName);
                string sPath = ConfigurationManager.AppSettings["HomePath"] + "\\" + StringTool.SubStr(imgName, 2) + "\\";
                string _FileSavePath = HttpContext.Current.Server.MapPath(sPath);
                if (Directory.Exists(Path.GetDirectoryName(_FileSavePath)) == false)
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(_FileSavePath));
                }
                oFile.SaveAs(_FileSavePath + imgName + ".jpg");
                context.Response.Write(imgName);
            }
            else
            {
                context.Response.Write("1");
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