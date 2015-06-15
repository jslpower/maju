<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebApp.Master" AutoEventWireup="true"
    CodeBehind="fanganDetail.aspx.cs" Inherits="Enow.MAJU.WebApp.fanganDetail" %>

<asp:Content ID="Content2" ContentPlaceHolderID="bd_hd" runat="server">
    <header>
        <a data-rel="back" data-direction="reverse"><i class="returnico"></i></a>
        <h1>
            方案详细</h1>
    </header>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="bd_bd" runat="server">
    <div class="list_item form_list mt10">
        <ul>
            <li><span class="label_name">方案名称</span>
                <asp:Literal ID="ltrFanganName" runat="server"></asp:Literal>
            </li>
            <li><span class="label_name">姓名</span>
            <asp:Literal ID="ltrTargetName" runat="server"></asp:Literal>
             </li>
            <li><span class="label_name">性别</span>
                <asp:Literal ID="ltrGender" runat="server"></asp:Literal>
            </li>
            <li><span class="label_name">年龄</span>
               <asp:Literal ID="ltrAge" runat="server"></asp:Literal>
            </li>
            <li><span class="label_name">家庭年收入</span>
               <asp:Literal ID="ltrRevenue" runat="server"></asp:Literal>
            </li>
            <li><span class="label_name">邮箱地址</span>
              <asp:Literal id="ltrEmail" runat="server"></asp:Literal>
            </li>
            <li><span class="label_name">方案状态</span>
            <asp:Literal ID="ltrState" runat="server"></asp:Literal>
            </li>
        </ul>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="bd_ft" runat="server">
</asp:Content>
