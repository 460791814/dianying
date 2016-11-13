using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DianYing.Model
{
    /// <summary>
    /// QQ用户信息类
    /// </summary>
    [Serializable]
    public class QQUser 
    {
        /// <summary>
        /// 
        /// </summary>
        public int userid { get; set; }
        /// <summary>
        /// OpenID
        /// </summary>
        public string openid { get; set; }
        /// <summary>
        /// accesstoken
        /// </summary>
        public string accesstoken { get; set; }
        /// <summary>
        /// 昵称 
        /// </summary>
        public string nickname { get; set; }
        /// <summary>
        /// 头像URL
        /// </summary>
        public string figureurl { get; set; }
        /// <summary>
        /// 返回状态
        /// </summary>
        public int ret { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary>
        public string msg { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string figureurl_1 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string figureurl_2 { get; set; }
    }
}
