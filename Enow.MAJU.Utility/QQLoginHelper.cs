using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Net;
using System.Security.Cryptography;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Web.Script.Serialization;


namespace Enow.MAJU.Utility
{
    public class QQLoginHelper
    {

        /// <summary>
        /// 通过Authorization Code获取Access Token认证参数
        /// </summary>
        /// <param name="code">Authorization Code</param>
        /// <returns></returns>
        public static string GetAccessToken(string code)
        {
            string url = "https://graph.qq.com/oauth2.0/token?grant_type=authorization_code&client_id=" + Utils.GetQQAppId() + "&client_secret=" + Utils.GetQQAppKey() + "&code=" + code + "&state=test&redirect_uri=" + Utils.GetRedirectUri() + "";
            string returnValue = RequestUrl(url);

            return returnValue;
        }

        /// <summary>
        /// 通过 Access Token 获取OpenID 的字符串
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public static string GetOpenToken(string accessToken)
        {
            string url = "https://graph.qq.com/oauth2.0/me?access_token=" + accessToken + "";
            string returnValue = RequestUrl(url);
            return returnValue;
        }
        /// <summary>
        /// 根据OpenId,获取用户信息，并写入数据库
        /// </summary>
        /// <param name="OpenId"></param>
        /// <returns></returns>
        public static string GetQQName(string TokenId,string OpenId)
        {
              string name=string.Empty;
           
            DataSet ds = new DataSet();
            ds.ReadXml("https://graph.qq.com/user/get_user_info?access_token="+TokenId+"&oauth_consumer_key="+Utils.GetQQAppId()+"&openid="+OpenId+"&format=xml");
            if (ds.Tables[0].Rows.Count > 0)
            {
             name  = ds.Tables[0].Rows[0]["nickname"].ToString();
            }

            return name;
        }

        /// <summary>
        /// 反序列化获取JSon中的OpenId
        /// </summary>
        /// <param name="Jsonstr"></param>
        /// <returns></returns>
        public static string GetOpenID(string Jsonstr)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();

           // string strJson = "[ {\"client_id\":\"101162813\",\"openid\":\"2718C00832F4869ED35C20260550B029\"} ]";
            List<Info> info = serializer.Deserialize<List<Info>>(Jsonstr);
            return info[0].openid;
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

        /// <summary>
        /// 将认证获得的参数简单转化为哈希表
        /// </summary>
        /// <param name="varstr">获得参数的字符串</param>
        /// <returns></returns>
        public static Hashtable Str2Hash(string varstr)
        {
            Hashtable myhash = new Hashtable();
            if (varstr.Trim() != string.Empty)
            {
                string[] temparr = varstr.Split('&');
                foreach (string onevarstr in temparr)
                {
                    string[] onevararr = onevarstr.Split('=');
                    myhash.Add(onevararr[0], onevararr[1]);
                }
                return myhash;
            }
            else
            {
                return null;
            }
        }

    }
    public class Info
    {
        public Info() { }
        public string client_id { get; set; }
        public string openid { get; set; }
    }
}
