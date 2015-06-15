<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/FinaWinBackPage.Master"
    AutoEventWireup="true" CodeBehind="ReplyList.aspx.cs" Inherits="Enow.MAJU.Web.Manage.Product.ReplyList" %>

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
                    &nbsp;&nbsp;产品名称关键字：
                    <asp:TextBox ID="txtKeyWord" runat="server" CssClass="input-txt formsize120"></asp:TextBox>
                     &nbsp;&nbsp;咨询内容关键字：
                    <asp:TextBox ID="txtContext" runat="server" CssClass="input-txt formsize120"></asp:TextBox>
                    &nbsp;&nbsp;发布/反馈日期：
                    <asp:TextBox ID="txtStartTime" runat="server" CssClass="input-txt formsize120" MaxLength="10"
                        onfocus="WdatePicker() "></asp:TextBox>-
                    <asp:TextBox ID="txtEndTime" runat="server" CssClass="input-txt formsize120" MaxLength="10"
                        onfocus="WdatePicker() "></asp:TextBox>
                    &nbsp;&nbsp;
                    <asp:Button ID="btnSearch" runat="server" Text="搜索" CausesValidation="false" 
                        CssClass="search-btn" onclick="btnSearch_Click" />
                </p>
            </span>
        </div>
        <div class="listbox">
            <div id="tablehead" class="tablehead">
                <ul class="fixed">
                </ul>
                <div class="pages">
                    <cc1:ExportPageInfo ID="ExportPageInfo1" runat="server" />
                </div>
            </div>
            <!--列表表格-->
            <div class="tablelist-box">
                <table width="100%" id="liststyle">
                    <tr>
                        <th align="center" >
                            编号
                        </th>
                        <th align="center" width="15%" >
                            产品名称
                        </th>
                        <th align="center" width="30%">
                            咨询/反馈内容
                        </th>
                        <th align="center">
                            用户昵称
                        </th>
                        <th align="center">
                            发布日期
                        </th>
                      
                        <th align="center">
                            消息类型
                        </th>
                        <th align="center">
                            操作
                        </th>
                    </tr>
                    <asp:Repeater ID="rptList" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td align="center" >
                                    <%#Container.ItemIndex+1%>
                                </td>
                                <td align="center">
                                 <a href="javascript:void(0)" class="transaction" style="color:blue;" data-rel='<%#Eval("ProductId") %>'>
                                    <%#Eval("ProductName")%>
                                    </a>
                                </td>
                                <td align="center">
                                    <%#Eval("Context") %>
                                </td>
                                <td align="center">
                                    <%#Enow.MAJU.BLL.BMember.GetUserName(Eval("MemberId").ToString())%>
                                </td>
                                <td align="center">
                                    <%#Eval("IssueTime", "{0:yyyy-MM-dd HH:mm}")%>
                                </td>
                                <td align="center">
                                 <%#Eval("IsSysReply").ToString() == "1" ? "系统回复" : "用户咨询"%>
                                </td>
                                <td align="center">
                                    <a href="javascript:void(0)" class="transactions" style="color:Blue;" data-rel='<%#Eval("ReplyId") %>'>
                                        <%#Eval("IsSysReply").ToString()=="1"?" ":"回复"%>
                                    </a>
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
                    url: '/Manage/Product',
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
            Add: function () {
                var data = this.DataBoxy();
                data.url += '/AddMessage.aspx?';
                data.title = '新增消息';
                data.url += $.param({
                    sl: this.Query.sl,
                    MId:<%=Request.QueryString["MId"] %>,
                    SMId:<%=Request.QueryString["SMId"] %>,
                    CId:<%=Request.QueryString["CId"] %>,
                    act:"add",
                    doType: "add"
                });
              window.location.href=data.url;
            },
            Update: function (objsArr) {
                var data = this.DataBoxy();
                data.url += '/NewsEdit.aspx?';
                data.title = '修改文章';
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
                }
                 
                if (list.length > 1) {
                    tableToolbar._showMsg("一次只能操作一条信息");
                    return;
                }
                if (window.confirm("确定删除此条记录？")) {

                    var data = this.DataBoxy();
                    data.url += '/Default.aspx?';

                    data.url += $.param({
                        sl: this.Query.sl,
                          MId:<%=Request.QueryString["MId"] %>,
                    SMId:<%=Request.QueryString["SMId"] %>,
                    CId:<%=Request.QueryString["CId"] %>,
                        doType: "delete",
                        id: objsArr[0].find('input[type="checkbox"]').val()
                    });

                }
                this.GoAjax(data.url);
            },
            
            Reply:function(Id){
            var data=this.DataBoxy();
            data.url +="/ProductReply.aspx?";
            data.title='回复列表';
            data.url+=$.param({
            sl:this.Query.sl,
             MId:<%=Request.QueryString["MId"] %>,
                    SMId:<%=Request.QueryString["SMId"] %>,
                    CId:<%=Request.QueryString["CId"] %>,
            doType:"reply",
            id:Id
            });
            window.location.href=data.url;
            },
             View: function (Id) {
                var data = this.DataBoxy();
                data.url += '/ProductView.aspx?';
                data.title = '产品预览';
                data.url += $.param({
                    sl: this.Query.sl,
                    doType: "view",
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
                //删除
                $(".toolbar_delete").click(function () {
                    var rows = tableToolbar.getSelectedCols();
                    PageJsDataObj.Delete(rows);
                    return false;
                });
               
                //消息回复
                $(".transactions").click(function () {
                    PageJsDataObj.Reply($(this).attr("Data-rel"));
                    return false;
                });
                     //查看产品
                $(".transaction").click(function () {
                    PageJsDataObj.View($(this).attr("Data-rel"));
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
