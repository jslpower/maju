using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.MAJU.BLL;
using Enow.MAJU.Model;
using Enow.MAJU.Utility;

namespace Enow.MAJU.Web.Manage.Product
{
    public partial class ProductView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitModel();
            }
        }

        private void InitModel()
        {
            string ProductId = Utils.GetQueryStringValue("Id");
            if (!string.IsNullOrWhiteSpace(ProductId))
            {
                var model = BProduct.GetModel(ProductId);
                if (model!=null)
                {
                    ltrProductName.Text = model.ProductName;
                    ltrType.Text = BProductType.GetModel(model.TypeId).TypeName;
                    ltrOrderNo.Text = model.SortId.ToString();
                    ltrCompany.Text = model.Company;
                    ltrTarget.Text = model.Target;
                    ltrAdvantage.Text = model.Advantage;
                    ltrOtherInfo.Text = model.OtherInfo;
                }
            }
        }
    }
}