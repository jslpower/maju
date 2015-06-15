<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="zixun.aspx.cs" Inherits="Enow.MAJU.WebApp.zixun" MasterPageFile="~/MasterPage/WebApp.Master" %>

<asp:Content ID="Content2" ContentPlaceHolderID="bd_hd" runat="server">
    <link rel="stylesheet" href="/css/zixun.css" type="text/css" media="screen" />
    <script src="/js/myscroll.js" type="text/javascript"></script>
    <style>
    #wrapper {
	top:250px; bottom:0px; left:0;
}
    </style>
    <header>
        <a data-rel="back" data-direction="reverse"><i class="returnico"></i></a>
        <h1>
            资讯</h1>
    </header>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="bd_bd" runat="server">
        <!--baner------------start-->
        <div class="slider_box">
            <div id="slider" class="slider" style="overflow: hidden;">
                <div id="slider_pic">
                    <ul id="slider_pic_ul">
                        <asp:Repeater ID="rptImgList" runat="server">
                            <ItemTemplate>
                                <li><a onclick="return false;">
                                   <img src="<%# Eval("PhotoPath").ToString() %>" alt="<%#Eval("Title") %>"></a></li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </div>
                <div>
                    <ol id="slider_dot" class="slider_dot">
                        
                    </ol>
                </div>
                <div class="banner_txt"></div>
            </div>
        </div>
        <!--baner------------end-->
    <div id="wrapper">
        <div class="zixun_list mt20" id="scroller">
		    <div id="pullDown" style="display:none;">
			    <span class="pullDownIcon"></span><span class="pullDownLabel">下拉刷新...</span>
		    </div>
            <ul id="thelist">
                <asp:Repeater ID="rpt_list" runat="server">
                    <ItemTemplate>
                        <li onclick="javascript:$.mobile.changePage('NewsDetail.aspx?Id=<%#Eval("NewsId") %>', { transition: 'slide' });" />
                            <dl>
                                <dt>
                                    <%#Eval("Title") %></dt>
                                <dd class="cont">
                                    <%#Eval("Context") %>
                                </dd>
                                <dd>
                                    <%#Eval("IssueTime","{0:yyyy-MM-dd HH:mm}")%></dd>
                            </dl>
                        </li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
		    <div id="pullUp" style="display:none;">
			    <span class="pullUpIcon"></span><span class="pullUpLabel">上拉加载更多...</span>
		    </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="bd_ft" runat="server">
    <script type="text/javascript">
        /**
        * 滚动翻页 （自定义实现此方法）
        * myScroll.refresh();		// 数据加载完成后，调用界面更新方法
        */
        pullDownAction = function () {
            setTimeout(function () {	// <-- Simulate network congestion, remove setTimeout from production!
                var el, li, l = "", h = "<i class='din'>顶</i>";
                el = document.getElementById('thelist');
                el.innerHTML = "";

                //下拉刷新
                var para = { page: pagedown };
                $.ajax({
                    type: "Get",
                    url: "/CommonPage/ajaxNewsList.aspx?" + $.param(para),
                    cache: false,
                    success: function (result) {
                        $(el).append(result);
                    },
                    error: function () {
                        ipage.ShowMsg("异常请重新提交！");
                    }
                });

                myScroll.refresh(); 	//数据加载完成后，调用界面更新方法   Remember to refresh when contents are loaded (ie: on ajax completion)
            }, 1000);               // <-- Simulate network congestion, remove setTimeout from production!
        }
        pullUpAction = function () {
            setTimeout(function () {	// <-- Simulate network congestion, remove setTimeout from production!
                var el, li, l = "", h = "<i class='din'>顶</i>";
                el = document.getElementById('thelist');

                //上拉获取翻页
                var para = { page: pageup };
                $.ajax({
                    type: "Get",
                    url: "/CommonPage/ajaxNewsList.aspx?" + $.param(para),
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
            }, 1000);    // <-- Simulate network congestion, remove setTimeout from production!
        }

        function initzixun() {
            //初始化slider
            setTimeout(function () {
                var scrollPicView = document.getElementById("slider"),
                                scrollPicViewDiv = document.getElementById("slider_pic"),
                                lis = scrollPicViewDiv.querySelectorAll("li"),
                                w = scrollPicView.offsetWidth,
                                len = lis.length;

                for (var i = 0; i < len; i++) {
                    lis[i].style.width = w + "px";
                    if (i == 0) {
                        $(".banner_txt").html($("#slider_pic_ul li a img").eq(i).attr("alt"));
                    }
                    if (i == len - 1) {
                        scrollPicViewDiv.style.width = w * len + "px";
                    }
                }

                var slider = new iScroll('slider', {
                    snap: true,
                    momentum: false,
                    hScrollbar: false,
                    useTransition: true,
                    onScrollEnd: function () {
                        $("#slider_dot li").removeClass("on").eq(this.currPageX).addClass("on");
                        $(".banner_txt").html($("#slider_pic_ul li a img").eq(this.currPageX).attr("alt"));
                    }
                });
                //
                var nav_lis = new Array(lis.length);
                $('<li class="on"></li>').appendTo($("#slider_dot"));
                for (var i = 1; i < nav_lis.length; i++) {
                    $('<li></li>').appendTo($("#slider_dot"));
                }
            }, 100);

            //初始化绑定iScroll控件 
            setTimeout(loaded, 100);

            $(document).off("pageinit", initzixun);
        }
        $(document).on("pageinit", initzixun);
    </script>
</asp:Content>
