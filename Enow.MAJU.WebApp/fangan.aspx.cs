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
    public partial class fangan : BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Utils.GetQueryStringValue("dotype") == "save") Save();
            if (!IsPostBack)
            {
                InitData();
            }
        }
        void InitData()
        {
            var a = 1;
            while (a <= 60)
            {
                selAge.Items.Add(new ListItem() { Value = a.ToString(), Text = a.ToString() });
                a++;
            }
            selAge.DataBind();
            foreach (var e in Enum.GetValues(typeof(Model.EnumType.家庭年收入)))
            {
                selRevenue.Items.Add(new ListItem() { Value = ((int)e).ToString(), Text = Enum.GetName(typeof(Model.EnumType.家庭年收入), e) });
            }
            selRevenue.DataBind();
            foreach (var e in Enum.GetValues(typeof(Model.EnumType.性别)))
            {
                selGender.Items.Add(new ListItem() { Value = ((int)e).ToString(), Text = Enum.GetName(typeof(Model.EnumType.性别), e) });
            }
            selGender.DataBind();
            var u = GetUserModel();
            if (u != null)
            {
                txtTargetName.Value = u.NickName;
                selGender.SelectedIndex = (int)u.Gender;
                if (u.Age>0)
                {
                    
                    selAge.Items.FindByValue(u.Age.ToString()).Selected = true;
                }
                selRevenue.SelectedIndex = (int)u.Revenue;
                txtEmail.Value = u.Email;
            }
            string ProductId = Utils.GetQueryStringValue("ProductId");
            if (!string.IsNullOrWhiteSpace(ProductId))
            {
                var model = BProduct.GetModel(ProductId);
                if (model!=null)
                {
                    ltrFanganName.Text = model.ProductName;
                }
            }
        }
        void Save()
        {
            var r = BProductSolution.Add(new tbl_ProductSolution() {
                SolutionId=Guid.NewGuid().ToString(),
                ProductId = Utils.GetQueryStringValue("ProductId"),
                MemberId=GetUserModel().MemberId,
                TargetName=Utils.GetFormValue(txtTargetName.UniqueID),
                Gender=byte.Parse( Utils.GetFormValue(selGender.UniqueID)),
                Age=byte.Parse(Utils.GetFormValue(selAge.UniqueID)),
                Revenue=byte.Parse(Utils.GetFormValue(selRevenue.UniqueID)),
                Email=Utils.GetFormValue(txtEmail.UniqueID),
                State=byte.Parse(((int)Model.EnumType.方案状态.待发送).ToString()),
                IssueTime=DateTime.Now
            });
            Utils.RCWE(UtilsCommons.AjaxReturnJson(r ? "1" : "0", r ? "方案保存成功" : "方案保存失败"));
        }
    }
}