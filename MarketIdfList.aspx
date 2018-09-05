<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MarketIdfList.aspx.cs" Inherits="MarketIdfList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Marketing Project Details</title>
</head>
<body>
    <form id="form1" runat="server">
    <table>
    <tr align="center">
    <td>
       <h2><asp:Label ID="lblTitle" runat="server"></asp:Label></h2>
    </td>
    </tr>
    <tr><td><h3>Marketing Project Details</h3></td></tr>
    <tr><td>
        <asp:DetailsView ID="dvMarket" runat="server" AutoGenerateRows="False"  
                CellPadding="4" ForeColor="Black" GridLines="Both" Font-Names="Arial" BackColor="White" 
                BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px">
            <RowStyle BackColor="#FFFFCC"/>
        <Fields>
        <asp:BoundField HeaderText="Project No" 
                ItemStyle-HorizontalAlign="Left" DataField="MktgProjectNo" ReadOnly="true">
            </asp:BoundField>
        <asp:BoundField HeaderText="Marketing Title" ItemStyle-HorizontalAlign="Left" 
                DataField="MktgTitle" ReadOnly="true">
            </asp:BoundField>
        <asp:BoundField HeaderText="Company Name" ItemStyle-HorizontalAlign="Left" 
                DataField="MktgCompany" ReadOnly="true">
            </asp:BoundField>
        <asp:BoundField HeaderText="Marketing Group" ItemStyle-HorizontalAlign="Left" 
                DataField="MktgGroup" ReadOnly="true">
            </asp:BoundField>
        <asp:BoundField HeaderText="Project Value" ItemStyle-HorizontalAlign="Left" 
                DataField="ProjectValue" ReadOnly="true">
            </asp:BoundField>
        <asp:BoundField HeaderText="Value Realization" ItemStyle-HorizontalAlign="Left" 
                DataField="ValueRealization" ReadOnly="true">
            </asp:BoundField>
        <asp:BoundField HeaderText="Status" ItemStyle-HorizontalAlign="Left" 
                DataField="Status" ReadOnly="true">
            </asp:BoundField>
        <asp:BoundField HeaderText="Remarks" ItemStyle-HorizontalAlign="Left" 
                DataField="Remarks" ReadOnly="true">
            </asp:BoundField>
        </Fields>
            <AlternatingRowStyle BackColor="White" />
        </asp:DetailsView>
    </td></tr>
    <tr><td><h3>IDF File Numbers details</h3></td></tr>
    <tr><td>
        <asp:GridView ID="gvIDF" AutoGenerateColumns="false" runat="server" Font-Names="Arial" BorderColor="#006633" runat="server">
        <Columns>
        <asp:BoundField HeaderText="IDF File No." ItemStyle-Wrap="true" DataField="fileno" />
        <asp:BoundField HeaderText="Title"  ItemStyle-Wrap="true" DataField="title" />
        <asp:BoundField HeaderText="Inventor" ItemStyle-Wrap="true" DataField="inventor1" />
        <asp:BoundField HeaderText="Application No." ItemStyle-Wrap="true" DataField="applcn_no" />
        <asp:BoundField HeaderText="Status" ItemStyle-Wrap="true" DataField="status" />
        </Columns>
        <EmptyDataTemplate>
     <p style="text-align:center; background-color:#C8C8C8">
     <asp:Image ID="Image2" BorderStyle="None" BackColor="#C8C8C8" ImageUrl="~/Images/NoData.jpg" runat="server" />
     </p>
     </EmptyDataTemplate>
     <HeaderStyle HorizontalAlign="Center" BackColor="#006633" VerticalAlign="Middle" Font-Size="14px" Height="35px" />
     <RowStyle BackColor="White" Height="25px" Font-Size="13px" Wrap="true"/>
     <AlternatingRowStyle Wrap="true" BackColor="#FFFFCC" />
        </asp:GridView>
    </td></tr>
    <tr><td><h3>Activity details</h3></td></tr>
    <tr><td>
        <asp:GridView ID="gvActivity" AutoGenerateColumns="false" runat="server" Font-Names="Arial" BorderColor="#006633" runat="server">
        <Columns>
        <asp:BoundField HeaderText="Activity Date" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-Wrap="true" DataField="ActivityDt" />
        <asp:BoundField HeaderText="Channel" ItemStyle-Wrap="true" DataField="Channel" />
        <asp:BoundField HeaderText="Activity Type" ItemStyle-Wrap="true" DataField="ActivityType" />
        <asp:BoundField HeaderText="Remarks" ItemStyle-Wrap="true" DataField="Remarks" />
        </Columns>
     <EmptyDataTemplate>
     <p style="text-align:center; background-color:#C8C8C8">
     <asp:Image ID="Image1" BorderStyle="None" BackColor="#C8C8C8" ImageUrl="~/Images/NoData.jpg" runat="server" />
     </p>
     </EmptyDataTemplate>
     <HeaderStyle HorizontalAlign="Center" BackColor="#006633" VerticalAlign="Middle" Font-Size="14px" Height="35px" />
     <RowStyle BackColor="White" Height="25px" Font-Size="13px" Wrap="true"/>
     <AlternatingRowStyle Wrap="true" BackColor="#FFFFCC" />
     </asp:GridView>
    </td></tr>
    
    </table>
    </form>
</body>
</html>
