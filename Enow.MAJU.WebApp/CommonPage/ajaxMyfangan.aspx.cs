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
    public partial class ajaxMyfangan : System.Web.UI.Page
    {
        protected int pagesize = 10;
        protected int pageindex = Utils.GetInt(Utils.GetQueryStringValue("page"), 1);
        protected int rowscount = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitPage();
            }
        }

        private void InitPage()
        {
            var d = GetData();
            if (d != null && d.Count > 0)
            {
                rpt.DataSource = d;
                rpt.DataBind();
            }


        }
        List<dt_ProductSolution> GetData()
        {
            return BProductSolution.GetViewtList(ref rowscount, pagesize, pageindex, new Model.ProductSolutionSearch() { MemberId = BackPage.GetUserModel().MemberId });
        }
    }
}