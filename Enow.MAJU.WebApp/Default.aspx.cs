using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.MAJU.BLL;
using Enow.MAJU.Utility;

namespace Enow.MAJU.WebApp
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Utils.GetQueryStringValue("doajax") == "login")
                Utils.RCWE(UserLogin());
            if (Utils.GetQueryStringValue("doajax") == "register")
                Register();
            if (Utils.GetQueryStringValue("doajax") == "findpwd")
                FindPassword();
            if (!IsPostBack && BackPage.IsLoginCheck())
                Response.Redirect("index.aspx");

        }




        private string UserLogin()
        {
            tbl_Member userInfo = null;
            string userName = Utils.GetFormValue("m");
            string pwd = Utils.GetFormValue("p");
            if (string.IsNullOrEmpty(userName))
            {
                return UtilsCommons.AjaxReturnJson("0", "请填写用户名");
            }
            if (string.IsNullOrEmpty(pwd))
            {
                return UtilsCommons.AjaxReturnJson("0", "请填写密码");
            }

            int loginState = BackPage.UserLogin(userName, pwd, out userInfo);

            switch (loginState)
            {
                case 1:
                    //  lblUserName.Text = userInfo.UserName;

                    return UtilsCommons.AjaxReturnJson("1", "登录成功");
                    break;
                case 2:
                    return UtilsCommons.AjaxReturnJson("2", "登录失败，您的账号尚未通过审核");
                    break;
                case 3:
                    return UtilsCommons.AjaxReturnJson("3", "登录失败，您的账号已禁用");
                    break;
                case -1:
                    return UtilsCommons.AjaxReturnJson("-1", "登录失败，用户名或者密码不正确");
                    break;
                default:
                    return UtilsCommons.AjaxReturnJson("-7", "登录错误，请联系管理员！");
                    break;
            }


        }

        private void Register()
        {
            if (GetVerCodeExists(Utils.GetFormValue("code1")))
            {
                var m = Utils.GetQueryStringValue("mobile");
                tbl_Member userInfo = null;
                if (BMember.IsExistsMobile(m))
                {
                    Utils.RCWE(UtilsCommons.AjaxReturnJson("-1", "该号码已注册"));
                }
                else
                {
                    var p = Utils.GetFormValue("pwd2");
                    var t = new tbl_Member() { MemberId = Guid.NewGuid().ToString(), Mobile = m, MemberName = m, Password = new HashCrypto().MD5Encrypt(p), IssueTime = DateTime.Now, IsPush = '1' };
                    if (BMember.Add(t))
                    {
                        BackPage.UserLogin(m, p, out userInfo);
                        Utils.RCWE(UtilsCommons.AjaxReturnJson("1", "注册成功"));
                    }

                    else
                    {
                        Utils.RCWE(UtilsCommons.AjaxReturnJson("-1", "注册失败"));
                    }
                }
            }
        }
        private void FindPassword()
        {
            if (GetVerCodeExists(Utils.GetFormValue("code")))
            {
                var memberid = string.Empty;
                tbl_Member userInfo = null;
                var m = Utils.GetQueryStringValue("mobile");
                if (!BMember.IsExistsMobile(m, out memberid))
                {
                    Utils.RCWE(UtilsCommons.AjaxReturnJson("-1", "该号码未注册"));
                }
                else
                {
                    var p = Utils.GetFormValue("pwd");
                    if (BMember.ChangePassword(memberid, new HashCrypto().MD5Encrypt(p)))
                    {
                        BackPage.UserLogin(m, p, out userInfo);
                        Utils.RCWE(UtilsCommons.AjaxReturnJson("1", "密码修改成功"));
                    }
                    else
                    {
                        Utils.RCWE(UtilsCommons.AjaxReturnJson("-1", "密码修改失败"));
                    }
                }
            }
        }
        private static bool GetVerCodeExists(string code)
        {
            var isok = false;
            HttpCookie hc = HttpContext.Current.Request.Cookies[BackPage.ValidCode];
            if (hc != null && !string.IsNullOrEmpty(hc.Value))
            {
                Enow.MAJU.Utility.HashCrypto CrypTo = new Enow.MAJU.Utility.HashCrypto();
                CrypTo.Key = BackPage.Key;
                CrypTo.IV = BackPage.IV;
                if (CrypTo.DeRC2Encrypt(hc.Value) != code)
                {
                    Utils.RCWE(UtilsCommons.AjaxReturnJson("-1", "验证码错误请重新输入"));
                }
                else
                {
                    isok = true;
                }
            }
            else
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("-1", "验证码无效请重新发送"));
            }
            return isok;
        }
    }
}