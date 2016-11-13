using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace XKCommon.QQConnect
{
    /// <summary>
    /// 
    /// </summary>
    public class QQApiUrl
    {

        #region Authorization Code
        /// <summary>
        /// 获取Authorization Code
        /// </summary>
        public static string AuthorizationCode()
        {
            return AuthorizationCode("", "get_user_info,add_share");
        }
        /// <summary>
        /// 获取Authorization Code
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public static string AuthorizationCode(string state)
        {
            return AuthorizationCode(state, "get_user_info,add_share");
        }
        /// <summary>
        /// 获取Authorization Code
        /// </summary>
        /// <param name="state"></param>
        /// <param name="scope">传入使用的OpenAPI名称</param>
        /// <returns></returns>
        public static string AuthorizationCode(string state, string scope)
        {
            return string.Format("https://graph.qq.com/oauth2.0/authorize?response_type=code&client_id={0}&redirect_uri={1}&scope={2}&state={3}",
                QQConfig.AppID,
                HttpUtility.UrlEncode(QQConfig.RedirectUrl),
                scope,
                state);
        }
        #endregion

        #region Access Token
        /// <summary>
        /// 获取Access Token
        /// </summary>
        /// <param name="code">回调地址获取的Authorization Code</param>
        /// <returns></returns>
        public static string AccessToken(string code)
        {
            return AccessToken(code, "");
        }
        /// <summary>
        /// 获取Access Token
        /// </summary>
        /// <param name="code">回调地址获取的Authorization Code</param>
        /// <param name="state">client端的状态值。用于第三方应用防止CSRF攻击，成功授权后回调时会原样带回</param>
        /// <returns></returns>
        public static string AccessToken(string code, string state)
        {
            return string.Format("https://graph.qq.com/oauth2.0/token?grant_type=authorization_code&client_id={0}&client_secret={1}&code={2}&state={3}&redirect_uri={4}", QQConfig.AppID, QQConfig.AppKey, code, state, HttpUtility.UrlEncode(QQConfig.RedirectUrl));
        }
        #endregion
        /// <summary>
        /// 使用Access Token来获取用户的OpenID
        /// </summary>
        /// <param name="token">Access Token</param>
        /// <returns></returns>
        public static string OpenID(string token)
        {
            return string.Format("https://graph.qq.com/oauth2.0/me?access_token={0}", token);
        }
        /// <summary>
        /// 使用Access Token以及OpenID来获取用户数据
        /// </summary>
        /// <param name="token">Access Token</param>
        /// <param name="openid">OpenID</param>
        /// <returns></returns>
        public static string Get_User_Info(string token, string openid)
        {
            return string.Format("https://graph.qq.com/user/get_user_info?access_token={0}&oauth_consumer_key={1}&openid={2}", token, QQConfig.AppID, openid);
        }
    }
}
