<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebApp.Master" AutoEventWireup="true"
    CodeBehind="NewsDetail.aspx.cs" Inherits="Enow.MAJU.WebApp.NewsDetail" %>

<asp:Content ID="Content2" ContentPlaceHolderID="bd_hd" runat="server">
    <link rel="stylesheet" href="css/basic.css" type="text/css" media="screen">
    <link rel="stylesheet" href="css/huati.css" type="text/css" media="screen">
    <script type="text/javascript" src="js/tinybox.js"></script>
    <header>
        <a data-rel="back" data-direction="reverse"><i class="returnico"></i></a>
        <h1>
            资讯详情</h1>
        <a href="#" id="reply"><b class="icon_jia"></b></a>
    </header>
    <div class="head_div" style="display: block;">
        <div class="head_box" style="display: none;">
            <ul>
                <li id="collect" style="display:none;">收藏</li>
                <li id="share">分享</li>
            </ul>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="bd_bd" runat="server">
    <div class="huati_xx mt20">
        <div class="huati_title" id="title">
            <asp:Literal ID="ltrTitle" runat="server"></asp:Literal></div>
        <div class="huati_time">
            <asp:Literal ID="ltrIssueTime" runat="server"></asp:Literal></div>
        <div class="cont_box"  style="text-indent:0; text-align:left;">
            <div id="context">
                <asp:Literal ID="ltrContent" runat="server"></asp:Literal></div>
        </div>
        <div class="huati_item">
            <ul>
                <asp:Repeater ID="rptlist" runat="server">
                    <ItemTemplate>
                        <li>
                            <div class="huati_head">
                                 <div class="radius">
                                    <img src="<%#string.IsNullOrWhiteSpace(Convert.ToString(Eval("HeadPhoto")))?"/images/touxian0.png":Eval("HeadPhoto") %>"
                                        alt=""></div>
                                <div class="huati_title">
                                    <%#Enow.MAJU.BLL.BMember.GetUserName(Eval("MemberId").ToString())%></div>
                                <div class="huati_time">
                                    <%#Enow.MAJU.Utility.Utils.DateStringFromNow(DateTime.Parse(Eval("IssueTime").ToString()))%></div>
                            </div>
                            <div class="huati_cont">
                                <%#Eval("Context")%></div>
                        </li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="bd_ft" runat="server">
    <div class="bot_nav" style="margin-top: 20px;">
        <div class="bot_box">
            <div class="caozuo padd" style="background: #fff; padding-top: 8px;">
                <%if (Enow.MAJU.BLL.BackPage.IsLoginCheck()) %>
                <%{ %>
                <a href="Newsreply.aspx?id=<%=NewsId %>" class="btn_yellow radius4" data-transition="pop">回复</a>
                <%} %>
                <%else %>
                <%{ %>
                <a href="default.aspx" class="btn_yellow radius4" data-ajax="false" data-transition="pop">回复</a>
                <%} %>
            </div>
        </div>
    </div>
    <script type="text/javascript">
    function initnewsdetail() {
        //点+
        $("#reply").on("tap", function () {
            $(".head_box").toggle();
        });
        $("#collect").click(function () {
            $(".head_box").hide();
            var id = '<%=Enow.MAJU.Utility.Utils.GetQueryStringValue("Id") %>';
            $.ajax({
                type: "post",
                url: '/NewsDetail.aspx?type=collect&id=' + id + '',

                cache: false,
                dataType: "json",
                async: false,
                success: function (responseType) {
                    if(responseType.result=="-2"){
                        window.location.href="/Default.aspx";
                    }else{
                        ShowMsg(responseType.msg);
                    }
                    return false;
                },
                error: function () {
                    ShowMsg("请求异常，请重新提交");
                    return false;
                }
            });
        });

        $("#share").on("tap",function(){
            $("#fenxiang").show();
        });

        //分享内容
        ipage.shareoption.webpageUrl = $.mobile.path.makeUrlAbsolute("newsdetail.aspx?id=<%=Request.QueryString["id"] %>");
        ipage.shareoption.title = $("#title").html();
        //ipage.shareoption.description = $("#context").html();

        $(document).off("pageinit", initnewsdetail);
    }
    $(document).on("pageinit", initnewsdetail);
        function  ShowMsg(msg) {
            TINY.box.show({
                html: msg,
                animate: false,
                close: false,
                boxid: 'error',
                top: 50,
                autohide: 2
            });
        }
    </script>
</asp:Content>
