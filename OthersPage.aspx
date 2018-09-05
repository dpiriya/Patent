<%@ Page Title="" Language="C#" MasterPageFile="~/Parent.master" AutoEventWireup="true" CodeFile="OthersPage.aspx.cs" Inherits="OthersPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
<link href="Styles/RptStyle.css" rel="Stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
<table id="tblReport">
<tr>
<td>
<p class="BoxTitle">Internship</p>
<p>&nbsp;&nbsp;&nbsp;&#9654<a href="Internship.aspx">New Internship</a></p>
<p>&nbsp;&nbsp;&nbsp;&#9654<a href="InternshipModification.aspx">Modify Internship</a></p>
<p>&nbsp;&nbsp;&nbsp;&#9654<a href="RptInternship.aspx?id=Internship">Internship Report</a></p>
</td>
<td>
<p class="BoxTitle">Forms</p>
<p>&nbsp;&nbsp;&nbsp;&#9654<a href="FilingConfirmation.aspx" style="text-align:left;">F01 - Filing Compliance Letter</a></p>
</td>
<td></td>
<td></td>
</tr>
</table>
</asp:Content>

