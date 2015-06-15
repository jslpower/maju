<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RelatedFile.aspx.cs" Inherits="Enow.MAJU.WebApp.RelatedFile" MasterPageFile="~/MasterPage/WebApp.Master" %>

<asp:Content ID="Content2" ContentPlaceHolderID="bd_hd" runat="server">
    <link rel="stylesheet" href="css/huati.css" type="text/css" media="screen">
    <script src="/js/myscroll.js" type="text/javascript"></script>
    <style type="text/css" media="all">
        
        #wrapper {
	        top:2px;
	        width:89%;
	        bottom:0px;
        }

    </style>
  <header>
    <a data-rel="back" data-direction="reverse"><i class="returnico"></i></a>
    <h1>有关"<%=Request.QueryString["title"] %>"的话题</h1>
  </header>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="bd_bd" runat="server">
     <div class="huati_xx mt20" id="wrapper">
       <div class="huati_item" style="padding:0;" id="scroller">
	    <div id="pullDown" style="display:none;">
		    <span class="pullDownIcon"></span><span class="pullDownLabel">下拉刷新...</span>
	    </div>
           <ul id="thelist"> 
           <asp:Repeater ID="rpt" runat="server"><ItemTemplate>
             <li>
                     <div class="huati_head">
                         <div class="radius"><img src="<%#Eval("HeadPhoto")==null||string.IsNullOrEmpty(Eval("HeadPhoto").ToString())?"images/touxian48.png":Eval("HeadPhoto") %>"></div>
                         <div class="huati_title"><%#Eval("NickName") %></div>
                         <div class="huati_time"><%#Eval("FIssueTime") %></div>
                     </div>
                     
                     <div class="huati_cont"><a href="huatidetail.aspx?id=<%#Eval("TopicId") %>" data-role="none" data-transition="slide"><%#Eval("Title") %></a> </div>
                     
               </li> 
           </ItemTemplate></asp:Repeater>
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
                var el, li, l = "", h = "/images/touxian48.png";
                el = document.getElementById('thelist');
                el.innerHTML = "";

                $.getJSON(
                "RelatedFile.aspx"
                , { dotype: "getdatas", page: pagedown,title:"<%=Request.QueryString["title"] %>" }
                , function (result) {
                    $(result).each(function (i, item) {
                        if (item.HeadPhoto != null && $.trim(item.HeadPhoto).length > 0) {
                            h = item.HeadPhoto;
                        }
                        li = document.createElement('li');
                        li.innerHTML = '<div class="huati_head"><div class="radius"><img src="' + h + '"></div><div class="huati_title">' + item.NickName + '</div><div class="huati_time">' + item.FIssueTime + '</div></div><div class="huati_cont"><a href="huatidetail.aspx?id='+item.TopicId+'" data-role="none" data-transition="slide">' + item.Title + '</a></div>';
                        $(li).appendTo(el);
                    });
                });
                myScroll.refresh(); 	//数据加载完成后，调用界面更新方法   Remember to refresh when contents are loaded (ie: on ajax completion)
            }, 1000);                               // <-- Simulate network congestion, remove setTimeout from production!
        }

        /**
        * 滚动翻页 （自定义实现此方法）
        * myScroll.refresh();		// 数据加载完成后，调用界面更新方法
        */
         pullUpAction=function() {
            setTimeout(function () {	// <-- Simulate network congestion, remove setTimeout from production!
                var el, li, l = "", h = "/images/touxian48.png";
                el = document.getElementById('thelist');

                //上拉获取精选话题和咨询
                $.getJSON(
                "RelatedFile.aspx"
                , { dotype: "getdatas", page: pageup,title:"<%=Request.QueryString["title"] %>" }
                , function (result) {
                    if ($(result).length > 0) {
                        pageup += 1;
                    }
                    $(result).each(function (i, item) {
                        if (item.HeadPhoto != null && $.trim(item.HeadPhoto).length > 0) {
                            h = item.HeadPhoto;
                        }
                        li = document.createElement('li');
//                        li.onclick = $.mobile.changePage('huatidetail.aspx?id=' + item.TopicId, { transition: 'slide' });
                        li.innerHTML = '<div class="huati_head"><div class="radius"><img src="' + h + '"></div><div class="huati_title">' + item.NickName + '</div><div class="huati_time">' + item.FIssueTime + '</div></div><div class="huati_cont"><a href="huatidetail.aspx?id='+item.TopicId+'" data-role="none" data-transition="slide">' + item.Title + '</a></div>';
                        $(li).appendTo(el);
                    });
                });
                myScroll.refresh(); 	// 数据加载完成后，调用界面更新方法 Remember to refresh when contents are loaded (ie: on ajax completion)
            }, 1000);      // <-- Simulate network congestion, remove setTimeout from production!
        }

        function initrelatedfile(){
            //初始化绑定iScroll控件 
            setTimeout(loaded, 100);
            $(document).off("pageinit", initrelatedfile);
        }
        $(document).on("pageinit", initrelatedfile)

    </script>
</asp:Content>