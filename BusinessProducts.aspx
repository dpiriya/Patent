<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BusinessProducts.aspx.cs" Inherits="BusinessProducts" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <table>
    <tr align="center">
    <td>
       <h2><asp:Label ID="lblTitle" runat="server"></asp:Label></h2>
    </td>
    </tr>
    <tr><td><h3>Business Area and Products Details</h3></td></tr>
    <tr><td>
        <asp:DetailsView ID="dvCompany" runat="server" AutoGenerateRows="False"  
                CellPadding="4" ForeColor="Black" GridLines="Both" Font-Names="Arial" BackColor="White" 
                BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px">
            <RowStyle BackColor="#FFFFCC"/>
        <Fields>
        <asp:BoundField HeaderText="Company ID" 
                ItemStyle-HorizontalAlign="Left" DataField="CompanyID" ReadOnly="true">
            </asp:BoundField>
        <asp:BoundField HeaderText="Company Name" ItemStyle-HorizontalAlign="Left" 
                DataField="CompanyName" ReadOnly="true">
            </asp:BoundField>
        <asp:TemplateField HeaderText="Address" ItemStyle-HorizontalAlign="Left">
        <ItemTemplate>
        <%#Eval("Address1") + "<br />" + Eval("Address2") + "<br />" + Eval("City") + " - " + Eval("Pincode")%>
        </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Contact Details" ItemStyle-HorizontalAlign="Left">
        <ItemTemplate>
        <%# "Phone No. : " + Eval("Phone1") + "<br /> " + Eval("Phone2") + "<br /> Email ID : " + Eval("EmailID1") + "<br /> " + Eval("EmailID2")%>
        </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField HeaderText="IndustryType" ItemStyle-HorizontalAlign="Left" 
                DataField="IndustryType" ReadOnly="true">
            </asp:BoundField>
        </Fields>
            <AlternatingRowStyle BackColor="White" />
        </asp:DetailsView>
    </td></tr>
     <tr><td><h3>Business Area and Products</h3></td></tr>
    <tr><td>
        <asp:GridView ID="gvBusiness" AutoGenerateColumns="false" runat="server" Font-Names="Arial" BorderColor="#006633" runat="server">
        <Columns>
        <asp:BoundField HeaderText="Sl.No." ItemStyle-Wrap="true" DataField="SlNo" />
        <asp:BoundField HeaderText="Business Area"  ItemStyle-Wrap="true" DataField="BusinessArea" />
        <asp:BoundField HeaderText="Products" ItemStyle-Wrap="true" DataField="Products" />
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
    <tr><td><h3>Contact Details</h3></td></tr>
    <tr><td>
        <asp:GridView ID="gvContact" AutoGenerateColumns="false" runat="server" Font-Names="Arial" BorderColor="#006633" runat="server">
        <Columns>
        <asp:BoundField HeaderText="Sl.No." ItemStyle-Wrap="true" DataField="SlNo" />
        <asp:BoundField HeaderText="Contact Person Name" ItemStyle-Wrap="true" DataField="ContactPersonName" />
        <asp:BoundField HeaderText="Business Area" ItemStyle-Wrap="true" DataField="BusinessArea" />
        <asp:TemplateField HeaderText="Address" ItemStyle-Wrap="true">
        <ItemTemplate>
        <%#Eval("Address1") + "<br />" + Eval("Address2")%>
        </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Contact Details" ItemStyle-HorizontalAlign="Left">
        <ItemTemplate>
        <%# "Phone No. : " + Eval("PhoneNo1") + "<br /> " + Eval("PhoneNo2") + "<br /> Email ID : " + Eval("EmailID") %>
        </ItemTemplate>
        </asp:TemplateField>
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
