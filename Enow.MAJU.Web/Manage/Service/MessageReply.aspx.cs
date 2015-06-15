using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.MAJU.BLL;
using Enow.MAJU.Utility;

namespace Enow.MAJU.Web.Manage.Service
{
    public partial class MessageReply : System.Web.UI.Page
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
                tbl_Member model = new tbl_Member();
                ManageUserAuth.ManageLoginCheck();
                string Id = Utils.GetQueryStringValue("Id");
                var MessageModel = BMessageReply.GetModel(Id);
                model = BMember.GetModel(MessageModel.MemberId);
                if (model!=null)
                {
                    ltrNickName.Text = model.NickName;
                }
                
             
                ltrDate.Text = MessageModel.IssueTime.ToString("yyyy-MM-dd HH:mm");
                ltrContext.Text = MessageModel.Context;
            }
        }
        
        /// <summary>
        /// 保存
        /// </summary>
        private void SaveData()
        {
            ManagerList model = ManageUserAuth.GetManageUserModel();
            string Context = Utils.GetFormValue("txtContent");
            if (string.IsNullOrWhiteSpace(Context))
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "请填写消息内容"));
                return;
            }
            string Id = Utils.GetQueryStringValue("Id");
            var MessageModel = BMessageReply.GetModel(Id);
            //ltrNickName.Text = BMember.GetModel(MessageModel.MemberId).NickName;
            //ltrDate.Text = MessageModel.IssueTime.ToString("yyyy-MM-dd HH:mm");
            //ltrContext.Text = MessageModel.Context;

            //插入回复记录
            bool ret = BMessageReply.Add(new tbl_SysMessageReply() { 
            ReplyId=Guid.NewGuid().ToString(),
            SendId=MessageModel.SendId,
            MessageId=MessageModel.MessageId,
            MemberId = MessageModel.MemberId,
            Context=Context,
            IssueTime=DateTime.Now,
            IsSysReply='1',
            OperatorId=model.Id
            });
            //发送消息给用户
            //BMessage.MessageSend(new tbl_SysMessageSend() { 
            //MessageId=MessageModel.MessageId,
            //MemberId=MessageModel.MemberId,
            //IsRead='0'
            //});
            if (ret)
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("1", "操作成功!"));
            }
            else
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败!"));
            }
        }
    }
}