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
    public partial class NewsDetail : System.Web.UI.Page
    {
        /// <summary>
        /// 分页数以及页码
        /// </summary>
        protected int PageSize = 10, PageIndex = 1, RecordCount = 0;
        protected string NewsId = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            #region  收藏
            if (Utils.GetQueryStringValue("type") == "collect")
            {
                NewsCollect();
            }

            #endregion
            if (!IsPostBack)
            {
                NewsId = Utils.GetQueryStringValue("Id");
                if (!string.IsNullOrWhiteSpace(NewsId))
                {
                    InitPage(NewsId);
                }
                else
                {
                    MessageBox.ShowAndRedirect("资讯不存在或已删除!", "huatilist.aspx");
                }
            }
        }

        /// <summary>
        /// 收藏
        /// </summary>
        private void NewsCollect()
        {
            string Id = Utils.GetQueryStringValue("Id");
            var MemberModel = BackPage.GetUserModel();
            if (MemberModel==null)
                Utils.RCWE(UtilsCommons.AjaxReturnJson("-2", "请先登录"));
            if (BMemberCollect.IsExists((int)Model.EnumType.收藏类别.资讯, Id, MemberModel.MemberId))
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "此资讯已收藏"));
            }
            else
            {
                var model = new MemberCollect()
                {
                    CollectId = Guid.NewGuid().ToString(),
                    RelatedType = (int)Model.EnumType.收藏类别.资讯,
                    RelatedId = Id,
                    MemberId = MemberModel.MemberId,
                    IssueTime = DateTime.Now
                };
                if (BMemberCollect.Add(model))
                {
                    BMemberCollect.UpdateCollects(Id, Model.EnumType.收藏类别.资讯, Model.EnumType.操作符号.加);
                    Utils.RCWE(UtilsCommons.AjaxReturnJson("1", "已收藏"));
                }
                else
                {
                    Utils.RCWE(UtilsCommons.AjaxReturnJson("-1", "收藏失败"));
                }
            }
        }

        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="NewsId"></param>
        private void InitPage(string NewsId)
        {
            #region 资讯详细
            var model = BNews.GetModel(NewsId);
            if (model != null)
            {
                ltrTitle.Text = model.Title;
                ltrContent.Text = model.Context;
                ltrIssueTime.Text = model.IssueTime.ToString("yyyy-MM-dd HH:mm");
                //资讯点击数加一
                BNews.UpdateNumber(NewsId, Model.EnumType.更新类别.点击量, Model.EnumType.操作符号.加);

                InitReply(NewsId);
            }
            else
            {
                MessageBox.ShowAndRedirect("资讯不存在或已删除!", "huatilist.aspx");
            }
            #endregion
        }

        /// <summary>
        /// 资讯评论
        /// </summary>
        /// <param name="NewsId"></param>
        private void InitReply(string NewsId)
        {
            PageIndex = UtilsCommons.GetPadingIndex("Page");
            if (PageIndex == 0)
            {
                PageIndex = 1;
            }
            var list = BNewsReply.GetList(ref RecordCount, PageSize, PageIndex, new Model.NewsReplySearch() { NewsId = NewsId });
            if (RecordCount > 0)
            {
                rptlist.DataSource = list;
                rptlist.DataBind();
            }

        }

    }
}