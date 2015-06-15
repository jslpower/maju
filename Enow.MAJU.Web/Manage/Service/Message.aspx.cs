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
    public partial class Message : System.Web.UI.Page
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
            string keyWords = Utils.GetQueryStringValue("keyWords");
            if (!string.IsNullOrWhiteSpace(keyWords))
            {
                keyWords = Server.UrlDecode(keyWords);
            }

            var list = BMessage.GetMessageList(ref RecordCount, PageSize, PageIndex, keyWords);
            if (list.Count > 0)
            {
                rptList.DataSource = list;
                rptList.DataBind();
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
        /// 搜索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string keyWords = this.txtKeyWords.Text;
            string uri = UtilsCommons.GetMenuUri(Request.ServerVariables["SCRIPT_NAME"]);
            Response.Redirect(uri + "&keyWords=" + Server.UrlEncode(keyWords), true);
        }
    }
}