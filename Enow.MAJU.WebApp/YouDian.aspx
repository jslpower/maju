<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="YouDian.aspx.cs" Inherits="Enow.MAJU.WebApp.YouDian" MasterPageFile="~/MasterPage/WebApp.Master" %>

<asp:Content ID="Content2" ContentPlaceHolderID="bd_hd" runat="server">
    <link rel="stylesheet" href="css/huati.css" type="text/css" media="screen">
    <link rel="stylesheet" href="/css/home.css" type="text/css" media="screen">
  <header>
    <a data-rel="back" data-direction="reverse"><i class="returnico"></i></a>
    <h1><asp:Literal ID="litType" runat="server"></asp:Literal></h1>
    <%--<a href="index.aspx" data-role="none" data-transition="slideup"><b class="icon_starR"></b></a>--%>
      <a href="" id="reply1"><b class="icon_jia"></b></a>
</header>
      <div class="head_div" style="display: block;">
        <div class="head_box" style="display: none;">
            <ul>
                <li id="collect" style="display:none;">收藏</li>
                <li id="share">分享</li>
            </ul>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="bd_bd" runat="server">
     <div class="baoxian_list mt10">
         <ul>
             <li style="border:none 0;">
                 <dl>
                     <dt class="cent bot_line" id="title"><asp:Literal ID="litProductName" runat="server"></asp:Literal></dt>
                     <dd class="mt10"><span class="Rtxt">收藏人数：<em class="font_yellow"><asp:Literal ID="litCollects" runat="server"></asp:Literal></em>人</span>出品公司：<asp:Literal ID="litCompany" runat="server"></asp:Literal></dd>
                     <dd>适合人群：<em class="font_yellow"><asp:Literal ID="litTarget" runat="server"></asp:Literal></em></dd>
                 </dl>
             </li>
         </ul>
     </div>
     
      <div class="cont_box">
      
            <div id="context"><asp:Literal ID="litContext" runat="server"></asp:Literal></div>
          
      </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="bd_ft" runat="server">
<script type="text/javascript">
    function inityoudian() {
        //点+
        $("#reply1").on("tap", function () {
            if ($(".head_box").is(":visible")) {
                $(".head_box").hide();
            }
            else {
                $(".head_box").show();
            }
        });
        $("#share").on("tap",function(){
            $("#fenxiang").show();
        });

        //分享内容
        ipage.shareoption.webpageUrl = $.mobile.path.makeUrlAbsolute("youdian.aspx?id=<%=Request.QueryString["id"] %>");
        ipage.shareoption.title = $("#title").html();
        //ipage.shareoption.description = $("#context").html();

        $(document).off("pageinit", inityoudian);
    }
    $(document).on("pageinit", inityoudian);
</script>
</asp:Content>
