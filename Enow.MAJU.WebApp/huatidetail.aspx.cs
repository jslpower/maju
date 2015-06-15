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
    public partial class huatidetail : System.Web.UI.Page
    {
        /// <summary>
        /// 分页数以及页码
        /// </summary>
        protected int PageSize = 10, PageIndex = 1, RecordCount = 0;
        protected string TopicId = "";
        protected Model.EnumType.收藏类别 T = (Model.EnumType.收藏类别)Utils.GetInt(Utils.GetQueryStringValue("t"), 0);
        protected void Page_Load(object sender, EventArgs e)
        {
            #region 收藏
            if (Utils.GetQueryStringValue("type") == "collect")
            {
                huatiCollect();
            }


            #endregion
           
                TopicId = Utils.GetQueryStringValue("Id");
                if (!string.IsNullOrWhiteSpace(TopicId))
                {
                    InitPage(TopicId);
                }
                else
                {
                    MessageBox.ShowAndRedirect("话题不存在或已删除!", "huatilist.aspx");
                }
            
        }

        /// <summary>
        /// 收藏
        /// </summary>
        private void huatiCollect()
        {
            string Id = Utils.GetQueryStringValue("Id");
            var MemberModel = BackPage.GetUserModel();
            if (MemberModel == null)
                Utils.RCWE(UtilsCommons.AjaxReturnJson("-2", "请先登录再收藏"));
            if (BMemberCollect.IsExists((int)T, Id, MemberModel.MemberId))
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "已收藏"));
            }
            else
            {
                var model = new MemberCollect()
                {
                    CollectId = Guid.NewGuid().ToString(),
                    RelatedType = (byte)T,
                    RelatedId = Id,
                    MemberId = MemberModel.MemberId,
                    IssueTime = DateTime.Now
                };
                if (BMemberCollect.Add(model))
                {
                    BMemberCollect.UpdateCollects(Id, T, Model.EnumType.操作符号.加);
                    Utils.RCWE(UtilsCommons.AjaxReturnJson("1", "收藏成功"));
                }
                else
                {
                    Utils.RCWE(UtilsCommons.AjaxReturnJson("-1", "收藏失败"));
                }
            }
        }


        #region 绑定话题详情
        /// <summary>
        /// 绑定话题详情
        /// </summary>
        /// <param name="Id"></param>
        private void InitPage(string Id)
        {
            var model = BMemberTopic.GetViewModel(Id);
            if (model != null)
            {
                if (!string.IsNullOrWhiteSpace(model.HeadPhoto))
                {
                    ltrImg.Text = "<img src='" + model.HeadPhoto + "' />";
                }
                else
                {
                    ltrImg.Text = "<img src='images/touxian48.png' />";
                }
                ltrTitle.Text = BMember.GetUserName(model.MemberId);
                ltrAuthor.Text = model.IssueTime.ToString("yyyy-MM-dd HH:mm");
                ltrContent.Text = model.Context;
                //话题点击数加一
                BMemberTopic.UpdateNumber(Id, Model.EnumType.更新类别.点击量, Model.EnumType.操作符号.加);
                var u = BackPage.GetUserModel();
                if (u != null)
                    BMemberTopicReply.UpdateRead(Id, u.MemberId);

            }
            else
            {
                MessageBox.ShowAndRedirect("话题不存在或已删除!", "huatilist.aspx");
            }
            InitReply(Id);
        }

        #endregion

        #region 话题评论
        /// <summary>
        /// 绑定用户评论
        /// </summary>
        /// <param name="Id"></param>
        private void InitReply(string Id)
        {
            Model.ReplySearch searchModel = new Model.ReplySearch();
            searchModel.TopicId = Id;
            PageIndex = Utils.GetInt(Utils.GetQueryStringValue("Page"));
            if (PageIndex == 0)
            {
                PageIndex = 1;
            }
            var list = BMemberTopicReply.GetViewList(ref RecordCount, PageSize, PageIndex, searchModel);
            if (RecordCount > 0)
            {
                rptList.DataSource = list;
                rptList.DataBind();
            }
        }
        #endregion
    }
}