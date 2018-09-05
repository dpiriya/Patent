<%@ Page Language="C#" MasterPageFile="~/Parent.master" AutoEventWireup="true" CodeFile="Payment.aspx.cs" Inherits="Payment" Title="Patent Payment" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
<link href="Styles/TableStyle1.css" rel="Stylesheet" type="text/css" />
<script src="Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
<script src="Scripts/jquery-1.4.1.js" type="text/javascript"></script>
<script type="text/javascript">
$(document).ready(function(){
    $("#trPayment").hide();
    $("#trExRate").hide();
    $("table.neTable tr:even").addClass("tdTable");
    $("table.neTable tr:odd").addClass("tdTable1");
    $("#<%=ddlCurrency.ClientID %>").change(function(){
    var val1=$("#<%=ddlCurrency.ClientID %>").val();
    $("#<%=txtPayment.ClientID %>").val("");
    $("#<%=txtExRate.ClientID %>").val("");
    if(val1 != "INR")
    {
        $("#trPayment").show();
    }
    else
    {
        $("#trPayment").hide();
    }
    });
  });
function Verify()
{
    var r= confirm('Are you sure; Do you want to insert this record?');
    if (r==true)
    {
        if (document.getElementById('<%=ddlFileNo.ClientID%>').value== "")
        {
            alert('File No. can not be empty');
            return false;
        }
        
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
<table align="center" class="neTable" cellpadding="12px" width="900px" cellspacing="10px" style=" text-align:left;">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <tr><td colspan="4" align="center" valign="middle" style="font-size:medium; text-decoration:underline;">PAYMENT</td></tr>
    <tr><td>File Number</td>
    <td> <asp:DropDownList ID="ddlFileNo" runat="server" AutoPostBack="true" onselectedindexchanged="ddlFileNo_SelectedIndexChanged1">
    </asp:DropDownList>
    </td>
    <td>Cost Group </td><td><asp:DropDownList ID="ddlCostGrp" runat="server">
        </asp:DropDownList>
    </td></tr>
    <tr><td>Payment Description</td><td colspan="3">
        <asp:TextBox ID="txtDescription" runat="server" Width="300px" TextMode="MultiLine"></asp:TextBox></td></tr>
    <tr><td>Party Group</td><td>
        <asp:DropDownList ID="ddlPMode" runat="server">
        </asp:DropDownList>
    </td>
    <td>Party Name</td><td>
        <asp:DropDownList ID="ddlParty" runat="server">
        </asp:DropDownList>
    </td></tr>
    <tr><td>Invoice Ref./Invoice Number</td><td>
        <asp:TextBox ID="txtInvoice" runat="server"></asp:TextBox></td> 
    <td>Invoice Ref./Invoice Date</td><td>
        <asp:TextBox ID="txtInvoiceDt" runat="server"></asp:TextBox>&nbsp;<asp:Image ID="imgInvoice"
            runat="server" ImageUrl="~/Images/RedCalendar.gif" /> 
        <asp:CalendarExtender ID="CalendarExtender2" runat="server" Animated="true" Format="dd/MM/yyyy" PopupButtonID="imgInvoice" TargetControlID="txtInvoiceDt">
        </asp:CalendarExtender>
    </td> </tr>
    <tr><td>Payment Ref./Cheque Number</td><td>
        <asp:TextBox ID="txtChequeNo" runat="server"></asp:TextBox></td> 
    <td>Payment/Cheque Date</td><td>
        <asp:TextBox ID="txtChequeDate" runat="server"></asp:TextBox>&nbsp;<asp:Image ID="imgCheque"
            runat="server" ImageUrl="~/Images/RedCalendar.gif" /> 
        <asp:CalendarExtender ID="CalendarExtender1" runat="server" Animated="true" Format="dd/MM/yyyy" PopupButtonID="imgCheque" TargetControlID="txtChequeDate">
        </asp:CalendarExtender>
    </td> </tr>
    <tr><td>Payment Amount in INR</td><td>
        <asp:TextBox ID="txtChequeAmt" runat="server"></asp:TextBox></td>
        <td>Payment Currency</td><td colspan="3">
        <asp:DropDownList ID="ddlCurrency" runat="server">
        </asp:DropDownList></td></tr> 
     <tr id="trPayment"><td>Payment Amount (in Foreign Currency)</td> <td>
      <asp:TextBox ID="txtPayment" runat="server"></asp:TextBox></td> 
     <td>Exchange Rate</td> <td>
      <asp:TextBox ID="txtExRate" runat="server"></asp:TextBox></td></tr> 
    <tr><td>Year</td><td>
        <asp:DropDownList ID="ddlYear" runat="server">
        </asp:DropDownList></td>            
    <td>Country</td><td>
        <asp:DropDownList ID="ddlCountry" runat="server">
        </asp:DropDownList></td></tr>  
    <tr><td>Remarks</td><td>
        <asp:TextBox ID="txtRemark" runat="server"></asp:TextBox></td><td></td><td></td></tr>                          
     <tr align="center"><td colspan="4">
         <asp:ImageButton ID="imgBtnSubmit" runat="server" 
             ImageUrl="~/Images/Save2.png" onclick="imgBtnSubmit_Click" OnClientClick="return Verify()" />&nbsp;
         <asp:ImageButton ID="imgBtnClear" runat="server" 
             ImageUrl="~/Images/Clear2.png" onclick="imgBtnClear_Click" />
    </td></tr>                   
 </table>
</asp:Content>

