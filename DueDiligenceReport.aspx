<%@ Page Title="" Language="C#" MasterPageFile="~/Parent.master" AutoEventWireup="true" CodeFile="DueDiligenceReport.aspx.cs" Inherits="DueDiligenceReport" %>

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
            <td runat="server" id="lblTitle" align="center" valign="middle" style="font-size: medium; text-decoration: underline;">Due Diligence Report</td>
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
                            <asp:Button ID="imgBtnReport" runat="server"
                                Text="Report A" OnClick="imgBtnReport_Click" />&nbsp;
                            <asp:Button ID="imgBtnReportb" runat="server"
                                Text="Report B" OnClick="imgBtnReportb_Click" />&nbsp;
         <asp:Button ID="imgBtnClear" runat="server" Text="Clear" OnClick="imgBtnClear_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
       <tr>
                <td class="style4">
                    <asp:ListView ID="lvSearch" runat="server" OnItemCommand="lvSearch_ItemCommand" OnItemDataBound="lvSearch_ItemDataBound">
                        <LayoutTemplate>
                            <table id="Table1" width="1000px" align="center" runat="server" style="text-align=leftborder-color: #994c00; border-style: solid; border-width: 1px;" cellpadding="5" cellspacing="0" border="1">
                                <tr id="Tr1" runat="server" style="background-color: #d2691e; color: #fffff0">
                                    <th id="Th10" align="left" runat="server">FileNo</th>
                                    <th id="Th1" align="left" runat="server">SNo</th>
                                    <th id="Th2" align="left" runat="server">Type</th>
                                    <th id="Th4" align="left" runat="server">Request Date</th>
                                    <th id="Th5" align="left" runat="server">Report Date</th>
                                    <th id="Th6" align="left" runat="server">Mode</th>
                                    <th id="Th3" align="left" runat="server">Allocation</th>
                                    <th id="Th7" align="left" runat="server">FileName</th>                                 
                                    
                                </tr>
                                <tr runat="server" id="itemPlaceholder" />
                            </table>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <tr id="Tr2" runat="server" style="background-color: White">
                                <td>
                                    <asp:Label ID="lblFileNo" runat="server" BackColor="Transparent" BorderStyle="None" Width="10px" Text='<%#Eval("FileNo")%>'></asp:Label></td>
                                <td>
                                    <asp:Label ID="lblSNo" runat="server" BackColor="Transparent" BorderStyle="None" Width="10px" Text='<%#Eval("Sno")%>'></asp:Label></td>
                                <td>
                                    <asp:Label ID="lbltype" runat="server" BackColor="Transparent" BorderStyle="None" Width="80px" Text='<%#Eval("ReportType")%>'></asp:Label></td>                                
                                <td>
                                    <asp:Label ID="lblreqdt" runat="server" BackColor="Transparent" BorderStyle="None" Width="40px" Text='<%#Eval("RequestDt", "{0:MM/dd/yyyy}")%>'></asp:Label></td>
                                <td>
                                    <asp:Label ID="lblrptdt" runat="server" BackColor="Transparent" BorderStyle="None" Width="40px" Text='<%#Eval("ReportDt", "{0:MM/dd/yyyy}")%>'></asp:Label></td>
                                <td>
                                    <asp:Label ID="lblMode" runat="server" BackColor="Transparent" BorderStyle="None" Width="40px" Text='<%#Eval("Mode")%>'></asp:Label></td>
                                <td>
                                    <asp:Label ID="lblAllocation" runat="server" BackColor="Transparent" BorderStyle="None" Width="40px" Text='<%#Eval("Allocation")%>'></asp:Label></td>                                
                                <td>
                                    <asp:LinkButton ID="lbtnFileName" runat="server" BackColor="Transparent" BorderStyle="None" Width="20px" Text='<%#Eval("FileName")%>' OnClientClick="return AddUpload()" CommandName="View"></asp:LinkButton>   </td>                             
                            </tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <tr id="Tr3" runat="server" style="background-color: #F7F7DE">
                                <td>
                                    <asp:Label ID="lblFileNo" runat="server" BackColor="Transparent" BorderStyle="None" Width="10px" Text='<%#Eval("FileNo")%>'></asp:Label></td>
                                <td>
                                    <asp:Label ID="lblSNo" runat="server" BackColor="Transparent" BorderStyle="None" Width="10px" Text='<%#Eval("Sno")%>'></asp:Label></td>
                                <td>
                                    <asp:Label ID="lbltype" runat="server" BackColor="Transparent" BorderStyle="None" Width="80px" Text='<%#Eval("ReportType")%>'></asp:Label></td>                                
                                <td>
                                    <asp:Label ID="lblreqdt" runat="server" BackColor="Transparent" BorderStyle="None" Width="40px" Text='<%#Eval("RequestDt", "{0:MM/dd/yyyy}")%>'></asp:Label></td>
                                <td>
                                    <asp:Label ID="lblrptdt" runat="server" BackColor="Transparent" BorderStyle="None" Width="40px" Text='<%#Eval("ReportDt", "{0:MM/dd/yyyy}")%>'></asp:Label></td>
                                <td>
                                    <asp:Label ID="lblMode" runat="server" BackColor="Transparent" BorderStyle="None" Width="40px" Text='<%#Eval("Mode")%>'></asp:Label></td>
                                <td>
                                    <asp:Label ID="lblAllocation" runat="server" BackColor="Transparent" BorderStyle="None" Width="40px" Text='<%#Eval("Allocation")%>'></asp:Label></td>                                
                                <td>
                                    <asp:LinkButton ID="lbtnFileName" runat="server" BackColor="Transparent" BorderStyle="None" Width="20px" Text='<%#Eval("FileName")%>' OnClientClick="return AddUpload()" CommandName="View"></asp:LinkButton>       </td>                         
                            </tr>
                        </AlternatingItemTemplate>
                    </asp:ListView>
                </td>
            </tr>
        <tr>
                <td class="style4">
                    <asp:ListView ID="lvSearchb" runat="server">
                        <LayoutTemplate>
                            <table id="Table1" width="1000px" align="center" runat="server" style="text-align=leftborder-color: #994c00; border-style: solid; border-width: 1px;" cellpadding="5" cellspacing="0" border="1">
                                <tr id="Tr1" runat="server" style="background-color: #d2691e; color: #fffff0">
                                    <th id="Th1" align="left" runat="server">FileNo</th>
                                    <th id="Th2" align="left" runat="server">SNo</th>
                                    <th id="Th3" align="left" runat="server">IPC Code</th>
                                    <th id="Th4" align="left" runat="server">Action</th>
                                    <th id="Th5" align="left" runat="server">Summary</th>                                   
                                    <th id="Th6" align="left" runat="server">Comments</th>
                                    <th id="Th7" align="left" runat="server">Inventor Inputs</th>                                 
                                    
                                </tr>
                                <tr runat="server" id="itemPlaceholder" />
                            </table>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <tr id="Tr2" runat="server" style="background-color: White">
                                <td>
                                    <asp:Label ID="lblFileNo" runat="server" BackColor="Transparent" BorderStyle="None" Width="10px" Text='<%#Eval("FileNo")%>'></asp:Label></td>
                                <td>
                                    <asp:Label ID="lblSNo" runat="server" BackColor="Transparent" BorderStyle="None" Width="10px" Text='<%#Eval("Sno")%>'></asp:Label></td>
                                <td>
                                    <asp:Label ID="lblipc" runat="server" BackColor="Transparent" BorderStyle="None" Width="80px" Text='<%#Eval("IPCCode")%>'></asp:Label></td>                                
                                <td>
                                    <asp:Label ID="lblaction" runat="server" BackColor="Transparent" BorderStyle="None" Width="40px" Text='<%#Eval("TechnologyAction")%>'></asp:Label></td>
                                <td>
                                    <asp:Label ID="lblsum" runat="server" BackColor="Transparent" BorderStyle="None" Width="40px" Text='<%#Eval("Summary")%>'></asp:Label></td>
                                <td>
                                    <asp:Label ID="lblcomment" runat="server" BackColor="Transparent" BorderStyle="None" Width="40px" Text='<%#Eval("comment")%>'></asp:Label></td>
                                <td>
                                    <asp:Label ID="lblinput" runat="server" BackColor="Transparent" BorderStyle="None" Width="40px" Text='<%#Eval("InventorInput")%>'></asp:Label></td>                                
                                
                            </tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <tr id="Tr3" runat="server" style="background-color: #F7F7DE">
                                <td>
                                    <asp:Label ID="lblFileNo" runat="server" BackColor="Transparent" BorderStyle="None" Width="10px" Text='<%#Eval("FileNo")%>'></asp:Label></td>
                                <td>
                                    <asp:Label ID="lblSNo" runat="server" BackColor="Transparent" BorderStyle="None" Width="10px" Text='<%#Eval("Sno")%>'></asp:Label></td>
                                <td>
                                    <asp:Label ID="lblipc" runat="server" BackColor="Transparent" BorderStyle="None" Width="80px" Text='<%#Eval("IPCCode")%>'></asp:Label></td>                                
                                <td>
                                    <asp:Label ID="lblaction" runat="server" BackColor="Transparent" BorderStyle="None" Width="40px" Text='<%#Eval("TechnologyAction")%>'></asp:Label></td>
                                <td>
                                    <asp:Label ID="lblsum" runat="server" BackColor="Transparent" BorderStyle="None" Width="40px" Text='<%#Eval("Summary")%>'></asp:Label></td>
                                <td>
                                    <asp:Label ID="lblcomment" runat="server" BackColor="Transparent" BorderStyle="None" Width="40px" Text='<%#Eval("comment")%>'></asp:Label></td>
                                <td>
                                    <asp:Label ID="lblinput" runat="server" BackColor="Transparent" BorderStyle="None" Width="40px" Text='<%#Eval("InventorInput")%>'></asp:Label></td>                                
                            </tr>
                        </AlternatingItemTemplate>
                    </asp:ListView>
                </td>
            </tr>
      <%--  <tr>
            <td>
                <asp:GridView ID="gvResult" Width="100%" AutoGenerateColumns="false"
                    runat="server" OnRowDataBound="gvResult_RowDataBound">
                    <Columns>                        
                        <asp:BoundField HeaderStyle-Font-Underline="true" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="true" DataField="FileNo" HeaderText="FileNo" SortExpression="FileNo" />
                        <asp:BoundField HeaderStyle-Font-Underline="true" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" ItemStyle-Wrap="true" DataField="Sno" HeaderText="Sno" SortExpression="Sno" />
                        <asp:BoundField HeaderStyle-Font-Underline="true" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="true" DataField="ReportType" HeaderText="ReportType" SortExpression="ReportType" />
                        <asp:BoundField HeaderStyle-Font-Underline="true" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="true" DataField="RequestDt" HeaderText="RequestDt" SortExpression="RequestDt" />
                        <asp:BoundField HeaderStyle-Font-Underline="true" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="true" DataField="ReportDt" HeaderText="ReportDt" SortExpression="ReportDt" />
                        <asp:BoundField HeaderStyle-Font-Underline="true" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="true" DataField="Mode" HeaderText="Mode" SortExpression="Mode" />
                        <asp:BoundField HeaderStyle-Font-Underline="true" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="true" DataField="Allocation" HeaderText="Allocation" SortExpression="Allocation" />
                        <asp:TemplateField HeaderStyle-Font-Underline="true" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderText="Document">
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
        </tr>--%>


    </table>
</asp:Content>

