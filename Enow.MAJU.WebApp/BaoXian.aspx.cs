using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.MAJU.BLL;
using Enow.MAJU.Utility;
using System.Data;

namespace Enow.MAJU.WebApp
{
    public partial class BaoXian : BackPage
    {
        protected int pagesize = 10;
        protected int pageindex = Utils.GetInt(Utils.GetQueryStringValue("page"),1);
        protected int rowscount = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Utils.GetQueryStringValue("dotype") == "getproducttypes") { Utils.RCWE(Newtonsoft.Json.JsonConvert.SerializeObject(GetProductTypes())); }
            //if (Utils.GetQueryStringValue("dotype") == "getproducts") Utils.RCWE(Newtonsoft.Json.JsonConvert.SerializeObject(GetProducts(Utils.GetQueryStringValue("typeid"))));
            if (!IsPostBack)
            {
                InitData();
            }
        }
        void InitData()
        {
            var d = GetProductTypes();
            if (d != null && d.Count > 0)
            {
                rpt1.DataSource = d;
                rpt1.DataBind();
            }
        }
        protected void rpt1_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var rpt = e.Item.FindControl("rpt2") as Repeater;
            var rowv = (ProductType)e.Item.DataItem;
            var d = rowv.Products;
            if (d != null && d.Count > 0)
            {
                rpt.DataSource = d;
                rpt.DataBind();
            }
        }
        private List<ProductType> GetProductTypes()
        {
            return BProductType.GetViewList(ref rowscount, pagesize, pageindex, string.Empty);
        }
    }
}