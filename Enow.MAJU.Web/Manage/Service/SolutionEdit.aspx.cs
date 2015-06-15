using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.MAJU.BLL;
using Enow.MAJU.Utility;
using Enow.MAJU.Model;

namespace Enow.MAJU.Web.Manage.Service
{
    public partial class SolutionEdit : System.Web.UI.Page
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

        private void InitData(string Id)
        {
            #region 绑定方案状态
            foreach (var item in Enum.GetValues(typeof(Model.EnumType.方案状态)))
            {
                ddlState.Items.Add(new ListItem() { Value = ((int)item).ToString(), Text = Enum.GetName(typeof(Model.EnumType.方案状态), item) });
            }
            #endregion
            var model = BProductSolution.GetViewModel(Id);
            if (model != null)
            {
                ltrFanganName.Text = model.TargetName + "的" + model.ProductName;
                ltrProductName.Text = model.ProductName;
                ltrTargetName.Text = model.TargetName;
                ltrGender.Text = model.Gender.ToString() == "1" ? "女" : "男";
                ltrAge.Text = model.Age.ToString();
                int Revenue = 0;
                int.TryParse(model.Revenue.ToString(), out Revenue);
                ltrRevenue.Text = ((Model.EnumType.家庭年收入)Revenue).ToString();
                ltrMobile.Text = model.Mobile.ToString();
                ddlState.Items.FindByValue(model.State.ToString()).Selected = true;
            }
        }

        private void SaveData()
        {
            string Id = Utils.GetQueryStringValue("Id");
            byte State = byte.Parse(Utils.GetFormValue(ddlState.UniqueID));

            bool ret = BProductSolution.UpdateState(Id, State);
            if (ret)
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("1", "方案状态修改成功!"));
            }
            else
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "方案状态修改失败!"));
            }

        }
    }
}