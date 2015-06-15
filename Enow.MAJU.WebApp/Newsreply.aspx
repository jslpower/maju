<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebApp.Master" AutoEventWireup="true"
    CodeBehind="Newsreply.aspx.cs" Inherits="Enow.MAJU.WebApp.Newsreply" %>

<asp:Content ID="Content2" ContentPlaceHolderID="bd_hd" runat="server">
    <link rel="stylesheet" href="/css/huati.css" type="text/css" media="screen">
    <header>
        <a data-rel="back" data-direction="reverse"><i class="returnico"></i></a>
        <h1>
            回复</h1>
        <a href=""><b class="icon_ok" id="newsreply"></b></a>
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
                   
                    <li style="width:100%;"><a href="" class="btn_yellow radius4"  data-role="none">
                        确定</a></li>
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

        $(function () {
            ipop.pop.find(".btn_yellow").on("tap", function () {
                var Status = $("#hidStatus").val();
                if (Status == "1") {
                    var id = '<%=Enow.MAJU.Utility.Utils.GetQueryStringValue("Id") %>';
                    $.mobile.changePage("Newsdetail.aspx?Id=" + id, { transition: "slideup" });
                } else if (Status == "2") {
                    $.mobile.changePage("newslist.aspx", { transition: "slideup" });
                }
                ipop.pop.hide();
            });

            $("#newsreply").click(function () {
                if ($("#content").val() == "" || $("#content").val() == "发表内容") {
                    ShowMsg("请输入发表内容!");
                    $("#content").focus();
                    return false;
                }

                if ($("#content").val().length < 4) {
                    ShowMsg("发表内容的长度不能少于4个中文！");
                    $("#content").focus();
                    return false;
                }
                var id = '<%=Enow.MAJU.Utility.Utils.GetQueryStringValue("Id") %>';
                $.ajax({
                    type: "post",
                    url: '/Newsreply.aspx?type=save&id=' + id + '',
                    data: $("#form1").serialize(),
                    cache: false,
                    dataType: "json",
                    async: false,
                    success: function (responseType) {
  
                        var msg = responseType.msg;
                        if (responseType.result == "1" && id != '') {
                            $("#hidStatus").val("1");
                            ShowMsg(msg);
                        }
                        else {
                            //  $.mobile.changePage("newslist.aspx", { transition: "slideup" });
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

        })
    </script>
</asp:Content>
