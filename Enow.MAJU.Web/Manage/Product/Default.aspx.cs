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
    public partial class Default : System.Web.UI.Page
    {
        /// <summary>
        /// 每页条数
        /// </summary>
        protected int PageSize = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["PageSize"]), RecordCount = 0, PageIndex = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            #region 处理AJAX请求
            string dotype = Utils.GetQueryStringValue("dotype").ToLower();
            switch (dotype)
            {
                case "delete":
                    DeleteProduct();
                    break;
                default:
                    break;
            }
            #endregion
            if (!IsPostBack)
            {
                ManageUserAuth.ManageLoginCheck();
                InitList();
            }
        }

        /// <summary>
        /// 加载产品列表页
        /// </summary>
        private void InitList()
        {
            #region 查询实体
            ProductSearch SearchModel = new ProductSearch();
            SearchModel.ProductName = Utils.GetQueryStringValue("name");
            SearchModel.Company = Utils.GetQueryStringValue("Company");
            SearchModel.StartDate = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("StartDate"));
            SearchModel.EndDate = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("EndDate"));
            this.txtName.Text = SearchModel.ProductName;
            if (SearchModel.StartDate.HasValue)
            {
                txtStartTime.Text = SearchModel.StartDate.Value.ToString("yyyy-MM-dd");
            }
            if (SearchModel.EndDate.HasValue)
            {
                txtEndTime.Text = SearchModel.EndDate.Value.ToString("yyyy-MM-dd");
            }
            #endregion

            PageIndex = UtilsCommons.GetPadingIndex();
            var list = BProduct.GetList(ref RecordCount, PageSize, PageIndex, SearchModel);
            if (RecordCount > 0)
            {
                rptList.DataSource = list;
                rptList.DataBind();
                ExportPageInfo1.LinkType = 3;
                ExportPageInfo1.intPageSize = PageSize;
                ExportPageInfo1.intRecordCount = RecordCount;
                ExportPageInfo1.CurrencyPage = PageIndex;
                ExportPageInfo1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"] + "?";
                ExportPageInfo1.UrlParams = Request.QueryString;
            }
            else
            {
                phNoData.Visible = true;
            }
        }

        /// <summary>
        /// 根据类别ID返回类别名称
        /// </summary>
        /// <param name="TypeId"></param>
        /// <returns></returns>
        protected string GetTypeName(string TypeId)
        {
            string RetValue = "";
            if (!string.IsNullOrWhiteSpace(TypeId))
            {
                var model = BProductType.GetModel(TypeId);
                if (model != null)
                {
                    RetValue = model.TypeName;
                }
                else
                {
                    RetValue = "";
                }
            }
            else
            {
                RetValue = "";
            }
            return RetValue;
        }

        #region 删除产品
        /// <summary>
        /// 删除产品
        /// </summary>
        private void DeleteProduct()
        {
            string Id = Utils.GetQueryStringValue("Id");
            if (string.IsNullOrWhiteSpace(Id))
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败：未选择任何要删除的信息！"));
            }
             string[] strId = Id.Split(',');
             int bllRet = 0;
             for (int i = 0; i < strId.Length; i++)
             {
                 bllRet = BProduct.Delete(strId[i]) == true ? 1 : 0;
             }
           
            if (bllRet == 1)
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("1", "操作成功!"));
            }
            else
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败：未选择任何要删除的信息！"));
            }
        }

        #endregion

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string name =this.txtName.Text;
            string startdate = this.txtStartTime.Text;
            string enddate = this.txtEndTime.Text;
            string uri = UtilsCommons.GetMenuUri(Request.ServerVariables["SCRIPT_NAME"]);
            Response.Redirect(uri + "&name=" + Server.UrlEncode(name) + "&StartDate=" + startdate + "&EndDate="+enddate, true);
        }
    }
}