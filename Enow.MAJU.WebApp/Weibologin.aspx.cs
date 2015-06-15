using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.MAJU.Utility;
using Enow.MAJU.BLL;

namespace Enow.MAJU.WebApp
{
    public partial class Weibologin : System.Web.UI.Page
    {
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(Utils.GetQueryStringValue("Code")))
            {
                GetWeiboLogin();
            }
        }

        /// <summary>
        /// 微博登录
        /// </summary>
        #region
        private void GetWeiboLogin()
        {
            string code = Utils.GetQueryStringValue("code");
            string access_token = WeiboHelper.GetAccessToken(code);
            string Uid = WeiboHelper.GetUid(access_token);

            var model = GetMemberInfo(Uid);
            tbl_Member UserInfo = null;
            if (BMember.IsExistsWeiBo(Uid))
            {
                BackPage.UserLogin(model.MemberName, model.Password, out UserInfo);
            }
            else
            {
                BMember.Add(model);
                BackPage.UserLogin(model.MemberName, model.Password, out UserInfo);
            }
            Response.Redirect("/index.aspx");
            Response.End();
        }
        #endregion

        #region 构造会员实体
        private tbl_Member GetMemberInfo(string Uid)
        {
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
                NickName = Uid,
                HeadPhoto =Uid,
                Gender = 0,
                Age = 0,
                Revenue = 0,
                Requirement = "",
                Email = "",
                Mobile = "",
                MemberName = Uid,
                Password = new HashCrypto().MD5Encrypt("123456"),
                LastLoginTime = DateTime.Now,
                LastUpdateTime = DateTime.Now,
                Status = 1,
                IssueTime = DateTime.Now,
                IsPush = '0',
                userid = "",
                channelid = "",
                qqsocialid = "",
                weibosocialid = Uid
            };
            return model;
        }
        #endregion
    }
}