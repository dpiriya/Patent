<%@ Page Language="C#" MasterPageFile="~/Parent.master" AutoEventWireup="true" CodeFile="ReportSelection2.aspx.cs" Inherits="ReportSelection2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">

<link href="Styles/newEntry.css" rel="Stylesheet" type="text/css" />
<script src="Scripts/jquery-1.4.1.js" type="text/javascript"></script>
<script src="Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
<script src="Scripts/jquery.maskedinput-1.2.2.min.js" type="text/javascript"></script>
<script type="text/javascript">
$(document).ready(function(){
 
  $("table.neTable tr:even").addClass("tdTable");
  $("table.neTable tr:odd").addClass("tdTable1");
  });
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
<div style="height:350px">
<table align="center" class="neTable" cellpadding="20px"  cellspacing="10px">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <tr><td runat="server" id="lblTitle" colspan="2" align="center" valign="middle" style="font-size:medium; text-decoration:underline;">
        FILING DATE WISE REPORT</td></tr>
    <tr><td id="lblInput1" runat="server">From Date (dd/MM/yyyy)</td><td> <asp:TextBox ID="txtFromDt" Width="90px" runat="server"></asp:TextBox>
        &nbsp;&nbsp;<asp:Image
            ID="imgFromDt" ImageUrl="~/Images/RedCalendar.gif" runat="server" /> 
        <asp:CalendarExtender ID="CalendarExtender1" PopupButtonID="imgFromDt" TargetControlID="txtFromDt" Format="dd/MM/yyyy" runat="server">
        </asp:CalendarExtender>
    </td></tr>
    <tr>
    <td id="lblInput2" runat="server">To Date (dd/MM/yyyy)</td><td> <asp:TextBox ID="txtToDt" Width="90px" runat="server"></asp:TextBox>
        &nbsp;&nbsp;<asp:Image
            ID="imgToDt" ImageUrl="~/Images/RedCalendar.gif" runat="server" /> 
        <asp:CalendarExtender ID="CalendarExtender2" PopupButtonID="imgToDt" TargetControlID="txtToDt" Format="dd/MM/yyyy" runat="server"></asp:CalendarExtender></td></tr>
    <tr id="Renewal" visible="false" runat="server">
    <td id="lblDrop" runat="server">Include Not Renewal</td><td><asp:DropDownList ID="ddlRenewal" runat="server"></asp:DropDownList></td></tr>
    <tr align="center"><td colspan="2">
         <asp:ImageButton ID="imgBtnReport" runat="server" 
             ImageUrl="~/Images/Report2.png" onclick="imgBtnReport_Click"/>&nbsp;
         <asp:ImageButton ID="imgBtnClear" runat="server" 
             ImageUrl="~/Images/Clear2.png" onclick="imgBtnClear_Click"/>&nbsp;
         <asp:ImageButton ID="imgBtnGraph" runat="server" Width="40px" Height="40px" 
             ImageUrl="~/Images/Graph.gif" Visible="false" onclick="imgBtnGraph_Click"/>
    </td></tr>   
</table>
</div>
</asp:Content>

