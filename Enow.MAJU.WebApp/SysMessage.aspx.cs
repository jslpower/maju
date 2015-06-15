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
    public partial class SysMessage : BackPage
    {
        protected int pagesize = 20;
        protected int pageindex = Utils.GetInt(Utils.GetQueryStringValue("page"), 1);
        protected int rowscount = 0;
        //protected int sendid = Utils.GetInt(Utils.GetQueryStringValue("id"));
        //protected int messageid = Utils.GetInt(Utils.GetQueryStringValue("messageid"));
        protected tbl_Member usermodel = GetUserModel();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsLoginCheck())
            {
                Response.Redirect("default.aspx");
            }
            if (Utils.GetQueryStringValue("dotype") == "getdatas") Utils.RCWE(Newtonsoft.Json.JsonConvert.SerializeObject(GetDatas()));
            if (!IsPostBack)
            {
                InitData();
            }
        }
        void InitData()
        {
          //  var m = BMessage.GetModel(sendid, messageid, usermodel.MemberId);
            var m = GetDatas();
            

            if (m!=null&&m.Count>0)
            {
                rpt_1.DataSource = m;
                rpt_1.DataBind();
            }
        }
        #region
        //List<tbl_SysMessageReply> GetDatas()
        //{
        //    return BMessageReply.GetList(ref rowscount, pagesize, pageindex, new Model.MessageSearch()
        //    {
        //        SendId = sendid,
        //        MessageId = messageid,
        //        MemberId = usermodel.MemberId
        //    });
        //}
        #endregion

        List<dt_SysMessageSend> GetDatas()
        {
            return BMessage.GetList(ref rowscount, pagesize, pageindex, new Model.MessageSearch() { MemberId = usermodel.MemberId });
        }

        protected void rpt_1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType==ListItemType.Item||e.Item.ItemType==ListItemType.AlternatingItem)
            {
                var rpt = e.Item.FindControl("rpt") as Repeater;
                var m = (dt_SysMessageSend)e.Item.DataItem;
                BMessage.UpdateRead(m.SendId, m.MessageId, m.MemberId);
                var list = BMessageReply.GetList(new Model.MessageSearch()
                { 
                SendId=m.SendId,
                MessageId=m.MessageId,
                MemberId=m.MemberId
                });
                if (list!=null&&list.Count>0)
                {
                    rpt.DataSource = list;
                    rpt.DataBind();
                }
            }
        }
    }
}