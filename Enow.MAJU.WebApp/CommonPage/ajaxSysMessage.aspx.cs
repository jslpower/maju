using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.MAJU.BLL;
using Enow.MAJU.Utility;

namespace Enow.MAJU.WebApp.CommonPage
{
    public partial class ajaxSysMessage : System.Web.UI.Page
    {
        protected int pagesize = 20;
        protected int pageindex = Utils.GetInt(Utils.GetQueryStringValue("page"), 1);
        protected int rowscount = 0;
        //protected int sendid = Utils.GetInt(Utils.GetQueryStringValue("id"));
        //protected int messageid = Utils.GetInt(Utils.GetQueryStringValue("messageid"));
        protected tbl_Member usermodel = BackPage.GetUserModel();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitData();
            }
        }
        void InitData()
        {
            var m = BMessage.GetList(ref rowscount, pagesize, pageindex, new Model.MessageSearch() { MemberId = BackPage.GetUserModel().MemberId });


            if (m != null && m.Count > 0)
            {
                rpt_1.DataSource = m;
                rpt_1.DataBind();
            }
        }
        protected void rpt_1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var rpt = e.Item.FindControl("rpt") as Repeater;
                var m = (dt_SysMessageSend)e.Item.DataItem;
                BMessage.UpdateRead(m.SendId, m.MessageId, m.MemberId);
                var list = BMessageReply.GetList(new Model.MessageSearch()
                {
                    SendId = m.SendId,
                    MessageId = m.MessageId,
                    MemberId = m.MemberId
                });
                if (list != null && list.Count > 0)
                {
                    rpt.DataSource = list;
                    rpt.DataBind();
                }
            }
        }
    }
}