using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.MAJU.BLL;
using Enow.MAJU.Model;
using Enow.MAJU.Utility;

namespace Enow.MAJU.Web.Manage.Topic
{
    public partial class TopicReply : System.Web.UI.Page
    {
        protected int PageSize = 20, PageIndex = 0, RecordCount = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            #region 处理ajax请求
            if (Utils.GetQueryStringValue("dotype")=="delete")
            {
                DeleteReply();
            }
            #endregion 

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

            ReplySearch searchModel = new ReplySearch();
            PageIndex = UtilsCommons.GetPadingIndex();
           
            searchModel.Context = Utils.GetQueryStringValue("keyWords");
            this.txtKeyWords.Text = searchModel.Context;
            searchModel.StartDate = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("StartDate"));
            searchModel.EndDate = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("EndDate"));
            if (searchModel.StartDate.HasValue)
            {
                txtStartTime.Text = searchModel.StartDate.Value.ToString("yyyy-MM-dd");
            }
            if (searchModel.EndDate.HasValue)
            {
                txtEndTime.Text = searchModel.EndDate.Value.ToString("yyyy-MM-dd");
            }
            var list = BMemberTopicReply.GetViewList(ref RecordCount, PageSize, PageIndex, searchModel);
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


            #endregion
        }

        /// <summary>
        /// 删除话题回复
        /// </summary>
        private void DeleteReply()
        {

            string Id = Utils.GetQueryStringValue("Id");
         
            int bllRetCode = 0;
            if (string.IsNullOrWhiteSpace(Id))
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败：未选择任何要删除的信息！"));
            }
            else
            {
                string[] strId = Id.Split(',');
                for (int i = 0; i < strId.Length; i++)
                {
                    var model = BMemberTopicReply.GetModel(strId[i]);
                    bllRetCode = BMemberTopicReply.Delete(strId[i]) == true ? 1 : 99;
                    //删除话题回复，话题回复数减一
                    BMemberTopic.UpdateNumber(model.TopicId, Model.EnumType.更新类别.回复, Model.EnumType.操作符号.减);
                }
                if (bllRetCode == 1)
                {
                    Utils.RCWE(UtilsCommons.AjaxReturnJson("1", "操作成功!"));
                }
                else
                {
                    Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败：未选择任何要删除的信息！"));
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
           
            string Keywords = this.txtKeyWords.Text;
            string startdate = this.txtStartTime.Text;
            string enddate = this.txtEndTime.Text;
            string uri = UtilsCommons.GetMenuUri(Request.ServerVariables["SCRIPT_NAME"]);
            Response.Redirect(uri + "&keyWords=" + Server.UrlEncode(Keywords)+"&StartDate="+startdate+"&EndDate="+enddate, true);
        }
    }
}