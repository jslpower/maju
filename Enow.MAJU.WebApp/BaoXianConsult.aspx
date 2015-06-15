<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebApp.Master" AutoEventWireup="true"
    CodeBehind="BaoXianConsult.aspx.cs" Inherits="Enow.MAJU.WebApp.BaoXianConsult" %>

<asp:Content ID="Content2" ContentPlaceHolderID="bd_hd" runat="server">
    <style>
        #wrapper
        {
            top: 35px;
            bottom: 0px;
            left: 0;
        }
    </style>
    <header>
        <a data-rel="back" data-direction="reverse"><i class="returnico"></i></a>
        <h1>
            保险咨询</h1>
    </header>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="bd_bd" runat="server">
    <div id="wrapper">
        <div class="list_item mt10" id="scroller">
            <ul id="thelist">
                <asp:Repeater ID="rpt" runat="server">
                    <ItemTemplate>
                        <li onclick="javascript:$.mobile.changePage('InsuranceConsult.aspx?ProductId=<%#Eval("ProductId") %>', { transition: 'slide' });">
                            <%#Enow.MAJU.Utility.Utils.GetText2(Eval("ProductName").ToString(),21,true) %>
                            <%#GetIsProductReplyAllRead(Eval("ProductId"))?string.Empty:"<i class='weidu_btn radius'></i>" %>
                        </li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
<%--            <div id="pullUp">
              <span class="pullUpIcon"></span><span class="pullUpLabel">上拉加载更多...</span>
            </div>
--%>        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="bd_ft" runat="server">
</asp:Content>
