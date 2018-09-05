<%@ Page Language="C#" MasterPageFile="~/Parent.master" AutoEventWireup="true" CodeFile="ActionTracker.aspx.cs" Inherits="ActionTracker" Title="Action Tracker" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
 <title></title>
<link href="Styles/newEntry.css" rel="Stylesheet" type="text/css" />
<script src="Scripts/jquery-1.9.1.js" type="text/javascript"></script>
<script src="Scripts/jquery-ui-1.10.3.custom.min.js" type="text/javascript"></script>
<link href="Styles/jquery-ui-1.10.3.custom.min.css" rel="stylesheet" type="text/css" />
<script type="text/javascript">
$(document).ready(function(){
    $("table.neTable tr:even").addClass("tdTable");
    $("table.neTable tr:odd").addClass("tdTable1");  
});
function Verify()
{
    var r= confirm('Are you sure; Do you want to insert this record?');
    if (r==true)
    {
        if (document.getElementById('<%=ddlCategory.ClientID%>').value== '')
        {
            alert('Category can not be empty');
            return false;
        }
        if (document.getElementById('<%=ddlSubCategory.ClientID%>').value== '')
        {
            alert('Sub Category can not be empty');
            return false;
        }
        if (document.getElementById('<%=ddlCategory.ClientID%>').value== 'Others' && (document.getElementById('<%=ddlSubCategory.ClientID%>').value== '' || document.getElementById('<%=txtSubCategory.ClientID%>').value== ''))
        {
            alert('Sub Category can not be empty');
            return false;
        }
        if (document.getElementById('<%=txtAction.ClientID%>').value == '')
        {
            alert('Action Description can not be empty');
            return false;
        }
        return true;
    }
    return false; 
} 
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    &nbsp;<table align="center" class="neTable" cellpadding="12px" width="500px"  cellspacing="10px">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <tr><td colspan="2" align="center" valign="middle" style="font-size:medium; text-decoration:underline;">
        ACTION TRACKER</td></tr>
         
    <tr><td>Category</td>
    <td> <asp:DropDownList ID="ddlCategory" runat="server" AutoPostBack="true"
            onselectedindexchanged="ddlCategory_SelectedIndexChanged">
    </asp:DropDownList> 
    </td></tr>    
    <tr><td>Sub Category<br /><br />
        <asp:Label ID="lblSub" Visible="false" runat="server" Text="Other Sub Category"></asp:Label></td>
    <td> <asp:DropDownList ID="ddlSubCategory" runat="server">
    </asp:DropDownList><br /><br />
        <asp:TextBox ID="txtSubCategory" Visible="false" runat="server"></asp:TextBox> 
    </td></tr>
    <tr><td>Action Description</td><td><asp:TextBox ID="txtAction" TextMode="MultiLine" Width="300px" runat="server"></asp:TextBox>
    </td></tr>
    <tr><td>Action Date</td><td>
        <asp:TextBox ID="txtActionDt" Enabled="false" runat="server"></asp:TextBox>       
    </td></tr>
    <tr><td>Action User</td><td>
        <asp:TextBox ID="txtActionUser" Enabled="false" runat="server"></asp:TextBox>
    </td></tr>
    <tr><td>Next Action Proposed</td><td>
        <asp:TextBox ID="txtNextAction" runat="server" TextMode="MultiLine" Width="300px" ></asp:TextBox></td></tr>            
    <tr><td>Follow-up Date</td><td>
        <asp:TextBox ID="txtFollowupDt" runat="server"></asp:TextBox>&nbsp;<asp:Image ID="imgFollowupDt"
            runat="server" ImageUrl="~/Images/RedCalendar.gif" /> 
        <asp:CalendarExtender ID="CalendarExtender2" runat="server" Animated="true" Format="dd/MM/yyyy" PopupButtonID="imgFollowupDt" TargetControlID="txtFollowupDt">
        </asp:CalendarExtender>
    </td></tr>
    <tr><td>Next Action Assign To</td><td>
        <asp:DropDownList ID="ddlAssignUser" runat="server"></asp:DropDownList>
    </td></tr>
    <tr><td>Action Complete</td><td>
        <asp:DropDownList ID="ddlComplete" runat="server"></asp:DropDownList>
    </td></tr>
    <tr><td>Comments</td><td>
        <asp:TextBox ID="txtComments" Width="300px" runat="server" TextMode="MultiLine" ></asp:TextBox></td></tr>            
     <tr align="center"><td colspan="2">
         <asp:ImageButton ID="imgBtnSubmit" runat="server" 
             ImageUrl="~/Images/Save2.png" OnClientClick="return Verify()" onclick="imgBtnSubmit_Click"/>&nbsp;
         <asp:ImageButton ID="imgBtnClear" runat="server" 
             ImageUrl="~/Images/Clear2.png" onclick="imgBtnClear_Click"/>
    </td></tr>         
    </table>
</asp:Content>

