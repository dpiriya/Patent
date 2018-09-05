<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RenewalActionReport.aspx.cs" Inherits="RenewalActionReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table width="100%">
                <tr>
                    <td align="center">
                        <asp:Label ForeColor="#993333" Text="P&M Action Plan" Font-Bold="true" runat="server"></asp:Label>
                    </td>
                    <td align="right">
                        <asp:LinkButton ID="linkback" runat="server" Text="BACK" OnClick="linkback_Click"></asp:LinkButton>
                    </td>
                </tr>
             </table>           
            <table class="neTable" cellpadding="1px" width="970px" cellspacing="10px" style="border: 1px solid;align-content:center;width:100%" >
                <tr>
                    <td class="style4">IDF NO</td>
                    <td>
                        <asp:Label ID="txtIDF" runat="server"
                            Width="77px" Height="16px"></asp:Label></td>

                    <td class="style4">Title</td>
                    <td colspan="4">
                        <asp:Label ID="txttitle" runat="server" TextMode="MultiLine"
                            Width="495px"></asp:Label></td>
                </tr>
                <tr>
                    <td class="style4">Applicant</td>
                    <td>
                        <asp:Label ID="txtApplicant" runat="server" Width="215px"></asp:Label></td>
                    <td>Inventor</td>
                    <td>
                        <asp:Label ID="txtInventor" runat="server" Width="215px"></asp:Label></td>
                </tr>
                <tr>
                    <td>Industry </td>
                    <td>
                        <asp:Label ID="txtIndustry" runat="server" Width="215px"></asp:Label></td>
                    <td>Commercialized</td>
                    <td>
                        <asp:Label ID="txtcommer" runat="server" Width="215px"></asp:Label></td>
                </tr>
                <tr>
                    <td class="style4">Application No (Indian)</td>
                    <td>
                        <asp:Label ID="txtApplnNo" runat="server" Width="150px"></asp:Label></td>
                    <td>Application No(International)</td>
                    <td>
                        <asp:Label ID="txtApllicationNo" runat="server" Width="150px"></asp:Label></td>

                </tr>
                <tr>
                    <td class="style4">Filing Date (Indian)</td>
                    <td>
                        <asp:Label ID="txtFilingDt" runat="server" Width="150px"></asp:Label></td>

                    <td>Filing Date(International)</td>
                    <td>
                        <asp:Label ID="txtFilingDtInt" runat="server" Width="150px"></asp:Label></td>
                </tr>
                <tr>
                    <td class="style4">Patent No</td>
                    <td>
                        <asp:Label ID="txtPatentNo" runat="server" Width="150px"></asp:Label></td>
                    <td>Patent Date</td>
                    <td>
                        <asp:Label ID="txtPatentDate" runat="server" Width="150px"></asp:Label></td>
                </tr>
                <tr>
                    <td class="style4">Country</td>
                    <td>
                        <asp:Label ID="txtCountry" runat="server" Width="150px"></asp:Label></td>
                    <td class="style4">Status</td>
                    <td>
                        <asp:Label ID="txtStatus" runat="server" Width="150px"></asp:Label></td>
                    <tr>
                        <td class="style4">Sub-Status</td>
                        <td>
                            <asp:Label ID="txtSubStatus" runat="server" Width="215px" Height="18px"></asp:Label></td>
                        <td class="style4">
                            <asp:Label ID="textFile" runat="server" Width="93px"
                                Visible="false"></asp:Label></td>
                    </tr>
            </table>
            <table>                
                <tr>
                    <td>
                        <h3>P&M Action Details</h3>
                    </td>
                </tr>                
                <%--<asp:GridView ID="GridView1" AutoGenerateColumns="false" CellPadding="4" CellSpacing="5" runat="server" Font-Names="Arial" BorderColor="#006633">
        <Columns>
        
 <asp:BoundField HeaderText="FileNo"  ItemStyle-Wrap="true" DataField="FileNo" />
        <asp:BoundField HeaderText="Title" DataField="Title" />
   <%--     <asp:BoundField HeaderText="FirstApplicant" DataField="FirstApplicant" DataFormatString="{0:dd/MM/yyy}" />--%>
                <%--<asp:BoundField HeaderText="FirstApplicant" DataField="FirstApplicant"/>--%>
                <%-- <asp:BoundField HeaderText="Inventor1" ItemStyle-Wrap="true" DataField="Inventor1"  />
        <asp:BoundField HeaderText="Applcn_no" ItemStyle-Wrap="true" DataField="Applcn_no" />
        <asp:BoundField HeaderText="Filing_dt" DataField="Filing_dt" />
        <asp:BoundField HeaderText="Pat_no" DataField="Pat_no" />
        <asp:BoundField HeaderText="Pat_dt" DataField="Pat_dt" />
        <asp:BoundField HeaderText="Status" DataField="Status" />
        <asp:BoundField HeaderText="Sub_Status" DataField="Sub_Status" />
        <asp:BoundField HeaderText="Industry1" DataField="Industry1" />
        <asp:BoundField HeaderText="Commercialized" DataField="Commercialized" />--%>

                <%--</Columns>--%>
                <%-- <EmptyDataTemplate>
         <p style="text-align:center; background-color:#C8C8C8">
         <asp:Image ID="Image2" BorderStyle="None" BackColor="#C8C8C8" ImageUrl="~/Images/NoData.jpg" runat="server" />
         </p>
         </EmptyDataTemplate>--%>
                <%--<HeaderStyle HorizontalAlign="Center" BackColor="#006633" ForeColor="YellowGreen" VerticalAlign="Middle" Font-Size="14px" Height="35px" />
         <RowStyle BackColor="White" Height="25px" Font-Size="13px" Wrap="true"/>
         <AlternatingRowStyle Wrap="true" BackColor="#FFFFCC" />
         </asp:GridView>--%>

                <asp:GridView ID="GridActionPlan" AutoGenerateColumns="false" runat="server" Font-Names="Arial" BorderColor="#006633">
                    <Columns>

                        <asp:BoundField HeaderText="FileNo" ItemStyle-Wrap="true" DataField="FileNo" />
                        <asp:BoundField HeaderText="Description" ItemStyle-Width="300" DataField="Description" />
                        <%--     <asp:BoundField HeaderText="FirstApplicant" DataField="FirstApplicant" DataFormatString="{0:dd/MM/yyy}" />--%>
                        <asp:BoundField HeaderText="AmtInFC" DataField="AmtInFC" />
                        <asp:BoundField HeaderText="AmtInINR" ItemStyle-Wrap="true" DataField="AmtInINR" />
                        <asp:BoundField HeaderText="DueDate" ItemStyle-Wrap="true" DataField="DueDate" />
                        <asp:BoundField HeaderText="Amt_InCond" DataField="Amt_InCond" />
                        <asp:BoundField HeaderText="Resto_DueDate" DataField="DueDate_Restoration" />
                        <asp:BoundField ItemStyle-Width="1" HeaderText="Responsibility" DataField="Responsibility" />
                        <asp:BoundField HeaderText="Sharing_Party" DataField="Sharing_Party" />
                        <asp:BoundField HeaderText="Percentage" DataField="Percentage" />
                        <asp:BoundField HeaderText="Inti_Date" DataField="Intimation_Date" />
                        <asp:BoundField HeaderText="Confi_Date" DataField="Confirmation_Date" />
                        <asp:BoundField HeaderText="Share_Received" DataField="Share_Received" />
                        <asp:BoundField HeaderText="POPaymentDate" DataField="POPaymentDate" />
                        <asp:BoundField HeaderText="Status" DataField="Status" />
                    </Columns>
                    <%-- <EmptyDataTemplate>
         <p style="text-align:center; background-color:#C8C8C8">
         <asp:Image ID="Image2" BorderStyle="None" BackColor="#C8C8C8" ImageUrl="~/Images/NoData.jpg" runat="server" />
         </p>
         </EmptyDataTemplate>--%>
                    <HeaderStyle HorizontalAlign="Center" BackColor="#006633" ForeColor="YellowGreen" VerticalAlign="Middle" Font-Size="14px" Height="35px" Wrap="true" />
                    <RowStyle BackColor="White" Height="25px" Font-Size="13px" Wrap="true" />
                    <AlternatingRowStyle Wrap="true" BackColor="#FFFFCC" />
                </asp:GridView>
            </table>
        </div>
    </form>
</body>
</html>
