using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.MAJU.BLL;
using Enow.MAJU.Model;
using Enow.MAJU.Utility;

namespace Enow.MAJU.Web.Manage.Product
{
    public partial class ProductEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            #region 处理AJAX请求
            string dotype = Utils.GetQueryStringValue("doType").ToLower();
            if (dotype == "save")
            {
                SaveData();
                return;
            }
            #endregion
            if (!IsPostBack)
            {
                ManageUserAuth.ManageIsLoginCheck();
                string Id = Utils.GetQueryStringValue("Id");
                string act = Utils.GetQueryStringValue("act").ToLower();
                InitType();
                switch (act)
                {
                    case "update":
                        InitModel(Id);
                        break;
                    default:
                        break;
                }
            }
        }

        #region 加载产品实体
        /// <summary>
        /// 加载产品实体
        /// </summary>
        /// <param name="Id">产品Id</param>
        private void InitModel(string Id)
        {
            if (!string.IsNullOrWhiteSpace(Id))
            {
                var model = BProduct.GetModel(Id);
                if (model != null)
                {
                    txtProductName.Text = model.ProductName;
                    ddlType.Items.FindByValue(model.TypeId).Selected = true;
                    txtCompany.Text = model.Company;
                    txtTarget.Text = model.Target;
                    txtAdvantage.Text = model.Advantage;
                    txtOtherInfo.Text = model.OtherInfo;
                    //  txtRelatedFile.Text = model.RelatedFile;
                    //txtInterviewNote.Text = model.InterviewNote;
                    txtSortId.Text = model.SortId.ToString();
                }
                else
                {
                    Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "未找到您要查看的信息！"));
                    return;
                }
            }
            else
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "未找到您要查看的信息！"));
                return;
            }
        }
        #endregion

        #region 绑定产品类别下拉列表
        /// <summary>
        /// 绑定产品类别下拉列表
        /// </summary>
        private void InitType()
        {
            int RecordCount = 0;
            var list = BProductType.GetList(ref RecordCount, 999, 1, "");
            if (RecordCount > 0)
            {
                ddlType.DataSource = list;
                ddlType.DataTextField = "TypeName";
                ddlType.DataValueField = "TypeId";
                ddlType.DataBind();
            }
        }
        #endregion

        /// <summary>
        /// 保存数据
        /// </summary>
        private void SaveData()
        {
            #region 取值
            string act = Utils.GetQueryStringValue("act");
            string Id = Utils.GetQueryStringValue("Id");
            string strErr = "";
            bool ret = false;
            string ProductName = Utils.GetFormValue(txtProductName.UniqueID);
            string TypeId = Utils.GetFormValue(ddlType.UniqueID);
            int SortId = Utils.GetInt(Utils.GetFormValue(txtSortId.UniqueID), 0);
            string Company = Utils.GetFormValue(txtCompany.UniqueID);
            string Target = Utils.GetFormValue(txtTarget.UniqueID);
            string Advantage = Utils.EditInputText(txtAdvantage.Text);
            string OtherInfo = Utils.GetFormEditorValue(txtOtherInfo.UniqueID);
            //string RelatedFile = Utils.GetFormEditorValue(txtRelatedFile.UniqueID);
            //string InterviewNote = Utils.GetFormEditorValue(txtInterviewNote.UniqueID);
            string RelatedFile = "";
            string InterviewNote = "";

            #endregion

            #region 判断
            if (string.IsNullOrWhiteSpace(ProductName))
            {
                strErr += "请填写产品名称，产品名称不可为空！";
            }
            if (string.IsNullOrWhiteSpace(Company))
            {
                strErr += "请填写公司名称，公司名称不可为空！";
            }

            if (string.IsNullOrWhiteSpace(Target))
            {
                strErr += "请填写适合人群！";
            }
            if (string.IsNullOrWhiteSpace(Advantage))
            {
                strErr += "请填写产品优势！";
            }
            if (string.IsNullOrWhiteSpace(OtherInfo))
            {
                strErr += "请填写其他信息！";
            }
            //if (string.IsNullOrWhiteSpace(RelatedFile))
            //{
            //    strErr += "请填写相关资料！";
            //}
            //if (string.IsNullOrWhiteSpace(InterviewNote))
            //{
            //    strErr += "请填写面签注意事项！";
            //}
            if (!String.IsNullOrWhiteSpace(strErr))
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", strErr));
                return;
            }
            #endregion

            #region 保存

            switch (act)
            {
                case "add":
                    ret = BProduct.Add(new tbl_Product
                    {
                        ProductId = Guid.NewGuid().ToString(),
                        TypeId = TypeId,
                        ProductName = ProductName,
                        Company = Company,
                        Target = Target,
                        Advantage = Advantage,
                        OtherInfo = OtherInfo,
                        RelatedFile = RelatedFile,
                        InterviewNote = InterviewNote,
                        Collects = 0,
                        SortId = SortId,
                        IssueTime = DateTime.Now
                    });
                    break;
                case "update":
                    ret = BProduct.Update(new tbl_Product
                    {
                        ProductId = Id,
                        TypeId = TypeId,
                        ProductName = ProductName,
                        Company = Company,
                        Target = Target,
                        Advantage = Advantage,
                        OtherInfo = OtherInfo,
                        RelatedFile = RelatedFile,
                        InterviewNote = InterviewNote,
                        SortId = SortId
                    });
                    break;
                default:
                    break;
            }
            if (ret)
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("1", "操作成功!"));

            }
            else
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败!"));

            }
            #endregion
        }
    }
}