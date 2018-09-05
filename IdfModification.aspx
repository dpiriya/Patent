<%@ Page Language="C#" MasterPageFile="~/Parent.master" AutoEventWireup="true" CodeFile="IdfModification.aspx.cs" Inherits="IdfModification" Title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
<table align="center" class="neTable"  cellpadding="12px" width="970px" cellspacing="10px">
<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
</asp:ToolkitScriptManager>
<tr><td colspan="4" align="center" valign="middle" style="font-size:medium; text-decoration:underline;">
    PATENT FILE MODIFICATION</td></tr>
<tr> 
    <td>File Number</td>
    <td> <asp:TextBox ID="txtFileNo" Width="80px" runat="server"></asp:TextBox>
        <asp:Button ID="btnFind" runat="server" Text="Find" BorderColor="Brown" 
            BorderStyle="Outset" BackColor="SandyBrown" onclick="btnFind_Click"/>
    </td>
  </tr>
   
  </table>

</asp:Content>

