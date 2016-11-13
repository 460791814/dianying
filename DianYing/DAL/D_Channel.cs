using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DianYing.Model;
using System.Data.SqlClient;
using Maticsoft.DBUtility;
using System.Data;

namespace DianYing.DAL
{
   public  class D_Channel
    {
        /// <summary>
        /// 根据表名获取对应的数据
        /// </summary>
        /// <param name="movieId"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public List<E_Channel> SelectChannelList()
        {

            string strSql = "select ChannelID, ChannelName from T_Channel";
            List<E_Channel> list = new List<E_Channel>();
            using (SqlDataReader reader = DbHelperSQL.ExecuteReader(strSql))
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        E_Channel e = new E_Channel();
                        e.ChannelID = Convert.ToInt32(reader["ChannelID"]);
                        e.ChannelName = reader["ChannelName"].ToString();
                        list.Add(e);

                    }

                }
            }
            return list;

        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(E_Channel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_Channel(");
            strSql.Append("ChannelName)");
            strSql.Append(" values (");
            strSql.Append("@ChannelName)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@ChannelName", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.ChannelName;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(E_Channel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_Channel set ");
            strSql.Append("ChannelName=@ChannelName");
            strSql.Append(" where ChannelID=@ChannelID");
            SqlParameter[] parameters = {
					new SqlParameter("@ChannelName", SqlDbType.NVarChar,50),
					new SqlParameter("@ChannelID", SqlDbType.Int,4)};
            parameters[0].Value = model.ChannelName;
            parameters[1].Value = model.ChannelID;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int ChannelID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_Channel ");
            strSql.Append(" where ChannelID=@ChannelID");
            SqlParameter[] parameters = {
					new SqlParameter("@ChannelID", SqlDbType.Int,4)
			};
            parameters[0].Value = ChannelID;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
