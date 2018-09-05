<%@ Page Language="C#" MasterPageFile="~/Parent.master" AutoEventWireup="true" CodeFile="ReportSelection1.aspx.cs" Inherits="ReportSelection1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
<link href="Styles/newEntry.css" rel="Stylesheet" type="text/css" />
<script src="Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
<script src="Scripts/jquery-1.4.1.js" type="text/javascript"></script>
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
        INVENTOR WISE REPORT</td></tr>
    <tr><td runat="server" id="lblListItem">Inventor Name</td>
    <td> <asp:DropDownList ID="ddlListItem" runat="server">
    </asp:DropDownList>
    </td></tr>
    <tr id="lblListItem2" runat="server" visible="false"><td>Filed Status</td>
    <td> <asp:DropDownList ID="ddlListItem2" runat="server">
    </asp:DropDownList>
    </td></tr>    
    <tr align="center"><td colspan="2">
         <asp:ImageButton ID="imgBtnReport" runat="server" 
             ImageUrl="~/Images/Report2.png" onclick="imgBtnReport_Click"/>&nbsp;
         <asp:ImageButton ID="imgBtnClear" runat="server" 
             ImageUrl="~/Images/Clear2.png" onclick="imgBtnClear_Click" />
    </td></tr>   
</table>
</div>
</asp:Content>

