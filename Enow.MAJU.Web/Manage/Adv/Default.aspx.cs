using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.MAJU.BLL;
using Enow.MAJU.Model;
using Enow.MAJU.Utility;

namespace Enow.MAJU.Web.Manage.Adv
{
    public partial class Default : System.Web.UI.Page
    {
        /// <summary>
        /// 每页条数
        /// </summary>
        protected int PageSize = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["PageSize"]), PageIndex = 0, RecordCount = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            #region 处理ajax请求
            if (Utils.GetQueryStringValue("dotype")=="delete")
            {
                DelAdv();
            }
            #endregion
            if (!IsPostBack)
            {
                ManageUserAuth.ManageLoginCheck();
                InitList();
            }
        }

        #region 删除广告
        private void DelAdv()
        {
            string Id = Utils.GetQueryStringValue("Id") ;
            if (string.IsNullOrEmpty(Id))
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败：未选择任何要删除的信息！"));
            }
            int bllRetCode = 0;
            string[] Idlist = Id.Split(',');
            for (int i = 0; i <Idlist.Length; i++)
            {
                bllRetCode = BAdv.Delete(int.Parse(Idlist[i])) == true ? 1 : 99;
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
        #endregion

        /// <summary>
        /// 加载广告列表
        /// </summary>
        private void InitList()
        {
            #region 查询实体
            AdvSearch SearchModel = new AdvSearch();
            SearchModel.Title = Utils.GetQueryStringValue("Title");
            
            int TypeId = Utils.GetInt(Utils.GetQueryStringValue("Type"), -1);
            if (TypeId>-1)
            {
                SearchModel.Type = (Model.EnumType.导航条位置)TypeId;
            }
            #endregion

            PageIndex = UtilsCommons.GetPadingIndex();
            var list = BAdv.GetList(ref RecordCount, PageSize, PageIndex, SearchModel);
            if (RecordCount > 0)
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
    }
}