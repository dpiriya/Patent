<%@ Page Title="" Language="C#" MasterPageFile="~/Parent.master" AutoEventWireup="true" CodeFile="RenewalActionPlanReport.aspx.cs" Inherits="Default2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>



<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
    <title></title>
<link href="Styles/TableStyle2.css" rel="Stylesheet" type="text/css" />
<script src="Scripts/jquery-1.9.1.js" type="text/javascript"></script>
<script src="Scripts/jquery-ui-1.10.3.custom.min.js" type="text/javascript"></script>
<link href="Styles/jquery-ui-1.10.3.custom.min.css" rel="stylesheet" type="text/css" />
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <table align="center" class="neTable" cellpadding="12px" width="970px"  cellspacing="10px">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <tr><td class="style5">
        P&M ACTION PLAN</td></tr>
    <tr><td>IDF / IDF SUB NO<asp:DropDownList ID="ddlIDFNo" runat="server" 
           AutoPostBack="true" 
            EnableViewState="true" Height="30px" Width="166px" 
            style="margin-left: 21px; margin-top: 0px;" 
            onselectedindexchanged="ddlIDFNo_SelectedIndexChanged"></asp:DropDownList><asp:ImageButton ID="ImageReportButn1" runat="server" 
             ImageUrl="~/Images/Report2.png" onclick="ImageReportButn1_Click"/></td>
    </tr>

</table>
</asp:Content>