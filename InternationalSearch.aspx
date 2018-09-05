<%@ Page Language="C#" MasterPageFile="~/Parent.master" AutoEventWireup="true" CodeFile="InternationalSearch.aspx.cs" Inherits="InternationalSearch" Title="International Patent Search" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
<title>Action Tracker Search</title>
<link href="Styles/newEntry.css" rel="Stylesheet" type="text/css" />
<script src="Scripts/jquery-1.9.1.js" type="text/javascript"></script>
<script src="Scripts/jquery-ui-1.10.3.custom.min.js" type="text/javascript"></script>
<link href="Styles/jquery-ui-1.10.3.custom.min.css" rel="stylesheet" type="text/css" />

<script type="text/javascript">
$(document).ready(function(){
    $("#<%=lstColumn.ClientID %>").change(function(){
        $("#<%=txtValue.ClientID %>").val('');
        var fld= $("#<%=lstColumn.ClientID %>").val(); 
        if (fld=='InputDt')
        {$("#<%=lblHelp.ClientID %>").text('Enter Entry Date (DD/MM/YYYY)');}
        else if (fld=='FileNo')
        {$("#<%=lblHelp.ClientID %>").text('Enter File Number');}
        else if (fld=='subFileNo')
        {$("#<%=lblHelp.ClientID %>").text('Enter Sub File Number');}
        else if (fld=='RequestDt')
        {$("#<%=lblHelp.ClientID %>").text('Enter Request Date (DD/MM/YYYY)');}
        else if (fld=='Country')
        {$("#<%=lblHelp.ClientID %>").text('Enter Country Name');}
        else if (fld=='Partner')
        {$("#<%=lblHelp.ClientID %>").text('Enter Partner Name');}
        else if (fld=='PartnerNo')
        {$("#<%=lblHelp.ClientID %>").text('Enter Partner Ref. No.');}
        else if (fld=='Type')
        {$("#<%=lblHelp.ClientID %>").text('Enter Type');}
        else if (fld=='Attorney')
        {$("#<%=lblHelp.ClientID %>").text('Enter Attorney Name');}
        else if (fld=='ApplicationNo')
        {$("#<%=lblHelp.ClientID %>").text('Enter Application No.');}
        else if (fld=='FilingDt')
        {$("#<%=lblHelp.ClientID %>").text('Enter Filing date (DD/MM/YYYY)');}
        else if (fld=='PublicationNo')
        {$("#<%=lblHelp.ClientID %>").text('Enter Publication No.');} 
        else if (fld=='PublicationDt')
        {$("#<%=lblHelp.ClientID %>").text('Enter Publication Date (DD/MM/YYYY)');} 
        else if (fld=='Status')
        {$("#<%=lblHelp.ClientID %>").text('Enter Patent Status');} 
        else if (fld=='SubStatus')
        {$("#<%=lblHelp.ClientID %>").text('Enter Patent Sub Status');} 
        else if (fld=='PatentNo')
        {$("#<%=lblHelp.ClientID %>").text('Enter Patent No.');} 
        else if (fld=='PatentDt')
        {$("#<%=lblHelp.ClientID %>").text('Enter Patent Date (DD/MM/YYYY)');} 
        else if (fld=='Remark')
        {$("#<%=lblHelp.ClientID %>").text('Enter Remark');} 
    });    
});
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
<h1 style="text-align:center; font-family:Times New Roman; font-size:16px; font-weight:bold ">
    International Patent Search</h1>
<table class="neTable" align="center" width="400">
<tr style="height:95px;vertical-align:top;"><td align="center">
    <asp:Label ID="Label2" runat="server" Text="Select Field Name"></asp:Label><br />
    <asp:ListBox ID="lstColumn" runat="server"></asp:ListBox>
</td><td>
    <asp:Label ID="Label3" runat="server" Text="Enter Field Value"></asp:Label><br /><asp:TextBox ID="txtValue" Width="200px" Height="50px" TextMode="MultiLine" runat="server"></asp:TextBox><br />
    <asp:Label ID="lblHelp" Font-Size="12px" Font-Bold="false" runat="server" Width="250px" ></asp:Label></td></tr>
<tr><td colspan="2" align="center">
    <asp:Button ID="btnAddVal" runat="server" Text="Add" 
         Width="60px" BackColor="Brown" ForeColor="Yellow" Font-Bold="true" 
        Font-Names="Times new roman" onclick="btnAddVal_Click" />
    <asp:Button ID="btnRemoveVal" runat="server" Text="Remove" 
        Width="60px" BackColor="Brown" ForeColor="Yellow" Font-Bold="true" 
        Font-Names="Times new roman" onclick="btnRemoveVal_Click" /></td></tr>
<tr><td colspan="2" align="center">
    <asp:TextBox ID="txtFilter" TextMode="MultiLine" Height="100px" Width="400px" 
        runat="server"></asp:TextBox> </td></tr>
<tr><td colspan="2" align="center">Select Your Report Columns</td></tr>
<tr><td colspan="2" align="center">
<table width="410px">
<tr align="center" class=""><td>Table Column</td><td></td><td>Selected Column</td></tr>
<tr align="center"><td><asp:ListBox ID="lstTable" Width="150px" runat="server"></asp:ListBox>
</td><td valign="middle">
 <asp:Button ID="btnAdd" runat="server" Text=">" BackColor="Green" ForeColor="Black" 
            Font-Bold="true" Font-Names="Times new roman" onclick="btnAdd_Click" /><br />
 <asp:Button ID="btnRemove" runat="server" Text="<" BackColor="Brown" 
            ForeColor="Yellow" Font-Bold="true" Font-Names="Times new roman" 
            onclick="btnRemove_Click" /></td>
 <td><asp:ListBox ID="lstSelected" Width="150px" runat="server"></asp:ListBox></td></tr>
</table></td></tr>
<tr><td colspan="2" align="center"> 
<asp:Button ID="btnReport" runat="server" BackColor="Brown" ForeColor="Yellow" 
        Font-Bold="true" Font-Names="Times new roman" Text="Report" 
        onclick="btnReport_Click" />
<asp:Button ID="btnClear" runat="server" Text="Clear" BackColor="Brown" 
        ForeColor="Yellow" Font-Bold="true" Font-Names="Times new roman" 
        onclick="btnClear_Click" />
        </td></tr>
</table>
</asp:Content>

