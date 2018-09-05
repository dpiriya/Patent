<%@ Page Language="C#" MasterPageFile="~/Parent.master" AutoEventWireup="true" CodeFile="GetPassword.aspx.cs" Inherits="GetPassword" Title="Reset Password" %>

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
    <table align="center" style="height:200px; width:400px; font-family:Times New Roman; font-size:medium;" border="0px" cellpadding="10px" cellspacing="0px">
    <tr><td colspan="2" align="center" valign="middle" style="font-size:medium; color:Red; text-decoration:underline;">
        Reset Password</td></tr>
    <tr><td>User Name</td>
    <td><asp:DropDownList ID="ddlUser" runat="server"></asp:DropDownList>
    </td></tr>
    <tr><td>Reset Password</td>
    <td><asp:TextBox ID="txtPassword" TextMode="Password" runat="server"></asp:TextBox></td></tr>
    <tr><td align="center" colspan="2"><asp:Button ID="btnReset" CssClass="btnStyle" runat="server" Text="Reset Password" 
            onclick="btnReset_Click" />&nbsp;&nbsp;
            <asp:Button ID="btnUnlock" CssClass="btnStyle" runat="server" 
            Text="Unlock User" onclick="btnUnlock_Click" 
             />&nbsp;&nbsp;
            <asp:Button ID="btnClear" runat="server" CssClass="btnStyle" Text="Clear" 
            onclick="btnClear_Click" />&nbsp;&nbsp;            
            <asp:Button ID="btnCancel" runat="server" CssClass="btnStyle" Text="Cancel" 
            onclick="btnCancel_Click" /></td></tr>
    </table>    
</asp:Content>

