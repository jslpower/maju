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
    public partial class InsuranceConsult : BackPage
    {
        protected int pagesize = 20;
        protected int pageindex = Utils.GetInt(Utils.GetQueryStringValue("page"), 1);
        protected int rowscount = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            LoginCheck();
            if (Utils.GetQueryStringValue("dotype") == "getdatas") Utils.RCWE(Newtonsoft.Json.JsonConvert.SerializeObject(GetDatas()));
            if (!Page.IsPostBack)
            {
                InitData();
            }
        }
        void InitData()
        {
            var d = GetDatas();
            if (d != null && d.Count > 0)
            {
                rpt.DataSource = d;
                rpt.DataBind();
                foreach (var m in d)
                {
                    if(!m.IsRead.Equals("1"))
                    BProductReply.UpdateRead(m.ReplyId);
                }
            }
        }
        List<dt_ProductReply> GetDatas()
        {
            return BProductReply.GetList(ref rowscount, pagesize, pageindex, new Model.ProductReplySearch() { ProductId = Utils.GetQueryStringValue("productid"), MemberId = GetUserModel().MemberId });
        }
    }
}