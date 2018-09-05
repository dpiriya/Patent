<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InternshipDetails.aspx.cs" Inherits="InternshipDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
     <form id="form1" runat="server">
    <div>
    <table>
    <tr align="center">
    <td>
       <h2><asp:Label ID="lblTitle" runat="server"></asp:Label></h2>
    </td>
    </tr>
    <tr><td>
        <asp:DetailsView ID="dvInternship" AutoGenerateRows="false" runat="server" Height="50px" Width="700px">
        <RowStyle  BackColor="#e4f3e6" />
        <AlternatingRowStyle BackColor="#FFFFFF" />
        <HeaderStyle BackColor="#006633" ForeColor="#ebfd4b" Height="30px" />
        <HeaderTemplate >
        <h3 style="text-align:center;">Student Complete Details</h3>
        </HeaderTemplate>
        <Fields>
        <asp:BoundField DataField="EntryDt" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Entry Date" />
        <asp:BoundField DataField="InternshipID" HeaderText="Internship ID" />
        <asp:BoundField DataField="CandidateName" HeaderText="Candidate Name" />
        <asp:BoundField DataField="RollNumber" HeaderText="Roll Number" />
        <asp:BoundField DataField="ProgramOrDegree" HeaderText="Program Or Degree" />
        <asp:BoundField DataField="Department" HeaderText="Department" />
        <asp:BoundField DataField="College" HeaderText="College" />
        <asp:BoundField DataField="FacultyName" HeaderText="Faculty Name" />
        <asp:BoundField DataField="FacultyDepartment" HeaderText="Faculty Department" />
        <asp:BoundField DataField="FacultyInfo1" HeaderText="Faculty Information" />
        <asp:BoundField DataField="FacultyInfo2" HeaderText="" />
        <asp:BoundField DataField="Title" HeaderText="Title" />
        <asp:BoundField DataField="CommunicationAddress1" HeaderText="Communication Address1" />
        <asp:BoundField DataField="CommunicationAddress2" HeaderText="Communication Address2" />
        <asp:BoundField DataField="CommunicationCity" HeaderText="Communication City" />
        <asp:BoundField DataField="CommunicationPincode" HeaderText="Communication Pincode" />
        <asp:BoundField DataField="PermanentAddress1" HeaderText="Permanent Address1" />
        <asp:BoundField DataField="PermanentAddress2" HeaderText="Permanent Address2" />
        <asp:BoundField DataField="PermanentCity" HeaderText="Permanent City" />
        <asp:BoundField DataField="PermanentPincode" HeaderText="Permanent Pincode" />
        <asp:BoundField DataField="ContactPhoneNo" HeaderText="Contact PhoneNo" />
        <asp:BoundField DataField="ContactMobileNo" HeaderText="Contact MobileNo" />
        <asp:BoundField DataField="EmailID" HeaderText="EmailID" />
        <asp:BoundField DataField="JoiningDate" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Joining Date" />
        <asp:BoundField DataField="RelievingDate" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Relieving Date" />
        <asp:BoundField DataField="PresentationDate" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Presentation Date" />
        <asp:BoundField DataField="UserName" HeaderText="UserName" />
        </Fields>
        </asp:DetailsView>
    </td></tr>    
    <tr><td><h3>Internship Document Details</h3></td></tr>
    <tr><td>
        <asp:GridView ID="gvDocument" AutoGenerateColumns="false" CellPadding="4" CellSpacing="5" runat="server" Font-Names="Arial" BorderColor="#006633" >
        <Columns>
        <asp:BoundField HeaderText="File Description" DataField="FileDescription" />
        <asp:TemplateField HeaderText="File Name" ItemStyle-HorizontalAlign="Left">
        <ItemTemplate>
        <asp:LinkButton ID="lbtnView" Text='<%#Eval("FileName")%>' Font-Underline="true" OnClick="lbtnAction_Click" runat="server"></asp:LinkButton> 
        </ItemTemplate>
        </asp:TemplateField>
        </Columns>
        <EmptyDataTemplate>
         <p style="text-align:center; background-color:#C8C8C8">
         <asp:Image ID="Image2" BorderStyle="None" BackColor="#C8C8C8" ImageUrl="~/Images/NoData.jpg" runat="server" />
         </p>
         </EmptyDataTemplate>
         <HeaderStyle HorizontalAlign="Center" BackColor="#006633" ForeColor="YellowGreen" VerticalAlign="Middle" Font-Size="14px" Height="35px" />
         <RowStyle BackColor="White" Height="25px" Font-Size="13px" Wrap="true"/>
         <AlternatingRowStyle Wrap="true" BackColor="#FFFFCC" />
        </asp:GridView>
    </td></tr>
    </table>
    </div>
    </form>
</body>
</html>
