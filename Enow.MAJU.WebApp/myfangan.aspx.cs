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
    public partial class myfangan : BackPage
    {
        protected int pagesize = 10;
        protected int pageindex = Utils.GetInt(Utils.GetQueryStringValue("page"), 1);
        protected int rowscount = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsLoginCheck())
            {
                return;
            }
            if (Utils.GetQueryStringValue("dotype") == "getdatas") Utils.RCWE(Newtonsoft.Json.JsonConvert.SerializeObject(GetData()));
            if (Utils.GetQueryStringValue("dotype") == "dodel") GetDelId(Utils.GetQueryStringValue("ids"));
            if (!IsPostBack)
            {
               
                InitData();
            }
        }
        void InitData()
        {
            var d = GetData();
            if (d != null && d.Count > 0)
            {
                rpt.DataSource = d;
                rpt.DataBind();
            }
            else
            {
                Response.Redirect("myfanganno.aspx");
            }

        }
        List<dt_ProductSolution> GetData()
        {
            return BProductSolution.GetViewtList(ref rowscount, pagesize, pageindex, new Model.ProductSolutionSearch() { MemberId = BackPage.GetUserModel().MemberId });
        }
        void GetDelId(string delids)
        {
            List<string> deletedids = new List<string>();
            if (string.IsNullOrEmpty(delids)) Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "请选择您要删除的方案", deletedids));
            var ids = delids.Split(',');
            foreach (var id in ids)
            {
                if (BProductInterview.IsExists(id))
                {
                    Utils.RCWE(UtilsCommons.AjaxReturnJson("-1", "面签已预约不能删除", deletedids));
                }
                else
                {
                    if (BProductSolution.Delete(id))
                    {
                        deletedids.Add(id);
                    }
                    else
                    {
                        Utils.RCWE(UtilsCommons.AjaxReturnJson("-1", "删除失败", deletedids));
                    }
                }
            }
            Utils.RCWE(UtilsCommons.AjaxReturnJson("1", "删除成功", deletedids));
        }
    }
}