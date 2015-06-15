<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Collect.aspx.cs" Inherits="Enow.MAJU.WebApp.Collect" MasterPageFile="~/MasterPage/WebApp.Master" %>

<asp:Content ID="Content2" ContentPlaceHolderID="bd_hd" runat="server">
    <script src="/js/myscroll.js" type="text/javascript"></script>
    <style>
    #wrapper {
	top:35px; bottom:0px; left:0;
}
    </style>
  <header>
    <a data-rel="back" data-direction="reverse"><i class="returnico"></i></a>
    <h1><%=this.T %>收藏</h1>
  </header>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="bd_bd" runat="server">
    <div id="wrapper">
      <div class="list_item mt10" id="scroller">
	    <div id="pullDown" style="display: none;">
		    <span class="pullDownIcon"></span><span class="pullDownLabel">下拉刷新...</span>
	    </div>
          <ul id="thelist">
            <asp:Repeater ID="rpt" runat="server"><ItemTemplate>
              <li><a href='<%#int.Parse(Eval("RelatedType").ToString())==(int)Enow.MAJU.Model.EnumType.收藏类别.话题?"huatidetail.aspx":(int.Parse(Eval("RelatedType").ToString())==(int)Enow.MAJU.Model.EnumType.收藏类别.资讯?"newsdetail.aspx":"baoxiandetail.aspx") %>?Id=<%#Eval("RelatedId") %>' data-transition="slide"><%#Enow.MAJU.Utility.Utils.GetText2(Eval("RelatedName").ToString(),21,true) %><i class="weidu_btn radius"><%#Eval("Collects")%></i></a></li>
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
                var el, li, l = "", h = "/images/touxian0.png";
                el = document.getElementById('thelist');
                el.innerHTML = "";

                $.getJSON(
                "collect.aspx"
                , { dotype: "getdatas", page: pagedown,T:"<%=Request.QueryString["T"] %>" }
                , function (result) {
                    $(result).each(function (i, item) {
                        switch(parseInt(item.RelatedType.toString())){
                            case <%=(int)Enow.MAJU.Model.EnumType.收藏类别.话题 %>:
                            l="huatidetail.aspx";
                            break;
                            case <%=(int)Enow.MAJU.Model.EnumType.收藏类别.资讯 %>:
                            l="newsdetail.aspx";
                            break;
                            default:
                            l="baoxiandetail.aspx";
                            break;
                        }
                        li = document.createElement('li');
                        li.innerHTML = '<a href="'+l+'?Id=' + item.RelatedId + '" data-transition="slide">' + (item.RelatedName.length < 21 ? item.RelatedName : item.RelatedName.substring(0, 21) + '...') + '<i class="weidu_btn radius">' + item.Collects + '</i></a>';
                        $(li).appendTo(el);
                    });
                });
                myScroll.refresh(); 	//数据加载完成后，调用界面更新方法   Remember to refresh when contents are loaded (ie: on ajax completion)
            }, 1000);                                 // <-- Simulate network congestion, remove setTimeout from production!
        }

        /**
        * 滚动翻页 （自定义实现此方法）
        * myScroll.refresh();		// 数据加载完成后，调用界面更新方法
        */
         pullUpAction=function() {
            setTimeout(function () {	// <-- Simulate network congestion, remove setTimeout from production!
                var el, li, l = "", h = "/images/touxian0.png";
                el = document.getElementById('thelist');

                //上拉获取翻页
                $.getJSON(
                "collect.aspx"
                , { dotype: "getdatas", page: pageup,T:"<%=Request.QueryString["T"] %>" }
                , function (result) {
                    if ($(result).length > 0) {
                        pageup += 1;
                    }
                    $(result).each(function (i, item) {
                        switch(parseInt(item.RelatedType.toString())){
                            case <%=(int)Enow.MAJU.Model.EnumType.收藏类别.话题 %>:
                            l="huatidetail.aspx";
                            break;
                            case <%=(int)Enow.MAJU.Model.EnumType.收藏类别.资讯 %>:
                            l="newsdetail.aspx";
                            break;
                            default:
                            l="baoxiandetail.aspx";
                            break;
                        }
                        li = document.createElement('li');
                        li.innerHTML = '<a href="'+l+'?Id=' + item.RelatedId + '" data-transition="slide">' + (item.RelatedName.length < 21 ? item.RelatedName : item.RelatedName.substring(0, 21) + '...') + '<i class="weidu_btn radius">' + item.Collects + '</i></a>';
                        el.appendChild(li, el.childNodes[0]);
                    });
                });
                myScroll.refresh(); 	// 数据加载完成后，调用界面更新方法 Remember to refresh when contents are loaded (ie: on ajax completion)
            }, 1000);   // <-- Simulate network congestion, remove setTimeout from production!
        }

        function initcollect(){
            setTimeout(loaded,100);
            $(document).off("pageinit",initcollect);
        }

        $(document).on("pageinit", initcollect);

    </script>
</asp:Content>
