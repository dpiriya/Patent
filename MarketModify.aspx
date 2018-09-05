<%@ Page Language="C#" MasterPageFile="~/Parent.master" AutoEventWireup="true" CodeFile="MarketModify.aspx.cs" Inherits="MarketModify" Title="Marketing Modification" %>
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
    
    $("#<%=txtMktgProjNo.ClientID %>").autocomplete({
    source: function(request, response) {
    $.ajax({
    type: "POST",
    contentType: "application/json; charset=utf-8",
    url: "MarketModify.aspx/GetProjectNo",
    data: "{'mktgprojno':'" + $("#<%=txtMktgProjNo.ClientID %>").val() + "'}",
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
function Verify()
{
   var r= confirm('Are you sure; Do you want to Modify this record?');
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
<table align="center" class="neTable" cellpadding="12px" width="970" cellspacing="10px">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <tr><td colspan="4" align="center" valign="middle" style="font-size:18px; color:Maroon;">
        Marketing Modification</td></tr>
    <tr><td>Marketing Project Number</td>
    <td><asp:TextBox ID="txtMktgProjNo" runat="server"></asp:TextBox>&nbsp;&nbsp;
    <asp:Button ID="btnFind" runat="server" Text="Find" BackColor="Brown" ForeColor="Yellow" onclick="btnFind_Click" />
    </td>
    <td>Marketing Group</td><td>
    <asp:TextBox ID="txtGroup" runat="server"></asp:TextBox>
    </td>
    </tr>
    <tr><td>Marketing Title</td><td>
    <asp:TextBox ID="txtTitle" TextMode="MultiLine" Wrap="true" Width="300px" runat="server"></asp:TextBox>
    </td>
    <td>Project Value</td>
    <td><asp:TextBox ID="txtValue" runat="server"></asp:TextBox>
    </td>
    </tr>
    <tr><td>Company Name</td><td>
    <asp:TextBox ID="txtCompany" Width="300px" runat="server"></asp:TextBox>
    </td>
    <td>Value Realization</td>
    <td><asp:TextBox ID="txtRealization" runat="server"></asp:TextBox>
    </td>
    </tr>
    <tr>
    <td>Project Status</td><td>
    <asp:DropDownList ID="ddlStatus" runat="server"></asp:DropDownList>
    </td>
    <td>Remarks</td>
    <td><asp:TextBox ID="txtRemark" runat="server"></asp:TextBox>
    </td>
    </tr>
    <tr><td colspan="4">
    <p>List of IDF File Numbers</p>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
<asp:ListView ID="lvIdf" runat="server" InsertItemPosition="LastItem" 
            onitemcreated="lvIdf_ItemCreated" onitemcanceling="lvIdf_ItemCanceling" 
            onitemdeleting="lvIdf_ItemDeleting" oniteminserting="lvIdf_ItemInserting">
<LayoutTemplate>
<table id="Table1" width="800px" align="center" runat="server" style="text-align:left; border-color:#DEDFDE; border-style:none; border-width:1px;" cellpadding="5" cellspacing="0" border="1">
    <tr id="Tr1" runat="server" style="background-color:#c8d59c">
    <th id="Th1" align="center" runat="server">Sl.No.</th>
    <th id="Th2" align="center" runat="server">IDF File Number</th>
    <th id="Th3" align="center" runat="server">IDF Title</th>
    <th id="Th4" align="center" runat="server">Inventor</th>
    <th id="Th7" align="center" runat="server">Application No</th>
    <th id="Th8" align="center" runat="server">Status</th>
    <th id="Th5" runat="server"></th>
    </tr>
    <tr runat="server" id="itemPlaceholder" />
    </table>
</LayoutTemplate>
<ItemTemplate>
    <tr id="Tr2" runat="server" style="background-color:White">
    <td><asp:Label ID="lblSlNo" Text='<%#Eval("SlNo")%>'  runat="server"></asp:Label></td>
    <td><asp:Label ID="lblIdfNo" BackColor="Transparent" BorderStyle="None" Width="50px" runat="server" Text='<%#Eval("FileNo")%>'></asp:Label></td>
    <td><asp:TextBox ID="lblTitle" BackColor="Transparent" BorderStyle="None" Width="300px"  TextMode="MultiLine" Wrap="true" ReadOnly="true" Text='<%#Eval("Title")%>' runat="server"></asp:TextBox></td>
    <td><asp:TextBox ID="lblInventor" BackColor="Transparent" BorderStyle="None" Width="150px" TextMode="MultiLine" Wrap="true" ReadOnly="true" Text='<%#Eval("Inventor1")%>' runat="server"></asp:TextBox></td>
    <td><asp:TextBox ID="lblApplcnNo" BackColor="Transparent" BorderStyle="None" Width="100px" TextMode="MultiLine" Wrap="true" ReadOnly="true" Text='<%#Eval("applcn_no")%>' runat="server"></asp:TextBox></td>
    <td><asp:TextBox ID="lblStatus" BackColor="Transparent" BorderStyle="None" Width="100px" TextMode="MultiLine" Wrap="true" ReadOnly="true" Text='<%#Eval("status")%>' runat="server"></asp:TextBox></td>
    <td><asp:LinkButton ID="lbtnDelete" Font-Underline="true" CommandName="delete" runat="server">Delete</asp:LinkButton>
    </td>
    </tr>
</ItemTemplate>
<AlternatingItemTemplate>
    <tr id="Tr3" runat="server" style="background-color:#F7F7DE">
    <td><asp:Label ID="lblSlNo" Text='<%#Eval("SlNo")%>'  runat="server"></asp:Label></td>
    <td><asp:Label ID="lblIdfNo" BackColor="Transparent" BorderStyle="None" Width="50px" runat="server" Text='<%#Eval("FileNo")%>'></asp:Label></td>
    <td><asp:TextBox ID="lblTitle" BackColor="Transparent" BorderStyle="None" Width="300px"  TextMode="MultiLine" Wrap="true" ReadOnly="true" Text='<%#Eval("Title")%>' runat="server"></asp:TextBox></td>
    <td><asp:TextBox ID="lblInventor" BackColor="Transparent" BorderStyle="None" Width="150px" TextMode="MultiLine" Wrap="true" ReadOnly="true" Text='<%#Eval("Inventor1")%>' runat="server"></asp:TextBox></td>
    <td><asp:TextBox ID="lblApplcnNo" BackColor="Transparent" BorderStyle="None" Width="100px" TextMode="MultiLine" Wrap="true" ReadOnly="true" Text='<%#Eval("applcn_no")%>' runat="server"></asp:TextBox></td>
    <td><asp:TextBox ID="lblStatus" BackColor="Transparent" BorderStyle="None" Width="100px" TextMode="MultiLine" Wrap="true" ReadOnly="true" Text='<%#Eval("status")%>' runat="server"></asp:TextBox></td>
    <td><asp:LinkButton ID="lbtnDelete" Font-Underline="true" CommandName="delete" runat="server">Delete</asp:LinkButton>
    </td>
    </tr>
</AlternatingItemTemplate>
<InsertItemTemplate>
<tr id="Tr4" runat="server">
    <td><asp:Label ID="lblNewSlNo" Text='<%#Eval("SlNo")%>' runat="server"></asp:Label></td>
    <td><asp:DropDownList ID="ddlNewIdfNo" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlNewIdfNo_SelectedIndexChanged"></asp:DropDownList></td>
    <td><asp:TextBox ID="txtTitle" BackColor="Transparent" ReadOnly="true" BorderStyle="None" Width="300px" TextMode="MultiLine" Wrap="true" Text='<%#Eval("Title")%>' runat="server"></asp:TextBox></td>
    <td><asp:TextBox ID="txtInventor" BackColor="Transparent" ReadOnly="true" BorderStyle="None" Width="150px" TextMode="MultiLine" Wrap="true" Text='<%#Eval("Inventor1")%>' runat="server"></asp:TextBox></td>
    <td><asp:TextBox ID="txtApplcnNo" BackColor="Transparent" ReadOnly="true" BorderStyle="None" Width="100px" TextMode="MultiLine" Wrap="true" Text='<%#Eval("applcn_no")%>' runat="server"></asp:TextBox></td>
    <td><asp:TextBox ID="txtStatus" BackColor="Transparent" ReadOnly="true" BorderStyle="None" Width="100px" TextMode="MultiLine" Wrap="true" Text='<%#Eval("status")%>' runat="server"></asp:TextBox></td>
    <td><asp:LinkButton ID="lbtnInsert" Font-Underline="true" CommandName="insert" runat="server">Insert</asp:LinkButton>
    <asp:LinkButton ID="lbtnCancel" Font-Underline="true" CommandName="cancel" runat="server">Cancel</asp:LinkButton>
    </td>
    </tr>
</InsertItemTemplate>
</asp:ListView>
</ContentTemplate>
</asp:UpdatePanel>
</td></tr>
<tr><td colspan="4" style="background-color:#f2d7d7">
<p>List of Activities</p> 
<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
<asp:ListView ID="lvActivity" runat="server" InsertItemPosition="LastItem" 
            onitemcanceling="lvActivity_ItemCanceling" 
            onitemdeleting="lvActivity_ItemDeleting" onitemediting="lvActivity_ItemEditing" 
            oniteminserting="lvActivity_ItemInserting" 
            onitemupdating="lvActivity_ItemUpdating" >
<LayoutTemplate>
<table id="Table1" width="800px" align="center" runat="server"  style="text-align:left; border-color:#DEDFDE; border-style:none; border-width:1px;" cellpadding="5" cellspacing="0" border="1">
    <tr id="Tr1" runat="server"  style="background-color:#c8d59c">
    <th id="Th1" align="center" runat="server">Sl.No.</th>
    <th id="Th2" align="center" runat="server">Activity Date</th>
    <th id="Th3" align="center" runat="server">Channel</th>
    <th id="Th4" align="center" runat="server">Activity Type</th>
    <th id="Th5" align="center" runat="server">Remarks</th>
    <th id="Th6" runat="server"></th>
    </tr>
    <tr runat="server" id="itemPlaceholder" />
    </table>
</LayoutTemplate>
<ItemTemplate>
    <tr id="Tr2" runat="server" style="background-color:White">
    <td><asp:Label ID="lblSlNo"  Text='<%#Eval("SlNo")%>' runat="server"></asp:Label></td>
    <td><asp:TextBox ID="lblActivityDt" BackColor="Transparent" BorderStyle="None" Width="100px" ReadOnly="true" Text='<%#Eval("ActivityDt","{0:dd/MM/yyyy}")%>' runat="server"></asp:TextBox></td>
    <td><asp:TextBox ID="lblChannel" BackColor="Transparent" BorderStyle="None" Width="150px" TextMode="MultiLine" Wrap="true" ReadOnly="true" Text='<%#Eval("Channel")%>' runat="server"></asp:TextBox></td>
    <td><asp:TextBox ID="lblActivityType" BackColor="Transparent" BorderStyle="None" Width="150px" TextMode="MultiLine" Wrap="true" ReadOnly="true" Text='<%#Eval("ActivityType")%>' runat="server"></asp:TextBox></td>
    <td><asp:TextBox ID="lblActivityDetails" BackColor="Transparent" BorderStyle="None" Width="300px" TextMode="MultiLine" Wrap="true" ReadOnly="true" Text='<%#Eval("Remarks")%>' runat="server"></asp:TextBox></td>
    <td><asp:LinkButton ID="lbtnEdit" Font-Underline="true" CommandName="edit" runat="server">Edit</asp:LinkButton>
    <asp:LinkButton ID="lbtnDelete" Font-Underline="true" CommandName="delete" runat="server">Delete</asp:LinkButton>
    </td>
    </tr>
</ItemTemplate>
<AlternatingItemTemplate>
    <tr id="Tr5" runat="server" style="background-color:#F7F7DE">
    <td><asp:Label ID="lblSlNo"  Text='<%#Eval("SlNo")%>' runat="server"></asp:Label></td>
    <td><asp:TextBox ID="lblActivityDt" BackColor="Transparent" BorderStyle="None" Width="100px" ReadOnly="true" Text='<%#Eval("ActivityDt","{0:dd/MM/yyyy}")%>' runat="server"></asp:TextBox></td>
    <td><asp:TextBox ID="lblChannel" BackColor="Transparent" BorderStyle="None" Width="150px" TextMode="MultiLine" Wrap="true" ReadOnly="true" Text='<%#Eval("Channel")%>' runat="server"></asp:TextBox></td>
    <td><asp:TextBox ID="lblActivityType" BackColor="Transparent" BorderStyle="None" Width="150px" TextMode="MultiLine" Wrap="true" ReadOnly="true" Text='<%#Eval("ActivityType")%>' runat="server"></asp:TextBox></td>
    <td><asp:TextBox ID="lblActivityDetails" BackColor="Transparent" BorderStyle="None" Width="300px" TextMode="MultiLine" Wrap="true" ReadOnly="true" Text='<%#Eval("Remarks")%>' runat="server"></asp:TextBox></td>
    <td><asp:LinkButton ID="lbtnEdit" Font-Underline="true" CommandName="edit" runat="server">Edit</asp:LinkButton>
    <asp:LinkButton ID="lbtnDelete" Font-Underline="true" CommandName="delete" runat="server">Delete</asp:LinkButton>
    </td>
    </tr>
</AlternatingItemTemplate>
<EditItemTemplate>
    <tr id="Tr3" runat="server">
    <td><asp:Label ID="lblEdSlNo" Text='<%#Eval("SlNo")%>' runat="server"></asp:Label></td>
    <td><asp:TextBox ID="txtEdActivityDt" BorderStyle="None" Width="100px" Text='<%#Eval("ActivityDt","{0:dd/MM/yyyy}")%>' runat="server"></asp:TextBox></td>
    <td><asp:TextBox ID="txtEdChannel" BorderStyle="None" Width="150px" TextMode="MultiLine" Wrap="true" Text='<%#Eval("Channel")%>' runat="server"></asp:TextBox></td>
    <td><asp:TextBox ID="txtEdActivityType" BorderStyle="None" Width="150px" TextMode="MultiLine" Wrap="true" Text='<%#Eval("ActivityType")%>' runat="server"></asp:TextBox></td>
    <td><asp:TextBox ID="txtEdActivityDetails" BorderStyle="None" Width="300px" TextMode="MultiLine" Wrap="true" Text='<%#Eval("Remarks")%>' runat="server"></asp:TextBox></td>
    <td><asp:LinkButton ID="lbtnUpdate" Font-Underline="true" CommandName="Update" runat="server">Update</asp:LinkButton>
    <asp:LinkButton ID="lbtnEtCancel" Font-Underline="true" CommandName="cancel" runat="server">Cancel</asp:LinkButton>
    </td>
    </tr>
</EditItemTemplate>
<InsertItemTemplate>
<tr id="Tr4" runat="server">
    <td><asp:Label ID="lblNewSlNo" Text='<%#Eval("SlNo")%>' runat="server"></asp:Label></td>
    <td><asp:TextBox ID="txtActivityDt" BorderStyle="None" Width="100px" Text='<%#Eval("ActivityDt")%>' runat="server"></asp:TextBox>
    <br /> (dd/mm/yyyy) </td>
    <td><asp:TextBox ID="txtChannel" BorderStyle="None" Width="150px" TextMode="MultiLine" Wrap="true" Text='<%#Eval("Channel")%>' runat="server"></asp:TextBox></td>
    <td><asp:TextBox ID="txtActivityType" BorderStyle="None" Width="150px" TextMode="MultiLine" Wrap="true" Text='<%#Eval("ActivityType")%>' runat="server"></asp:TextBox></td>
    <td><asp:TextBox ID="txtActivityDetails" BorderStyle="None" Width="300px" TextMode="MultiLine" Wrap="true" Text='<%#Eval("Remarks")%>' runat="server"></asp:TextBox></td>
    <td><asp:LinkButton ID="lbtnInsert" Font-Underline="true" CommandName="insert" runat="server">Insert</asp:LinkButton>
    <asp:LinkButton ID="lbtnCancel" Font-Underline="true" CommandName="cancel" runat="server">Cancel</asp:LinkButton>
    </td>
    </tr>
</InsertItemTemplate>
</asp:ListView>
</ContentTemplate>
</asp:UpdatePanel>
</td></tr>
 <tr align="center"><td colspan="4">
     <asp:ImageButton ID="imgBtnSubmit" runat="server" 
         ImageUrl="~/Images/Save2.png" OnClientClick="return Verify()" onclick="imgBtnSubmit_Click" />&nbsp;
     <asp:ImageButton ID="imgBtnClear" runat="server" 
         ImageUrl="~/Images/Clear2.png" onclick="imgBtnClear_Click" />
</td></tr>         
</table>
</asp:Content>

