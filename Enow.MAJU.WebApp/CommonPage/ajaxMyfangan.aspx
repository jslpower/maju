<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ajaxMyfangan.aspx.cs" Inherits="Enow.MAJU.WebApp.CommonPage.ajaxMyfangan" %>

<asp:repeater id="rpt" runat="server">
                    <ItemTemplate>
                        <li><span class='floatR  <%#int.Parse(Eval("State").ToString())==(int)Enow.MAJU.Model.EnumType.方案状态.已预约?"font_r":(int.Parse(Eval("State").ToString())==(int)Enow.MAJU.Model.EnumType.方案状态.已发送?"font_green":(int.Parse(Eval("State").ToString())==(int)Enow.MAJU.Model.EnumType.方案状态.待发送?"font_blue":"font_blue")) %>'>
                            <%#(Enow.MAJU.Model.EnumType.方案状态)int.Parse(Eval("State").ToString())%></span><a
                                href="javascript:$.mobile.changePage('fanganDetail.aspx?id=<%#Eval("SolutionId") %>', { transition: 'slide' });"><em
                                    class="font_yellow"><%#Eval("TargetName")%></em>的<%#Eval("ProductName") %></a><a
                                        href="javascript:;" class="fxk" data-id="<%#Eval("SolutionId") %>" data-productid="<%#Eval("ProductId") %>"></a></li>
                    </ItemTemplate>
                </asp:repeater>
