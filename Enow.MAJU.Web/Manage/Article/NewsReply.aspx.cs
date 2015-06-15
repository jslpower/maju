using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.MAJU.BLL;
using Enow.MAJU.Model;
using Enow.MAJU.Utility;

namespace Enow.MAJU.Web.Manage.Article
{
    public partial class NewsReply : System.Web.UI.Page
    {
        protected int PageSize = 20,PageIndex=0,RecordCount=0;
        protected void Page_Load(object sender, EventArgs e)
        {
            #region 处理Ajax请求
            string dotype = Utils.GetQueryStringValue("dotype").ToLower();
            switch (dotype)
            {
                case "delete":
                    DeleteReply();
                    break;
                default:
                    break;
            }
            #endregion
            if (!IsPostBack)
            {
                ManageUserAuth.ManageLoginCheck();
            
            }
            InitList();
           
        }

        #region 加载回复列表
        private void InitList()
        {
            #region 查询实体
            NewsReplySearch SearchModel = new NewsReplySearch();
         
            
                SearchModel.StartDate = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("startDate"));
                SearchModel.EndDate = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("endDate"));
                SearchModel.KewWords = Utils.GetQueryStringValue("Keywords");
                SearchModel.NewsTitle = Utils.GetQueryStringValue("Title");
            
            PageIndex = UtilsCommons.GetPadingIndex();
            var list = BNewsReply.GetList(ref RecordCount, PageSize, PageIndex, SearchModel);
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
            
            #endregion
        }
        #endregion

        #region 删除用户回复
        private void DeleteReply()
        {
            string Id = Utils.GetQueryStringValue("Id");
            int bllRetCode = 0;
            if (string.IsNullOrWhiteSpace(Id))
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败：未选择任何要删除的信息！"));
            }
            else
            {
                string[] strId = Id.Split(',');
                for (int i = 0; i < strId.Length; i++)
                {
                    bllRetCode = BNewsReply.Delete(strId[i]) == true ? 1 : 99;
                }
                if (bllRetCode == 1)
                {
                    Utils.RCWE(UtilsCommons.AjaxReturnJson("1", "操作成功!"));
                }
                else
                {
                    Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败：未选择任何要删除的信息！"));
                }
            }
        }
        #endregion 
    }
}