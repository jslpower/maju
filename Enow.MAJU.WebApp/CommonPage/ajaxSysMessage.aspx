<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ajaxSysMessage.aspx.cs" Inherits="Enow.MAJU.WebApp.CommonPage.ajaxSysMessage" %>

            <asp:Repeater ID="rpt_1" runat="server" OnItemDataBound="rpt_1_ItemDataBound">
                <ItemTemplate>
                    <div class="time_txt">
                        <%#Convert.ToDateTime(Eval("IssueTime").ToString()).ToString("yyyy-MM-dd HH:mm:ss")%></div>
                    <div class="baoxian_q">
                        <div class="head_img radius">
                            <img src="images/xx_head1.png" /></div>
                        <div class="zixun_box radius4">
                            <i></i>
                            <%#Eval("Context")%></div>
                    </div>
                        <asp:Repeater ID="rpt" runat="server">
                            <ItemTemplate>
                                    <div class="time_txt">
                                        <%#Eval("IssueTime","{0:yyyy-MM-dd HH:mm:ss}") %></div>
                                    <div class="<%#Eval("IsSysReply").ToString()=="0"?"baoxian_a":"baoxian_q" %>">
                                        <div class="head_img radius">
                                            <img src="<%#string.IsNullOrEmpty(usermodel.HeadPhoto)?(Eval("IsSysReply").ToString()=="0"?"images/touxian72.png":"images/xx_head1.png"):usermodel.HeadPhoto %>" /></div>
                                        <div class="zixun_box radius4">
                                            <i></i>
                                            <%#Eval("Context") %></div>
                                    </div>
                            </ItemTemplate>
                        </asp:Repeater>
                </ItemTemplate>
            </asp:Repeater>
