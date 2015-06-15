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
    public partial class YouDian : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
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
                switch (Utils.GetQueryStringValue("T"))
                {
                    case "2":
                        litType.Text = "产品特点";
                        litContext.Text = m.OtherInfo;
                        break;
                    case "3":
                        litType.Text = "有关资料";
                        litContext.Text = m.RelatedFile;
                        break;
                    default:
                        litType.Text = "为什么适合您";
                        litContext.Text = m.Advantage;
                        break;
                }
            }
            else
            {
                MessageBox.ShowAndRedirect("该产品不存在", "baoxian.aspx");
            }
        }
    }
}