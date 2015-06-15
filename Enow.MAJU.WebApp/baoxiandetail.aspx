<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="baoxiandetail.aspx.cs"
    Inherits="Enow.MAJU.WebApp.baoxiandetail" MasterPageFile="~/MasterPage/WebApp.Master" %>

<asp:Content ID="Content2" ContentPlaceHolderID="bd_hd" runat="server">
    <link rel="stylesheet" href="/css/home.css" type="text/css" media="screen">
    <link rel="stylesheet" href="css/huati.css" type="text/css" media="screen">
    <header>
        <a data-rel="back" data-direction="reverse"><i class="returnico"></i></a>
        <h1>
            保险详情</h1>
        <a href="" id="reply12"><b class="icon_jia"></b></a>
    </header>
    <div class="head_div" style="display: block;">
        <div class="head_box" style="display: none;">
            <ul>
                <li id="collect12">收藏</li>
                <%--<li id="share">分享</li>--%>
            </ul>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="bd_bd" runat="server">
    <div class="baoxian_list mt10">
        <ul>
            <li>
                <dl>
                    <dt class="cent">
                        <asp:Literal ID="litProductName" runat="server"></asp:Literal></dt>
                    <dd>
                        <span class="Rtxt">收藏人数：<em class="font_yellow"><asp:Literal ID="litCollects" runat="server"></asp:Literal></em>人</span>出品公司：<asp:Literal
                            ID="litCompany" runat="server"></asp:Literal></dd>
                    <dd>
                        适合人群：<em class="font_yellow"><asp:Literal ID="litTarget" runat="server"></asp:Literal></em></dd>
                </dl>
            </li>
        </ul>
    </div>
    <div class="list_item mt10">
        <ul>
            <li class="R_jiantou" onclick="javascript:$.mobile.changePage('youdian.aspx?T=1&id=<%=Request.QueryString["id"] %>', { transition: 'slide' });">
                为什么适合您</li>
            <li class="R_jiantou" onclick="javascript:$.mobile.changePage('youdian.aspx?T=2&id=<%=Request.QueryString["id"] %>', { transition: 'slide' });">
                产品特点</li>
        </ul>
    </div>
    <div class="caozuo mt20 paddB">
        <ul>
            <li><a id="btn_red" class="btn_red radius4">我要做方案</a></li>
            <li><a id="btn_yellow" class="btn_yellow radius4">我要咨询</a></li>
        </ul>
    </div>
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

    function ShowMsg(msg) {
    var id = $("#popMsg");
    id.find("span").text(msg);
    id.show();
}

        function initbaoxiandetail() {
         $("#popMsg").find(".btn_yellow").on("tap", function () {
          $("#popMsg").hide();
            });
            $("#reply12").on("tap", function () {
                if ($(".head_box").is(":visible")) {
                    $(".head_box").hide();
                }
                else {
                    $(".head_box").show();
                }
            });
            $("#btn_yellow").on("tap", function () { 
            var login=<%=loginState %>;
            var id='<%=Request.QueryString["id"] %>';
            if (login==0) {
                        window.location.href="/Default.aspx";
                   }
                       else{
                      $.mobile.changePage("InsuranceConsult.aspx?ProductId=" +id , { transition: "slide" });
                                }
                         });

                         $("#btn_red").on("tap",function(){
                         
                          var login=<%=loginState %>;
            var id='<%=Request.QueryString["id"] %>';
            if (login==0) {
                        window.location.href="/Default.aspx";
                   }
                       else{
                      $.mobile.changePage("fangan.aspx?ProductId=" +id , { transition: "slide" });
                                }
                         });
            $("#collect12").on("tap", function () {
                $(".head_box").hide();
                var id = '<%=Enow.MAJU.Utility.Utils.GetQueryStringValue("Id") %>';
                $.ajax({
                    type: "post",
                    url: '/huatidetail.aspx?type=collect&t=<%=(int)Enow.MAJU.Model.EnumType.收藏类别.保险 %>&id=' + id + '',

                    cache: false,
                    dataType: "json",
                    async: false,
                    success: function (responseType) {
                        ShowMsg(responseType.msg);
                        return false;
                    },
                    error: function () {
                        ShowMsg("请求异常，请重新提交");
                        return false;
                    }
                });
            });

            $(document).off("pageinit", initbaoxiandetail);
        }

        $(document).on("pageinit", initbaoxiandetail)
    </script>
</asp:Content>
