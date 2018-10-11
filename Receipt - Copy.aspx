<%@ Page Language="C#" MasterPageFile="~/Parent.master" AutoEventWireup="true" CodeFile="Receipt - Copy.aspx.cs" Inherits="Receipt" Title="New Receipt Entry" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
<link href="Styles/newEntry.css" rel="Stylesheet" type="text/css" />
<script src="Scripts/jquery-1.9.1.js" type="text/javascript"></script>
<script src="Scripts/jquery-ui-1.10.3.custom.min.js" type="text/javascript"></script>
<link href="Styles/jquery-ui-1.10.3.custom.min.css" rel="stylesheet" type="text/css" />

<script type="text/javascript">
$(document).ready(function(){
    $("#trExRate").hide();
    $("table.neTable tr:even").addClass("tdTable");
    $("table.neTable tr:odd").addClass("tdTable1");
    $("#<%=ddlCurrency.ClientID %>").change(function(){
    var val1=$("#<%=ddlCurrency.ClientID %>").val();
    $("#<%=txtCost.ClientID %>").val("");
    $("#<%=txtExRate.ClientID %>").val("");
    if(val1 != "INR")
    {
        $("#trExRate").show();
    }
    else
    {
        $("#trExRate").hide();
    }
    });
    
    $("#<%=txtPartyName.ClientID %>").autocomplete({
    source: function(request, response) {
    $.ajax({
    type: "POST",
    contentType: "application/json; charset=utf-8",
    url: "Receipt.aspx/GetAutoCompleteData",
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
    url: "Receipt.aspx/GetTransTypeData",
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
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
<table align="center" class="neTable" cellpadding="12px" width="970px" cellspacing="10px">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <tr><td colspan="4" align="center" valign="middle" style="font-size:medium; text-decoration:underline;">
        NEW RECEIPT</td></tr>
   <tr><td>Receipt Ref No</td>
    <td> <asp:TextBox ID="txtrcpt" runat="server">
    </asp:TextBox> 
    </td>
     <td>File Number</td>
    <td> <asp:DropDownList ID="ddlFileNo" runat="server" AutoPostBack="true" 
            onselectedindexchanged="ddlFileNo_SelectedIndexChanged">
    </asp:DropDownList> 
    </td>       
   </tr>
   <tr>
 <td>Source</td><td>
    <asp:DropDownList ID="ddlsrc" runat="server"></asp:DropDownList>
    </td><td>Account no</td>
        <td><asp:TextBox ID="txtaccno" runat="server"></asp:TextBox></td>
    </tr>
    <tr>
    <td>Technology Transfer No.</td><td>
    <asp:TextBox ID="txtTechTransNo" runat="server"></asp:TextBox>
    </td><td>Party Name</td><td>
        <asp:TextBox ID="txtPartyName" runat="server"></asp:TextBox>
    </td>
        </tr>
    <tr>
    <td>Party Reference No.</td><td>
    <asp:TextBox ID="txtPartyRefNo" runat="server"></asp:TextBox>
    </td>

    <td>Intimation Reference</td><td>
        <asp:TextBox ID="txtTransType" runat="server"></asp:TextBox></td>
        </tr> 
    <tr>
    <td>Intimation Date</td><td>
        <asp:TextBox ID="txtSubDate" runat="server"></asp:TextBox>&nbsp;<asp:Image ID="imgSubmission"
            runat="server" ImageUrl="~/Images/RedCalendar.gif" /> 
        <asp:CalendarExtender ID="CalendarExtender1" runat="server" Animated="true" Format="dd/MM/yyyy" PopupButtonID="imgSubmission" TargetControlID="txtSubDate">
        </asp:CalendarExtender>
    </td><td>Transaction Description</td><td>
        <asp:TextBox ID="txtTransDesc" runat="server" TextMode="MultiLine" ></asp:TextBox></td>            
    <%--<td>Receipt Group</td><td>
        <asp:DropDownList ID="ddlPaymentGroup" runat="server">
        </asp:DropDownList></td>--%></tr> 
    <tr><td>Receipt Reference</td><td>
        <asp:TextBox ID="txtPaymentRef" runat="server"></asp:TextBox></td>           
    <td>Receipt Date</td><td>
        <asp:TextBox ID="txtPaymentDt" runat="server"></asp:TextBox>
        <asp:Image ID="imgPaymentDt" runat="server" ImageUrl="~/Images/RedCalendar.gif" /> 
        <asp:CalendarExtender ID="CalendarExtender2" runat="server" Animated="true" Format="dd/MM/yyyy" PopupButtonID="imgPaymentDt" TargetControlID="txtPaymentDt">
        </asp:CalendarExtender></td></tr>                          
    <tr><td>Receipt Description</td><td>
        <asp:TextBox ID="txtPaymentDesc" runat="server" TextMode="MultiLine" ></asp:TextBox></td>            
    <td>Receipt Currency</td><td>
        <asp:DropDownList ID="ddlCurrency" runat="server">
        </asp:DropDownList></td></tr>            
    <tr id="trExRate"><td>Exchange Rate</td><td>
        <asp:TextBox ID="txtExRate" runat="server"></asp:TextBox></td>            
    <td>Amount in Foreign Currency</td><td>
        <asp:TextBox ID="txtCost" runat="server"></asp:TextBox></td></tr>            
    <%--<td>Amount in INR</td><td>
        <asp:TextBox ID="txtCostINR" runat="server"></asp:TextBox></td>            --%>  
    <tr>          
        <td>Year</td><td>
        <asp:DropDownList ID="ddlYear" runat="server">
        </asp:DropDownList></td><td></td><td></td></tr>            
     <tr align="center"><td colspan="4">
         <asp:ImageButton ID="imgBtnSubmit" runat="server" 
             ImageUrl="~/Images/Save2.png" onclick="imgBtnSubmit_Click" />&nbsp;
         <asp:ImageButton ID="imgBtnClear" runat="server" 
             ImageUrl="~/Images/Clear2.png" onclick="imgBtnClear_Click" />
    </td></tr>                   
 </table>
</asp:Content>

