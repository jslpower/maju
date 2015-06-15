using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.MAJU.BLL;
using Enow.MAJU.Model;
using Enow.MAJU.Utility;

namespace Enow.MAJU.Web.Manage.Service
{
    public partial class AddMessage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            #region 处理AJAX请求

            if (Utils.GetQueryStringValue("doType") == "save")
            {
                SaveData();
            }
            #endregion
            if (!IsPostBack)
            {
                ManageUserAuth.ManageLoginCheck();
            }
        }

        /// <summary>
        /// 新增系统消息
        /// </summary>
        private void SaveData()  
        {
            ManagerList ManageModel = ManageUserAuth.GetManageUserModel();
            string Context = Utils.GetFormValue("txtContent");
            if (string.IsNullOrWhiteSpace(Context))
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "请填写消息内容"));
                return;
            }

            int Id = BMessage.Add(new tbl_SysMessage()
            {
                Context = Context,
                Replys = 0,
                Clicks = 0,
                Collects = 0,
                IsHot = '0',
                IsTop = '0',
                OperatorId = ManageModel.Id.ToString(),
                OperatorName = ManageModel.UserName,
                IssueTime = DateTime.Now
            });
            //插入消息回复
            BMessageReply.Add(new tbl_SysMessageReply()
            {
                ReplyId = Guid.NewGuid().ToString(),
                SendId = 0,
                MessageId = Id,
                MemberId = ManageModel.Id.ToString(),
                Context = Context,
                IssueTime = DateTime.Now,
                IsSysReply='1'
            });
            //发送消息给所有会员

            var list = BMember.GetMemberList();
            if (list.Count > 0)
            {
                foreach (var lst in list)
                {
                    BMessage.MessageSend(new tbl_SysMessageSend() { 
                    MessageId=Id,
                    MemberId=lst.MemberId,
                    IsRead='0'
                    });
                }
            }

            if (Id > 0)
            {
                Enow.MAJU.Utility.Push.JPush.PushAndroid("", System.Configuration.ConfigurationManager.AppSettings["SysMessageTitle"].ToString(), Context);
                Utils.RCWE(UtilsCommons.AjaxReturnJson("1", "操作成功!"));
            }
            else
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败!"));
            }

        }
    }
}