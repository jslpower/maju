<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="about.aspx.cs" Inherits="Enow.MAJU.WebApp.about"
    MasterPageFile="~/MasterPage/WebApp.Master" %>

<asp:Content ID="Content2" ContentPlaceHolderID="bd_hd" runat="server">
    <link rel="stylesheet" href="/css/kehu.css" type="text/css" media="screen">
    <header>
        <a data-rel="back" data-direction="reverse"><i class="returnico"></i></a>
        <h1>
            关于马驹</h1>
    </header>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="bd_bd" runat="server">
    <div class="about_box">
        <div>
            <asp:Literal ID="ltrimg" runat="server"></asp:Literal></div>
        <div class="mt10">
        </div>
    </div>
    <div class="list_item mt10">
        <ul>
            <li class="R_jiantou">评价一下</li>
            <li class="R_jiantou">检查更新</li>
            <li class="R_jiantou">关注我们</li>
        </ul>
    </div>
    <div class="cent" style="margin-top: 50px;">
        v3.2.0</div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="bd_ft" runat="server">
</asp:Content>
