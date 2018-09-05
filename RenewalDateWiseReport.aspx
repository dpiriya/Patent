<%@ Page Title="" Language="C#" MasterPageFile="~/Parent.master" AutoEventWireup="true" CodeFile="RenewalDateWiseReport.aspx.cs" Inherits="Default2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="Server">
    <link href="Styles/newEntry.css" rel="Stylesheet" type="text/css" />
    <script src="Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script src="Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="Scripts/jquery.maskedinput-1.2.2.min.js" type="text/javascript"></script>
    <style type="text/css">
        .style1 {
            width: 288px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">
    <table align="center" border="1">
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
        <tr>
            <td class="style1">From Date:<asp:TextBox runat="server" ID="txtFDate"></asp:TextBox>&nbsp;<asp:Image ID="imgFDate" ImageUrl="~/Images/GreenCalendar.gif" runat="server" />
                <asp:CalendarExtender ID="CalendarExtender1" TargetControlID="txtFDate" runat="server" PopupButtonID="imgFDate" Format="yyyy-MM-dd">
                </asp:CalendarExtender>
            </td>
            <td>To Date:<asp:TextBox runat="server" ID="txtTDate"></asp:TextBox>&nbsp;<asp:Image ID="imgTDate" ImageUrl="~/Images/GreenCalendar.gif" runat="server" />
                <asp:CalendarExtender ID="CalendarExtender2" TargetControlID="txtTDate" PopupButtonID="imgTDate" runat="server" Format="yyyy-MM-dd">
                </asp:CalendarExtender>
            </td>
        </tr>
        <tr>
            <td align="center" class="style1">Responsibility:<asp:DropDownList ID="ddlResponsibility"
                runat="server" Width="197px">
                <asp:ListItem>All</asp:ListItem>
                <asp:ListItem>IITM</asp:ListItem>
                <asp:ListItem>3rd Party</asp:ListItem>
                <asp:ListItem>Licencee</asp:ListItem>
            </asp:DropDownList></td>
            <td>Status:&nbsp;&nbsp;<asp:DropDownList ID="ddlstatus" runat="server" Width="137px">
                <asp:ListItem>All</asp:ListItem>
                <asp:ListItem>Pending</asp:ListItem>
                <asp:ListItem>Dropped</asp:ListItem>
                <asp:ListItem>Renewed</asp:ListItem>
                <asp:ListItem>Not Renewed</asp:ListItem>
                <asp:ListItem>Surrender</asp:ListItem>                
                <asp:ListItem>Complete</asp:ListItem>   
            </asp:DropDownList></td>
        </tr>
        <tr>
            <td>Phase: &nbsp;&nbsp;<asp:DropDownList ID="ddlPhase" runat="server">
                <asp:ListItem>Prosecution</asp:ListItem>
                <asp:ListItem>Maintenance</asp:ListItem>
                <asp:ListItem>Both</asp:ListItem>
                                   </asp:DropDownList></td>
        </tr>
        <tr>
            <td align="center" class="style1" colspan="4" align="center">
                <%-- <asp:ImageButton ID="ImageReportButn1" runat="server" 
             ImageUrl="~/Images/Report2.png" onclick="ImageReportButn1_Click"/>--%>
                <asp:Button ID="ImageReportButn1" runat="server" Text="Part-A"
                    OnClick="ImageReportButn1_Click1" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
              <asp:Button ID="ImageReportButn" runat="server" Text="Part-B"
                  OnClick="ImageReportButn_Click" />
                <asp:ImageButton
                    ID="ImageButton2" runat="server"
                    ImageUrl="~/Images/Clear2.png" />&nbsp;&nbsp;</td>
        </tr>

    </table>
    <table width="100%">
        <tr>
            <td align="center">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:GridView ID="GvReportDate" Width="100%" AutoGenerateColumns="false"
                    runat="server">
                    <Columns>
                        <asp:BoundField HeaderStyle-Font-Underline="true" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" ItemStyle-Wrap="true" DataField="FileNo" HeaderText="FileNo" SortExpression="FileNo" />
                        <asp:BoundField HeaderStyle-Font-Underline="true" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" ItemStyle-Wrap="true" ItemStyle-Width="400" DataField="Description" HeaderText="Description" SortExpression="Description" />
                        <asp:BoundField HeaderStyle-Font-Underline="true" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" ItemStyle-Wrap="true" DataField="Responsibility" HeaderText="Responsibility" SortExpression="Responsibility" />
                        <asp:BoundField HeaderStyle-Font-Underline="true" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" ItemStyle-Wrap="true" DataField="DueDate" HeaderText="DueDate" SortExpression="DueDate" />
                        <asp:BoundField HeaderStyle-Font-Underline="true" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" ItemStyle-Wrap="true" DataField="AmtInFc" HeaderText="AmtInFc" SortExpression="AmtInFc" />
                        <asp:BoundField HeaderStyle-Font-Underline="true" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" ItemStyle-Wrap="true" DataField="AmtInINR" HeaderText="AmtInINR" SortExpression="AmtInINR" />
                        <asp:BoundField HeaderStyle-Font-Underline="true" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" ItemStyle-Wrap="true" ItemStyle-Width="200" DataField="Status" HeaderText="Status" SortExpression="Status" />
                    </Columns>
                    <HeaderStyle ForeColor="#dffaa4" HorizontalAlign="Center" BackColor="#A55129" BorderStyle="Solid" BorderWidth="2" BorderColor="#A55129" VerticalAlign="Middle" Font-Size="13px" Height="35px" />
                    <RowStyle BackColor="White" HorizontalAlign="Left" Height="25px" Font-Size="12px" Wrap="true" />
                    <AlternatingRowStyle Wrap="true" HorizontalAlign="Left" BackColor="#E9CEA4" />
                    <PagerSettings Mode="Numeric" PageButtonCount="9" Position="TopAndBottom" />
                    <PagerStyle Font-Underline="true" Font-Bold="true" Font-Italic="true" ForeColor="Red" />
                </asp:GridView>
            </td>
        </tr>


        <tr>
            <td align="center">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:GridView ID="GridReportAmount" Width="100%" AutoGenerateColumns="false"
                    runat="server">
                    <Columns>
                        <asp:BoundField HeaderStyle-Font-Underline="true" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" ItemStyle-Wrap="true" DataField="FileNo" HeaderText="FileNo" SortExpression="FileNo" />
                        <asp:BoundField HeaderStyle-Font-Underline="true" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="200px" ItemStyle-HorizontalAlign="left" ItemStyle-Wrap="true" DataField="Description" HeaderText="Description" SortExpression="Description" />
                        <asp:BoundField HeaderStyle-Font-Underline="true" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" ItemStyle-Wrap="true" DataField="Responsibility" HeaderText="Responsibility" SortExpression="Responsibility" />                        
                        <asp:BoundField HeaderStyle-Font-Underline="true" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" ItemStyle-Wrap="true" DataField="DueDate" HeaderText="DueDate" SortExpression="DueDate" />                        
                        <asp:BoundField HeaderStyle-Font-Underline="true" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" ItemStyle-Wrap="true" DataField="AmtInINR" HeaderText="AmtInINR" SortExpression="AmtInINR" />
                        <asp:BoundField HeaderStyle-Font-Underline="true" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" ItemStyle-Wrap="true" DataField="Sharing_Party" HeaderText="Sharing_Party" SortExpression="Sharing_Party" />
                        <asp:BoundField HeaderStyle-Font-Underline="true" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" ItemStyle-Wrap="true" DataField="Intimation_Date" HeaderText="Inti_Date" SortExpression="Intimation_Date" />
                        <asp:BoundField HeaderStyle-Font-Underline="true" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" ItemStyle-Wrap="true" DataField="Confirmation_Date" HeaderText="Conf_Date" SortExpression="Confirmation_Date" />
                        <asp:BoundField HeaderStyle-Font-Underline="true" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" ItemStyle-Wrap="true" DataField="Share_Received" HeaderText="Share_Received" SortExpression="Share_Received" />

                    </Columns>
                    <HeaderStyle ForeColor="#dffaa4" HorizontalAlign="Center" BackColor="#A55129" BorderStyle="Solid" BorderWidth="2" BorderColor="#A55129" VerticalAlign="Middle" Font-Size="13px" Height="35px" />
                    <RowStyle BackColor="White" HorizontalAlign="Left" Height="25px" Font-Size="12px" Wrap="true" />
                    <AlternatingRowStyle Wrap="true" HorizontalAlign="Left" BackColor="#E9CEA4" />
                    <PagerSettings Mode="Numeric" PageButtonCount="9" Position="TopAndBottom" />
                    <PagerStyle Font-Underline="true" Font-Bold="true" Font-Italic="true" ForeColor="Red" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:Label ID="lblmsg" runat="server" Text="No Record Exist" Font-Bold="true" Visible="false"></asp:Label></td>
        </tr>
    </table>
</asp:Content>







