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
    public partial class Weibo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Utils.GetQueryStringValue("dotype") == "login")
            {
                Weibologin();
            }
        }

        #region 微博登录
        /// <summary>
        /// 微博登录
        /// </summary>
        private void Weibologin()
        {
            //与微博账号唯一对应的ID
            string Id = Utils.GetQueryStringValue("id");
            //用户昵称
            string Screen_name = Server.UrlDecode(Utils.GetQueryStringValue("screen_name"));
            //性别 m:男 f:女  n:未知
            string Gender = Utils.GetQueryStringValue("gender");
            //头像地址
            string PhotoUrl = Utils.GetQueryStringValue("avatar_large");

            #region 构造会员实体

            var model = new tbl_Member
            {
                MemberId = Guid.NewGuid().ToString(),
                CountryId = 0,
                CountryName = "",
                ProvinceId = 0,
                ProvinceName = "",
                CityId = 0,
                CityName = "",
                AreaId = 0,
                AreaName = "",
                NickName = Screen_name,
                HeadPhoto = PhotoUrl,
                Gender = 0,
                Age = 0,
                Revenue = 0,
                Requirement = "",
                Email = "",
                Mobile = "",
                MemberName = Screen_name,
                Password = "123456",
                LastLoginTime = DateTime.Now,
                LastUpdateTime = DateTime.Now,
                Status = 1,
                IssueTime = DateTime.Now,
                IsPush = '0',
                userid = "",
                channelid = "",
                qqsocialid = "",
                weibosocialid = Id
            };
            #endregion
            tbl_Member UserInfo = null;
            var r = 0;
            if (BMember.IsExistsWeiBo(Id))
            {
                r = BackPage.UserLogin(model.MemberName, model.Password, out UserInfo);
            }
            else
            {
                model.Password = new HashCrypto().MD5Encrypt(model.Password);
                BMember.Add(model);
                r = BackPage.UserLogin(model.MemberName, "123456", out UserInfo);
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
    }
}