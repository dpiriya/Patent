<%@ Page Title="" Language="C#" MasterPageFile="~/Parent.master" AutoEventWireup="true" CodeFile="NewRenewal.aspx.cs" Inherits="Default2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>



<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
    <title></title>
<link href="Styles/TableStyle2.css" rel="Stylesheet" type="text/css" />
<script src="Scripts/jquery-1.9.1.js" type="text/javascript"></script>
<script src="Scripts/jquery-ui-1.10.3.custom.min.js" type="text/javascript"></script>
<link href="Styles/jquery-ui-1.10.3.custom.min.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style2
        {
            width: 173px;
        }
        .style3
        {
            width: 206px;
        }
        .style4
        {
            width: 219px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
<div id="Divrenewal" runat="server">
<table>
<tr>
<td align="center"  colspan="4" valign="middle" style="font-size:medium; color:#994c00;"> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; P&M Followup</td></tr></table>
    
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <br />
    <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    
    <tr><td>Entry Type: &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td><td><asp:RadioButton ID="radioSingle" 
        Text="Single Entry" runat="server" GroupName="Entry" 
        oncheckedchanged="radioSingle_CheckedChanged" /> </td>
        <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:RadioButton ID="radioBulk" GroupName="Entry" runat="server" Text="Block Entry" /></td>
        <td></td></tr>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="BtnSubmit" Text="Submit" runat="server" 
                onclick="BtnSubmit_Click" />
                </div>
          <div id="divrenewalEntry" runat="server">      
    
        <table align="center" class="neTable" cellpadding="12px" width="970px"  cellspacing="10px">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <tr><td colspan="4" align="center" valign="middle" style="font-size:medium; color:#994c00; text-decoration:underline;">
        P&M Followup</td>
        
        </tr>
    <tr><td class="style4">IDF NO</td>
    <td><asp:DropDownList ID="ddlIDFNo" runat="server"></asp:DropDownList>
            <asp:Button ID="btnFind" runat="server" Text="Find" 
            onclick="btnFind_Click" /></td>
    </tr>
    <tr><td class="style4">Title</td>
    <td colspan="3"><asp:TextBox ID="txttitle" runat="server" TextMode="MultiLine" 
            Width="495px" ></asp:TextBox></td>
    </tr>
    <tr><td class="style4">Applicant</td>
    <td><asp:TextBox ID="txtApplicant" runat="server" Width="215px" Height="24px"></asp:TextBox></td>
    <td>Inventor</td>
    <td><asp:TextBox ID="txtInventor" runat="server" Width="215px"></asp:TextBox></td></tr>
   <tr><td class="style4">Application No (Indian)</td>
   <td><asp:TextBox ID="txtApplnNo" runat="server" Width="150px"></asp:TextBox></td>
       <td>Application No(International)</td>
    <td><asp:TextBox ID="txtApllicationNo" runat="server" Width="150px"></asp:TextBox></td>
  
        </tr>
        <tr><td class="style4">Filing Date (Indian)</td>
        <td><asp:TextBox ID="txtFilingDt" runat="server" Width="150px"></asp:TextBox></td>
    
<td>Filing Date(International)</td>
    <td><asp:TextBox ID="txtFilingDtInt" runat="server" Width="150px"></asp:TextBox></td></tr>
    <tr>
     <td class="style4">Patent No</td>
     <td><asp:TextBox ID="txtPatentNo" runat="server" Width="150px"></asp:TextBox></td>
     <td>Patent Date</td>
     <td><asp:TextBox ID="txtPatentDate" runat="server" Width="150px"></asp:TextBox></td></tr>
     <tr><td class="style4">Country</td>
     <td><asp:TextBox ID="txtCountry" runat="server" Width="150px"></asp:TextBox></td>
     <td class="style4">Status</td>
     <td><asp:TextBox ID="txtStatus" runat="server" Width="150px"></asp:TextBox></td>
  <tr>
  <td class="style4">Sub-Status</td>
     <td><asp:TextBox ID="txtSubStatus" runat="server" Width="215px" Height="22px"></asp:TextBox></td>
    <td class="style4"><asp:TextBox ID="textFile" runat="server" Width="93px" 
            Visible=false></asp:TextBox></td></tr>
    <tr><td colspan="4">-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------</td></tr>
   <tr><td colspan=1 class="style4">SLNo</td><td><asp:TextBox ID="txtSlno" runat="server" 
           Width="38px" Height="22px"></asp:TextBox></td><td class="style3">Description</td>
       <td>
        <asp:TextBox ID="txtDescription" 
            runat="server" TextMode="MultiLine" 
            Width="249px"></asp:TextBox></td></tr><tr><td class="style4">Amount In FC</td><td><asp:TextBox ID="txtAmtInFc" runat="server" Width="100px"></asp:TextBox></td><td class="style2">Amount In INR</td><td><asp:TextBox ID="txtAmtInINR" runat="server" Width="100px"></asp:TextBox></td></tr><tr>
        <td class="style4">Due Date</td><td><asp:TextBox ID="txtDueDate" runat="server" Width="100px"></asp:TextBox>&nbsp;<asp:Image ID="Image2" ImageUrl="~/Images/GreenCalendar.gif" runat="server" />
    <asp:CalendarExtender ID="CalendarExtender4" TargetControlID="txtDueDate" PopupButtonID="imgExpiry" runat="server" Format="dd/MM/yyyy">
    </asp:CalendarExtender></td>
    <td class="style2">Amount(OnCondonation)</td><td><asp:TextBox ID="txtAmtOnCond" runat="server" Width="100px"></asp:TextBox></td><tr>
        <td class="style4">Due Date(Restoration)</td><td><asp:TextBox ID="txtDueDateResto" runat="server" Width="100px"></asp:TextBox>&nbsp;<asp:Image ID="imgEffective" ImageUrl="~/Images/GreenCalendar.gif" runat="server" />
    <asp:CalendarExtender ID="CalendarExtender1" TargetControlID="txtDueDateResto" PopupButtonID="imgEffective" runat="server" Format="dd/MM/yyyy">
    </asp:CalendarExtender></td>

    <td class="style2">Responsiblity</td><td><asp:DropDownList ID="ddlResponsiblity" runat="server">
        <asp:ListItem>--Select--</asp:ListItem><asp:ListItem>IITM</asp:ListItem><asp:ListItem>3rd Party</asp:ListItem><asp:ListItem>Licencee</asp:ListItem></asp:DropDownList></td></tr><tr>
            <td class="style4">Sharing Party</td><td><asp:TextBox ID="txtSharingParty" runat="server" Width="150px"></asp:TextBox></td><td class="style2">Percentage(%)</td><td><asp:TextBox ID="txtPercentage" runat="server" Width="100px"></asp:TextBox></td></tr><tr><%--<td class="style1">Request</td>--%>
    <td class="style4">Intimation Date</td><td><asp:TextBox ID="txtIntimationDt" runat="server" Width="100px"></asp:TextBox>&nbsp;<asp:Image ID="Image1" ImageUrl="~/Images/GreenCalendar.gif" runat="server" />
    <asp:CalendarExtender ID="CalendarExtender3" TargetControlID="txtIntimationDt" PopupButtonID="imgRequest" runat="server" Format="dd/MM/yyyy">
    </asp:CalendarExtender></td>
    <td class="style2">Confirm Date</td><td><asp:TextBox ID="txtConfirmDt" runat="server" Width="100px"></asp:TextBox>&nbsp;<asp:Image ID="Image3" ImageUrl="~/Images/GreenCalendar.gif" runat="server" />
    <asp:CalendarExtender ID="CalendarExtender2" TargetControlID="txtConfirmDt" PopupButtonID="imgRequest" runat="server" Format="dd/MM/yyyy">
    </asp:CalendarExtender></td></tr>
    <tr><td class="style4">Share Received </td>
    <td><asp:DropDownList ID="ddlShare" runat="server">
        <asp:ListItem>--Select--</asp:ListItem>
        <asp:ListItem>Yes</asp:ListItem>
        <asp:ListItem>No</asp:ListItem>
        <asp:ListItem>NA</asp:ListItem>
        </asp:DropDownList></td><td>PO PaymentDate</td><td><asp:TextBox ID="txtPaymentDate" runat="server" Width="100px"></asp:TextBox>&nbsp;<asp:Image ID="Image4" ImageUrl="~/Images/GreenCalendar.gif" runat="server" />
    <asp:CalendarExtender ID="CalendarExtender5" TargetControlID="txtPaymentDate" PopupButtonID="imgRequest" runat="server" Format="dd/MM/yyyy">
    </asp:CalendarExtender></td>
    </tr>
    <tr><td class="style4">Status</td><td><asp:DropDownList ID="dropstatus" runat="server">
     <asp:ListItem>--Select--</asp:ListItem><asp:ListItem>Renewed</asp:ListItem><asp:ListItem>Not Renewed</asp:ListItem><asp:ListItem>Surrender</asp:ListItem><asp:ListItem>Pending</asp:ListItem><asp:ListItem>Complete</asp:ListItem></asp:DropDownList></td></tr><tr><td colspan="4" align="center">
  <asp:ImageButton ID="imgBtnInsert" ImageUrl="~/Images/Save3.png" runat="server" 
      OnClientClick="return Verify()" onclick="imgBtnInsert_Click" />&nbsp;
    <asp:ImageButton ID="imgBtnClear" runat="server" 
      ImageUrl="~/Images/Clear3.png" onclick="imgBtnClear_Click" />         
</td></tr>
</div>
    
<div id="divBlock" runat="server">
<table>
 <br />
    <br />
    <br />
    <br />
<tr>
<td> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
   
    IDF No</td><td><asp:DropDownList ID="dropIDF" runat="server"></asp:DropDownList></td><td>
    <asp:Button ID="buttonSubmit" Text="Submit" runat="server" 
        onclick="buttonSubmit_Click" /></td></tr>
        </table>
        </div>
</asp:Content>

