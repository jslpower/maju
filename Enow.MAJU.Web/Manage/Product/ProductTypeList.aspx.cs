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
    public partial class ProductTypeList : System.Web.UI.Page
    {
        /// <summary>
        /// 每页条数
        /// </summary>
        protected int PageSize = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["PageSize"]), RecordCount = 0, PageIndex = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            #region 处理AJAX请求
            string dotype = Utils.GetQueryStringValue("doType").ToLower();
            switch (dotype)
            {
                case "delete":
                    DeleteType();
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
        /// 加载产品分类列表
        /// </summary>
        private void InitList()
        {
            string KeyWords = Utils.GetQueryStringValue("keyWords");
            this.txtKeyWords.Text = KeyWords;
            PageIndex = UtilsCommons.GetPadingIndex();
            var list = BProductType.GetList(ref RecordCount, PageSize, PageIndex, KeyWords);
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

        #region 删除产品类别

        private void DeleteType()
        {
            string Id = Utils.GetQueryStringValue("id");
            if (string.IsNullOrWhiteSpace(Id))
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败：未选择任何要删除的信息！"));
            }
             string[] strId = Id.Split(',');
             int bllRet = 0;
             for (int i = 0; i < strId.Length; i++)
             {
                 bllRet = BProductType.Delete(strId[i]) == true ? 1 : 0;
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
            string keyWords = this.txtKeyWords.Text;
            string uri = UtilsCommons.GetMenuUri(Request.ServerVariables["SCRIPT_NAME"]);
            Response.Redirect(uri + "&keyWords=" + Server.UrlEncode(keyWords), true);
        }
    }
}