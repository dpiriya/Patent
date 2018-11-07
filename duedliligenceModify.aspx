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
        function DeleteVerify() {

            var r = confirm('Are you sure; Do you want to delete this record?');
            if (r == true) {
                return true;
            }
            else {
                return false;
            }
        }
        
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
                                    <th id="Th1" align="left" runat="server">SNo</th>
                                    <th id="Th2" align="left" runat="server">Type</th>                                    
                                    <th id="Th3" align="left" runat="server">Allocation</th>
                                    <th id="Th4" align="left" runat="server">Request Date</th>
                                    <th id="Th5" align="left" runat="server">Report Date</th>
                                    <th id="Th6" align="left" runat="server">Comments</th>
                                    <th id="Th7" align="left" runat="server">FileName</th>
                                    <th id="Th8" align="left" runat="server">File Upload /Delete</th>
                                    <th id="Th9" align="left" runat="server">Link</th>
                                </tr>
                                <tr runat="server" id="itemPlaceholder" />
                            </table>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <tr id="Tr2" runat="server" style="background-color: White">
                                <td>
                                    <asp:Label ID="lblSNo" runat="server" BackColor="Transparent" BorderStyle="None" Width="10px" Text='<%#Eval("Sno")%>'></asp:Label></td>
                                <td>
                                    <asp:Label ID="lbltype" runat="server" BackColor="Transparent" BorderStyle="None" Width="80px" Text='<%#Eval("ReportType")%>'></asp:Label></td>
                                <%--<td>
                                    <asp:Label ID="lblMode" runat="server" BackColor="Transparent" BorderStyle="None" Width="40px" Text='<%#Eval("Mode")%>'></asp:Label></td>--%>
                                <td>
                                    <asp:Label ID="lblAllocation" runat="server" BackColor="Transparent" BorderStyle="None" Width="40px" Text='<%#Eval("Allocation")%>'></asp:Label></td>
                                 <td>
                                    <asp:Label ID="lblreqdt" runat="server" BackColor="Transparent" BorderStyle="None" Width="40px" Text='<%#Eval("RequestDt", "{0:dd/MM/yyyy}")%>'></asp:Label></td>
                                <td>
                                    <asp:Label ID="lblrptdt" runat="server" BackColor="Transparent" BorderStyle="None" Width="40px" Text='<%#Eval("ReportDt", "{0:dd/MM/yyyy}")%>'></asp:Label></td>
                                <td><asp:Label ID="lblcomment" runat="server" BackColor="Transparent" BorderStyle="None" Width="20px" Text='<%#Eval("Comment")%>'></asp:Label></td>
                                <td>
                                    <asp:LinkButton ID="lbtnFileName" runat="server" BackColor="Transparent" BorderStyle="None" Width="20px" Text='<%#Eval("FileName")%>' OnClientClick="return AddUpload()" CommandName="View"></asp:LinkButton>
                                   <asp:FileUpload runat="server" ID="fp1" Visible="false" /></td>
                                <td style="width:10px;">
                                     <asp:ImageButton runat="server" ID="lbtnup" Visible="false" ImageUrl="~/Images/tick.png" CommandName="Upload" ></asp:ImageButton>
                                    <asp:ImageButton runat="server" ID="lbtndel" Visible="false" ImageUrl="~/Images/delete.png" CommandName="DelFile"></asp:ImageButton>
                                </td>
                                <td>
                                    <asp:LinkButton ID="lbtnEdit" Font-Underline="true" CommandName="Modify" runat="server" >Edit</asp:LinkButton>
                                    <asp:LinkButton ID="lbtnDelete" Font-Underline="true" OnClientClick="return DeleteVerify()" CommandName="delete" runat="server" >Delete</asp:LinkButton></td>
                            </tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <tr id="Tr3" runat="server" style="background-color: #F7F7DE">
                                <td>
                                    <asp:Label ID="lblSNo" runat="server" BackColor="Transparent" BorderStyle="None" Width="10px" Text='<%#Eval("Sno")%>'></asp:Label></td>
                                <td>
                                    <asp:Label ID="lbltype" runat="server" BackColor="Transparent" BorderStyle="None" Width="70px" Text='<%#Eval("ReportType")%>'></asp:Label></td>
                                <%--<td>
                                    <asp:Label ID="lblMode" runat="server" BackColor="Transparent" BorderStyle="None" Width="40px" Text='<%#Eval("Mode")%>'></asp:Label></td>--%>
                                <td>
                                    <asp:Label ID="lblAllocation" runat="server" BackColor="Transparent" BorderStyle="None" Width="40px" Text='<%#Eval("Allocation")%>'></asp:Label></td>
                                <td>
                                    <asp:Label ID="lblreqdt" runat="server" BackColor="Transparent" BorderStyle="None" Width="40px" Text='<%#Eval("RequestDt", "{0:dd/MM/yyyy}")%>'></asp:Label></td>
                                <td>
                                    <asp:Label ID="lblrptdt" runat="server" BackColor="Transparent" BorderStyle="None" Width="40px" Text='<%#Eval("ReportDt", "{0:dd/MM/yyyy}")%>'></asp:Label></td>
                                <td><asp:Label ID="lblcomment" runat="server" BackColor="Transparent" BorderStyle="None" Width="20px" Text='<%#Eval("Comment")%>'></asp:Label></td>
                                <td>
                                    <asp:LinkButton ID="lbtnFileName" runat="server" BackColor="Transparent" BorderStyle="None" Width="30px" Text='<%#Eval("FileName")%>'  CommandName="View"></asp:LinkButton>
                                    <asp:FileUpload runat="server" ID="fp1" Visible="false" /></td>
                                <td>
                                    <asp:ImageButton runat="server" ID="lbtnup" Visible="false" ImageUrl="~/Images/tick.png" CommandName="Upload"></asp:ImageButton>
                                    <asp:ImageButton runat="server" ID="lbtndel" Visible="false" ImageUrl="~/Images/delete.png" CommandName="DelFile"></asp:ImageButton>
                                </td>
                                <td>
                                    <asp:LinkButton ID="lbtnEdit" Font-Underline="true" CommandName="Modify" runat="server">Edit</asp:LinkButton>
                                    <asp:LinkButton ID="lbtnDelete" Font-Underline="true" OnClientClick="return DeleteVerify()" CommandName="delete" runat="server" >Delete</asp:LinkButton></td>
                            </tr>
                        </AlternatingItemTemplate>
                    </asp:ListView>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="panUpdate" CssClass="borderStyle" Width="100%" runat="server" Visible="false">
        <table style="width: 100%; border-width: 1px; border-color: #666; border-style: solid;" class="MVStyle" cellspacing="10px" id="Table11">          
            <tr>
                <td>SNo</td>
                <td>
                    <asp:TextBox ID="txtSno" runat="server" Height="22px" Width="38px"></asp:TextBox></td>
                <td>
                <asp:Label runat="server" Text="Entry Date"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtentrydt" runat="server" class="date"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label runat="server">Service Request</asp:Label></td>
            <td>
                <asp:TextBox runat="server" ID="txtsrno"></asp:TextBox></td>
            <td>
                <asp:Label runat="server">Request Date</asp:Label></td>
            <td>
                <asp:TextBox ID="txtrequestdt" runat="server" class="date"></asp:TextBox></td>
        </tr>
        <tr>
            <td></td>
            <td></td>
            <td>
                <asp:Label runat="server">Report Date</asp:Label></td>
            <td>
                <asp:TextBox ID="txtrptdt" runat="server" class="date" /></td>
        </tr>
        <tr>
            <td>
                <asp:Label runat="server">Type of Report</asp:Label></td>
            <td>
                <asp:DropDownList runat="server" ID="ddlrpt" Width="170px"></asp:DropDownList></td>
            <td>
                <asp:Label runat="server">Inhouse/External</asp:Label></td>
            <td>
                <asp:DropDownList runat="server" ID="ddlmode" AutoPostBack="true" OnSelectedIndexChanged="ddlmode_SelectedIndexChanged"></asp:DropDownList></td>
        </tr>
        <tr>
            <td>
                <asp:Label runat="server">Allocation</asp:Label></td>
            <td>
                <asp:DropDownList runat="server" ID="ddlallocation" Width="170px" Visible="false"></asp:DropDownList>
                <asp:TextBox runat="server" ID="txtallocation"></asp:TextBox>
            </td>
            <td>
                <asp:Label runat="server">Participants</asp:Label></td>
            <td>
                <asp:TextBox runat="server" ID="txtparticipants"></asp:TextBox></td>
        </tr>
        <tr>
            <td>
                <asp:Label runat="server">IPC Code</asp:Label></td>
            <td>
                <asp:TextBox runat="server" ID="txtipc" TextMode="MultiLine" Width="170px"></asp:TextBox></td>
            <td>
                <asp:Label runat="server">Technology Action</asp:Label></td>
            <td>
                <asp:TextBox runat="server" ID="txtaction" TextMode="MultiLine" Width="170px"></asp:TextBox></td>
        </tr>
        <tr>
            <td>
                <asp:Label runat="server">Summary Finding</asp:Label></td>
            <td>
                <asp:TextBox runat="server" ID="txtsummary"></asp:TextBox></td>
            <td>
                <asp:Label runat="server">Comment</asp:Label></td>
            <td>
                <asp:TextBox runat="server" ID="txtcomment"></asp:TextBox></td>
        </tr>
        <tr>
            <td>
                <asp:Label runat="server">Inventor's Input</asp:Label></td>
            <td>
                <asp:TextBox runat="server" ID="txtinput"></asp:TextBox></td>
            <td>
                <asp:Label runat="server">Followup</asp:Label></td>
            <td>
                <asp:TextBox runat="server" ID="txtfollowup"></asp:TextBox></td>
        </tr>
            <tr style="display: none;" id="uploadrow">
            <td>
                <asp:Label runat="server">File Upload</asp:Label></td>
            <td>
                <asp:FileUpload ID="fp1" runat="server" /></td>
        </tr>
            <tr style="display: none;" id="displayrow">
                <td><asp:Label runat="server"></asp:Label></td>
                <td><asp:LinkButton runat="server" ID="lnkView"></asp:LinkButton></td>
                <td><asp:LinkButton runat="server" ID="lnkDownload"></asp:LinkButton></td>

            </tr>
            <tr>
                <td align="center" colspan="2">                   
                    <asp:Button ID="btnActUpdate" runat="server" CssClass="submitButton"
                        Text="Update" OnClick="btnActUpdate_Click" />
                    &nbsp;&nbsp;
                    <asp:Button ID="btnClose" runat="server" CssClass="submitButton"
                        Text="Close" OnClick="btnClose_Click" />
                </td>
            </tr>         
        </table>
    </asp:Panel>
    <%--<div>
        <asp:GridView ID="gv1" runat="server" AutoGenerateColumns="false" EmptyDataText="No Files Uploaded">
            <Columns>
                <asp:BoundField DataField="Text" HeaderText="FileName" />
                <asp:TemplateField>
                    <asp:ItemTemplate>
                        <asp:LinkButton ID="lnkDownload" Text = "Download" CommandArgument = '<%# Eval("Value") %>' runat="server" OnClick = "DownloadFile"></asp:LinkButton>
                    </asp:ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <asp:ItemTemplate>
                        <asp:LinkButton ID = "lnkDelete" Text = "Delete" CommandArgument = '<%# Eval("Value") %>' runat = "server" OnClick = "DeleteFile" />
                    </asp:ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>--%>
</asp:Content>
