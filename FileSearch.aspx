﻿<%@ Page Language="C#" MasterPageFile="~/Parent.master" AutoEventWireup="true" CodeFile="FileSearch.aspx.cs" Inherits="FileSearch" Title="Indian Patent Search" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
<link href="Styles/newEntry.css" rel="Stylesheet" type="text/css" />
<script src="Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
<script src="Scripts/jquery-1.4.1.js" type="text/javascript"></script>
<script type="text/javascript">
$(document).ready(function(){
    $("#<%=lstColumn.ClientID %>").change(function(){
        $("#<%=txtValue.ClientID %>").val('');
        var fld= $("#<%=lstColumn.ClientID %>").val(); 
        if (fld=='EntryDt')
        {$("#<%=lblHelp.ClientID %>").text('Enter "Entry Date (DD/MM/YYYY)"');}
        else if (fld=='fileno')
        {$("#<%=lblHelp.ClientID %>").text('Enter File No.');}
        else if (fld=='title')
        {$("#<%=lblHelp.ClientID %>").text('Enter Patent Title');}
        else if (fld=='commercial')
        {$("#<%=lblHelp.ClientID %>").text('Enter commercial details');}
        else if (fld=='Assign_fileno')
        {$("#<%=lblHelp.ClientID %>").text('Enter IV File Number');}
        else if (fld=='FirstApplicant')
        {$("#<%=lblHelp.ClientID %>").text('Enter first applicant name');}
        else if (fld=='SecondApplicant')
        {$("#<%=lblHelp.ClientID %>").text('Enter second applicant name');}
        else if (fld=='department')
        {$("#<%=lblHelp.ClientID %>").text('Enter department name of Inventor1');}
        else if (fld=='department2')
        {$("#<%=lblHelp.ClientID %>").text('Enter department name of Inventor2');}
        else if (fld=='department3')
        {$("#<%=lblHelp.ClientID %>").text('Enter department name of Inventor3');}
        else if (fld=='department4')
        {$("#<%=lblHelp.ClientID %>").text('Enter department name of Inventor4');}
        else if (fld=='request_dt')
        {$("#<%=lblHelp.ClientID %>").text('Enter Request Date (DD/MM/YYYY)');}
        else if (fld=='InventionNo')
        {$("#<%=lblHelp.ClientID %>").text('Enter Invention Number');}
        else if (fld=='applcn_no')
        {$("#<%=lblHelp.ClientID %>").text('Enter Application Number');}
        else if (fld=='filing_dt')
        {$("#<%=lblHelp.ClientID %>").text('Enter filing date (DD/MM/YYYY)');}
        else if (fld=='Examination')
        {$("#<%=lblHelp.ClientID %>").text('Enter Examination Number');}
        else if (fld=='Exam_dt')
        {$("#<%=lblHelp.ClientID %>").text('Enter Examination date (DD/MM/YYYY)');}
        else if (fld=='publication')
        {$("#<%=lblHelp.ClientID %>").text('Enter publication Number');}
        else if (fld=='pub_dt')
        {$("#<%=lblHelp.ClientID %>").text('Enter Publication date (DD/MM/YYYY)');}
        else if (fld=='pat_no')
        {$("#<%=lblHelp.ClientID %>").text('Enter Patent Grant Number');}
        else if (fld=='pat_dt')
        {$("#<%=lblHelp.ClientID %>").text('Enter patent Grant date (DD/MM/YYYY)');}
        else if (fld=='validity_from_dt')
        {$("#<%=lblHelp.ClientID %>").text('Enter  Validity start date (DD/MM/YYYY)');}
        else if (fld=='validity_to_dt')
        {$("#<%=lblHelp.ClientID %>").text('Enter Validity end date (DD/MM/YYYY)');}
        else if (fld=='pct')
        {$("#<%=lblHelp.ClientID %>").text('Weather PCT filed or not (yes/No)');}
        else if (fld=='status')
        {$("#<%=lblHelp.ClientID %>").text('Enter Indian Patent Status');}
        else if (fld=='Sub_Status')
        {$("#<%=lblHelp.ClientID %>").text('Enter Indian Patent sub status');}
        else if (fld=='Inventor1')
        {$("#<%=lblHelp.ClientID %>").text('Enter Inventor1 name');}
        else if (fld=='Inventor2')
        {$("#<%=lblHelp.ClientID %>").text('Enter Inventor2 name');}
        else if (fld=='Inventor3')
        {$("#<%=lblHelp.ClientID %>").text('Enter Inventor3 name');}
        else if (fld=='Inventor4')
        {$("#<%=lblHelp.ClientID %>").text('Enter Inventor4 name');}
        else if (fld=='type')
        {$("#<%=lblHelp.ClientID %>").text('Enter Filing Type (eg: patent, copyright)');}
        else if (fld=='InitialFiling')
        {$("#<%=lblHelp.ClientID %>").text('Enter Initial Filing');}
        else if (fld=='Attorney')
        {$("#<%=lblHelp.ClientID %>").text('Enter Attorney name');}
        else if (fld=='industry1')
        {$("#<%=lblHelp.ClientID %>").text('Enter Industry1');}
        else if (fld=='industry2')
        {$("#<%=lblHelp.ClientID %>").text('Enter Industry2');}
        else if (fld=='abstract')
        {$("#<%=lblHelp.ClientID %>").text('Enter Abstract Details(Approved / Pending)');}
        else if (fld=='Commercialized')
        {$("#<%=lblHelp.ClientID %>").text('Weather commercialized or not (Yes/No)');}
        else if (fld=='PatentLicense')
        {$("#<%=lblHelp.ClientID %>").text('Enter name of company');}
        else if (fld=='TechTransNo')
        {$("#<%=lblHelp.ClientID %>").text('Enter Technology Transfer Number');}
        else if (fld=='remarks')
        {$("#<%=lblHelp.ClientID %>").text('Enter Remarks');}
    });
    $("#<%=btnAddVal.ClientID %>").click(function(){
        var fv=  $("#<%=txtValue.ClientID %>").val();
        var fld= $("#<%=lstColumn.ClientID %>").val();   
        var filterVal=$("#<%=txtFilter.ClientID %>").val(); 
        if (fv !='')
        {
            var filter="";
            if (fld != 'EntryDt' && fld != 'request_dt' && fld != 'filing_dt' && fld != 'Exam_dt' && fld != 'pub_dt' && fld != 'validity_from_dt' && fld != 'validity_to_dt' && fld != 'pat_dt')
            {
                filter= fld + " like '%" + fv +"%'";
            }
            else if (fld == 'EntryDt' || fld == 'request_dt' || fld == 'filing_dt' || fld == 'Exam_dt' || fld == 'pub_dt' || fld == 'validity_from_dt' || fld == 'validity_to_dt' || fld == 'pat_dt')
            {
                filter= fld + "= convert(smalldatetime,'" + fv +"',103)";
            }
            if (filterVal !='')
            {
                filterVal=filterVal + ' and ' + filter; 
            }
            else
            {
                filterVal=filter; 
            }
            $("#<%=txtFilter.ClientID %>").val(filterVal);
        }
    });
    
    $("table.neTable tr:even").addClass("tdTable");
    $("table.neTable tr:odd").addClass("tdTable1");
});
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
<h1 style="text-align:center; font-family:Times New Roman; font-size:16px; font-weight:bold ">
    Indian Patent Search</h1>
<table class="neTable" align="center" width="400">
<tr><td align="center">
    <asp:Label ID="Label1" runat="server" Text="Patent List"></asp:Label></td><td>
    <asp:RadioButton ID="RdoAll" Text="All Patents" GroupName="commercial"  Checked="true" runat="server" /><br />
    <asp:RadioButton ID="RdoIITM" Text="Only IIT Madras" GroupName="commercial"  runat="server" /><br /><asp:RadioButton ID="RdoIV" Text="Only Commercial"
        runat="server" GroupName="commercial" /></td></tr>
<tr style="height:95px;vertical-align:top;"><td align="center">
    <asp:Label ID="Label2" runat="server" Text="Select Field Name"></asp:Label><br />
    <asp:ListBox ID="lstColumn" runat="server"></asp:ListBox>
</td><td>
    <asp:Label ID="Label3" runat="server" Text="Enter Field Value"></asp:Label><br /><asp:TextBox ID="txtValue" Width="200px" Height="50px" TextMode="MultiLine" runat="server"></asp:TextBox><br />
    <asp:Label ID="lblHelp" Font-Size="12px" Font-Bold="false" runat="server" Width="250px" ></asp:Label></td></tr>
<tr><td colspan="2" align="center">
    <asp:Button ID="btnAddVal" runat="server" Text="Add" 
         Width="70px" BackColor="Brown" ForeColor="Yellow" Font-Bold="true" Font-Names="Times new roman"/>
    <asp:Button ID="btnRemoveVal" runat="server" Text="Remove" 
        onclick="btnRemoveVal_Click" Width="70px" BackColor="Brown" ForeColor="Yellow" Font-Bold="true" Font-Names="Times new roman" /></td></tr>
<tr><td colspan="2" align="center">
    <asp:TextBox ID="txtFilter" TextMode="MultiLine" Height="40px" Width="400px" 
        runat="server"></asp:TextBox> </td></tr>
<tr><td colspan="2" align="center">Select Your Report Columns</td></tr>
<tr><td colspan="2" align="center">
<table width="410px">
<tr align="center" class=""><td>Table Column</td><td></td><td>Selected Column</td></tr>
<tr align="center"><td><asp:ListBox ID="lstTable" Width="150px" runat="server"></asp:ListBox>
</td><td valign="middle">
 <asp:Button ID="btnAdd" runat="server" Text=">" onclick="btnAdd_Click" BackColor="Green" ForeColor="Black" Font-Bold="true" Font-Names="Times new roman" /><br />
 <asp:Button ID="btnRemove" runat="server" Text="<" onclick="btnRemove_Click" BackColor="Brown" ForeColor="Yellow" Font-Bold="true" Font-Names="Times new roman" /></td>
 <td><asp:ListBox ID="lstSelected" Width="150px" runat="server"></asp:ListBox></td></tr>
</table></td></tr>
<tr><td colspan="2" align="center"> 
<asp:Button ID="btnReport" runat="server" BackColor="Brown" ForeColor="Yellow" Font-Bold="true" Font-Names="Times new roman" Text="Report" onclick="btnReport_Click" />
<asp:Button ID="btnClear" runat="server" Text="Clear" onclick="btnClear_Click" BackColor="Brown" ForeColor="Yellow" Font-Bold="true" Font-Names="Times new roman" /></td></tr>
</table>
</asp:Content>

