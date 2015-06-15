using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.MAJU.BLL;
using Enow.MAJU.Utility;
using System.Text;

namespace Enow.MAJU.WebApp
{
    public partial class NewsList : System.Web.UI.Page
    {

        /// <summary>
        /// 分页数以及页码
        /// </summary>
        protected int PageSize = 10, PageIndex = 1, RecordCount = 0;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                InitList();
            }
        }



        /// <summary>
        /// 绑定列表条导航图片
        /// </summary>
        private void InitPhoto()
        {
            var list = BAdv.GetList(ref RecordCount, 5, 1, new Model.AdvSearch() { Type = Model.EnumType.导航条位置.资讯 });

            Utils.RCWE(Newtonsoft.Json.JsonConvert.SerializeObject(list));
        }

        /// <summary>
        /// 绑定资讯列表
        /// </summary>
        private void InitList()
        {
            PageIndex = UtilsCommons.GetPadingIndex();
            var list = BNews.GetList(ref RecordCount, PageSize, PageIndex, new Model.NewsSearch() { State = Model.EnumType.状态.正常 });
            if (RecordCount > 0)
            {
                rpt_list.DataSource = list;
                rpt_list.DataBind();
            }
        }
    }
}