using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.MAJU.BLL;
using Enow.MAJU.Model;
using Enow.MAJU.Utility;

namespace Enow.MAJU.Web.Manage.Service
{
    public partial class Solution : System.Web.UI.Page
    {
        /// <summary>
        /// 每页条数
        /// </summary>
        protected int PageSize = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["PageSize"]), RecordCount = 0, PageIndex = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            #region 处理AJAX请求
            if (Utils.GetQueryStringValue("doType") == "delete")
            {
                DelData();
            }
            #endregion
            if (!IsPostBack)
            {
                ManageUserAuth.ManageLoginCheck();


            }
            InitList();
        }

        /// <summary>
        /// 删除方案
        /// </summary>
        private void DelData()
        {
            string Id = Utils.GetQueryStringValue("Id");
            if (string.IsNullOrWhiteSpace(Id))
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败：未选择任何要删除的信息！"));
            }
            string[] strId = Id.Split(',');
            bool bllRet = false;
            for (int i = 0; i < strId.Length; i++)
            {
                bllRet = BProductSolution.Delete(strId[i]);
            }

            if (bllRet)
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("1", "操作成功!"));
            }
            else
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败：未选择任何要删除的信息！"));
            }
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        private void InitList()
        {
            PageIndex = UtilsCommons.GetPadingIndex();
            var list = BProductSolution.GetViewtList(ref RecordCount, PageSize, PageIndex, GetSearchModel());
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
        /// 构造查询实体
        /// </summary>
        /// <returns></returns>
        private ProductSolutionSearch GetSearchModel()
        {
            ProductSolutionSearch SearchModel = new ProductSolutionSearch();
            string KeyWords = Utils.GetQueryStringValue("KeyWords");
            DateTime? IssueBeginTime = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("startDate"));
            DateTime? IssueEndTime = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("endDate"));
            if (!string.IsNullOrWhiteSpace(KeyWords))
            {
                SearchModel.KeyWords = KeyWords;
                txtKeyWords.Text = KeyWords;
            }
            if (IssueBeginTime.HasValue)
            {
                SearchModel.IssueBeginTime = IssueBeginTime;
                txtIBeginDate.Text = Convert.ToDateTime(IssueBeginTime.ToString()).ToString("yyyy-MM-dd");

            }
            if (IssueEndTime.HasValue)
            {
                SearchModel.IssueEndTime = IssueEndTime;
                txtIEndDate.Text = Convert.ToDateTime(IssueEndTime.ToString()).ToString("yyyy-MM-dd");
            }
            return SearchModel;

        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string KeyWords = Utils.GetFormValue(txtKeyWords.UniqueID);
            string startDate = Utils.GetFormValue(txtIBeginDate.UniqueID);
            string endDate = Utils.GetFormValue(txtIEndDate.UniqueID);
            string uri = UtilsCommons.GetMenuUri(Request.ServerVariables["SCRIPT_NAME"]);
            Response.Redirect(uri + "&KeyWords=" + KeyWords + "&StartDate=" + startDate + "&EndDate=" + endDate, true);
        }

    }
}