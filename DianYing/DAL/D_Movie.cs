using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.DBUtility;
using System.Collections.Generic;
using DianYing.Model;//Please add references
namespace DianYing.DAL
{
	/// <summary>
	/// 数据访问类:T_Movie
	/// </summary>
	public partial class D_Movie
	{
		public D_Movie()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("MovieId", "T_Movie"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int MovieId)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from T_Movie");
			strSql.Append(" where MovieId=@MovieId");
			SqlParameter[] parameters = {
					new SqlParameter("@MovieId", SqlDbType.Int,4)
			};
			parameters[0].Value = MovieId;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(E_Movie model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_Movie(");
            strSql.Append("MovieName,Director,Actor,Type,Area,Length,AnotherName,DBScore,IMDBScore,ImagePath,BigImagePath,DBUrl,IMDBUrl,Intro,Hit,ReleaseTime,Year,UpdateTime,ChannelID,SourceID,IsFinish)");
            strSql.Append(" values (");
            strSql.Append("@MovieName,@Director,@Actor,@Type,@Area,@Length,@AnotherName,@DBScore,@IMDBScore,@ImagePath,@BigImagePath,@DBUrl,@IMDBUrl,@Intro,@Hit,@ReleaseTime,@Year,@UpdateTime,@ChannelID,@SourceID,@IsFinish)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@MovieName", SqlDbType.NVarChar,150),
					new SqlParameter("@Director", SqlDbType.NVarChar,50),
					new SqlParameter("@Actor", SqlDbType.NVarChar,150),
					new SqlParameter("@Type", SqlDbType.NVarChar,50),
					new SqlParameter("@Area", SqlDbType.NVarChar,50),
					new SqlParameter("@Length", SqlDbType.NVarChar,50),
					new SqlParameter("@AnotherName", SqlDbType.NVarChar,150),
					new SqlParameter("@DBScore", SqlDbType.Float,8),
					new SqlParameter("@IMDBScore", SqlDbType.Float,8),
					new SqlParameter("@ImagePath", SqlDbType.NVarChar,150),
					new SqlParameter("@BigImagePath", SqlDbType.NVarChar,150),
					new SqlParameter("@DBUrl", SqlDbType.NVarChar,150),
					new SqlParameter("@IMDBUrl", SqlDbType.NVarChar,150),
					new SqlParameter("@Intro", SqlDbType.NVarChar,2000),
					new SqlParameter("@Hit", SqlDbType.Int,4),
					new SqlParameter("@ReleaseTime", SqlDbType.NVarChar,50),
					new SqlParameter("@Year", SqlDbType.Int,4),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@ChannelID", SqlDbType.Int,4),
					new SqlParameter("@SourceID", SqlDbType.NVarChar,50),
					new SqlParameter("@IsFinish", SqlDbType.Bit,1)};
            parameters[0].Value = model.MovieName;
            parameters[1].Value = model.Director;
            parameters[2].Value = model.Actor;
            parameters[3].Value = model.Type;
            parameters[4].Value = model.Area;
            parameters[5].Value = model.Length;
            parameters[6].Value = model.AnotherName;
            parameters[7].Value = model.DBScore;
            parameters[8].Value = model.IMDBScore;
            parameters[9].Value = model.ImagePath;
            parameters[10].Value = model.BigImagePath;
            parameters[11].Value = model.DBUrl;
            parameters[12].Value = model.IMDBUrl;
            parameters[13].Value = model.Intro;
            parameters[14].Value = model.Hit;
            parameters[15].Value = model.ReleaseTime;
            parameters[16].Value = model.Year;
            parameters[17].Value = model.UpdateTime;
            parameters[18].Value = model.ChannelID;
            parameters[19].Value = model.SourceID;
            parameters[20].Value = model.IsFinish;

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
		public bool Update(DianYing.Model.E_Movie model)
		{
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_Movie set ");
            strSql.Append("MovieName=@MovieName,");
            strSql.Append("Director=@Director,");
            strSql.Append("Actor=@Actor,");
            strSql.Append("Type=@Type,");
            strSql.Append("Area=@Area,");
            strSql.Append("Length=@Length,");
            strSql.Append("AnotherName=@AnotherName,");
            strSql.Append("DBScore=@DBScore,");
            strSql.Append("IMDBScore=@IMDBScore,");
            strSql.Append("ImagePath=@ImagePath,");
            strSql.Append("BigImagePath=@BigImagePath,");
            strSql.Append("DBUrl=@DBUrl,");
            strSql.Append("IMDBUrl=@IMDBUrl,");
            strSql.Append("Intro=@Intro,");
            strSql.Append("Hit=@Hit,");
            strSql.Append("ReleaseTime=@ReleaseTime,");
            strSql.Append("Year=@Year,");
            strSql.Append("UpdateTime=@UpdateTime,");
            strSql.Append("ChannelID=@ChannelID,");
            strSql.Append("SourceID=@SourceID,");
            strSql.Append("IsFinish=@IsFinish");
            strSql.Append(" where MovieId=@MovieId");
            SqlParameter[] parameters = {
					new SqlParameter("@MovieName", SqlDbType.NVarChar,150),
					new SqlParameter("@Director", SqlDbType.NVarChar,50),
					new SqlParameter("@Actor", SqlDbType.NVarChar,150),
					new SqlParameter("@Type", SqlDbType.NVarChar,50),
					new SqlParameter("@Area", SqlDbType.NVarChar,50),
					new SqlParameter("@Length", SqlDbType.NVarChar,50),
					new SqlParameter("@AnotherName", SqlDbType.NVarChar,150),
					new SqlParameter("@DBScore", SqlDbType.Float,8),
					new SqlParameter("@IMDBScore", SqlDbType.Float,8),
					new SqlParameter("@ImagePath", SqlDbType.NVarChar,150),
					new SqlParameter("@BigImagePath", SqlDbType.NVarChar,150),
					new SqlParameter("@DBUrl", SqlDbType.NVarChar,150),
					new SqlParameter("@IMDBUrl", SqlDbType.NVarChar,150),
					new SqlParameter("@Intro", SqlDbType.NVarChar,2000),
					new SqlParameter("@Hit", SqlDbType.Int,4),
					new SqlParameter("@ReleaseTime", SqlDbType.NVarChar,50),
					new SqlParameter("@Year", SqlDbType.Int,4),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@ChannelID", SqlDbType.Int,4),
					new SqlParameter("@SourceID", SqlDbType.NVarChar,50),
					new SqlParameter("@IsFinish", SqlDbType.Bit,1),
					new SqlParameter("@MovieId", SqlDbType.Int,4)};
            parameters[0].Value = model.MovieName;
            parameters[1].Value = model.Director;
            parameters[2].Value = model.Actor;
            parameters[3].Value = model.Type;
            parameters[4].Value = model.Area;
            parameters[5].Value = model.Length;
            parameters[6].Value = model.AnotherName;
            parameters[7].Value = model.DBScore;
            parameters[8].Value = model.IMDBScore;
            parameters[9].Value = model.ImagePath;
            parameters[10].Value = model.BigImagePath;
            parameters[11].Value = model.DBUrl;
            parameters[12].Value = model.IMDBUrl;
            parameters[13].Value = model.Intro;
            parameters[14].Value = model.Hit;
            parameters[15].Value = model.ReleaseTime;
            parameters[16].Value = model.Year;
            parameters[17].Value = model.UpdateTime;
            parameters[18].Value = model.ChannelID;
            parameters[19].Value = model.SourceID;
            parameters[20].Value = model.IsFinish;
            parameters[21].Value = model.MovieId;

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
		public bool Delete(int MovieId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from T_Movie ");
			strSql.Append(" where MovieId=@MovieId");
			SqlParameter[] parameters = {
					new SqlParameter("@MovieId", SqlDbType.Int,4)
			};
			parameters[0].Value = MovieId;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
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
		public bool DeleteList(string MovieIdlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from T_Movie ");
			strSql.Append(" where MovieId in ("+MovieIdlist + ")  ");
			int rows=DbHelperSQL.ExecuteSql(strSql.ToString());
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
		public DianYing.Model.E_Movie GetModel(int MovieId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 MovieId,MovieName,Director,Actor,Type,Area,Length,AnotherName,DBScore,IMDBScore,ImagePath,DBUrl,IMDBUrl,Intro,Hit,ReleaseTime,Year,UpdateTime,ChannelID from T_Movie ");
			strSql.Append(" where MovieId=@MovieId");
			SqlParameter[] parameters = {
					new SqlParameter("@MovieId", SqlDbType.Int,4)
			};
			parameters[0].Value = MovieId;

			DianYing.Model.E_Movie model=new DianYing.Model.E_Movie();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
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
		public DianYing.Model.E_Movie DataRowToModel(DataRow row)
		{
			DianYing.Model.E_Movie model=new DianYing.Model.E_Movie();
			if (row != null)
			{
				if(row["MovieId"]!=null && row["MovieId"].ToString()!="")
				{
					model.MovieId=int.Parse(row["MovieId"].ToString());
				}
				if(row["MovieName"]!=null)
				{
					model.MovieName=row["MovieName"].ToString();
				}
				if(row["Director"]!=null)
				{
					model.Director=row["Director"].ToString();
				}
				if(row["Actor"]!=null)
				{
					model.Actor=row["Actor"].ToString();
				}
				if(row["Type"]!=null)
				{
					model.Type=row["Type"].ToString();
				}
				if(row["Area"]!=null)
				{
					model.Area=row["Area"].ToString();
				}
				if(row["Length"]!=null)
				{
					model.Length=row["Length"].ToString();
				}
				if(row["AnotherName"]!=null)
				{
					model.AnotherName=row["AnotherName"].ToString();
				}
				if(row["DBScore"]!=null && row["DBScore"].ToString()!="")
				{
					model.DBScore=decimal.Parse(row["DBScore"].ToString());
				}
				if(row["IMDBScore"]!=null && row["IMDBScore"].ToString()!="")
				{
					model.IMDBScore=decimal.Parse(row["IMDBScore"].ToString());
				}
				if(row["ImagePath"]!=null)
				{
					model.ImagePath=row["ImagePath"].ToString();
				}
				if(row["DBUrl"]!=null)
				{
					model.DBUrl=row["DBUrl"].ToString();
				}
				if(row["IMDBUrl"]!=null)
				{
					model.IMDBUrl=row["IMDBUrl"].ToString();
				}
				if(row["Intro"]!=null)
				{
					model.Intro=row["Intro"].ToString();
				}
				if(row["Hit"]!=null && row["Hit"].ToString()!="")
				{
					model.Hit=int.Parse(row["Hit"].ToString());
				}
				if(row["ReleaseTime"]!=null)
				{
					model.ReleaseTime=row["ReleaseTime"].ToString();
				}
				if(row["Year"]!=null && row["Year"].ToString()!="")
				{
					model.Year=int.Parse(row["Year"].ToString());
				}
				if(row["UpdateTime"]!=null && row["UpdateTime"].ToString()!="")
				{
					model.UpdateTime=DateTime.Parse(row["UpdateTime"].ToString());
				}
				if(row["ChannelID"]!=null && row["ChannelID"].ToString()!="")
				{
					model.ChannelID=int.Parse(row["ChannelID"].ToString());
				}
			}
			return model;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select MovieId,MovieName,Director,Actor,Type,Area,Length,AnotherName,DBScore,IMDBScore,ImagePath,DBUrl,IMDBUrl,Intro,Hit,ReleaseTime,Year,UpdateTime,ChannelID ");
			strSql.Append(" FROM T_Movie ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" MovieId,MovieName,Director,Actor,Type,Area,Length,AnotherName,DBScore,IMDBScore,ImagePath,DBUrl,IMDBUrl,Intro,Hit,ReleaseTime,Year,UpdateTime,ChannelID ");
			strSql.Append(" FROM T_Movie ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM T_Movie ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
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
			StringBuilder strSql=new StringBuilder();
			strSql.Append("SELECT * FROM ( ");
			strSql.Append(" SELECT ROW_NUMBER() OVER (");
			if (!string.IsNullOrEmpty(orderby.Trim()))
			{
				strSql.Append("order by T." + orderby );
			}
			else
			{
				strSql.Append("order by T.MovieId desc");
			}
			strSql.Append(")AS Row, T.*  from T_Movie T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);

			return DbHelperSQL.Query(strSql.ToString());
		}


		#endregion  BasicMethod
		#region  ExtensionMethod
        /// <summary>
        /// 获取电影列表
        /// </summary>
        /// <param name="eSoft"></param>
        /// <returns></returns>
        public List<E_Movie> SelectMovieList(E_Movie eMovie, ref int total)
        {

            List<Model.E_Movie> list = new List<Model.E_Movie>();
            StringBuilder sql = new StringBuilder();
            string sqlStr= @" select  ROW_NUMBER() OVER ( ORDER BY {2} desc ) AS RID,
                                m.[MovieId]
                                  ,[MovieName]
                                  ,[Director]
                                  ,[Actor]
                                  ,[Type]
                                  ,[Area]
                                  ,[Length]
                                  ,[AnotherName]
                                  ,[DBScore]
                                  ,[IMDBScore]
                                  ,[ImagePath]
                                  ,[DBUrl]
                                  ,[IMDBUrl]
                                  ,[Intro]
                                  ,[Hit]
                                  ,[ReleaseTime]
                                  ,[Year]
                                  ,[UpdateTime]
                                  ,[ChannelID]
                                  ,IsFinish
                                 {0}
                              FROM [dbo].[T_Movie] m {1} where 1=1";


        
            List<SqlParameter> para = new List<SqlParameter>();
            if (eMovie.UserId > 0)
            {
             sqlStr=   string.Format(sqlStr, ",i.IsLike ,i.IsPlan,i.IsRead", "left join dbo.T_Identify as i on i.MovieId=m.MovieId and i.UserId=@UserId",eMovie.OrderBy);
                para.Add(new SqlParameter("@UserId", eMovie.UserId));
            }
            else {
                sqlStr = string.Format(sqlStr, " ", " ",eMovie.OrderBy);
            }
           
            sql.Append(sqlStr);
            if (eMovie.ChannelID != 0)
            {
                sql.Append(" and ChannelID=@ChannelID");
                para.Add(new SqlParameter("@ChannelID", eMovie.ChannelID));
            }

            if (!string.IsNullOrEmpty(eMovie.OrderBy))
            {
                para.Add(new SqlParameter("@Order", eMovie.OrderBy));
            }
            else
            {
                para.Add(new SqlParameter("@Order", eMovie.OrderBy));
            }

            if (eMovie.Year != 0)
            {
                if (eMovie.Year < 100)
                {
                    sql.Append(" and  Year like '%'+cast(@Year as varchar)+'' ");
                }
                else
                {
                    sql.Append(" and Year=@Year");
                }

                para.Add(new SqlParameter("@Year", eMovie.Year));
            }
            if (!string.IsNullOrEmpty(eMovie.Area))
            {
                sql.Append(" and Area like '%'+@Area +'%'");
                para.Add(new SqlParameter("@Area", eMovie.Area));
            }
            if (!string.IsNullOrEmpty(eMovie.Type))
            {
                sql.Append(" and Type like '%'+@Type +'%'");
                para.Add(new SqlParameter("@Type", eMovie.Type));
            }
            if (!string.IsNullOrEmpty(eMovie.MovieName))
            {
                sql.Append(" and MovieName like '%'+@MovieName +'%'");
                para.Add(new SqlParameter("@MovieName", eMovie.MovieName));
            }
            if (!string.IsNullOrEmpty(eMovie.Director))
            {
                sql.Append(" and Director =@Director ");
                para.Add(new SqlParameter("@Director", eMovie.Director));
            }
            if (!string.IsNullOrEmpty(eMovie.Actor))
            {
                sql.Append(" and Actor like '%'+@Actor +'%'");
                para.Add(new SqlParameter("@Actor", eMovie.Actor));
            }
            if (!string.IsNullOrEmpty(eMovie.BigImagePath))
            {
                sql.Append(" and  BigImagePath is not null and BigImagePath!=''");
            }
            string PageCountSqlstr = "SELECT COUNT(1) as RowsCount FROM (" + sql + ") AS CountList";
            var ObjCount = DbHelperSQL.GetSingle(PageCountSqlstr, para.ToArray());
            int RowCount = Convert.ToInt32(ObjCount);
            total = RowCount;
            int PageCount = RowCount / eMovie.PageSize;
            if (RowCount % eMovie.PageSize > 0)
            {
                PageCount += 1;
            }

            if (eMovie.CurrentPage >= PageCount)
            {
                eMovie.CurrentPage = PageCount;
            }
            string PageSqlStr = "select * from ( " + sql + " ) as Temp_PageData where Temp_PageData.RID BETWEEN {0} AND {1}";
            PageSqlStr = string.Format(PageSqlStr, (eMovie.PageSize * (eMovie.CurrentPage - 1) + 1).ToString(), (eMovie.PageSize * eMovie.CurrentPage).ToString());

            using (SqlDataReader reader = DbHelperSQL.ExecuteReader(PageSqlStr, para.ToArray()))
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Model.E_Movie _eMovie = new Model.E_Movie();
                        _eMovie.MovieId = Convert.ToInt32(reader["MovieId"]);
                        _eMovie.Actor = reader["Actor"].ToString();
                        _eMovie.ImagePath = reader["ImagePath"].ToString();
                        _eMovie.MovieName = reader["MovieName"].ToString();
                        _eMovie.Year = Convert.ToInt32(reader["Year"]);
                        _eMovie.Type = reader["Type"].ToString();
                        _eMovie.DBScore = Convert.ToDecimal(reader["DBScore"]);
                        _eMovie.IMDBScore = Convert.ToDecimal(reader["IMDBScore"]);
                        _eMovie.UpdateTime = Convert.ToDateTime(reader["UpdateTime"]);
                        _eMovie.IsFinish = Convert.ToBoolean(reader["IsFinish"]);
                        if (eMovie.UserId > 0)
                        {
                            _eMovie.IsLike = reader["IsLike"] == DBNull.Value ? false : Convert.ToBoolean(reader["IsLike"]);
                            _eMovie.IsPlan = reader["IsPlan"] == DBNull.Value ? false : Convert.ToBoolean(reader["IsPlan"]);
                            _eMovie.IsRead = reader["IsRead"] == DBNull.Value ? false : Convert.ToBoolean(reader["IsRead"]);
                        }
                        list.Add(_eMovie);
                    }

                }
            }
            return list;
        }

        /// <summary>
        /// 获取推荐电影列表
        /// </summary>
        /// <param name="eSoft"></param>
        /// <returns></returns>
        public List<int> SelectHomeMovieList()
        {

           // List<Model.E_Movie> list = new List<Model.E_Movie>();
            List<int> list = new List<int>();
            StringBuilder sql = new StringBuilder();
            string sqlStr = @" select 
                                    [MovieId]
                                  ,[MovieName]
                                  ,[Director]
                                  ,[Actor]
                                  ,[Type]
                                  ,[Area]
                                  ,[Length]
                                  ,[AnotherName]
                                  ,[DBScore]
                                  ,[IMDBScore]
                                  ,[ImagePath]
                                  ,[DBUrl]
                                  ,[IMDBUrl]
                                  ,[Intro]
                                  ,[Hit]
                                  ,[ReleaseTime]
                                  ,[Year]
                                  ,[UpdateTime]
                                  ,[ChannelID]
                                  ,IsFinish
                                 
                              FROM [dbo].[T_Movie]  where   BigImagePath is not null and BigImagePath!=''";



            List<SqlParameter> para = new List<SqlParameter>();

             using (SqlDataReader reader = DbHelperSQL.ExecuteReader(sqlStr, para.ToArray()))
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                       // Model.E_Movie _eMovie = new Model.E_Movie();
                      //  _eMovie.MovieId = Convert.ToInt32(reader["MovieId"]);
                        //_eMovie.Actor = reader["Actor"].ToString();
                        //_eMovie.ImagePath = reader["ImagePath"].ToString();
                        //_eMovie.MovieName = reader["MovieName"].ToString();
                        //_eMovie.Year = Convert.ToInt32(reader["Year"]);
                        //_eMovie.Type = reader["Type"].ToString();
                        //_eMovie.DBScore = Convert.ToDecimal(reader["DBScore"]);
                        //_eMovie.IMDBScore = Convert.ToDecimal(reader["IMDBScore"]);
                        //_eMovie.UpdateTime = Convert.ToDateTime(reader["UpdateTime"]);
                        //_eMovie.IsFinish = Convert.ToBoolean(reader["IsFinish"]);
                
                        list.Add(Convert.ToInt32(reader["MovieId"]));
                    }

                }
            }
            return list;
        }

        /// <summary>
        /// 根据ID获取电影详情
        /// </summary>
        /// <param name="movieId"></param>
        /// <returns></returns>
        public E_Movie SelectMovie(E_Movie eMovie)
        {
            string strSql = " select m.MovieId,MovieName,Director,Actor,Type,Area,Length,AnotherName,DBScore,IMDBScore,ImagePath,BigImagePath,DBUrl,IMDBUrl,Intro,Hit,ReleaseTime,Year,UpdateTime,ChannelID,SourceID,IsFinish {0} FROM T_Movie m {1} where m.MovieId=@MovieId";
            Model.E_Movie _eMovie = new Model.E_Movie();
            List<SqlParameter> list = new List<SqlParameter>();
            if (eMovie.UserId > 0) {

               strSql= string.Format(strSql, ",i.IsLike,i.IsPlan,i.IsRead", "left join dbo.T_Identify as i on i.MovieId=m.MovieId and i.UserId=@UserId");
                list.Add(new SqlParameter("@UserId", eMovie.UserId));
            }
            else
            {

                strSql = string.Format(strSql, " ", " ");
            }
            list.Add(new SqlParameter("@MovieId", eMovie.MovieId));

            using (SqlDataReader reader = DbHelperSQL.ExecuteReader(strSql, list.ToArray()))
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                      
                        _eMovie.MovieId = Convert.ToInt32(reader["MovieId"]);
                        _eMovie.MovieName = reader["MovieName"].ToString();
                        _eMovie.Director = reader["Director"].ToString();
                        _eMovie.Actor = reader["Actor"].ToString();
                        _eMovie.Type = reader["Type"].ToString();
                        _eMovie.Area = reader["Area"].ToString();
                        _eMovie.Length = reader["Length"].ToString();
                        _eMovie.AnotherName = reader["AnotherName"].ToString();
                        _eMovie.DBScore = Convert.ToDecimal(reader["DBScore"]);
                        _eMovie.IMDBScore = Convert.ToDecimal(reader["IMDBScore"]);
                        _eMovie.ImagePath = reader["ImagePath"].ToString();
                        _eMovie.BigImagePath = reader["BigImagePath"].ToString();
                        _eMovie.DBUrl = reader["DBUrl"].ToString();
                        _eMovie.IMDBUrl = reader["IMDBUrl"].ToString();
                        _eMovie.Intro = reader["Intro"].ToString();
                        _eMovie.Hit = Convert.ToInt32(reader["Hit"]);
                        _eMovie.ReleaseTime = reader["ReleaseTime"].ToString();
                        _eMovie.Year = Convert.ToInt32(reader["Year"]);
                        _eMovie.ChannelID = Convert.ToInt32(reader["ChannelID"]);
                      
                        _eMovie.UpdateTime = Convert.ToDateTime(reader["UpdateTime"]);
                        _eMovie.IsFinish = Convert.ToBoolean(reader["IsFinish"]);
                        _eMovie.SourceID = reader["SourceID"].ToString();
                        if (eMovie.UserId > 0)
                        {
                            _eMovie.IsLike = reader["IsLike"] == DBNull.Value ? false : Convert.ToBoolean(reader["IsLike"]);
                            _eMovie.IsPlan = reader["IsPlan"] == DBNull.Value ? false : Convert.ToBoolean(reader["IsPlan"]);
                            _eMovie.IsRead = reader["IsRead"] == DBNull.Value ? false : Convert.ToBoolean(reader["IsRead"]);
                        }
                      
                    }

                }
            }
            return _eMovie;
        }
        /// <summary>
        /// 根据表名获取对应的数据
        /// </summary>
        /// <param name="movieId"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public List<E_Source> SelectURLList(int movieId, string tableName)
        {
         
            string strSql = " select Id,Name,URL FROM "+tableName+" where MovieId=@MovieId";
            List<E_Source> list = new List<E_Source>();
            using (SqlDataReader reader = DbHelperSQL.ExecuteReader(strSql, new SqlParameter("@MovieId", movieId)))
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        E_Source e = new E_Source();
                        e.Id = Convert.ToInt32(reader["Id"]);
                        e.MovieName = reader["Name"].ToString();
                        e.MovieURL = reader["URL"].ToString();
                        list.Add(e);

                    }

                }
            }
            return list;

        }

        /// <summary>
        /// 根据表名获取对应的数据
        /// </summary>
        /// <param name="movieId"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public DataTable SelectSourceList(int movieId)
        {

            string strSql = " select Id,MovieName,MovieURL,Episode,Site  FROM T_Source where MovieId=@MovieId order by Episode";

            DataSet set = DbHelperSQL.Query(strSql, new SqlParameter("@MovieId", movieId));

            if (set != null)
            {
                return set.Tables[0];
            }
            else
            {
                return null;
            }



        }
        /// <summary>
        /// 根据movie获取对应的数据(首页)
        /// </summary>
        /// <param name="movieId"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public E_Movie SelectBigImg(E_Movie eMovie)
        {

            string strSql = @" SELECT 
                              b.MovieId
                              ,[MovieName]
                              ,[Actor]
                              ,[Type]
                              ,[DBScore]
                              ,[IMDBScore]
                              ,[Intro]
	                          ,b.Img as [ImagePath]
                              ,b.Source,b.SourceUrl
                               {0}
                               FROM T_BigImg as  b  left join [dbo].[T_Movie] as m on   b.MovieId=m.MovieId 
                               {1}
                              where b.IsUse=1 and b.MovieId =@MovieId  ";
            Model.E_Movie _eMovie = new Model.E_Movie();
            List<SqlParameter> list = new List<SqlParameter>();

            if (eMovie.UserId > 0)
            {

                strSql = string.Format(strSql, ",i.IsLike,i.IsPlan,i.IsRead", "left join dbo.T_Identify as i on i.MovieId=m.MovieId and i.UserId=@UserId");
                list.Add(new SqlParameter("@UserId", eMovie.UserId));
            }
            else {

                strSql = string.Format(strSql, " ", " ");
            }
            list.Add(new SqlParameter("@MovieId", eMovie.MovieId));
            using (SqlDataReader reader = DbHelperSQL.ExecuteReader(strSql, list.ToArray()))
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        _eMovie.MovieId = Convert.ToInt32(reader["MovieId"]);
                        _eMovie.MovieName = reader["MovieName"].ToString();
                   
                        _eMovie.Actor = reader["Actor"].ToString();
                        _eMovie.Type = reader["Type"].ToString();
                   
                        _eMovie.DBScore = Convert.ToDecimal(reader["DBScore"]);
                        _eMovie.IMDBScore = Convert.ToDecimal(reader["IMDBScore"]);
                        _eMovie.ImagePath = reader["ImagePath"].ToString();
                        _eMovie.Source = reader["Source"].ToString();
                        _eMovie.SourceUrl = reader["SourceUrl"].ToString();
                     
                        _eMovie.Intro = reader["Intro"].ToString();
                        if (eMovie.UserId > 0)
                        {
                            _eMovie.IsLike = reader["IsLike"] == DBNull.Value ? false : Convert.ToBoolean(reader["IsLike"]);
                            _eMovie.IsPlan = reader["IsPlan"] == DBNull.Value ? false : Convert.ToBoolean(reader["IsPlan"]);
                            _eMovie.IsRead = reader["IsRead"] == DBNull.Value ? false : Convert.ToBoolean(reader["IsRead"]);
                        }

                    }

                }
            }
            return _eMovie;

        }


        /// <summary>
        /// 获取site数组
        /// </summary>
        /// <param name="movieId"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public List<string> SelectSiteList(int movieId)
        {

            string strSql = " select Site FROM T_Source where MovieId=@MovieId group by Site";
            List<string> list = new List<string>();
            using (SqlDataReader reader = DbHelperSQL.ExecuteReader(strSql, new SqlParameter("@MovieId", movieId)))
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        list.Add(reader["Site"].ToString());

                    }

                }
            }
            return list;

        }


        /// <summary>
        /// 加入到首页
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public bool SetHome(E_Movie eMovie)
        {
            string sql = @"update T_Movie set BigImagePath=@BigImagePath  WHERE   MovieId=@MovieId";
            SqlParameter[] parameters = {
					new SqlParameter("@MovieId", eMovie.MovieId),
                    new SqlParameter("@BigImagePath", eMovie.BigImagePath)
					
                  
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

		#endregion  ExtensionMethod
	}
}

