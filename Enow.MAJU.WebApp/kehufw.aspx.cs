using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.MAJU.BLL;

namespace Enow.MAJU.WebApp
{
    public partial class kehufw : BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitPage();
            }
        }

        private void InitPage()
        {
            var model = BService.GetModel();
            if (model != null)
            {

                txtWeChat.Value = model.WeChat;
                txtQQ.Value = model.QQ;
                txtWeibo.Value = model.Weibo;
                txtEmail.Value = model.Email;
            }
        }
    }
}