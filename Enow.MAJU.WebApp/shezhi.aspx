<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="shezhi.aspx.cs" Inherits="Enow.MAJU.WebApp.shezhi" MasterPageFile="~/MasterPage/WebApp.Master" EnableEventValidation="false" %>

<asp:Content ID="Content2" ContentPlaceHolderID="bd_hd" runat="server">
    <script src="/js/jquery.cookie.js" type="text/javascript"></script>
    <script type="text/javascript" src="js/CitySelect.js"></script>
    <script src="/js/enow.core.js" type="text/javascript"></script>
    <%--<script src="/js/mobiscroll/mobiscroll.custom-2.14.4.min.js" type="text/javascript"></script>--%>
  <header>
    <a data-rel="back" data-direction="reverse"><i class="returnico"></i></a>
    <h1>设置</h1>
  </header>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="bd_bd" runat="server">
      <div class="list_item form_list mt10">
          <ul>
              <li>
                 <span class="label_name">消息推送</span>
        <select name="switch" id="switch" data-role="slider" runat="server">
          <option value="1">On</option>
          <option value="0">Off</option>
        </select>
              </li>
          </ul>
          
          <div class="xiaoxi_jieshao" style="display:none;">消息通知用来即时接受消息等内容通知、可以在手机的系统设置—通知功能中，找到“马驹”更改。</div>
          
      </div>
     

      <div class="list_item form_list mt10">
          <ul>
              <li class="R_jiantou" onclick="javascript:$.mobile.changePage('ziliao.aspx', { transition: 'slide',ajax:false });">
                 <span class="label_name">个人资料</span>
              </li>

              <li class="R_jiantou" onClick="javascript:$.mobile.changePage('kehufw.aspx', { transition: 'slide' });">
                 <span class="label_name">客户服务</span>
              </li>

              <li class="R_jiantou" onClick="javascript:$.mobile.changePage('about.aspx', { transition: 'slide' });">
                 <span class="label_name">关于马驹</span>
              </li>
          </ul>
      </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="bd_ft" runat="server">
     <div class="padd cent mt20 paddB"><input name="" type="button" class="btn_yellow radius4" value="退  出" data-role="none" id="logout" ></div>
     <script>
         var initshezhi = function () {
             $("#logout").on("tap", function () {
                 $.ajax({ type: "post", url: 'shezhi.aspx?dotype=save', data: $("#form1").serialize(), cache: false, dataType: "json", async: false,
                     success: function (response) {
                         if (response.result == "1") {
                             $.cookieHelper("<%=AuthorName %>", null);
                             // $.mobile.changePage("/", { transition: "slideup" });
                             window.location.href = "/Default.aspx";
                         } else {
                             ipage.ShowMsg(response.msg);
                         }
                     },
                     error: function () {
                         ipage.ShowMsg("请求异常");
                     }
                 });
             })
             $(document).off("pageinit", initshezhi);
         }
         $(document).on("pageinit", initshezhi);
     </script>
</asp:Content>
