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
    public partial class ServiceEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            #region 处理AJAX请求

            string dotype = Utils.GetQueryStringValue("dotype");
            switch (dotype)
            {
                case "save":
                    SaveData();
                    break;
                default:
                    break;
            }
            #endregion

            if (!IsPostBack)
            {
                ManageUserAuth.ManageIsLoginCheck();
                InitPage();
            }
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        private void InitPage()
        {
            var list = BService.GetList();
            if (list.Count>0)
            {
                txtWechat.Text = list[0].WeChat;
                txtQQ.Text = list[0].QQ;
                txtWeibo.Text = list[0].Weibo;
                txtEmail.Text = list[0].Email;
            }
        }



        /// <summary>
        /// 保存数据
        /// </summary>
        private void SaveData()
        {
            #region 取值
            string strErr = "";
            bool IsRet = false;
            string weChat = Utils.GetFormValue(txtWechat.UniqueID);
            string qq = Utils.GetFormValue(txtQQ.UniqueID);
            string weiBo = Utils.GetFormValue(txtWeibo.UniqueID);
            string email = Utils.GetFormValue(txtEmail.UniqueID);
            #endregion

            #region 判断
            if (String.IsNullOrWhiteSpace(weChat))
            {
                strErr += "微信订阅号不可为空!";
            }
            if (String.IsNullOrWhiteSpace(qq)||!StringValidate.IsNumber(qq))
            {
                strErr += "企业QQ不可为空，企业QQ只能为数字！";
            }
            if (String.IsNullOrWhiteSpace(weiBo))
            {
                strErr += "微博账号不可为空！";
            }
            if (String.IsNullOrWhiteSpace(email)||!StringValidate.IsEmail(email))
            {
                strErr += "企业邮箱不可为空,请填写正确的邮箱地址！";
            }
            if (!String.IsNullOrWhiteSpace(strErr))
            {
                 Utils.RCWE(UtilsCommons.AjaxReturnJson("0", strErr));
                 return;
            }
            #endregion

            var list = BService.GetList();
            if (list.Count > 0)
            {
                IsRet = BService.Update(new tbl_ServiceInfo
                {
                    ID = list[0].ID,
                    WeChat = weChat,
                    QQ = qq,
                    Weibo = weiBo,
                    Email = email
                });
            }
            else
            {
                IsRet = BService.Add(new tbl_ServiceInfo { 
                WeChat=weChat,
                QQ=qq,
                Weibo=weiBo,
                Email=email
                });
            }
            if (IsRet)
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("1", "操作成功！"));
            }
            else
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败！"));
            }
        }
    }
}