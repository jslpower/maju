using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Enow.MAJU.SMS;
using Enow.MAJU.Utility;
using Enow.MAJU.BLL;

namespace Enow.MAJU.Web.Ashx
{
    /// <summary>
    /// GetVerCode 的摘要说明
    /// </summary>
    public class GetVerCode : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string MobilePhone = context.Request.QueryString["MobilePhone"];
            context.Response.ContentType = "text/plain";
            context.Response.Expires = 0;
            context.Response.CacheControl = "no-cache";
            context.Response.Cache.SetNoStore();
            getVerCode(MobilePhone);
            context.Response.End();
        }
        /// <summary>
        /// 获取注册验证码
        /// </summary>
        private void getVerCode(string MobilePhone)
        {
            ///获取6位随机验证码
            string ValidCode = MakeValidCode();
            //设置加密cookie
            Enow.MAJU.Utility.HashCrypto CrypTo = new Enow.MAJU.Utility.HashCrypto();
            CrypTo.Key = BackPage.Key;
            CrypTo.IV = BackPage.IV;
            HttpCookie cd = new HttpCookie(BackPage.ValidCode, CrypTo.RC2Encrypt(ValidCode));
            cd.Expires = DateTime.Now.AddMinutes(30);//验证码30分钟内有效
            HttpContext.Current.Response.Cookies.Add(cd);
            //发送验证码至手机
            var v = SMSClass.ProxySMS(MobilePhone, "您的马驹保险校验码为：" + ValidCode + "，请在30分钟内按页面提示提交校验码。");
            Utils.RCWE(UtilsCommons.AjaxReturnJson(v ? "1" : "-1", v ? "已发送" : "发送失败"));
        }
        /// <summary>
        /// 生成6位数字随机验证码
        /// </summary>
        /// <returns></returns>
        private static string MakeValidCode()
        {
            char[] str = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            string num = "";
            Random r = new Random();
            for (int i = 0; i < 6; i++)
            {
                num += str[r.Next(0, str.Length)].ToString();
            }

            return num;
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}