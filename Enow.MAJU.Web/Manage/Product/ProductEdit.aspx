<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/FinaWinBackPage.Master"
    ValidateRequest="false" AutoEventWireup="true" CodeBehind="ProductEdit.aspx.cs"
    Inherits="Enow.MAJU.Web.Manage.Product.ProductEdit" %>

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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">
    <div class="clear">
    </div>
    <div class="contentbox">
        <div class="firsttable">
            <table width="100%" cellspacing="0" cellpadding="0" border="0" id="tableInfo">
                <tr>
                    <th width="100">
                        产品名称：
                    </th>
                    <td>
                        <asp:TextBox ID="txtProductName" runat="server" CssClass="input-txt formsize450"
                            MaxLength="100" errmsg="请填写产品名称!" valid="required"></asp:TextBox>
                        <span class="fontred">*</span>
                    </td>
                </tr>
                <tr>
                    <th width="100">
                        产品类别：
                    </th>
                    <td>
                        <asp:DropDownList ID="ddlType" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <th>
                        排序号：
                    </th>
                    <td>
                        <asp:TextBox ID="txtSortId" runat="server" CssClass="input-txt formsize40" MaxLength="6"
                            Text="0"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th width="100">
                        公司名称：
                    </th>
                    <td>
                        <asp:TextBox ID="txtCompany" runat="server" CssClass="input-txt formsize450" MaxLength="100"
                            errmsg="请填写公司名称!" valid="required"></asp:TextBox><span class="fontred">*</span>
                    </td>
                </tr>
                <tr>
                    <th width="100">
                        适合人群：
                    </th>
                    <td>
                        <asp:TextBox ID="txtTarget" runat="server" CssClass="input-txt formsize450" MaxLength="100"
                            errmsg="请填写适合人群!" valid="required"></asp:TextBox><span class="fontred">*</span>
                    </td>
                </tr>
                <tr>
                    <th width="100">
                        为什么适合您：
                    </th>
                    <td>
                        <asp:TextBox ID="txtAdvantage" runat="server" TextMode="MultiLine" Width="95%"></asp:TextBox><span
                            class="fontred">*</span>
                    </td>
                </tr>
                <tr>
                    <th width="100">
                        产品特点：
                    </th>
                    <td>
                        <asp:TextBox ID="txtOtherInfo" runat="server" TextMode="MultiLine" Width="95%"></asp:TextBox><span
                            class="fontred">*</span>
                    </td>
                </tr>
                <tr style="display: none;">
                    <th width="100">
                        相关资料：
                    </th>
                    <td>
                        <asp:TextBox ID="txtRelatedFile" runat="server" TextMode="MultiLine" Width="95%"></asp:TextBox>
                    </td>
                </tr>
                <tr style="display: none;">
                    <th width="100">
                        面签注意事项：
                    </th>
                    <td>
                        <asp:TextBox ID="txtInterviewNote" runat="server" TextMode="MultiLine" Width="95%"></asp:TextBox>
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
                KEditer.init("<%=txtAdvantage.ClientID %>", { resizeMode: 1, items: keMore_HaveImage, height: "300px" });
                KEditer.init("<%=txtOtherInfo.ClientID %>", { resizeMode: 1, items: keMore_HaveImage, height: "300px" });

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
                    url: "ProductEdit.aspx?dotype=save&" + $.param(PageJsDataObj.Data),
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
