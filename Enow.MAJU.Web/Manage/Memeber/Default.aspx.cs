using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.MAJU.BLL;
using Enow.MAJU.Model;
using Enow.MAJU.Utility;

namespace Enow.MAJU.Web.Manage.Memeber
{
    public partial class Default : System.Web.UI.Page
    {
        /// <summary>
        /// 每页条数
        /// </summary>
        protected int PageSize = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["PageSize"]), RecordCount = 0, PageIndex = 0;
        /// <summary>
        /// 省市联动下拉框初始化
        /// </summary>
        protected string PId = "", CSId = "", AId = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            #region 处理AJAX请求

            string dotype = Utils.GetQueryStringValue("dotype");
            switch (dotype)
            {
                case "Disabled":
                    DisableData();
                    break;
                case "Enable":
                    EnableData();
                    break;
                default:
                    break;
            }
            #endregion

            if (!IsPostBack)
            {
                ManageUserAuth.ManageLoginUrlCheck();
                
              

            }
            BindStatus();
            InitList();
        }



        /// <summary>
        /// 绑定用户状态下拉框
        /// </summary>
        private void BindStatus()
        {
            Array array = Enum.GetValues(typeof(Model.EnumType.用户状态));
            foreach (var arr in array)
            {
                int value = (int)Enum.Parse(typeof(Model.EnumType.用户状态), arr.ToString());
                string text = Enum.GetName(typeof(Model.EnumType.用户状态), arr);
                this.ddlStatus.Items.Add(new ListItem() { Text = text, Value = value.ToString() });
            }
            this.ddlStatus.Items.Insert(0, new ListItem("请选择", "0"));

        }

        /// <summary>
        /// 加载页面
        /// </summary>
        private void InitList()
        {
            #region 构造查询条件
            MemeberSearch SearchModel = new MemeberSearch();
            int status=Utils.GetInt(Utils.GetQueryStringValue("status"));
            SearchModel.Status = (Model.EnumType.用户状态)status;
            if (SearchModel.Status.Value > 0)
            {
                this.ddlStatus.Items.FindByValue(status.ToString()).Selected = true;
            }
            else
            {
                SearchModel.Status = null;
            }
            SearchModel.Mobile = Utils.GetQueryStringValue("Mobile");
            this.txtMobile.Text = SearchModel.Mobile;
            SearchModel.ProvinceId = Utils.GetInt(Utils.GetQueryStringValue("ProvinceId"));
            if (SearchModel.ProvinceId.Value > 0)
            {
                PId = SearchModel.ProvinceId.Value.ToString();
            }
            else
            {
                SearchModel.ProvinceId = null;
            }
            SearchModel.CityId = Utils.GetInt(Utils.GetQueryStringValue("CityId"));
            if (SearchModel.CityId.Value > 0)
            {
                CSId = SearchModel.CityId.Value.ToString();
            }
            else
            {
                SearchModel.CityId = null;
            }
            SearchModel.AreaId = Utils.GetInt(Utils.GetQueryStringValue("AreaId"));
            if (SearchModel.AreaId.Value > 0)
            {
                AId = SearchModel.AreaId.Value.ToString();
            }
            else
            {
                SearchModel.AreaId = null;
            }
            SearchModel.MemberName =Server.UrlDecode(Utils.GetQueryStringValue("MemberName"));
            this.txtMemberName.Text = SearchModel.MemberName;
            SearchModel.IssueBeginTime = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("StartDate"));
            SearchModel.IssueEndTime = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("EndDate"));
            if (SearchModel.IssueBeginTime.HasValue)
            {
                this.txtIBeginDate.Text = SearchModel.IssueBeginTime.Value.ToString("yyyy-MM-dd");
            }
            if (SearchModel.IssueEndTime.HasValue)
            {
                this.txtIEndDate.Text = SearchModel.IssueEndTime.Value.ToString("yyyy-MM-dd");
            }
            SearchModel.NickName = Utils.GetQueryStringValue("NickName");
            this.txtNickName.Text = SearchModel.NickName;
            #endregion

            PageIndex = UtilsCommons.GetPadingIndex();
            var list = BMember.GetList(ref RecordCount, PageSize, PageIndex, SearchModel);
            if (RecordCount > 0)
            {
                this.rptList.DataSource = list;
                this.rptList.DataBind();
                this.ExportPageInfo1.LinkType = 3;
                this.ExportPageInfo1.intPageSize = PageSize;
                this.ExportPageInfo1.intRecordCount = RecordCount;
                this.ExportPageInfo1.CurrencyPage = PageIndex;
                this.ExportPageInfo1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"] + "?";
                this.ExportPageInfo1.UrlParams = Request.QueryString;
            }
            else
            {
                phNoData.Visible = true;
            }
        }

        #region  锁定/恢复账号
        /// <summary>
        /// 锁定账号
        /// </summary>
        private void DisableData()
        {
            string Id = Utils.GetQueryStringValue("Id");
            if (string.IsNullOrWhiteSpace(Id))
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败：未选择任何要操作的信息！"));
            }
            bool bllRetCode = BMember.UpdateStatus(Id, Model.EnumType.用户状态.已停用);
            if (bllRetCode)
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("1", "操作成功！"));
            }
            else
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败！"));
            }
        }

        /// <summary>
        /// 恢复账号
        /// </summary>
        private void EnableData()
        {
            string Id = Utils.GetQueryStringValue("Id");
            if (string.IsNullOrWhiteSpace(Id))
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败：未选择任何要操作的信息！"));
            }
            bool bllRetCode = BMember.UpdateStatus(Id, Model.EnumType.用户状态.正常);
            if (bllRetCode)
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("1", "操作成功！"));
            }
            else
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败！"));
            }
        }
        #endregion



        /// <summary>
        /// 搜索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string Status = Utils.GetFormValue(ddlStatus.UniqueID);
            string MemberName = Utils.GetFormValue(txtMemberName.UniqueID);
            string NickName = Utils.GetFormValue(txtNickName.UniqueID);
            string Mobile = Utils.GetFormValue(txtMobile.UniqueID);
            int ProvinceId = Utils.GetInt(Utils.GetFormValue("ddlProvince"), 0);
            int Cityid = Utils.GetInt(Utils.GetFormValue("ddlCity"), 0);
            int AreaId = Utils.GetInt(Utils.GetFormValue("ddlArea"), 0);
            string IssueBeginTime =Utils.GetFormValue(txtIBeginDate.UniqueID);
            string IssueEndTime = Utils.GetFormValue(txtIEndDate.UniqueID);
            string uri = UtilsCommons.GetMenuUri(Request.ServerVariables["SCRIPT_NAME"]);
            Response.Redirect(uri + "&status=" + Status + "&Mobile=" + Mobile + "&ProvinceId=" + ProvinceId + "&CityId=" + Cityid + "&AreaId=" + AreaId + "&MemberName=" + Server.UrlEncode(MemberName) + "&NickName="+NickName+"&StartDate=" + IssueBeginTime + "&EndDate=" + IssueEndTime, true);
        }

        #region 导出Excel
        /// <summary>
        /// 导出EXCEL
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnExport_Click(object sender, EventArgs e)
        {
            #region 取值
            ddlStatus.Items.Clear();
            BindStatus();
            int Status = Utils.GetInt(Utils.GetFormValue(ddlStatus.UniqueID), 0);
            string MemberName = Utils.GetFormValue(txtMemberName.UniqueID);
            string Mobile = Utils.GetFormValue(txtMobile.UniqueID);
            int ProvinceId = Utils.GetInt(Utils.GetFormValue("ddlProvince"), 0);
            int Cityid = Utils.GetInt(Utils.GetFormValue("ddlCity"), 0);
            int AreaId = Utils.GetInt(Utils.GetFormValue("ddlArea"), 0);
            DateTime? IssueBeginTime = Utils.GetDateTimeNullable(Utils.GetFormValue(txtIBeginDate.UniqueID));
            DateTime? IssueEndTime = Utils.GetDateTimeNullable(Utils.GetFormValue(txtIEndDate.UniqueID));


            MemeberSearch SearchModel = new MemeberSearch();
            if (Status > 0)
            {
                SearchModel.Status = (Model.EnumType.用户状态)Status;

            }
            ddlStatus.Items.FindByValue(Status.ToString()).Selected = true;
            SearchModel.MemberName = MemberName;
            txtMemberName.Text = MemberName;
            SearchModel.Mobile = Mobile;
            txtMobile.Text = Mobile;
            if (ProvinceId > 0)
            {
                SearchModel.ProvinceId = ProvinceId;
                PId = ProvinceId.ToString();
            }


            if (Cityid > 0)
            {
                SearchModel.CityId = Cityid;
                CSId = Cityid.ToString();
            }

            if (AreaId > 0)
            {
                SearchModel.AreaId = AreaId;
                AId = AreaId.ToString();
            }

            SearchModel.IssueBeginTime = IssueBeginTime;
            SearchModel.IssueEndTime = IssueEndTime;
            #endregion
            var list = BMember.GetList(ref RecordCount, PageSize, PageIndex, SearchModel);
            if (RecordCount > 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("地区", typeof(System.String));
                dt.Columns.Add("用户名", typeof(System.String));
                dt.Columns.Add("姓名", typeof(System.String));
                dt.Columns.Add("性别", typeof(System.String));
                dt.Columns.Add("年龄", typeof(System.String));
                dt.Columns.Add("Email", typeof(System.String));
                dt.Columns.Add("手机号码", typeof(System.String));
                dt.Columns.Add("家庭年收入", typeof(System.String));
                dt.Columns.Add("用户状态", typeof(System.String));
                dt.Columns.Add("注册时间", typeof(System.DateTime));
                foreach (var lst in list)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = lst.ProvinceName + "-" + lst.CityName + "-" + lst.AreaName;
                    dr[1] = lst.NickName;
                    dr[2] = lst.MemberName;
                    dr[3] = lst.Gender == 0 ? "男" : "女";
                    dr[4] = lst.Age.ToString();
                    dr[5] = lst.Email;
                    dr[6] = lst.Mobile;
                    dr[7] = lst.Revenue;
                    dr[8] = (Model.EnumType.用户状态)lst.Status;
                    dr[9] = lst.IssueTime.ToString("yyyy-MM-dd");
                    dt.Rows.Add(dr);
                }
                NPOIHelper.TableToExcelForXLSAny(dt, "用户信息报表");
            }
            else
            {
                Utils.ShowMsg("暂无满足条件的数据!");
            }
        }
        #endregion
    }
}