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
    public partial class BaoXianConsult :BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoginCheck();
            if (!IsPostBack)
            {
                initData();
            }
        }

        private void initData()
        {
            var list = BProductReply.GetConsultList(GetUserModel().MemberId);
            if (list.Count>0&&list!=null)
            {
                rpt.DataSource = list;
                rpt.DataBind();
            }
        }

        protected bool GetIsProductReplyAllRead(object productid)
        {
            return BProductReply.IsProductReplyAllRead(GetUserModel().MemberId, productid.ToString());
        }
    }
}