using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Enow.MAJU.BLL;
using Enow.MAJU.Model;
using Enow.MAJU.Utility;

namespace Enow.MAJU.WebApp
{
    public partial class zixun : BackPage
    {
        /// <summary>
        /// 分页数以及页码
        /// </summary>
        protected int PageSize = 10, PageIndex = 1, RecordCount = 0;

        //图片裁剪后保存的文件夹
        protected const string DIRPATH = "/ufiles/";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitPage();
            }
           
        }

        private void InitPage()
        {
            #region 绑定导航图片
            var list = BAdv.GetList(ref RecordCount, 5, 1, new Model.AdvSearch() { Type = Model.EnumType.导航条位置.资讯 });
            
            if (list.Count>0)
            {
                rptImgList.DataSource = list;
                rptImgList.DataBind();
            }
            #endregion

            #region 绑定资讯列表
            PageIndex = UtilsCommons.GetPadingIndex();
            var Newslist = BNews.GetList(ref RecordCount, PageSize, PageIndex, new Model.NewsSearch() { State = Model.EnumType.状态.正常 });
            if (Newslist.Count>0)
            {
                rpt_list.DataSource = Newslist;
                rpt_list.DataBind();
            }
            #endregion
        }
    }
}