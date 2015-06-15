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
    public partial class huatireplay : BackPage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            string Id = Utils.GetQueryStringValue("Id");
            if (string.IsNullOrWhiteSpace(Id))
            {
                lbltitle.Text = "话题发表";
            }
            else
            {
                lbltitle.Text = "话题回复";
            }
            if (Utils.GetQueryStringValue("type")=="save")
            {
               
                var memberModel = GetUserModel();
                string content =Request.Form["content"].ToString();
                content = content.Replace("\r\n", "<br/>");
                //TopicId不为空，则为话题评论
                if (!string.IsNullOrWhiteSpace(Id))
                {
                    var model = new tbl_MemberTopicReply()
                    {
                        ReplyId = Guid.NewGuid().ToString(),
                        TopicId = Id,
                        MemberId = memberModel.MemberId,
                        Context = content,
                        IssueTime = DateTime.Now,
                        IsRead='0'
                    };

                    if (BMemberTopicReply.Add(model))
                    {
                        //话题回复数加一
                        BMemberTopic.UpdateNumber(Id, Model.EnumType.更新类别.回复, Model.EnumType.操作符号.加);

                        Utils.RCWE(UtilsCommons.AjaxReturnJson("1", "回复成功"));
                    }
                    else
                    {
                        Utils.RCWE(UtilsCommons.AjaxReturnJson("-1", "回复异常，请重新提交"));

                    }
                }
                   //新增话题
                else
                {
                    var model = new tbl_MemberTopic()
                    {
                        TopicId = Guid.NewGuid().ToString(),
                        MemberId = memberModel.MemberId,
                        Title = "",
                        Context = content,
                        Replys = 0,
                        Clicks = 0,
                        Collects = 0,
                        IsHot ='0',
                        IsTop = '0',
                        State ='0',
                        IsEssence='0',
                        IssueTime = DateTime.Now
                    };

                    if (BMemberTopic.Add(model))
                    {
                        Utils.RCWE(UtilsCommons.AjaxReturnJson("1", "发表成功!"));
                    }
                    else
                    {
                        Utils.RCWE(UtilsCommons.AjaxReturnJson("-1", "发表失败！"));
                    }
                }
            }
        }
    }
}