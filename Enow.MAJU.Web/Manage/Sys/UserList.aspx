﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/FinaWinBackPage.Master"
    AutoEventWireup="true" CodeBehind="UserList.aspx.cs" Inherits="Enow.MAJU.Web.Manage.Sys.UserList" %>

<%@ Register Assembly="Enow.MAJU.Utility" Namespace="Enow.MAJU.Utility.ExportPageSet"
    TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <link href="/css/boxynew.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/js/jquery.boxy.js"></script>
    <!--[if lte IE 6]><script src="/js/jquery.bgiframe.min.js" type="text/javascript"></script><![endif]-->
    <script type="text/javascript" src="/js/jquery.blockUI.js"></script>
    <script type="text/javascript" src="/js/moveScroll.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">
    <div class="contentbox">
        <div class="searchbox fixed">
            <span class="searchT">
                <p>
                    用户名：
                    <asp:TextBox ID="txtuserName" CssClass="formsize120 input-txt" runat="server" />
                    &nbsp;&nbsp;姓名：
                    <asp:TextBox ID="txtKeyWord" CssClass="formsize120 input-txt" runat="server" />
                    &nbsp;&nbsp;
                    <asp:Button ID="btnSearch" runat="server" Text="搜索" CssClass="search-btn" CausesValidation="False"
                        OnClick="btnSearch_Click" />
                </p>
            </span>
        </div>
        <div class="listbox">
           <div id="tablehead" class="tablehead">
                <ul class="fixed">
                    <li><a href="javascript:void(0)" hidefocus="true" class="toolbar_add add_gg"><s class="addicon"></s>
                        新增</a></li>
                    <li class="line"></li>
                    <li><a href="#" hidefocus="true" class="toolbar_update"><s class="updateicon"></s>修改用户</a></li>
                    <li class="line"></li>
                    <li><a href="#" hidefocus="true" class="toolbar_disabled" class="toolbar_disabled"><s
                        class="jinyicon"></s>禁用</a></li>
                    <li class="line"></li>
                    <li><a href="#" hidefocus="true" class="toolbar_enable" class="toolbar_enable"><s
                        class="qiyicon"></s>启用</a></li>
                </ul>
                <div class="pages">
                    <cc1:ExportPageInfo ID="ExportPageInfo1" runat="server" />
                </div>
            </div>
            <!--列表表格-->
            <div class="tablelist-box">
                <table width="100%" id="liststyle">
                    <tr>
                        <th width="30" class="thinputbg">
                            <input type="checkbox" name="checkbox" id="checkbox" />
                        </th>
                        <th align="left">
                            姓名
                        </th>
                        <th align="center">
                            用户名
                        </th>
                        <th align="center">
                            用户角色
                        </th>
                        <th align="center">
                            联系电话
                        </th>
                        <th align="center">
                            最后登陆时间
                        </th>
                        <th align="center">
                            状态
                        </th>
                        <th align="center">
                            授权
                        </th>
                    </tr>
                    <asp:Repeater ID="rptList" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td align="center">
                                    <input type="checkbox" name="checkbox" id="checkbox" value="<%#Eval("Id") %>" data-state="<%#Convert.ToBoolean(Eval("IsEnable"))?"1":"0" %>" />
                                </td>
                                <td align="left">
                                    <%#Eval("ContactName")%>
                                </td>
                                <td align="center">
                                    <%#Eval("UserName")%>
                                </td>
                                <td align="center">
                                    <%#GetRoleName(Eval("RoleId"))%>
                                </td>
                                <td align="center">
                                    <%#Eval("ContactTel")%>
                                </td>
                                <td align="center">
                                    <%#Eval("LastLoginTime","{0:yyyy-MM-dd}")%>
                                </td>
                                <td align="center">
                                    <%#(bool)Eval("IsEnable") ? "开通" : "<span class=\"fontred\">禁用</span>"%>
                                </td>
                                <td align="center">
                                    <a href="javascript:void(0)" class="transactions" data-rel='<%#Eval("Id") %>'>独立授权</a>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <asp:PlaceHolder ID="phNoData" runat="server" Visible="false">
                        <tr>
                            <td colspan="7" align="center">
                                暂无数据
                            </td>
                        </tr>
                    </asp:PlaceHolder>
                </table>
            </div>
                <div class="tablehead botborder">
                <script type="text/javascript">
                    document.write(document.getElementById("tablehead").innerHTML);
                </script>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        var PageJsDataObj = {
            Query: {/*URL参数对象*/
                sl: ''
            },
            //浏览弹窗关闭后刷新当前浏览数
            BindClose: function () {
                $("a[data-class='a_close']").unbind().click(function () {
                    window.location.reload();
                    return false;
                })
            },
            DataBoxy: function () {/*弹窗默认参数*/
                return {
                    url: '/Manage/Sys',
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
            GoAjax: function (url) {
                $.newAjax({
                    type: "post",
                    cache: false,
                    url: url,
                    dataType: "json",
                    success: function (result) {
                        if (result.result == "1") {
                            tableToolbar._showMsg(result.msg, function () {
                                window.location.reload();
                            });
                        }
                        else { tableToolbar._showMsg(result.msg); }
                    },
                    error: function () {
                        //ajax异常--你懂得
                        tableToolbar._showMsg(tableToolbar.errorMsg);
                    }
                });
            },
             Add: function () {
                var data = this.DataBoxy();
                data.url += '/UserAdd.aspx?';
                data.title = '新增管理员';
                data.url += $.param({
                    sl: this.Query.sl,
                    MId:<%=Request.QueryString["MId"] %>,
                    SMId:<%=Request.QueryString["SMId"] %>,
                    CId:<%=Request.QueryString["CId"] %>,
                    act:"add",
                    doType: "add"
                });
              this.ShowBoxy(data);
            },
            Update: function (objsArr) {
                var data = this.DataBoxy();
                data.url += '/UserEdit.aspx?';
                data.title = '修改用户信息';
                data.url += $.param({
                    sl: this.Query.sl,
                    doType: "update",
                    id: objsArr[0].find('input[type="checkbox"]').val()
                });
                this.ShowBoxy(data);
            },
            Enable: function (objsArr) {
                var list = new Array();
                var state=new Array();
                for (var i = 0; i < objsArr.length; i++) {
                    list.push(objsArr[i].find("input[type='checkbox']").val());
                     state.push(objsArr[i].find("input[type='checkbox']").attr("data-state"));
                }
                  for (var j = 0; j < state.length; j++) {
                    if (state[j] != '0') {
                        tableToolbar._showMsg("该记录已是启用状态！");
                        return;
                    }
                }
                var data = this.DataBoxy();
                data.url += "/UserList.aspx?";
                data.url += $.param({
                    sl: this.Query.sl,
                    doType: "Enable",
                    id: list.join(",")
                });
                this.GoAjax(data.url);
            },
            Disabled: function (objsArr) {
                var list = new Array();
                var state=new Array();
                for (var i = 0; i < objsArr.length; i++) {
                    list.push(objsArr[i].find("input[type='checkbox']").val());
                     state.push(objsArr[i].find("input[type='checkbox']").attr("data-state"));
                }
                for (var j = 0; j < state.length; j++) {
                    if (state[j] != '1') {
                        tableToolbar._showMsg("该记录已是禁用状态！");
                        return;
                    }
                }
                var data = this.DataBoxy();
                data.url += "/UserList.aspx?";
                data.url += $.param({
                    sl: this.Query.sl,
                    doType: "Disabled",
                    id: list.join(",")
                });
                this.GoAjax(data.url);
            },
            SetPer: function (Id) {
                var data = this.DataBoxy();
                data.url += '/UserPerSet.aspx?';
                data.title = '用户独立授权';
                data.url += $.param({
                    sl: this.Query.sl,
                    doType: "SetPer",
                    id: Id
                });
                this.ShowBoxy(data);
            },
            BindBtn: function () {
             //添加
                $(".add_gg").click(function () {
                    PageJsDataObj.Add();
                    return false;
                })
                //启用
                $(".toolbar_enable").click(function () {
                    var rows = tableToolbar.getSelectedCols();
                    PageJsDataObj.Enable(rows);
                    return true;
                });
                //禁用
                $(".toolbar_disabled").click(function () {
                    var rows = tableToolbar.getSelectedCols();
                    PageJsDataObj.Disabled(rows);
                    return false;
                });
                //独立授权
                $(".transactions").click(function () {
                    PageJsDataObj.SetPer($(this).attr("Data-rel"));
                    return false;
                });
                tableToolbar.init({
                    tableContainerSelector: "#liststyle", //表格选择器
                    objectName: "用户管理",
                    updateCallBack: function (objsArr) {
                        PageJsDataObj.Update(objsArr);
                        return false;
                    }
                });
            },
            PageInit: function () {
                //绑定功能按钮
                this.BindBtn();
                //当列表页面出现横向滚动条时使用以下方法 $("需要滚动最外层选择器").moveScroll();
                $('.tablelist-box').moveScroll();
            },
            CallBackFun: function (data) {
                newToobar.backFun(data);
            }

        }
        $(function () {
            PageJsDataObj.PageInit();
            PageJsDataObj.BindClose();
            return false;
        })
    </script>
</asp:Content>
