using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using System.Text;
using Enow.MAJU.Utility;
using System.Xml;

namespace Enow.MAJU.SMS
{
    /// <summary>
    /// 短信发送
    /// </summary>
    public class SMSClass
    {
        /// <summary>
        /// 签名
        /// </summary>
        private static string SmsSignature = System.Configuration.ConfigurationManager.AppSettings["SmsSignature"];
        /// <summary>
        /// 请求地址
        /// </summary>
        private static string SmsSendUrl = System.Configuration.ConfigurationManager.AppSettings["SmsSendUrl"];
        /// <summary>
        /// 用户名
        /// </summary>
        private static string SmsSendUserName = System.Configuration.ConfigurationManager.AppSettings["SmsSendUserName"];
        /// <summary>
        /// 密码
        /// </summary>
        private static string SmsSendPassword = System.Configuration.ConfigurationManager.AppSettings["SmsSendPassword"];
        /// <summary>
        /// 验证码1
        /// </summary>
        private static string SmsSendCode1 = System.Configuration.ConfigurationManager.AppSettings["SmsSendCode1"];
        /// <summary>
        /// 验证码2
        /// </summary>
        private static string SmsSendCode2 = System.Configuration.ConfigurationManager.AppSettings["SmsSendCode2"];
        
        /// <summary>
        /// 短信提醒
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static bool ProxySMS(string mobile, string content)
        {
            if (string.IsNullOrEmpty(mobile)) return false;
            var m = mobile.Split(',')[0];
            if (string.IsNullOrEmpty(m) || m.Length < 4) return false;
            string s = HttpUtility.UrlEncode(SmsSignature + content, Encoding.GetEncoding("UTF-8"));
            //var ES = Utils.GetInt(m.Substring(m.Length - 4, 4)) * Utils.GetInt(SmsSendCode1) + Utils.GetInt(SmsSendCode2);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(SmsSendUrl + "?username=" + SmsSendUserName + "&password=" + SmsSendPassword + "&mobile=" + mobile + "&content=" + s);
            request.Method = "POST";
            request.ContentType = "text/html;charset=UTF-8";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("UTF-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(retString);
            var x=doc.GetElementsByTagName("resultcode");
            var r=x[0].InnerText;
            return r == "0";
        }
    }
}
