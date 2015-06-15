<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MessageCenter.aspx.cs" Inherits="Enow.MAJU.WebApp.MessageCenter" MasterPageFile="~/MasterPage/WebApp.Master" %>

<asp:Content ID="Content2" ContentPlaceHolderID="bd_hd" runat="server">
    <link rel="stylesheet" href="css/xiaoxi.css" type="text/css" media="screen">
    <header>
    <a data-rel="back" data-direction="reverse"><i class="returnico"></i></a>
    <h1>消息中心</h1>
  </header>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="bd_bd" runat="server">
      <div class="list_menu mt10">
          <ul>
              <li onclick="javascript:$.mobile.changePage('huati.aspx', { transition: 'slide' });">
                  <s class="ico_huati"></s>
                  <span class="menu_name">我的话题回复</span>
                  <%if (!IsMemberTopicReplyAllRead) %>
                  <%{ %>
                  <i class="weidu_btn radius"></i>
                  <%} %>
              </li>
                 <li onclick="javascript:$.mobile.changePage('BaoXianConsult.aspx', { transition: 'slide' });">
                  <s class="ico_baoxian"></s>
                  <span class="menu_name">保险咨询</span>
                  <%if (!IsProductReplyAllRead) %>
                  <%{ %>
                  <i class="weidu_btn radius"></i>
                  <%} %>
              </li>
              <li onclick="javascript:$.mobile.changePage('SysMessage.aspx', { transition: 'slide' });">
                  <s class="ico_xitong"></s>
                  <span class="menu_name">系统消息</span>
                  <%if (!IsSysMessageAllRead) %>
                  <%{ %>
                  <i class="weidu_btn radius"></i>
                  <%} %>
              </li>

          </ul>
      </div>
        <script type="text/javascript">
      $(function () { 
      var isLogin="<%=IsLoginCheck() %>";
      if (isLogin=="False") {
    window.location.href="/Default.aspx";
}
      });
  </script>
</asp:Content>