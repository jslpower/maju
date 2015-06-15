<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/FinaWinBackPage.Master"
    AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Enow.MAJU.Web.Manage.Memeber.Default" %>

<%@ Register Assembly="Enow.MAJU.Utility" Namespace="Enow.MAJU.Utility.ExportPageSet"
    TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <link href="/css/boxynew.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/js/jquery.boxy.js"></script>
    <script type="text/javascript" src="/js/jquery.blockUI.js"></script>
    <script type="text/javascript" src="/Js/datepicker/WdatePicker.js"></script>
    <script type="text/javascript" src="/js/moveScroll.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">
    <div class="contentbox">
        <div class="searchbox fixed">
            <span class="searchT">
                <p>
                    审核状态：
                    <asp:DropDownList ID="ddlStatus" runat="server">
                    </asp:DropDownList>
                    &nbsp;&nbsp;用户名：
                    <asp:TextBox ID="txtMemberName" CssClass="formsize120 input-txt" runat="server" />
                    &nbsp;&nbsp;用户昵称：
                    <asp:TextBox ID="txtNickName" CssClass="formsize120 input-txt" runat="server"></asp:TextBox>
                    &nbsp;&nbsp;手机号：
                    <asp:TextBox ID="txtMobile" CssClass="formsize120 input-txt" runat="server" />
                </p>
                <p>
                    &nbsp;&nbsp;省份:
                    <select id="ddlProvince" name="ddlProvince">
                    </select>城市:
                    <select id="ddlCity" name="ddlCity">
                    </select>区县:
                    <select id="ddlArea" name="ddlArea">
                    </select>
                    &nbsp;&nbsp;注册时间：
                    <asp:TextBox ID="txtIBeginDate" onfocus="WdatePicker()" runat="server" CssClass="formsize80 input-txt"
                        MaxLength="10"></asp:TextBox>-<asp:TextBox ID="txtIEndDate" onfocus="WdatePicker()"
                            runat="server" CssClass="formsize80 input-txt" MaxLength="10"></asp:TextBox>
                    <asp:Button ID="btnSearch" runat="server" Text="搜索" CssClass="search-btn" CausesValidation="False"
                        OnClick="btnSearch_Click" />
                </p>
            </span>
        </div>
        <div class="listbox">
            <div id="tablehead" class="tablehead">
                <ul class="fixed">
                    <li><a href="#" hidefocus="true" class="toolbar_disabled"><s class="jinyicon"></s>锁定账号</a></li>
                    <li class="line"></li>
                    <li><a href="#" hidefocus="true" class="toolbar_enable"><s class="qiyicon"></s>恢复账号</a></li>
                    <li class="line"></li>
                    <li>
                        <asp:LinkButton ID="lbtnToExcel" CssClass="toolbar_ToExcel" runat="server" OnClick="btnExport_Click"><s class="addicon"></s>导出Excel</asp:LinkButton></li>
                    <li class="line"></li>
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
                        <th align="center">
                            地域
                        </th>
                        <th align="center">
                            用户昵称
                        </th>
                        <th align="center">
                            用户名
                        </th>
                        <th align="center">
                            性别
                        </th>
                        <th align="center">
                            年龄
                        </th>
                        <th align="center">
                            Email
                        </th>
                        <th align="center">
                            手机号码
                        </th>
                        <th align="center">
                            家庭年收入
                        </th>
                        <th align="center">
                            用户状态
                        </th>
                        <th align="center">
                            注册时间
                        </th>
                    </tr>
                    <asp:Repeater ID="rptList" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td align="center">
                                    <input type="checkbox" name="checkbox" id="checkbox" value="<%#Eval("MemberId") %>"
                                        data-state="<%#Eval("Status") %>" />
                                </td>
                                <td align="center">
                                    <%#Eval("ProvinceName")%>-<%#Eval("CityName")%>-<%#Eval("AreaName") %>
                                </td>
                                <td align="center">
                                    <%#Eval("NickName")%>
                                </td>
                                <td align="center">
                                    <%#Eval("MemberName")%>
                                </td>
                                <td align="center">
                                    <%#Eval("Gender")=="1"?"女":"男"%>
                                </td>
                                <td align="center">
                                    <%#Eval("Age")%>
                                </td>
                                <td align="center">
                                    <%#Eval("Email") %>
                                </td>
                                <td align="center">
                                    <%#Eval("Mobile")%>
                                </td>
                                <td align="center">
                                    <%#Eval("Revenue")%>
                                </td>
                                <td align="center">
                                    <%#(Enow.MAJU.Model.EnumType.用户状态)Convert.ToInt32(Eval("Status")) %>
                                </td>
                                <td align="center">
                                    <%#Eval("IssueTime","{0:yyyy-MM-dd}")%>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <asp:PlaceHolder ID="phNoData" runat="server" Visible="false">
                        <tr>
                            <td colspan="11" align="center">
                                暂无数据
                            </td>
                        </tr>
                    </asp:PlaceHolder>
                </table>
            </div>
            <!--列表结束-->
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
                    url: '/Manage/Memeber',
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
            Enable: function (objsArr) {
                var list = new Array();
                var state = new Array();
                for (var i = 0; i < objsArr.length; i++) {
                    list.push(objsArr[i].find("input[type='checkbox']").val());
                    state.push(objsArr[i].find("input[type='checkbox']").attr("data-state"));
                }
                //检查状态
                for (var j = 0; j < state.length; j++) {
                    if (state[j] != '<%=(int)Enow.MAJU.Model.EnumType.用户状态.已停用 %>') {
                        tableToolbar._showMsg("该记录已是正常状态！");
                        return;
                    }
                }
                var data = this.DataBoxy();
                data.url += "/Default.aspx?";
                data.url += $.param({
                    sl: this.Query.sl,
                    doType: "Enable",
                    id: list.join(",")
                });
                this.GoAjax(data.url);
            },
            Disabled: function (objsArr) {
                var list = new Array();
                var state = new Array();
                for (var i = 0; i < objsArr.length; i++) {
                    list.push(objsArr[i].find("input[type='checkbox']").val());
                    state.push(objsArr[i].find("input[type='checkbox']").attr("data-state"));
                }
                //检查状态
                for (var j = 0; j < state.length; j++) {
                    if (state[j] != '<%=(int)Enow.MAJU.Model.EnumType.用户状态.正常 %>') {
                        tableToolbar._showMsg("该条记录已是停用状态！");
                        return;
                    }
                }
                var data = this.DataBoxy();
                data.url += "/Default.aspx?";
                data.url += $.param({
                    sl: this.Query.sl,
                    doType: "Disabled",
                    id: list.join(",")
                });
                this.GoAjax(data.url);
            },


            BindBtn: function () {
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

                tableToolbar.init({
                    tableContainerSelector: "#liststyle", //表格选择器
                    objectName: "铁丝管理"
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

            pcToobar.init({

                pID: "#ddlProvince",
                cID: "#ddlCity",
                xID: "#ddlArea",
                comID: '',

                pSelect: '<%=PId %>',
                cSelect: '<%=CSId %>',
                xSelect: '<%=AId %>'
            });
            PageJsDataObj.PageInit();
            PageJsDataObj.BindClose();
            return false;
        })
    </script>
</asp:Content>
