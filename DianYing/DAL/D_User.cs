using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DianYing.Model;
using Maticsoft.DBUtility;
using System.Data.SqlClient;
using System.Data;

namespace DianYing.DAL
{
   public  class D_User
    {
       /// <summary>
       /// 新建用户
       /// </summary>
       /// <param name="eUser"></param>
       /// <returns></returns>
       public int Insert(E_User eUser)
       { 
         string sql= @"INSERT INTO [dbo].[T_User]
           ([UserName]
           ,[PassWord]
           ,[NickName]
           ,Type )
     VALUES
           (@UserName
           ,@PassWord
           ,@NickName
           ,@Type 
);select @@IDENTITY";
        SqlParameter[] parameters = {
					new SqlParameter("@UserName", eUser.UserName),
					new SqlParameter("@PassWord", eUser.PassWord),
                    new SqlParameter("@NickName", eUser.UserName),
                    new SqlParameter("@Type", 1)
				};


        object obj = DbHelperSQL.GetSingle(sql.ToString(), parameters);
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
       /// 是否存在
       /// </summary>
       /// <param name="e"></param>
       /// <returns></returns>
       public int IsExist(E_User eUser)
       {
           string sql = @"Select Id from T_User
   
 WHERE  UserName=@UserName and PassWord=@PassWord";
           SqlParameter[] parameters = {
					new SqlParameter("@UserName", eUser.UserName),
                    new SqlParameter("@PassWord", eUser.PassWord)

				};
           object obj= DbHelperSQL.GetSingle(sql, parameters);
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
       /// 是否存在相同的用户名
       /// </summary>
       /// <param name="e"></param>
       /// <returns></returns>
       public bool IsExistName(string eUser)
       {
           string sql = @"Select count(1) from T_User
   
 WHERE  UserName=@UserName ";
           SqlParameter[] parameters = {
					new SqlParameter("@UserName", eUser)
                  

				};
           return DbHelperSQL.Exists(sql, parameters);

       }

       /// <summary>
       /// 增加一条数据
       /// </summary>
       public int Add(E_User model)
       {
           StringBuilder strSql = new StringBuilder();
           strSql.Append("insert into T_User(");
           strSql.Append("UserName,PassWord,AccessToken,NickName,Figureurl,Figureurl_1,Figureurl_2,LastTime,Type)");
           strSql.Append(" values (");
           strSql.Append("@UserName,@PassWord,@AccessToken,@NickName,@Figureurl,@Figureurl_1,@Figureurl_2,@LastTime,@Type)");
           strSql.Append(";select @@IDENTITY");
           SqlParameter[] parameters = {
					new SqlParameter("@UserName", SqlDbType.NVarChar,50),
					new SqlParameter("@PassWord", SqlDbType.NVarChar,50),
					new SqlParameter("@AccessToken", SqlDbType.NVarChar,80),
					new SqlParameter("@NickName", SqlDbType.NVarChar,50),
					new SqlParameter("@Figureurl", SqlDbType.NVarChar,250),
					new SqlParameter("@Figureurl_1", SqlDbType.NVarChar,250),
					new SqlParameter("@Figureurl_2", SqlDbType.NVarChar,250),
					new SqlParameter("@LastTime", SqlDbType.NVarChar,50),
					new SqlParameter("@Type", SqlDbType.Int,4)};
           parameters[0].Value = model.UserName;
           parameters[1].Value = model.PassWord;
           parameters[2].Value = model.AccessToken;
           parameters[3].Value = model.NickName;
           parameters[4].Value = model.Figureurl;
           parameters[5].Value = model.Figureurl_1;
           parameters[6].Value = model.Figureurl_2;
           parameters[7].Value = model.LastTime;
           parameters[8].Value = model.Type;

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
       public bool Update(E_User model)
       {
           StringBuilder strSql = new StringBuilder();
           strSql.Append("update T_User set ");
          
           strSql.Append("PassWord=@PassWord,");
           strSql.Append("AccessToken=@AccessToken,");
           strSql.Append("NickName=@NickName,");
           strSql.Append("Figureurl=@Figureurl,");
           strSql.Append("Figureurl_1=@Figureurl_1,");
           strSql.Append("Figureurl_2=@Figureurl_2,");
           strSql.Append("LastTime=@LastTime,");
           strSql.Append("Type=@Type");
           strSql.Append(" where UserName=@UserName");
           SqlParameter[] parameters = {
					new SqlParameter("@UserName", SqlDbType.NVarChar,50),
					new SqlParameter("@PassWord", SqlDbType.NVarChar,50),
					new SqlParameter("@AccessToken", SqlDbType.NVarChar,80),
					new SqlParameter("@NickName", SqlDbType.NVarChar,50),
					new SqlParameter("@Figureurl", SqlDbType.NVarChar,250),
					new SqlParameter("@Figureurl_1", SqlDbType.NVarChar,250),
					new SqlParameter("@Figureurl_2", SqlDbType.NVarChar,250),
					new SqlParameter("@LastTime", SqlDbType.NVarChar,50),
					new SqlParameter("@Type", SqlDbType.Int,4)
				};
           parameters[0].Value = model.UserName;
           parameters[1].Value = model.PassWord;
           parameters[2].Value = model.AccessToken;
           parameters[3].Value = model.NickName;
           parameters[4].Value = model.Figureurl;
           parameters[5].Value = model.Figureurl_1;
           parameters[6].Value = model.Figureurl_2;
           parameters[7].Value = model.LastTime;
           parameters[8].Value = model.Type;
        

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
