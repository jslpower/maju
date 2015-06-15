<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Weibo.aspx.cs" Inherits="Enow.MAJU.WebApp.Weibo" MasterPageFile="~/MasterPage/WebApp.Master" EnableEventValidation="false" %>

<asp:Content ID="Content2" ContentPlaceHolderID="bd_hd" runat="server">
<div id="wb_connect_btn"></div>
<script type="text/javascript">
//    WB2.anyWhere(function (W) {
//        W.widget.connectButton({
//            id: "weibo",
//            type: '3,2',
//            callback: {
//                login: function (o) { //登录后的回调函数
//                    $.ajax({ type: "post", url: "Weibo.aspx?dotype=login&id=" + o.id + "&screen_name=" + o.screen_name + "&gender=" + o.gender + "&avatar_large=" + o.avatar_large + "", cache: false, dataType: "json", async: false,
//                        success: function (response) {
//                            if (response.result != "1") {
//                                alert(response.msg);
//                            }
//                            $.mobile.changePage(response.obj, { transition: "slideup" });
//                        },
//                        error: function () {
//                            alert("请求异常");
//                        }
//                    });

//                },
//                logout: function () { //退出后的回调函数
//                    alert('logout');
//                }
//            }
//        });
//    });
</script>
</asp:Content>

