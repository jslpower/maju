<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="kehufw.aspx.cs" Inherits="Enow.MAJU.WebApp.kehufw"
    MasterPageFile="~/MasterPage/WebApp.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="css/kehu.css" type="text/css" media="screen">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bd_hd" runat="server">
    <header>
        <a data-rel="back" data-direction="reverse"><i class="returnico"></i></a>
        <h1>
            客户服务</h1>
    </header>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="bd_bd" runat="server">
    <div class="list_item form_list kehu_list mt10">
        <ul>
            <%--<li onclick="javascript:$.mobile.changePage('SysMessage.aspx', { transition: 'slide' });">
                <s class="ico_fankui"></s><span class="label_name">意见反馈</span> </li>--%>
            <li><s class="ico_weixin"></s><span class="label_name">微信订阅号</span>
                <input type="text" value="" id="txtWeChat" runat="server" class="u-input" data-role="none"
                    readonly />
            </li>
            <li><s class="ico_qq"></s><span class="label_name">企业QQ</span>
                <input type="text" value="" id="txtQQ" runat="server" class="u-input" data-role="none"
                    readonly />
            </li>
            <li><s class="ico_weibo"></s><span class="label_name">新浪微博</span>
                <input type="text" value="" id="txtWeibo" runat="server" class="u-input" data-role="none"
                    readonly />
            </li>
            <li><s class="ico_email"></s><span class="label_name">邮箱</span>
                <input type="text" value="" id="txtEmail" runat="server" class="u-input" data-role="none"
                    readonly />
            </li>
        </ul>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="bd_ft" runat="server">
</asp:Content>
