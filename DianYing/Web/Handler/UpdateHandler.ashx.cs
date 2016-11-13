using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DianYing.BLL;
using DianYing.Model;
using DianYing.Web.Tool;

namespace DianYing.Web.Handler
{
    /// <summary>
    /// UpdateHandler 的摘要说明
    /// </summary>
    public class UpdateHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string method = context.Request["method"];
            T_Source tSource = new T_Source();
            E_Source eSource = new E_Source();
            eSource.Id = StringTool.GetInt(context.Request["SourceId"]);
            eSource.MovieId = StringTool.GetInt(context.Request["MovieId"]);
            eSource.MovieName = context.Request["MovieName"];
            eSource.MovieURL = context.Request["MovieURL"];
            eSource.Site = context.Request["Site"];
            eSource.Episode = StringTool.GetInt(context.Request["Episode"]);
            switch (method)
            {
                case "add":
                    int id = tSource.Add(eSource);
                    bool b = true;
                    if (id <= 0)
                    {
                        b = false;
                    }

                    context.Response.Write(b);

                    break;
                case "update":
                    context.Response.Write(tSource.Update(eSource));
                    break;
                case "delete":
                    context.Response.Write(tSource.Delete(eSource.Id));
                    break;
                default:
                    break;
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