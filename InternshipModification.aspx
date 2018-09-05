<%@ Page Title="" Language="C#" MasterPageFile="~/Parent.master" AutoEventWireup="true" CodeFile="InternshipModification.aspx.cs" Inherits="InternshipModification" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
<link href="Styles/ContractTabStyle.css" rel="Stylesheet" type="text/css" />
<script src="Scripts/jquery-1.9.1.js" type="text/javascript"></script>
<script src="Scripts/jquery-ui-1.10.3.custom.min.js" type="text/javascript"></script>
<link href="Styles/jquery-ui-1.10.3.custom.min.css" rel="stylesheet" type="text/css" />
<link href="Styles/TabStyle.css" rel="Stylesheet" type="text/css" />
<script type="text/javascript">
    function Verify() {
        var r = confirm('Are you sure; Do you want to insert this record?');
        if (r == true) {
            if (document.getElementById('<%=txtName.ClientID%>').value == "") {
                alert('Name can not be empty');
                return false;
            }
            if (document.getElementById('<%=txtProgram.ClientID%>').value == "") {
                alert('Degree/Program can not be empty');
                return false;
            }
            if (document.getElementById('<%=txtRollNo.ClientID%>').value == "") {
                alert('Roll Number can not be empty');
                return false;
            }
            if (document.getElementById('<%=txtCandidateDept.ClientID%>').value == "") {
                alert('Department can not be empty');
                return false;
            }
            if (document.getElementById('<%=txtCollege.ClientID%>').value == "") {
                alert('College Name can not be empty');
                return false;
            }
            if (document.getElementById('<%=txtTitle.ClientID%>').value == "") {
                alert('Project Title can not be empty');
                return false;
            }
            if (document.getElementById('<%=txtCommAddress1.ClientID%>').value == "") {
                alert('Communication Address can not be empty');
                return false;
            }
            if (document.getElementById('<%=txtJoiningDt.ClientID%>').value == "") {
                alert('Joining Date can not be empty');
                return false;
            }
            if (document.getElementById('<%=txtRelieveDt.ClientID%>').value == "") {
                alert('Relieving Date can not be empty');
                return false;
            }
            if (document.getElementById('<%=txtPresentationDt.ClientID%>').value == "") {
                alert('Presentation Date can not be empty');
                return false;
            }
            return true;
        }
        else {
            return false;
        }
    }
    function DeleteVerify() {

        var r = confirm('Are you sure; Do you want to delete this record?');
        if (r == true) {
            return true;
        }
        else {
            return false;
        }
    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>    
<table align="center" class="neTable" width="970px" cellpadding="0px" cellspacing="0px">
<tr style="height:30px;"><td colspan="4" align="center" valign="top" style="font-size:medium; color:#654724; text-decoration:underline;">
    INTERNSHIP MODIFICATION</td></tr>
<tr align="center">
    <td valign="bottom">Internship ID &nbsp;&nbsp;
    <asp:TextBox ID="txtInternshipID" runat="server" Width="100px"></asp:TextBox>&nbsp;&nbsp;<asp:Button 
            ID="btnFind" runat="server" CssClass="submitButton" Text="Find" 
            onclick="btnFind_Click" />&nbsp;&nbsp;
        <asp:Button ID="btnClear" runat="server" CssClass="submitButton" Text="Clear" 
            onclick="btnClear_Click" /></td></tr>    
<tr><td>
    <asp:Button ID="Tab1" BorderStyle="None" runat="server" Text="Internship" CssClass="Initial" 
        onclick="Tab1_Click" />
    <asp:Button ID="Tab2" BorderStyle="None" runat="server" Text="Files" CssClass="Initial" 
        onclick="Tab2_Click" />
    </td></tr>
    <tr><td>
    <asp:MultiView ID="MainView" runat="server">
    <asp:View ID="View1" runat="server">
    <table style="width:100%;border-width:1px;border-color:#666;border-style:solid;" cellpadding="4px" class="MVStyle" cellspacing="10px">
     <tr> 
        <td>Name</td>
        <td><asp:TextBox ID="txtName" runat="server" Width="250px"></asp:TextBox></td>
        <td colspan="2" align="right"><asp:Button ID="btnUpdate" Visible="false" runat="server" 
            Text="Enable Update" BackColor="Transparent" Font-Names="Arial" ForeColor="#9f5a0b"
            Font-Bold="true" BorderStyle="None" Font-Underline="true" 
            onclick="btnUpdate_Click" /></td>
        </tr>        
      <tr> 
        <td>Program / Degree</td>
        <td><asp:TextBox ID="txtProgram" Width="250px" runat="server"></asp:TextBox></td>      
        <td>Roll Number</td> 
        <td><asp:TextBox ID="txtRollNo" Width="250px" runat="server"></asp:TextBox></td>
        </tr>
      <tr>
      <td>Department</td>
        <td>
            <asp:TextBox ID="txtCandidateDept" Width="250px" runat="server"></asp:TextBox>
          </td> 
        <td>College</td>
        <td>
            <asp:TextBox ID="txtCollege" Width="250px" runat="server"></asp:TextBox>
        </td></tr>
      <tr> 
      <td>Faculty</td>
        <td><asp:TextBox ID="txtFaculty" Width="250px" runat="server"></asp:TextBox></td>
      <td>Faculty Department</td> 
      <td><asp:TextBox ID="txtFacultyDept" Width="250px" runat="server"></asp:TextBox></td>
      </tr><tr>
      <td>Faculty Contact Info.</td><td>
            <asp:TextBox ID="txtFacultyContact1" Width="250px" runat="server"></asp:TextBox><br />
            <asp:TextBox ID="txtFacultyContact2" Width="250px" runat="server"></asp:TextBox> </td>
        <td>Title of the Project</td>
        <td><asp:TextBox ID="txtTitle" TextMode="MultiLine" Width="250px" runat="server"></asp:TextBox></td>
        </tr>
    <tr>
    <td>Communication Address</td>
    <td>
        <asp:TextBox ID="txtCommAddress1" Width="250px" runat="server"></asp:TextBox><br />
        <asp:TextBox ID="txtCommAddress2" Width="250px" runat="server"></asp:TextBox><br />
        <asp:TextBox ID="txtCommCity" Width="250px" runat="server"></asp:TextBox><br />
        <asp:TextBox ID="txtCommPincode" Width="250px" runat="server"></asp:TextBox>
    </td>
    <td>Permanent Address</td>
    <td>
        <asp:TextBox ID="txtPermanentAddress1" Width="250px" runat="server"></asp:TextBox><br />
        <asp:TextBox ID="txtPermanentAddress2" Width="250px" runat="server"></asp:TextBox><br />
        <asp:TextBox ID="txtPermanentCity" Width="250px" runat="server"></asp:TextBox><br />
        <asp:TextBox ID="txtPermanentPincode" Width="250px" runat="server"></asp:TextBox>
    </td>
    </tr>
    <tr> 
    <td>Contact Phone Number</td>
    <td><asp:TextBox ID="txtPhoneNo" Width="250px" runat="server"></asp:TextBox></td>
    <td>Contact Mobile Number</td> 
    <td><asp:TextBox ID="txtMobileNo" Width="250px" runat="server"></asp:TextBox></td>
    </tr>
    <tr> 
    <td>Email ID</td>
    <td><asp:TextBox ID="txtEmailID" Width="250px" runat="server"></asp:TextBox></td>
    <td>Joining Date</td> 
    <td><asp:TextBox ID="txtJoiningDt" Width="100px" runat="server"></asp:TextBox>&nbsp;<asp:Image ID="imgJoining" ImageUrl="~/Images/RedCalendar.gif" runat="server" />
    <asp:CalendarExtender ID="CalendarExtender1" TargetControlID="txtJoiningDt" PopupButtonID="imgJoining" runat="server" Format="dd/MM/yyyy">
    </asp:CalendarExtender></td></tr>
    <tr> 
      <td>Relieving Date</td>
        <td><asp:TextBox ID="txtRelieveDt" Width="100px" runat="server"></asp:TextBox>&nbsp;<asp:Image ID="imgRelieveDt" ImageUrl="~/Images/RedCalendar.gif" runat="server" />
    <asp:CalendarExtender ID="CalendarExtender2" TargetControlID="txtRelieveDt" PopupButtonID="imgRelieveDt" runat="server" Format="dd/MM/yyyy">
    </asp:CalendarExtender></td>
      <td>Date of Presentation/Meetup</td> 
      <td><asp:TextBox ID="txtPresentationDt" Width="100px" runat="server"></asp:TextBox>&nbsp;<asp:Image ID="imgPresentationDt" ImageUrl="~/Images/RedCalendar.gif" runat="server" />
    <asp:CalendarExtender ID="CalendarExtender3" TargetControlID="txtPresentationDt" PopupButtonID="imgPresentationDt" runat="server" Format="dd/MM/yyyy">
    </asp:CalendarExtender></td>
     </tr>
     <tr><td align="center" colspan="4">
        <asp:Button ID="btnInternship" runat="server" Visible="false" 
             CssClass="submitButton" Text="Update Internship" 
             onclick="btnInternship_Click" OnClientClick="return Verify()" />
        </td>
    </tr>
     </table>
    </asp:View>
    <asp:View ID="View2" runat="server">
    <table style="width:100%;border-width:1px;border-color:#666;border-style:solid;" class="MVStyle" cellspacing="10px">
    <tr><td align="right"><asp:Button ID="btnUpload" runat="server" 
            Text="Upload File" BackColor="Transparent" Font-Names="Arial" ForeColor="#9f5a0b" 
            Font-Bold="true" BorderStyle="None" Font-Underline="true" 
            onclick="btnUpload_Click" /></td></tr>
    <tr><td>
    <asp:Panel ID="panNewFile" CssClass="borderStyle" Visible="false" Width="100%" runat="server">
    <table width="100%"  align="center">
    <tr><td id="Td1" colspan="4" align="center" class="TitleStyle">New File Upload</td></tr>
    <tr> <td>Description of File</td><td><asp:TextBox ID="txtFileDesc" Width="300px" runat="server"></asp:TextBox></td>
    <td>Uploading File</td>
    <td><asp:FileUpload ID="FileUploadAgree" runat="server" /></td>
    </tr><tr>
    <td>Comments</td><td><asp:TextBox ID="txtComment" Width="300" runat="server"></asp:TextBox></td>
    <td colspan="2" align="center">
        <asp:Button ID="btnFileSave" runat="server" 
            CssClass="submitButton" Text="Save" onclick="btnFileSave_Click" />&nbsp;&nbsp;
    <asp:Button ID="btnFileClose" runat="server" CssClass="submitButton" Text="Close" 
            onclick="btnFileClose_Click"/>
    </td></tr>
    </table>
    </asp:Panel>    
    </td></tr>
    <tr><td><div>List of Internship Files</div></td></tr>
    <tr><td>
 <asp:GridView ID="gvFileDetails" AutoGenerateColumns="false" Width="950px"  
         CellPadding="5" CellSpacing="0" 
         Font-Names="Arial" BorderColor="#1b5c09" runat="server" 
         AllowSorting="true" AllowPaging="true" PageSize="25"
         onpageindexchanging="gvFileDetails_PageIndexChanging" 
         onselectedindexchanging="gvFileDetails_SelectedIndexChanging" 
         onsorting="gvFileDetails_Sorting" 
         onrowdatabound="gvFileDetails_RowDataBound" 
         onrowcommand="gvFileDetails_RowCommand" 
         onrowdeleting="gvFileDetails_RowDeleting">
 <Columns>
 <asp:BoundField DataField="EntryDt"  HeaderStyle-Font-Underline="true" HeaderStyle-ForeColor="Ivory" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Entry Dt." SortExpression="EntryDt"  />
 <asp:BoundField HeaderStyle-Font-Underline="true" DataField="InternshipID" HeaderText="Internship ID" SortExpression="InternshipID"  />
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
    <asp:LinkButton ID="lbtnDelete" Text="Delete" OnClientClick="return DeleteVerify()" Font-Underline="true" CommandName="Delete" runat="server"></asp:LinkButton>
 </ItemTemplate>
 </asp:TemplateField>
 </Columns>
 <EmptyDataTemplate>
 <p style="text-align:center; background-color:#C8C8C8">
 <asp:Image ID="Image1" BorderStyle="None" BackColor="#C8C8C8" ImageUrl="~/Images/NoData.jpg" runat="server" />
 </p>
 </EmptyDataTemplate>
 <HeaderStyle CssClass="HeaderStyle" ForeColor="Ivory" HorizontalAlign="Center" VerticalAlign="Middle" Height="25px" />
 <RowStyle BackColor="White" HorizontalAlign="Left" Height="25px" Font-Size="12px" Wrap="true"/>
 <AlternatingRowStyle Wrap="true" HorizontalAlign="Left" BackColor="#f9ede0" />
 <PagerSettings Mode="Numeric" PageButtonCount="9" Position="TopAndBottom"/>
 <PagerStyle Font-Underline="true" Font-Bold="true" Font-Italic="true" ForeColor="Red" />
 </asp:GridView> 
 <i id="pageNumber" visible="false" style="font-weight:bold" runat="server"> You are 
     viewing page <%=gvFileDetails.PageIndex + 1%> of <%= gvFileDetails.PageCount%></i> 
 </td></tr>
 </table> 
 </asp:View>
</asp:MultiView>
</td></tr>
<tr><td>&nbsp;</td></tr>
</table>
</asp:Content>

