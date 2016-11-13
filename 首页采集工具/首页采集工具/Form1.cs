using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using Tool;
using System.Configuration;
using System.IO;

namespace 首页采集工具
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<string> list = new List<string>();
            string strSql = "select  ImagePath from T_Movie where ChannelID=1";
            using (SqlDataReader reader = SqlHelper.ExecuteDataReader(strSql))
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        list.Add(reader["ImagePath"].ToString());

                    }

                }
            }

            int count = list.Count();
            for (int i = 0; i < list.Count; i++)
            {
                string imgPath = list[i];
                string msPath = ConfigurationManager.AppSettings["mpath"] + "\\" + imgPath.Substring(0, 2) + "\\";

                if (Directory.Exists(msPath) == false)
                {
                    Directory.CreateDirectory(msPath);
                }
               
                HttpTool.DownLoadImg("http://dianying.fm/poster/m/" + imgPath, msPath + imgPath + ".jpg");
            }
            MessageBox.Show("处理完毕");
          
        }
    }
}
