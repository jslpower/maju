<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="huatilist.aspx.cs" Inherits="Enow.MAJU.WebApp.huatilist"
    MasterPageFile="~/MasterPage/WebApp.Master" %>

<asp:Content ID="Content2" ContentPlaceHolderID="bd_hd" runat="server">
    <link rel="stylesheet" href="css/huati.css" type="text/css" media="screen">
    <script type="text/javascript" src="js/myscroll.js"></script>
    <style>
    #wrapper {
	top:45px; bottom:0px; left:0;
}
    </style>
    <header>
        <a data-rel="back" data-direction="reverse"><i class="returnico"></i></a>
        <h1>
            话题</h1>
       
    </header>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="bd_bd" runat="server">
    <div id="wrapper">
        <div class="huati_list" id="scroller">
            <div id="pullDown" style="display:none;">
                <span class="pullDownIcon"></span><span class="pullDownLabel">下拉刷新...</span>
            </div>
            <ul id="linelist">
                <asp:Repeater ID="rptList" runat="server">
                    <ItemTemplate>
                        <li onclick="javascript:$.mobile.changePage('huatidetail.aspx?Id=<%#Eval("TopicId") %>', { transition: 'slide' });">
                            <div class="huati_biaoti">
                                <%#Eval("IsTop").ToString()=="1"?"<i class='din'>顶</i>":""%><%#Enow.MAJU.Utility.Utils.GetText2(Eval("Context").ToString(),14,true) %></div>
                            <div class="huati_qita">
                                <span class="huati_num">
                                    <%#Eval("Replys")%></span><%#Eval("IssueTime","{0:yyyy-MM-dd}")%>
                               <%#Enow.MAJU.BLL.BMember.GetUserName(Eval("MemberId").ToString())%></div>
                        </li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
            <div id="pullUp" style="display:none;">
                <span class="pullUpIcon"></span><span class="pullUpLabel">上拉加载更多...</span>
            </div>
            <input id="pageindex" type="hidden" value="<%=PageIndex %>" />
            <input id="hidPageend" type="hidden" value="0" />
        </div>
    </div>
   <asp:PlaceHolder ID="PHadd" runat="server">
    <div class="huati_div">
    <a href="huatireplay.aspx" data-transition="pop">
        <div class="huati_btn radius">
        </div>
        </a>
    </div>
   </asp:PlaceHolder>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="bd_ft" runat="server">
    <script type="text/javascript">

        /**
        * 下拉刷新 （自定义实现此方法）
        * myScroll.refresh();		// 数据加载完成后，调用界面更新方法
        */
        function pullDownAction() {

            setTimeout(function () {	// <-- Simulate network congestion, remove setTimeout from production!
                var el, li, i;
                el = document.getElementById('linelist');
                /*==============================================================*/
                var index = 1;
                $("#pageindex").val("1");
                var pageEnd = 0;

                if (pageEnd == "0") {
                    $("#pageindex").val(index);

                    var para = { Page: pagedown, isGet: 1 };
                    $.ajax({
                        type: "Get",
                        url: "/CommonPage/ajaxTopicList.aspx?" + $.param(para),
                        cache: false,
                        success: function (result) {
                            if (result != "") {
                                $(el).empty();
                                $(el).append(result);
                            } else {
                                $("#pullDown").hide();
                                $("#hidPageend").val("1");
                            }
                        },
                        error: function () {
                            ipage.ShowMsg("异常请重新提交！");
                        }
                    });
                    myScroll.refresh(); 	// Remember to refresh when contents are loaded (ie: on ajax completion)
                }
            }, 1000);
        }

        /**
        * 滚动翻页 （自定义实现此方法）
        * myScroll.refresh();		// 数据加载完成后，调用界面更新方法
        */
        function pullUpAction() {
            setTimeout(function () {	// <-- Simulate network congestion, remove setTimeout from production!
                var el, li, i;
                el = document.getElementById('linelist');
                /*==============================================================*/
                var index = parseInt($("#pageindex").val()) + 1;
                var pageEnd = $("#hidPageend").val();

                if (pageEnd == "0") {
                    $("#pageindex").val(index);

                    var para = { Page: pageup, isGet: 1 };
                    $.ajax({
                        type: "Get",
                        url: "/CommonPage/ajaxTopicList.aspx?" + $.param(para),
                        cache: false,
                        success: function (result) {
                            if (result != "") {
                                pageup += 1;
                                $(el).append(result);
                            } else {
                                $("#pullUp").hide();
                                $("#hidPageend").val("1");
                            }
                        },
                        error: function () {
                            ipage.ShowMsg("异常请重新提交！");
                        }
                    });
                    myScroll.refresh(); 	// Remember to refresh when contents are loaded (ie: on ajax completion)
                }
            }, 1000);              // <-- Simulate network congestion, remove setTimeout from production
        }

        function inithuatilist() {
            setTimeout(loaded, 100);
            $(document).off("pageinit", inithuatilist);
        }

        $(document).on("pageinit", inithuatilist);
    </script>
</asp:Content>
