<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Parent.master" CodeFile="Rpt_SR.aspx.cs" Inherits="SR_Report" %>
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
    <style type="text/css">
        .auto-style1 {
            height: 83px;
        }
    </style>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <div style="height:350px">
<table align="center" class="neTable" cellpadding="20px"  cellspacing="10px">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <tr><td runat="server" id="lblTitle" colspan="2" align="center" valign="middle" style="font-size:medium; text-decoration:underline;">Service Request Report</td></tr>
    <tr><td runat="server" id="lblSRNo">Service Request No</td>
    <td> <asp:DropDownList ID="ddlSRNo" runat="server">
    </asp:DropDownList>
    </td></tr>
    <tr align="center"><td colspan="2">
         <asp:ImageButton ID="imgBtnSubmit" runat="server" 
             ImageUrl="~/Images/Report2.png" onclick="imgBtnSubmit_Click"/>&nbsp;
         <asp:ImageButton ID="imgBtnClear" runat="server" 
             ImageUrl="~/Images/Clear2.png" onclick="imgBtnClear_Click"/>
    </td></tr>   
</table>
</div>
</asp:Content>
