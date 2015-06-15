<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="huatidetail.aspx.cs" Inherits="Enow.MAJU.WebApp.huatidetail"
    MasterPageFile="~/MasterPage/WebApp.Master" %>

<asp:Content ID="Content2" ContentPlaceHolderID="bd_hd" runat="server">
    <link rel="stylesheet" href="css/huati.css" type="text/css" media="screen">
    <header>
        <a data-rel="back" data-direction="reverse"><i class="returnico"></i></a>
        <h1>
            话题详情</h1>
        <a href="#" id="reply"><b class="icon_jia"></b></a>
    </header>
    <div class="head_div" style="display: block;">
        <div class="head_box" style="display: none;">
            <ul>
                <li id="collect">收藏</li>
                <li id="share">分享</li>
            </ul>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="bd_bd" runat="server">
    <div class="huati_xx mt20">
        <div class="huati_head">
            <div class="radius">
                <asp:Literal ID="ltrImg" runat="server"></asp:Literal></div>
            <div class="huati_title" id="title">
                <asp:Literal ID="ltrTitle" runat="server"></asp:Literal></div>
            <div class="huati_time">
                <asp:Literal ID="ltrAuthor" runat="server"></asp:Literal></div>
        </div>
        <div class="huati_cont">
            <div id="context">
                <asp:Literal ID="ltrContent" runat="server"></asp:Literal></div>
        </div>
        <div class="huati_item">
            <ul>
                <asp:Repeater ID="rptList" runat="server">
                    <ItemTemplate>
                        <li>
                            <div class="huati_head">
                                <div class="radius">
                                    <img src="<%#string.IsNullOrWhiteSpace(Convert.ToString(Eval("HeadPhoto")))?"/images/touxian48.png":Eval("HeadPhoto") %>"
                                        alt=""></div>
                                <div class="huati_title">
                                    <%#Enow.MAJU.BLL.BMember.GetUserName(Eval("MemberId").ToString()) %></div>
                                <div class="huati_time">
                                    <%#Enow.MAJU.Utility.Utils.DateStringFromNow(DateTime.Parse(Eval("IssueTime").ToString()))%></div>
                            </div>
                            <div class="huati_cont">
                                <%#Eval("context") %></div>
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
                    <a href="huatireplay.aspx?Id=<%=TopicId %>" data-transition="pop"
                        class="btn_yellow radius4">回复</a>
                <%} %>
                <%else %>
                <%{ %>
                <a href="default.aspx" class="btn_yellow radius4" data-ajax="false" data-transition="pop">回复</a>
                <%} %>
                </div>
            </div>
    </div>
    <script type="text/javascript">
    function inithuatidetail() {
        $("#collect").on("tap",function () {
            $(".head_box").hide();
            var id = '<%=Enow.MAJU.Utility.Utils.GetQueryStringValue("Id") %>';
            $.ajax({
                type: "post",
                url: '/huatidetail.aspx?type=collect&id=' + id + '',

                cache: false,
                dataType: "json",
                async: false,
                success: function (responseType) {
                    if(responseType.result=="-2"){
                        window.location.href="/Default.aspx";
                    }else{
                        ipage.ShowMsg(responseType.msg);
                    }
                    return false;
                },
                error: function () {
                    ipage.ShowMsg("请求异常，请重新提交");
                    return false;
                }
            });
        });

        //点+
        $("#reply").on("tap", function () {
            if ($(".head_box").is(":visible")) {
                $(".head_box").hide();
            }
            else {
                $(".head_box").show();
            }
        });
        $("#share").on("tap",function(){
            $("#fenxiang").show();
        });

        //分享内容
        ipage.shareoption.webpageUrl = $.mobile.path.makeUrlAbsolute("huatidetail.aspx?id=<%=Request.QueryString["id"] %>");
        ipage.shareoption.title = $("#title").html();
        //ipage.shareoption.description = $("#context").html();

        $(document).off("pageinit", inithuatidetail);
    }

    $(document).on("pageinit", inithuatidetail);
    </script>
</asp:Content>
