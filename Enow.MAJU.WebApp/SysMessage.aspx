<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SysMessage.aspx.cs" Inherits="Enow.MAJU.WebApp.SysMessage"
    MasterPageFile="~/MasterPage/WebApp.Master" %>

<asp:Content ID="Content2" ContentPlaceHolderID="bd_hd" runat="server">
    <link rel="stylesheet" href="css/xiaoxi.css" type="text/css" media="screen">
    <script src="/js/myscroll.js" type="text/javascript"></script>
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
            系统消息</h1>
        <asp:PlaceHolder ID="phAdd" runat="server" Visible="false"><a href="sysmessagereply.aspx"
            data-transition="pop"><b class="icon_Rbaoxian"></b></a></asp:PlaceHolder>
    </header>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="bd_bd" runat="server">
    <div id="wrapper">
        <div class="baoxian_box" id="scroller">
            <div id="pullDown" style="display: none;">
                <span class="pullDownIcon"></span><span class="pullDownLabel">下拉刷新...</span>
            </div>
                    <div id="thelist">
            <asp:Repeater ID="rpt_1" runat="server" OnItemDataBound="rpt_1_ItemDataBound">
                <ItemTemplate>
                    <div class="time_txt">
                        <%#Convert.ToDateTime(Eval("IssueTime").ToString()).ToString("yyyy-MM-dd HH:mm:ss")%></div>
                    <div class="baoxian_q">
                        <div class="head_img radius">
                            <img src="images/xx_head1.png" /></div>
                        <div class="zixun_box radius4">
                            <i></i>
                            <%#Eval("Context")%></div>
                    </div>
                        <asp:Repeater ID="rpt" runat="server">
                            <ItemTemplate>
                                    <div class="time_txt">
                                        <%#Eval("IssueTime","{0:yyyy-MM-dd HH:mm:ss}") %></div>
                                    <div class="<%#Eval("IsSysReply").ToString()=="0"?"baoxian_a":"baoxian_q" %>">
                                        <div class="head_img radius">
                                            <img src="<%#string.IsNullOrEmpty(usermodel.HeadPhoto)?(Eval("IsSysReply").ToString()=="0"?"images/touxian72.png":"images/xx_head1.png"):usermodel.HeadPhoto %>" /></div>
                                        <div class="zixun_box radius4">
                                            <i></i>
                                            <%#Eval("Context") %></div>
                                    </div>
                            </ItemTemplate>
                        </asp:Repeater>
                </ItemTemplate>
            </asp:Repeater>
                    </div>
            <div id="pullUp" style="display: none;">
                <span class="pullUpIcon"></span><span class="pullUpLabel">上拉加载更多...</span>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="bd_ft" runat="server">
    <script type="text/javascript">

        /**
        * 下拉刷新 （自定义实现此方法）
        * myScroll.refresh();		// 数据加载完成后，调用界面更新方法
        */
        pullDownAction = function () {
            setTimeout(function () {	// <-- Simulate network congestion, remove setTimeout from production!
                var el, li, l = "baoxian_q", h = "images/touxian0.png";
                el = document.getElementById('thelist');
                el.innerHTML = "";

                //下拉刷新
                var para = { page: pagedown };
                $.ajax({
                    type: "Get",
                    url: "/CommonPage/ajaxSysMessage.aspx?" + $.param(para),
                    cache: false,
                    success: function (result) {
                        $(el).append(result);
                    },
                    error: function () {
                        ipage.ShowMsg("异常请重新提交！");
                    }
                });

                myScroll.refresh(); 	//数据加载完成后，调用界面更新方法   Remember to refresh when contents are loaded (ie: on ajax completion)
            }, 1000);                                // <-- Simulate network congestion, remove setTimeout from production!
        }

        /**
        * 滚动翻页 （自定义实现此方法）
        * myScroll.refresh();		// 数据加载完成后，调用界面更新方法
        */
        pullUpAction = function () {

            setTimeout(function () {	// <-- Simulate network congestion, remove setTimeout from production!
                var el, li, l = "baoxian_q", h = "images/touxian0.png";
                el = document.getElementById('thelist');

                //上拉获取翻页
                var para = { page: pageup };
                $.ajax({
                    type: "Get",
                    url: "/CommonPage/ajaxSysMessage.aspx?" + $.param(para),
                    cache: false,
                    success: function (result) {
                        if ($.trim(result).length > 0) {
                            pageup += 1;
                        }
                        $(el).append(result);
                    },
                    error: function () {
                        ipage.ShowMsg("异常请重新提交！");
                    }
                });

                myScroll.refresh(); 	// 数据加载完成后，调用界面更新方法 Remember to refresh when contents are loaded (ie: on ajax completion)
            }, 1000);      // <-- Simulate network congestion, remove setTimeout from production!
        }

        function initsysmessage() {
            setTimeout(loaded, 100);
            $(document).off("pageinit", initsysmessage);
        }

        $(document).on("pageinit", initsysmessage);

    </script>
</asp:Content>
