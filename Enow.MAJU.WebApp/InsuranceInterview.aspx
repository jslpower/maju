<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InsuranceInterview.aspx.cs" Inherits="Enow.MAJU.WebApp.InsuranceInterview" MasterPageFile="~/MasterPage/WebApp.Master" %>

<asp:Content ID="Content2" ContentPlaceHolderID="bd_hd" runat="server">
    <script src="/js/myscroll.js" type="text/javascript"></script>
    <style>
    #wrapper {
	top:35px; bottom:0px; left:0;
}
    </style>
  <header>
    <a data-rel="back" data-direction="reverse"><i class="returnico"></i></a>
    <h1>面签预约</h1>
  </header>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="bd_bd" runat="server">
<div id="wrapper">
      <div class="list_item fangan_list mt20" id="scroller">
	    <div id="pullDown" style="display:none;">
		    <span class="pullDownIcon"></span><span class="pullDownLabel">下拉刷新...</span>
	    </div>
          <ul id="thelist">
          <asp:Repeater ID="rpt" runat="server"><ItemTemplate>
            <li class="R_jiantou"><span class='floatR <%#int.Parse(Eval("Status").ToString())==(int)Enow.MAJU.Model.EnumType.面签状态.已预约?"font_r":(int.Parse(Eval("Status").ToString())==(int)Enow.MAJU.Model.EnumType.面签状态.待确定?"font_blue":(int.Parse(Eval("Status").ToString())==(int)Enow.MAJU.Model.EnumType.面签状态.已取消?"font_hui":"font_green")) %>'><%#(Enow.MAJU.Model.EnumType.面签状态)int.Parse(Eval("Status").ToString())%></span>预约编号：<em class="font_red" onClick="javascript:$.mobile.changePage('insuranceinterviewdetail.aspx?id=<%#Eval("InterviewId") %>', { transition: 'slide' });"><%#Eval("OrderNo") %></em></li>
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
    pullDownAction = function () {
        setTimeout(function () {	// <-- Simulate network congestion, remove setTimeout from production!
            var el, li, l = "baoxian_a", h = "/images/photo.jpg";
            el = document.getElementById('thelist');
            el.innerHTML = "";

            var para = { page: pagedown };
            $.ajax({
                type: "Get",
                url: "/CommonPage/ajaxinsuranceinterview.aspx?" + $.param(para),
                cache: false,
                success: function (result) {
                    $(el).append(result);
                },
                error: function () {
                    ipage.ShowMsg("异常请重新提交！");
                }
            });
            myScroll.refresh(); 	//数据加载完成后，调用界面更新方法   Remember to refresh when contents are loaded (ie: on ajax completion)
        }, 1000);                          // <-- Simulate network congestion, remove setTimeout from production!
    };

    /**
    * 滚动翻页 （自定义实现此方法）
    * myScroll.refresh();		// 数据加载完成后，调用界面更新方法
    */
    pullUpAction = function () {
        setTimeout(function () {	// <-- Simulate network congestion, remove setTimeout from production!
            var el, li, l = "baoxian_a", h = "/images/photo.jpg";
            el = document.getElementById('thelist');

            //上拉获取精选话题和咨询
            var para = { page: pageup };
            $.ajax({
                type: "Get",
                url: "/CommonPage/ajaxinsuranceinterview.aspx?" + $.param(para),
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
        }, 1000);     // <-- Simulate network congestion, remove setTimeout from production!
    };

   function initinsuranceinterview(){
   setTimeout(loaded,100);
   $(document).off("pageinit",initinsuranceinterview);
   }
    $(document).on("pageinit", initinsuranceinterview);

    </script>
</asp:Content>

