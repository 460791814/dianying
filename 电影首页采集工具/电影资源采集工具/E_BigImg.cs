using System;
namespace DianYing.Model
{
	/// <summary>
	/// T_BigImg:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class E_BigImg
	{
		public E_BigImg()
		{}
		#region Model
		private int _id;
		private int? _movieid;
		private string _img;
		private bool _isuse;
		/// <summary>
		/// 
		/// </summary>
		public int Id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 电影ID
		/// </summary>
		public int? MovieId
		{
			set{ _movieid=value;}
			get{return _movieid;}
		}
		/// <summary>
		/// 720P图片
		/// </summary>
		public string Img
		{
			set{ _img=value;}
			get{return _img;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool IsUse
		{
			set{ _isuse=value;}
			get{return _isuse;}
		}
		#endregion Model
        public string Source
        {
            get;
            set;
        }
        public string SourceUrl
        {
            get;
            set;
        }
	}
}

