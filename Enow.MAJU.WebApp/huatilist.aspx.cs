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
    public partial class huatilist : System.Web.UI.Page
    {
        /// <summary>
        /// 分页数以及页码
        /// </summary>
        protected int PageSize = 10, PageIndex = 1, RecordCount = 0;
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
            if (BackPage.IsLoginCheck())
            {
                PHadd.Visible = true;
            }
            else
            {
                PHadd.Visible = false;
            }
        }

        /// <summary>
        /// 加载话题列表
        /// </summary>
        private void InitList()
        {
            Model.TopicSearch SearchModel = new Model.TopicSearch();

            SearchModel.State = Model.EnumType.状态.正常;
            string Page = Request.QueryString["Page"];
            if (!string.IsNullOrWhiteSpace(Page) && StringValidate.IsInteger(Page))
            {
                int.TryParse(Page, out PageIndex);
                if (PageIndex < 1)
                {
                    PageIndex = 1;
                }
            }

            var list = BMemberTopic.GetList(ref RecordCount, PageSize, PageIndex, SearchModel);
            if (RecordCount > 0)
            {
                rptList.DataSource = list;
                rptList.DataBind();
            }

        }
     
    }
}