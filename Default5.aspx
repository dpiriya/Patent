<%@ Page Title="" Language="C#" MasterPageFile="~/Parent.master" AutoEventWireup="true" CodeFile="Default5.aspx.cs" Inherits="Default5" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
<link href="Styles/newEntry.css" rel="Stylesheet" type="text/css" />
<script src="Scripts/jquery-1.9.1.js" type="text/javascript"></script>
<script src="Scripts/jquery-ui-1.10.3.custom.min.js" type="text/javascript"></script>
<link href="Styles/jquery-ui-1.10.3.custom.min.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
<table align="center" class="neTable"  cellpadding="12px" width="970px" cellspacing="10px">
        <asp:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server">
        </asp:ToolkitScriptManager>
        <tr><td> Select IDF <asp:DropDownList ID="ddlIDFNo" runat="server"></asp:DropDownList></td></tr>
        <tr>
        <td align="center"><asp:GridView ID="Grid1" runat="server" AutoGenerateColumns="false" Width="400px">
    <Columns>
    <asp:TemplateField HeaderText="Select">
    <ItemTemplate><asp:CheckBox ID="cbSelect" runat="server" /></ItemTemplate></asp:TemplateField>
<asp:BoundField DataField="ID" HeaderText="ID" />
<asp:BoundField DataField="Details" HeaderText="Details" />
    
    </Columns></asp:GridView> </td></tr>
    </table>
</asp:Content>

