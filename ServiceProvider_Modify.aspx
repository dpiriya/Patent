<%@ Page Title="" Language="C#" MasterPageFile="~/Parent.master" AutoEventWireup="true" CodeFile="ServiceProvider_Modify.aspx.cs" Inherits="ServiceProvider_Modify" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<script runat="server">


</script>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
<link href="Styles/newEntry.css" rel="Stylesheet" type="text/css" />
<script src="Scripts/jquery-1.9.1.js" type="text/javascript"></script>
<script src="Scripts/jquery-ui-1.10.3.custom.min.js" type="text/javascript"></script>
<link href="Styles/jquery-ui-1.10.3.custom.min.css" rel="stylesheet" type="text/css" />
<script type="text/javascript">
    $(document).ready(function() {
        $("table.neTable tr:even").addClass("tdTable");
        $("table.neTable tr:odd").addClass("tdTable1");

    });
</script> 

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
   <table align="center" class="neTable" cellpadding="12px"  cellspacing="10px" width="950px">
<tr><td colspan="4" align="center" valign="middle" style="font-size:medium; text-decoration:underline;">
    Indian Patent Attorney</td></tr>
<tr>
<td class="style4">Attorney ID</td><td class="style3">
<asp:DropDownList ID="dropId" runat="server" EnableViewState="true" onselectedindexchanged="dropId_SelectedIndexChanged"></asp:DropDownList>
    <asp:Button ID="button1" runat="server" Text="Find" onclick="button1_Click"/></td>
<td class="style4">Category</td><td class="style5"><asp:DropDownList ID="ddlCategory" runat="server">
    <asp:ListItem>Patent Attorney</asp:ListItem>
    <asp:ListItem>Attorney</asp:ListItem>
    <asp:ListItem>Search Agency</asp:ListItem>
    <asp:ListItem>Tech Commercial Evaluation</asp:ListItem>
    <asp:ListItem>Market Partners</asp:ListItem>
    <asp:ListItem>Multiple Services</asp:ListItem>
    </asp:DropDownList></td></tr>
<tr><td class="style4">Attorney Name</td>
<td class="style3"><asp:TextBox ID="txtAttorneyName" Width="300px" runat="server"></asp:TextBox></td>
    <td class="style4">Address 1</td><td class="style5">
    <asp:TextBox ID="txtAddress1" Width="300px" runat="server"></asp:TextBox></td><tr>
        <td class="style4">Address 2</td><td class="style3">
<asp:TextBox ID="txtAddress2" Width="300px" runat="server"></asp:TextBox></td>
<td class="style4">Address 3</td><td class="style5">
    <asp:TextBox ID="txtAddress3" Width="300px" runat="server"></asp:TextBox></td></tr>
    <td class="style4">City</td><td class="style3">
<asp:TextBox ID="txtCity" Width="200px" runat="server"></asp:TextBox></td>
    <td class="style4">Pin code</td><td class="style5">
<asp:TextBox ID="txtPincode"  runat="server"></asp:TextBox></td></tr><tr>
        <td class="style4">County</td>
<td class="style3"><asp:DropDownList ID="ddlCountry" runat="server"></asp:DropDownList></td>
        <td class="style4">Phone Number</td><td class="style5">
<asp:TextBox ID="txtPhone"  runat="server"></asp:TextBox></td></tr><tr>
        <td class="style4">Fax Number</td><td class="style3">
<asp:TextBox ID="txtFax" runat="server"></asp:TextBox></td>
<td class="style4">Email ID</td><td class="style5">
<asp:TextBox ID="txtEmail" Width="200px" runat="server"></asp:TextBox></td></tr>
            <tr><td class="style4">Mobile Number</td>
            <td class="style3"><asp:TextBox ID="txtMobile" runat="server"></asp:TextBox></td></td>
<td class="style4">Range of Services</td>
<td class="style5"><asp:TextBox ID="TxtRange" runat="server" Width="350px"></asp:TextBox></td></tr><tr align="center"><td colspan="4">
<asp:ImageButton ID="imgBtnSubmit" runat="server" 
 ImageUrl="~/Images/Save2.png" OnClientClick="return Verify()"/>&nbsp;
<asp:ImageButton ID="imgBtnClear" runat="server" 
 ImageUrl="~/Images/Clear2.png"/>
</td></tr>
</table>
</asp:Content>

