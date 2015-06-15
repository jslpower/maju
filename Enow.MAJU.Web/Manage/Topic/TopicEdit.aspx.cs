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
    public partial class TopicEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            #region 处理AJAX请求
            string doType = Utils.GetQueryStringValue("dotype");
            if (doType == "save")
            {
                SaveData();
            }

            #endregion
            if (!IsPostBack)
            {
                ManageUserAuth.ManageIsLoginCheck();
                string Id = Utils.GetQueryStringValue("id");
                if (!string.IsNullOrWhiteSpace(Id))
                {
                    initPage(Id);

                }
                else
                {
                    Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "未找到您要查看的信息！"));
                    return;
                }
            }

        }

        /// <summary>
        /// 初始化页面
        /// </summary>
        private void initPage(string Id)
        {
            var model = BMemberTopic.GetModel(Id);
            if (model != null)
            {
                txtTitle.Text = model.Title;
                int IsEssence = 0;
                int.TryParse(model.IsEssence.ToString(), out IsEssence);
                int IsTop = 0;
                int.TryParse(model.IsTop.ToString(), out IsTop);
                int IsHot = 0;
                int.TryParse(model.IsHot.ToString(), out IsHot);
                int State = 0;
                int.TryParse(model.State.ToString(), out State);
                ddlTop.Items.FindByValue(IsTop.ToString()).Selected = true;
                ddlHot.Items.FindByValue(IsHot.ToString()).Selected = true;
                ddlState.Items.FindByValue(State.ToString()).Selected = true;
              
                ltrContent.Text = model.Context;
            }
            else
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "未找到您要查看的信息！"));
                return;
            }
        }

        /// <summary>
        /// 保存话题
        /// </summary>
        private void SaveData()
        {
            #region 保存
            string id = Utils.GetQueryStringValue("id");
            string Title = Utils.GetFormValue(txtTitle.UniqueID);
            char IsTop = char.Parse(Utils.GetFormValue(ddlTop.UniqueID));
            char IsHot = char.Parse(Utils.GetFormValue(ddlHot.UniqueID));
            char IsEssence = '0';
            char state = char.Parse(Utils.GetFormValue(ddlState.UniqueID));
            bool IsResult = false;
            if (IsHot == char.Parse(((int)Model.EnumType.是否热门.是).ToString()) && string.IsNullOrWhiteSpace(Title))
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "热门话题必须填写标题!"));
                return;
            }
           

            IsResult = BMemberTopic.Update(new tbl_MemberTopic()
            {
                TopicId = id,
                Title = Title,
                IsTop = IsTop,
                IsHot = IsHot,
                IsEssence = IsEssence,
                State = state
            });
            if (IsResult)
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("1", "操作成功!"));

            }
            else
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败!"));

            }

            #endregion
        }
    }
}