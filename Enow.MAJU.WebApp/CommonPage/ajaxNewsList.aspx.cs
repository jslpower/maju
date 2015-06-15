using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.MAJU.BLL;
using Enow.MAJU.Model;
using Enow.MAJU.Utility;

namespace Enow.MAJU.WebApp.CommonPage
{
    public partial class ajaxNewsList : System.Web.UI.Page
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
            initList();
        }

        private void initList()
        {
            var list = BNews.GetList(ref RecordCount, PageSize, PageIndex, new Model.NewsSearch() { State = Model.EnumType.状态.正常 });
            if (RecordCount > 0)
            {
                rpt_list.DataSource = list;
                rpt_list.DataBind();
            }
        }
    }
}