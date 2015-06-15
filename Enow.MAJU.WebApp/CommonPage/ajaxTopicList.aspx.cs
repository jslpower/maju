using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.MAJU.BLL;
using Enow.MAJU.Utility;

namespace Enow.MAJU.WebApp.CommonPage
{
    public partial class ajaxTopicList : System.Web.UI.Page
    {
        /// <summary>
        /// 分页数以及页码
        /// </summary>
        protected int PageSize = 10, RecordCount = 0;

        protected int PageIndex = Utils.GetInt(Utils.GetQueryStringValue("page"), 1);
        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitList();
            }
        }

        /// <summary>
        /// 加载话题列表
        /// </summary>
        private void InitList()
        {
            Model.TopicSearch SearchModel = new Model.TopicSearch();
            SearchModel.isView = true;
            var list = BMemberTopic.GetList(ref RecordCount, PageSize, PageIndex, SearchModel);
            if (RecordCount > 0)
            {
                rptList.DataSource = list;
                rptList.DataBind();
            }

        }
    }
}