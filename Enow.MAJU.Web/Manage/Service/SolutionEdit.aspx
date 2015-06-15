<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/FinaWinBackPage.Master"
    AutoEventWireup="true" CodeBehind="SolutionEdit.aspx.cs" Inherits="Enow.MAJU.Web.Manage.Service.SolutionEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <link href="/Css/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Js/jquery-1.4.4.js"></script>
    <link href="/css/boxynew.css" rel="stylesheet" type="text/css" />
    <script src="/Js/jquery.blockUI.js" type="text/javascript"></script>
    <script src="/Js/table-toolbar.js" type="text/javascript"></script>
    <script src="/Js/kindeditor-4.1/kindeditor-min.js" type="text/javascript"></script>
    <script src="/Js/ValiDatorForm.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">
    <div class="clear">
    </div>
    <div class="contentbox">
        <div class="firsttable">
            <table width="100%" cellspacing="0" cellpadding="0" border="0" id="tableInfo">
                <tr>
                    <th width="100">
                        方案名称：
                    </th>
                    <td>
                        <asp:Literal ID="ltrFanganName" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <th width="100">
                        产品名称：
                    </th>
                    <td>
                        <asp:Literal ID="ltrProductName" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <th width="100">
                        客户姓名：
                    </th>
                    <td>
                        <asp:Literal ID="ltrTargetName" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <th width="100">
                        性别：
                    </th>
                    <td>
                        <asp:Literal ID="ltrGender" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <th width="100">
                        年龄：
                    </th>
                    <td>
                        <asp:Literal ID="ltrAge" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <th width="100">
                        年收入：
                    </th>
                    <td>
                        <asp:Literal ID="ltrRevenue" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <th width="100">
                        手机号码：
                    </th>
                    <td>
                        <asp:Literal ID="ltrMobile" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <th width="100">
                        方案状态：
                    </th>
                    <td>
                        <asp:DropDownList ID="ddlState" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
        </div>
        <div class="Basic_btn fixed">
            <ul>
                <li><a href="javascript:void(0);" id="btnSave">保存&gt;&gt;</a></li>
                <li><a href="javascript:void(0);" id="btnCanel">返回&gt;&gt;</a></li>
            </ul>
            <div class="hr_10">
            </div>
        </div>
    </div>
    <script type="text/javascript">
        var PageJsDataObj = {
            Data: {
                MId: '<%=Request.QueryString["MId"] %>',
                SMId: '<%=Request.QueryString["SMId"] %>',
                act: '<%=Request.QueryString["act"] %>',
                CId: '<%=Request.QueryString["CId"] %>',
                id: '<%=Request.QueryString["id"] %>'
            },
            DataBoxy: function () {/*弹窗默认参数*/
                return {
                    url: '',
                    title: "",
                    width: "710px",
                    height: "420px"
                }
            },
            ShowBoxy: function (data) {/*显示弹窗*/
                Boxy.iframeDialog({
                    iframeUrl: data.url,
                    title: data.title,
                    modal: true,
                    width: data.width,
                    height: data.height
                });
            },

            CheckForm: function () {
                var form = $("form").get(0);
                return ValiDatorForm.validator(form, "alert");

            },

            Save: function () {
                $("#btnSave").text("提交中...");
                KEditer.sync();
                PageJsDataObj.submit();
            },
            submit: function () {
                $.newAjax({
                    type: "post",
                    cache: false,
                    url: "SolutionEdit.aspx?dotype=save&" + $.param(PageJsDataObj.Data),
                    data: $("#btnSave").closest("form").serialize(),
                    dataType: "json",
                    success: function (ret) {
                        //ajax回发提示
                        if (ret.result != "0") {
                            tableToolbar._showMsg(ret.msg, function () {
                                if (document.referrer != "" && document.referrer != "undefined")
                                    window.location.href = document.referrer;
                                else
                                    window.location.href = "Solution.aspx?" + $.param(PageJsDataObj.Data);
                            });
                        } else {
                            tableToolbar._showMsg(ret.msg);
                            $("#btnSave").text("保 存");
                            PageJsDataObj.BindBtn();
                        }
                    },
                    error: function () {
                        tableToolbar._showMsg(tableToolbar.errorMsg);
                        $("#btnSave").text("保 存");
                        PageJsDataObj.BindBtn();
                    }
                });
            },
            PageInit: function () {
              
            },
            BindBtn: function () {

                $("#btnSave").unbind("click").click(function () {
                    if (PageJsDataObj.CheckForm()) {
                        PageJsDataObj.Save();
                    }
                });
                $("#btnCanel").unbind("click").click(function () {
                    window.history.go(-1);
                    return false;
                })
            }
        }
        $(function () {

            PageJsDataObj.PageInit();
            PageJsDataObj.BindBtn();
        });
    </script>
</asp:Content>
