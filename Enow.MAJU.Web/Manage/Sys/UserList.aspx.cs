using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.MAJU.BLL;
using Enow.MAJU.Utility;

namespace Enow.MAJU.Web.Manage.Sys
{
    public partial class UserList : System.Web.UI.Page
    {
        /// <summary>
        /// 每页条数
        /// </summary>
        protected int intPageSize = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["PageSize"]);
        /// <summary>
        /// 当前页号
        /// </summary>
        protected int CurrencyPage = 1;
        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            #region 处理AJAX请求
            //获取ajax请求
            string doType = Utils.GetQueryStringValue("doType");

            switch (doType)
            {
                case "Disabled": DisabledData(); break;
                case "Enable": EnableData(); break;
                default: break;
            }
            #endregion
            if (!IsPostBack)
            {
                ManageUserAuth.ManageLoginUrlCheck();
                InitList();
            }
        }
        /// <summary>
        /// 加载列表
        /// </summary>
        private void InitList()
        {
            int rowsCount = 0;
            string Page = Request.QueryString["Page"];
            string keyword = Request.QueryString["KeyWord"];
            this.txtKeyWord.Text = keyword;
            string UserName = Utils.GetQueryStringValue("UserName");
            this.txtuserName.Text = UserName;
            if (!String.IsNullOrEmpty(Page) && StringValidate.IsInteger(Page))
            {
                CurrencyPage = int.Parse(Page);
                if (CurrencyPage < 1)
                    CurrencyPage = 1;
            }
            var list = SysUser.GetList(ref rowsCount, intPageSize, CurrencyPage, UserName, keyword);
            if (rowsCount > 0)
            {
                this.rptList.DataSource = list;
                this.rptList.DataBind();
                ExportPageInfo1.LinkType = 3;
                ExportPageInfo1.intPageSize = intPageSize;
                ExportPageInfo1.intRecordCount = rowsCount;
                ExportPageInfo1.CurrencyPage = CurrencyPage;
                ExportPageInfo1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"] + "?";
                ExportPageInfo1.UrlParams = Request.QueryString;

            }
        }
        #region 禁用/启用
        /// <summary>
        /// 启用操作
        /// </summary>
        private void EnableData()
        {
            string s = Utils.GetQueryStringValue("id");
            if (string.IsNullOrEmpty(s)) Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败：未选择任何要操作的信息！"));
            if (StringValidate.IsInteger(s) == false) Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败：未选择任何要操作的信息！"));
            bool bllRetCode = SysUser.Enable(Convert.ToInt32(s));

            if (bllRetCode)
                Utils.RCWE(UtilsCommons.AjaxReturnJson("1", "操作成功！"));
            else
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败：信息不存在！"));
        }
        /// <summary>
        /// 禁用操作
        /// </summary>
        private void DisabledData()
        {
            string s = Utils.GetQueryStringValue("id");
            if (string.IsNullOrEmpty(s)) Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败：未选择任何要操作的信息！"));
            if (StringValidate.IsInteger(s) == false) Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败：未选择任何要操作的信息！"));
            bool bllRetCode = SysUser.Disabled(Convert.ToInt32(s));

            if (bllRetCode)
                Utils.RCWE(UtilsCommons.AjaxReturnJson("1", "操作成功！"));
            else
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败：信息不存在！"));
        }
        #endregion
        /// <summary>
        /// 搜索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string KeyWord = this.txtKeyWord.Text;
            string uri = UtilsCommons.GetMenuUri(Request.ServerVariables["SCRIPT_NAME"]);
            Response.Redirect(uri + "&KeyWord=" + Server.UrlEncode(KeyWord) + "&UserName=" + Server.UrlEncode(this.txtuserName.Text), true);
        }

        protected string GetRoleName(object RoleId)
        {
            int ID = Utils.GetInt(RoleId.ToString());
            var model = UserRole.GetModel(ID);
            if (model != null)
            {
                return model.RoleName;
            }
            return string.Empty;
        }
    }
}