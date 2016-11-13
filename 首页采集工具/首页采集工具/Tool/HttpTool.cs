using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Net;
using System.IO;

namespace Tool
{
   public static class HttpTool
    {
        public static long UNIX_TIMESP(DateTime dateTime)
        {

            return (dateTime.Ticks - DateTime.Parse("1970-01-01 00:00:00").Ticks) / 10000000;

        }
        public static string getMd5Hash2(string input)
        {

            byte[] buffer = MD5.Create().ComputeHash(Encoding.Default.GetBytes(input));

            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < buffer.Length; i++)
            {

                builder.Append(buffer[i].ToString("x2"));

            }

            return builder.ToString();

        }
          public static string GetBetween(string s, string s1, string s2)

        {

            int n1, n2;

            n1 = s.IndexOf(s1, 0);

            if (n1 == -1)

                return "";

            n1=n1+s1.Length;   //开端地位

            n2 = s.IndexOf(s2, n1);               //停止地位

            if (n2 == -1)

                return "";

            return s.Substring(n1, n2 - n1);   //取搜刮的条数,用停止的地位-开端的地位,并返回

        }
         public static Stream GetStream(string URL, CookieContainer cookieContainer)

        {

            HttpWebRequest httpWebRequest;

            HttpWebResponse webResponse;

            Stream getStream;

            httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(URL);

            httpWebRequest.CookieContainer = cookieContainer;

            httpWebRequest.Accept = "*/*";

            //httpWebRequest.ContentType = "application/x-www-form-urlencoded";

            httpWebRequest.UserAgent = "User-Agent:Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/535.2 (KHTML, like Gecko) Chrome/15.0.874.106 Safari/535.2";

            httpWebRequest.Method = "GET";

            webResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            string header = webResponse.Headers.ToString();



            getStream = webResponse.GetResponseStream();



            return getStream;

        }

        public static string GetHtmlHeader(string URL, CookieContainer cookieContainer)

        {

            HttpWebRequest httpWebRequest;

            HttpWebResponse webResponse;

            Stream getStream;

            StreamReader streamReader;

            string getString = "";

            httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(URL);

            httpWebRequest.CookieContainer = cookieContainer;

            httpWebRequest.Accept = "*/*";

            httpWebRequest.ContentType = "text/html;charset=UTF-8";

            httpWebRequest.UserAgent = "User-Agent:Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/535.2 (KHTML, like Gecko) Chrome/15.0.874.106 Safari/535.2";

            httpWebRequest.Method = "GET";

            webResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            string header = webResponse.Headers.ToString();

         

            getStream = webResponse.GetResponseStream();

            streamReader = new StreamReader(getStream, Encoding.UTF8);

            getString = streamReader.ReadToEnd();



            streamReader.Close();

            getStream.Close();

            httpWebRequest.Abort();

            webResponse.Close();



            return header;

        }

        public static string GetHtmlPostHeader(string URL, string postData, CookieContainer cookieContainer)

        {

            HttpWebRequest httpWebRequest;

            HttpWebResponse webResponse;

            Stream getStream;

            StreamReader streamReader;

            string getString = "";

            httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(URL);

            httpWebRequest.CookieContainer = cookieContainer;

            httpWebRequest.Accept = "*/*";

            httpWebRequest.ContentType = "application/x-www-form-urlencoded";

            httpWebRequest.Referer = "http://i.xunlei.com/login.html";

            httpWebRequest.UserAgent = "User-Agent:Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/535.2 (KHTML, like Gecko) Chrome/15.0.874.106 Safari/535.2";

            httpWebRequest.Method = "Post";

            byte[] byteRequest = Encoding.Default.GetBytes(postData);

            httpWebRequest.ContentLength = byteRequest.Length;

            Stream stream;

            stream = httpWebRequest.GetRequestStream();

            stream.Write(byteRequest, 0, byteRequest.Length);

            stream.Close();

            webResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            string header = webResponse.Headers.ToString();

            getStream = webResponse.GetResponseStream();

            streamReader = new StreamReader(getStream, Encoding.UTF8);

            getString = streamReader.ReadToEnd();





            streamReader.Close();

            getStream.Close();

            httpWebRequest.Abort();

            webResponse.Close();



            return header;

        }

        public static string GetHtmlPostMsg(string URL, CookieContainer cookieContainer)

        {

            HttpWebRequest httpWebRequest;

            HttpWebResponse webResponse;

            Stream getStream;

            StreamReader streamReader;

            string getString = "";

            httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(URL);

            httpWebRequest.CookieContainer = cookieContainer;

            httpWebRequest.Accept = "*/*";

            httpWebRequest.ContentType = "application/x-www-form-urlencoded";

            //httpWebRequest.Referer = "http://vod.xunlei.com/iplay.html？uvs=26792633_4_166BF1D2FD43C79F8605A78BFE0E65CD8FE775E494F79F64FB63CA6C692D71156FB80795D39CB79BC4DA348AFE399ECE96856BEAFAB89C9D7598D1AFBEAAFD5E&=vlist&url=bt％3A％2F％2F0C8F6F3292F7049E4E76716C242A4BA0944D1466％2F2&list=all";

            httpWebRequest.Referer = "http://vod.xunlei.com/iplay.html";

            httpWebRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.2; WOW64) AppleWebKit/537.17 (KHTML, like Gecko) Chrome/24.0.1312.57 Safari/537.17";

            httpWebRequest.Method = "Get";



            webResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            string header = webResponse.Headers.ToString();

            getStream = webResponse.GetResponseStream();

            streamReader = new StreamReader(getStream, Encoding.UTF8);

            getString = streamReader.ReadToEnd();



            streamReader.Close();

            getStream.Close();

            httpWebRequest.Abort();

            webResponse.Close();



            return getString;

        }
        public static string GetHtml(string URL, CookieContainer cookieContainer)
        {
            try
            {

         
            HttpWebRequest httpWebRequest;

            HttpWebResponse webResponse;

            Stream getStream;

            StreamReader streamReader;

            string getString = "";

            httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(URL);

            httpWebRequest.CookieContainer = cookieContainer;

            httpWebRequest.Accept = "*/*";

            httpWebRequest.ContentType = "text/html;charset=UTF-8";

            httpWebRequest.UserAgent = "User-Agent:Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/535.2 (KHTML, like Gecko) Chrome/15.0.874.106 Safari/535.2";

            httpWebRequest.Method = "GET";

            webResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            string header = webResponse.Headers.ToString();



            getStream = webResponse.GetResponseStream();

            streamReader = new StreamReader(getStream, Encoding.UTF8);

            getString = streamReader.ReadToEnd();



            streamReader.Close();

            getStream.Close();

            httpWebRequest.Abort();

            webResponse.Close();



            return getString;
            }
            catch (Exception)
            {

                return null;
            }

        }
       /// <summary>
       /// 下载图片的方法
       /// </summary>
       /// <param name="imgUrl"></param>
       /// <param name="filePath"></param>
        public static void DownLoadImg(string imgUrl,string filePath)
        {
            try
            {


            string url = imgUrl;
         
            WebClient mywebclient = new WebClient();
            mywebclient.DownloadFile(url, filePath);
            }
            catch (Exception)
            {

               // throw;
            }
        }
        public static void DownImg()
        {

            WebRequest request = WebRequest.Create("http://images.cnblogs.com/logo_small.gif");
            WebResponse response = request.GetResponse();
            Stream reader = response.GetResponseStream();
            FileStream writer = new FileStream("x:\\pic.jpg", FileMode.OpenOrCreate, FileAccess.Write);
            byte[] buff = new byte[512];
            int c = 0; //实际读取的字节数
            while ((c = reader.Read(buff, 0, buff.Length)) > 0)
            {
                writer.Write(buff, 0, c);
            }
            writer.Close();
            writer.Dispose();
            reader.Close();
            reader.Dispose();
            response.Close();
        }
    }
}
