<%@ Page Language="C#" MasterPageFile="~/Parent.master" AutoEventWireup="true" CodeFile="DueDiligenceSearch.aspx.cs" Inherits="Default2" Title="Untitled Page" %>

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
        else if (fld=='FileNo')
        {$("#<%=lblHelp.ClientID %>").text('Enter File No.');}
        else if (fld == 'InventorType')
        { $("#<%=lblHelp.ClientID%>").text('Enter Inventor Type');}             
        else if (fld=='InventionID')
        { $("#<%=lblHelp.ClientID %>").text('Enter Invention ID'); }
        else if (fld == 'InventorName')
        { $("#<%=lblHelp.ClientID %>").text('Enter Inventor name'); }
        else if (fld=='DeptCode')
        {$("#<%=lblHelp.ClientID %>").text('Enter Department Code');}            
     
   
    });
    $("#<%=btnAddVal.ClientID %>").click(function(){
        var fv=  $("#<%=txtValue.ClientID %>").val();
        var fld= $("#<%=lstColumn.ClientID %>").val();   
        var filterVal=$("#<%=txtFilter.ClientID %>").val(); 
        if (fv !='')
        {
            var filter="";
            if (fld != 'EntryDt')
            {
                filter= fld + " like '%" + fv +"%'";
            }
            else if (fld == 'EntryDt')
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
    Duediligence Search</h1>
<table class="neTable" align="center" width="400">
<tr style="height:95px;vertical-align:top;"><td align="center">
    <asp:Label ID="Label2" runat="server" Text="Select Field Name"></asp:Label><br />
    <asp:ListBox ID="lstColumn" runat="server"></asp:ListBox>
</td><td>
    <asp:Label ID="Label3" runat="server" Text="Enter Field Value"></asp:Label><br /><asp:TextBox ID="txtValue" Width="200px" Height="50px" TextMode="MultiLine" runat="server"></asp:TextBox><br />
    <asp:Label ID="lblHelp" Font-Size="12px" Font-Bold="false" runat="server" Width="250px" ></asp:Label></td></tr>
<tr><td colspan="2" align="center">
    <asp:Button ID="btnAddVal" runat="server" Text="Add" 
         Width="70px" BackColor="Brown" ForeColor="Yellow" Font-Bold="true" 
        Font-Names="Times new roman"/>
    <asp:Button ID="btnRemoveVal" runat="server" Text="Remove" 
        Width="70px" BackColor="Brown" ForeColor="Yellow" Font-Bold="true" Font-Names="Times new roman" /></td></tr>
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
<asp:Button ID="btnClear" runat="server" Text="Clear" BackColor="Brown" ForeColor="Yellow" Font-Bold="true" Font-Names="Times new roman" /></td></tr>
</table>
</asp:Content>


