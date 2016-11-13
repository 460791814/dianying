using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DianYing.Web.Base;
using DianYing.BLL;
using DianYing.Model;
using DianYing.Web.Tool;

namespace DianYing.Web.Manage
{
    public partial class ChannelList : ManagePage
    {
        T_Channel tChannel = new T_Channel();
        public List<E_Channel> list;
        protected void Page_Load(object sender, EventArgs e)
        {
            E_Channel eChannel = new E_Channel();
            eChannel.ChannelID = StringTool.GetInt(this.Request["ChannelID"]);
            eChannel.ChannelName = this.Request["ChannelName"];
            switch (this.Request["method"])
            {
                case "add":
                  int id=   tChannel.Add(eChannel);
                  if (id > 0)
                  {
                      Response.Write("True");
                      Response.End();
                  }
                  else {
                      Response.Write("End");
                      Response.End();
                  }
                    break;
                case "update":
                    Response.Write(tChannel.Update(eChannel));
                    Response.End();
                    break;
                case "delete":
                    Response.Write(tChannel.Delete(eChannel.ChannelID));
                    Response.End();
                    break;
                default:
                    DefaultMethod();
                    break;
            }
         
        }
        public void DefaultMethod()
        {
            list = tChannel.SelectChannelList();
        }
    }
}