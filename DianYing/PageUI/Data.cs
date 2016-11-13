using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DianYing.BLL;
using DianYing.Model;
using System.Configuration;

namespace Tool
{
   public static class Data
    {

       public static string[] dyYypes = new string[] { "喜剧", "恐怖", "爱情", "动作", "科幻", "战争", "犯罪", "惊悚", "动画", "剧情", "古装", "奇幻", "武侠", "冒险", "悬疑", "传记", "运动", "音乐" };
       public static string[] areas = new string[] { "内地", "美国", "香港", "台湾", "法国", "英国", "德国", "泰国", "印度", "欧洲地区", "东南亚地区", "其他地区" };
       public static string[] TVTypes = new string[] { "古装","战争","青春偶像","情感","喜剧","家庭伦理","犯罪","动作","奇幻","剧情","历史","经典","乡村","情景","商战","其他" };
       public static string[] TVAreas = new string[] { "内地","韩国","香港","台湾","日本","美国","泰国","英国","新加坡","其他" };
       public static string type = "/喜剧/恐怖/爱情/动作/科幻/战争/犯罪/惊悚/动画/剧情/古装/奇幻/武侠/冒险/悬疑/传记/运动/音乐/青春偶像/情感/家庭伦理/历史/经典/乡村/情景/商战/";
       public static string[] MovieArea = ConfigurationManager.AppSettings["MovieArea"].Split('|');//new string[] { "内地", "美国", "韩国", "香港", "台湾", "法国", "英国", "德国", "泰国", "印度", "日本", "新加坡", "欧洲地区", "东南亚地区", "其他" };
       public static string[] MovieType = ConfigurationManager.AppSettings["MovieType"].Split('|');//new string[] { "喜剧","恐怖","爱情","动作","科幻","战争","犯罪","惊悚","动画","剧情","古装","奇幻","武侠","冒险","悬疑","传记","运动","音乐","青春偶像","情感","家庭伦理","历史","经典","乡村","情景","商战" };
       public static string[] DomainName = ConfigurationManager.AppSettings["DomainName"].Split('|');
       public static List<E_Channel> ChannelList = new T_Channel().SelectChannelList();
       public static string CaseName(string str)
       {
           string name = "";
           switch (str)
           {
               case "m1905.com":
                   name = "电影网";
                   break;
               case "fun.tv":
                   name = "风行网";
                   break;
               case "letv.com":
                   name = "乐视";
                   break;
               case "iqiyi.com":
                   name = "爱奇艺";
                   break;
               case "sohu.com":
                   name = "搜狐";
                   break;
               case "kankan.com ":
                   name = "迅雷看看";
                   break;
               case "wasu.cn":
                   name = "华数";
                   break;
               case "pps.tv":
                   name = "PPS";
                   break;
               case "pptv.com":
                   name = "PPTV";
                   break;
               case "baofeng.com":
                   name = "暴风";
                   break;
               case "56.com":
                   name = "56";
                   break;
               case "cntv.cn":
                   name = "央视网";
                   break;
               case "61.com":
                   name = "淘米网";
                   break;
               case "kumi.cn":
                   name = "酷米";
                   break;
               case "hunantv.com":
                   name = "芒果TV";
                   break;
               case "tudou.com":
                   name = "土豆";
                   break;
               case "youku.com":
                   name = "优酷";
                   break;
               default:
                   name = str;
                   break;
           }
           return name;
       }
    }
}
