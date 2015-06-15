<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Enow.MAJU.Web.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>马驹理财管控系统</title>
    <link href="Css/login.css" rel="Stylesheet" type="text/css" />
    <script src="/Js/jquery-1.4.4.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <%--     <div class="loginbar">
        <div class="login_logo"><img src="images/login_logo.png" /></div>
        <div class="login-form fixed">
              <div class="left_form">
                  <ul>
                         <li><i>用户名</i>
                        <asp:TextBox ID="txtUid" Text="" CssClass="inputbg formsize180" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="请输入用户名"
                            ControlToValidate="txtUid">*</asp:RequiredFieldValidator></li>
                         <li><i>密　码</i>
                       <asp:TextBox TextMode="Password" ID="txtPwd" CssClass="inputbg formsize180" Text="请输入用户密码"
                            runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="请输入密码"
                            ControlToValidate="txtPwd">*</asp:RequiredFieldValidator></li>
                  </ul>
              </div>
              <div class="loginbtn"><asp:LinkButton ID="linkBtnLogin" runat="server" OnClick="linkBtnLogin_Click">登 录</asp:LinkButton></div>
        </div>
  </div>
  
  <div class="login_foot">浙江马驹理财有限公司 备案证编号：浙ICP备00000000号-1 许可证号：L-ZJ01409</div>--%>
    <div class="login_box">
        <div class="login_logo">
            <img src="images/logo.png" alt="" /></div>
        <div class="login-form fixed">
            <div class="left_form">
                <ul>
                    <li><i>用户名</i>
                        <asp:TextBox ID="txtUid" Text="" CssClass="inputbg formsize180" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="请输入用户名"
                            ControlToValidate="txtUid">*</asp:RequiredFieldValidator></li>
                    <li><i>密 码</i>
                        <asp:TextBox TextMode="Password" ID="txtPwd" CssClass="inputbg formsize180" Text="请输入用户密码"
                            runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="请输入密码"
                            ControlToValidate="txtPwd">*</asp:RequiredFieldValidator></li>
                </ul>
            </div>
            <div class="loginbtn">
                <asp:LinkButton ID="linkBtnLogin" runat="server" OnClick="linkBtnLogin_Click"></asp:LinkButton></div>
        </div>
    </div>
    <div class="login_foot">
        浙江马驹理财有限公司 备案证编号：浙ICP备00000000号-1 许可证号：L-ZJ01409</div>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
        ShowSummary="False" />
    </form>
    <script type="text/javascript">
        $(function () {
            $("#txtUid,#txtPwd").keypress(function (e) {
                if (e.keyCode == 13) {
                    WebForm_DoPostBackWithOptions(new WebForm_PostBackOptions("linkBtnLogin", "", true, "", "", false, true));
                }
            });
        });
    </script>
</body>
</html>
