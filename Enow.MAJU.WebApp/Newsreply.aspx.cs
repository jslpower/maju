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
    public partial class Newsreply : BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Utils.GetQueryStringValue("type") == "save")
            {
                string Id = Utils.GetQueryStringValue("Id");
                var MemberModel = GetUserModel();
                string Content = Request.Form["content"].ToString();
                if (!string.IsNullOrWhiteSpace(Id))
                {
                    var model = new tbl_NewsReply()
                    {
                        ReplyId = Guid.NewGuid().ToString(),
                        NewsId = Id,
                        MemberId = MemberModel.MemberId,
                        Context = Content,
                        IssueTime = DateTime.Now
                    };

                    if (BNewsReply.Add(model))
                    {
                        //回复数加一
                        BNews.UpdateNumber(Id, Model.EnumType.更新类别.回复, Model.EnumType.操作符号.加);

                        Utils.RCWE(UtilsCommons.AjaxReturnJson("1", "回复成功"));
                    }
                    else
                    {
                        Utils.RCWE(UtilsCommons.AjaxReturnJson("-1", "回复异常，请重新提交！"));
                    }
                }
                else
                {
                    Utils.RCWE(UtilsCommons.AjaxReturnJson("-2", "数据异常！"));
                }
            }

        }
    }
}