<%@ Page Language="C#" MasterPageFile="~/Parent.master" AutoEventWireup="true" CodeFile="FindFiles.aspx.cs" Inherits="FindFiles" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
<link href="Styles/newEntry.css" rel="Stylesheet" type="text/css" />
<script src="Scripts/jquery-1.9.1.js" type="text/javascript"></script>
<script src="Scripts/jquery-ui-1.10.3.custom.min.js" type="text/javascript"></script>
<link href="Styles/jquery-ui-1.10.3.custom.min.css" rel="stylesheet" type="text/css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <div style="height:350px;">
    <table align="center"  style="border-style:none;" class="neTable" cellpadding="12px"  cellspacing="10px">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <tr><td id="tdTitle" runat="server" colspan="2" align="center" valign="middle" style="font-size:medium; text-decoration:underline;">
        Provisional To Complete</td></tr>
    <tr id="trDynamic4" runat="server"><td id="tdDynamic4" runat="server">Months Since Filing Date</td>
    <td> <asp:DropDownList ID="ddlMonth" Width="100px" runat="server">
    </asp:DropDownList> 
    </td></tr>
    <tr id="trDynamic1" visible="false" runat="server"><td id="tdDynamic1" runat="server">Patent Status</td>
    <td> <asp:DropDownList ID="ddlDynamic1"  runat="server">
    </asp:DropDownList> 
    </td></tr>
    <tr id="trDynamic2" visible="false" runat="server"><td id="tdDynamic2" runat="server">Commercialization Response</td>
    <td> <asp:DropDownList ID="ddlDynamic2" runat="server">
    </asp:DropDownList> 
    </td></tr>    
    <tr id="trDate" runat="server" visible="false">
        <td id="tdDate" runat="server">
            Position as on
        </td>
        <td>
            <asp:TextBox ID="txtDate" runat="server" Width="100px"></asp:TextBox>&nbsp;<asp:Image
                ID="imgDate" ImageUrl="~/Images/RedCalendar.gif" runat="server" />
            <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDate"
                PopupButtonID="imgDate" Format="dd/MM/yyyy">
            </asp:CalendarExtender>
            <label id="lblDateFormat" runat="server">&nbsp;&nbsp;(dd/MM/yyyy)</label>                
        </td>
    </tr>
    <tr id="trDynamic3" runat="server" visible="false">
        <td id="tdDynamic3" colspan="2" align="center" runat="server">
            No. of Months Since Indian Filing Date&nbsp;&nbsp;<asp:TextBox ID="txtDynamic3" runat="server" Width="50px" ValidationGroup="Check"></asp:TextBox><asp:RegularExpressionValidator ID="REVMonth1" ControlToValidate="txtDynamic3" runat="server" ErrorMessage="*" ValidationExpression="^\d+$" Display="Dynamic" ValidationGroup="Check"></asp:RegularExpressionValidator>&nbsp;&nbsp;    
            To&nbsp;&nbsp;<asp:TextBox ID="txtDynamic4" runat="server" Width="50px"></asp:TextBox><asp:RegularExpressionValidator ID="REVMonth2" ControlToValidate="txtDynamic4" runat="server" ErrorMessage="*" ValidationExpression="^\d+$" Display="Dynamic" ValidationGroup="Check"></asp:RegularExpressionValidator>                
        </td>
    </tr>
    <tr align="center"><td colspan="2">
         <asp:ImageButton ID="imgBtnSubmit" runat="server" 
             ImageUrl="~/Images/Report2.png" onclick="imgBtnSubmit_Click"/>&nbsp;
         <asp:ImageButton ID="imgBtnClear" runat="server" 
             ImageUrl="~/Images/Clear2.png" onclick="imgBtnClear_Click"/>
    </td></tr>  
    <tr>
    <td colspan="2">
    <asp:ValidationSummary id="VS1" runat="server" EnableClientScript="true" HeaderText="Please Enter Number" />
    </td>
    </tr>
    </table>
    </div>
</asp:Content>

