<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/FinaWinBackPage.Master"
    AutoEventWireup="true" CodeBehind="ServiceEdit.aspx.cs" Inherits="Enow.MAJU.Web.Manage.Adv.ServiceEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <link href="/Css/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Js/jquery-1.4.4.js"></script>
    <link href="/css/boxynew.css" rel="stylesheet" type="text/css" />
    <script src="/Js/jquery.blockUI.js" type="text/javascript"></script>
    <script src="/Js/table-toolbar.js" type="text/javascript"></script>
    <script src="/Js/ValiDatorForm.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">
    <div class="firsttable">
        <table width="100%" cellspacing="0" cellpadding="0" border="0" id="tableInfo">
            <tr>
                <th width="100">
                    微信订阅号：
                </th>
                <td>
                    <asp:TextBox ID="txtWechat" runat="server" CssClass="input-txt formsize240" MaxLength="100"
                        errmsg="请填写微信订阅号!" valid="required"></asp:TextBox>
                    <span class="fontred">*</span>
                </td>
            </tr>
            <tr>
                <th width="100">
                    企业QQ：
                </th>
                <td>
                    <asp:TextBox ID="txtQQ" runat="server" MaxLength="12" valid="required|isNo" errmsg="请填写企业QQ号|企业QQ号只能为数字"
                        CssClass="input-txt formsize240"></asp:TextBox>
                    <span class="fontred">*</span>
                </td>
            </tr>
            <tr>
                <th width="100">
                    新浪微博：
                </th>
                <td>
                    <asp:TextBox ID="txtWeibo" runat="server" MaxLength="4" valid="required" errmsg="请填写微博账号"
                        CssClass="input-txt formsize240"></asp:TextBox>
                    <span class="fontred">*</span>
                </td>
            </tr>
            <tr>
                <th width="100">
                    企业邮箱：
                </th>
                <td>
                    <asp:TextBox ID="txtEmail" runat="server" valid="required|isEmail" errmsg="请填写企业邮箱账号|请填写正确的邮箱地址"
                        CssClass="input-txt formsize240"></asp:TextBox>
                    <span class="fontred">*</span>
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
            Form: null,
            Save: function () {
                $("#btnSave").text("提交中...");

                PageJsDataObj.submit();
            },
            submit: function () {
                $.newAjax({
                    type: "post",
                    cache: false,
                    url: "ServiceEdit.aspx?dotype=save&" + $.param(PageJsDataObj.Data),
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
