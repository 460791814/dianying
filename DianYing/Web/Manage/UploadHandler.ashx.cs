using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using DianYing.Web.Tool;
using System.Configuration;

namespace DianYing.Web.Manage
{
    /// <summary>
    /// UploadHandler 的摘要说明
    /// </summary>
    public class UploadHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";
            HttpPostedFile oFile = context.Request.Files["fileinput"];


        
          
                if (oFile.ContentLength <= 2048 * 1024)
                { //图片大小小于2M
                    string imgName = StringTool.Md5(oFile.FileName);
                    string sPath = ConfigurationManager.AppSettings["ListPath"] + "\\" + StringTool.SubStr(imgName, 2) + "\\";
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