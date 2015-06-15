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
    public partial class InsuranceInterviewDetail : BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoginCheck();
            if (!IsPostBack)
            {
                InitPage();
            }
        }
        void InitPage()
        {
            var m = BProductInterview.GetViewModel(Utils.GetQueryStringValue("id"));
            if (m != null)
            {
                ltStatus.Text = ((Model.EnumType.面签状态)Utils.GetInt(m.Status.ToString())).ToString();
                ltOrderNo.Text = m.OrderNo;
                ltInterviewTarget.Text = m.InterviewTarget;
                ltProductName.Text = m.ProductName;
                ltOrderTime.Text = m.OrderTime.HasValue ? m.OrderTime.Value.ToString("yyyy-MM-dd HH:mm:ss") : string.Empty;
                ltOrderPlace.Text = m.OrderPlace;
                ltContactTel.Text = m.ContactTel;
                ltManager.Text = m.Manager;
                ltManagerMob.Text = m.ManagerMob;
                ltInterviewNote.Text = m.InterviewNote;
            }
        }
    }
}