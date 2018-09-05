<%@ Page Language="C#" MasterPageFile="~/Parent.master" AutoEventWireup="true" CodeFile="IP.aspx.cs" Inherits="IP" Title="International Patent" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">

<link href="Styles/newEntry.css" rel="Stylesheet" type="text/css" />
<script src="Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
<script src="Scripts/jquery-1.4.1.js" type="text/javascript"></script>

<script type="text/javascript">
$(document).ready(function(){
    $("table.neTable tr:even").addClass("tdTable");
    $("table.neTable tr:odd").addClass("tdTable1");    
});
function Verify()
{
    var r= confirm('Are you sure; Do you want to insert this record?');
    if (r==true)
    {
        var SelFileNo=document.getElementById('<%=ddlFileNo.ClientID%>');
        var SelVal= SelFileNo.options[SelFileNo.selectedIndex].value;
        if (SelVal== "")
        {
            alert('File No. can not be empty');
            return false;
        }
        if (document.getElementById('<%=txtSFileNo.ClientID%>').value== "")
        {
            alert('Sub File No. can not be empty');
            return false;
        }
        var SelCountry=document.getElementById('<%=ddlCountry.ClientID%>');
        var countryVal= SelCountry.options[SelCountry.selectedIndex].value;
        if (countryVal== "")
        {
            alert('Country can not be empty');
            return false;
        }
        var SelType=document.getElementById('<%=ddlType.ClientID%>');
        var TypeVal= SelType.options[SelType.selectedIndex].value;
        if (TypeVal== "")
        {
            alert('IPR Type can not be empty');
            return false;
        }
        return true;
    }
    else
    {
        return false;
    }
}
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <table align="center" class="neTable" cellpadding="12px" width="970px"  cellspacing="10px">
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
    <tr><td colspan="4" align="center" valign="middle" style="font-size:medium; text-decoration:underline;">
        INTERNATIONAL PATENT</td></tr>
    <tr><td>File Number</td>
        <td><asp:DropDownList ID="ddlFileNo" runat="server" 
                onselectedindexchanged="ddlFileNo_SelectedIndexChanged" AutoPostBack="true" EnableViewState="true">
            </asp:DropDownList>
        </td>
    <td>Sub File Number</td>
        <td><asp:TextBox ID="txtSFileNo" runat="server"></asp:TextBox>
    </td></tr>
    <tr> 
    <td>Request Date</td>
    <td><asp:TextBox ID="txtRequestDt" runat="server"></asp:TextBox>&nbsp;<asp:Image ID="imgRequestDt" ImageUrl="~/Images/RedCalendar.gif" runat="server" />
    <asp:CalendarExtender ID="CalendarExtender2" PopupButtonID="imgRequestDt" TargetControlID="txtRequestDt" runat="server" Format="dd/MM/yyyy">
        </asp:CalendarExtender></td>
    <td>Country</td>
        <td><asp:DropDownList ID="ddlCountry" runat="server"></asp:DropDownList></td></tr>
    <tr><td>Partner Name</td>
    <td><asp:TextBox ID="txtPartName" runat="server"></asp:TextBox></td>
    <td>Partner Reference Number</td>
    <td><asp:TextBox ID="txtPartRefNo" runat="server"></asp:TextBox></td></tr>
    <tr><td>Type</td>
    <td><asp:DropDownList ID="ddlType" runat="server"></asp:DropDownList></td>
    <td>Patent Attorney</td>
    <td><asp:DropDownList ID="ddlAttorney" runat="server"></asp:DropDownList></td></tr>
    <tr><td>Application Number</td>
    <td><asp:TextBox ID="txtAppNo" runat="server"></asp:TextBox></td>
    <td>Date of Filing</td>
    <td><asp:TextBox ID="txtDtFiling" runat="server"></asp:TextBox>&nbsp;<asp:Image ID="imgFilingDt" ImageUrl="~/Images/RedCalendar.gif" runat="server" />
    <asp:CalendarExtender ID="CalendarExtender13" PopupButtonID="imgFilingDt" TargetControlID="txtDtFiling" runat="server" Format="dd/MM/yyyy">
        </asp:CalendarExtender></td></tr>
    <tr><td>Publication Number</td>
    <td><asp:TextBox ID="txtPubNo" runat="server"></asp:TextBox></td>
    <td>Date of Publishing</td>
    <td><asp:TextBox ID="txtPubDt" runat="server"></asp:TextBox>&nbsp;<asp:Image ID="imgPubDt" ImageUrl="~/Images/RedCalendar.gif" runat="server" />
    <asp:CalendarExtender ID="CalendarExtender1" PopupButtonID="imgPubDt" TargetControlID="txtPubDt" runat="server" Format="dd/MM/yyyy">
        </asp:CalendarExtender></td></tr>
    <tr><td>Status</td>
        <td><asp:DropDownList ID="ddlStatus" runat="server" 
                onselectedindexchanged="ddlStatus_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></td>
    <td>Sub Status</td>
        <td><asp:DropDownList ID="ddlSubStatus" runat="server"></asp:DropDownList></td></tr>
    <tr><td>Patent Number</td>
    <td><asp:TextBox ID="txtPatNo" runat="server"></asp:TextBox></td>
    <td>Patent Issue Date</td>
    <td><asp:TextBox ID="txtPatDt" runat="server"></asp:TextBox>&nbsp;<asp:Image ID="imgPatDt" ImageUrl="~/Images/RedCalendar.gif" runat="server" />
    <asp:CalendarExtender ID="CalendarExtender3" PopupButtonID="imgPatDt" TargetControlID="txtPatDt" runat="server" Format="dd/MM/yyyy">
    </asp:CalendarExtender></td></tr>
    <tr><td>Remark</td>
    <td><asp:TextBox ID="txtRemark" runat="server"></asp:TextBox></td><td></td><td></td></tr>
    <tr>
      <td colspan="4" align="center">
          <asp:ImageButton ID="imgBtnInsert" runat="server" 
              ImageUrl="~/Images/Save3.png" onclick="imgBtnInsert_Click" OnClientClick="return Verify()" />&nbsp;
          <asp:ImageButton ID="imgBtnClear" runat="server" 
              ImageUrl="~/Images/Clear3.png" onclick="imgBtnClear_Click" />          
      </td>
      </tr>      
</table>
</asp:Content>

