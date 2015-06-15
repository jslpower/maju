<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TypeEdit.aspx.cs" Inherits="Enow.MAJU.Web.Manage.Product.TypeEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/Css/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Js/jquery-1.4.4.js"></script>
    <link href="/css/boxynew.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="clear">
    </div>
    <div class="contentbox">
        <div class="firsttable">
            <table width="100%" cellspacing="0" cellpadding="0" border="0" id="tableInfo">
                <tr>
                    <th width="100">
                        类别名称：
                    </th>
                    <td>
                        <asp:TextBox ID="txtTypeName" runat="server" CssClass="input-txt formsize240" MaxLength="100"></asp:TextBox>
                        <span class="fontred">*</span>
                    </td>
                </tr>
                <tr>
                    <th>
                        排序号：
                    </th>
                    <td>
                        <asp:TextBox ID="txtSortId" runat="server" CssClass="input-txt formsize40" MaxLength="6" Text="0" ></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th width="100">
                        是否置顶：
                    </th>
                    <td>
                        <asp:DropDownList ID="ddlTop" runat="server">
                            <asp:ListItem Value="0">否</asp:ListItem>
                            <asp:ListItem Value="1">是</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
        </div>
        <div class="Basic_btn fixed">
             <ul>
                <li>
                    <asp:LinkButton ID="linkBtnSave" runat="server" onclick="linkBtnSave_Click">保 存 >></asp:LinkButton>
                </li>
                <li><a href="javascript:void(0);" onclick="parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"] %>').hide()"
                    hidefocus="true">返 回 >></a></li>
            </ul>
            <div class="hr_10">
            </div>
        </div>
    </div>
    </form>
</body>
</html>
