using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.MAJU.BLL;

namespace Enow.MAJU.WebApp
{
    public partial class MessageCenter : BackPage
    {
        protected static tbl_Member UserInfo = GetUserModel();
        public bool IsMemberTopicReplyAllRead = BMemberTopicReply.IsMemberTopicReplyAllRead(UserInfo != null ? UserInfo.MemberId : string.Empty);
        public bool IsSysMessageAllRead = BMessage.IsSysMessageAllRead(UserInfo != null ? UserInfo.MemberId : string.Empty);
        public bool IsProductReplyAllRead = BProductReply.IsProductReplyAllRead(UserInfo != null ? UserInfo.MemberId : string.Empty, string.Empty);

        //public MessageCenter()
        //{
        //    IsMemberTopicReplyAllRead = BMemberTopicReply.IsMemberTopicReplyAllRead(UserInfo != null ? UserInfo.MemberId : string.Empty);
        //    IsSysMessageAllRead = BMessage.IsSysMessageAllRead(UserInfo != null ? UserInfo.MemberId : string.Empty);
        //    IsProductReplyAllRead = BProductReply.IsProductReplyAllRead(UserInfo != null ? UserInfo.MemberId : string.Empty, string.Empty);
        //}

        protected void Page_Load(object sender, EventArgs e)
        {            
        }
    }
}