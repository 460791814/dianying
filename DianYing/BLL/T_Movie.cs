using System;
using System.Data;
using System.Collections.Generic;
using Maticsoft.Common;
using DianYing.Model;
namespace DianYing.BLL
{
	/// <summary>
	/// T_Movie
	/// </summary>
	public partial class T_Movie
	{
		private readonly DianYing.DAL.D_Movie dal=new DianYing.DAL.D_Movie();
		public T_Movie()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
			return dal.GetMaxId();
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int MovieId)
		{
			return dal.Exists(MovieId);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(DianYing.Model.E_Movie model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(DianYing.Model.E_Movie model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int MovieId)
		{
			
			return dal.Delete(MovieId);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string MovieIdlist )
		{
			return dal.DeleteList(MovieIdlist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public DianYing.Model.E_Movie GetModel(int MovieId)
		{
			
			return dal.GetModel(MovieId);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public DianYing.Model.E_Movie GetModelByCache(int MovieId)
		{
			
			string CacheKey = "T_MovieModel-" + MovieId;
			object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(MovieId);
					if (objModel != null)
					{
						int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt("ModelCache");
						Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (DianYing.Model.E_Movie)objModel;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}
		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			return dal.GetList(Top,strWhere,filedOrder);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<DianYing.Model.E_Movie> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<DianYing.Model.E_Movie> DataTableToList(DataTable dt)
		{
			List<DianYing.Model.E_Movie> modelList = new List<DianYing.Model.E_Movie>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				DianYing.Model.E_Movie model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = dal.DataRowToModel(dt.Rows[n]);
					if (model != null)
					{
						modelList.Add(model);
					}
				}
			}
			return modelList;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetAllList()
		{
			return GetList("");
		}

		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			return dal.GetRecordCount(strWhere);
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
		{
			return dal.GetListByPage( strWhere,  orderby,  startIndex,  endIndex);
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		//public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		//{
			//return dal.GetList(PageSize,PageIndex,strWhere);
		//}

		#endregion  BasicMethod
		#region  ExtensionMethod
        /// <summary>
        /// 获取电影列表
        /// </summary>
        /// <param name="eSoft"></param>
        /// <returns></returns>
        public List<Model.E_Movie> SelectMovieList(E_Movie eMovie, ref int total)
        {
            return dal.SelectMovieList(eMovie, ref total);
        }
                /// <summary>
        /// 根据表名获取对应的数据
        /// </summary>
        /// <param name="movieId"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public List<E_Source> SelectURLList(int movieId, string tableName)
        {
            return dal.SelectURLList(movieId,tableName);
        }
                /// <summary>
        /// 根据表名获取对应的数据
        /// </summary>
        /// <param name="movieId"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public DataTable SelectSourceList(int movieId)
        {
            return dal.SelectSourceList(movieId);
        }
                /// <summary>
        /// 根据ID获取电影详情
        /// </summary>
        /// <param name="movieId"></param>
        /// <returns></returns>
        public E_Movie SelectMovie(E_Movie eMovie)
        {
            return dal.SelectMovie(eMovie);
        }
                /// <summary>
        /// 根据movie获取对应的数据(首页)
        /// </summary>
        /// <param name="movieId"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public E_Movie SelectBigImg(E_Movie eMovie)
        {
            return dal.SelectBigImg(eMovie);
        }
        
        /// <summary>
        /// 获取site数组
        /// </summary>
        /// <param name="movieId"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public List<string> SelectSiteList(int movieId)
        {
            return dal.SelectSiteList(movieId);
        }

                /// <summary>
        /// 加入到首页
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public bool SetHome(E_Movie eMovie)
        {
            return dal.SetHome(eMovie);
        }
                /// <summary>
        /// 获取推荐电影列表
        /// </summary>
        /// <param name="eSoft"></param>
        /// <returns></returns>
        public List<int> SelectHomeMovieList()
        {
            return dal.SelectHomeMovieList();
        }
		#endregion  ExtensionMethod
	}
}

