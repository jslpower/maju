using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.MAJU.BLL;
using Enow.MAJU.Model;
using Enow.MAJU.Utility;

namespace Enow.MAJU.Web.Manage.Service
{
    public partial class InterViewEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            #region 处理AJAX请求
            if (Utils.GetQueryStringValue("dotype") == "save")
            {
                SaveData();
            }
            #endregion
            if (!IsPostBack)
            {
                ManageUserAuth.ManageLoginCheck();
                string Id = Utils.GetQueryStringValue("Id");
                InitData(Id);
            }
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        /// <param name="Id"></param>
        private void InitData(string Id)
        {
            var model = BProductInterview.GetViewModel(Id);
            if (model != null)
            {
                ltrCustomerName.Text = model.InterviewTarget;
                ltrTel.Text = model.ContactTel;
                ltrSolutionName.Text = model.MemberName + "的" + model.ProductName;
                ltrProductName.Text = model.ProductName;
                ltrCompanyName.Text = model.Company;
                ltrTarget.Text = model.Target;
                ltrAdvantage.Text= model.Advantage;
                ltrOtherInfo.Text = model.OtherInfo;
                ltrRelatedFile.Text = model.RelatedFile;
                if (string.IsNullOrWhiteSpace(model.InterviewNote))
                {
                    txtInterviewNote.Text = model.ProductInterviewNote;
                }
                else
                {
                    txtInterviewNote.Text = model.InterviewNote;
                }
            }
            else
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "未找到您要查看的信息！"));
                return;
            }
        }
        /// <summary>
        /// 保存
        /// </summary>
        private void SaveData()
        {
            string Id = Utils.GetQueryStringValue("Id");
            string InterviewNote = Utils.GetFormValue(txtInterviewNote.UniqueID);
            if (string.IsNullOrWhiteSpace(InterviewNote))
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "请填写面签注意事项!"));
                return;
            }
            var model = BProductInterview.GetModel(Id);
            if (model != null)
            {
                model.InterviewNote = InterviewNote;

                bool ret = BProductInterview.Update(model);
                if (ret)
                {
                    Utils.RCWE(UtilsCommons.AjaxReturnJson("1", "操作成功!"));
                }
                else
                {
                    Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败!"));
                }
            }
            else
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("-1", "数据异常，请重新提交!"));
            }
        }
    }
}