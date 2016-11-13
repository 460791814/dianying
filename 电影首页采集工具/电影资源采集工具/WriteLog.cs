using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace TorrentTool.Tool
{
  
      public static class WriteLog
        {
            public static void Print(string format, params object[] args)
            {
                string msg = GetMessage(format, args);
                DateTime dt = System.DateTime.Now.ToLocalTime();

                ConsoleColor color = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Gray;
                if (_newLine)
                {
                    string str = string.Format("[{0}]: ", dt);
                    Console.Write(str);
                    WriteToLogFile(str);
                    _newLine = false;
                }
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(msg);
                WriteToLogFile(msg);
                Console.ForegroundColor = color;
            }

            public static void PrintLn1(string format, params object[] args)
            {
                Print(format, args);
                WriteToLogFile("\r\n");
                Console.WriteLine();
                _newLine = true;
            }

            public static void Error(string format, params object[] args)
            {
                string msg = GetMessage(format, args);
                DateTime dt = System.DateTime.Now.ToLocalTime();

                ConsoleColor color = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                if (!_newLine)
                {
                    WriteToLogFile("\r\n");
                    Console.WriteLine();
                }
                string str = string.Format("[{0}]: {1}", dt, msg);
                WriteToLogFile(str + "\r\n");
                Console.WriteLine(str);
                Console.ForegroundColor = color;
                _newLine = true;
            }

            private static string GetMessage(string format, object[] args)
            {
                if (args.Length > 0)
                {
                    return string.Format(format, args);
                }
                else
                {
                    return format;
                }
            }

            private static void WriteToLogFile(string str)
            {

                if (!WriteToFile)
                {
                    return;
                }
                if (string.IsNullOrEmpty(str))
                {
                    return;
                }

                str = str.Replace("\b", "");
                StreamWriter sw = new StreamWriter(c_LogFile, true);
                try
                {
                    sw.Write(str);
                    sw.Flush();
                    sw.Close();
                }
                catch (Exception)
                {
                    
                    sw.Flush();
                    sw.Close();
                    sw.Dispose();
                }
                
            }
            
            public static string LogFile = "";
            public static string c_LogFile = "log" + DateTime.Today.ToString("yyyy-MM-dd-hh") + ".txt";
            private static bool _newLine = true;
            public static bool WriteToFile = true;

            /// <summary>
            /// 保存记录（Log/年-月-日）
            /// </summary>
            /// <param name="sMessage"></param>
            public static void PrintLn(string sMessage)
            {
                try
                {

             
                DateTime dt = System.DateTime.Now.ToLocalTime();
                sMessage = string.Format("[{0}]: ", dt) + sMessage;
                string sPath = Environment.CurrentDirectory + "/Log/" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "/";
                if (Directory.Exists(sPath) == false) Directory.CreateDirectory(sPath);
                File.AppendAllText(sPath + DateTime.Now.Day.ToString() + ".log", sMessage + "\r\n");
                }
                catch (Exception)
                {

                   
                }
            }
        }
}
