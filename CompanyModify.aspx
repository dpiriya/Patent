<%@ Page Language="C#" MasterPageFile="~/Parent.master" AutoEventWireup="true" CodeFile="CompanyModify.aspx.cs" Inherits="CompanyModify" Title="Marketing Company Modification" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
<link href="Styles/newEntry.css" rel="Stylesheet" type="text/css" />
<script src="Scripts/jquery-1.9.1.js" type="text/javascript"></script>
<script src="Scripts/jquery-ui-1.10.3.custom.min.js" type="text/javascript"></script>
<link href="Styles/jquery-ui-1.10.3.custom.min.css" rel="stylesheet" type="text/css" />

<script type="text/javascript">
$(document).ready(function(){
    $("table.neTable tr:even").addClass("tdTable");
    $("table.neTable tr:odd").addClass("tdTable1");  
    
    $("#<%=txtCompanyID.ClientID %>").autocomplete({
    source: function(request, response) {
    $.ajax({
    type: "POST",
    contentType: "application/json; charset=utf-8",
    url: "CompanyModify.aspx/GetCompany",
    data: "{'company':'" + $("#<%=txtCompanyID.ClientID %>").val() + "'}",
    dataType: "json",
    success: function(data) {
    response(data.d);
    },
    error: function(result) {
    alert("Error");
    }
    });
    },
    minLength: 2
    });
});
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
<div runat="server" style="text-align:center;color:Red; font-weight:bold" id="ErrorMsg"></div>
<table align="center" class="neTable" cellpadding="12px"  cellspacing="10px" width="950px">
<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
</asp:ToolkitScriptManager>
<tr><td colspan="4" align="center" valign="middle" style="font-size:18px; color:Maroon;">
    Company Modification</td></tr>
<tr><td>Company ID</td>
<td><asp:TextBox ID="txtCompanyID" Width="100px" runat="server"></asp:TextBox>&nbsp;&nbsp;&nbsp;
<asp:Button ID="btnFind" runat="server" Text="Find" BackColor="Brown" 
        ForeColor="Yellow" onclick="btnFind_Click" /></td>
<td>Company Name</td><td>
<asp:TextBox ID="txtCompanyName" Width="300px" runat="server"></asp:TextBox>
</td></tr>
<tr><td>Address 1</td><td>
    <asp:TextBox ID="txtAddress1" Width="300px" runat="server"></asp:TextBox>
</td>
<td>Address 2</td><td>
<asp:TextBox ID="txtAddress2" Width="300px" runat="server"></asp:TextBox>
</td></tr>
<tr><td>City</td><td>
<asp:TextBox ID="txtCity" Width="300px" runat="server"></asp:TextBox>
</td>
<td>State</td><td>
<asp:TextBox ID="txtState" Width="300px" runat="server"></asp:TextBox>
</td></tr>
<tr>
<td>Country</td><td>
<asp:TextBox ID="txtCountry" Width="300px" runat="server"></asp:TextBox>
</td> 
<td>Pin Code</td><td>
<asp:TextBox ID="txtPincode" Width="100px" runat="server"></asp:TextBox>
</td></tr> 
<tr><td>Phone Number 1</td><td>
<asp:TextBox ID="txtPhone1"  runat="server"></asp:TextBox>
</td> 
<td>Phone Number 2</td><td>
<asp:TextBox ID="txtPhone2" runat="server"></asp:TextBox>
</td></tr> 
<tr><td>Fax Number</td><td>
<asp:TextBox ID="txtFax" runat="server"></asp:TextBox>
</td> 
<td>Email ID 1</td><td>
<asp:TextBox ID="txtEmail1" Width="300px" runat="server"></asp:TextBox>
</td></tr> 
<tr><td>Email ID 2</td><td>
<asp:TextBox ID="txtEmail2" Width="300px" runat="server"></asp:TextBox>
</td> 
<td>Industry Type</td><td>
<asp:TextBox ID="txtIndustry" Width="300px" runat="server"></asp:TextBox> 
</td></tr> 
<tr><td colspan="4"><h3>Business Area</h3>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
<asp:ListView ID="lvBusiness" runat="server" onitemediting="lvBusiness_ItemEditing" 
        InsertItemPosition="LastItem" onitemcanceling="lvBusiness_ItemCanceling" 
        onitemdeleting="lvBusiness_ItemDeleting" 
        oniteminserting="lvBusiness_ItemInserting" 
        onitemupdating="lvBusiness_ItemUpdating" 
        onitemdatabound="lvBusiness_ItemDataBound">
<LayoutTemplate>
<table id="Table1" width="700px" align="center" runat="server"  style="text-align:left" cellpadding="5" cellspacing="0" border="1">
    <tr id="Tr1" runat="server" style="background-color:#c8d59c">
    <th id="Th1" align="center" runat="server">Sl.No.</th>
    <th id="Th2" align="center" runat="server">Business Area</th>
    <th id="Th3" align="center" runat="server">Products</th>
    <th id="Th4" runat="server"></th>
    </tr>
    <tr runat="server" id="itemPlaceholder" />
    </table>
</LayoutTemplate>
<ItemTemplate>
    <tr id="Tr2" runat="server" style="background-color:White">
    <td><asp:Label ID="lblSlNo"  Text='<%#Eval("SlNo")%>' runat="server"></asp:Label></td>
    <td><asp:TextBox ID="lblBusiness" BackColor="Transparent" BorderStyle="None" Width="300px" TextMode="MultiLine" Wrap="true" ReadOnly="true" Text='<%#Eval("BusinessArea")%>' runat="server"></asp:TextBox></td>
    <td><asp:TextBox ID="lblProduct" BackColor="Transparent" BorderStyle="None" Width="300px" TextMode="MultiLine" Wrap="true" ReadOnly="true"  Text='<%#Eval("Products")%>' runat="server"></asp:TextBox></td>
    <td><asp:LinkButton ID="lbtnEdit" Font-Underline="true" CommandName="edit" runat="server">Edit</asp:LinkButton>
    <asp:LinkButton ID="lbtnDelete" Font-Underline="true" CommandName="delete" runat="server">Delete</asp:LinkButton>
    </td>
    </tr>
</ItemTemplate>
<AlternatingItemTemplate>
    <tr id="Tr5" runat="server" style="background-color:#F7F7DE">
    <td><asp:Label ID="lblSlNo"  Text='<%#Eval("SlNo")%>' runat="server"></asp:Label></td>
    <td><asp:TextBox ID="lblBusiness" BackColor="Transparent" BorderStyle="None" Width="300px" TextMode="MultiLine" Wrap="true" ReadOnly="true" Text='<%#Eval("BusinessArea")%>' runat="server"></asp:TextBox></td>
    <td><asp:TextBox ID="lblProduct" BackColor="Transparent" BorderStyle="None" Width="300px" TextMode="MultiLine" Wrap="true" ReadOnly="true"  Text='<%#Eval("Products")%>' runat="server"></asp:TextBox></td>
    <td><asp:LinkButton ID="lbtnEdit" Font-Underline="true" CommandName="edit" runat="server">Edit</asp:LinkButton>
    <asp:LinkButton ID="lbtnDelete" Font-Underline="true" CommandName="delete" runat="server">Delete</asp:LinkButton>
    </td>
    </tr>
</AlternatingItemTemplate>
<EditItemTemplate>
    <tr id="Tr3" runat="server">
    <td><asp:Label ID="lblSlNo" Text='<%#Eval("SlNo")%>' runat="server"></asp:Label></td>
    <td><asp:TextBox ID="txtEditBusiness"  Width="300px" Text='<%#Eval("BusinessArea")%>' TextMode="MultiLine" Wrap="true" runat="server"></asp:TextBox></td>
    <td><asp:TextBox  ID="txtEditProduct" Width="300px" Text='<%#Eval("Products")%>' TextMode="MultiLine" Wrap="true" runat="server"></asp:TextBox></td>
    <td><asp:LinkButton ID="lbtnUpdate" Font-Underline="true" CommandName="Update" runat="server">Update</asp:LinkButton>
    <asp:LinkButton ID="lbtnEtCancel" Font-Underline="true" CommandName="cancel" runat="server">Cancel</asp:LinkButton>
    </td>
    </tr>
</EditItemTemplate>
<InsertItemTemplate>
<tr id="Tr4" runat="server">
    <td><asp:Label ID="lblNewSlNo" Text='<%#Eval("SlNo")%>' runat="server"></asp:Label></td>
    <td><asp:TextBox ID="txtNewBusiness" Width="100%" Text='<%#Eval("BusinessArea")%>' TextMode="MultiLine" Wrap="true" runat="server"></asp:TextBox></td>
    <td><asp:TextBox ID="txtNewProduct" Width="100%" Text='<%#Eval("Products")%>' TextMode="MultiLine" Wrap="true" runat="server"></asp:TextBox></td>
    <td><asp:LinkButton ID="lbtnInsert" Font-Underline="true" CommandName="insert" runat="server">Insert</asp:LinkButton>
    <asp:LinkButton ID="lbtnCancel" Font-Underline="true" CommandName="cancel" runat="server">Cancel</asp:LinkButton>
    </td>
    </tr>
</InsertItemTemplate>
</asp:ListView>
</ContentTemplate>
</asp:UpdatePanel>
</td></tr>
<tr style="background-color:White;"><td colspan="4"><h3>Contact Person Details</h3>
<asp:UpdatePanel ID="UpdatePanel2" runat="server">
<ContentTemplate>
<asp:ListView ID="lvContact" runat="server" InsertItemPosition="LastItem" 
        onitemcanceling="lvContact_ItemCanceling" 
        onitemdatabound="lvContact_ItemDataBound" 
        onitemdeleting="lvContact_ItemDeleting" onitemediting="lvContact_ItemEditing" 
        oniteminserting="lvContact_ItemInserting" 
        onitemupdating="lvContact_ItemUpdating">
<LayoutTemplate>
<table id="Table1" width="850px" align="center" runat="server"  style="text-align:left" cellpadding="5" cellspacing="0" border="1">
    <tr id="Tr1" runat="server" style="background-color:#c8d59c">
    <th id="Th1" align="center" runat="server">Sl.No.</th>
    <th id="Th2" align="center" runat="server">Contact Person Name</th>
    <th id="Th3" align="center" runat="server">Business Area</th>
    <th id="Th4" align="center" runat="server">Adress1</th>
    <th id="Th5" align="center" runat="server">Adress2</th>
    <th id="Th6" align="center" runat="server">Phone No.1</th>
    <th id="Th7" align="center" runat="server">Phone No.2</th>
    <th id="Th8" align="center" runat="server">Email ID</th>
    <th id="Th9" runat="server"></th>
    </tr>
    <tr runat="server" id="itemPlaceholder" />
    </table>
</LayoutTemplate>
<ItemTemplate>
    <tr id="Tr2" runat="server" style="background-color:White">
    <td><asp:Label ID="lblSlNo"  Text='<%#Eval("SlNo")%>' runat="server"></asp:Label></td>
    <td><asp:TextBox ID="lblContactName" BackColor="Transparent" BorderStyle="None" Width="100px" ReadOnly="true" Text='<%#Eval("ContactPersonName")%>' runat="server"></asp:TextBox></td>
    <td><asp:TextBox ID="lblBusiness" BackColor="Transparent" BorderStyle="None" Width="100px" ReadOnly="true" Text='<%#Eval("BusinessArea")%>' runat="server"></asp:TextBox></td>
    <td><asp:TextBox ID="lblAddress1" BackColor="Transparent" BorderStyle="None" Width="100px" ReadOnly="true"  Text='<%#Eval("Address1")%>' runat="server"></asp:TextBox></td>
    <td><asp:TextBox ID="lblAddress2" BackColor="Transparent" BorderStyle="None" Width="100px" ReadOnly="true"  Text='<%#Eval("Address2")%>' runat="server"></asp:TextBox></td>
    <td><asp:TextBox ID="lblPhone1" BackColor="Transparent" BorderStyle="None" Width="100px" ReadOnly="true"  Text='<%#Eval("PhoneNo1")%>' runat="server"></asp:TextBox></td>
    <td><asp:TextBox ID="lblPhone2" BackColor="Transparent" BorderStyle="None" Width="100px" ReadOnly="true"  Text='<%#Eval("PhoneNo2")%>' runat="server"></asp:TextBox></td>
    <td><asp:TextBox ID="lblEmail" BackColor="Transparent" BorderStyle="None" Width="100px" ReadOnly="true"  Text='<%#Eval("EmailID")%>' runat="server"></asp:TextBox></td>
    <td><asp:LinkButton ID="btnEdit" Font-Underline="true" CommandName="edit" runat="server">Edit</asp:LinkButton>
    <asp:LinkButton ID="btnDelete" Font-Underline="true" CommandName="delete" runat="server">Delete</asp:LinkButton>
    </td>
    </tr>
</ItemTemplate>
<AlternatingItemTemplate>
<tr id="Tr5" runat="server" style="background-color:#F7F7DE">
    <td><asp:Label ID="lblSlNo"  Text='<%#Eval("SlNo")%>' runat="server"></asp:Label></td>
    <td><asp:TextBox ID="lblContactName" BackColor="Transparent" BorderStyle="None" Width="100px" ReadOnly="true" Text='<%#Eval("ContactPersonName")%>' runat="server"></asp:TextBox></td>
    <td><asp:TextBox ID="lblBusiness" BackColor="Transparent" BorderStyle="None" Width="100px" ReadOnly="true" Text='<%#Eval("BusinessArea")%>' runat="server"></asp:TextBox></td>
    <td><asp:TextBox ID="lblAddress1" BackColor="Transparent" BorderStyle="None" Width="100px" ReadOnly="true"  Text='<%#Eval("Address1")%>' runat="server"></asp:TextBox></td>
    <td><asp:TextBox ID="lblAddress2" BackColor="Transparent" BorderStyle="None" Width="100px" ReadOnly="true"  Text='<%#Eval("Address2")%>' runat="server"></asp:TextBox></td>
    <td><asp:TextBox ID="lblPhone1" BackColor="Transparent" BorderStyle="None" Width="100px" ReadOnly="true"  Text='<%#Eval("PhoneNo1")%>' runat="server"></asp:TextBox></td>
    <td><asp:TextBox ID="lblPhone2" BackColor="Transparent" BorderStyle="None" Width="100px" ReadOnly="true"  Text='<%#Eval("PhoneNo2")%>' runat="server"></asp:TextBox></td>
    <td><asp:TextBox ID="lblEmail" BackColor="Transparent" BorderStyle="None" Width="100px" ReadOnly="true"  Text='<%#Eval("EmailID")%>' runat="server"></asp:TextBox></td>
    <td><asp:LinkButton ID="btnEdit" Font-Underline="true" CommandName="edit" runat="server">Edit</asp:LinkButton>
    <asp:LinkButton ID="btnDelete" Font-Underline="true" CommandName="delete" runat="server">Delete</asp:LinkButton>
    </td>
    </tr>
</AlternatingItemTemplate>
<EditItemTemplate>
    <tr id="Tr3" runat="server">
    <td><asp:Label ID="lblEditSlNo" Text='<%#Eval("SlNo")%>' runat="server"></asp:Label></td>
    <td><asp:TextBox ID="txtEditContact"  Width="100px" Text='<%#Eval("ContactPersonName")%>' runat="server"></asp:TextBox></td>
    <td><asp:TextBox ID="txtEditBusiness"  Width="100px" Text='<%#Eval("BusinessArea")%>' runat="server"></asp:TextBox></td>
    <td><asp:TextBox  ID="txtEditAddress1" Width="100px" Text='<%#Eval("Address1")%>' runat="server"></asp:TextBox></td>
    <td><asp:TextBox  ID="txtEditAddress2" Width="100px" Text='<%#Eval("Address2")%>' runat="server"></asp:TextBox></td>
    <td><asp:TextBox  ID="txtPhone1" Width="100px" Text='<%#Eval("PhoneNo1")%>' runat="server"></asp:TextBox></td>
    <td><asp:TextBox  ID="txtPhone2" Width="100px" Text='<%#Eval("PhoneNo2")%>' runat="server"></asp:TextBox></td>
    <td><asp:TextBox  ID="txtEmail" Width="100px" Text='<%#Eval("EmailID")%>' runat="server"></asp:TextBox></td>
    <td><asp:LinkButton ID="btnUpdate" Font-Underline="true" CommandName="Update" runat="server">Update</asp:LinkButton>
    <asp:LinkButton ID="btnEtCancel" Font-Underline="true" CommandName="cancel" runat="server">Cancel</asp:LinkButton>
    </td>
    </tr>
</EditItemTemplate>
<InsertItemTemplate>
<tr id="Tr4" runat="server">
    <td><asp:Label ID="lblNewSlNo" Text='<%#Eval("SlNo")%>' runat="server"></asp:Label></td>
    <td><asp:TextBox ID="txtNewContact"  Width="100%" Text='<%#Eval("ContactPersonName")%>' runat="server"></asp:TextBox></td>
    <td><asp:TextBox ID="txtNewBusiness"  Width="100%" Text='<%#Eval("BusinessArea")%>' runat="server"></asp:TextBox></td>
    <td><asp:TextBox  ID="txtNewAddress1" Width="100%" Text='<%#Eval("Address1")%>' runat="server"></asp:TextBox></td>
    <td><asp:TextBox  ID="txtNewAddress2" Width="100%" Text='<%#Eval("Address2")%>' runat="server"></asp:TextBox></td>
    <td><asp:TextBox  ID="txtNewPhone1" Width="100%" Text='<%#Eval("PhoneNo1")%>' runat="server"></asp:TextBox></td>
    <td><asp:TextBox  ID="txtNewPhone2" Width="100%" Text='<%#Eval("PhoneNo2")%>' runat="server"></asp:TextBox></td>
    <td><asp:TextBox  ID="txtNewEmail" Width="100%" Text='<%#Eval("EmailID")%>' runat="server"></asp:TextBox></td>
    <td><asp:LinkButton ID="btnInsert" Font-Underline="true" CommandName="insert" runat="server">Insert</asp:LinkButton>
    <asp:LinkButton ID="btnCancel" Font-Underline="true" CommandName="cancel" runat="server">Cancel</asp:LinkButton>
    </td>
    </tr>
</InsertItemTemplate>
</asp:ListView>
</ContentTemplate>
</asp:UpdatePanel>
</td></tr>
<tr align="center"><td colspan="4">
<asp:ImageButton ID="imgBtnSubmit" runat="server" 
 ImageUrl="~/Images/Save2.png" onclick="imgBtnSubmit_Click"/>&nbsp;
<asp:ImageButton ID="imgBtnClear" runat="server" 
 ImageUrl="~/Images/Clear2.png" onclick="imgBtnClear_Click"/>
</td></tr>   
</table>
</asp:Content>

