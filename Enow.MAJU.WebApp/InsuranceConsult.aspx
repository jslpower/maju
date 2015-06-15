<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InsuranceConsult.aspx.cs" Inherits="Enow.MAJU.WebApp.InsuranceConsult" MasterPageFile="~/MasterPage/WebApp.Master" %>

<asp:Content ID="Content2" ContentPlaceHolderID="bd_hd" runat="server">
    <link rel="stylesheet" href="css/xiaoxi.css" type="text/css" media="screen">
    <script src="/js/myscroll.js" type="text/javascript"></script>
    <style>
    #wrapper {
	top:35px; bottom:0px; left:0;
}
    </style>
  <header>
    <a data-rel="back" data-direction="reverse"><i class="returnico"></i></a>
    <h1>保险咨询</h1>
    <a href="InsuranceReply.aspx?id=<%=Request.QueryString["ProductId"] %>" data-transition="pop"><b class="icon_Rbaoxian"></b></a>
  </header>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="bd_bd" runat="server">
<div id="wrapper">
    <div class="baoxian_box" id="scroller">
	    <div id="pullDown" style="display:none;">
		    <span class="pullDownIcon"></span><span class="pullDownLabel">下拉刷新...</span>
	    </div>
        <ul id="thelist">
            <asp:Repeater ID="rpt" runat="server">
            <ItemTemplate>
            <li>
                <div class="time_txt"><%#Eval("FIssueTime") %></div>

                <div class='<%#Eval("IsSysReply").ToString()=="0"?"baoxian_a":"baoxian_q" %>'>
                    <div class="head_img radius"><img src='<%#string.IsNullOrEmpty((string)Eval("HeadPhoto"))?(Eval("IsSysReply").ToString()=="0"?"images/touxian72.png":"images/xx_head1.png"):Eval("HeadPhoto") %>' /></div>
                    <div class="zixun_box radius4"><i></i><%#Eval("Context")%></div>
                </div>
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
        * 下拉刷新 （自定义实现此方法）
        * myScroll.refresh();		// 数据加载完成后，调用界面更新方法
        */
         pullDownAction=function() {
            setTimeout(function () {	// <-- Simulate network congestion, remove setTimeout from production!
                var el, li, l = "baoxian_q", h = "/images/xx_head1.png", s = "";
                el = document.getElementById('thelist');
                el.innerHTML = s;

                $.getJSON(
                "insuranceconsult.aspx?productid=<%=Request.QueryString["productid"] %>"
                , { dotype: "getdatas", page: pagedown }
                , function (result) {
                    $(result).each(function (i, item) {
                        if (item.IsSysReply=="0") {
                            l = "baoxian_a";
                            h = "/images/touxian72.png";
                        }else{
                            l = "baoxian_q";
                            h = "/images/xx_head1.png";
                        }
                        if (item.HeadPhoto != null && $.trim(item.HeadPhoto).length > 0) {
                            h = item.HeadPhoto;
                        }
                        li = document.createElement('li');
                        li.innerHTML = '<div class="time_txt">' + item.FIssueTime + '</div><div class="' + l + '"><div class="head_img radius"><img src="' + h + '" /></div><div class="zixun_box radius4"><i></i>' + item.Context + '</div></div>';
                        $(li).appendTo(el);
                    });
                });
                myScroll.refresh(); 	//数据加载完成后，调用界面更新方法   Remember to refresh when contents are loaded (ie: on ajax completion)
            }, 1000);                          // <-- Simulate network congestion, remove setTimeout from production!
        }

        /**
        * 滚动翻页 （自定义实现此方法）
        * myScroll.refresh();		// 数据加载完成后，调用界面更新方法
        */
        pullUpAction = function () {
            setTimeout(function () {	// <-- Simulate network congestion, remove setTimeout from production!
                var el, li, l = "baoxian_q", h = "/images/xx_head1.png";
                el = document.getElementById('thelist');

                //上拉获取精选话题和咨询
                $.getJSON(
                "insuranceconsult.aspx?productid=<%=Request.QueryString["productid"] %>"
                , { dotype: "getdatas", page: pageup }
                , function (result) {
                    if ($(result).length > 0) {
                        pageup += 1;
                    }
                    $(result).each(function (i, item) {
                        if (item.IsSysReply == "0") {
                            l = "baoxian_a";
                            h = "/images/touxian72.png";
                        }else{
                            l = "baoxian_q";
                            h = "/images/xx_head1.png";
                        }
                        if (item.HeadPhoto != null && $.trim(item.HeadPhoto).length > 0) {
                            h = item.HeadPhoto;
                        }
                        li = document.createElement('li');
                        li.innerHTML = '<div class="time_txt">' + item.FIssueTime + '</div><div class="' + l + '"><div class="head_img radius"><img src="' + h + '" /></div><div class="zixun_box radius4"><i></i>' + item.Context + '</div></div>';
                        $(li).appendTo(el);
                    });
                });
                myScroll.refresh(); 	// 数据加载完成后，调用界面更新方法 Remember to refresh when contents are loaded (ie: on ajax completion)
            }, 1000);     // <-- Simulate network congestion, remove setTimeout from production!
        }

        function initinsuranceconsult() {
            setTimeout(loaded, 100);
            $(document).off("pageinit", initinsuranceconsult);
        }

        $(document).on("pageinit", initinsuranceconsult)

    </script>
</asp:Content>
