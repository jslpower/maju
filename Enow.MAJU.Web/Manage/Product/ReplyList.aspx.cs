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
    public partial class ReplyList : System.Web.UI.Page
    {
        /// <summary>
        /// 数据分页
        /// </summary>
        protected int PageSize = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["PageSize"]);
        protected int PageIndex = UtilsCommons.GetPadingIndex(), RecordCount = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ManageUserAuth.ManageLoginCheck();
                InitList();
            }
        }

        /// <summary>
        /// 加载列表
        /// </summary>
        private void InitList()
        {
            #region 查询实体
            ProductReplySearch searchModel = new ProductReplySearch();
            searchModel.KeyWords =Server.UrlDecode(Utils.GetQueryStringValue("KeyWords"));
            txtKeyWord.Text = searchModel.KeyWords;
            searchModel.Context = Server.UrlDecode(Utils.GetQueryStringValue("Context"));
            txtContext.Text = searchModel.Context;
            searchModel.StartDate = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("Sdate"));
            if (searchModel.StartDate.HasValue)
            {
                txtStartTime.Text = searchModel.StartDate.Value.ToString("yyyy-MM-dd");
            }
            searchModel.EndDate = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("Edate"));
            if (searchModel.EndDate.HasValue)
            {
                txtEndTime.Text = searchModel.EndDate.Value.ToString("yyyy-MM-dd");
            }
            
            #endregion 

            var list = BProductReply.GetViewList(ref RecordCount, PageSize, PageIndex, searchModel);
            if (list.Count > 0)
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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
          
            string KeyWords = this.txtKeyWord.Text;
            string Context = this.txtContext.Text;
            DateTime? StartDate = Utils.GetDateTimeNullable(Utils.GetFormValue(txtStartTime.UniqueID));
            DateTime? EndDate = Utils.GetDateTimeNullable(Utils.GetFormValue(txtEndTime.UniqueID));
            string uri = UtilsCommons.GetMenuUri(Request.ServerVariables["SCRIPT_NAME"]);
            Response.Redirect(uri + "&keyWords=" + Server.UrlEncode(KeyWords) + "&Sdate=" + StartDate + "&Edate=" + EndDate + "&context=" + Server.UrlEncode(Context) + "", true);
        }

       
    }
}