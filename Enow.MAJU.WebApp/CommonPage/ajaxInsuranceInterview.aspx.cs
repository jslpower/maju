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
    public partial class ajaxInsuranceInterview : System.Web.UI.Page
    {
        protected int pagesize = 20;
        protected int pageindex = Utils.GetInt(Utils.GetQueryStringValue("page"), 1);
        protected int rowscount = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitPage();
            }
        }
        List<dt_ProductInterview> GetDatas()
        {
            return BProductInterview.GetViewList(ref rowscount, pagesize, pageindex, new Model.ProductInterViewSearch() { MemberId = BackPage.GetUserModel().MemberId });
        }
        void InitPage()
        {
            var d = GetDatas();
            if (d != null && d.Count > 0)
            {
                rpt.DataSource = d;
                rpt.DataBind();
            }
        }
    }
}