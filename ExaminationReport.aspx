<%@ Page Title="" Language="C#" MasterPageFile="~/Parent.master" AutoEventWireup="true" CodeFile="ExaminationReport.aspx.cs" Inherits="ExaminationReport" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ PreviousPageType VirtualPath="~/IndianPatent.aspx"%>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
<link href="Styles/newEntry.css" rel="Stylesheet" type="text/css" />
<script src="Scripts/jquery-1.9.1.js" type="text/javascript"></script>
<script src="Scripts/jquery-ui-1.10.3.custom.min.js" type="text/javascript"></script>
<link href="Styles/jquery-ui-1.10.3.custom.min.css" rel="stylesheet" type="text/css" />
<script type="text/javascript">
function VerifyDelete() {
    var r = confirm('Are you sure; Do you want to delete this record?');
    if (r == true) return true; else return false;    
}
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
<table align="center" class="neTable" width="970px" cellpadding="7px">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>    
    <tr><td colspan="4" align="center" valign="middle" style="font-size:medium; text-decoration:underline;">
        Examination Report</td></tr>
     <tr><td colspan="2" align ="center"> File Number &nbsp;&nbsp;&nbsp;&nbsp; 
         <asp:Label ID="lblFileNo" runat="server" Font-Bold="true"></asp:Label></td> 
         <td colspan="2" align ="center"> Examination Report &nbsp;&nbsp;&nbsp;&nbsp;<asp:DropDownList 
                 ID="ddlFER" runat="server" AutoPostBack="true" onselectedindexchanged="ddlFER_SelectedIndexChanged">
         </asp:DropDownList></td></tr>
        <tr><td colspan="4">
        <h4>Examination Report</h4>
            <asp:ListView ID="lvExamRpt" runat="server" 
                onitemcanceling="lvExamRpt_ItemCanceling" 
                onitemdeleting="lvExamRpt_ItemDeleting"  
                oniteminserting="lvExamRpt_ItemInserting" 
                onitemdatabound="lvExamRpt_ItemDataBound" 
                onselectedindexchanging="lvExamRpt_SelectedIndexChanging" >
            <LayoutTemplate>
            <table width="850px" align="center" runat="server"  style="text-align:left;border-color:#DEDFDE; border-style:solid; border-width:1px;" cellpadding="5" cellspacing="0" border="1">
            <tr id="Tr1" runat="server" style="background-color:#f9cfaa;padding-top:0px;padding-bottom:0px;height:25px;">
            <th id="Th6" align="center" runat="server">Sl. No.</th>
            <th id="Th1" align="center" runat="server">Date of Examination Report</th>
            <th id="Th2" align="center" runat="server">Examination Report</th> 
            <th id="Th7" align="center" runat="server">Remarks</th> 
            <th id="Th3" align="center" runat="server"><asp:ImageButton ID="btnExamAdd" Width="25px" Height="25px" ImageUrl="~/Images/Plus.gif" runat="server" OnClick="btnExamAdd_Click" /></th> 
            </tr>
            <tr runat="server" id="itemPlaceholder" />
            </table>
            </LayoutTemplate>
            <ItemTemplate>
            <tr id="Tr2" runat="server" style="background-color:White">
            <td><asp:Label ID="lblExamSlNo" Text='<%#Eval("SlNo")%>' runat="server"></asp:Label></td>
            <td><asp:Label ID="lblExamDt" Text='<%#Eval("ExaminationDate","{0:dd/MM/yyyy}")%>' runat="server"></asp:Label></td>
            <td><asp:LinkButton ID="lbtnFile" Text='<%#Eval("ExaminationDocument")%>' Font-Underline="true" CommandName="Select" runat="server"></asp:LinkButton> </td>
            <td><asp:Label ID="lblRemarks" Text='<%#Eval("Remarks")%>' runat="server"></asp:Label></td>
            <td><asp:LinkButton ID="lbtnDelete" Font-Underline="true" CommandName="delete" Visible="false" OnClientClick="return VerifyDelete()" runat="server">Delete</asp:LinkButton></td>
            </tr>
            </ItemTemplate>
            <InsertItemTemplate>
            <tr id="Tr3" runat="server">
            <td><asp:Label ID="lblNewExamSlNo" runat="server"></asp:Label></td>
            <td><asp:TextBox ID="txtExamDt" runat="server" Width="80px"></asp:TextBox>
            &nbsp;<asp:Image ID="imgExamDt" ImageUrl="~/Images/RedCalendar.gif" runat="server" />
            <asp:CalendarExtender ID="CalendarExtender2" TargetControlID="txtExamDt" PopupButtonID="imgExamDt" runat="server" Format="dd/MM/yyyy"></asp:CalendarExtender>
            </td>
            <td><asp:FileUpload ID="FileUploadER" runat="server" /></td>
            <td><asp:TextBox ID="txtExamRemarks" runat="server" Width="250px"></asp:TextBox></td>
            <td><asp:LinkButton ID="lbtnInsert" Font-Underline="true" CommandName="insert" runat="server">Insert</asp:LinkButton>
            <asp:LinkButton ID="lbtnCancel" Font-Underline="true" CommandName="cancel" runat="server">Cancel</asp:LinkButton></td>
            </tr>
            </InsertItemTemplate>
            </asp:ListView>
    </td></tr>     
        <tr><td colspan="4">
        <h4>Response for Examination</h4>
            <asp:ListView ID="lvExamResponse" runat="server" 
                onitemcanceling="lvExamResponse_ItemCanceling" 
                onitemdeleting="lvExamResponse_ItemDeleting" 
                oniteminserting="lvExamResponse_ItemInserting" 
                onselectedindexchanging="lvExamResponse_SelectedIndexChanging" 
                onitemdatabound="lvExamResponse_ItemDataBound">
            <LayoutTemplate>
            <table id="Table1" width="850px" align="center" runat="server"  style="text-align:left;border-color:#DEDFDE; border-style:solid; border-width:1px;" cellpadding="5" cellspacing="0" border="1">
            <tr id="Tr1" runat="server" style="background-color:#f9cfaa;padding-top:0px;padding-bottom:0px;height:25px;">
            <th id="Th4" align="center" runat="server">Sl. No.</th>
            <th id="Th1" align="center" runat="server">Date of Response</th>
            <th id="Th2" align="center" runat="server">Response Report</th> 
            <th id="Th5" align="center" runat="server">Remarks</th> 
            <th id="Th3" align="center" runat="server"><asp:ImageButton ID="btnResponseAdd" Width="25px" Height="25px" ImageUrl="~/Images/Plus.gif"  runat="server" OnClick="btnExamResponseAdd_Click" /></th> 
            </tr>
            <tr runat="server" id="itemPlaceholder" />
            </table>
            </LayoutTemplate>
            <ItemTemplate>
            <tr id="Tr2" runat="server" style="background-color:White">
            <td><asp:Label ID="lblSlNo" Text='<%#Eval("ResponseSlNo")%>' runat="server"></asp:Label></td>
            <td><asp:Label ID="lblResponseDt" Text='<%#Eval("ResponseDate","{0:dd/MM/yyyy}")%>' runat="server"></asp:Label></td>
            <td><asp:LinkButton ID="lbtnResponseFile" Text='<%#Eval("ResponseDocument")%>' Font-Underline="true" CommandName="Select" runat="server"></asp:LinkButton> </td>
            <td><asp:Label ID="lblResponseRemarks" Text='<%#Eval("Remarks")%>' runat="server"></asp:Label></td>
            <td><asp:LinkButton ID="lbtnDelete" Font-Underline="true" CommandName="delete" Visible="false"  runat="server" OnClientClick="return VerifyDelete()">Delete</asp:LinkButton></td>
            </tr>
            </ItemTemplate>
            <InsertItemTemplate>
            <tr id="Tr3" runat="server">
            <td><asp:Label ID="lblNewSlNo" runat="server"></asp:Label></td>
            <td><asp:TextBox ID="txtResponseDt" runat="server" Width="80px"></asp:TextBox>&nbsp;<asp:Image ID="imgResponseDt" ImageUrl="~/Images/RedCalendar.gif" runat="server" />
            <asp:CalendarExtender ID="CalendarExtender4" TargetControlID="txtResponseDt" PopupButtonID="imgResponseDt" runat="server" Format="dd/MM/yyyy"></asp:CalendarExtender></td>
            <td><asp:FileUpload ID="FileUploadResponse" runat="server" /></td>
            <td><asp:TextBox ID="txtResponseRemarks" runat="server" Width="250px"></asp:TextBox></td>
            <td><asp:LinkButton ID="lbtnInsert" Font-Underline="true" CommandName="insert" runat="server">Insert</asp:LinkButton>
            <asp:LinkButton ID="lbtnCancel" Font-Underline="true" CommandName="cancel" runat="server">Cancel</asp:LinkButton></td>
            </tr>
            </InsertItemTemplate>            
            </asp:ListView>
    </td></tr>
    <tr><td></td></tr>         
</table>    
</asp:Content>

