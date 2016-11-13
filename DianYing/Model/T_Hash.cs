using System;
namespace DianYing.Model
{
	/// <summary>
	/// T_Hash:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class T_Hash
	{
		public T_Hash()
		{}
		#region Model
		private int _id;
		private int? _movieid;
		private string _name;
		private string _url;
		/// <summary>
		/// 
		/// </summary>
		public int Id
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
		public string Name
		{
			set{ _name=value;}
			get{return _name;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string URL
		{
			set{ _url=value;}
			get{return _url;}
		}
		#endregion Model

	}
}

