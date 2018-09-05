<%@ Page Language="C#" MasterPageFile="~/Parent.master" AutoEventWireup="true" CodeFile="IndianAttorney.aspx.cs" Inherits="IndianAttorney" Title="Indian Attorney" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
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
        if (document.getElementById('<%=txtAttorneyID.ClientID%>').value== "")
        {
            alert('Attorney ID can not be empty');
            return false;
        }
        if (document.getElementById('<%=txtAttorneyName.ClientID%>').value == "")
        {
            alert('Attorney Name can not be empty');
            return false;
        }
        return true;
    }
    return false;
}
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
<table align="center" class="neTable" cellpadding="12px"  cellspacing="10px" width="950px">
<tr><td colspan="4" align="center" valign="middle" style="font-size:medium; text-decoration:underline;">
    Service Provider</td></tr>
<tr><td>Category</td><td><asp:DropDownList ID="ddlCategory" runat="server">
    <asp:ListItem>Patent Attorney</asp:ListItem>
    <asp:ListItem>Attorney</asp:ListItem>
    <asp:ListItem>Search Agency</asp:ListItem>
    <asp:ListItem>Tech Commercial Evaluation</asp:ListItem>
    <asp:ListItem>Market Partners</asp:ListItem>
    <asp:ListItem>Multiple Services</asp:ListItem>
    <asp:ListItem>Patent Agent</asp:ListItem>
    <asp:ListItem>Patent Services</asp:ListItem>
    </asp:DropDownList></td>
<td>Attorney ID</td><td><asp:TextBox ID="txtAttorneyID" Enabled="false" Width="100px" runat="server"></asp:TextBox></td></tr>
<tr><td>Attorney Name</td>
<td><asp:TextBox ID="txtAttorneyName" Width="300px" runat="server"></asp:TextBox></td><td>Address 1</td><td>
    <asp:TextBox ID="txtAddress1" Width="300px" runat="server"></asp:TextBox></td><tr><td>Address 2</td><td>
<asp:TextBox ID="txtAddress2" Width="300px" runat="server"></asp:TextBox></td>
<td>Address 3</td><td>
    <asp:TextBox ID="txtAddress3" Width="300px" runat="server"></asp:TextBox></td></tr><td>City</td><td>
<asp:TextBox ID="txtCity" Width="200px" runat="server"></asp:TextBox></td><td>Pin code</td><td>
<asp:TextBox ID="txtPincode"  runat="server"></asp:TextBox></td></tr><tr><td>County</td>
<td><asp:DropDownList ID="ddlCountry" runat="server"></asp:DropDownList></td><td>Phone Number</td><td>
<asp:TextBox ID="txtPhone"  runat="server"></asp:TextBox></td></tr><tr><td>Fax Number</td><td>
<asp:TextBox ID="txtFax" runat="server"></asp:TextBox></td>
<td>Email ID</td><td>
<asp:TextBox ID="txtEmail" Width="200px" runat="server" 
            ontextchanged="txtEmail_TextChanged"></asp:TextBox></td></tr>
            <tr><td>Mobile Number</td>
            <td><asp:TextBox ID="txtMobile" runat="server"></asp:TextBox></td></td>
<td>Range of Services</td>
<td><asp:TextBox ID="TxtRange" runat="server" Width="350px"></asp:TextBox></td></tr><tr align="center"><td colspan="4">
<asp:ImageButton ID="imgBtnSubmit" runat="server" 
 ImageUrl="~/Images/Save2.png" OnClientClick="return Verify()" onclick="imgBtnSubmit_Click"/>&nbsp;
<asp:ImageButton ID="imgBtnClear" runat="server" 
 ImageUrl="~/Images/Clear2.png" onclick="imgBtnClear_Click"/>
</td></tr>
</table>
</asp:Content>

