using System.Text;
using System.Net;
using System.Security.Cryptography;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Web.Script.Serialization;


namespace Enow.MAJU.Utility
{
    public class WeiboHelper
    {
        // <summary>
        /// 通过Authorization Code获取Access Token认证参数
        /// </summary>
        /// <param name="Authorization_code">Authorization Code</param>
        /// <returns></returns>
        public static string GetAccessToken(string Authorization_code)
        {
            string url = "https://api.weibo.com/oauth2/access_token?client_id=" + Utils.GetWeiboAppId() + "&client_secret=" + Utils.GetWeiboAppSecretKey() + "&grant_type="+Authorization_code+"";
            string returnValue = RequestUrl(url);

            return returnValue;
        }

        /// <summary>
        /// 通过AccessToken取得微博用户的Uid
        /// </summary>
        /// <param name="access_token"></param>
        /// <returns></returns>
        public static string GetUid(string access_token)
        {
            string url = "https://api.weibo.com/oauth2/get_token_info?access_token="+access_token+"";
            string returnValue = RequestUrl(url);
            if (!string.IsNullOrWhiteSpace(returnValue))
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                List<UserInfo> info = serializer.Deserialize<List<UserInfo>>(returnValue);
                returnValue = info[0].Uid;
            }
            return returnValue;
        }
        /// <summary>
        /// 请求指定url地址并返回结果
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string RequestUrl(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.MaximumAutomaticRedirections = 3;
            request.Timeout = 0x2710;
            Stream responseStream = ((HttpWebResponse)request.GetResponse()).GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            string str = reader.ReadToEnd();
            reader.Close();
            responseStream.Close();
            return str;
        }
    }

    public class UserInfo
    {
        public UserInfo() { }
        /// <summary>
        /// 微博授权用户的UID
        /// </summary>
        public string Uid { get; set; }
        /// <summary>
        /// AccessToken所属的应用appKey
        /// </summary>
        public string AppKey { get; set; }
        /// <summary>
        /// 用户授权的Scope权限
        /// </summary>
        public string Scope { get; set; }
        /// <summary>
        /// access_Token的创建时间
        /// </summary>
        public string Create_at { get; set; }
       /// <summary>
       /// access_token的剩余时间，单位为秒
       /// </summary>
        public string Expire_in { get; set; }
    }
}
