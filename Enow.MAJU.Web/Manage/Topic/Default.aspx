<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/FinaWinBackPage.Master"
    AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Enow.MAJU.Web.Manage.Topic.Default" %>

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
                    话题内容：
                    <input type="text" id="txtTitle" name="txtTitle" class="input-txt" value='<%=Enow.MAJU.Utility.Utils.GetQueryStringValue("context")%>' />
                    &nbsp;&nbsp;是否置顶：
                    <select id="ddlTop" name="ddlTop" class=" inputselect">
                        <option value="-1">请选择</option>
                        <%=Enow.MAJU.Utility.Function.UtilsCommons.GetEnumDDL
                                                   (Enow.MAJU.Utility.EnumObj.GetList(typeof(Enow.MAJU.Model.EnumType.是否置顶),
                                                               new string[] { }),"-1")
                        %>
                    </select>
                    &nbsp;&nbsp;是否热门：
                    <select id="ddlHot" name="ddlHot" class="inputselect">
                        <option value="-1">请选择</option>
                        <%=Enow.MAJU.Utility.Function.UtilsCommons.GetEnumDDL
                                                   (Enow.MAJU.Utility.EnumObj.GetList(typeof(Enow.MAJU.Model.EnumType.是否热门),
                                                               new string[] { }),"-1")
                        %>
                    </select>
                  
                    &nbsp;&nbsp;发布日期：
                    <input type="text" onfocus="WdatePicker()" name="txtStartTime" id="txtStartTime"
                        value='<%=Enow.MAJU.Utility.Utils.GetQueryStringValue("startDate")%>' size="10"
                        cssclass="inputtext" />-<input type="text" onfocus="WdatePicker({minDate:'#F{$dp.$D(\'txtStartTime\')}'})"
                            cssclass="inputtext" name="txtEndTime" id="txtEndTime" size="10" value='<%=Enow.MAJU.Utility.Utils.GetQueryStringValue("endDate")%>' />
                    <input type="submit" id="btnSearch" class="search-btn" causesvalidation="False" value="搜索" />
                </p>
            </span>
        </div>
        <div class="listbox">
            <div id="tablehead" class="tablehead">
                <ul class="fixed">
                    <li class="line"></li>
                    <li><a href="#" hidefocus="true" class="toolbar_update"><s class="updateicon"></s>编辑</a></li>
                    <li class="line"></li>
                    <li><a href="#" hidefocus="true" class="toolbar_delete"><s class="delicon"></s>删除</a></li>
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
                        <th width="3%" class="thinputbg">
                            <input type="checkbox" name="checkbox" id="checkbox" />
                        </th>
                        <th align="center" width="3%">
                            编号
                        </th>
                        <th align="center"  width="34%">
                            话题内容
                        </th>
                        <th align="center"  width="10%">
                            用户昵称
                        </th>
                        <th align="center"  width="5%">
                            是否置顶
                        </th>
                        <th align="center"  width="5%">
                            是否热门
                        </th>
                        
                        <th align="center"  width="5%">
                            回复数
                        </th>
                        <th align="center"  width="8%">
                            点击数
                        </th>
                        <th align="center"  width="7%">
                            收藏数
                        </th>
                        <th align="center"  width="8%">
                            状态
                        </th>
                        <th align="center"  width="7%">
                            发布日期
                        </th>
                    </tr>
                    <asp:Repeater ID="rptList" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td align="center">
                                    <input type="checkbox" name="checkbox" id="checkbox" value="<%#Eval("TopicId") %>" />
                                </td>
                                <td align="center">
                                    <%#Container.ItemIndex+1%>
                                </td>
                                <td align="left">
                                    <a href="javascript:void(0)" class="transactions" style="color: Blue;" data-rel='<%#Eval("TopicId") %>'>
                                        <%#Eval("Context")%>
                                    </a>
                                </td>
                                <td align="center">
                                    <%#Enow.MAJU.BLL.BMember.GetUserName(Eval("MemberId").ToString()) %>
                                </td>
                                <td align="center">
                                    <%#(Enow.MAJU.Model.EnumType.是否置顶)(Convert.ToInt32(Eval("isTop").ToString()))%>
                                </td>
                                <td align="center">
                                    <%#(Enow.MAJU.Model.EnumType.是否热门)(Convert.ToInt32(Eval("isHot").ToString()))%>
                                </td>
                                <td align="center">
                                    <%#Eval("Replys") %>
                                </td>
                                <td align="center">
                                    <%#Eval("Clicks")%>
                                </td>
                                <td align="center">
                                    <%#Eval("Collects")%>
                                </td>
                                <td align="center">
                                    <%#(Enow.MAJU.Model.EnumType.状态)(Convert.ToInt32(Eval("state").ToString())) %>
                                </td>
                                <td align="center">
                                    <%#Eval("IssueTime", "{0:yyyy-MM-dd}")%>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <asp:PlaceHolder ID="phNoData" runat="server" Visible="false">
                        <tr>
                            <td colspan="12" align="center">
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
                    url: '/Manage/Topic',
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
                data.url += '/TopicEdit.aspx?';
                data.title = '编辑话题';
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
                 
                if (list.length ==0) {
                    tableToolbar._showMsg("未选中任何要删除的数据！");
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
                        id:list.join(",")
                    });

                }
                this.GoAjax(data.url);
            },

            Search: function () {
            
                var Context = $("#txtTitle").val();
                var ddlTop = $("#ddlTop").val();
                var ddlHot = $("#ddlHot").val();
              
                var startDate = $("#txtStartTime").val();
                var endDate=$("#txtEndTime").val();
                window.location.href = "Default.aspx?Context=" + Context + "&IsTop=" + ddlTop + "&IsHot=" + ddlHot + "&startDate="+startDate+"&endDate="+endDate+"&MId="+<%=Request.QueryString["MId"] %>+"&SMId="+<%=Request.QueryString["SMId"] %>+"&CId="+<%=Request.QueryString["CId"] %>+"";
            },
            View: function (Id) {
                var data = this.DataBoxy();
                data.url += '/TopicView.aspx?';
                data.title = '话题详情';
                data.url += $.param({
                    sl: this.Query.sl,
                    doType: "view",
                    id: Id
                });
                this.ShowBoxy(data);
            },
           
            BindBtn: function () {
              
                //删除
                $(".toolbar_delete").click(function () {
                    var rows = tableToolbar.getSelectedCols();
                    PageJsDataObj.Delete(rows);
                    return false;
                });
                //搜索
                $("#btnSearch").click(function () {
                    PageJsDataObj.Search();
                    return false;
                });
               
                //查看话题
                $(".transactions").click(function () {
                    PageJsDataObj.View($(this).attr("Data-rel"));
                    return false;
                });
                
                tableToolbar.init({
                    tableContainerSelector: "#liststyle", //表格选择器
                    objectName: "话题管理",
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
