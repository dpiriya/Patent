<%@ Page Language="C#" MasterPageFile="~/Parent.master" AutoEventWireup="true" CodeFile="ReceiptModify.aspx.cs" Inherits="ReceiptModify" Title="Receipt Modification" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
<link href="Styles/newEntry.css" rel="Stylesheet" type="text/css" />
<script src="Scripts/jquery-1.9.1.js" type="text/javascript"></script>
<script src="Scripts/jquery-ui-1.10.3.custom.min.js" type="text/javascript"></script>
<link href="Styles/jquery-ui-1.10.3.custom.min.css" rel="stylesheet" type="text/css" />

<script type="text/javascript">
$(document).ready(function(){
    $("#<%=txtPartyName.ClientID %>").autocomplete({
    source: function(request, response) {
    $.ajax({
    type: "POST",
    contentType: "application/json; charset=utf-8",
    url: "ReceiptModify.aspx/GetAutoCompleteData",
    data: "{'party':'" + $("#<%=txtPartyName.ClientID %>").val() + "'}",
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
   
    $("#<%=txtTransType.ClientID %>").autocomplete({
    source: function(request, response) {
    $.ajax({
    type: "POST",
    contentType: "application/json; charset=utf-8",
    url: "ReceiptModify.aspx/GetTransTypeData",
    data: "{'Trans':'" + $("#<%=txtTransType.ClientID %>").val() + "'}",
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
<table width="990px">
    <tr align="center" valign="middle" style="font-size:14px; font-family:Arial;text-decoration:underline;"><th id="lblTitle" runat="server" colspan="2"></th></tr>
    <tr align="center" valign="middle" style="font-size:14px; font-family:Arial;text-decoration:underline;"><th>
        List of Receipts</th><th>Receipt Modification</th></tr>
    <tr><td valign="top">
    <asp:ListView ID="lvReceipt" runat="server" onitemdeleting="lvReceipt_ItemDeleting" onitemdatabound="lvReceipt_ItemDataBound"
            onselectedindexchanging="lvReceipt_SelectedIndexChanging">
    <LayoutTemplate>
    <table id="Table1" width="500px" runat="server" class="neTable" cellpadding="5" cellspacing="5" border="1">
    <tr id="Tr1" runat="server">
    <th id="Th2" runat="server">Sl.No.</th>
    <th id="Th3" runat="server">Party</th>
    <th id="Th1" runat="server">Receipt Date</th>
    <th id="Th4" runat="server">Receipt Description</th>
    <th id="Th6" runat="server">Amount in Rs.</th>
    <th id="Th5" runat="server"></th>
    </tr>
    <tr runat="server" id="itemPlaceholder" />
    </table>
    </LayoutTemplate>
    <ItemTemplate>
    <tr id="Tr2" runat="server">
    <td><asp:Label ID="lblSlNo" Text='<%#Eval("slno")%>' runat="server"></asp:Label></td>
    <td><asp:Label ID="lblParty" Width="90px" Text='<%#Eval("party")%>' runat="server"></asp:Label></td>
    <td><asp:Label ID="lblDate" Width="" Text='<% #Eval("PaymentDate","{0:dd/MM/yyyy}")%>' runat="server"></asp:Label></td>
    <td><asp:Label ID="lblDescription" Width="100px" Text='<%#Eval("paymentDescription")%>' runat="server"></asp:Label></td>
    <td><asp:Label ID="lblAmount" Text='<%#Eval("Cost_Rs")%>' runat="server"></asp:Label></td>
    <td><asp:LinkButton ID="lbtnEdit" Font-Underline="true" CommandName="Select" runat="server">Edit</asp:LinkButton>
    <asp:LinkButton ID="lbtnDelete" OnClientClick="return Verify()" Font-Underline="true" CommandName="Delete" runat="server">Delete</asp:LinkButton>
    </td>
    </tr>
    </ItemTemplate>
    <SelectedItemTemplate>
    <tr id="Tr2" runat="server">
    <td><asp:Label ID="lblSlNo" Text='<%#Eval("slno")%>' runat="server"></asp:Label></td>
    <td><asp:Label ID="lblParty" Text='<%#Eval("party")%>' runat="server"></asp:Label></td>
    <td><asp:Label ID="lblDate" Text='<%#Eval("PaymentDate")%>' runat="server"></asp:Label></td>
    <td><asp:Label ID="lblDescription" Text='<%#Eval("paymentDescription")%>' runat="server"></asp:Label></td>
    <td><asp:Label ID="lblAmount" Text='<%#Eval("Cost_Rs")%>' runat="server"></asp:Label></td>
        <td><asp:LinkButton ID="lbtnEdit" Font-Underline="true" CommandName="Select" runat="server">Edit</asp:LinkButton>
    <asp:LinkButton ID="lbtnDelete" OnClientClick="return Verify()" Font-Underline="true" CommandName="Delete" runat="server">Delete</asp:LinkButton></td>
    </tr>
    </SelectedItemTemplate>
    </asp:ListView>
    </td>
    <td valign="top">
    <table align="center" width="475px" class="neTable" cellpadding="12px"  cellspacing="10px">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <tr><td>File No</td>
    <td><asp:Label ID="lblFileNo" runat="server"  EnableViewState="true"></asp:Label></td></tr>
    <tr><td>Serial No</td>
    <td><asp:Label ID="lblSerial" runat="server"></asp:Label></td></tr>
    <tr><td>Technology Transfer No.</td><td>
    <asp:TextBox ID="txtTechTransNo" Width="200px" runat="server"></asp:TextBox>
    </td></tr>
    <tr><td>Party Name</td><td>
        <asp:TextBox ID="txtPartyName" Width="200px" TextMode="MultiLine" runat="server"></asp:TextBox>
    </td></tr>
    <tr><td>Party Reference No.</td><td>
    <asp:TextBox ID="txtPartyRefNo" runat="server"></asp:TextBox>
    </td></tr>
    <tr><td>Intimation Reference</td><td>
        <asp:TextBox ID="txtTransType" runat="server"></asp:TextBox></td></tr> 
    <tr><td>Intimation Date</td><td>
        <asp:TextBox ID="txtSubDate" runat="server"></asp:TextBox>&nbsp;<asp:Image ID="imgSubmission"
            runat="server" ImageUrl="~/Images/RedCalendar.gif" /> 
        <asp:CalendarExtender ID="CalendarExtender1" runat="server" Animated="true" Format="dd/MM/yyyy" PopupButtonID="imgSubmission" TargetControlID="txtSubDate">
        </asp:CalendarExtender>
    </td></tr> 
    <tr><td>Transaction Description</td><td>
        <asp:TextBox ID="txtTransDesc" runat="server" TextMode="MultiLine" ></asp:TextBox></td></tr>            
    <tr><td>Receipt Group</td><td>
        <asp:DropDownList ID="ddlPaymentGroup" runat="server">
        </asp:DropDownList></td></tr> 
    <tr><td>Receipt Reference</td><td>
        <asp:TextBox ID="txtPaymentRef" runat="server"></asp:TextBox></td></tr>           
    <tr><td>Receipt Date</td><td>
        <asp:TextBox ID="txtPaymentDt" runat="server"></asp:TextBox>
        <asp:Image ID="imgPaymentDt" runat="server" ImageUrl="~/Images/RedCalendar.gif" /> 
        <asp:CalendarExtender ID="CalendarExtender2" runat="server" Animated="true" Format="dd/MM/yyyy" PopupButtonID="imgPaymentDt" TargetControlID="txtPaymentDt">
        </asp:CalendarExtender></td></tr>                          
    <tr><td>Receipt Description</td><td>
        <asp:TextBox ID="txtPaymentDesc" runat="server" TextMode="MultiLine" ></asp:TextBox></td></tr>            
    <tr><td>Receipt Currency</td><td>
        <asp:DropDownList ID="ddlCurrency" runat="server">
        </asp:DropDownList></td></tr>            
    <tr id="trExRate"><td>Exchange Rate</td><td>
        <asp:TextBox ID="txtExRate" runat="server"></asp:TextBox></td></tr>            
    <tr id="trCost"><td>Amount in Foreign Currency</td><td>
        <asp:TextBox ID="txtCost" runat="server"></asp:TextBox></td></tr>            
    <tr><td>Amount in INR</td><td>
        <asp:TextBox ID="txtCostINR" runat="server"></asp:TextBox></td></tr>            
    <tr><td>Year</td><td>
        <asp:DropDownList ID="ddlYear" runat="server">
        </asp:DropDownList></td></tr>             
     <tr align="center"><td colspan="2">
         <asp:ImageButton ID="imgBtnSubmit" runat="server" 
             ImageUrl="~/Images/Save2.png" onclick="imgBtnSubmit_Click" />&nbsp;
         <asp:ImageButton ID="imgBtnClear" runat="server" 
             ImageUrl="~/Images/Clear2.png" onclick="imgBtnClear_Click" />
    </td></tr>                   
 </table>
    </td>
    </tr>
    </table>
</asp:Content>

