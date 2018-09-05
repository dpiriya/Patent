<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ContractActionAndFile.aspx.cs" Inherits="ContractActionAndFile" %>

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
    <tr><td><h3>Contract Action Details</h3></td></tr>
    <tr><td>
        <asp:GridView ID="gvAction" AutoGenerateColumns="false" CellPadding="4" CellSpacing="5" runat="server" Font-Names="Arial" BorderColor="#006633">
        <Columns>
        <%--<asp:BoundField HeaderText="Action Type" DataField="ActionType" />
        <asp:BoundField HeaderText="Action Description"  ItemStyle-Wrap="true" DataField="Narration" />
        <asp:BoundField HeaderText="Clause Ref." DataField="ClauseRef" />
        <asp:BoundField HeaderText="Response Person" DataField="ResponsePerson" />
        <asp:BoundField HeaderText="TargetDt" DataField="TargetDt" DataFormatString="{0:dd/MM/yyy}" />
        <asp:BoundField HeaderText="Remark" ItemStyle-Wrap="true" DataField="Remark" />
        <asp:BoundField HeaderText="Action Status" DataField="ActionStatus" />
        --%>
        
<asp:BoundField HeaderText="SlNo" DataField="SlNo" />
       <%-- <asp:BoundField HeaderText="Term"  ItemStyle-Wrap="true" DataField="Event" />--%>
        <asp:TemplateField HeaderText="Term">
            <ItemTemplate>
                <asp:LinkButton ID="lbtnTerm" Text='<%#Eval("Event")%>' Font-Underline="true" OnClick="lbtnTerm_Click" runat="server"></asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField HeaderText="Type" DataField="Frequency" />
        <asp:BoundField HeaderText="ExecutionDate" DataField="ExecutionDate" DataFormatString="{0:dd/MM/yyy}" />
        <asp:BoundField HeaderText="Basis" ItemStyle-Wrap="true" DataField="Basis"  />
        <asp:BoundField HeaderText="DeclDueAmt" ItemStyle-Wrap="true" DataField="DeclDueAmt" />
        <asp:BoundField HeaderText="InvoiceNo" DataField="InvoiceNo" />
        <asp:BoundField HeaderText="InvoiceDate" DataField="InvoiceRaised" />
        <asp:BoundField HeaderText="RecieptNo" DataField="RecieptNo" />
        <asp:BoundField HeaderText="RecieptDate" DataField="RecieptDate" />
        <asp:BoundField HeaderText="Mode" DataField="Mode" />
        <asp:BoundField HeaderText="AmountInRs" DataField="AmountInRs" />
        <asp:BoundField HeaderText="Status" DataField="Status" />
        <asp:BoundField HeaderText="Remarks" DataField="Remarks" />
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
    <tr><td><h3>Contract Document Details</h3></td></tr>
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
