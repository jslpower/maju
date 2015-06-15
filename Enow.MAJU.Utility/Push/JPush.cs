using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Security.Cryptography;

namespace Enow.MAJU.Utility.Push
{
    public class JPush
    {
        private static readonly string ApiKey = System.Configuration.ConfigurationManager.AppSettings["JApiKey"].ToString();//Android ApiKey

        private static readonly string APIMasterSecret = System.Configuration.ConfigurationManager.AppSettings["JAPIMasterSecret"].ToString();//Android密码

        /// <summary>
        /// Android极光推送
        /// </summary>
        /// <param name="RegistrationID"></param>
        public static void PushAndroid(string RegistrationID, string Title, string Content)
        {

            try
            {

                Random ran = new Random();

                int sendno = ran.Next(1, 2100000000);//随机生成的一个编号

                string app_key = ApiKey;

                string masterSecret = APIMasterSecret;

                int receiver_type = 4;//接收者类型。2、指定的 tag。3、指定的 alias。4、广播：对 app_key 下的所有用户推送消息。5、根据 RegistrationID=0308a096ef0 进行推送。当前只是 Android SDK r1.6.0 版本支持

                string receiver_value = RegistrationID;

                int msg_type = 1;//1、通知2、自定义消息（只有 Android 支持）

                string msg_content = "{\"n_builder_id\":\"00\",\"n_title\":\"" + Title + "\",\"n_content\":\"" + Content + "\"}";//消息内容

                string platform = "android";//目标用户终端手机的平台类型，如： android, ios 多个请使用逗号分隔。

                string verification_code = GetMD5Str(sendno.ToString(), receiver_type.ToString(), receiver_value, masterSecret);//验证串，用于校验发送的合法性。MD5

                string postData = "sendno=" + sendno;

                postData += ("&app_key=" + app_key);

                postData += ("&masterSecret=" + masterSecret);

                postData += ("&receiver_type=" + receiver_type);

                postData += ("&receiver_value=" + receiver_value);

                postData += ("&msg_type=" + msg_type);

                postData += ("&msg_content=" + msg_content);

                postData += ("&platform=" + platform);

                postData += ("&verification_code=" + verification_code);

                //byte[] data = encoding.GetBytes(postData);

                byte[] data = Encoding.UTF8.GetBytes(postData);

                string resCode = GetPostRequest(data);//调用极光的接口获取返回值

                //JpushMsg msg = (JpushMsg)Newtonsoft.Json.JsonConvert.DeserializeObject(resCode);//定义一个JpushMsg类，包含返回值信息，将返回的json格式字符串转成JpushMsg对象

            }

            catch (Exception ex)
            {

            }

        }

        /// <summary>
        /// MD5加密验证字符串，用于调用接口的时候，极光将做验证使用
        /// </summary>
        /// <param name="paras"></param>
        /// <returns></returns>
        public static string GetMD5Str(params string[] paras)
        {

            string str = "";

            for (int i = 0; i < paras.Length; i++)
            {

                str += paras[i];

            }

            byte[] buffer = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(str));

            string md5Str = string.Empty;

            for (int i = 0; i < buffer.Length; i++)
            {

                md5Str = md5Str + buffer[i].ToString("X2");

            }

            return md5Str;

        }

        /// <summary>
        /// http Post方式调用极光的推送服务
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string GetPostRequest(byte[] data)
        {

            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create("http://api.jpush.cn:8800/v2/push");

            myRequest.Method = "POST";//极光http请求方式为post

            myRequest.ContentType = "application/x-www-form-urlencoded";//按照极光的要求

            myRequest.ContentLength = data.Length;

            Stream newStream = myRequest.GetRequestStream();

            // Send the data.

            newStream.Write(data, 0, data.Length);

            newStream.Close();

            // Get response

            var response = (HttpWebResponse)myRequest.GetResponse();

            using (var reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("UTF-8")))
            {

                string result = reader.ReadToEnd();

                reader.Close();

                response.Close();

                return result;

            }

        }

        /// <summary>
        /// 定义一个类，接收返回值
        /// </summary>
        public class JpushMsg
        {

            private string sendno;//编号

            public string Sendno
            {

                get { return sendno; }

                set { sendno = value; }

            }

            private string msg_id;//信息编号

            public string Msg_id
            {

                get { return msg_id; }

                set { msg_id = value; }

            }

            private string errcode;//返回码

            public string Errcode
            {

                get { return errcode; }

                set { errcode = value; }

            }

            private string errmsg;//错误信息

            public string Errmsg
            {

                get { return errmsg; }

                set { errmsg = value; }

            }

        }
    }
}
