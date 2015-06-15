<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="fangan.aspx.cs" Inherits="Enow.MAJU.WebApp.fangan" MasterPageFile="~/MasterPage/WebApp.Master" EnableEventValidation="false" %>

<asp:Content ID="Content2" ContentPlaceHolderID="bd_hd" runat="server">
    <header>
    <a data-rel="back" data-direction="reverse"><i class="returnico"></i></a>
    <h1>我要做方案</h1>
    <a href="myfangan.aspx" data-transition="slideup" data-role="none"><b class="icon_fangan"></b></a>
  </header>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="bd_bd" runat="server">
      <div class="list_item form_list mt10">
          <ul>
         <li class="R_jiantou"><span class="label_name">方案名称</span> <span class="u-input">
                <asp:Literal ID="ltrFanganName" runat="server"></asp:Literal></span> </li>
              <li class="R_jiantou">
                 <span class="label_name">姓名</span>
                 <input id="txtTargetName" runat="server" type="text" class="u-input" data-role="none" placeholder="请填写姓名" readonly>
              </li>

              <li class="R_jiantou">
                 <span class="label_name">性别</span>
                 <select id="selGender" runat="server" class="u-input" data-role="none"></select>
              </li>

              <li class="R_jiantou">
                 <span class="label_name">年龄</span>
                 <select id="selAge" runat="server" class="u-input" data-role="none"></select>
              </li>
              
              <li class="R_jiantou">
                 <span class="label_name">家庭年收入</span>
                 <select id="selRevenue" runat="server" class="u-input" data-role="none"></select>
              </li>
              
              <li class="R_jiantou">
                 <span class="label_name">邮箱地址</span>
                 <input id="txtEmail" runat="server" type="text" class="u-input" data-role="none" placeholder="请填写邮箱" readonly>
              </li>
              
        </ul>
      </div>
     
     <div class="caozuo mt20 padd paddB">
        <a id="add" href="javascript:;" class="btn_red radius4" data-transition="slide" data-role="none">属于我的方案</a>
     </div>

    <!-----姓名输入--------->
    <div class="user-mask" data-role="none" style="display:none;" id="pop">

       <div class="user-mask-box radius4" style="margin-top:-95px;">
       
           <div class="font18 cent">请输入您的<span></span></div>
        
           <div class="cent"><input name="" type="text" class="input_bk radius4" data-role="none"></div>
       
           <div class="caozuo">
                <ul>
                  <li class="paddR10"><a href="" class="btn_gray radius4" data-role="none">取消</a></li>
                  <li style="padding-left:10px;"><a href="" class="btn_yellow radius4" data-role="none">确定</a></li>
                </ul>
           </div>
          
       </div>
    </div>

    <!-----提示信息--------->
    <div class="user-mask" data-role="none" style="display:none;" id="popupconfirm">

       <div class="user-mask-box radius4" style="margin-top:-76px;">
       
           <div class="font18 cent">方案稍后会发到您的邮箱<br>请注意查收</div>
        
           <div class="cent mt10"><a href="myfangan.aspx" class="btn_yellow radius4" data-transition="slideup" data-role="none">确定</a></div>
          
       </div>
    </div>
    <!--弹出窗口-->
     <div class="user-mask" data-role="none" style="display: none;" id="popMsg">
        <div class="user-mask-box radius4" style="margin-top: -95px;">
            <div class="font18 cent">
                <input type="hidden" id="hidStatus" value="0" />
                <span></span>
            </div>
            <div class="caozuo mt10">
                <ul>
                    <li style="width: 100%;"><a href="javascript:;" class="btn_yellow radius4" data-role="none" id="a_sure">确定</a></li>
                </ul>
            </div>
        </div>
    </div>
     <script>
         ipop = {
             pop: $("#pop"),
             that: null
         };
         function ShowMsg(msg) {
             var id = $("#popMsg");
             id.find("span").text(msg);
             id.show();
         }

         function initfangan() {
             $("#a_sure").on("tap", function () {
                 $("#popMsg").hide();
             });
             $("li input").on("tap", function () {
                 ipop.that = $(this);
                 ipop.pop.find("span").text(ipop.that.closest("li").find("span").text());
                 ipop.pop.find("input").val(ipop.that.val());
                 ipop.pop.show();
             });
             ipop.pop.find(".btn_gray").on("tap", function () {
                 ipop.pop.hide();
             });
             ipop.pop.find(".btn_yellow").on("tap", function () {
                 ipop.that.val(ipop.pop.find("input").val());
                 ipop.pop.hide();
             });
             $("#add").on("tap", function () {
                 if ($.trim($("#<%=txtTargetName.ClientID %>").val()) == "") {
                     ShowMsg("请填写姓名");
                     return false;
                 }
                 if ($.trim($("#<%=txtEmail.ClientID %>").val()) == "" || !$("#<%=txtEmail.ClientID %>").val().match(/^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$/)) {
                     ShowMsg("请填写邮箱");
                     return false;
                 }
                 $.ajax({ type: "post", url: 'fangan.aspx?dotype=save&ProductId=<%=Request.QueryString["ProductId"] %>', data: $("#form1").serialize(), cache: false, dataType: "json", async: false,
                     success: function (response) {
                         if (response.result == "1") {
                             $("#popupconfirm").show();
                         } else {
                             ShowMsg(response.msg);
                         }
                     },
                     error: function () {
                         ShowMsg("请求异常");
                     }
                 });
             });
             $(document).off("pageinit", initfangan);
         }
         $(document).on("pageinit", initfangan);
     </script>
</asp:Content>
