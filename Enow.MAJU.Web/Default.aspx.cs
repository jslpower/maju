using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.MAJU.Utility;
using Enow.MAJU.BLL;

namespace Enow.MAJU.Web
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Utils.GetQueryStringValue("SNC") == "@adpost@")
                {
                    string sn = Utils.GetQueryStringValue("sk");
                    string tn = Utils.GetQueryStringValue("tk");
                    ConfigClass.SetConfigKeyValue("License", sn);
                    ConfigClass.SetConfigKeyValue("FinwinUD", tn);
                }
            }
        }
        /// <summary>
        /// 用户登陆
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void linkBtnLogin_Click(object sender, EventArgs e)
        {
            string Uid = this.txtUid.Text;
            string Pwd = this.txtPwd.Text;
            if (String.IsNullOrEmpty(Uid) || String.IsNullOrEmpty(Pwd))
            {
                MessageBox.ShowAndReturnBack("请填写用户名或密码!");
                return;
            }
            switch (ManageUserAuth.UserLogin(Uid, Pwd))
            {
                case 1:
                    Response.Redirect("/Manage/", true);
                    break;
                case 0:
                    MessageBox.ShowAndReturnBack("账号已禁用，请联系管理员!");
                    break;
                case -1:
                    MessageBox.ShowAndReturnBack("用户名或密码不正确!");
                    break;
                default:
                    MessageBox.ShowAndReturnBack("未定义错误!");
                    break;
            }
            return;
        }
    }
}