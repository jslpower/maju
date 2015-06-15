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
    public partial class NewsView : System.Web.UI.Page
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
        /// 加载资讯实体
        /// </summary>
        private void InitModel()
        {
            string Id = Utils.GetQueryStringValue("id");
            if (!string.IsNullOrWhiteSpace(Id))
            {
                var model = BNews.GetModel(Id);
                if (model != null)
                {
                    ltrTitle.Text = model.Title;
                    ltrOperatorName.Text = model.OperatorName;
                    ltrIssueTime.Text = model.IssueTime.ToString("yyyy-MM-dd");
                    //if (!string.IsNullOrWhiteSpace(model.PhotoPath))
                    //{
                    //    ltrPhotoPath.Text = "<img src=" + model.PhotoPath + " width=\"300\" height=\"300\"/>";
                    //}
                    ltrTop.Text = ((Model.EnumType.是否置顶)Convert.ToInt32(model.IsTop.ToString())).ToString();
                    ltrHot.Text = ((Model.EnumType.是否热门)Convert.ToInt32(model.IsHot.ToString())).ToString();
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