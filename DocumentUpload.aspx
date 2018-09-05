<%@ Page Language="C#" MasterPageFile="~/Parent.master" AutoEventWireup="true" CodeFile="DocumentUpload.aspx.cs" Inherits="DocumentUpload" Title="Document Upload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
<link href="Styles/newEntry.css" rel="Stylesheet" type="text/css" />
<script src="Scripts/jquery-1.9.1.js" type="text/javascript"></script>
<script src="Scripts/jquery-ui-1.10.3.custom.min.js" type="text/javascript"></script>
<link href="Styles/jquery-ui-1.10.3.custom.min.css" rel="stylesheet" type="text/css" />
<script type="text/javascript">
$(document).ready(function(){
    $("#<%=txtDocInfo.ClientID %>").hide(); 
    $("table.neTable tr:even").addClass("tdTable");
    $("table.neTable tr:odd").addClass("tdTable1");
    
    $("#<%=ddlDocInfo.ClientID %>").change(function(){
        var Info=$("#<%=ddlDocInfo.ClientID %>").val();
        if (Info == "Others")
        {
            
            $("#<%=txtDocInfo.ClientID %>").show(); 
            $("#<%=txtDocInfo.ClientID %>").val("");
        }
        else
        {
            $("#<%=txtDocInfo.ClientID %>").hide(); 
            $("#<%=txtDocInfo.ClientID %>").val("");
        }
    });
 });

function Verify()
{
    var r= confirm('Are you sure; Do you want to insert this record?');
    if (r==true)
    {
        if (document.getElementById('<%=ddlFileType.ClientID%>').value== "")
        {
            alert('Type of File can not be empty');
            return false;
        }
        if (document.getElementById('<%=ddlFileNo.ClientID%>').value== "")
        {
            alert('File No. can not be empty');
            return false;
        }
        if (document.getElementById('<%=ddlDocInfo.ClientID%>').value == "")
        {
            alert('File Type can not be empty');
            return false;
        }
        if (document.getElementById('<%=ddlDocInfo.ClientID%>').value == "Others" && document.getElementById('<%=txtDocInfo.ClientID%>').value == "" )
        {
            alert('File Type can not be empty');
            return false;
        }
        
     }
 }
 </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
<table align="center" class="neTable"  cellpadding="12px" cellspacing="10px">
    <tr><td colspan="2" align="center" valign="middle" style="font-size:medium; text-decoration:underline;">Document Upload</td></tr>
    <tr> 
        <td>Type of File</td>
        <td><asp:DropDownList ID="ddlFileType" runat="server" AutoPostBack="true" 
                onselectedindexchanged="ddlFileType_SelectedIndexChanged">
            </asp:DropDownList></td></tr>
    <tr> 
        <td>File Number</td><td><asp:DropDownList ID="ddlFileNo" runat="server">
            </asp:DropDownList></td></tr>
      <tr> 
        <td>File Type</td>          
        <td><asp:DropDownList ID="ddlDocInfo" runat="server">
          </asp:DropDownList><br />
          <asp:TextBox ID="txtDocInfo" runat="server" Width="240px"></asp:TextBox></td></tr>
      <tr> 
        <td>Uploading File</td> 
        <td>
            <asp:FileUpload ID="FileUpload1" runat="server" />
        </td></tr>
       <tr> 
        <td>Comments</td> 
        <td>
           <asp:TextBox ID="txtComment" TextMode="MultiLine" runat="server" Width="240px"></asp:TextBox> 
        </td></tr>
       <tr>
      <td colspan="2" align="center">
          <asp:ImageButton ID="imgBtnInsert" ImageUrl="~/Images/Save3.png" runat="server" 
               OnClientClick="return Verify()" onclick="imgBtnInsert_Click" />&nbsp;
          <asp:ImageButton ID="imgBtnClear" runat="server" 
              ImageUrl="~/Images/Clear3.png" onclick="imgBtnClear_Click" />         
      </td></tr>
 </table>
</asp:Content>

