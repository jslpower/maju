using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.MAJU.BLL;
using Enow.MAJU.Utility;

namespace Enow.MAJU.Web.Manage.Product
{
    public partial class ProductReply : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            #region 处理ajax请求
            if (Utils.GetQueryStringValue("doType") == "save")
            {
                SaveData();
            }
            #endregion
            if (!IsPostBack)
            {
                ManageUserAuth.ManageLoginCheck();
                string Id = Utils.GetQueryStringValue("Id");
                var replyModel = BProductReply.GetModel(Id);
                ltrNickName.Text = BMember.GetModel(replyModel.MemberId).NickName;
                ltrProductName.Text = BProduct.GetModel(replyModel.ProductId).ProductName;
                ltrDate.Text = replyModel.IssueTime.ToString("yyyy-MM-dd HH:mm");
                ltrContext.Text = replyModel.Context;
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        private void SaveData()
        {
            string Context = Utils.GetFormValue("txtContent");
            if (string.IsNullOrWhiteSpace(Context))
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "请填写消息内容"));
                return;
            }
            string Id = Utils.GetQueryStringValue("Id");
            var replyModel = BProductReply.GetModel(Id);

            bool ret = BProductReply.Add(new tbl_ProductReply() { 
            ReplyId=Guid.NewGuid().ToString(),
            ProductId=replyModel.ProductId,
            MemberId=replyModel.MemberId,
            Context=Context,
            IssueTime=DateTime.Now,
            IsSysReply='1'
            });
            if (ret)
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("1", "操作成功!"));
            }
            else
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败!"));
            }

        }
    }
}