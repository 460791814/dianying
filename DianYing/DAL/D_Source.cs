using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Maticsoft.DBUtility;
using System.Data.SqlClient;
using System.Data;
using DianYing.Model;

namespace DianYing.DAL
{
   public  class D_Source
    {
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("Id", "T_Source");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from T_Source");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
			};
            parameters[0].Value = Id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(E_Source model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_Source(");
            strSql.Append("MovieId,MovieName,MovieURL,Episode,Site)");
            strSql.Append(" values (");
            strSql.Append("@MovieId,@MovieName,@MovieURL,@Episode,@Site)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@MovieId", SqlDbType.Int,4),
					new SqlParameter("@MovieName", SqlDbType.NVarChar,250),
					new SqlParameter("@MovieURL", SqlDbType.NVarChar,350),
					new SqlParameter("@Episode", SqlDbType.Int,4),
					new SqlParameter("@Site", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.MovieId;
            parameters[1].Value = model.MovieName;
            parameters[2].Value = model.MovieURL;
            parameters[3].Value = model.Episode;
            parameters[4].Value = model.Site;

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
        public bool Update(E_Source model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_Source set ");
        
            strSql.Append("MovieName=@MovieName,");
            strSql.Append("MovieURL=@MovieURL,");
            strSql.Append("Episode=@Episode,");
            strSql.Append("Site=@Site");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					
					new SqlParameter("@MovieName", SqlDbType.NVarChar,250),
					new SqlParameter("@MovieURL", SqlDbType.NVarChar,350),
					new SqlParameter("@Episode", SqlDbType.Int,4),
					new SqlParameter("@Site", SqlDbType.NVarChar,50),
					new SqlParameter("@Id", SqlDbType.Int,4)};
           
            parameters[0].Value = model.MovieName;
            parameters[1].Value = model.MovieURL;
            parameters[2].Value = model.Episode;
            parameters[3].Value = model.Site;
            parameters[4].Value = model.Id;

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
        public bool Delete(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_Source ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
			};
            parameters[0].Value = Id;

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
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string Idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_Source ");
            strSql.Append(" where Id in (" + Idlist + ")  ");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
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
        /// 得到一个对象实体
        /// </summary>
        public E_Source GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 Id,MovieId,MovieName,MovieURL,Episode,Site from T_Source ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
			};
            parameters[0].Value = Id;

            E_Source model = new E_Source();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public E_Source GetModelByMovieId(int MovieId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 Id,MovieId,MovieName,MovieURL,Episode,Site from T_Source ");
            strSql.Append(" where MovieId=@MovieId");
            SqlParameter[] parameters = {
					new SqlParameter("@MovieId", SqlDbType.Int,4)
			};
            parameters[0].Value = MovieId;

            E_Source model = new E_Source();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public E_Source DataRowToModel(DataRow row)
        {
            E_Source model = new E_Source();
            if (row != null)
            {
                if (row["Id"] != null && row["Id"].ToString() != "")
                {
                    model.Id = int.Parse(row["Id"].ToString());
                }
                if (row["MovieId"] != null && row["MovieId"].ToString() != "")
                {
                    model.MovieId = int.Parse(row["MovieId"].ToString());
                }
                if (row["MovieName"] != null)
                {
                    model.MovieName = row["MovieName"].ToString();
                }
                if (row["MovieURL"] != null)
                {
                    model.MovieURL = row["MovieURL"].ToString();
                }
                if (row["Episode"] != null && row["Episode"].ToString() != "")
                {
                    model.Episode = int.Parse(row["Episode"].ToString());
                }
                if (row["Site"] != null)
                {
                    model.Site = row["Site"].ToString();
                }
            }
            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,MovieId,MovieName,MovieURL,Episode,Site ");
            strSql.Append(" FROM T_Source ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" Id,MovieId,MovieName,MovieURL,Episode,Site ");
            strSql.Append(" FROM T_Source ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM T_Source ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
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
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.Id desc");
            }
            strSql.Append(")AS Row, T.*  from T_Source T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /*
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@tblName", SqlDbType.VarChar, 255),
                    new SqlParameter("@fldName", SqlDbType.VarChar, 255),
                    new SqlParameter("@PageSize", SqlDbType.Int),
                    new SqlParameter("@PageIndex", SqlDbType.Int),
                    new SqlParameter("@IsReCount", SqlDbType.Bit),
                    new SqlParameter("@OrderType", SqlDbType.Bit),
                    new SqlParameter("@strWhere", SqlDbType.VarChar,1000),
                    };
            parameters[0].Value = "T_Source";
            parameters[1].Value = "Id";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  BasicMethod
        #region  ExtensionMethod

        #endregion  ExtensionMethod
    }
}
