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
    public partial class ReplyList : System.Web.UI.Page
    {
        /// <summary>
        /// 数据分页
        /// </summary>
        protected int PageSize = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["PageSize"]);
        protected int PageIndex = UtilsCommons.GetPadingIndex(), RecordCount = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ManageUserAuth.ManageLoginCheck();
                InitList();
            }
        }

        /// <summary>
        /// 加载列表
        /// </summary>
        private void InitList()
        {
            #region 查询实体

            MessageSearch searchModel = new MessageSearch();
            searchModel.MessageId = Utils.GetInt(Utils.GetQueryStringValue("Id"));
            
            LtrMessageContext.Text = BMessage.GetMmodel(searchModel.MessageId).Context;
            searchModel.Keywords = Server.UrlDecode(Utils.GetQueryStringValue("keywords"));
            txtKeyWord.Text = searchModel.Keywords;
            searchModel.IssueBeginTime = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("Sdate"));
            if (searchModel.IssueBeginTime.HasValue)
            {
                txtStartTime.Text = searchModel.IssueBeginTime.Value.ToString("yyyy-MM-dd");
            }
            searchModel.IssueEndTime = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("Edate"));
            if (searchModel.IssueEndTime.HasValue)
            {
                txtEndTime.Text = searchModel.IssueEndTime.Value.ToString("yyyy-MM-dd");
            }
            #endregion
            var list = BMessageReply.GetList(ref RecordCount, PageSize, PageIndex, searchModel);
            if (list.Count > 0)
            {
                rptList.DataSource = list;
                rptList.DataBind();
                ExportPageInfo1.LinkType = 3;
                ExportPageInfo1.intPageSize = PageSize;
                ExportPageInfo1.intRecordCount = RecordCount;
                ExportPageInfo1.CurrencyPage = PageIndex;
                ExportPageInfo1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"] + "?";
                ExportPageInfo1.UrlParams = Request.QueryString;
            }
            else
            {
                phNoData.Visible = true;
            }
           
        }

        /// <summary>
        /// 根据消息类型
        /// </summary>
        /// <param name="MemberId"></param>
        /// <param name="IsSysReply"></param>
        /// <returns></returns>
        protected string GetMemberName(string MemberId, string IsSysReply,string operatorid)
        {
            string StrName = "";
            ManagerList AdminModel = new ManagerList();
            tbl_Member model = new tbl_Member();
            switch (IsSysReply)
            {
                case "1":
                   AdminModel= SysUser.GetModel(int.Parse(operatorid));
                   if (AdminModel != null)
                   {
                       StrName = AdminModel.ContactName;
                   }
                   else
                   {
                       StrName = "管理员";
                   }
                    break;
                case "0":
                    model = BMember.GetModel(MemberId);
                    if (model != null)
                    {
                        StrName = model.NickName;
                    }
                    else
                    {
                        StrName = "";
                    }
                    break;
                default:
                    StrName = "";
                    break;
            }
            return StrName;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string Id = Utils.GetQueryStringValue("Id");
            string KeyWords = this.txtKeyWord.Text;
            DateTime? StartDate = Utils.GetDateTimeNullable(Utils.GetFormValue(txtStartTime.UniqueID));
            DateTime? EndDate = Utils.GetDateTimeNullable(Utils.GetFormValue(txtEndTime.UniqueID));
            string uri = UtilsCommons.GetMenuUri(Request.ServerVariables["SCRIPT_NAME"]);
            Response.Redirect(uri + "&Id="+Id+"&keyWords=" + Server.UrlEncode(KeyWords)+"&Sdate="+StartDate+"&Edate="+EndDate+"", true);
        }
    }
}