<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="huatireplay.aspx.cs" Inherits="Enow.MAJU.WebApp.huatireplay"
    MasterPageFile="~/MasterPage/WebApp.Master" %>

<asp:Content ID="Content2" ContentPlaceHolderID="bd_hd" runat="server">
    <link rel="stylesheet" href="/css/huati.css" type="text/css" media="screen">
    <header>
        <a data-rel="back" data-direction="reverse"><i class="returnico"></i></a>
        <h1>
            <asp:Label ID="lbltitle" runat="server"></asp:Label></h1>
        <a href="#"><b class="icon_ok" id="report"></b></a>
    </header>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="bd_bd" runat="server">
    <div class="fabiao_box">
        <div class="fabiao_huati">
            <textarea cols="100" rows="23" id="content" name="content" data-role="none" placeholder="发表内容"></textarea>
        </div>
    </div>
    <div class="user-mask" data-role="none" style="display: none;" id="pop">
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
        ipop = {
            pop: $("#pop"),
            that: null
        };
        function ShowMsg(msg) {
            ipop.pop.find("span").text(msg);
            ipop.pop.show();
        }
        function inithuatireply() {
            ipop.pop.find(".btn_yellow").on("tap", function () {
                var Status = $("#hidStatus").val();
                if (Status == "1") {
                    var id = '<%=Enow.MAJU.Utility.Utils.GetQueryStringValue("Id") %>';
                    $.mobile.changePage("huatidetail.aspx?Id=" + id, { transition: "slideup"});
                 
                } else if (Status == "2") {
                  $.mobile.changePage("huatilist.aspx", { transition: "slideup" });
                
                }
                ipop.pop.hide();
            });

            $("#report").on("tap", function () {
                var str = $("#content").val().replace(/^(\s|\u00A0)+/, '').replace(/(\s|\u00A0)+$/, '');
                if (str == "" || str == "发表内容") {
                    ShowMsg("请输入发表内容!");
                    $("#content").focus();
                    return false;
                }

                if (str.length < 4) {
                    ShowMsg("发表内容的长度不能少于4个中文！");
                    $("#content").focus();
                    return false;
                }
                var id = '<%=Enow.MAJU.Utility.Utils.GetQueryStringValue("Id") %>';
                $.ajax({
                    type: "post",
                    url: '/huatireplay.aspx?type=save&id=' + id + '',
                    data: $("#form1").serialize(),
                    cache: false,
                    dataType: "json",
                    async: false,
                    success: function (responseType) {
                        // ShowMsg(responseType.msg);
                        var msg = responseType.msg;
                        if (responseType.result == "1" && id != '') {


                            $("#hidStatus").val("1");
                            ShowMsg(msg);
                        }
                        else {

                            $("#hidStatus").val("2");
                            ShowMsg(msg);



                        }
                    },
                    error: function () {
                        ShowMsg("请求异常，请重新提交!");
                        return false;
                    }
                });
            });
            $(document).off("pageinit", inithuatireply);
        }
        $(document).on("pageinit", inithuatireply);
    </script>
</asp:Content>
