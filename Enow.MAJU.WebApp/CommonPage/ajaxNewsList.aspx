<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ajaxNewsList.aspx.cs" Inherits="Enow.MAJU.WebApp.CommonPage.ajaxNewsList" %>

<asp:repeater id="rpt_list" runat="server">
                    <ItemTemplate>
                        <li onclick="javascript:$.mobile.changePage('NewsDetail.aspx?Id=<%#Eval("NewsId") %>', { transition: 'slide' });">
                            <dl>
                                <dt>
                                    <%#Enow.MAJU.Utility.Utils.GetText2(Eval("Title").ToString(),14,true) %></dt>
                                <dd class="cont">
                                    <%#Eval("Context") %>
                                </dd>
                                <dd>
                                    <%#Eval("IssueTime","{0:yyyy-MM-dd HH:mm}")%></dd>
                            </dl>
                        </li>
                    </ItemTemplate>
                </asp:repeater>
