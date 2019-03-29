<%@ Page Title="" Language="C#" MasterPageFile="~/Parent.master" AutoEventWireup="true" CodeFile="Contract.aspx.cs" Inherits="Contract" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
 <title></title>
<link href="Styles/TableStyle2.css" rel="Stylesheet" type="text/css" />
<script src="Scripts/jquery-1.9.1.js" type="text/javascript"></script>
<script src="Scripts/jquery-ui-1.10.3.custom.min.js" type="text/javascript"></script>
<link href="Styles/jquery-ui-1.10.3.custom.min.css" rel="stylesheet" type="text/css" />
<script type="text/javascript">
$(document).ready(function(){
    $("table.neTable tr:even").addClass("tdTable");
    $("table.neTable tr:odd").addClass("tdTable1");

    $("#<%=txtParty.ClientID %>").autocomplete({
        source: function(request, response) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "Contract.aspx/GetCompanyName",
                data: "{'company':'" + $("#<%=txtParty.ClientID %>").val() + "'}",
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
function Verify() {
    var r = confirm('Are you sure; Do you want to insert this record?');
    if (r == true) {
        if (document.getElementById('<%=txtContractNo.ClientID%>').value == "") {
            alert('Agreement No. can not be empty');
            return false;
        }
        if (document.getElementById('<%=ddlAgreeType.ClientID%>').selectedIndex == 0 || document.getElementById('<%=ddlAgreeType.ClientID%>').selectedIndex == -1) {
            alert('Type of Agreement can not be empty');
            return false;
        }
        if (document.getElementById('<%=txtTitle.ClientID%>').value == "") {
            alert('Title can not be empty');
            return false;
        }
        if (document.getElementById('<%=txtScope.ClientID%>').value == "") {
            alert('Scope of Contract can not be empty');
            return false;
        }
        if (document.getElementById('<%=txtParty.ClientID%>').value == "") {
            alert('Contract Party can not be empty');
            return false;
        }
        if (document.getElementById('<%=ddlCoor.ClientID%>').selectedIndex == 0 || document.getElementById('<%=ddlCoor.ClientID%>').selectedIndex == -1) {
            alert('Coordinating Person can not be empty');
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
    <style type="text/css">
        .style1
        {
            width: 276px;
        }
        .style2
        {
            width: 173px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
<table align="center" class="neTable" cellpadding="12px" width="970px"  cellspacing="10px">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <tr><td colspan="4" align="center" valign="middle" style="font-size:medium; color:#994c00; text-decoration:underline;">
        CONTRACT FOLLOW UP</td></tr>
    <tr><td class="style1">Contract Number</td>
    <td><asp:TextBox ID="txtContractNo" BorderStyle="None" Font-Names="times new roman" ForeColor="#994c00" BackColor="#ffffff" Font-Bold="true" Font-Size="Large" Enabled="false" runat="server" Width="100px"></asp:TextBox></td>
    <td class="style2">Type of Agreement</td>
    <td><asp:DropDownList ID="ddlAgreeType" runat="server"></asp:DropDownList></td></tr>
    <tr><td class="style1">Title</td>
    <td colspan="3"><asp:TextBox ID="txtTitle" runat="server" TextMode="MultiLine" Width="750px"></asp:TextBox></td></tr>
    <tr><td class="style1">Scope of Contract</td>
    <td colspan="3"><asp:TextBox ID="txtScope" runat="server" TextMode="MultiLine" Width="750px"></asp:TextBox></td></tr>
    <tr><td class="style1">Contract Party</td>
    <td><asp:TextBox ID="txtParty" runat="server" Width="250px" 
            AutoCompleteType="Company"></asp:TextBox></td>
    <td class="style2">Coordinating Person
    <td><asp:UpdatePanel ID="UpdatePanel1" runat="server"><ContentTemplate>
    <asp:DropDownList ID="ddlDept" runat="server" AutoPostBack="true" 
        onselectedindexchanged="ddlDept_SelectedIndexChanged"></asp:DropDownList>
    <asp:DropDownList ID="ddlCoor" Width="119px" runat="server"></asp:DropDownList></ContentTemplate></asp:UpdatePanel>
    </td></tr>
   <%-- <tr><td class="style1">Effective Date</td>--%>
   <tr><td class="style1">Request Date</td>
    <td><asp:TextBox ID="txtRequest" runat="server" Width="100px"></asp:TextBox>&nbsp;<asp:Image ID="imgEffective" ImageUrl="~/Images/GreenCalendar.gif" runat="server" />
    <asp:CalendarExtender ID="CalendarExtender1" TargetControlID="txtRequest" PopupButtonID="imgEffective" runat="server" Format="dd/MM/yyyy">
    </asp:CalendarExtender></td>
    <%--<td class="style2">Expiry Date</td>--%>
    <td class="style2">Effective Date</td>
    <td><asp:TextBox ID="txtEffectiveDt" runat="server" Width="100px"> </asp:TextBox>&nbsp;<asp:Image ID="imgExpiry" ImageUrl="~/Images/GreenCalendar.gif" runat="server" />
    <asp:CalendarExtender ID="CalendarExtender2" TargetControlID="txtEffectiveDt" PopupButtonID="imgExpiry" runat="server" Format="dd/MM/yyyy">
    </asp:CalendarExtender></td></tr>
    <tr><td class="style1">Agreement No.</td>
    <td><asp:TextBox ID="txtAgmtNo" runat="server" Width="150px"></asp:TextBox></td>
    <td class="style2">Tech Transfer A/c</td>
    <td><asp:TextBox ID="txtTechTransfer" runat="server" Width="100px"></asp:TextBox></td>
    
    </tr>
    <tr><%--<td class="style1">Request</td>--%>
    <td class="style2">Expiry Date</td>
    <td><asp:TextBox ID="txtExpiryDt" runat="server" Width="100px"></asp:TextBox>&nbsp;<asp:Image ID="Image1" ImageUrl="~/Images/GreenCalendar.gif" runat="server" />
    <asp:CalendarExtender ID="CalendarExtender3" TargetControlID="txtExpiryDt" PopupButtonID="imgRequest" runat="server" Format="dd/MM/yyyy">
    </asp:CalendarExtender></td>
    <td class="style2">Remarks</td>
    <td><asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" Width="269px"></asp:TextBox></td>
    
    </tr>
    <tr><td>Status</td>
    <td><asp:DropDownList ID="dropstatus" runat="server">
      <%--<asp:ListItem>Draft-Party </asp:ListItem>
        <asp:ListItem>Draft-Legal</asp:ListItem>
        <asp:ListItem>Draft-IP Cell</asp:ListItem>
        <asp:ListItem>Executed-Subsisting</asp:ListItem>
        <asp:ListItem>Executed-Expired</asp:ListItem>
        <asp:ListItem>Executed-Rescinded</asp:ListItem>
        <asp:ListItem>Executed-Suspended</asp:ListItem>
        <asp:ListItem>Renewal Due</asp:ListItem>
        <asp:ListItem>To be amended</asp:ListItem>
        <asp:ListItem>Closed</asp:ListItem>--%>
        </asp:DropDownList></td></tr>
    <tr><td colspan="4">-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------</td>
  <tr><td colspan="4" align="center" valign="middle" style="font-size:medium; color:#994c00; text-decoration:underline;">
      LIST OF ACTION PLAN</td></tr>
    <tr><td>SLNO</td><td><asp:TextBox ID="txtslno" runat="server" Width="21px"></asp:TextBox></td>
     <td class="style2">Type</td>
    <td><asp:DropDownList ID="ddl1" runat="server" 
            onselectedindexchanged="ddl1_SelectedIndexChanged">
    <asp:ListItem></asp:ListItem>
        <asp:ListItem>Upfront Payment</asp:ListItem>
        <asp:ListItem>Annual</asp:ListItem>
        <asp:ListItem>Royalty</asp:ListItem>
        <asp:ListItem>Reimbursement</asp:ListItem>
        <asp:ListItem>Equity</asp:ListItem>
        <asp:ListItem>Part Payment</asp:ListItem>
        </asp:DropDownList>
    </td>
 </tr>
    <tr>
<td class="style2">Term</td>
<td><asp:DropDownList ID="ddl2" runat="server">
    <asp:ListItem></asp:ListItem>
    <asp:ListItem>Annual</asp:ListItem>
    <asp:ListItem>Once</asp:ListItem>
    <asp:ListItem>Quarterly</asp:ListItem>
    </asp:DropDownList></td>
    <td class="style2">DueDate</td>
<td><asp:TextBox ID="txtexecDt" runat="server" Width="100px" Height="22px"></asp:TextBox>
    &nbsp;<asp:Image ID="Image2" ImageUrl="~/Images/GreenCalendar.gif" runat="server" />
    <asp:CalendarExtender ID="CalendarExtender4" TargetControlID="txtexecDt" PopupButtonID="imgInvoice" runat="server" Format="dd/MM/yyyy">
    </asp:CalendarExtender></td>
</tr>
     <td class="style2">Basis<td><asp:TextBox ID="txtBasis" runat="server" Width="150px"></asp:TextBox></td>
    <td class="style1">Declaration Amount</td>
   <td><asp:TextBox ID="txtDeclAmt" runat="server" Width="150px" 
           ontextchanged="txtDeclAmt_TextChanged"></asp:TextBox></td>
</tr>
  <tr>
       <td class="style1">Invoice No</td>
       <td><asp:TextBox ID="txtInvoiceNo" runat="server" Width="150"></asp:TextBox></td>
       <td class="style2">Invoice Date</td>
   <td><asp:TextBox ID="txtInvRaised" runat="server" Width="150px"> </asp:TextBox>&nbsp;<asp:Image ID="Image3" ImageUrl="~/Images/GreenCalendar.gif" runat="server" />
    <asp:CalendarExtender ID="CalendarExtender5" TargetControlID="txtInvRaised" PopupButtonID="imgInvoice" runat="server" Format="dd/MM/yyyy">
    </asp:CalendarExtender></td>

    </tr>
    <tr><td>Receipt No</td>
     <td><asp:TextBox ID="txtReceiptNo" runat="server" Width="150px"></asp:TextBox></td>
     <td>Receipt Date</td>
     <td><asp:TextBox ID="txtRecieptDate" runat="server" Width="150px"></asp:TextBox>&nbsp;<asp:Image ID="Image4" ImageUrl="~/Images/GreenCalendar.gif" runat="server" />
    <asp:CalendarExtender ID="CalendarExtender6" TargetControlID="txtRecieptDate" PopupButtonID="imgInvoice" runat="server" Format="dd/MM/yyyy">
    </asp:CalendarExtender></td>
</tr>
<tr><td>Mode</td>
<td><asp:DropDownList ID="ddlMode" runat="server" Width="150">
 <asp:ListItem></asp:ListItem>
    <asp:ListItem>Cheque</asp:ListItem>
    <asp:ListItem>Wire Transfer</asp:ListItem>
   </asp:DropDownList></td>
<td class="style1">Amount in FC</td>
   <td><asp:TextBox ID="txtAmtInCF" runat="server" Width="150px"> </asp:TextBox></td></tr>
   <tr><td class="style2">Convertion Factor</td>
   <td><asp:TextBox ID="txtCF" runat="server" Width="150px"> </asp:TextBox>
</td>

<td class="style1">Amount In Rs</td>
<td><asp:TextBox ID="txtAmtInRs" runat="server" Width="150px"> </asp:TextBox>
</td></tr>
<tr><td class="style1">Status</td>
<td><asp:DropDownList ID="drop1" runat="server" Width="150">
 <asp:ListItem></asp:ListItem>
    <asp:ListItem>Received</asp:ListItem>
    <asp:ListItem>Pending</asp:ListItem>
   </asp:DropDownList>
</td>
<td class="style2">Remarks</td>
   <td><asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" Width="269px"></asp:TextBox></td></tr>
<tr><td></td><td align=center><asp:Button ID="button1" runat="server" Text="Insert" BackColor="DeepSkyBlue" 
        onclick="button1_Click"/>&nbsp;&nbsp;
   <asp:Button ID="button2" runat="server" Text="Clear" BackColor="DeepSkyBlue"/></td>
    <td class="style2"> 
        </td>
    <td>&nbsp;</td></tr>
<tr>

<td colspan="4" align="center">
 <asp:GridView ID="GV1" runat="server" AutoGenerateDeleteButton="True" 
        onrowdeleting="GV1_RowDeleting" Width="595px"></asp:GridView>
        
        </td>
        </tr>
   
<tr>
<td colspan="4" align="center">
  <asp:ImageButton ID="imgBtnInsert" ImageUrl="~/Images/Save3.png" runat="server" 
      OnClientClick="return Verify()" onclick="imgBtnInsert_Click" />&nbsp;
    <asp:ImageButton ID="imgBtnClear" runat="server" 
      ImageUrl="~/Images/Clear3.png" onclick="imgBtnClear_Click" />         
</td>
</tr>
</table>      
</asp:Content>

