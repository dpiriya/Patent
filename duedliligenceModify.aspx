<%@ Page Language="C#" MasterPageFile="~/Parent.master" AutoEventWireup="true" CodeFile="duedliligenceModify.aspx.cs" Inherits="duedliligenceModify" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="Server">
    <link href="Styles/newEntry.css" rel="Stylesheet" type="text/css" />
    <%--<link href="Content/bootstrap.min.css" rel="stylesheet" />--%>
    <link href="Styles/jquery-ui-1.10.3.custom.min.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">
    <script src="packages/jQuery.3.3.1/Content/Scripts/jquery-3.3.1.js" type="text/javascript"></script>
    <script src="Scripts/jquery-ui-1.10.3.custom.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            $(".date").datepicker({
                changeMonth: true,
                changeYear: true,
                inline: true
            });
        });
    </script>
    <table align="center">
        <tr align="center">
            <td colspan="4" style="font-size: 18px; color: Maroon; padding-bottom: 30px">
                <asp:Label runat="server" ID="title">Modify DueDiligence</asp:Label>
            </td>
        </tr>

        <tr>
            <td>
                <asp:Label runat="server" Font-Size="15px">FileNo</asp:Label></td>
            <td>
                <asp:DropDownList runat="server" AutoPostBack="true" ID="ddlidf" OnSelectedIndexChanged="btnFind_Click"></asp:DropDownList></td>
            <td>
                <asp:Button ID="btnFind" runat="server" CssClass="submitButton" Text="Find" OnClick="btnFind_Click" /></td>
        </tr>
    </table>
    <asp:Panel ID="panelList" CssClass="borderStyle" Visible="false" Width="100%" runat="server">
        <table>
            <tr>
                <td class="style4">
                    <div>
                        <br />
                        <br />
                        <br />
                        <br />
                        List of Search Reports
                    </div>
                </td>
            </tr>
            <tr>
                <td class="style4">
                    <asp:ListView ID="lvSearch" runat="server" OnItemCommand="lvSearch_ItemCommand" OnItemDataBound="lvSearch_ItemDataBound"
                        OnItemDeleting="lvSearch_ItemDeleting">
                        <LayoutTemplate>
                            <table id="Table1" width="1000px" align="center" runat="server" style="text-align=leftborder-color: #994c00; border-style: solid; border-width: 1px;" cellpadding="5" cellspacing="0" border="1">
                                <tr id="Tr1" runat="server" style="background-color: #d2691e; color: #fffff0">
                                    <th id="Th1" align="center" runat="server">SNo</th>
                                    <th id="Th2" align="center" runat="server">Type</th>
                                    <th id="Th3" align="center" runat="server">Outsource</th>
                                    <th id="Th4" align="center" runat="server">Allocation</th>
                                    <th id="Th5" align="center" runat="server">Report Date</th>
                                    <th id="Th6" align="center" runat="server">Link</th>
                                </tr>
                                <tr runat="server" id="itemPlaceholder" />
                            </table>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <tr id="Tr2" runat="server" style="background-color: White">
                                <td>
                                    <asp:Label ID="lblSNo" runat="server" BackColor="Transparent" BorderStyle="None" Width="20px" Text='<%#Eval("Sno")%>'></asp:Label></td>
                                <td>
                                    <asp:Label ID="lbltype" runat="server" BackColor="Transparent" BorderStyle="None" Width="50px" Text='<%#Eval("ReportType")%>'></asp:Label></td>
                                <td>
                                    <asp:Label ID="lblMode" runat="server" BackColor="Transparent" BorderStyle="None" Width="50px" Text='<%#Eval("Mode")%>'></asp:Label></td>
                                <td>
                                    <asp:Label ID="lblAllocation" runat="server" BackColor="Transparent" BorderStyle="None" Width="50px" Text='<%#Eval("Allocation")%>'></asp:Label></td>
                                <td>
                                    <asp:Label ID="lblrptdt" runat="server" BackColor="Transparent" BorderStyle="None" Width="50px" Text='<%#Eval("ReportDt")%>'></asp:Label></td>
                                <td>
                                    <asp:LinkButton ID="lbtnEdit" Font-Underline="true" CommandName="Modify" runat="server">Edit</asp:LinkButton>
                                    <asp:LinkButton ID="lbtnDelete" Font-Underline="true" OnClientClick="return DeleteVerify()" CommandName="delete" runat="server">Delete</asp:LinkButton></td>
                            </tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <tr id="Tr3" runat="server" style="background-color: #F7F7DE">
                                <td>
                                    <asp:Label ID="lblSNo" runat="server" BackColor="Transparent" BorderStyle="None" Width="20px" Text='<%#Eval("Sno")%>'></asp:Label></td>
                                <td>
                                    <asp:Label ID="lbltype" runat="server" BackColor="Transparent" BorderStyle="None" Width="50px" Text='<%#Eval("ReportType")%>'></asp:Label></td>
                                <td>
                                    <asp:Label ID="lblMode" runat="server" BackColor="Transparent" BorderStyle="None" Width="50px" Text='<%#Eval("Mode")%>'></asp:Label></td>
                                <td>
                                    <asp:Label ID="lblAllocation" runat="server" BackColor="Transparent" BorderStyle="None" Width="50px" Text='<%#Eval("Allocation")%>'></asp:Label></td>
                                <td>
                                    <asp:Label ID="lblrptdt" runat="server" BackColor="Transparent" BorderStyle="None" Width="20px" Text='<%#Eval("ReportDt")%>'></asp:Label></td>

                                <td>
                                    <asp:LinkButton ID="lbtnEdit" Font-Underline="true" CommandName="Modify" runat="server">Edit</asp:LinkButton>
                                    <asp:LinkButton ID="lbtnDelete" Font-Underline="true" OnClientClick="return DeleteVerify()" CommandName="delete" runat="server">Delete</asp:LinkButton></td>
                            </tr>
                        </AlternatingItemTemplate>
                    </asp:ListView>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="panUpdate" CssClass="borderStyle" Width="100%" runat="server" Visible="false">
        <table style="width: 100%; border-width: 1px; border-color: #666; border-style: solid;" class="MVStyle" cellspacing="10px" id="Table11">
           <%-- <tr>
                <td align="right">
                    <asp:Button ID="btnNewAction" runat="server"
                        Text="New Action" BackColor="Transparent" Font-Names="Arial" ForeColor="#9f5a0b"
                        Font-Bold="true" BorderStyle="None" Font-Underline="true" /></td>
            </tr>--%>
            <tr>
                <td>SNo</td>
                <td>
                    <asp:TextBox ID="txtSno" runat="server" Height="22px" Width="38px"></asp:TextBox><asp:Label ID="lblsno" runat="server"></asp:Label></td>
                <td>Description</td>
                <td>
                    <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine"
                        Width="245px"></asp:TextBox></td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
