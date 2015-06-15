<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ajaxTopicList.aspx.cs"
    Inherits="Enow.MAJU.WebApp.CommonPage.ajaxTopicList" %>

<asp:Repeater ID="rptList" runat="server">
                    <ItemTemplate>
                        <li onclick="javascript:$.mobile.changePage('huatidetail.aspx?Id=<%#Eval("TopicId") %>', { transition: 'slide' });">
                            <div class="huati_biaoti">
                                <%#Eval("IsTop").ToString()=="1"?"<i class='din'>顶</i>":""%><%#Enow.MAJU.Utility.Utils.GetText2(Eval("Context").ToString(),14,true) %></div>
                            <div class="huati_qita">
                                <span class="huati_num">
                                    <%#Eval("Replys")%></span><%#Eval("IssueTime","{0:yyyy-MM-dd}")%>
                               <%#Enow.MAJU.BLL.BMember.GetUserName(Eval("MemberId").ToString())%></div>
                        </li>
                    </ItemTemplate>
                </asp:Repeater>
