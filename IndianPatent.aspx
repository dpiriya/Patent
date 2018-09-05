<%@ Page Language="C#" MasterPageFile="~/Parent.master" AutoEventWireup="true"  CodeFile="IndianPatent.aspx.cs" Inherits="IndianPatent" Title="Indian Patent" %>
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
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server"  >
    <table align="center" class="neTable" cellpadding="12px" style="width:985px" cellspacing="10px">
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
    <tr><td colspan="4" id="lbltitle" runat="server" align="center" valign="middle" style="font-size:medium; text-decoration:underline;">
        INDIAN PATENT</td></tr>
    <tr><td>File Number</td>
        <td><asp:DropDownList ID="ddlFileNo" runat="server" AutoPostBack="true" EnableViewState="true"  
                onselectedindexchanged="ddlFileNo_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
    <td>Patent Attorney</td>
        <td><asp:DropDownList ID="ddlAttorney" runat="server" 
                onselectedindexchanged="ddlAttorney_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>        
        </td></tr>
    <tr><td>Application Number</td>
    <td><asp:TextBox ID="txtAppNo" runat="server"></asp:TextBox></td>
    <td>Date of Filing</td>
    <td><asp:TextBox ID="txtDtFiling" runat="server"></asp:TextBox>&nbsp;<asp:Image ID="imgFilingDt" runat="server" ImageUrl="~/Images/RedCalendar.gif" />
    <asp:CalendarExtender ID="CalendarExtender13" PopupButtonID="imgFilingDt" TargetControlID="txtDtFiling" runat="server" Format="dd/MM/yyyy">
        </asp:CalendarExtender>
    </td></tr>
    <tr><td>Complete Filing Date</td><td><asp:TextBox ID="txtCompleteDt" runat="server"></asp:TextBox>&nbsp;<asp:Image ID="ImgCompleteDt" runat="server" ImageUrl="~/Images/RedCalendar.gif" />
    <asp:CalendarExtender ID="CalendarExtender2" PopupButtonID="imgCompleteDt" TargetControlID="txtCompleteDt" runat="server" Format="dd/MM/yyyy">
        </asp:CalendarExtender></td>
    <td>Patent Search</td><td><asp:DropDownList ID="ddlPSearch" Width="100px" 
            runat="server"></asp:DropDownList><asp:TextBox ID="txtpatsearchDt" runat="server"></asp:TextBox>&nbsp;<asp:Image ID="imgpatsearchDt" runat="server" ImageUrl="~/Images/RedCalendar.gif" />
    <asp:CalendarExtender ID="CalendarExtender4" PopupButtonID="imgpatsearchDt" TargetControlID="txtpatsearchDt" runat="server" Format="dd/MM/yyyy">
        </asp:CalendarExtender>
    
    </td></tr> 
    <tr><td style="width:200px">Request for Examination (RFE)</td>
    <td><asp:DropDownList ID="ddlExam" Width="100px" runat="server"></asp:DropDownList>
    </td>
    <td style="width:200px">Date of Request for Examination </td>
    <td><asp:TextBox ID="txtExamDt" runat="server"></asp:TextBox>&nbsp;<asp:Image ID="imgExamDt" ImageUrl="~/Images/RedCalendar.gif" runat="server" />
    <asp:CalendarExtender ID="CalendarExtender5" PopupButtonID="imgExamDt" TargetControlID="txtExamDt" runat="server" Format="dd/MM/yyyy">
        </asp:CalendarExtender>&nbsp;&nbsp;&nbsp;<asp:LinkButton ID="lbFER"  
            runat="server" Visible="false" PostBackUrl="~/ExaminationReport.aspx">FER</asp:LinkButton> </td></tr>
    <tr><td>Publication</td>
    <td><asp:TextBox ID="txtPub" runat="server"></asp:TextBox></td>
    <td>Date of Publishing</td>
    <td><asp:TextBox ID="txtPubDt" runat="server"></asp:TextBox>&nbsp;<asp:Image ID="imgPubDt" ImageUrl="~/Images/RedCalendar.gif" runat="server" />
    <asp:CalendarExtender ID="CalendarExtender1" PopupButtonID="imgPubDt" TargetControlID="txtPubDt" runat="server" Format="dd/MM/yyyy">
        </asp:CalendarExtender></td></tr>
    
   <asp:UpdatePanel ID="UpdatePanel1" runat="server">
   <ContentTemplate>
   <tr><td>Status</td>
        <td><asp:DropDownList ID="ddlStatus" runat="server" 
                onselectedindexchanged="ddlStatus_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
   </td>
   <td>Sub Status</td>
        <td><asp:DropDownList ID="ddlSubStatus" runat="server" 
                onselectedindexchanged="ddlSubStatus_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList><br />
        <asp:TextBox ID="txtSubStatus" runat="server"></asp:TextBox>
   </td></tr>
   </ContentTemplate>
   </asp:UpdatePanel>    
    <tr><td>Patent Number</td>
    <td><asp:TextBox ID="txtPatNo" runat="server"></asp:TextBox></td>
    
    <td>Patent Issue Date</td>
    <td><asp:TextBox ID="txtPatDt" runat="server"></asp:TextBox>&nbsp;<asp:Image ID="imgPatDt" ImageUrl="~/Images/RedCalendar.gif" runat="server" />
    <asp:CalendarExtender ID="CalendarExtender3" PopupButtonID="imgPatDt" TargetControlID="txtPatDt" runat="server" Format="dd/MM/yyyy">
        </asp:CalendarExtender></td></tr>
        <asp:HiddenField ID="hfType" runat="server" />
        <asp:HiddenField ID="hfPreviousStatus" runat="server" />
    <tr>
      <td colspan="4" align="center">
          <asp:ImageButton ID="imgBtnInsert" runat="server" 
              ImageUrl="~/Images/Save2.png" onclick="imgBtnInsert_Click" OnClientClick="return Verify()" />&nbsp;
          <asp:ImageButton ID="imgBtnClear" runat="server" 
              ImageUrl="~/Images/Clear2.png" onclick="imgBtnClear_Click" />          
      </td>
      </tr>
        <tr>
            <td colspan="4">
                <asp:Label runat="server" ID="pminfo"></asp:Label>
            </td>
        </tr>
</table>
</asp:Content>

