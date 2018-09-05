<%@ Page Language="C#" MasterPageFile="~/Parent.master" AutoEventWireup="true" CodeFile="AuditDetails.aspx.cs" Inherits="AuditDetails" Title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
<link href="Styles/newEntry.css" rel="Stylesheet" type="text/css" />
<script src="Scripts/jquery-1.9.1.js" type="text/javascript"></script>
<script src="Scripts/jquery-ui-1.10.3.custom.min.js" type="text/javascript"></script>
<link href="Styles/jquery-ui-1.10.3.custom.min.css" rel="stylesheet" type="text/css" />
<script type="text/javascript">
$(document).ready(function(){
    
});
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
<table align="center" cellpadding="12px" cellspacing="2px" style="font-family:Arial; font-size:12px; font-weight:bold; " >
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
    <tr><td colspan="3" align="center" valign="middle" style=" font-family:Arial;font-size:16px;font-weight:bold;width:95%;color:Maroon; text-decoration:underline;">
        User Audit Search</td><td style="width:5%;">
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
 <table align="center">
     <tr><td align="left" style=" font-family:Arial;font-size:14px;font-weight:bold; text-decoration:underline;">
        <i id="SearchResult" visible="false" runat="server">Search Results</i></td></tr>
     <tr><td >
     <asp:GridView ID="gvAudit" AutoGenerateColumns="false" Width="930px" 
             CellPadding="3" CellSpacing="0" DataKeyNames="TableName" 
             Font-Names="Arial" BorderColor="#006633" HeaderStyle-ForeColor="#dffaa4" runat="server" 
             AllowSorting="true" AllowPaging="true" PageSize="15" 
             onpageindexchanging="gvAudit_PageIndexChanging" onsorting="gvAudit_Sorting">
     <Columns>
     <asp:BoundField DataField="ModifyDt" HeaderStyle-Font-Underline="true" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Modify Dt." SortExpression="ModifyDt"  />
     <asp:BoundField DataField="TableName" HeaderStyle-Font-Underline="true" ItemStyle-HorizontalAlign="Left" SortExpression="TableName"  HeaderText="Table Name" />
     <asp:BoundField HeaderStyle-Font-Underline="true" ItemStyle-HorizontalAlign="Left" DataField="ColumnName" SortExpression="ColumnName" HeaderText="Column Name" />
     <asp:BoundField HeaderStyle-Font-Underline="true" ItemStyle-HorizontalAlign="Left" DataField="FileNo" SortExpression="FileNo" HeaderText="File No." />
     <asp:BoundField ItemStyle-HorizontalAlign="Left" DataField="RowID" HeaderText="Row ID" />
     <asp:BoundField ItemStyle-Wrap="true" ItemStyle-HorizontalAlign="Left" DataField="PreviousValue" HeaderText="Previous Value" />
     <asp:BoundField ItemStyle-Wrap="true" ItemStyle-HorizontalAlign="Left" DataField="CurrentValue" HeaderText="Current Value" />
     <asp:BoundField HeaderStyle-Font-Underline="true" ItemStyle-HorizontalAlign="Left" DataField="UserName" SortExpression="UserName" HeaderText="User Name" />
     </Columns>
     <EmptyDataTemplate>
     <p style="text-align:center; background-color:#C8C8C8">
     <asp:Image ID="Image1" BorderStyle="None" BackColor="#C8C8C8" ImageUrl="~/Images/NoData.jpg" runat="server" />
     </p>
     </EmptyDataTemplate>
     <HeaderStyle HorizontalAlign="Center" BackColor="#006633" VerticalAlign="Middle" Font-Size="14px" Height="35px" />
     <RowStyle BackColor="White" Height="25px" Font-Size="13px" Wrap="true"/>
     <AlternatingRowStyle Wrap="true" BackColor="#FFFFCC" />
     <PagerSettings Mode="Numeric" PageButtonCount="9" Position="TopAndBottom"/>
     <PagerStyle Font-Underline="true" Font-Bold="true" Font-Italic="true" ForeColor="Red"/>
     </asp:GridView> 
     <i id="pageNumber" visible="false" style="font-weight:bold" runat="server"> You are viewing page <%=gvAudit.PageIndex+1%> of <%= gvAudit.PageCount%></i> 
     </td></tr></table>
</asp:Content>

