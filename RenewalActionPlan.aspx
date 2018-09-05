<%@ Page Title="" Language="C#" MasterPageFile="~/Parent.master" AutoEventWireup="true" CodeFile="RenewalActionPlan.aspx.cs" Inherits="Default2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
    <link href="Styles/newEntry.css" rel="Stylesheet" type="text/css" />
<script src="Scripts/jquery-1.4.1.js" type="text/javascript"></script>
<script src="Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
<script src="Scripts/jquery.maskedinput-1.2.2.min.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
 <table>
   <tr><td align="center"> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
       <asp:GridView ID="GvReportDate" Width="80%" AutoGenerateColumns="false"  
            runat="server">
        <Columns> 
        <asp:BoundField HeaderStyle-Font-Underline="true" 
                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" 
                ItemStyle-Wrap="true" DataField="FileNo" HeaderText="FileNo" 
                SortExpression="FileNo" >
<HeaderStyle HorizontalAlign="Center" Font-Underline="True"></HeaderStyle>

<ItemStyle HorizontalAlign="Left" Wrap="True"></ItemStyle>
            </asp:BoundField>
        <asp:BoundField HeaderStyle-Font-Underline="true" 
                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" 
                ItemStyle-Wrap="true" DataField="Title" HeaderText="Title" 
                SortExpression="Title" >
<HeaderStyle HorizontalAlign="Center" Font-Underline="True"></HeaderStyle>

<ItemStyle HorizontalAlign="Left" Wrap="True"></ItemStyle>
            </asp:BoundField>
        <asp:BoundField HeaderStyle-Font-Underline="true" 
                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" 
                ItemStyle-Wrap="true" DataField="FirstApplicant" HeaderText="FirstApplicant" 
                SortExpression="FirstApplicant" >
<HeaderStyle HorizontalAlign="Center" Font-Underline="True"></HeaderStyle>

<ItemStyle HorizontalAlign="Left" Wrap="True"></ItemStyle>
            </asp:BoundField>
        <asp:BoundField HeaderStyle-Font-Underline="true" 
                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" 
                ItemStyle-Wrap="true" DataField="Inventor1" HeaderText="Inventor1" 
                SortExpression="Inventor1" >
<HeaderStyle HorizontalAlign="Center" Font-Underline="True"></HeaderStyle>

<ItemStyle HorizontalAlign="Left" Wrap="True"></ItemStyle>
            </asp:BoundField>
        <asp:BoundField HeaderStyle-Font-Underline="true" 
                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" 
                ItemStyle-Wrap="true" DataField="Applcn_no" HeaderText="Applcn_no" 
                SortExpression="Applcn_no" >
<HeaderStyle HorizontalAlign="Center" Font-Underline="True"></HeaderStyle>

<ItemStyle HorizontalAlign="Left" Wrap="True"></ItemStyle>
            </asp:BoundField>
        <asp:BoundField HeaderStyle-Font-Underline="true" 
                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" 
                ItemStyle-Wrap="true" DataField="Filing_dt" HeaderText="Filing_dt" 
                SortExpression="Filing_dt" >
<HeaderStyle HorizontalAlign="Center" Font-Underline="True"></HeaderStyle>

<ItemStyle HorizontalAlign="Left" Wrap="True"></ItemStyle>
            </asp:BoundField>
        <asp:BoundField HeaderStyle-Font-Underline="true" 
                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" 
                ItemStyle-Wrap="true" DataField="Pat_no" HeaderText="Pat_no" 
                SortExpression="Pat_no" >
<HeaderStyle HorizontalAlign="Center" Font-Underline="True"></HeaderStyle>

<ItemStyle HorizontalAlign="Left" Wrap="True"></ItemStyle>
            </asp:BoundField>
        <asp:BoundField HeaderStyle-Font-Underline="true" 
                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" 
                ItemStyle-Wrap="true" DataField="Pat_dt" HeaderText="Pat_dt" 
                SortExpression="Pat_dt" >
<HeaderStyle HorizontalAlign="Center" Font-Underline="True"></HeaderStyle>

<ItemStyle HorizontalAlign="Left" Wrap="True"></ItemStyle>
            </asp:BoundField>
        <asp:BoundField HeaderStyle-Font-Underline="true" 
                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" 
                ItemStyle-Wrap="true" DataField="Status" HeaderText="Status" 
                SortExpression="Status" >
<HeaderStyle HorizontalAlign="Center" Font-Underline="True"></HeaderStyle>

<ItemStyle HorizontalAlign="Left" Wrap="True"></ItemStyle>
            </asp:BoundField>
        <asp:BoundField HeaderStyle-Font-Underline="true" 
                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" 
                ItemStyle-Wrap="true" DataField="Sub_Status" HeaderText="Sub_Status" 
                SortExpression="Sub_Status" >
<HeaderStyle HorizontalAlign="Center" Font-Underline="True"></HeaderStyle>

<ItemStyle HorizontalAlign="Left" Wrap="True"></ItemStyle>
            </asp:BoundField>
        <asp:BoundField HeaderStyle-Font-Underline="true" 
                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" 
                ItemStyle-Wrap="true" DataField="Industry1" HeaderText="Industry1" 
                SortExpression="Industry1" >
<HeaderStyle HorizontalAlign="Center" Font-Underline="True"></HeaderStyle>

<ItemStyle HorizontalAlign="Left" Wrap="True"></ItemStyle>
            </asp:BoundField>
      <%--  <asp:BoundField HeaderStyle-Font-Underline="true" 
                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" 
                ItemStyle-Wrap="true" DataField="Industry2" HeaderText="Industry2" 
                SortExpression="Industry2" >
<HeaderStyle HorizontalAlign="Center" Font-Underline="True"></HeaderStyle>

<ItemStyle HorizontalAlign="Left" Wrap="True"></ItemStyle>
            </asp:BoundField>--%>
       <asp:BoundField HeaderStyle-Font-Underline="true" 
                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" 
                ItemStyle-Wrap="true" DataField="Commercialized" 
                HeaderText="Commercialized Status" SortExpression="Commercialized Status" >
<HeaderStyle HorizontalAlign="Center" Font-Underline="True"></HeaderStyle>

<ItemStyle HorizontalAlign="Left" Wrap="True"></ItemStyle>
            </asp:BoundField>
        </Columns>
        <HeaderStyle ForeColor="#dffaa4" HorizontalAlign="Center" BackColor="#A55129" BorderStyle="Solid" BorderWidth="2" BorderColor="#A55129" VerticalAlign="Middle" Font-Size="13px" Height="35px" />
    <RowStyle BackColor="White" HorizontalAlign="Left" Height="25px" Font-Size="12px" Wrap="true"/>
    <AlternatingRowStyle Wrap="true" HorizontalAlign="Left" BackColor="#E9CEA4" />
    <PagerSettings Mode="Numeric" PageButtonCount="9" Position="TopAndBottom"/>
    <PagerStyle Font-Underline="true" Font-Bold="true" Font-Italic="true" ForeColor="Red"/>
        </asp:GridView></td></tr>
        </table>
</asp:Content>


