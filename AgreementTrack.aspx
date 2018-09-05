<%@ Page Title="" Language="C#" MasterPageFile="~/Parent.master" AutoEventWireup="true" CodeFile="AgreementTrack.aspx.cs" Inherits="Default2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<script runat="server">

</script>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
    <link href="Styles/newEntry.css" rel="Stylesheet" type="text/css" />
<script src="Scripts/jquery-1.4.1.js" type="text/javascript"></script>
<script src="Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
<script src="Scripts/jquery.maskedinput-1.2.2.min.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <table align=center border=1> 
<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>  <tr><td>From Date:<asp:TextBox runat="server" ID="txtFDate"></asp:TextBox>&nbsp;<asp:Image ID="imgFDate" ImageUrl="~/Images/GreenCalendar.gif" runat="server" />
    <asp:CalendarExtender ID="CalendarExtender1" TargetControlID="txtFDate" runat="server" PopupButtonID="imgFDate" Format="yyyy-MM-dd">
    </asp:CalendarExtender></td>
   <td>To Date:<asp:TextBox runat="server" ID="txtTDate"></asp:TextBox>&nbsp;<asp:Image ID="imgTDate" ImageUrl="~/Images/GreenCalendar.gif" runat="server" />
    <asp:CalendarExtender ID="CalendarExtender2" TargetControlID="txtTDate" PopupButtonID="imgTDate" runat="server" Format="yyyy-MM-dd">
    </asp:CalendarExtender></td></tr>
    <tr><td align="center">Type:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:DropDownList ID="ddlAgreeType" 
            runat="server" Width="197px">
    </asp:DropDownList></td>
   <td>Status:&nbsp;&nbsp<asp:DropDownList ID="drop1" runat="server">
      <asp:ListItem>Draft-Party </asp:ListItem>
        <asp:ListItem>Draft-Legal</asp:ListItem>
        <asp:ListItem>Draft-IP Cell</asp:ListItem>
        <asp:ListItem>Executed-Subsisting</asp:ListItem>
        <asp:ListItem>Executed-Expired</asp:ListItem>
        <asp:ListItem>Executed-Rescinded</asp:ListItem>
        <asp:ListItem>Executed-Suspended</asp:ListItem>
        <asp:ListItem>Renewal Due</asp:ListItem>
        <asp:ListItem>To be amended</asp:ListItem>
        </asp:DropDownList></td></tr>
   <tr>
    <td align="center">
        <asp:ImageButton ID="ImageReportButn1" runat="server" 
             ImageUrl="~/Images/Report2.png" onclick="ImageReportButn1_Click"/>&nbsp;&nbsp;&nbsp;&nbsp;<asp:ImageButton 
            ID="ImageButton2" runat="server" 
             ImageUrl="~/Images/Clear2.png" onclick="ImageButton2_Click"/>&nbsp;&nbsp;</td></tr>
  
  </table>
  <table>
   <tr><td> <asp:GridView ID="GvReportDate" Width="100%" AutoGenerateColumns="false" 
            runat="server">
        <Columns>
       
        <asp:BoundField HeaderStyle-Font-Underline="true" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" ItemStyle-Wrap="true" DataField="ContractNo" HeaderText="ContractNo" SortExpression="ContractNo" />
        <asp:BoundField HeaderStyle-Font-Underline="true" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="true" DataField="AgreementType" HeaderText="Agreement Type" SortExpression="AgreementType" />
        <asp:BoundField HeaderStyle-Font-Underline="true" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="true" DataField="AgreementNo" HeaderText="Agreement No" SortExpression="AgreementNo" />
         <asp:BoundField HeaderStyle-Font-Underline="true" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="true" DataField="Title" HeaderText="Title" SortExpression="Titlee" />
         <asp:BoundField HeaderStyle-Font-Underline="true" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="true" DataField="Scope" HeaderText="Scope" SortExpression="Scope" /> 
        <asp:BoundField HeaderStyle-Font-Underline="true" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="true" DataField="Party" HeaderText="Party" SortExpression="Party" />
        <asp:BoundField HeaderStyle-Font-Underline="true" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="true" DataField="CoordinatingPerson" HeaderText="Coordinating Person" SortExpression="CoordinatingPerson" />
        <asp:BoundField HeaderStyle-Font-Underline="true" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="true" DataField="Remark" HeaderText="Remarks" SortExpression="Remark" />
        <asp:BoundField HeaderStyle-Font-Underline="true" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="true" DataField="Request" HeaderText="Request Date" SortExpression="Request" />       
       <asp:BoundField HeaderStyle-Font-Underline="true" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="true" DataField="TechTransfer" HeaderText="Tech Transfer " SortExpression="TechTransfer" />
   <asp:BoundField HeaderStyle-Font-Underline="true" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="true" DataField="Status" HeaderText="Status " SortExpression="Status" />
   <asp:BoundField HeaderStyle-Font-Underline="true" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="true" DataField="EffectiveDt" HeaderText="EffectiveDt " SortExpression="EffectiveDt" />



       <%-- <asp:TemplateField HeaderStyle-Font-Underline="true" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="true" DataField="Amount In Rs" HeaderText="AmountInRs" SortExpression="AmountInRs">
        <ItemTemplate>
        <asp:LinkButton ID="lbtnAction" Text="View" Font-Underline="true"  CommandName="View" OnClick="lbtnAction_Click" runat="server"></asp:LinkButton>
        </ItemTemplate>
        </asp:TemplateField>  --%>
      </Columns>
        <HeaderStyle ForeColor="#dffaa4" HorizontalAlign="Center" BackColor="#A55129" BorderStyle="Solid" BorderWidth="2" BorderColor="#A55129" VerticalAlign="Middle" Font-Size="13px" Height="35px" />
    <RowStyle BackColor="White" HorizontalAlign="Left" Height="25px" Font-Size="12px" Wrap="true"/>
    <AlternatingRowStyle Wrap="true" HorizontalAlign="Left" BackColor="#E9CEA4" />
    <PagerSettings Mode="Numeric" PageButtonCount="9" Position="TopAndBottom"/>
    <PagerStyle Font-Underline="true" Font-Bold="true" Font-Italic="true" ForeColor="Red"/>
        </asp:GridView></td></tr>
<tr><td align= "center"><asp:Label ID="lblmsg" runat="server" Text="No Record Exist" Font-Bold="true" Visible="false"></asp:Label></td></tr>
   </table>
</asp:Content>

