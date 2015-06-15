<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TopicView.aspx.cs" Inherits="Enow.MAJU.Web.Manage.Topic.TopicView" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
            <span class="firsttableT">话题预览</span>
            <table width="100%" cellspacing="0" cellpadding="0" border="0" id="tableInfo">
               <tr>
                    <th width="100">
                        话题内容：
                    </th>
                    <td>
                        <asp:Literal ID="ltrContext" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <th width="100">
                        用户昵称：
                    </th>
                    <td>
                        <asp:Literal ID="ltrNickName" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <th width="100">
                        发布日期：
                    </th>
                    <td>
                        <asp:Literal ID="ltrIssueTime" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <th width="100">
                        是否置顶：
                    </th>
                    <td>
                        <asp:Literal ID="ltrTop" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <th width="100">
                        是否热门：
                    </th>
                    <td>
                        <asp:Literal ID="ltrHot" runat="server"></asp:Literal>
                    </td>
                </tr>
              
                <tr>
                    <th width="100">
                        话题状态：
                    </th>
                    <td>
                        <asp:Literal ID="ltrState" runat="server"></asp:Literal>
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
