using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.MAJU.BLL;
using Enow.MAJU.Model;
using Enow.MAJU.Utility;

namespace Enow.MAJU.WebApp
{
    public partial class fanganDetail :BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoginCheck();
            if (!IsPostBack)
            {
                string Id = Utils.GetQueryStringValue("Id");
                InitData(Id);
            }
        }

        private void InitData(string Id)
        {
            var model = BProductSolution.GetViewModel(Id);
            if (model!=null)
            {
                ltrFanganName.Text = model.TargetName + "的" + model.ProductName;
                ltrTargetName.Text = model.TargetName;
                ltrAge.Text = model.Age.ToString();
                ltrGender.Text = model.Gender.ToString() == "1" ? "女" : "男";
                ltrRevenue.Text = ((Model.EnumType.家庭年收入)int.Parse(model.Revenue.ToString())).ToString();
                ltrEmail.Text = model.Email.ToString();
                ltrState.Text = ((Model.EnumType.方案状态)int.Parse(model.State.ToString())).ToString();
            }
        }
    }
}