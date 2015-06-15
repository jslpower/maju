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
    public partial class TypeEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ManageUserAuth.ManageIsLoginCheck();
                string dotype = Utils.GetQueryStringValue("dotype").ToLower();
                string Id = Utils.GetQueryStringValue("Id");
                switch (dotype)
                {
                    case "update":
                        InitModel(Id);
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// 加载产品类别
        /// </summary>
        /// <param name="Id"></param>
        private void InitModel(string Id)
        {
            if (!string.IsNullOrWhiteSpace(Id))
            {
                var model = BProductType.GetModel(Id);
                if (model != null)
                {
                    txtTypeName.Text = model.TypeName;
                    txtSortId.Text = model.SortId.ToString();
                    ddlTop.Items.FindByValue(model.IsTop.ToString()).Selected = true;
                }
                else
                {
                    MessageBox.ShowAndReturnBack("未找到您要查看的信息！");
                }
            }
            else
            {
                MessageBox.ShowAndReturnBack("未找到您要查看的信息！");
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void linkBtnSave_Click(object sender, EventArgs e)
        {
            bool ret = false;
            string TypeName = Utils.GetFormValue(txtTypeName.UniqueID);
            int SortId = Utils.GetInt(Utils.GetFormValue(txtSortId.UniqueID), 0);
            char IsTop = char.Parse(Utils.GetFormValue(ddlTop.UniqueID));
            if (string.IsNullOrWhiteSpace(TypeName))
            {
                MessageBox.ShowAndReload("类别名称不能为空！");
                return;
            }
            string dotype = Utils.GetQueryStringValue("dotype").ToLower();
            string Id = Utils.GetQueryStringValue("Id");

            switch (dotype)
            {
                case "add":
                    ret = BProductType.Add(new tbl_ProductType
                    {
                        TypeId = Guid.NewGuid().ToString(),
                        TypeName = TypeName,
                        IssueTime = DateTime.Now,
                        SortId=SortId,
                        IsTop=IsTop
                    });
                    break;
                case "update":
                    ret = BProductType.Update(new tbl_ProductType
                    {
                        TypeId = Id,
                        TypeName = TypeName,
                        SortId=SortId,
                        IsTop=IsTop
                    });
                    break;
                default:
                    break;
            }
            if (ret)
            {
                MessageBox.ShowAndParentReload("操作成功!");
            }
            else
            {
                MessageBox.ShowAndReload("操作失败！");
            }
        }
    }
}