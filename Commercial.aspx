<%@ Page Language="C#" MasterPageFile="~/Parent.master" AutoEventWireup="true" CodeFile="Commercial.aspx.cs" Inherits="Commercial" Title="Commercialization" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
<link href="Styles/newEntry.css" rel="Stylesheet" type="text/css" />
<script src="Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
<script src="Scripts/jquery-1.4.1.js" type="text/javascript"></script>
<script type="text/javascript">
$(document).ready(function(){
  $("#trLicense").hide();
  $("table.neTable tr:even").addClass("tdTable");
  $("table.neTable tr:odd").addClass("tdTable1");
  $("#<%=ddlRoyal.ClientID %>").change(function(){
    var val1=$("#<%=ddlRoyal.ClientID %>").val();
   /* $("#<%=txtLicense.ClientID %>").val("");
    $("#<%=txtTechTransNo.ClientID %>").val("");*/
    if(val1 != "Yes")
    {
        $("#trLicense").hide();
        /*$("#trTech").hide();*/
    }
    else
    {
        $("#trLicense").show();
        /*$("#trTech").show();*/
    }
    });
  });
function Verify()
{
    var r= confirm('Are you sure; Do you want to insert/modify this record?');
    if (r==true)
    {
        var SelFileNo=document.getElementById('<%=ddlFileNo.ClientID%>');
        var SelVal= SelFileNo.options[SelFileNo.selectedIndex].value;
        if (SelVal== "")
        {
            alert('File No. can not be empty');
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
    <table align="center" class="neTable" cellpadding="12px" style="width:970px" cellspacing="10px">
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
    <tr><td colspan="4" id="lbltitle" runat="server" align="center" valign="middle" style="font-size:medium; text-decoration:underline;">
        COMMERCIALIZATION</td></tr>
    <tr><td>File Number</td>
        <td><asp:DropDownList ID="ddlFileNo" runat="server" AutoPostBack="true" EnableViewState="true"  
                onselectedindexchanged="ddlFileNo_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
    <td>Commercialization Resp.</td>
        <td><asp:DropDownList ID="ddlCommercial" runat="server" 
                onselectedindexchanged="ddlCommercial_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList><br />
        <asp:TextBox ID="txtCommercial"  Width="250px" runat="server"></asp:TextBox>
        </td></tr>
     <tr><td>Partner Ref. No</td>
        <td><asp:TextBox ID="txtInventionNo"  Width="250px" runat="server"></asp:TextBox>
        </td>
    <td>Validity from Date</td>
    <td><asp:TextBox ID="txtVFromDt" runat="server" Enabled="false"></asp:TextBox></td></tr>
    <tr> 
    <td>Renewal till Date</td>
    <td><asp:TextBox ID="txtVToDt" runat="server"></asp:TextBox>&nbsp;<asp:Image ID="imgVToDt" ImageUrl="~/Images/RedCalendar.gif" runat="server" />
    <asp:CalendarExtender ID="CalendarExtender4" TargetControlID="txtVToDt" PopupButtonID="imgVToDt" runat="server" Format="dd/MM/yyyy">
        </asp:CalendarExtender></td>
    <td>Industry</td>
    <td><asp:DropDownList ID="ddlIndustry" runat="server">
        </asp:DropDownList>
    </td></tr>
    <tr><td>Industry 2</td>
    <td><asp:TextBox ID="txtIndustry2" Width="300px" runat="server"></asp:TextBox>
    </td>
    <td>Usage Area</td>
    <td><asp:TextBox ID="txtIndustry3" Width="300px" runat="server"></asp:TextBox>
    </td></tr>
    <tr>
    <td>International Patent Classification(IPC) Code</td>
    <td><asp:TextBox ID="txtIPC" Width="300px" runat="server"></asp:TextBox>
    </td>
    <td>Abstract (Value Proposition)</td>
    <td><asp:DropDownList ID="ddlAbstract" Width="150px" runat="server"></asp:DropDownList>
    </td></tr>
    <tr>
    <td>Development Status</td>
    <td><asp:TextBox ID="txtDevelop" Width="300px" runat="server"></asp:TextBox>
    </td>
    <td>Commercialization Status</td>
    <td><asp:DropDownList ID="ddlRoyal" Width="150px" runat="server" >
        </asp:DropDownList>
    </td></tr>
    <tr id="trLicense"><td>Partner Agreement</td>
    <td><asp:TextBox ID="txtLicense" runat="server" Width="300px"></asp:TextBox></td>
    <td>Tech. Transfer No.</td>
    <td><asp:TextBox ID="txtTechTransNo" runat="server"></asp:TextBox></td></tr>
    <tr><td>Remark</td>
    <td><asp:TextBox ID="txtRemark" runat="server"></asp:TextBox></td><td></td><td></td></tr>
     <tr>
      <td colspan="4" align="center">
          <asp:ImageButton ID="imgBtnInsert" runat="server" 
              ImageUrl="~/Images/Save2.png" onclick="imgBtnInsert_Click" OnClientClick="return Verify()" />&nbsp;
          <asp:ImageButton ID="imgBtnClear" runat="server" 
              ImageUrl="~/Images/Clear2.png" onclick="imgBtnClear_Click" />          
      </td>
      </tr>
</table>
</asp:Content>

