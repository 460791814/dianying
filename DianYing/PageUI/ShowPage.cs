using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;


namespace PageUI
{
    public class ShowPage
    {
        /**
      * 对搜索返回的前n条结果进行分页显示
      * @param keyWord       查询关键词
      * @param pageSize      每页显示记录数
      * @param currentPage   当前页 
      * @throws ParseException
      * @throws CorruptIndexException
      * @throws IOException
      */

        /// <summary>
        /// 生成翻页代码
        /// </summary>
        /// <param name="totalPageCount">总页数</param>
        /// <param name="pageSize">每页数据量</param>
        /// <param name="currentPageIndex">当前页</param>
        /// <param name="sLinkUrl">跳转地址</param>
        /// <returns></returns>
        public  static string ShowPageV(int totalPageCount, int pageSize, int currentPageIndex, string sLinkUrl)
        {
            int proPage = currentPageIndex - 1;
            int nextPage = proPage + 2;
            if (proPage < 1)
            {
                proPage = 1;
            }
            if (nextPage > totalPageCount)
            {
                nextPage = totalPageCount;
            }
            if (totalPageCount < 1 || pageSize < 1)
            {
                return "";
            }
            int start = currentPageIndex - (int)(Math.Ceiling(Convert.ToDouble(pageSize / 2)) - 1);
            if (pageSize < totalPageCount)
            {
                if (start < 1)
                {
                    start = 1;
                }
                else if (start + pageSize > totalPageCount)
                {
                    start = totalPageCount - pageSize + 1;
                }
            }
            else
            {
                start = 1;
            }
            // int end = start + pageSize - 1;
            int end = start + 10;
            if (end > totalPageCount)
            {
                end = totalPageCount;
            }
            StringBuilder newNumberStr = new StringBuilder();
            if (currentPageIndex <= 1)
            {
                newNumberStr.Append("<a href=\"javascript:void(0);\"  class=\"shouye\">首页</a><a href=\"javascript:void(0);\" disabled=\"disabled\" class=\"next\">上一页</a>");
            }
            else
            {
                newNumberStr.AppendFormat("<a href=\"{0}/1\" class=\"shouye\">首页</a><a href=\"{0}/{1}.html\" class=\"next\">上一页</a>", sLinkUrl, proPage);
            }
            for (var i = start; i <= end; i++)
            {

                if (i == currentPageIndex)
                {
                    newNumberStr.AppendFormat("<a title='" + i + "' href=\"javascript:void(0);\" class=\"se\">" + i + "</a>");
                }
                else
                {
                    newNumberStr.AppendFormat("<a title='" + i + "' href=\"{0}/{1}\">" + i + "</a>", sLinkUrl, i);
                }
            }
            if (currentPageIndex == totalPageCount)
            {
                newNumberStr.Append("<a href=\"javascript:void(0);\" disabled=\"disabled\" class=\"next\">下一页</a>");
            }
            else
            {
                newNumberStr.AppendFormat("<a href=\"{0}/{1}.html\" class=\"next\">下一页</a>", sLinkUrl, nextPage);
            }
         //   newNumberStr.AppendFormat("<a href=\"javascript:void(0);\" class=\"next\">共{0}页</a>", totalPageCount);
            if (totalPageCount > 1)
            {
                return newNumberStr.ToString();
            }
            else
            {
                return "";
            }
        }



    }
}
