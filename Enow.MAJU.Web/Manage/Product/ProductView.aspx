<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductView.aspx.cs" Inherits="Enow.MAJU.Web.Manage.Product.ProductView" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="/Css/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Js/jquery-1.4.4.js"></script>
    <link href="/css/boxynew.css" rel="stylesheet" type="text/css" />
    <script src="/Js/jquery.blockUI.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="clear">
    </div>
    <div class="contentbox">
        <div class="firsttable">
            <span class="firsttableT">产品预览</span>
            <table width="100%" cellspacing="0" cellpadding="0" border="0" id="tableInfo">
                <tr>
                    <th width="100">
                        产品名称：
                    </th>
                    <td>
                        <asp:Literal ID="ltrProductName" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <th width="100">
                        产品类别：
                    </th>
                    <td>
                        <asp:Literal ID="ltrType" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <th width="100">
                        排序号：
                    </th>
                    <td>
                        <asp:Literal ID="ltrOrderNo" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <th width="100">
                        公司名称：
                    </th>
                    <td>
                        <asp:Literal ID="ltrCompany" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <th width="100">
                        适合人群：
                    </th>
                    <td>
                        <asp:Literal ID="ltrTarget" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <th width="100">
                        为什么适合您：
                    </th>
                    <td>
                        <asp:Literal ID="ltrAdvantage" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <th width="100">
                        产品特点：
                    </th>
                    <td>
                        <asp:Literal ID="ltrOtherInfo" runat="server"></asp:Literal>
                    </td>
                </tr>
            </table>
        </div>
        <div class="Basic_btn fixed">
            <ul>
                <li><a href="javascript:void(0);" onclick="parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"] %>').hide()"
                    hidefocus="true">关 闭 &gt;&gt;</a></li>
            </ul>
            <div class="hr_10">
            </div>
        </div>
    </div>
    </form>
</body>
</html>
