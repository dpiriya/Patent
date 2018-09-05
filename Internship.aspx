<%@ Page Title="" Language="C#" MasterPageFile="~/Parent.master" AutoEventWireup="true" CodeFile="Internship.aspx.cs" Inherits="Internship" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
<title>Internship Details</title>
<link href="Styles/newEntry.css" rel="Stylesheet" type="text/css" />
<script src="Scripts/jquery-1.9.1.js" type="text/javascript"></script>
<script src="Scripts/jquery-ui-1.10.3.custom.min.js" type="text/javascript"></script>
<link href="Styles/jquery-ui-1.10.3.custom.min.css" rel="stylesheet" type="text/css" />
<script type="text/javascript">
$(document).ready(function(){
    $("table.neTable tr:even").addClass("tdTable");
    $("table.neTable tr:odd").addClass("tdTable1");
});
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
    else 
    {
        return false;
    }
} 
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
<table align="center" class="neTable"  cellpadding="12px" width="970px" cellspacing="10px">
        <asp:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server">
        </asp:ToolkitScriptManager>
    <tr><td colspan="4" align="center" valign="middle" style="font-size:medium; text-decoration:underline; color:#755F43;">
        STUDENT INTERNSHIP PROGRAM</td></tr>
    <tr> 
        <td>Internship ID</td>
        <td> <asp:Label ID="lblInternshipID" ForeColor="#755F43" Font-Size="Medium" Font-Bold="true" runat="server"></asp:Label></td>
        <td>Name</td>
        <td><asp:TextBox ID="txtName" runat="server" Width="250px"></asp:TextBox></td></tr>        
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
      <tr>
      <td colspan="4">
      <h3 style="color:Maroon; text-decoration:underline;">Supporting Document</h3>
      </td>
      </tr>  
      <tr> 
    <td>File Description</td>
    <td><asp:TextBox ID="txtFileDesc" Width="250px" runat="server"></asp:TextBox></td>
    <td>Uploading File Name</td> 
    <td><asp:FileUpload ID="flInternship" runat="server" /></td>
    </tr>    
<tr><td colspan="4" align="center">
  <asp:ImageButton ID="imgBtnInsert" ImageUrl="~/Images/Save3.png" runat="server" 
      OnClientClick="return Verify()" onclick="imgBtnInsert_Click" style="width: 74px" />&nbsp;
  <asp:ImageButton ID="imgBtnClear" runat="server" 
      ImageUrl="~/Images/Clear3.png" onclick="imgBtnClear_Click" />         
</td>
</tr>
</table>
</asp:Content>

