<%@ Page Language="C#" MasterPageFile="~/Parent.master" AutoEventWireup="true" CodeFile="PaymentModify.aspx.cs" Inherits="PaymentModify" Title="Patent Payment Modification" %>
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
        $("#trExRate").show();
    }
    else
    {
        $("#trPayment").hide();
        $("#trExRate").hide();
    }
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
    <tr align="center" valign="middle" style="font-size:14px; font-family:Arial;text-decoration:underline;"><th>List of Payments</th><th>Payment Modification</th></tr>
    <tr><td valign="top">
        <asp:GridView ID="gvPayment" AutoGenerateColumns="false" 
         CellPadding="5" CellSpacing="0" 
         Font-Names="Arial" BorderColor="#DEBA84" runat="server" 
         AllowSorting="true" AllowPaging="true" PageSize="15" 
            onpageindexchanging="gvPayment_PageIndexChanging" 
            onrowdeleting="gvPayment_RowDeleting" 
            onselectedindexchanging="gvPayment_SelectedIndexChanging" 
            onsorting="gvPayment_Sorting" onrowdatabound="gvPayment_RowDataBound">
 <Columns>
 <asp:BoundField DataField="slno" HeaderStyle-Font-Underline="true" HeaderText="Sl. No." SortExpression="slno"  />
 <asp:BoundField ItemStyle-Width="150" HeaderStyle-Font-Underline="true" DataField="activity" HeaderText="Activity" SortExpression="activity"  />
 <asp:BoundField ItemStyle-Width="90" ItemStyle-Wrap="true" HeaderStyle-Font-Underline="true" DataField="party" HeaderText="Party" SortExpression="party" />
 <asp:BoundField HeaderStyle-Font-Underline="true" DataField="PaymentAmtINR" SortExpression="PaymentAmtINR" HeaderText="Payment" />
 <asp:TemplateField>
 <ItemTemplate>
 <asp:LinkButton ID="lbtnEdit" Font-Underline="true" CommandName="Select" runat="server">Edit</asp:LinkButton>
 <asp:LinkButton ID="lbtnDelete" OnClientClick="return Verify()" Font-Underline="true" CommandName="Delete" runat="server">Delete</asp:LinkButton>
 </ItemTemplate>
 </asp:TemplateField>
 </Columns>
 <HeaderStyle ForeColor="#dffaa4" HorizontalAlign="Center" BackColor="#A55129" BorderStyle="Solid" BorderWidth="2" BorderColor="#A55129" VerticalAlign="Middle" Font-Size="13px" Height="35px" />
 <RowStyle BackColor="White" HorizontalAlign="Left" Height="25px" Font-Size="12px" Wrap="true"/>
 <AlternatingRowStyle Wrap="true" HorizontalAlign="Left" BackColor="#EECCAA" />
 <PagerSettings Mode="Numeric" PageButtonCount="9" Position="TopAndBottom"/>
 <PagerStyle Font-Underline="true" Font-Bold="true" Font-Italic="true" ForeColor="Red"/>
 </asp:GridView> 
 <i id="pageNumber" visible="false" style="font-weight:bold" runat="server"> You are 
     viewing page <%=gvPayment.PageIndex + 1%> of <%= gvPayment.PageCount%></i> 
    </td>
    <td valign="top">
    <table align="center" class="neTable" cellpadding="12px"  cellspacing="10px">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <tr><td>File No</td>
    <td><asp:Label ID="lblFileNo" runat="server"  EnableViewState="true"></asp:Label></td></tr>
    <tr><td>Serial No</td>
    <td><asp:Label ID="lblSerial" runat="server"></asp:Label></td></tr>
    <tr><td>Cost Group</td><td>
        <asp:DropDownList ID="ddlCostGrp" runat="server">
        </asp:DropDownList>
    </td></tr>
    <tr><td>Payment Description</td><td>
        <asp:TextBox ID="txtDescription" Width="250px" runat="server" TextMode="MultiLine"></asp:TextBox></td></tr> 
    <tr><td>Party Group</td><td>
        <asp:DropDownList ID="ddlPMode" runat="server">
        </asp:DropDownList>
    </td></tr>
    <tr><td>Party Name</td><td>
        <asp:DropDownList ID="ddlParty" runat="server">
        </asp:DropDownList>
    </td></tr>
    <tr><td>Invoice Ref./Invoice Number</td><td>
        <asp:TextBox ID="txtInvoice" runat="server"></asp:TextBox></td></tr>
      <tr><td>Invoice Ref./Invoice Date</td><td>
        <asp:TextBox ID="txtInvoiceDt" runat="server"></asp:TextBox>&nbsp;<asp:Image ID="imgInvoice"
            runat="server" ImageUrl="~/Images/RedCalendar.gif" /> 
        <asp:CalendarExtender ID="CalendarExtender2" runat="server" Animated="true" Format="dd/MM/yyyy" PopupButtonID="imgInvoice" TargetControlID="txtInvoiceDt">
        </asp:CalendarExtender>
    </td></tr>
    <tr><td align="left">Payment Ref./Cheque Number</td><td>
        <asp:TextBox ID="txtChequeNo" runat="server"></asp:TextBox></td></tr> 
    <tr><td>Payment/Cheque Date</td><td>
        <asp:TextBox ID="txtChequeDate" runat="server"></asp:TextBox>&nbsp;<asp:Image ID="imgCheque"
            runat="server" ImageUrl="~/Images/RedCalendar.gif" /> 
        <asp:CalendarExtender ID="CalendarExtender1" runat="server" Animated="true" Format="dd/MM/yyyy" PopupButtonID="imgCheque" TargetControlID="txtChequeDate">
        </asp:CalendarExtender>
    </td></tr> 
    <tr><td>Payment Amount in INR</td><td>
        <asp:TextBox ID="txtChequeAmt" runat="server"></asp:TextBox></td></tr> 
    <tr><td>Payment Currency</td><td>
        <asp:DropDownList ID="ddlCurrency" runat="server">
        </asp:DropDownList></td></tr> 
     <tr id="trPayment" runat="server"><td>Payment Amount (in Foreign Currency)</td> <td>
      <asp:TextBox ID="txtPayment" runat="server"></asp:TextBox></td></tr> 
     <tr id="trExRate" runat="server"><td>Exchange Rate</td> <td>
      <asp:TextBox ID="txtExRate" runat="server"></asp:TextBox></td></tr>            
    <tr><td>Year</td><td>
        <asp:DropDownList ID="ddlYear" runat="server">
        </asp:DropDownList></td></tr>            
    <tr><td>Country</td><td>
        <asp:DropDownList ID="ddlCountry" runat="server">
        </asp:DropDownList></td></tr>            
    <tr><td>Remarks</td><td>
        <asp:TextBox ID="txtRemark" runat="server"></asp:TextBox></td></tr>            
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

