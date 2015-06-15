using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.MAJU.BLL;
using Enow.MAJU.Utility;

namespace Enow.MAJU.Web.Manage.Topic
{
    public partial class TopicView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ManageUserAuth.ManageLoginCheck();
                string dotype = Utils.GetQueryStringValue("doType").ToLower();
                switch (dotype)
                {
                    case "view":
                        InitModel();
                        break;
                    default:
                        break;
                }

            }


        }
        /// <summary>
        /// 加载话题信息
        /// </summary>
        private void InitModel()
        {
            string id = Utils.GetQueryStringValue("id");
            if (!string.IsNullOrWhiteSpace(id))
            {
                var model = BMemberTopic.GetViewModel(id);
                if (model != null)
                {

                    ltrNickName.Text =BMember.GetUserName(model.MemberId);
                    ltrTop.Text = ((Model.EnumType.是否置顶)Convert.ToInt32(model.IsTop.ToString())).ToString();
                    ltrHot.Text = ((Model.EnumType.是否热门)Convert.ToInt32(model.IsHot.ToString())).ToString();
                    ltrState.Text = ((Model.EnumType.状态)Convert.ToInt32(model.State.ToString())).ToString();

                    ltrIssueTime.Text = model.IssueTime.ToString("yyyy-MM-dd HH:mm");
                    ltrContext.Text = model.Context;
                }
                else
                {
                    MessageBox.ShowAndReturnBack("未找到您要查看的信息!");
                }
            }
            else
            {
                MessageBox.ShowAndReturnBack("未找到您要查看的信息!");
            }

        }
    }
}