<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SysMessageReply.aspx.cs" Inherits="Enow.MAJU.WebApp.SysMessageReply" MasterPageFile="~/MasterPage/WebApp.Master" %>

<asp:Content ID="Content2" ContentPlaceHolderID="bd_hd" runat="server">
    <link rel="stylesheet" href="/css/huati.css" type="text/css" media="screen">
    <header>
        <a data-rel="back" data-direction="reverse"><i class="returnico"></i></a>
        <h1>
            消息反馈</h1>
        <a href=""><b class="icon_ok" id="report"></b></a>
    </header>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="bd_bd" runat="server">
    <div class="fabiao_box">
        <div class="fabiao_huati">
            <textarea cols="100" rows="23" id="content" name="content" data-role="none" placeholder="发表内容"></textarea>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="bd_ft" runat="server">
    <script type="text/javascript">

    function initsysmessagereply(){
            $("#report").on("tap",function () {
                if ($("#content").val() == "" || $("#content").val() == "发表内容") {
                    ipage.ShowMsg("请输入发表内容!");
                    $("#content").focus();
                    return false;
                }

                if ($("#content").val().length < 4) {
                    ipage.ShowMsg("发表内容的长度不能少于4个中文！");
                    $("#content").focus();
                    return false;
                }
                var id = '<%=Enow.MAJU.Utility.Utils.GetQueryStringValue("Id") %>';
                $.ajax({
                    type: "post",
                    url: 'sysmessagereply.aspx?type=save&messageid=<%=Request.QueryString["messageid"] %>&id=' + id + '',
                    data: $("#form1").serialize(),
                    cache: false,
                    dataType: "json",
                    async: false,
                    success: function (responseType) {
                        ipage.ShowMsg(responseType.msg);
                        if (responseType.result == "1") {
                            $.mobile.changePage("SysMessage.aspx", { transition: "slideup" });
                        }
                    },
                    error: function () {
                        ipage.ShowMsg("请求异常，请重新提交!");
                        return false;
                    }
                });
            });
            $(document).off("pageinit",initsysmessagereply);
    }
        $(document).on("pageinit",initsysmessagereply);
    </script>
</asp:Content>