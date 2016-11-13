using System;
namespace DianYing.Model
{
	/// <summary>
	/// T_Type:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class T_Type
	{
		public T_Type()
		{}
		#region Model
		private int _typeid;
		private string _typename;
		/// <summary>
		/// 
		/// </summary>
		public int TypeId
		{
			set{ _typeid=value;}
			get{return _typeid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string TypeName
		{
			set{ _typename=value;}
			get{return _typename;}
		}
		#endregion Model

	}
}

