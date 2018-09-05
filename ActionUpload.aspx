<%@ Page Language="C#" MasterPageFile="~/Parent.master" AutoEventWireup="true" CodeFile="ActionUpload.aspx.cs" Inherits="ActionUpload" Title="Tracker File Upload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
<style type="text/css">
.btnStyle
{
    font-family:Arial;
    font-size:12px;
    font-weight:700;
    background-color:Maroon;
    color:Silver; 	
}
</style>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <table align="center" style="width:850px; font-family:Times New Roman; font-size:medium;" border="0px" cellpadding="10px" cellspacing="0px">
    <tr><td colspan="2" align="center" valign="middle" style="font-size:medium; color:Red; text-decoration:underline;">
        Action Tracker Excel File Upload</td></tr>
    <tr ><td align="center" colspan="2">File Name &nbsp;&nbsp;&nbsp;&nbsp;<asp:FileUpload ID="FileUpload1" EnableViewState="true" runat="server" />
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="FileUpload1" ValidationGroup="upload" ErrorMessage="* Required">
    </asp:RequiredFieldValidator></td></tr>
    <tr><td align="center" colspan="2">
        <asp:Button ID="btnUpload" CssClass="btnStyle" 
            runat="server" Text="Upload" ValidationGroup="upload" onclick="btnUpload_Click" />
    </td></tr>
    <tr><td align="center" colspan="2">
    <asp:GridView ID="GridView1" RowStyle-Wrap="true" Width="800px" runat="server">
    </asp:GridView>
    </td></tr>
    <tr><td align="center" colspan="2">
        <asp:Button ID="btnInsert" CssClass="btnStyle" 
            runat="server" Text="Insert" Visible="false" onclick="btnInsert_Click"/>
    </td></tr>    
    </table>    
</asp:Content>

