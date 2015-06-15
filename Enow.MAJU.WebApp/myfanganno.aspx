<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="myfanganno.aspx.cs" Inherits="Enow.MAJU.WebApp.myfanganno" MasterPageFile="~/MasterPage/WebApp.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bd_hd" runat="server">
  <header>
    <a data-rel="back" data-direction="reverse"><i class="returnico"></i></a>
    <h1>我的方案</h1>
  </header>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="bd_bd" runat="server">
      <div class="fangan_no mt10">
         
            <h3 class="cent">您还没配置方案</h3>
            
            <div class="caozuo mt20 paddB">
                <ul>
                  <li><a href="baoxian.aspx" data-role="none" data-transition="slide" class="btn_red radius4">立即配置</a></li>
                  <li><a href="baoxian.aspx" data-role="none" data-transition="slide" class="btn_yellow radius4">我要咨询</a></li>
                </ul>
            </div>
            
      </div>


      <div class="list_item mt10">
          <ul>
              <li class="R_jiantou" onclick="javascript:$.mobile.changePage('baoxian.aspx',{transition:'slide'});">去看看适合我的产品</li>
 
              <li class="R_jiantou" onclick="javascript:$.mobile.changePage('huatilist.aspx',{transition:'slide'});">您感兴趣的</li>

          </ul>
      </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="bd_ft" runat="server">
</asp:Content>