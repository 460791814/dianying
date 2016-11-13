using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DianYing.BLL;
using DianYing.Model;
using DianYing.Web.Tool;

namespace DianYing.Web.Manage
{
    public partial class LinkInfo : System.Web.UI.Page
    {
        public E_Source eSource;
        protected void Page_Load(object sender, EventArgs e)
        {
            string method = Request["method"];
            T_Source tSource = new T_Source();
             eSource = new E_Source();
            eSource.Id = StringTool.GetInt(Request["SourceId"]);
            eSource.MovieId = StringTool.GetInt(Request["MovieId"]);
            eSource.MovieName = Request["MovieName"];
            eSource.MovieURL = Request["MovieURL"];
            eSource.Site = Request["Site"];
            eSource.Episode = StringTool.GetInt(Request["Episode"]);

           
            switch (method)
            {
                case "add":
                    int id = tSource.Add(eSource);
                    bool b = true;
                    if (id <= 0)
                    {
                        b = false;
                    }

                    Response.Write(b);
                    Response.End();
                    break;
                case "update":
                    Response.Write(tSource.Update(eSource));
                    Response.End();
                    break;
                case "delete":
                   Response.Write(tSource.Delete(eSource.Id));
                   Response.End();
                    break;
                default:
                    if (eSource.Id > 0)
                    {
                        eSource = tSource.GetModel(eSource.Id);
                    }
                    break;
            }
           
        }
      
    }
}