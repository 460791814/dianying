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
    public class D_Identify
    {
        /// <summary>
        /// 插入一条数据
        /// 
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public bool Insert(E_Identify e)
        {
            string sql = @"INSERT INTO [DianYing].[dbo].[T_Identify]
           (
UserId
           ,[MovieId]
           ,[IsLike]
           ,[IsPlan]
           ,[IsRead])
     VALUES
           (
@UserId
           ,@MovieId 
           ,@IsLike
           ,@IsPlan
           ,@IsRead )";
            SqlParameter[] parameters = {
                                           	new SqlParameter("@UserId", e.UserId),
					new SqlParameter("@MovieId", e.MovieId),
					new SqlParameter("@IsLike", e.IsLike),
                    new SqlParameter("@IsPlan", e.IsPlan),
                    new SqlParameter("@IsRead", e.IsRead)
				};


            object obj = DbHelperSQL.GetSingle(sql.ToString(), parameters);
            if (obj == null)
            {
                return false;
            }
            else
            {
                return Convert.ToInt32(obj) > 0;
            }
        }
        /// <summary>
        /// 是否喜欢
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public bool UpdateLike(E_Identify e)
        {
            string sql = @"UPDATE [DianYing].[dbo].[T_Identify]
   SET [IsLike] = @IsLike
   
 WHERE  UserId=@UserId and MovieId=@MovieId";
            SqlParameter[] parameters = {
					new SqlParameter("@MovieId", e.MovieId),
                    new SqlParameter("@UserId", e.UserId),
					new SqlParameter("@IsLike", e.IsLike)
                  
				};
            int rows = DbHelperSQL.ExecuteSql(sql, parameters);
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
        /// 是否计划
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public bool UpdateRead(E_Identify e)
        {
            string sql = @"UPDATE [DianYing].[dbo].[T_Identify]
   SET [IsPlan] =@IsPlan,[IsRead] = @IsRead
   
 WHERE  UserId=@UserId and MovieId=@MovieId";
            SqlParameter[] parameters = {
					new SqlParameter("@MovieId", e.MovieId),
                    new SqlParameter("@UserId", e.UserId),
					new SqlParameter("@IsRead", e.IsRead),
                    new SqlParameter("@IsPlan", e.IsPlan)
                  
				};
            int rows = DbHelperSQL.ExecuteSql(sql, parameters);
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
        /// 是否存在
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public bool IsExist(E_Identify e)
        {
            string sql = @"Select count(1) from T_Identify
   
 WHERE  UserId=@UserId and MovieId=@MovieId";
            SqlParameter[] parameters = {
					new SqlParameter("@MovieId", e.MovieId),
                    new SqlParameter("@UserId", e.UserId),
				
                  
				};
            return DbHelperSQL.Exists(sql, parameters);

        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <param name="strWhere">like plan read</param>
        /// <returns></returns>
        public DataSet GetList(E_Identify e, ref int total)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT Id,  T_Identify.MovieId,T_Movie.[MovieName],T_Movie.Director,T_Movie.Actor,T_Movie.ReleaseTime FROM [dbo].[T_Identify] inner join T_Movie on T_Identify.[MovieId]=T_Movie.MovieId where  UserId=@UserId ");
            if (!string.IsNullOrEmpty(e.StrWhere))
            {
                strSql.Append(" and "+e.StrWhere+"=1 ");
            }

            string PageCountSqlstr = "SELECT COUNT(1) as RowsCount FROM (" + strSql.ToString() + ") AS CountList";
            var ObjCount = DbHelperSQL.GetSingle(PageCountSqlstr, new SqlParameter("@UserId",e.UserId));
            int RowCount = Convert.ToInt32(ObjCount);
            total = RowCount;
            int PageCount = RowCount / e.PageSize;
            if (RowCount % e.PageSize > 0)
            {
                PageCount += 1;
            }

            if (e.CurrentPage >= PageCount)
            {
                e.CurrentPage = PageCount;
            }
            string PageSqlStr = "select * from ( " + strSql.ToString() + " ) as Temp_PageData where Temp_PageData.RID BETWEEN {0} AND {1}";
            PageSqlStr = string.Format(PageSqlStr, (e.PageSize * (e.CurrentPage - 1) + 1).ToString(), (e.PageSize * e.CurrentPage).ToString());

            if (e.StrWhere.Trim() != "")
            {
                strSql.Append(" and  " + e.StrWhere + "=1");
            }
            return DbHelperSQL.Query(strSql.ToString(), new SqlParameter("@UserId", e.UserId));
        }
    }
}
