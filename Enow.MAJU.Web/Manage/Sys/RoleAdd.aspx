﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RoleAdd.aspx.cs" Inherits="Enow.MAJU.Web.Manage.Sys.RoleAdd" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta charset="utf-8" content="IE=EmulateIE8" http-equiv="X-UA-Compatible" />
<title>添加</title>
<link href="/Css/style.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="/Js/jquery-1.4.4.js"></script>
<link href="/css/boxynew.css" rel="stylesheet" type="text/css" />
<script src="/Js/jquery.blockUI.js" type="text/javascript"></script>
<script src="/Js/table-toolbar.js" type="text/javascript"></script>
<script language="JavaScript">
    function SelectPermissionClass(chkVal, idVal) {
        var frm = document.forms[0];

        for (i = 0; i < frm.length; i++) {

            if (frm.elements[i].id == ("PermissionID_" + idVal)) {
                if (chkVal == true && frm.elements[i].disabled == false) {
                    frm.elements[i].checked = true;
                }
                else {
                    frm.elements[i].checked = false;
                }
            }
        }
    }
    function checkAll(obj, childTB) {
        var TBid = "child" + childTB;
        $("#" + TBid + " input:checkbox").each(
			function () {
			    if (obj.checked) {
			        $(this).attr("checked", "checked");
			    }
			    else {
			        $(this).removeAttr("checked");
			    }
			}
			)

    }
		</script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="contentbox">
       <table width="100%" border="0" cellspacing="0" cellpadding="0">
          <tr>
            <th width="150" height="40" align="right">权限说明：</th>
            <td>
                <table align="center" border="0" cellpadding="0" cellspacing="0" width="99%">
                    <tr>
                        <td><br />
                            <ul>
                                <li>权限类别：指指栏目权限，不勾选权限的用户看不到该二级栏目。</li>
                            </ul>
                        </td>
                    </tr>
                </table>
              </td>
          </tr>
          <tr>
            <th width="150" height="40" align="right">角色名称：</th>
            <td>
                <asp:TextBox ID="txtRoleName" MaxLength="50" CssClass="input-txt formsize180" 
                    runat="server" errmsg="请填写角色名称!" valid="required"></asp:TextBox><span class="fontred">*</span><asp:RequiredFieldValidator 
                    ID="RequiredFieldValidator1" runat="server" ErrorMessage="请填写角色名称！" 
                    ControlToValidate="txtRoleName">*</asp:RequiredFieldValidator>
              </td>
          </tr>
          <tr>
            <th align="right" height="60">描述：</th>
            <td><asp:TextBox ID="txtRemark" CssClass="input-txt formsize180" runat="server"
                    Height="51px" TextMode="MultiLine" Width="90%"></asp:TextBox></td>
          </tr>
		  <tr>
            <th align="right" valign="top">角色权限：</th>
            <td><asp:Repeater id="dlOperatorPermission" runat="server" 
              onitemdatabound="dlOperatorPermission_ItemDataBound">
				<ItemTemplate>
					<table border="0" align="center" cellpadding="0" cellspacing="0" style="border-collapse:collapse">
						<tr>
							<td width="109" height="23" align="center" background="/Images/PerTopHeadLeft.gif">
							<span class="white"><strong><%# DataBinder.Eval(Container.DataItem,"MenuName") %></strong></span>
							</td>
							<td background="/Images/PerTopBack.gif" width="557"><input name="PID" type="checkbox" id="PID" onclick="SelectPermissionClass(this.checked,'<%#Eval("Id") %>')">&nbsp;全选</td>
							<td width="14"><img src="/Images/PerTopRight.gif" width="14" height="23" /></td>
						</tr>
						<tr>
							<td colspan="3"><table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
									<tr>
										<td width="7" background="/Images/PerLeftBack.gif"></td>
										<td align="center" valign="top"><asp:DataList id="dlPermissionClass" runat="server" HorizontalAlign="Center" RepeatDirection="Horizontal"
				Width="98%" RepeatColumns="3" BorderWidth="0px" CellPadding="0" CellSpacing="0" ItemStyle-VerticalAlign="Top">
				<ItemTemplate>
				<table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
              <td height="27" align="center"><span class="BlnFnt"><input type="checkbox" onclick="checkAll(this,<%#  DataBinder.Eval(Container.DataItem,"Id") %>)" /><%# DataBinder.Eval(Container.DataItem,"ClassName") %></span></td>
            </tr>
            <tr>
              <td valign="top" align="left"><%# ShowOP(Convert.ToString(DataBinder.Eval(Container.DataItem,"MenuId")),Convert.ToInt32(DataBinder.Eval(Container.DataItem,"Id"))) %></td>
            </tr>
          </table>          
				</ItemTemplate>
				</asp:DataList>
										</td>
										<td width="6" background="/Images/PerRightBack.gif"><img src="/Images/spacer.gif" width="1" height="1" /></td>
									</tr>
								</table>
							</td>
						</tr>
						<tr>
							<td colspan="3"><table width="100%" border="0" cellspacing="0" cellpadding="0">
									<tr>
										<td width="24" height="13"><img src="/Images/PerFootLeft.gif" width="24" height="13" /></td>
										<td background="/Images/PerFootBack.gif"><img src="/Images/spacer.gif" width="1" height="1" /></td>
										<td width="21"><img src="/Images/PerFootRight.gif" width="21" height="13" /></td>
									</tr>
								</table>
							</td>
						</tr>
					</table>
				</ItemTemplate>
			</asp:Repeater></td>
          </tr>
		  </table>
       <div class="Basic_btn fixed">
          <ul>
               <li>
                   <asp:LinkButton ID="linkBtnSave" runat="server" onclick="linkBtnSave_Click">保 存 >></asp:LinkButton>
               </li>
               <li><a href="javascript:void(0);" onclick="PageJsData.HideForm()" hidefocus="true">返 回 >></a></li>
          </ul>
          <div class="hr_10"></div>
        </div>
	  </div><asp:ValidationSummary 
              ID="ValidationSummary1" runat="server" ShowMessageBox="True" 
              ShowSummary="False" />
    </form>
    <script type="text/javascript">
        var PageJsData = {
            HideForm: function () {
                parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"] %>').hide();
            }
        }
    </script>
</body>
</html>
