using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Enow.MAJU.BLL
{
    public class BackPage : System.Web.UI.Page
    {
        #region 变量定义
        private static string _ValidCode = "MAJUValidCode";
        /// <summary>
        /// 获取验证码的key
        /// </summary>
        public static string ValidCode
        {
            get { return _ValidCode; }
        }
        private static string _RedirectUrl = "/Default.aspx";
        /// <summary>
        /// 转向地址
        /// </summary>
        public static string RedirectUrl
        {
            get { return _RedirectUrl; }
            set { _RedirectUrl = value; }
        }
        private const string _AuthorName = "MAJUMemberLogin";
        /// <summary>
        /// 获得数据验证的key
        /// </summary>
        public static string AuthorName
        {
            get { return _AuthorName; }
        }
        private const string _Key = "12$#@!#@5tr%u8wsfr543$,23ve7w%$#";
        /// <summary>
        /// 获得数据验证的key
        /// </summary>
        public static string Key
        {
            get { return _Key; }
        }
        private const string _IV = "!54~1)e74&%3+-q#";
        /// <summary>
        /// 获得数据验证的IV
        /// </summary>
        public static string IV
        {
            get { return _IV; }
        }
        #endregion

        #region 用户登陆
        /// <summary>
        /// 登陆验证
        /// </summary>        
        /// <param name="UserName">用户名</param>
        /// <param name="Password">密码</param>
        /// <returns>1:登陆成功  -1:用户名或密码不正确 2账号未审核 3账号已禁用</returns>
        public static int UserLogin(string UserName, string Password, out tbl_Member UserInfo)
        {
            using (FWDC rdc = new FWDC())
            {
                Enow.MAJU.Utility.HashCrypto CrypTo = new Enow.MAJU.Utility.HashCrypto();
                Password = CrypTo.MD5Encrypt(Password);
                CrypTo.Dispose();
                var model = rdc.tbl_Member.FirstOrDefault(u => u.MemberName == UserName && u.Password == Password);
                if (model != null)
                {
                    rdc.ExecuteCommand("UPDATE [dbo].[tbl_Member] SET LastLoginTime=GETDATE() WHERE MemberId = '" + model.MemberId + "'");
                    //model.LastLoginTime = DateTime.Now;
                    //rdc.SubmitChanges();
                    UserInfo = model;
                    //if (model.State == (int)Model.EnumType.会员状态.审核中)
                    //{
                    //    return 2;
                    //}
                    //else if (model.State==(int)Model.EnumType.会员状态.拒绝)
                    //{
                    //    return 3;
                    //}
                    //else
                    //{
                    WriteCookieInfo(model);
                    return 1;
                    //}
                }
                else
                {
                    UserInfo = null;
                    return -1;
                }
            }
        }
        #endregion

        #region 用户退出
        /// <summary>
        /// 用户退出
        /// </summary>
        /// <param name="Url">退出后转向网址</param>
        public static void Logout(string Url)
        {
            HttpContext.Current.Response.Cookies[AuthorName].Expires = DateTime.Now.AddDays(-1);
            HttpContext.Current.Response.Redirect(Url, true);
        }
        #endregion

        #region 用户验证及取得已登陆的用户信息
        /// <summary>
        /// 验证用户是否成功登陆
        /// </summary>
        public static void LoginCheck()
        {
            tbl_Member model = GetUserModel();
            if (model == null)
                HttpContext.Current.Response.Redirect(RedirectUrl, true);

        }
        /// <summary>
        /// 验证用户是否成功登陆
        /// </summary>
        /// <returns>true:已登陆 false:未登陆</returns>
        public static bool IsLoginCheck()
        {
            tbl_Member model = GetUserModel();
            if (model == null)
                return false;
            else
                return true;

        }
        /// <summary>
        /// 验证用户是否成功登陆
        /// </summary>
        /// <param name="url">跳转的URL</param>
        public static void LoginCheck(string url)
        {
            tbl_Member model = GetUserModel();
            if (model == null)
                HttpContext.Current.Response.Redirect(url, true);
        }
        /// <summary>
        /// 取得登陆用户的凭据数据
        /// </summary>
        /// <returns>返回登陆用户的实体</returns>
        public static tbl_Member GetUserModel()
        {
            HttpCookie hc = HttpContext.Current.Request.Cookies[AuthorName];
            if (hc != null)
            {
                try
                {
                    bool IsLoginState = true;
                    tbl_Member model = new tbl_Member();
                    Enow.MAJU.Utility.HashCrypto crypto = new Enow.MAJU.Utility.HashCrypto();
                    crypto.Key = Key;
                    crypto.IV = IV;
                    if (hc["MemberId"] != null && String.Empty != hc["MemberId"])
                    {
                        model.MemberId = crypto.DeRC2Encrypt(hc["MemberId"]);
                    }
                    else
                    {
                        IsLoginState = false;
                    }
                    if (hc["NickName"] != null && String.Empty != hc["NickName"])
                    {
                        model.NickName = crypto.DeRC2Encrypt(hc["NickName"]);
                    }
                    if (hc["Mobile"] != null && String.Empty != hc["Mobile"])
                    {
                        model.Mobile = crypto.DeRC2Encrypt(hc["Mobile"]);
                    }
                    if (hc["MemberName"] != null && String.Empty != hc["MemberName"])
                    {
                        model.MemberName = crypto.DeRC2Encrypt(hc["MemberName"]);
                    }
                    if (hc["Email"] != null && String.Empty != hc["Email"])
                    {
                        model.Email = crypto.DeRC2Encrypt(hc["Email"]);
                    }
                    if (hc["HeadPhoto"] != null && String.Empty != hc["HeadPhoto"])
                    {
                        model.HeadPhoto = crypto.DeRC2Encrypt(hc["HeadPhoto"]);
                    }
                    if (hc["Gender"] != null && String.Empty != hc["Gender"])
                    {
                        model.Gender = byte.Parse(crypto.DeRC2Encrypt(hc["Gender"]));
                    }
                    if (hc["Age"] != null && String.Empty != hc["Age"])
                    {
                        model.Age = byte.Parse(crypto.DeRC2Encrypt(hc["Age"]));
                    }
                    if (hc["Revenue"] != null && String.Empty != hc["Revenue"])
                    {
                        model.Revenue = byte.Parse(crypto.DeRC2Encrypt(hc["Revenue"]));
                    }
                    if (hc["ProvinceId"]!=null&&string.Empty!=hc["ProvinceId"])
                    {
                        model.ProvinceId = int.Parse(crypto.DeRC2Encrypt(hc["ProvinceId"]));
                    }
                    if (hc["CityId"] != null && string.Empty != hc["CityId"])
                    {
                        model.CityId = int.Parse(crypto.DeRC2Encrypt(hc["CityId"]));
                    }
                    if (hc["Requirement"]!=null&&string.Empty!=hc["Requirement"])
                    {
                        model.Requirement = crypto.DeRC2Encrypt(hc["Requirement"]);
                    }
                    if (hc["IsPush"] != null && string.Empty != hc["IsPush"])
                    {
                        model.IsPush = char.Parse(crypto.DeRC2Encrypt(hc["IsPush"]));
                    }
                    if (hc["userid"] != null && string.Empty != hc["userid"])
                    {
                        model.userid = crypto.DeRC2Encrypt(hc["userid"]);
                    }
                    if (hc["channelid"] != null && string.Empty != hc["channelid"])
                    {
                        model.channelid = crypto.DeRC2Encrypt(hc["channelid"]);
                    }

                   
                    if (IsLoginState)
                        return model;
                    else
                        return null;
                }
                catch { return null; }
            }
            else { return null; }
        }
        #endregion

        #region 保存用户凭证
        /// <summary>
        /// 保存用户登陆凭证
        /// </summary>
        /// <param name="model">用户实体</param>
        /// <param name="cookieName">凭证名称</param>
        protected static void WriteCookieInfo(tbl_Member model)
        {
            Enow.MAJU.Utility.HashCrypto CrypTo = new Enow.MAJU.Utility.HashCrypto();
            CrypTo.Key = Key;
            CrypTo.IV = IV;
            HttpCookie Hc = new HttpCookie(AuthorName);
            Hc.Values.Add("MemberId", CrypTo.RC2Encrypt(model.MemberId));
            Hc.Values.Add("NickName", string.IsNullOrWhiteSpace(model.NickName) ? string.Empty : CrypTo.RC2Encrypt(model.NickName));
            Hc.Values.Add("Mobile", CrypTo.RC2Encrypt(model.Mobile));
            Hc.Values.Add("MemberName", CrypTo.RC2Encrypt(model.MemberName));
            Hc.Values.Add("Email", string.IsNullOrWhiteSpace(model.Email) ? string.Empty : CrypTo.RC2Encrypt(model.Email));
            Hc.Values.Add("HeadPhoto", string.IsNullOrWhiteSpace(model.HeadPhoto) ? string.Empty : CrypTo.RC2Encrypt(model.HeadPhoto));
            Hc.Values.Add("Gender", CrypTo.RC2Encrypt(model.Gender.ToString()));
            Hc.Values.Add("Age", CrypTo.RC2Encrypt(model.Age.ToString()));
            Hc.Values.Add("Revenue", CrypTo.RC2Encrypt(model.Revenue.ToString()));
            Hc.Values.Add("ProvinceId", CrypTo.RC2Encrypt(model.ProvinceId.ToString()));
            Hc.Values.Add("CityId", CrypTo.RC2Encrypt(model.CityId.ToString()));
            Hc.Values.Add("Requirement", string.IsNullOrWhiteSpace(model.Requirement) ? string.Empty : CrypTo.RC2Encrypt(model.Requirement));
            Hc.Values.Add("IsPush", CrypTo.RC2Encrypt(model.IsPush.ToString()));
            Hc.Values.Add("userid", string.IsNullOrWhiteSpace(model.userid) ? string.Empty : CrypTo.RC2Encrypt(model.userid));
            Hc.Values.Add("channelid", string.IsNullOrWhiteSpace(model.channelid) ? string.Empty : CrypTo.RC2Encrypt(model.channelid));
            HttpContext.Current.Response.Cookies.Add(Hc);
            CrypTo.Dispose();
            CrypTo = null;
            model = null;
        }
        #endregion

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            //LoginCheck();
        }
    }
}
