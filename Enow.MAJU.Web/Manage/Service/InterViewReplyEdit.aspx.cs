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
    public partial class InterViewReplyEdit : System.Web.UI.Page
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
                BindStatus();
                InitPage();

            }
        }

        /// <summary>
        /// 加载页面
        /// </summary>
        private void InitPage()
        {
            string Id = Utils.GetQueryStringValue("Id");
            var model = BProductInterview.GetViewModel(Id);
            if (model != null)
            {
                ltrOrderNo.Text = model.OrderNo;
                ltrContactName.Text = model.MemberName;
                ltrTel.Text = model.ContactTel;
                ltrProductName.Text = model.ProductName;
                if (string.IsNullOrWhiteSpace(model.InterviewNote))
                {
                    txtInterviewNote.Text = model.ProductInterviewNote;
                }
                else
                {
                    txtInterviewNote.Text = model.InterviewNote;
                }
                ddlStatus.Items.FindByValue(model.Status.ToString()).Selected = true;
                txtContext.Text = model.Context;
                txtAddress.Text = model.OrderPlace;
                txtManager.Text = model.Manager;
                txtManagerTel.Text = model.ManagerMob;
                txtTime.Text = Convert.ToDateTime(model.OrderTime.ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                ltrTarget.Text = model.InterviewTarget;
            }
        }

        #region
        /// <summary>
        /// 绑定预约状态
        /// </summary>
        private void BindStatus()
        {
            foreach (var e in Enum.GetValues(typeof(Model.EnumType.面签状态)))
            {
                ddlStatus.Items.Add(new ListItem() { Value = ((int)e).ToString(), Text = Enum.GetName(typeof(Model.EnumType.面签状态), e) });
            }
        }
        #endregion

        #region 保存

        private void SaveData()
        {
            #region 取值
            string Id = Utils.GetQueryStringValue("Id");
            string ManagerName = Utils.GetFormValue(txtManager.UniqueID);
            string ManagerMob = Utils.GetFormValue(txtManagerTel.UniqueID);
            DateTime? OrderTime = Utils.GetDateTimeNullable(Utils.GetFormValue(txtTime.UniqueID));
            string OrderPlace = Utils.GetFormValue(txtAddress.UniqueID);
            byte Status = byte.Parse(Utils.GetFormValue(ddlStatus.UniqueID));
            string InterViewNote = Utils.GetFormValue(txtInterviewNote.UniqueID);
            string Context = Utils.GetFormValue(txtContext.UniqueID);
            if (string.IsNullOrWhiteSpace(Context))
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "请填写预约反馈!"));
                return;
            }
            var model = BProductInterview.GetModel(Id);
            if (model != null)
            {
                model.OrderTime = OrderTime;
                model.OrderPlace = OrderPlace;
                model.Manager = ManagerName;
                model.ManagerMob = ManagerMob;
                model.Context = Context;
                model.InterviewNote = InterViewNote;
                model.Status = Status;
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

            #endregion
        }
        #endregion
    }
}