<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InsuranceInterviewDetail.aspx.cs" Inherits="Enow.MAJU.WebApp.InsuranceInterviewDetail" MasterPageFile="~/MasterPage/WebApp.Master" %>

<asp:Content ID="Content2" ContentPlaceHolderID="bd_hd" runat="server">
  <header>
    <a data-rel="back" data-direction="reverse"><i class="returnico"></i></a>
    <h1>预约详情</h1>
  </header>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="bd_bd" runat="server">
      <div class="list_item mianqian_list mt20">
          <ul>
            <li><span class="floatR font_r"><asp:Literal ID="ltStatus" runat="server"></asp:Literal></span>预约编号：<em class="font_red"><asp:Literal ID="ltOrderNo" runat="server"></asp:Literal></em></li>
          </ul>
      </div>
      
      <div class="list_item form_list mianqian_list mt10">
          <ul>
            <li>
                 <span class="label_name">姓名</span>
                 <span class="txt"><asp:Literal ID="ltInterviewTarget" runat="server"></asp:Literal></span>
            </li>

            <li>
                 <span class="label_name">方案</span>
                 <span class="txt"><asp:Literal ID="ltProductName" runat="server"></asp:Literal></span>
            </li>

            <li>
                 <span class="label_name">预约时间</span>
                 <span class="txt"><asp:Literal ID="ltOrderTime" runat="server"></asp:Literal></span>
            </li>

            <li>
                 <span class="label_name">预约地点</span>
                 <span class="txt"><asp:Literal ID="ltOrderPlace" runat="server"></asp:Literal></span>
            </li>

            <li>
                 <span class="label_name">客户电话</span>
                 <span class="txt"><asp:Literal ID="ltContactTel" runat="server"></asp:Literal></span>
            </li>

            <li>
                 <span class="label_name">客户经理</span>
                 <span class="txt"><asp:Literal ID="ltManager" runat="server"></asp:Literal></span>
            </li>

            <li>
                 <span class="label_name">经理电话</span>
                 <span class="txt"><asp:Literal ID="ltManagerMob" runat="server"></asp:Literal></span>
            </li>
          </ul>
          
      </div>

      <div class="mianqian_cont mt10">
            <div>面签须知：</div>
            <div><asp:Literal ID="ltInterviewNote" runat="server"></asp:Literal></div>
      </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="bd_ft" runat="server">
</asp:Content>
