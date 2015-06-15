<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="myfangan.aspx.cs" Inherits="Enow.MAJU.WebApp.myfangan"
    MasterPageFile="~/MasterPage/WebApp.Master" %>

<asp:Content ID="Content2" ContentPlaceHolderID="bd_hd" runat="server">
    <script src="/js/myscroll.js" type="text/javascript"></script>
    <style>
        #wrapper
        {
            top: 120px;
            bottom: 0px;
        }
    </style>
    <header>
        <a data-rel="back" data-direction="reverse"><i class="returnico"></i></a>
        <h1>
            我的方案</h1>
        <%if (1 == 0) %>
        <%{ %>
        <i class="icon_close" style="display: block;"></i>
        <%} %>
        <i class="icon_del"></i>
    </header>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="bd_bd" runat="server">
    <div id="wrapper">
        <div class="list_item fangan_list mt10" id="scroller">
            <div id="pullDown" style="display:none;">
                <span class="pullDownIcon"></span><span class="pullDownLabel">下拉刷新...</span>
            </div>
            <ul id="thelist">
                <asp:Repeater ID="rpt" runat="server">
                    <ItemTemplate>
                       <li><span class='floatR  <%#int.Parse(Eval("State").ToString())==(int)Enow.MAJU.Model.EnumType.方案状态.已预约?"font_r":(int.Parse(Eval("State").ToString())==(int)Enow.MAJU.Model.EnumType.方案状态.已发送?"font_green":(int.Parse(Eval("State").ToString())==(int)Enow.MAJU.Model.EnumType.方案状态.待发送?"font_blue":"font_blue")) %>'>
                            <%#(Enow.MAJU.Model.EnumType.方案状态)int.Parse(Eval("State").ToString())%></span><a
                                href="javascript:$.mobile.changePage('fanganDetail.aspx?id=<%#Eval("SolutionId") %>', { transition: 'slide' });"><em
                                    class="font_yellow"><%#Eval("TargetName")%></em>的<%#Eval("ProductName") %></a><a
                                        href="javascript:;" class="fxk" data-id="<%#Eval("SolutionId") %>" data-productid="<%#Eval("ProductId") %>"></a></li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
            <div id="pullUp" style="display:none;">
                <span class="pullUpIcon"></span><span class="pullUpLabel">上拉加载更多...</span>
            </div>
        </div>
    </div>
    <div class="caozuo mt20 paddB">
        <ul>
            <li><a id="zhitong" href="javascript:;" class="btn_red radius4">面签直通车</a></li>
            <li><a id="zixun" href="javascript:;" class="btn_yellow radius4">我要咨询</a></li>
        </ul>
    </div>
    <!-----提示信息--------->
    <div class="user-mask" style="display: none;" data-role="none" id="popconfirm">
        <div class="user-mask-box radius4" style="margin-top: -63px;">
            <div class="font18 cent">
                您确定要删除吗？</div>
            <div class="caozuo mt10">
                <ul>
                    <li class="paddR10"><a href="" class="btn_gray radius4">取消</a></li>
                    <li style="padding-left: 10px;"><a href="" class="btn_yellow radius4">确定</a></li>
                </ul>
            </div>
        </div>
    </div>
    <!--消息弹出框-->
        <div class="user-mask" data-role="none" style="display: none;" id="popMsg">
        <div class="user-mask-box radius4" style="margin-top: -95px;">
            <div class="font18 cent">
                <input type="hidden" id="hidStatus" value="0" />
                <span></span>
            </div>
            <div class="caozuo mt10">
                <ul>
                    <li style="width: 100%;"><a href="" class="btn_yellow radius4" data-role="none">确定</a></li>
                </ul>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="bd_ft" runat="server">
    <script type="text/javascript">
    $(function () {
        var isLogin = "<%=IsLoginCheck() %>";
        if (isLogin == "False") {
            window.location.href = "/Default.aspx";
}
      });
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
                    url: "/CommonPage/ajaxMyfangan.aspx?" + $.param(para),
                    cache: false,
                    success: function (result) {
                        if (result != "") {
                                $(el).empty();
                                $(el).append(result);
                            } 

                    },
                    error: function () {
                        ShowMsg("异常请重新提交！");
                    }
                });

                myScroll.refresh(); 	//数据加载完成后，调用界面更新方法   Remember to refresh when contents are loaded (ie: on ajax completion)
            }, 1000);                          // <-- Simulate network congestion, remove setTimeout from production!
            $(".fxk").on("tap", function () { $(this).toggleClass("fxk_on"); });
        }
        /**
        * 滚动翻页 （自定义实现此方法）
        * myScroll.refresh();		// 数据加载完成后，调用界面更新方法
        */
        pullUpAction = function () {

            setTimeout(function () {	// <-- Simulate network congestion, remove setTimeout from production!
                var el;
                el = document.getElementById('thelist');

                //上拉获取精选话题和咨询
                var para = { page: pageup };
                $.ajax({
                    type: "Get",
                    url: "/CommonPage/ajaxMyfangan.aspx?" + $.param(para),
                    cache: false,
                    success: function (result) {
                        if ($.trim(result).length > 0) {
                            pageup += 1;
                        }
                        $(el).append(result);

                    },
                    error: function () {
                        ShowMsg("异常请重新提交！");
                    }
                });

                myScroll.refresh(); 	// 数据加载完成后，调用界面更新方法 Remember to refresh when contents are loaded (ie: on ajax completion)
            }, 1000);    // <-- Simulate network congestion, remove setTimeout from production!
            $(".fxk").on("tap", function () { $(this).toggleClass("fxk_on"); });

        }
        function ShowMsg(msg) {
    var id = $("#popMsg");
    id.find("span").text(msg);
    id.show();
}


        function initmyfangan() {
        $("#popMsg").find(".btn_yellow").on("tap", function () {
          $("#popMsg").hide();
            });
            var ids = [];
            //初始化绑定iScroll控件 
            setTimeout(loaded, 100);
            $(".fxk").on("tap", function () { $(this).toggleClass("fxk_on"); });
            $(".icon_del").on("tap", function () {
                ids = [];
                $(".fxk_on").each(function () {
                    ids.push($(this).attr("data-id"));
                });
                if (ids.length == 0) {
                    ShowMsg("请选择您要删除的方案");
                    return false;
                }
                $("#popconfirm").show();
            });
            $("#popconfirm").find(".btn_gray").on("tap", function () { $("#popconfirm").hide(); });
            $("#popconfirm").find(".btn_yellow").on("tap", function () {
                $("#popconfirm").hide();
                $.getJSON(
                    "myfangan.aspx"
                    , { dotype: "dodel", ids: ids.join(',') }
                    , function (result) {
                        $(result.obj).each(function (i, item) {
                            $("a[data-id='" + item + "']").closest("li").remove();
                        });
                        alert(result.msg);
                    });
            });
            $("#zixun").on("tap", function () {
                ids = [];
                $(".fxk_on").each(function () {
                    ids.push($(this).attr("data-productid"));
                });
                if (ids.length == 0) {
                    ShowMsg("请选择您要咨询的产品");
                    return false;
                }
                if (ids.length > 1) {
                    ShowMsg("请选择一个您要咨询的产品");
                    return false;
                }
                $.mobile.changePage("InsuranceConsult.aspx?productid=" + ids.join(','), { transition: "slide" });
            });
            $("#zhitong").on("tap", function () {
                ids = [];
                $(".fxk_on").each(function () {
                    ids.push($(this).attr("data-id"));
                });
                if (ids.length == 0) {
                    ShowMsg("请选择您要面签的方案");
                    return false;
                }
                if (ids.length > 1) {
                    ShowMsg("请选择一个您要面签的方案");
                    return false;
                }
                $.mobile.changePage("zhitongche.aspx?id=" + ids.join(','), { transition: "slide" });
//                window.location.href = "zhitongche.aspx?id=" + ids.join(',');
            });

            $(document).off("pageinit", initmyfangan);
        }
        $(document).on("pageinit", initmyfangan);
    </script>
</asp:Content>
