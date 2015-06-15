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
    public partial class SysMessageReply : BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
          
            int messageid = BMessage.GetMaxId();
            var memberModel = GetUserModel();
            var Id = BMessage.GetSendId(messageid, memberModel.MemberId);
            var content = Utils.GetFormValue("content");
            if (Utils.GetQueryStringValue("type") == "save")
            {
                var model = new tbl_SysMessageReply()
                {
                    ReplyId = Guid.NewGuid().ToString(),
                    SendId =Id,
                    MessageId = messageid,
                    MemberId = memberModel.MemberId,
                    Context = content,
                    IssueTime = DateTime.Now,
                    IsSysReply='0'
                };

                if (Id > 0 && messageid > 0 && BMessageReply.Add(model))
                {
                    Utils.RCWE(UtilsCommons.AjaxReturnJson("1", "反馈成功"));
                }
                else
                {
                    Utils.RCWE(UtilsCommons.AjaxReturnJson("-1", "反馈失败，请重新提交"));
                }
            }
            if (!IsPostBack)
            {
                
            }
        }
    }
}