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
    public partial class Default : System.Web.UI.Page
    {
        /// <summary>
        /// 每页条数
        /// </summary>
        protected int PageSize = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["PageSize"]);
        protected void Page_Load(object sender, EventArgs e)
        {
            #region 处理Ajax请求
            string dotype = Utils.GetQueryStringValue("dotype").ToLower();
            switch (dotype)
            {
                case "delete":
                    DeleteArticle();
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


        /// <summary>
        /// 加载资讯列表
        /// </summary>
        private void InitList()
        {
            #region 查询实体
            NewsSearch searchModel = new NewsSearch();
            searchModel.Title = Utils.GetQueryStringValue("Title");
            int IsHot = Utils.GetInt(Utils.GetQueryStringValue("IsHot"), -1);
            int IsTop = Utils.GetInt(Utils.GetQueryStringValue("IsTop"), -1);
            int IsEssence =-1;
            if (IsHot>-1)
            {
                searchModel.IsHot = (Model.EnumType.是否热门)IsHot;
            }
            if (IsTop>-1)
            {
                searchModel.IsTop = (Model.EnumType.是否置顶)IsTop;
            }
            if (IsEssence>-1)
            {
                searchModel.IsEssence = (Model.EnumType.是否精华)IsEssence;
            }
            searchModel.StartDate = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("startDate"));
            searchModel.EndDate = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("endDate"));
            #endregion

            int RowCount = 0;
            int pageIndex = UtilsCommons.GetPadingIndex();
            List<tbl_News> list = BNews.GetList(ref RowCount, PageSize, pageIndex, searchModel);
            if (RowCount > 0)
            {
                rptList.DataSource = list;
                rptList.DataBind();
                ExportPageInfo1.LinkType = 3;
                ExportPageInfo1.intPageSize = PageSize;
                ExportPageInfo1.intRecordCount = RowCount;
                ExportPageInfo1.CurrencyPage = pageIndex;
                ExportPageInfo1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"] + "?";
                ExportPageInfo1.UrlParams = Request.QueryString;
            }
            else
            {
                phNoData.Visible = true;
            }
        }

        /// <summary>
        /// 删除文章
        /// </summary>
        private void DeleteArticle()
        {
            string Id = Utils.GetQueryStringValue("Id");
            if (string.IsNullOrWhiteSpace(Id))
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败：未选择任何要删除的信息！"));
            }
            string[] strId = Id.Split(',');
             int bllRetCode=0;
            for (int i = 0; i < strId.Length; i++)
            {

                bllRetCode = BNews.Delete(strId[i]) == true ? 1 : 99;
            }
           
            if (bllRetCode==1)
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("1","操作成功!"));
            }
            else
            {
            Utils.RCWE(UtilsCommons.AjaxReturnJson("0","操作失败：未选择任何要删除的信息！"));
            }
        }

    }
}