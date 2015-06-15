<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SysMessageList.aspx.cs" Inherits="Enow.MAJU.WebApp.SysMessageList" MasterPageFile="~/MasterPage/WebApp.Master" %>

<asp:Content ID="Content2" ContentPlaceHolderID="bd_hd" runat="server">
    <link rel="stylesheet" href="/css/xiaoxi.css" type="text/css" media="screen">
    <script src="/js/myscroll.js" type="text/javascript"></script>
    <style>
    #wrapper {
	top:35px; bottom:0px; left:0;
}
    </style>
  <header>
    <a data-rel="back" data-direction="reverse"><i class="returnico"></i></a>
    <h1>系统消息</h1>
  </header>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="bd_bd" runat="server">
    <div id="wrapper">
     <div class="list_item mt10" id="scroller">
	    <div id="pullDown">
		    <span class="pullDownIcon"></span><span class="pullDownLabel">下拉刷新...</span>
	    </div>
         <ul id="thelist">
         <asp:Repeater ID="rpt" runat="server"><ItemTemplate>
              <li><a href="sysmessage.aspx?id=<%#Eval("SendId") %>&messageid=<%#Eval("MessageId") %>" data-transition="slide"><%#Enow.MAJU.Utility.Utils.GetText2(Eval("Context").ToString(),21,true) %></a><%#Eval("IsRead").ToString()=="0"?"<i class='weidu_btn radius'></i>":"" %></li>
         </ItemTemplate></asp:Repeater>
         </ul>
		    <div id="pullUp">
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
         pullDownAction=function() {
            setTimeout(function () {	// <-- Simulate network congestion, remove setTimeout from production!
                var el, li, l = "", h = "/images/photo.jpg";
                el = document.getElementById('thelist');
                el.innerHTML = "";

                $.getJSON(
                "sysmessagelist.aspx"
                , { dotype: "getdatas", page: pagedown }
                , function (result) {
                    $(result).each(function (i, item) {
                        if (item.IsRead != null && $.trim(item.IsRead) == "0") {
                            l = "<i class='weidu_btn radius'></i>";
                        }
                        li = document.createElement('li');
                        li.innerHTML = '<a href="sysmessage.aspx?id=' + item.SendId + '&messageid=' + item.MessageId+'" data-transition="slide">' + $.trim(item.Context) + '</a>' + l;
                        $(li).appendTo(el);
                    });
                });
                myScroll.refresh(); 	//数据加载完成后，调用界面更新方法   Remember to refresh when contents are loaded (ie: on ajax completion)
            }, 1000);                              // <-- Simulate network congestion, remove setTimeout from production!
        }

        /**
        * 滚动翻页 （自定义实现此方法）
        * myScroll.refresh();		// 数据加载完成后，调用界面更新方法
        */
         pullUpAction=function() {
            setTimeout(function () {	// <-- Simulate network congestion, remove setTimeout from production!
                var el, li, l = "", h = "/images/photo.jpg";
                el = document.getElementById('thelist');

                //上拉获取精选话题和咨询
                $.getJSON(
                "sysmessagelist.aspx"
                , { dotype: "getdatas", page: pageup }
                , function (result) {
                    if ($(result).length > 0) {
                        pageup += 1;
                    }
                    $(result).each(function (i, item) {
                        if (item.IsRead != null && $.trim(item.IsRead) == "0") {
                            l = "<i class='weidu_btn radius'></i>";
                        }
                        li = document.createElement('li');
                        li.innerHTML = '<a href="sysmessage.aspx?id=' + item.SendId + '&messageid=' + item.MessageId + '" data-transition="slide">' + $.trim(item.Context) + '</a>' + l;
                        $(li).appendTo(el);
                    });
                });
                myScroll.refresh(); 	// 数据加载完成后，调用界面更新方法 Remember to refresh when contents are loaded (ie: on ajax completion)
            }, 1000);      // <-- Simulate network congestion, remove setTimeout from production!
        }

        function initsysmessagelist() {
            setTimeout(loaded, 100);
            $(document).off("pageinit", initsysmessagelist);
        }

        $(document).on("pageinit", initsysmessagelist);

    </script>
</asp:Content>
