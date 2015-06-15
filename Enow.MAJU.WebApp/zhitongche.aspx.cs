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
    public partial class zhitongche : BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoginCheck();
            if (Utils.GetQueryStringValue("dotype") == "save") Save();
            if (!IsPostBack)
            {
                InitData();
            }
        }
        void InitData()
        {
            var m = BProductSolution.GetViewModel(Utils.GetQueryStringValue("id"));
            if (m != null)
            {
                txtInterviewTarget.Value = m.TargetName;
                txtFangAn.Value = m.TargetName + "的" + m.ProductName;
                txtContactTel.Value = GetUserModel().Mobile;
                litInterviewNote.Text = m.InterviewNote;
            }
        }
        void Save()
        {
             var m = BProductSolution.GetViewModel(Utils.GetQueryStringValue("id"));
             if (m != null)
             {
                 switch (m.State)
                 { 
                     case (int)Model.EnumType.方案状态.待发送:
                         Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "请耐心等待计划发送至您的邮箱"));
                           break;
                     case (int)Model.EnumType.方案状态.已预约:
                           Utils.RCWE(UtilsCommons.AjaxReturnJson("0","该方案已预约，不可重复预约"));
                           break;
                     default:
                           break;

                 }
             }

            var r = BProductInterview.Add(new tbl_ProductInterview() { 
                InterviewId=Guid.NewGuid().ToString(),
                OrderNo=DateTime.Now.ToString("yyMdHHmmssffff"),
                SolutionId=Utils.GetQueryStringValue("id"),
                InterviewTarget=Utils.GetFormValue(txtInterviewTarget.UniqueID),
                OrderTime=Utils.GetDateTimeNullable(Utils.GetFormValue(txtOrderTime.UniqueID)),
                ContactTel=Utils.GetFormValue(txtContactTel.UniqueID),
                InterviewNote=Utils.GetFormValue(txtInterviewNote.UniqueID),
                Status=(int)Model.EnumType.面签状态.待确定,
                IssueTime=DateTime.Now
            });
            //方案为已预约状态
            BProductSolution.UpdateState(Utils.GetQueryStringValue("id"), byte.Parse(((int)Model.EnumType.方案状态.已预约).ToString()));
            Utils.RCWE(UtilsCommons.AjaxReturnJson(r ? "1" : "0", r ? "预约成功" : "预约失败"));
        }
    }
}