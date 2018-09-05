<%@ Page Title="" Language="C#" MasterPageFile="~/Parent.master" AutoEventWireup="true" CodeFile="CompanySearch2.aspx.cs" Inherits="CompanySearch2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
    <link href="Styles/newEntry.css" rel="Stylesheet" type="text/css" />
<script src="Scripts/jquery-1.9.1.js" type="text/javascript"></script>
<script src="Scripts/jquery-ui-1.10.3.custom.min.js" type="text/javascript"></script>
<link href="Styles/jquery-ui-1.10.3.custom.min.css" rel="stylesheet" type="text/css" />
<script type="text/javascript">    
$(document).ready(function(){
    
});
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <table align="center" cellpadding="12px" cellspacing="10px" style="font-family:Arial; font-size:12px; font-weight:bold; " >
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
    <tr><td colspan="3" align="center" valign="middle" style=" font-family:Arial;font-size:16px;font-weight:bold;width:90%;color:Maroon; text-decoration:underline;">
        Company Search</td><td style="width:10%;">
        <asp:ImageButton ID="ibtnLetter" ImageUrl="~/Images/Notepad.jpg" ImageAlign="Left" 
                runat= "server" OnClientClick="" style="height: 24px" 
                onclick="ibtnLetter_Click"/> &nbsp;&nbsp;
        <asp:ImageButton ID="ibtnPrinter" ImageUrl="~/Images/Printer.gif" runat="server" OnClientClick="return Verify()" style="height: 24px" onclick="ibtnPrinter_Click"/></td></tr>
    <tr align="center"><td style="width:350px;" align="right"><strong>List of Search 
        Items</strong><br /><asp:DropDownList AutoPostBack="true" ID="ddlSearch" runat="server" 
            onselectedindexchanged="ddlSearch_SelectedIndexChanged"></asp:DropDownList></td>
        <td style="width:250px;"><strong>Search Text</strong><br /><asp:TextBox ID="txtSearch" Width="250px" runat="server"></asp:TextBox>
        <asp:DropDownList ID="ddlSearchVal" runat="server" Visible="false" ></asp:DropDownList>
        <asp:DropDownCheckBoxes ID="ddcboxSearchVal"  runat="server" Visible="false" >
        <Style2 SelectBoxWidth="300px" /></asp:DropDownCheckBoxes>
        </td>
        <td style="width:250px;" align="left"><br />
            <asp:ImageButton ID="ibtnSearch" ImageUrl="~/Images/search32.jpg"  
                runat="server" onclick="ibtnSearch_Click" /></td><td></td> 
        </tr>
        
     </table>    
     <table align="center">
     <tr><td align="left" style=" font-family:Arial;font-size:14px;font-weight:bold; text-decoration:underline;">
         Search Results</td></tr>
     <tr><td >
     <asp:GridView ID="gvCompany" AutoGenerateColumns="false" Width="930px" 
             CellPadding="3" CellSpacing="0"  
             Font-Names="Arial" BorderColor="#006633" runat="server" 
             onrowdatabound="gvCompany_RowDataBound" 
             onrowdeleting="gvCompany_RowDeleting" 
             onselectedindexchanging="gvCompany_SelectedIndexChanging" 
             AllowSorting="true" onsorting="gvCompany_Sorting" AllowPaging="true" 
             onpageindexchanging="gvCompany_PageIndexChanging" PageSize="10" 
             ondatabinding="gvCompany_DataBinding" >
     <Columns>
     <asp:BoundField DataField="EntryDt" DataFormatString="{0:dd/MM/yyyy}" SortExpression="EntryDt" HeaderText="Entry Dt." />
     <asp:BoundField DataField="CompanyID" HeaderText="Company ID" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="true" />
     <asp:BoundField DataField="CompanyName" HeaderText="Company Name" ItemStyle-HorizontalAlign="Left" SortExpression="CompanyName" ItemStyle-Wrap="true" />
     <asp:TemplateField HeaderText="Address" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="true">
     <ItemTemplate>
        <%#Eval("Address1") + "<br />" + Eval("Address2") + "<br />" + Eval("City") + " - " + Eval("Pincode")%>
     </ItemTemplate>
     </asp:TemplateField>
     <asp:TemplateField HeaderText="Contact Details" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="true">
     <ItemTemplate>
        <%# "Phone No. : " + Eval("Phone1") + "<br /> " + Eval("Phone2") + "<br /> Email ID : " + Eval("EmailID1") + "<br /> " + Eval("EmailID2")%>
     </ItemTemplate>
     </asp:TemplateField>
     <asp:BoundField ItemStyle-Wrap="true" DataField="IndustryType" SortExpression="IndustryType" HeaderText="IndustryType" />
     <asp:TemplateField HeaderText="Business Area & Products" ItemStyle-HorizontalAlign="Center"  ItemStyle-Wrap="true" >
     <ItemTemplate>
         <asp:LinkButton ID="lbtnView" Font-Underline="true" CommandName="delete" runat="server">View</asp:LinkButton>
         &nbsp;        
         <asp:LinkButton ID="lbtnPrint" Font-Underline="true" CommandName="select" runat="server">Print</asp:LinkButton>         
     </ItemTemplate>
     </asp:TemplateField>
     </Columns>
     <EmptyDataTemplate>
     <p style="text-align:center; background-color:#C8C8C8">
     <asp:Image ID="Image1" BorderStyle="None" BackColor="#C8C8C8" ImageUrl="~/Images/NoData.jpg" runat="server" />
     </p>
     </EmptyDataTemplate>
     <HeaderStyle HorizontalAlign="Center" BackColor="#006633" VerticalAlign="Middle" Font-Size="14px" Height="35px" />
     <RowStyle BackColor="White" Height="25px" Font-Size="13px" Wrap="true"/>
     <AlternatingRowStyle Wrap="true" BackColor="#FFFFCC" />
     <PagerSettings Position="TopAndBottom" Mode="NextPreviousFirstLast" FirstPageText="First" PreviousPageText="Previous" NextPageText="Next" LastPageText="Last" />
     <PagerStyle ForeColor="Black" Font-Bold="true" Font-Italic="true"  Font-Underline="true" HorizontalAlign="Center" />
     </asp:GridView>   
     <i>Your are viewing page <%=gvCompany.PageIndex+1 %> of <%=gvCompany.PageCount %></i>
     </td></tr></table>
</asp:Content>


