using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.MAJU.BLL;
using Enow.MAJU.Model;
using Enow.MAJU.Utility;

namespace Enow.MAJU.Web.Manage.Topic
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
                    DeleteTopic();
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
        /// 删除话题
        /// </summary>
        private void DeleteTopic()
        {
            string Id = Utils.GetQueryStringValue("Id");
            if (string.IsNullOrWhiteSpace(Id))
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败：未选择任何要删除的信息！"));
            }
            int bllRetCode = 0;
             string[] strId = Id.Split(',');
             for (int i = 0; i < strId.Length; i++)
             {
                 bllRetCode = BMemberTopic.Delete(strId[i]) == true ? 1 : 99;
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

        /// <summary>
        /// 加载话题列表
        /// </summary>
        private void InitList()
        {
            #region  查询实体
            TopicSearch SearchModel = new TopicSearch();
            SearchModel.Title = Utils.GetQueryStringValue("Title");
            SearchModel.Context = Utils.GetQueryStringValue("Context");
            int IsHot = Utils.GetInt(Utils.GetQueryStringValue("IsHot"), -1);
            int IsTop = Utils.GetInt(Utils.GetQueryStringValue("IsTop"), -1);
            int IsEssence = Utils.GetInt(Utils.GetQueryStringValue("IsEssence"), -1);
            if (IsHot > -1)
            {
                SearchModel.IsHot = (Model.EnumType.是否热门)IsHot;
            }
            if (IsTop > -1)
            {
                SearchModel.IsTop = (Model.EnumType.是否置顶)IsTop;
            }
            if (IsEssence>-1)
            {
                SearchModel.IsEssence = (Model.EnumType.是否精华)IsEssence;
            }
            SearchModel.StartDate = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("startDate"));
            SearchModel.EndDate = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("endDate"));
            #endregion 
            int RowCount = 0;
            int pageIndex = UtilsCommons.GetPadingIndex();
            List<dt_MemberTopic> list = BMemberTopic.GetList(ref RowCount, PageSize, pageIndex, SearchModel);
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
    }
}