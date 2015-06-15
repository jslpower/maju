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
    /// <summary>
    /// 用户添加
    /// </summary>
    public partial class UserAdd : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ManageUserAuth.ManageLoginCheck();
            if (!IsPostBack)
            {
                InitRole();
            }
        }

        /// <summary>
        /// 绑定角色组
        /// </summary>
        /// <param name="RoleId"></param>
        private void InitRole()
        {
            this.ddlRole.DataSource = UserRole.GetList("");
            this.ddlRole.DataTextField = "RoleName";
            this.ddlRole.DataValueField = "Id";
            this.ddlRole.DataBind();
            this.ddlRole.Items.Insert(0, new ListItem("请选择角色", "0"));

        }
        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void linkBtnSave_Click(object sender, EventArgs e)
        {
            string Contact_Name = txtContactName.Text.Trim();
            string User_Name = txtUserName.Text.Trim();
            string User_Pwd = txtPwd.Text.Trim();
            string userTel = txtTel.Text.Trim();
            string strErr = "";
            string rolePer = string.Empty;
            int RoleId = 0;
            string strRoleId = Request.Form["ddlRole"];
            if (StringValidate.IsInteger(strRoleId) == false)
            {
                strErr = "请选择角色！";
            }
            else
            {
                int.TryParse(strRoleId, out RoleId);
            }
            if (RoleId < 1)
            {
                strErr = "请选择角色！";
            }
            else
            {
                var roleModel = UserRole.GetModel(RoleId);
                if (roleModel != null)
                {
                    rolePer = roleModel.PerList;
                }
                else
                {
                    strErr = "系统不存在您选择的角色！";
                }
            }
            if (!String.IsNullOrEmpty(User_Pwd))
            {
                HashCrypto CrypTo = new HashCrypto();
                User_Pwd = CrypTo.MD5Encrypt(User_Pwd.Trim());
                CrypTo.Dispose();
            }
            if (!String.IsNullOrEmpty(strErr))
            {
                MessageBox.ShowAndReturnBack(strErr);
                return;
            }
            if (SysUser.IsExistsUserName(User_Name)==false)
            {
                MessageBox.ShowAndReturnBack("用户名已存在，请重新输入");
                return;
            }

            SysUser.Add(new ManagerList
            {
                RoleId = RoleId,
                EmployeeId = 0,
                UserName = User_Name,
                Password = User_Pwd,
                ContactName = Contact_Name,
                ContactTel = userTel,
                PermissionList = rolePer,
                IsEnable = true,
                LastLoginIp = null,
                FieldId = "00000000-0000-0000-0000-000000000000",
                LastLoginTime = DateTime.Now,
                IssueTime = DateTime.Now
            });
            MessageBox.ShowAndParentReload("添加成功！");
            return;
        }
    }
}