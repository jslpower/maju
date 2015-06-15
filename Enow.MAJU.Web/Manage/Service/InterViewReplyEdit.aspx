<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/FinaWinBackPage.Master"
    AutoEventWireup="true" CodeBehind="InterViewReplyEdit.aspx.cs" Inherits="Enow.MAJU.Web.Manage.Service.InterViewReplyEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <link href="/Css/style.css" rel="stylesheet" type="text/css" />
    <link href="/Css/swfupload/default.css" type="text/css" />
    <script type="text/javascript" src="/Js/jquery-1.4.4.js"></script>
    <link href="/css/boxynew.css" rel="stylesheet" type="text/css" />
    <script src="/Js/jquery.blockUI.js" type="text/javascript"></script>
    <script src="/Js/table-toolbar.js" type="text/javascript"></script>
    <script src="/Js/kindeditor-4.1/kindeditor-min.js" type="text/javascript"></script>
    <script src="/Js/swfupload/swfupload.js" type="text/javascript"></script>
    <script src="/Js/ValiDatorForm.js" type="text/javascript"></script>
    <script type="text/javascript" src="/Js/datepicker/WdatePicker.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">
    <div class="clear">
    </div>
    <div class="contentbox">
        <div class="firsttable">
            <table width="100%" cellspacing="0" cellpadding="0" border="0" id="tableInfo">
                <tr>
                    <th width="100">
                        预约编号：
                    </th>
                    <td>
                        <asp:Literal ID="ltrOrderNo" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <th width="100">
                        客户姓名：
                    </th>
                    <td>
                        <asp:Literal ID="ltrContactName" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <th width="100">
                        面签对象：
                    </th>
                    <td>
                        <asp:Literal ID="ltrTarget" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <th>
                        联系电话：
                    </th>
                    <td>
                        <asp:Literal ID="ltrTel" runat="server"></asp:Literal>
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
                        客户经理：
                    </th>
                    <td>
                        <asp:TextBox ID="txtManager" runat="server" CssClass="input-txt formsize240" MaxLength="100"
                            errmsg="请填写客户经理姓名!" valid="required"></asp:TextBox><span class="fontred">*</span>
                    </td>
                </tr>
                <tr>
                    <th width="100">
                        客户经理电话：
                    </th>
                    <td>
                        <asp:TextBox ID="txtManagerTel" runat="server" CssClass="input-txt formsize180" errmsg="请填写客户经理联系方式"
                            valid="required"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th width="100">
                        预约时间：
                    </th>
                    <td>
                        <asp:TextBox ID="txtTime" runat="server" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})"
                            CssClass="input-txt formsize140" errmsg="请选择预约时间!" valid="required"></asp:TextBox><span
                                class="fontred">*</span>
                    </td>
                </tr>
                <tr>
                    <th width="100">
                        预约地点：
                    </th>
                    <td>
                        <asp:TextBox ID="txtAddress" runat="server" CssClass="input-txt formsize240" errmsg="请填写预约地点!"
                            valid="required"></asp:TextBox><span class="fontred">*</span>
                    </td>
                </tr>
                <tr>
                    <th width="100">
                        预约状态：
                    </th>
                    <td>
                        <asp:DropDownList ID="ddlStatus" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <th width="100">
                        面签注意事项：
                    </th>
                    <td>
                        <asp:TextBox ID="txtInterviewNote" runat="server" TextMode="MultiLine" Width="95%"
                            errmsg="请填写面签注意事项!" valid="required"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th width="100">
                        预约反馈：
                    </th>
                    <td>
                        <asp:TextBox ID="txtContext" runat="server" TextMode="MultiLine" Width="95%"></asp:TextBox>
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
            CreatePlanEdit: function () {

                KEditer.init("<%=txtContext.ClientID %>", { resizeMode: 1, items: keMore_HaveImage, height: "300px" });
                KEditer.init("<%=txtInterviewNote.ClientID %>", { resizeMode: 1, items: keMore_HaveImage, height: "300px" });
            },

            CheckForm: function () {
                var form = $("form").get(0);
                return ValiDatorForm.validator(form, "alert");

            },
            Form: null,
            Save: function () {
                $("#btnSave").text("提交中...");
                KEditer.sync();
                PageJsDataObj.submit();
            },
            submit: function () {
                $.newAjax({
                    type: "post",
                    cache: false,
                    url: "InterViewReplyEdit.aspx?dotype=save&" + $.param(PageJsDataObj.Data),
                    data: $("#btnSave").closest("form").serialize(),
                    dataType: "json",
                    success: function (ret) {
                        //ajax回发提示
                        if (ret.result != "0") {
                            tableToolbar._showMsg(ret.msg, function () {
                                if (document.referrer != "" && document.referrer != "undefined")
                                    window.location.href = document.referrer;
                                else
                                    window.location.href = "Default.aspx?" + $.param(PageJsDataObj.Data);
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
                PageJsDataObj.CreatePlanEdit();
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
