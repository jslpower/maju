using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.MAJU.Utility;
using Enow.MAJU.BLL;

namespace Enow.MAJU.WebApp
{
    public partial class qqlogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!string.IsNullOrEmpty(Utils.GetQueryStringValue("code")))
            //{
                GetQQlogin();
              
            //}
        }
        #region QQ登录
        /// <summary>
        /// QQ登录
        /// </summary>
        private void GetQQlogin()
        {
            //string strCode = Utils.GetQueryStringValue("code");
            //string strAccessToken = QQLoginHelper.GetAccessToken(strCode);
            //Hashtable ht = QQLoginHelper.Str2Hash(strAccessToken);
            //string strOpenId = QQLoginHelper.GetOpenToken(ht["access_token"].ToString());
            //strOpenId = strOpenId.Replace("callback", "").Replace("(", "[").Replace(")", "]").Replace(";", "");
            string OpenId = Utils.GetQueryStringValue("openid");//QQLoginHelper.GetOpenID(strOpenId);

            var model = GetMemberInfo(OpenId);
            tbl_Member userInfo = null;
            var r = 0;
            if (BMember.IsExistsQQ(OpenId))
            {
                r=BackPage.UserLogin(model.MemberName, model.Password, out userInfo);
            }
            else
            {
                model.Password = new HashCrypto().MD5Encrypt(model.Password);
                BMember.Add(model);
                r = BackPage.UserLogin(model.MemberName, "123456", out userInfo);
            }
            switch (r)
            {
                case 1:
                    Utils.RCWE(UtilsCommons.AjaxReturnJson("1", "登录成功", "index.aspx"));
                    break;
                case 2:
                    Utils.RCWE(UtilsCommons.AjaxReturnJson("2", "登录失败，您的账号尚未通过审核", "/"));
                    break;
                case 3:
                    Utils.RCWE(UtilsCommons.AjaxReturnJson("3", "登录失败，您的账号已禁用", "/"));
                    break;
                case -1:
                    Utils.RCWE(UtilsCommons.AjaxReturnJson("-1", "登录失败，用户名或者密码不正确", "/"));
                    break;
                default:
                    Utils.RCWE(UtilsCommons.AjaxReturnJson("-7", "登录错误，请联系管理员！", "/"));
                    break;
            }

        }
        #endregion

        #region 构造会员实体
        private tbl_Member GetMemberInfo(string OpenId)
        {
            string name = Server.UrlDecode(Utils.GetQueryStringValue("nickname"));//QQLoginHelper.GetQQName(TokenId, OpenId);

            var model = new tbl_Member 
            { 
            MemberId=Guid.NewGuid().ToString(),
            CountryId=0,
            CountryName="",
            ProvinceId=0,
            ProvinceName="",
            CityId=0,
            CityName="",
            AreaId=0,
            AreaName="",
            NickName=name,
            HeadPhoto = Utils.GetQueryStringValue("figureurl"),
            Gender = (byte)(Server.UrlDecode(Utils.GetQueryStringValue("gender"))=="男"?0:1),
            Age=0,
            Revenue=0,
            Requirement="",
            Email="",
            Mobile="",
            MemberName=name,
            Password="123456",
            LastLoginTime=DateTime.Now,
            LastUpdateTime=DateTime.Now,
            Status=1,
            IssueTime=DateTime.Now,
            IsPush='0',
            userid="",
            channelid="",
            qqsocialid=OpenId,
            weibosocialid=""
            };
            return model;
        }
        #endregion
    }
}