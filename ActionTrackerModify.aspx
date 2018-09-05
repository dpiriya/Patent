<%@ Page Language="C#" MasterPageFile="~/Parent.master" AutoEventWireup="true" CodeFile="ActionTrackerModify.aspx.cs" Inherits="ActionTrackerModify" Title="Action Tracker Modification" %>
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
    var r= confirm('Are you sure; Do you want to modify this record?');
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
function DeleteVerify()
{
   var r= confirm('Are you sure; Do you want to delete this record?');
   if (r==true)
   {
        return true;
   }
   else
   {
        return false;
   }
} 
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
<table align="center" cellpadding="12px" cellspacing="10px" style="font-family:Arial; font-size:12px; font-weight:bold; " >
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
    <tr><td colspan="3" align="center" valign="middle" style=" font-family:Arial;font-size:16px;font-weight:bold;width:95%;color:Maroon; text-decoration:underline;">
        Action Tracker Modification</td><td style="width:5%;">
                                <asp:ImageButton ID="ibtnPrinter" 
                                    ImageUrl="~/Images/Printer.gif" runat="server" OnClientClick="return Verify()" 
                                    style="height: 24px" onclick="ibtnPrinter_Click"/></td></tr>
    <tr align="center"><td style="width:350px;" align="right"><strong>List of Search 
        Items</strong><br /><asp:DropDownList ID="ddlSearch" runat="server"></asp:DropDownList></td>
        <td style="width:250px;"><strong>Search Text</strong><br /><asp:TextBox ID="txtSearch" Width="250px" runat="server"></asp:TextBox></td>
        <td style="width:250px;" align="left"><br />
            <asp:ImageButton ID="ibtnSearch" ImageUrl="~/Images/search32.jpg"  
                runat="server" onclick="ibtnSearch_Click" /></td><td></td> 
        </tr>
     </table>
<table width="980"><tr><td style="vertical-align:top;">
<table width="550">
     <tr><td align="left" style=" font-family:Arial;font-size:14px;font-weight:bold; text-decoration:underline;">
        <i id="SearchResult" visible="false" runat="server">Search Results</i></td></tr>
     <tr><td >
     <asp:GridView ID="gvTracker" AutoGenerateColumns="false" Width="550px"  
             CellPadding="3" CellSpacing="0" DataKeyNames="EntryDt" 
             Font-Names="Arial" BorderColor="#006633"  runat="server" 
             AllowSorting="true" AllowPaging="true" 
             onpageindexchanging="gvTracker_PageIndexChanging" 
             onrowdeleting="gvTracker_RowDeleting" 
             onselectedindexchanging="gvTracker_SelectedIndexChanging" 
             onsorting="gvTracker_Sorting">
     <Columns>
     <asp:BoundField DataField="EntryDt" HeaderStyle-Font-Underline="true" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Entry Dt." SortExpression="EntryDt"  />
     <asp:BoundField ItemStyle-HorizontalAlign="Left" DataField="SlNo" HeaderText="Serial No." />
     <asp:BoundField ItemStyle-Width="150" ItemStyle-Wrap="true" ItemStyle-HorizontalAlign="Left" DataField="NextAction" HeaderText="Next Action" />
     <asp:BoundField HeaderStyle-Font-Underline="true" ItemStyle-HorizontalAlign="Left" DataFormatString="{0:dd/MM/yyyy}" DataField="FollowupDt" SortExpression="FollowupDt" HeaderText="Followup Date" />
     <asp:BoundField HeaderStyle-Font-Underline="true" ItemStyle-HorizontalAlign="Left" DataField="AssignUser" SortExpression="AssignUser" HeaderText="Assignee" />
     <asp:TemplateField ItemStyle-HorizontalAlign="Center" >
     <ItemTemplate>
     <asp:LinkButton ID="lbtnEdit" Font-Underline="true" CommandName="Select" runat="server">Edit</asp:LinkButton>
     <asp:LinkButton ID="lbtnDelete" OnClientClick="return DeleteVerify()" Font-Underline="true" CommandName="Delete" runat="server">Delete</asp:LinkButton>
     </ItemTemplate>
     </asp:TemplateField>
     </Columns>
     <EmptyDataTemplate>
     <p style="text-align:center; background-color:#C8C8C8">
     <asp:Image ID="Image1" BorderStyle="None" BackColor="#C8C8C8" ImageUrl="~/Images/NoData.jpg" runat="server" />
     </p>
     </EmptyDataTemplate>
     <HeaderStyle HorizontalAlign="Center" ForeColor="#dffaa4" BackColor="#006633" BorderColor="#006633" BorderStyle="Solid" BorderWidth="2" VerticalAlign="Middle" Font-Size="13px" Height="35px" />
     <RowStyle BackColor="White" Height="25px" Font-Size="12px" Wrap="true"/>
     <AlternatingRowStyle Wrap="true" BackColor="#FFFFCC" />
     <PagerSettings Mode="Numeric" PageButtonCount="9" Position="TopAndBottom"/>
     <PagerStyle Font-Underline="true" Font-Bold="true" Font-Italic="true" ForeColor="Red"/>
     </asp:GridView> 
     <i id="pageNumber" visible="false" style="font-weight:bold" runat="server"> You are 
         viewing page <%=gvTracker.PageIndex+1%> of <%= gvTracker.PageCount%></i> 
     </td></tr></table>
</td>
<td>
<table style="text-align:left" class="neTable" cellpadding="12px" width="430px"  cellspacing="10px">
    <tr><td>Entry Date</td>
    <td><asp:Label ID="lblEntryDt" runat="server"></asp:Label></td></tr>    
    <tr><td>Serial Number</td>
    <td><asp:Label ID="lblSlNo" runat="server"></asp:Label></td></tr>    
    <tr><td>Category</td>
    <td> <asp:DropDownList ID="ddlCategory" runat="server" AutoPostBack="true" 
            onselectedindexchanged="ddlCategory_SelectedIndexChanged">
    </asp:DropDownList> 
    </td></tr>    
    <tr><td>Sub Category<br /><br />
        <asp:Label ID="lblSub" Visible="false" runat="server" Text="Other Sub Category"></asp:Label></td>
    <td> <asp:DropDownList ID="ddlSubCategory" AutoPostBack="true" runat="server">
    </asp:DropDownList><br /><br />
        <asp:TextBox ID="txtSubCategory" Visible="false" runat="server"></asp:TextBox> 
    </td></tr>
    <tr><td>Action Description</td><td><asp:TextBox ID="txtAction" TextMode="MultiLine" Width="250px" runat="server"></asp:TextBox>
    </td></tr>
    <tr><td>Next Action Proposed</td><td>
        <asp:TextBox ID="txtNextAction" runat="server" TextMode="MultiLine" Width="250px" ></asp:TextBox></td></tr>            
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
        <asp:TextBox ID="txtComments" Width="250px" runat="server" TextMode="MultiLine" ></asp:TextBox></td></tr>            
     <tr align="center"><td colspan="2">
         <asp:ImageButton ID="imgBtnSubmit" runat="server" 
             ImageUrl="~/Images/Save2.png" OnClientClick="return Verify()" 
             onclick="imgBtnSubmit_Click" />&nbsp;
         <asp:ImageButton ID="imgBtnClear" runat="server" 
             ImageUrl="~/Images/Clear2.png" onclick="imgBtnClear_Click" />
    </td></tr>         
    </table>
</td>
</tr></table>
</asp:Content>

