<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ajaxInsuranceInterview.aspx.cs" Inherits="Enow.MAJU.WebApp.CommonPage.ajaxInsuranceInterview" %>

          <asp:Repeater ID="rpt" runat="server"><ItemTemplate>
            <li class="R_jiantou"><span class='floatR <%#int.Parse(Eval("Status").ToString())==(int)Enow.MAJU.Model.EnumType.面签状态.已预约?"font_r":(int.Parse(Eval("Status").ToString())==(int)Enow.MAJU.Model.EnumType.面签状态.待确定?"font_blue":(int.Parse(Eval("Status").ToString())==(int)Enow.MAJU.Model.EnumType.面签状态.已取消?"font_hui":"font_green")) %>'><%#(Enow.MAJU.Model.EnumType.面签状态)int.Parse(Eval("Status").ToString())%></span>预约编号：<em class="font_red" onClick="javascript:$.mobile.changePage('insuranceinterviewdetail.aspx?id=<%#Eval("InterviewId") %>', { transition: 'slide' });"><%#Eval("OrderNo") %></em></li>
          </ItemTemplate></asp:Repeater>
