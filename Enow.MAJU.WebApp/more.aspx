<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="more.aspx.cs" Inherits="Enow.MAJU.WebApp.more" MasterPageFile="~/MasterPage/WebApp.Master" %>

<asp:Content ID="Content2" ContentPlaceHolderID="bd_hd" runat="server">
    <link rel="stylesheet" href="/css/home.css" type="text/css" media="screen">
  <header>
    <a data-rel="back" data-direction="reverse"><i class="returnico"></i></a>
    <h1>更多</h1>
        <a href="messagecenter.aspx" data-transition="slide"><b class="icon_email">
        <%if (!(new Enow.MAJU.WebApp.MessageCenter().IsMemberTopicReplyAllRead && new Enow.MAJU.WebApp.MessageCenter().IsSysMessageAllRead && new Enow.MAJU.WebApp.MessageCenter().IsProductReplyAllRead)) %>
        <%{ %>
        <em class="radius"></em>
        <%} %>
        </b></a>
  </header>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="bd_bd" runat="server">
      <div class="list_menu mt10">
          <ul>
              <li class="R_jiantou" onclick="javascript:$.mobile.changePage('messagecenter.aspx', { transition: 'slide' });">
                  <s class="ico_xiaoxi"></s>
                  <span class="menu_name">我的消息</span>
              </li>

              <li class="R_jiantou" onClick="javascript:$.mobile.changePage('mycollect.aspx', { transition: 'slide' });">
                  <s class="ico_fav"></s>
                  <span class="menu_name">我的收藏</span>
              </li>

              <li class="R_jiantou" onclick="javascript:$.mobile.changePage('huati.aspx', { transition: 'slide' });">
                  <s class="ico_huati"></s>
                  <span class="menu_name">我的话题</span>
              </li>

              <li class="R_jiantou" onclick="javascript:$.mobile.changePage('shezhi.aspx', { transition: 'slide' });"">
                  <s class="ico_shezhi"></s>
                  <span class="menu_name">设置</span>
              </li>

          </ul>
      </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="bd_ft" runat="server">
  <div class="bot_nav">
     <div class="bot_box">
         <ul class="clearfix">
             <li onclick="javascript:$.mobile.changePage('index.aspx', { transition: 'slideup' });">
                <div class="ico"></div>
                <p>首页</p>
             </li>

             <li class="on">
                <div class="ico"></div>
                <p>更多</p>
             </li>
         </ul>
     </div>
  </div>
    <script type="text/javascript">
      $(function () {
          var isLogin = "<%=IsLoginCheck() %>";
          if (isLogin == "False") {
              window.location.href = "/Default.aspx";
}
      });
  </script>
</asp:Content>


