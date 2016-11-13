using System;
namespace Maticsoft.Model
{
	/// <summary>
	/// T_Source:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class T_Source
	{
		public T_Source()
		{}
		#region Model
		private int? _id;
		private int? _movieid;
		private string _moviename;
		private string _movieurl;
		private int? _episode;
		private string _site;
		/// <summary>
		/// 
		/// </summary>
		public int? Id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? MovieId
		{
			set{ _movieid=value;}
			get{return _movieid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string MovieName
		{
			set{ _moviename=value;}
			get{return _moviename;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string MovieURL
		{
			set{ _movieurl=value;}
			get{return _movieurl;}
		}
		/// <summary>
		/// 第几集
		/// </summary>
		public int? Episode
		{
			set{ _episode=value;}
			get{return _episode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Site
		{
			set{ _site=value;}
			get{return _site;}
		}
		#endregion Model

	}
}

