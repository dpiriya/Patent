<%@ Page Title="" Language="C#" MasterPageFile="~/Parent.master" AutoEventWireup="true" CodeFile="ReportSelection4.aspx.cs" Inherits="ReportSelection4" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="Server">
    <link href="Styles/newEntry.css" rel="Stylesheet" type="text/css" />
    <script src="Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script src="Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="Scripts/jquery.maskedinput-1.2.2.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {

        });
    </script>
    <style type="text/css">
        .style1 {
            width: 547px;
        }

        .style2 {
            height: 40px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">
    <table align="center" class="neTable" cellspacing="3px" width="95%">
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
        <tr>
            <td runat="server" id="lblTitle" align="center" valign="middle" style="font-size: medium; text-decoration: underline;">CONTRACT REPORT</td>
        </tr>
        <tr>
            <td class="tdTable1">
                <table align="center" width="50%" cellpadding="10px" cellspacing="3px">
                    <tr>
                        <td>
                            <asp:Label ID="lblField" runat="server"></asp:Label><br />
                            <asp:ListBox ID="lstField" runat="server"
                                OnSelectedIndexChanged="lstField_SelectedIndexChanged" AutoPostBack="true"></asp:ListBox></td>
                        <td style="width: 50%" valign="top">
                            <asp:Label ID="lblValue" runat="server"></asp:Label><br />
                            <asp:DropDownList ID="ddlListItem" runat="server" Visible="false">
                            </asp:DropDownList>
                            <asp:TextBox ID="txtValue" runat="server" Visible="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr align="center">
                        <td colspan="2">
                            <asp:ImageButton ID="imgBtnReport" runat="server"
                                ImageUrl="~/Images/Report2.png" OnClick="imgBtnReport_Click" />&nbsp;
         <asp:ImageButton ID="imgBtnClear" runat="server"
             ImageUrl="~/Images/Clear2.png" OnClick="imgBtnClear_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>


        <tr>
            <td>
                <asp:GridView ID="gvResult" Width="100%" AutoGenerateColumns="false"
                    runat="server" OnSelectedIndexChanging="gvResult_SelectedIndexChanging">
                    <Columns>
                        <asp:TemplateField HeaderStyle-Font-Underline="true" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderText="Contract No" SortExpression="ContractNo">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnSelect" Text='<%#Eval("ContractNo")%>' Font-Underline="true" CommandName="Select" runat="server"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderStyle-Font-Underline="true" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" ItemStyle-Wrap="true" DataField="Title" HeaderText="Title" SortExpression="Title" />
                        <asp:BoundField HeaderStyle-Font-Underline="true" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="true" DataField="Scope" HeaderText="Scope" SortExpression="Scope" />
                        <asp:BoundField HeaderStyle-Font-Underline="true" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="true" DataField="Party" HeaderText="Party" SortExpression="Party" />
                        <asp:BoundField HeaderStyle-Font-Underline="true" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="true" DataField="CoordinatingPerson" HeaderText="Coordinating Person" SortExpression="CoordinatingPerson" />
                        <asp:TemplateField HeaderStyle-Font-Underline="true" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderText="Action and Document">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnAction" Text="View" Font-Underline="true" CommandName="View" OnClick="lbtnAction_Click" runat="server"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle ForeColor="#dffaa4" HorizontalAlign="Center" BackColor="#A55129" BorderStyle="Solid" BorderWidth="2" BorderColor="#A55129" VerticalAlign="Middle" Font-Size="13px" Height="35px" />
                    <RowStyle BackColor="White" HorizontalAlign="Left" Height="25px" Font-Size="12px" Wrap="true" />
                    <AlternatingRowStyle Wrap="true" HorizontalAlign="Left" BackColor="#E9CEA4" />
                    <PagerSettings Mode="Numeric" PageButtonCount="9" Position="TopAndBottom" />
                    <PagerStyle Font-Underline="true" Font-Bold="true" Font-Italic="true" ForeColor="Red" />
                </asp:GridView>
            </td>
        </tr>


    </table>
</asp:Content>

