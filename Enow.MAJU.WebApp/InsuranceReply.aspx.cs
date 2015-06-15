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
    public partial class InsuranceReply : BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoginCheck();
            if (Utils.GetQueryStringValue("type") == "save")
            {
                string Id = Utils.GetQueryStringValue("Id");
                var memberModel = GetUserModel();
                string content = Utils.GetFormValue("content");
                //TopicId不为空，则为话题评论
                if (!string.IsNullOrWhiteSpace(Id))
                {
                    var model = new tbl_ProductReply()
                    {
                        ReplyId = Guid.NewGuid().ToString(),
                        ProductId = Id,
                        MemberId = memberModel.MemberId,
                        Context = content,
                        IssueTime = DateTime.Now,
                        IsSysReply='0'
                    };

                    if (BProductReply.Add(model))
                    {
                        Utils.RCWE(UtilsCommons.AjaxReturnJson("1", "咨询成功"));
                    }
                    else
                    {
                        Utils.RCWE(UtilsCommons.AjaxReturnJson("-1", "咨询内容异常，请重新提交"));

                    }
                }
            }
        }
    }
}