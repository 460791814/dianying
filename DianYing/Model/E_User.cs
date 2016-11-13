using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DianYing.Model
{
    /// <summary>
    /// T_User:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class E_User
    {
        public E_User()
        { }
        #region Model
        private int _id;
        private string _username;
        private string _password;
        private string _accesstoken;
        private string _nickname;
        private string _figureurl;
        private string _figureurl_1;
        private string _figureurl_2;
        private string _lasttime;
        private int? _type;
        /// <summary>
        /// 
        /// </summary>
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 用户名,openID,UID
        /// </summary>
        public string UserName
        {
            set { _username = value; }
            get { return _username; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PassWord
        {
            set { _password = value; }
            get { return _password; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string AccessToken
        {
            set { _accesstoken = value; }
            get { return _accesstoken; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string NickName
        {
            set { _nickname = value; }
            get { return _nickname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Figureurl
        {
            set { _figureurl = value; }
            get { return _figureurl; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Figureurl_1
        {
            set { _figureurl_1 = value; }
            get { return _figureurl_1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Figureurl_2
        {
            set { _figureurl_2 = value; }
            get { return _figureurl_2; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string LastTime
        {
            set { _lasttime = value; }
            get { return _lasttime; }
        }
        /// <summary>
        /// 1是普通，2是qq，3是新浪
        /// </summary>
        public int? Type
        {
            set { _type = value; }
            get { return _type; }
        }
        #endregion Model

    }
}
