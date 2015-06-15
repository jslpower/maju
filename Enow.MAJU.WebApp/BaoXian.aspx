<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BaoXian.aspx.cs" Inherits="Enow.MAJU.WebApp.BaoXian" MasterPageFile="~/MasterPage/WebApp.Master" %>

<asp:Content ID="Content2" ContentPlaceHolderID="bd_hd" runat="server">
    <link rel="stylesheet" href="/css/home.css" type="text/css" media="screen">
    <script src="/js/myscroll.js" type="text/javascript"></script>
    <style>
    #wrapper {
	top:35px; bottom:0px; left:0;
}
    </style>
  <header>
    <a data-rel="back" data-direction="reverse"><i class="returnico"></i></a>
    <h1>保险</h1>
        <a href="messagecenter.aspx" data-transition="slide"><b class="icon_email">
        <%if (!(new Enow.MAJU.WebApp.MessageCenter().IsMemberTopicReplyAllRead && new Enow.MAJU.WebApp.MessageCenter().IsSysMessageAllRead && new Enow.MAJU.WebApp.MessageCenter().IsProductReplyAllRead)) %>
        <%{ %>
        <em class="radius"></em>
        <%} %>
        </b></a>
  </header>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="bd_bd" runat="server">
    <div id="wrapper">
     <div class="baoxian_list mt10" id="scroller">
		    <div id="pullDown" style="display:none;">
			    <span class="pullDownIcon"></span><span class="pullDownLabel">下拉刷新...</span>
		    </div>
         <ul id="thelist">
         <asp:Repeater ID="rpt1" runat="server" OnItemDataBound="rpt1_OnItemDataBound"><ItemTemplate>
             <li>
                 <dl>
                     <dt><%#Eval("TypeName") %><%#Eval("IsTop").ToString()=="1"?"<i class='din'>顶</i>":"" %></dt>
                     <asp:Repeater ID="rpt2" runat="server"><ItemTemplate>
                     <dd><span class="Rtxt"><%#Eval("Company")%></span><a href="baoxiandetail.aspx?id=<%#Eval("ProductId") %>" data-transition="slide"><%#Container.ItemIndex+1 %>.<%#Eval("ProductName")%></a></dd>
                     </ItemTemplate></asp:Repeater>
                 </dl>
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
                    url: "/CommonPage/ajaxBaoXian.aspx?" + $.param(para),
                    cache: false,
                    success: function (result) {
                        $(el).append(result);
                    },
                    error: function () {
                        alert("异常请重新提交！");
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
                    url: "/CommonPage/ajaxBaoXian.aspx?" + $.param(para),
                    cache: false,
                    success: function (result) {
                        if ($.trim(result).length>0) {
                            pageup += 1;
                        }
                        $(el).append(result);
                    },
                    error: function () {
                        alert("异常请重新提交！");
                    }
                });

                myScroll.refresh(); 	// 数据加载完成后，调用界面更新方法 Remember to refresh when contents are loaded (ie: on ajax completion)
            }, 1000);    // <-- Simulate network congestion, remove setTimeout from production!
        }
        function initbaoxian() {
            //初始化绑定iScroll控件 
            setTimeout(loaded, 100);
            $(document).off("pageinit", initbaoxian);
        }
        $(document).on("pageinit", initbaoxian)
    </script>
</asp:Content>
