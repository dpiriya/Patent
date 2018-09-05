<%@ Page Language="C#" MasterPageFile="~/Parent.master" AutoEventWireup="true" CodeFile="DocumentViewer.aspx.cs" Inherits="DocumentViewer" Title="Document Viewer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
<link href="Styles/newEntry.css" rel="Stylesheet" type="text/css" />
<script src="Scripts/jquery-1.9.1.js" type="text/javascript"></script>
<script src="Scripts/jquery-ui-1.10.3.custom.min.js" type="text/javascript"></script>
<link href="Styles/jquery-ui-1.10.3.custom.min.css" rel="stylesheet" type="text/css" />
<script type="text/javascript">
$(document).ready(function(){
    $("#<%=txtValue1.ClientID %>").hide();
    $("#<%=txtValue.ClientID %>").val('');
    $("#<%=lstColumn.ClientID %>").change(function(){
        $("#<%=txtValue.ClientID %>").val('');
        $("#<%=txtValue1.ClientID %>").val('');
        var fld= $("#<%=lstColumn.ClientID %>").val(); 
        if (fld=='FileNo')
        {
            $("#<%=lblHelp.ClientID %>").text('Enter File No.');
            $("#<%=txtValue.ClientID %>").show();
            $("#<%=txtValue1.ClientID %>").hide();
        }
        else if (fld=='EntryDt')
        {
            $("#<%=lblHelp.ClientID %>").text('Enter Upload Date (dd/mm/yyyy)');
            $("#<%=txtValue.ClientID %>").show();
            $("#<%=txtValue1.ClientID %>").show();
        }
        else if (fld=='ModifiedDt')
        {
            $("#<%=lblHelp.ClientID %>").text('Enter Modified date (dd/mm/yyyy)');
            $("#<%=txtValue.ClientID %>").show();
            $("#<%=txtValue1.ClientID %>").show();
        }
        else if (fld=='FileDescription')
        {
            $("#<%=lblHelp.ClientID %>").text('Enter File Type');
            $("#<%=txtValue.ClientID %>").show();
            $("#<%=txtValue1.ClientID %>").hide();
        }
    });
    
    $("#<%=btnAddVal.ClientID %>").click(function(){
        var fv=  $("#<%=txtValue.ClientID %>").val();
        var fv1=  $("#<%=txtValue1.ClientID %>").val();
        var fld= $("#<%=lstColumn.ClientID %>").val();
        var filterVal=$("#<%=txtFilter.ClientID %>").val(); 
        if (fv !='')
        {
            var filter="";
            if (fld == 'FileNo')
            {
                filter= fld + " = '" + fv +"'";
            }
            else if (fld == 'EntryDt')
            {
                if (fv1 !='')
                {
                    filter= fld + " between convert(smalldatetime,'" + fv +"',103) and convert(smalldatetime,'" + fv1 +"',103)";
                }
                else
                {
                    filter= fld + " = convert(smalldatetime,'" + fv +"',103)";
                }
            }
            else if (fld == 'ModifiedDt')
            {
                if (fv1 !='')
                {
                    filter= fld + " between convert(smalldatetime,'" + fv +"',103) and convert(smalldatetime,'" + fv1 +"',103)";
                }
                else
                {
                    filter= fld + " = convert(smalldatetime,'" + fv +"',103)";
                }
            }
            else if (fld == 'FileDescription')
            {
                filter= fld + " like '%" + fv +"%'";                
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

function AllowAlpha(e) {
    isIE = document.all ? 1 : 0
    keyEntry = !isIE ? e.which : e.keyCode;
    if (((keyEntry >= '48') && (keyEntry <= '57')) || ((keyEntry >= '65') && (keyEntry <= '90')) || ((keyEntry >= '97') && (keyEntry <= '122')) || (keyEntry <= '32'))
        return true;
    else
        return false;     
}
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
<h1 style="text-align:center; font-family:Times New Roman; font-size:16px; font-weight:bold">
    Document Search</h1>
<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
<table class="neTable" align="center" width="400">
<tr style="height:95px;vertical-align:top;"><td align="center">
    <asp:Label ID="Label2" runat="server" Text="Select Field Name"></asp:Label><br />
    <asp:ListBox ID="lstColumn" runat="server"></asp:ListBox>
</td><td>
    <asp:Label ID="Label3" runat="server" Text="Enter Field Value"></asp:Label><br /><asp:TextBox ID="txtValue" Width="200px" runat="server"></asp:TextBox><br />
    <asp:TextBox ID="txtValue1" Width="200px" runat="server"></asp:TextBox><br />
    <asp:Label ID="lblHelp" Font-Size="12px" Font-Bold="false" runat="server" Width="250px" ></asp:Label></td></tr>
<tr><td colspan="2" align="center">
    <asp:Button ID="btnAddVal" runat="server" Text="Add" 
         Width="70px" BackColor="Brown" ForeColor="Yellow" Font-Bold="true" Font-Names="Times new roman"/>
    <asp:Button ID="btnRemoveVal" runat="server" Text="Remove" 
        Width="70px" BackColor="Brown" ForeColor="Yellow" Font-Bold="true" 
        Font-Names="Times new roman" onclick="btnRemoveVal_Click" /></td></tr>
<tr><td colspan="2" align="center">
    <asp:TextBox ID="txtFilter" TextMode="MultiLine" Height="40px" Width="400px" 
        runat="server"></asp:TextBox> </td></tr>
<tr id="trFile" runat="server"><td colspan="2" align="center"> 
    <asp:Label ID="lblDownload" runat="server" Text="Download File Name"></asp:Label>&nbsp;&nbsp;<asp:TextBox ID="txtFileName" Width="200px" onkeypress="return AllowAlpha(event);" runat="server"></asp:TextBox>
    <br /><asp:Label ID="lblGroup" runat="server" Text="Group by File No"></asp:Label>&nbsp;&nbsp;<asp:CheckBox ID="chkGroup" runat="server" />    
</td></tr>
<tr><td colspan="2" align="center"> 
<asp:Button ID="btnReport" runat="server" BackColor="Brown" ForeColor="Yellow" 
        Font-Bold="true" Font-Names="Times new roman" Text="Report" 
        onclick="btnReport_Click"/>
<asp:Button ID="btnClear" runat="server" Text="Clear" BackColor="Brown" 
        ForeColor="Yellow" Font-Bold="true" Font-Names="Times new roman" 
        onclick="btnClear_Click" />
    <asp:Button ID="btnDownload" runat="server" BackColor="Brown" ForeColor="Yellow" 
    Font-Bold="true" Font-Names="Times new roman" Text="Download" 
     onclick="btnDownload_Click" />
</td></tr>        
</table>
<br /><br />
<table width="950" align="center" style="border:none">
 <tr><td align="left" style=" font-family:Arial;font-size:14px;font-weight:bold; text-decoration:underline;">
    <i id="SearchResult" visible="false" runat="server">Search Results</i></td></tr>
 <tr><td>
 <asp:GridView ID="gvFileDetails" AutoGenerateColumns="false" Width="950px"  
         CellPadding="5" CellSpacing="0" 
         Font-Names="Arial" BorderColor="#DEBA84" runat="server" 
         AllowSorting="true" AllowPaging="true" PageSize="25"
         onpageindexchanging="gvFileDetails_PageIndexChanging" 
         onselectedindexchanging="gvFileDetails_SelectedIndexChanging" 
         onsorting="gvFileDetails_Sorting" 
         onrowdatabound="gvFileDetails_RowDataBound" 
         onrowcommand="gvFileDetails_RowCommand" 
         onrowdeleting="gvFileDetails_RowDeleting">
 <Columns>
 <asp:BoundField DataField="EntryDt" HeaderStyle-Font-Underline="true" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Entry Dt." SortExpression="EntryDt"  />
 <asp:BoundField HeaderStyle-Font-Underline="true" DataField="FileNo" HeaderText="File No." SortExpression="FileNo"  />
 <asp:BoundField ItemStyle-Width="170" ItemStyle-Wrap="true" HeaderStyle-Font-Underline="true" DataField="FileDescription" HeaderText="File Description" SortExpression="FileDescription" />
 <asp:BoundField HeaderStyle-Font-Underline="true" DataFormatString="{0:dd/MM/yyyy}" DataField="ModifiedDt" SortExpression="ModifiedDt" HeaderText="Modified Dt." />
 <asp:BoundField ItemStyle-Width="120" ItemStyle-Wrap="true" HeaderStyle-Font-Underline="true" DataField="Comments" SortExpression="Comments" HeaderText="Comments" />
 <asp:TemplateField HeaderText="File Name">
 <ItemTemplate>
    <asp:LinkButton ID="lbtnEdit" Text='<%#Eval("FileName")%>' Font-Underline="true" CommandName="Select" runat="server"></asp:LinkButton> 
 </ItemTemplate> 
 </asp:TemplateField>
 <asp:TemplateField>
 <ItemTemplate>
    <asp:LinkButton ID="lbtnDelete" Text="Delete" OnClientClick="return Verify()" Visible="false" Font-Underline="true" CommandName="Delete" runat="server"></asp:LinkButton>
 </ItemTemplate>
 </asp:TemplateField>
 </Columns>
 <EmptyDataTemplate>
 <p style="text-align:center; background-color:#C8C8C8">
 <asp:Image ID="Image1" BorderStyle="None" BackColor="#C8C8C8" ImageUrl="~/Images/NoData.jpg" runat="server" />
 </p>
 </EmptyDataTemplate>
 <HeaderStyle ForeColor="#dffaa4" HorizontalAlign="Center" BackColor="#A55129" BorderStyle="Solid" BorderWidth="2" BorderColor="#A55129" VerticalAlign="Middle" Font-Size="13px" Height="35px" />
 <RowStyle BackColor="White" HorizontalAlign="Left" Height="25px" Font-Size="12px" Wrap="true"/>
 <AlternatingRowStyle Wrap="true" HorizontalAlign="Left" BackColor="#E9CEA4" />
 <PagerSettings Mode="Numeric" PageButtonCount="9" Position="TopAndBottom"/>
 <PagerStyle Font-Underline="true" Font-Bold="true" Font-Italic="true" ForeColor="Red"/>
 </asp:GridView> 
 <i id="pageNumber" visible="false" style="font-weight:bold" runat="server"> You are 
     viewing page <%=gvFileDetails.PageIndex + 1%> of <%= gvFileDetails.PageCount%></i> 
 </td></tr>
 </table> 
</asp:Content>

