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
    public partial class baoxiandetail : BackPage
    {
        protected int loginState = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            loginState = IsLoginCheck() == true ? 1 : 0;
            if (!IsPostBack)
            {
                InitData();
            }
        }
        void InitData()
        {
            var m = BProduct.GetModel(Utils.GetQueryStringValue("id"));
            if (m != null)
            {
                litProductName.Text = m.ProductName;
                litCompany.Text = m.Company;
                litCollects.Text = m.Collects.ToString();
                litTarget.Text = m.Target;
            }
            else
            {
                MessageBox.ShowAndRedirect("该产品不存在", "baoxian.aspx");
            }
        }
    }
}