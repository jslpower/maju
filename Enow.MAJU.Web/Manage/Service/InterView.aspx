<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/FinaWinBackPage.Master"
    AutoEventWireup="true" CodeBehind="InterView.aspx.cs" Inherits="Enow.MAJU.Web.Manage.Service.InterView" %>

<%@ Register Assembly="Enow.MAJU.Utility" Namespace="Enow.MAJU.Utility.ExportPageSet"
    TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <link href="/css/boxynew.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/js/jquery.boxy.js"></script>
    <script type="text/javascript" src="/js/jquery.blockUI.js"></script>
    <script type="text/javascript" src="/js/moveScroll.js"></script>
    <script type="text/javascript" src="/Js/datepicker/WdatePicker.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">
    <div class="contentbox">
        <div class="searchbox fixed">
            <span class="searchT">
                <p>
                    面签关键字查询：
                    <asp:TextBox ID="txtKeyWords" runat="server" CssClass="input-txt formsize120"></asp:TextBox>
                    &nbsp;&nbsp;预约日期：
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
                    <li><a href="#" hidefocus="true" class="toolbar_update"><s class="updateicon"></s>编辑</a></li>
                    <li class="line"></li>
                    <li style="display: none;"><s class="delicon"></s><a href="#" hidefocus="true" class="toolbar_delete">
                        删除</a></li>
                    <li style="display: none;" class="line"></li>
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
                            预约编号
                        </th>
                        <th align="center">
                            方案名称
                        </th>
                        <th align="center">
                            客户姓名
                        </th>
                        <th align="center">
                            联系电话
                        </th>
                        <th align="center">
                            预约时间
                        </th>
                        <th align="center">
                            发布日期
                        </th>
                    </tr>
                    <asp:Repeater ID="rptList" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td align="center">
                                    <input type="checkbox" name="checkbox" id="checkbox" value="<%#Eval("InterviewId") %>"
                                        data-state="<%#Eval("status") %>" />
                                </td>
                                <td align="center">
                                    <%#Eval("OrderNo")%>
                                </td>
                                <td align="left">
                                    <%#Eval("MemberName")%>"的"<%#Eval("ProductName")%>
                                </td>
                                <td align="center">
                                    <%#Eval("InterviewTarget")%>
                                </td>
                                <td align="center">
                                    <%#Eval("ContactTel")%>
                                </td>
                                <td align="center">
                                    <%#Eval("OrderTime","{0:yyyy-MM-dd HH:mm}")%>
                                </td>
                                <td align="center">
                                    <%#Eval("IssueTime", "{0:yyyy-MM-dd}")%>
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
                    url: '/Manage/Service',
                    title: "",
                    width: "830px",
                    height: "620px"
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
           
            Update: function (objsArr) {
                var data = this.DataBoxy();
                data.url += '/InterViewEdit.aspx?';
                data.title = '编辑面签';
                data.url += $.param({
                    sl: this.Query.sl,
                   MId:<%=Request.QueryString["MId"] %>,
                    SMId:<%=Request.QueryString["SMId"] %>,
                    CId:<%=Request.QueryString["CId"] %>,
                    act:"update",
                    doType: "update",
                    id: objsArr[0].find('input[type="checkbox"]').val()
                });
               window.location.href=data.url;
            },
            Delete: function (objsArr) {
                var list = new Array();
                var state=new Array();
                for (var i = 0; i < objsArr.length; i++) {
                    list.push(objsArr[i].find("input[type='checkbox']").val());
                      state.push(objsArr[i].find("input[type='checkbox']").attr("data-state"));
                }
                 //检查状态
                for (var j = 0; j < state.length; j++) {
                    if (state[j] == '3') {
                        tableToolbar._showMsg("第" + (j + 1) + "条记录状态为已面签，不可删除！");
                        return;
                    }
                }
                 
                 if (list.length==0) {
                    tableToolbar._showMsg("未选中任何项");
                    return;
                }
                if (window.confirm("确定删除此条记录？")) {

                    var data = this.DataBoxy();
                    data.url += '/InterView.aspx?';

                    data.url += $.param({
                        sl: this.Query.sl,
                          MId:<%=Request.QueryString["MId"] %>,
                    SMId:<%=Request.QueryString["SMId"] %>,
                    CId:<%=Request.QueryString["CId"] %>,
                        doType: "delete",
                        id:list.join(",")
                    });

                }
                this.GoAjax(data.url);
            },   
           
            BindBtn: function () {
                //删除
                $(".toolbar_delete").click(function () {
                    var rows = tableToolbar.getSelectedCols();
                    PageJsDataObj.Delete(rows);
                    return false;
                });
                
               
               
                tableToolbar.init({
                    tableContainerSelector: "#liststyle", //表格选择器
                    objectName: "面签管理",
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
