<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyCollect.aspx.cs" Inherits="Enow.MAJU.WebApp.MyCollect" MasterPageFile="~/MasterPage/WebApp.Master" %>

<asp:Content ID="Content2" ContentPlaceHolderID="bd_hd" runat="server">
  <header>
    <a data-rel="back" data-direction="reverse"><i class="returnico"></i></a>
    <h1>我的收藏</h1>
  </header>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="bd_bd" runat="server">
      <div class="list_menu mt10">
          <ul>
              <li onclick="javascript:$.mobile.changePage('InsuranceInterview.aspx', { transition: 'slide' });">
                  <s class="ico_mianqian"></s>
                  <span class="menu_name">面签预约</span>
                  <%--<i class="weidu_btn radius"></i>--%>
              </li>

              <li onClick="javascript:$.mobile.changePage('myfangan.aspx', { transition: 'slide' });">
                  <s class="ico_fangan"></s>
                  <span class="menu_name">我的方案</span>
              </li>

              <li onclick="javascript:$.mobile.changePage('collect.aspx?t=<%=(int)Enow.MAJU.Model.EnumType.收藏类别.话题 %>', { transition: 'slide' });">
                  <s class="ico_huati"></s>
                  <span class="menu_name">话题收藏</span>
              </li>

              <li onclick="javascript:$.mobile.changePage('collect.aspx?t=<%=(int)Enow.MAJU.Model.EnumType.收藏类别.保险 %>', { transition: 'slide' });">
                  <s class="ico_cpsc"></s>
                  <span class="menu_name">产品收藏</span>
              </li>

          </ul>
      </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="bd_ft" runat="server">
  <script type="text/javascript">
      $(function () {
          var isLogin = "<%=IsLoginCheck() %>";
          if (isLogin == "False") {
              window.location.href = "/Default.aspx";
}
      });
  </script>
</asp:Content>

