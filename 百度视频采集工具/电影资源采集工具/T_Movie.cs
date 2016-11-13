using System;
namespace Maticsoft.Model
{
	/// <summary>
	/// T_Movie:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class T_Movie
	{
		public T_Movie()
		{}
		#region Model
		private int _movieid;
		private string _moviename;
		private string _director;
		private string _actor;
		private string _type;
		private string _area;
		private string _length;
		private string _anothername;
		private decimal? _dbscore;
		private decimal? _imdbscore;
		private string _imagepath;
		private string _dburl;
		private string _imdburl;
		private string _intro;
		private int? _hit;
		private string _releasetime;
		private int? _year;
		private DateTime? _updatetime;
		private int? _channelid;
		private string _sourceid;
		private bool _isfinish;
		/// <summary>
		/// 
		/// </summary>
		public int MovieId
		{
			set{ _movieid=value;}
			get{return _movieid;}
		}
		/// <summary>
		/// 电影名称
		/// </summary>
		public string MovieName
		{
			set{ _moviename=value;}
			get{return _moviename;}
		}
		/// <summary>
		/// 导演
		/// </summary>
		public string Director
		{
			set{ _director=value;}
			get{return _director;}
		}
		/// <summary>
		/// 演员
		/// </summary>
		public string Actor
		{
			set{ _actor=value;}
			get{return _actor;}
		}
		/// <summary>
		/// 类型
		/// </summary>
		public string Type
		{
			set{ _type=value;}
			get{return _type;}
		}
		/// <summary>
		/// 地区
		/// </summary>
		public string Area
		{
			set{ _area=value;}
			get{return _area;}
		}
		/// <summary>
		/// 片长
		/// </summary>
		public string Length
		{
			set{ _length=value;}
			get{return _length;}
		}
		/// <summary>
		/// 别名
		/// </summary>
		public string AnotherName
		{
			set{ _anothername=value;}
			get{return _anothername;}
		}
		/// <summary>
		/// 豆瓣评分
		/// </summary>
		public decimal? DBScore
		{
			set{ _dbscore=value;}
			get{return _dbscore;}
		}
		/// <summary>
		/// IMDB评分
		/// </summary>
		public decimal? IMDBScore
		{
			set{ _imdbscore=value;}
			get{return _imdbscore;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ImagePath
		{
			set{ _imagepath=value;}
			get{return _imagepath;}
		}
		/// <summary>
		/// 豆瓣链接
		/// </summary>
		public string DBUrl
		{
			set{ _dburl=value;}
			get{return _dburl;}
		}
		/// <summary>
		/// IMDB链接
		/// </summary>
		public string IMDBUrl
		{
			set{ _imdburl=value;}
			get{return _imdburl;}
		}
		/// <summary>
		/// 简介
		/// </summary>
		public string Intro
		{
			set{ _intro=value;}
			get{return _intro;}
		}
		/// <summary>
		/// 点击次数
		/// </summary>
		public int? Hit
		{
			set{ _hit=value;}
			get{return _hit;}
		}
		/// <summary>
		/// 上映时间
		/// </summary>
		public string ReleaseTime
		{
			set{ _releasetime=value;}
			get{return _releasetime;}
		}
		/// <summary>
		/// 年份
		/// </summary>
		public int? Year
		{
			set{ _year=value;}
			get{return _year;}
		}
		/// <summary>
		/// 更新时间
		/// </summary>
		public DateTime? UpdateTime
		{
			set{ _updatetime=value;}
			get{return _updatetime;}
		}
		/// <summary>
		/// 1电影，2电视剧
		/// </summary>
		public int? ChannelID
		{
			set{ _channelid=value;}
			get{return _channelid;}
		}
		/// <summary>
		/// 来源ID
		/// </summary>
		public string SourceID
		{
			set{ _sourceid=value;}
			get{return _sourceid;}
		}
		/// <summary>
		/// 是否完结
		/// </summary>
		public bool IsFinish
		{
			set{ _isfinish=value;}
			get{return _isfinish;}
		}
		#endregion Model

	}
}

