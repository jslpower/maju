<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ajaxBaoXian.aspx.cs" Inherits="Enow.MAJU.WebApp.CommonPage.ajaxBaoXian" %>

         <asp:Repeater ID="rpt1" runat="server" OnItemDataBound="rpt1_OnItemDataBound"><ItemTemplate>
             <li>
                 <dl>
                     <dt><%#Eval("TypeName") %><%#Eval("IsTop").ToString()=="1"?"<i class='din'>顶</i>":"" %></dt>
                     <asp:Repeater ID="rpt2" runat="server"><ItemTemplate>
                     <dd><span class="Rtxt"><%#Eval("Company")%></span><a href="baoxiandetail.aspx?id=<%#Eval("ProductId") %>" data-transition="slide"><%#Eval("ProductName")%></a></dd>
                     </ItemTemplate></asp:Repeater>
                 </dl>
             </li>             
         </ItemTemplate></asp:Repeater>

