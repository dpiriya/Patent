<%@ Page Title="" Language="C#" MasterPageFile="~/Parent.master" AutoEventWireup="true" CodeFile="ModifyRenwal.aspx.cs" Inherits="Default2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="Server">
    <title></title>
    <link href="Styles/TableStyle2.css" rel="Stylesheet" type="text/css" />
    <script src="Scripts/jquery-1.9.1.js" type="text/javascript"></script>
    <script src="Scripts/jquery-ui-1.10.3.custom.min.js" type="text/javascript"></script>
    <link href="Styles/jquery-ui-1.10.3.custom.min.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>

    <table align="center" class="neTable" cellpadding="12px" width="970px" cellspacing="10px">

        <tr>
            <td align="center" valign="middle"
                style="font-size: medium; color: #994c00; text-decoration: underline;">P&M FOLLOW UP</td>
        </tr>
        <tr>
            <td align="center">IDF NO<asp:DropDownList ID="ddlIDFNo" runat="server"
                AutoPostBack="true" EnableViewState="true">
            </asp:DropDownList>
                <asp:DropDownList ID="ddlphase" runat="server"
                AutoPostBack="true" EnableViewState="true">
            </asp:DropDownList>
                <asp:Button
                    ID="btnFind" runat="server" CssClass="submitButton" Text="Find"
                    OnClick="btnFind_Click" />&nbsp;&nbsp;
        <asp:Button ID="btnClear" runat="server" CssClass="submitButton" Text="Clear" OnClick="btnClear_Click" /></td>
        </tr>
    </table>

    <asp:Panel ID="panUpdate" CssClass="borderStyle" Width="100%" runat="server" Visible="false">
        <table style="width: 100%; border-width: 1px; border-color: #666; border-style: solid;" class="MVStyle" cellspacing="10px" id="Table11">
            <tr>
                <td align="right">
                    <asp:Button ID="btnNewAction" runat="server"
                        Text="New Action" BackColor="Transparent" Font-Names="Arial" ForeColor="#9f5a0b"
                        Font-Bold="true" BorderStyle="None" Font-Underline="true" /></td>
            </tr>
            <tr>
                <td>SLNo</td>
                <td>
                    <asp:TextBox ID="txtSlno" runat="server" Height="22px" Width="38px"></asp:TextBox><asp:Label ID="tmpLabel" runat="server"></asp:Label></td>
                <td>Description</td>
                <td>
                    <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine"
                        Width="245px"></asp:TextBox></td>
            </tr>

            <tr>
                <td>Amount In FC</td>
                <td>
                    <asp:TextBox ID="txtAmtInFc" runat="server" Width="100px"></asp:TextBox></td>
                <td>Amount In INR</td>
                <td>
                    <asp:TextBox ID="txtAmtInINR" runat="server" Width="100px"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Due Date</td>
                <td>
                    <asp:TextBox ID="txtDueDate" runat="server" Width="100px"></asp:TextBox>
                    &nbsp;<asp:Image ID="Image2" runat="server" ImageUrl="~/Images/GreenCalendar.gif" />
                    <asp:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd/MM/yyyy"
                        PopupButtonID="imgExpiry" TargetControlID="txtDueDate">
                    </asp:CalendarExtender></td>
                    <td>Amount(OnCondonation)</td>
                    <td>
                        <asp:TextBox ID="txtAmtOnCond" runat="server"
                            Width="100px"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Due Date(Restoration)</td>
                <td>
                    <asp:TextBox
                        ID="txtDueDateResto" runat="server" Width="100px"></asp:TextBox>
                    &nbsp;<asp:Image ID="imgEffective" runat="server"
                        ImageUrl="~/Images/GreenCalendar.gif" />
                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                        PopupButtonID="imgEffective" TargetControlID="txtDueDateResto">
                    </asp:CalendarExtender>
                </td>
                <td>Responsiblity</td>
                <td>
                    <asp:DropDownList ID="ddlResponsiblity" runat="server">
                        <asp:ListItem>--Select--</asp:ListItem>
                        <asp:ListItem>IITM</asp:ListItem>
                        <asp:ListItem>3rd Party</asp:ListItem>
                        <asp:ListItem>Licencee</asp:ListItem>
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td>Sharing Party</td>
                <td>
                    <asp:TextBox ID="txtSharingParty" runat="server" Width="150px"></asp:TextBox></td>
                <td>Percentage(%)</td>
                <td>
                    <asp:TextBox ID="txtPercentage" runat="server" Width="100px"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Intimation Date</td>
                <td>
                    <asp:TextBox ID="txtIntimationDt" runat="server" Width="100px"></asp:TextBox>
                    &nbsp;<asp:Image ID="Image1" runat="server" ImageUrl="~/Images/GreenCalendar.gif" />
                    <asp:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy"
                        PopupButtonID="imgRequest" TargetControlID="txtIntimationDt">
                    </asp:CalendarExtender>
                </td>

                <td>Confirm Date</td>
                <td>
                    <asp:TextBox ID="txtConfirmDt" runat="server" Width="100px"></asp:TextBox>
                    &nbsp;<asp:Image ID="Image3" runat="server" ImageUrl="~/Images/GreenCalendar.gif" />
                    <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                        PopupButtonID="imgRequest" TargetControlID="txtConfirmDt">
                    </asp:CalendarExtender>
                </td>
            </tr>
            <tr>
                <td>Share Received</td>
                <td>
                    <asp:DropDownList ID="ddlShare" runat="server">
                        <asp:ListItem>--Select--</asp:ListItem>
                        <asp:ListItem>Yes</asp:ListItem>
                        <asp:ListItem>No</asp:ListItem>
                        <asp:ListItem>NA</asp:ListItem>
                    </asp:DropDownList></td>
                <td>PO PaymentDate</td>
                <td>
                    <asp:TextBox ID="txtPaymentDate" runat="server" Width="100px"></asp:TextBox>
                    &nbsp;<asp:Image ID="Image4" runat="server" ImageUrl="~/Images/GreenCalendar.gif" />
                    <asp:CalendarExtender ID="CalendarExtender5" runat="server" Format="dd/MM/yyyy"
                        PopupButtonID="imgRequest" TargetControlID="txtPaymentDate">
                    </asp:CalendarExtender>
                </td>
            </tr>
            <tr>
                <td>Status</td>
                <td>
                    <asp:DropDownList ID="dropstatus" runat="server">
                        <asp:ListItem>--Select--</asp:ListItem>
                        <asp:ListItem>Renewed</asp:ListItem>
                        <asp:ListItem>Not Renewed</asp:ListItem>
                        <asp:ListItem>Surrender</asp:ListItem>
                        <asp:ListItem>Pending</asp:ListItem>
                        <asp:ListItem>Not due</asp:ListItem>
                         <asp:ListItem>Complete</asp:ListItem>
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    <asp:Button ID="btnActSave" runat="server" CssClass="submitButton" Text="Save" />

                    &nbsp;&nbsp;
                    <asp:Button ID="btnActUpdate" runat="server" CssClass="submitButton"
                        Text="Update" OnClick="btnActUpdate_Click" />
                    &nbsp;&nbsp;
                    <asp:Button ID="btnClose" runat="server" CssClass="submitButton"
                        Text="Close" OnClick="btnClose_Click" />
                </td>
            </tr>          
        </table>
    </asp:Panel>
    <asp:Panel ID="PanelList" CssClass="borderStyle" Visible="false" Width="100%" runat="server">
        <table>
            <tr>
                <td class="style4">
                    <div>
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        List of Action Plans
                    </div>
                </td>
            </tr>
            <tr>
                <td class="style4">
                    <asp:ListView ID="lvAction" runat="server" OnItemCommand="lvAction_ItemCommand"
                        OnItemDataBound="lvAction_ItemDataBound" OnItemDeleting="lvAction_ItemDeleting">


                        <LayoutTemplate>
                            <table id="Table1" width="1000px" align="center" runat="server" style="text-align: left; border-color: #994c00; border-style: solid; border-width: 1px;" cellpadding="5" cellspacing="0" border="1">
                                <tr id="Tr1" runat="server" style="background-color: #d2691e; color: #fffff0">
                                    <th id="Th1" align="center" runat="server">SLNo</th>
                                     <th id="Th2" align="center" runat="server">Description</th>
                                    <th id="Th3" align="center" runat="server">Amt FC</th>
                                    <th id="Th4" align="center" runat="server">Amt INR</th>
                                    <th id="Th5" align="center" runat="server">Due Date</th>
                                    <%--<th id="Th6" align="center" runat="server">con_Amt</th>
                                    <th id="Th7" align="center" runat="server">Resto_Date</th>--%>
                                    <th id="Th8" align="center" runat="server">Responsibility</th>
                                    <th id="Th6" align="center" runat="server">Sharing party</th>
                                    <th id="Th9" align="center" runat="server">Inti_Date</th>
                                    <th id="Th10" align="center" runat="server">ConfirmDt</th>
                                    <th id="Th7" align="center" runat="server">Share received</th>
                                    <th id="Th11" align="center" runat="server">POPayment Dt</th>
                                    <th id="Th12" runat="server">Status</th>
                                </tr>
                                <tr runat="server" id="itemPlaceholder" />
                            </table>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <tr id="Tr2" runat="server" style="background-color: White">
                                <td>
                                    <asp:Label ID="lblSlNo" runat="server" BackColor="Transparent" BorderStyle="None" Width="20px" Text='<%#Eval("SLNo")%>'></asp:Label></td>
                                <%--<td><asp:Label ID="lblFileNo" runat="server" BackColor="Transparent" BorderStyle="None" Width="20px" Text='<%#Eval("FileNo")%>'></asp:Label></td>--%>
                                <td>
                                    <asp:Label ID="Descrip" runat="server" BackColor="Transparent" BorderStyle="None" Width="50px" Text='<%#Eval("Description")%>'></asp:Label></td>
                                <td>
                                    <asp:Label ID="lblAmtInFC" runat="server" BackColor="Transparent" BorderStyle="None" Width="20px" Text='<%#Eval("AmtInFC")%>'></asp:Label></td>
                                <td>
                                    <asp:Label ID="lblAmtInINR" runat="server" BackColor="Transparent" BorderStyle="None" Width="30px" Text='<%#Eval("AmtInINR")%>'></asp:Label></td>
                                <td>
                                    <asp:Label ID="lblDDueate" runat="server" BackColor="Transparent" BorderStyle="None" Width="40px" Text='<%#Eval("DueDate")%>'></asp:Label></td>
                               <%-- <td>
                                    <asp:Label ID="lblAmt_InCond" runat="server" BackColor="Transparent" BorderStyle="None" Width="40px" Text='<%#Eval("Amt_InCond")%>'></asp:Label></td>
                                <td>
                                    <asp:Label ID="lbDueDate_Restoration" runat="server" BackColor="Transparent" BorderStyle="None" Width="40px" Text='<%#Eval("DueDate_Restoration")%>'></asp:Label></td>--%>
                                <td>
                                    <asp:Label ID="lblResponsibility" runat="server" BackColor="Transparent" BorderStyle="None" Width="20px" Text='<%#Eval("Responsibility")%>'></asp:Label></td>
                                <td>
                                    <asp:Label ID="lblSh_party" runat="server" BackColor="Transparent" BorderStyle="None" Width="50px" Text='<%#Eval("Sharing_Party")%>'></asp:Label></td>
                                <td>
                                    <asp:Label ID="lblIntimation_Date" runat="server" BackColor="Transparent" BorderStyle="None" Width="50px" Text='<%#Eval("Intimation_Date")%>'></asp:Label></td>
                                <td>
                                    <asp:Label ID="lblConfirmation_Date" runat="server" BackColor="Transparent" BorderStyle="None" Width="50px" Text='<%#Eval("Confirmation_Date")%>'></asp:Label></td>
                                <td>
                                    <asp:Label ID="lblsh_received" runat="server" BackColor="Transparent" BorderStyle="None" Width="15px" Text='<%#Eval("Share_Received")%>'></asp:Label></td>
                                <td>
                                    <asp:Label ID="lblPOPaymentDate" runat="server" BackColor="Transparent" BorderStyle="None" Width="50px" Text='<%#Eval("POPaymentDate")%>'></asp:Label></td>
                                <td>
                                    <asp:Label ID="lblStatus" runat="server" BackColor="Transparent" BorderStyle="None" Width="50px" Text='<%#Eval("Status")%>'></asp:Label></td>
                                

                                <td>
                                    <asp:LinkButton ID="lbtnUpdate" Font-Underline="true" CommandName="Modify" runat="server">Update</asp:LinkButton>
                                    <asp:LinkButton ID="lbtnDelete" Font-Underline="true" OnClientClick="return DeleteVerify()" CommandName="delete" runat="server">Delete</asp:LinkButton>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <tr id="Tr3" runat="server" style="background-color: #F7F7DE">
                                <td>
                                    <asp:Label ID="lblSlNo" runat="server" BackColor="Transparent" BorderStyle="None" Width="20px" Text='<%#Eval("SLNo")%>'></asp:Label></td>
                                <td> <asp:Label ID="Descrip" runat="server" BackColor="Transparent" BorderStyle="None" Width="50px" Text='<%#Eval("Description")%>'></asp:Label></td>

                                
                                <td>
                                    <asp:Label ID="lblAmtInFC" runat="server" BackColor="Transparent" BorderStyle="None" Width="20px" Text='<%#Eval("AmtInFC")%>'></asp:Label></td>
                                <td>
                                    <asp:Label ID="lblAmtInINR" runat="server" BackColor="Transparent" BorderStyle="None" Width="30px" Text='<%#Eval("AmtInINR")%>'></asp:Label></td>
                                <td>
                                    <asp:Label ID="lblDDueate" runat="server" BackColor="Transparent" BorderStyle="None" Width="40px" Text='<%#Eval("DueDate")%>'></asp:Label></td>
                                <%--<td>
                                    <asp:Label ID="lblAmt_InCond" runat="server" BackColor="Transparent" BorderStyle="None" Width="40px" Text='<%#Eval("Amt_InCond")%>'></asp:Label></td>
                                <td>
                                    <asp:Label ID="lbDueDate_Restoration" runat="server" BackColor="Transparent" BorderStyle="None" Width="90px" Text='<%#Eval("DueDate_Restoration")%>'></asp:Label></td>--%>
                                <td>
                                    <asp:Label ID="lblResponsibility" runat="server" BackColor="Transparent" BorderStyle="None" Width="20px" Text='<%#Eval("Responsibility")%>'></asp:Label></td>
                                <td>
                                    <asp:Label ID="lblSh_party" runat="server" BackColor="Transparent" BorderStyle="None" Width="50px" Text='<%#Eval("Sharing_Party")%>'></asp:Label></td>
                                <td>
                                    <asp:Label ID="lblIntimation_Date" runat="server" BackColor="Transparent" BorderStyle="None" Width="50px" Text='<%#Eval("Intimation_Date")%>'></asp:Label></td>
                                <td>
                                    <asp:Label ID="lblConfirmation_Date" runat="server" BackColor="Transparent" BorderStyle="None" Width="50px" Text='<%#Eval("Confirmation_Date")%>'></asp:Label></td>
                                 <td>
                                    <asp:Label ID="lblsh_received" runat="server" BackColor="Transparent" BorderStyle="None" Width="15px" Text='<%#Eval("Share_Received")%>'></asp:Label></td>
                                <td>
                                    <asp:Label ID="lblPOPaymentDate" runat="server" BackColor="Transparent" BorderStyle="None" Width="50px" Text='<%#Eval("POPaymentDate")%>'></asp:Label></td>
                                <td>
                                    <asp:Label ID="lblStatus" runat="server" BackColor="Transparent" BorderStyle="None" Width="50px" Text='<%#Eval("Status")%>'></asp:Label></td>
                                <td>
                                    <asp:LinkButton ID="lbtnUpdate" Font-Underline="true" CommandName="Modify" runat="server">Update</asp:LinkButton>
                                    <asp:LinkButton ID="lbtnDelete" Font-Underline="true" OnClientClick="return DeleteVerify()" CommandName="delete" runat="server">Delete</asp:LinkButton>
                                </td>
                            </tr>
                        </AlternatingItemTemplate>
                    </asp:ListView>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>

