<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="zhitongche.aspx.cs" Inherits="Enow.MAJU.WebApp.zhitongche" MasterPageFile="~/MasterPage/WebApp.Master" EnableEventValidation="false" %>

<asp:Content ID="Content2" ContentPlaceHolderID="bd_hd" runat="server">
    <script src="/js/mobiscroll/mobiscroll.core-2.5.2.js" type="text/javascript"></script>
	<script src="/js/mobiscroll/mobiscroll.core-2.5.2-zh.js" type="text/javascript"></script>

	<link href="/css/mobiscroll/mobiscroll.core-2.5.2.css" rel="stylesheet" type="text/css" />
	<link href="/css/mobiscroll/mobiscroll.animation-2.5.2.css" rel="stylesheet" type="text/css" />
	<script src="/js/mobiscroll/mobiscroll.datetime-2.5.1.js" type="text/javascript"></script>
	<script src="/js/mobiscroll/mobiscroll.datetime-2.5.1-zh.js" type="text/javascript"></script>

	<!-- S 可根据自己喜好引入样式风格文件 -->
	<script src="/js/mobiscroll/mobiscroll.android-ics-2.5.2.js" type="text/javascript"></script>
	<link href="/css/mobiscroll/mobiscroll.android-ics-2.5.2.css" rel="stylesheet" type="text/css" />
	<!-- E 可根据自己喜好引入样式风格文件 -->
  <header>
    <a data-rel="back" data-direction="reverse"><i class="returnico"></i></a>
    <h1>面签直通车</h1>
  </header>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="bd_bd" runat="server">
      <div class="list_item form_list mt10">
          <ul>
              <li class="R_jiantou">
                 <span class="label_name">姓名</span>
                 <input type="text" class="u-input" value="" data-role="none" placeholder="请输入姓名" id="txtInterviewTarget" name="txtInterviewTarget" runat="server" readonly>
              </li>

              <li class="R_jiantou">
                 <span class="label_name">方案</span>
                 <input type="text" class="u-input" value="" data-role="none" placeholder="请选择方案" id="txtFangAn" runat="server" readonly>
              </li>

              <li class="R_jiantou">
                 <span class="label_name">预约时间</span>
                 <input type="text" class="u-input" value="" data-role="none" placeholder="请选择预约时间" id="txtOrderTime" name="txtOrderTime" runat="server">
              </li>
              
              <li class="R_jiantou">
                 <span class="label_name">联系电话</span>
                 <input type="text" class="u-input" value="" data-role="none" placeholder="请输入联系电话" id="txtContactTel" name="txtContactTel" runat="server" readonly>
              </li>
              <%if (1 == 0) %>
              <%{ %>
              <li class="R_jiantou">
                 <span class="label_name">留言备注</span>
                 <input type="text" class="u-input" value="" data-role="none" placeholder="请留言" id="txtInterviewNote" name="txtInterviewNote" runat="server">
              </li>
              <%} %>
        </ul>
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
        <!--消息弹出框-->
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
     <div class="caozuo mt20 padd paddB">
        <a id="yuyue" href="javascript:;" class="btn_yellow radius4">立即预约</a>
     </div>
     面签注意事宜：<asp:Literal ID="litInterviewNote" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="bd_ft" runat="server">
<script type="text/javascript">
    ipop = {
        pop: $("#pop"),
        that: null
    };

            function ShowMsg(msg) {
    var id = $("#popMsg");
    id.find("span").text(msg);
    id.show();
}

    function initzhitongche(){
        $("#a_sure").on("tap", function () {
          $("#popMsg").hide();
        });
        $("#<%=txtInterviewTarget.ClientID %>,#<%=txtContactTel.ClientID %>").on("tap", function () {
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

			var currYear = (new Date()).getFullYear();	
        var now = new Date();
		var opt={};
		opt.date = {preset : 'date'};
		opt.datetime = { preset : 'datetime', minDate:  new Date(now.getFullYear(), now.getMonth(), now.getDate()), maxDate: new Date(now.getFullYear()+30, now.getMonth(), now.getDate()), stepMinute: 5  };
		//opt.datetime = {preset : 'datetime'};
		opt.time = {preset : 'time'};
		opt.default = {
			theme: 'android-ics light', //皮肤样式
		    display: 'modal', //显示方式 
		    mode: 'scroller', //日期选择模式
			lang:'zh'
//		    startYear:currYear , //开始年份
//		    endYear:currYear +30 //结束年份
		};

//		$("#appDate").val('').scroller('destroy').scroller($.extend(opt['date'], opt['default']));
		var optDateTime = $.extend(opt['datetime'], opt['default']);
//		var optTime = $.extend(opt['time'], opt['default']);
		$("#<%=txtOrderTime.ClientID %>").mobiscroll(optDateTime).datetime(optDateTime);
//		$("#appTime").mobiscroll(optTime).time(optTime);

        $('#yuyue').on('tap', function () {
            if ($.trim($('#<%=txtInterviewTarget.ClientID %>').val()).length == 0) {
                ShowMsg('请输入姓名');
                $('#<%=txtInterviewTarget.ClientID %>').focus();
                return false;
            }
            if ($.trim($('#<%=txtOrderTime.ClientID %>').val()).length == 0) {
                ShowMsg('请选择预约时间');
               
                return false;
            }
            if ($.trim($('#<%=txtContactTel.ClientID %>').val()).length == 0) {
                ShowMsg('请输入联系电话');
                $('#<%=txtContactTel.ClientID %>').focus();
                return false;
            }
            var isMobile = /^(?:13\d|15\d|18\d)\d{5}(\d{3}|\*{3})$/; //手机号码验证规则
            if (!isMobile.test($.trim($('#<%=txtContactTel.ClientID %>').val()))) {
                ShowMsg('请输入正确的联系电话');
                $('#<%=txtContactTel.ClientID %>').focus();
                return false;
            }
            $.ajax({ type: "post", url: 'zhitongche.aspx?dotype=save&id=<%=Request.QueryString["id"] %>', data: $("#form1").serialize(), cache: false, dataType: "json", async: false,
                success: function (response) {
                    if (response.result != "1") {
                        ShowMsg(response.msg);
                    } else {
                        $.mobile.changePage("myfangan.aspx", { transition: "slideup" });
                    }
                },
                error: function () {
                    ShowMsg("请求异常");
                }
            });
        });
        $(document).off("pageinit",initzhitongche);
    }

    $(document).on("pageinit", initzhitongche);
</script>
</asp:Content>