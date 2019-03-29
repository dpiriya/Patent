<%@ Page Language="C#" MasterPageFile="~/Parent.master" AutoEventWireup="true" CodeFile="ServiceRequest.aspx.cs" Inherits="ServiceRequest" Title="New Service Request" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="Server">
    <link href="Styles/newEntry.css" rel="Stylesheet" type="text/css" />
    <script src="Scripts/jquery-1.9.1.js" type="text/javascript"></script>
    <script src="Scripts/jquery-ui-1.10.3.custom.min.js" type="text/javascript"></script>
    <link href="Styles/jquery-ui-1.10.3.custom.min.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function Verify() {
            var r = confirm('Are you sure; Do you want to Insert this record?');
            if (r == true) {
                if (document.getElementById('<%= dropId.ClientID%>').value == "") {
                alert('Service Provider can not be empty');
                return false;
            }
            else
                return true;
        }

        }

    </script>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">
 <table  align="right"><tr>
     <td >
         <asp:LinkButton OnClick="New_Click" Font-Underline="true" runat="server">New</asp:LinkButton>
     </td>
     <td>
         <asp:LinkButton OnClick="Modify_Click" Font-Underline="true" runat="server">Modify</asp:LinkButton>
     </td>
        </tr>      
 </table>
    <table align="center" class="neTable" cellpadding="12px" width="950px" cellspacing="10px">        
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>                
        <tr>      
            <td></td>
            <td style="font-size: 18px; color: Maroon;">
                <asp:Label ID="title" runat="server">New Service Request</asp:Label>
            </td>        
        </tr>
        <%--<tr>
            <td align="right"></td>
        </tr>--%>
        <tr>
            <td style="font:bold">Service Request No</td>
            <td>
                <asp:TextBox ID="txtSRNo" Enabled="false" runat="server"></asp:TextBox> 
                <asp:DropDownList ID="ddlSRNo" runat="server" OnSelectedIndexChanged="ddlSRNo_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
            </td>            
           <%-- <td>
                <asp:ImageButton ID="imgBtnReport" runat="server"
                    ImageUrl="~/Images/Save2.png" OnClick="imgBtnReport_Click" />
            </td>--%>
        </tr>   
        <tr>
            <td>Intimation Date</td>
            <td><asp:TextBox ID="IntDate" runat="server" OnTextChanged="IntDate_TextChanged"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Service Provider</td>
            <td>
                <asp:DropDownList ID="dropId" runat="server"></asp:DropDownList>
            </td>
        </tr>
        <%--<tr>
            <td>A/C Ref No</td>
            <td>
                <asp:TextBox ID="txtCompany" runat="server"></asp:TextBox>
            </td>
        </tr>--%>
        <tr>
            <td colspan="2">
                <p>Services to be provided:</p>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" width="550px">
                    <ContentTemplate>
                        <asp:ListView ID="lvIdf" runat="server" InsertItemPosition="LastItem" OnItemDataBound="lvIdf_ItemDataBound"
                            OnItemCreated="lvIdf_ItemCreated" OnItemCanceling="lvIdf_ItemCanceling"
                            OnItemDeleting="lvIdf_ItemDeleting" OnItemInserting="lvIdf_ItemInserting" OnItemUpdating="lvIdf_ItemUpdating" onitemediting="lvIdf_ItemEditing">
                            <LayoutTemplate>
                                <table id="Table1" width="inherit" align="center" runat="server" style="text-align: left; border-color: #DEDFDE; border-style: none; border-width: 1px;" cellpadding="5" cellspacing="0" border="1">
                                    <tr id="Tr1" runat="server" style="background-color: #c8d59c">
                                        <th id="Th1" runat="server">Sl.No.</th>
                                        <th id="Th2" runat="server">IDF</th>
                                        <th id="Th3" runat="server">Action</th>
                                        <th id="Th4" runat="server">Party</th>
                                        <th id="Th5" runat="server">Share</th>
                                        <th id="Th6" runat="server">MDoc</th>
                                        <th id="Th7" runat="server">Target Date</th>
                                        <th id="Th8" runat="server">Actual Date</th>
                                        <th id="Th9" runat="server">Status</th>
                                        <th id="Th10" runat="server">Remarks</th>

                                    </tr>
                                    <tr runat="server" id="itemPlaceholder" />
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr id="Tr2" runat="server" style="background-color: White">
                                    <td>
                                        <asp:Label ID="lblSlNo" Text='<%#Eval("Sno")%>' Width="50px" runat="server"></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblIdfNo" BackColor="Transparent" BorderStyle="None" runat="server" Text='<%#Eval("FileNo")%>'></asp:Label></td>
                                    <td>
                                        <asp:Label ID="ddlAction" BackColor="Transparent" BorderStyle="None" Width="100px" Text='<%#Eval("Action")%>' runat="server"></asp:Label></td>
                                    <td>
                                        <asp:Label ID="ddlParty" BackColor="Transparent" BorderStyle="None" TextMode="MultiLine" Text='<%#Eval("SharingParty")%>' runat="server"></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblShare" BackColor="Transparent" BorderStyle="None" Width="50px" Text='<%#Eval("Share")%>' runat="server"></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblMDoc" BackColor="Transparent" BorderStyle="None" Width="70px" Text='<%#Eval("MDocNo")%>' runat="server"></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblTargetDt" BackColor="Transparent" BorderStyle="None" Width="80px" Text='<%#Eval("TargetDt")%>' runat="server"></asp:Label></td>

                                    <td>
                                        <asp:Label ID="lblActualDt" BackColor="Transparent" BorderStyle="None" Width="80px" Text='<%#Eval("ActualDt")%>' runat="server"></asp:Label></td>
                                    <td>
                                        <asp:Label ID="ddlStatus" BackColor="Transparent" BorderStyle="None" Width="70px" Text='<%#Eval("Status")%>' runat="server"></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblRemarks" BackColor="Transparent" BorderStyle="None" Width="70px" TextMode="MultiLine" Text='<%#Eval("Remarks")%>' runat="server"></asp:Label></td>
                                    <td><asp:LinkButton ID="lbEdit" Font-Underline="true" CommandName="edit" runat="server">Edit</asp:LinkButton>
                                    
                                        <asp:LinkButton ID="lbtnDelete" Font-Underline="true" CommandName="delete" runat="server">Delete</asp:LinkButton>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                <tr id="Tr3" runat="server" style="background-color: #F7F7DE">
                                    <td>
                                        <asp:Label ID="lblSlNo" Text='<%#Eval("Sno")%>' Width="50px"  runat="server"></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblIdfNo" BackColor="Transparent" BorderStyle="None" runat="server" Text='<%#Eval("FileNo")%>'></asp:Label></td>
                                    <td>
                                        <asp:Label ID="ddlAction" BackColor="Transparent" BorderStyle="None" Width="100px" Text='<%#Eval("Action")%>' runat="server"></asp:Label></td>
                                    <td>
                                        <asp:Label ID="ddlParty" BackColor="Transparent" BorderStyle="None" TextMode="MultiLine" Text='<%#Eval("SharingParty")%>' runat="server"></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblShare" BackColor="Transparent" BorderStyle="None" Width="50px" Text='<%#Eval("Share")%>' runat="server"></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblMDoc" BackColor="Transparent" BorderStyle="None" Width="70px" Text='<%#Eval("MDocNo")%>' runat="server"></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblTargetDt" BackColor="Transparent" BorderStyle="None" Width="80px" Text='<%#Eval("TargetDt")%>' runat="server"></asp:Label></td>

                                    <td>
                                        <asp:Label ID="lblActualDt" BackColor="Transparent" BorderStyle="None" Width="80px" Text='<%#Eval("ActualDt")%>' runat="server"></asp:Label></td>
                                    <td>
                                        <asp:Label ID="ddlStatus" BackColor="Transparent" BorderStyle="None" Width="70px" Text='<%#Eval("Status")%>' runat="server"></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblRemarks" BackColor="Transparent" BorderStyle="None" Width="70px" TextMode="MultiLine" Text='<%#Eval("Remarks")%>' runat="server"></asp:Label></td>
                                    <td><asp:LinkButton ID="lbEdit" Font-Underline="true" CommandName="edit" runat="server">Edit</asp:LinkButton>
                                      
                                        <asp:LinkButton ID="lbtnDelete" Font-Underline="true" CommandName="delete" runat="server">Delete</asp:LinkButton>
                                    </td>
                                    <%-- <td>
                                        <asp:Label ID="lblTargetDt" BackColor="Transparent" BorderStyle="None" Width="100px" TextMode="MultiLine"   Text='<%#Eval("TargetDt")%>' runat="server"></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblRemarks" BackColor="Transparent" BorderStyle="None" Width="100px" TextMode="MultiLine"   Text='<%#Eval("Remarks")%>' runat="server"></asp:Label></td>
                                    <td>
                                        <asp:LinkButton ID="lbtnDelete" Font-Underline="true" CommandName="delete" runat="server">Delete</asp:LinkButton>
                                    </td>--%>
                                </tr>
                            </AlternatingItemTemplate>
                            
                            <InsertItemTemplate>
                                <tr id="Tr4" runat="server">
                                    <td>
                                        <asp:TextBox ID="lblNewSlNo" Text='<%#Eval("Sno")%>' Width="50px"  runat="server"></asp:TextBox></td>
                                    <td>
                                        <asp:DropDownList ID="ddlNewIdfNo" runat="server" Width="50px" ></asp:DropDownList></td>

                                    <td>
                                        <asp:DropDownList ID="ddlNewAction" BackColor="Transparent" BorderStyle="None" Width="100px"  runat="server"></asp:DropDownList></td>
                                    <td>
                                        <asp:DropDownList ID="ddlNewParty" BackColor="Transparent" BorderStyle="None" Width="100px"  runat="server"></asp:DropDownList></td>
                                    <td>
                                        <asp:TextBox ID="txtShare" BackColor="Transparent" BorderStyle="None" Width="50px" Text='<%#Eval("Share")%>' runat="server"></asp:TextBox></td>
                                    <td>
                                        <asp:DropDownList ID="ddlNewMDoc" BackColor="Transparent" BorderStyle="None" Width="70px"  runat="server"></asp:DropDownList></td>
                                    <td>
                                        <asp:TextBox ID="txtTargetDt" BackColor="Transparent" BorderStyle="None" Width="80px" Text='<%#Eval("TargetDt")%>' runat="server"></asp:TextBox></td>

                                    <td>
                                        <asp:TextBox ID="txtActualDt" BackColor="Transparent" BorderStyle="None" Width="80px" Text='<%#Eval("ActualDt")%>' runat="server"></asp:TextBox></td>
                                    <td>
                                        <asp:DropDownList ID="ddlNewStatus" BackColor="Transparent" BorderStyle="None" Width="70px" runat="server"></asp:DropDownList></td>
                                    <td>
                                        <asp:TextBox ID="txtRemarks" BackColor="Transparent" BorderStyle="None" Width="70px" TextMode="MultiLine" Text='<%#Eval("Remarks")%>' runat="server"></asp:TextBox></td>

                                    <td>
                                        <asp:LinkButton ID="lbtnInsert" Font-Underline="true" CommandName="insert" runat="server">Insert</asp:LinkButton>
                                        <asp:LinkButton ID="lbtnCancel" Font-Underline="true" CommandName="cancel" runat="server">Cancel</asp:LinkButton>
                                    </td>
                                </tr>
                            </InsertItemTemplate>
                            <EditItemTemplate>
                                    <tr id="Tr5" runat="server">
                                    <td>
                                        <asp:TextBox ID="lblEdSlNo" Text='<%#Eval("Sno")%>' Width="50px"  runat="server"></asp:TextBox></td>
                                    <td>
                                        <asp:DropDownList ID="ddlEdIdfNo" runat="server" AutoPostBack="true" Width="50px" ></asp:DropDownList>
                                        <asp:Label ID="lblEdIdfNo" runat="server" Text='<%#Eval("FileNo") %>' visible="false"></asp:Label>
                                    </td>

                                    <td>
                                        <asp:DropDownList ID="ddlEdAction" BackColor="Transparent" BorderStyle="None" Width="100px"  runat="server"></asp:DropDownList>
                                        <asp:Label ID="lblEdAction" visible="false" Text='<%#Eval("Action") %>' runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlEdParty" BackColor="Transparent" BorderStyle="None" Width="100px"  runat="server"></asp:DropDownList>
                                        <asp:Label ID="lblEdParty" Visible="false" Text='<%#Eval("SharingParty")%>'  runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtEdShare" BackColor="Transparent" BorderStyle="None" Width="50px" Text='<%#Eval("Share")%>' runat="server"></asp:TextBox></td>
                                    <td>
                                        <asp:DropDownList ID="ddlEdMDoc" BackColor="Transparent" BorderStyle="None" Width="70px"  runat="server"></asp:DropDownList>
                                        <asp:Label ID="lblEdMDoc" Text='<%#Eval("MDocNo")%>' Visible="false" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtEdTargetDt" BackColor="Transparent" BorderStyle="None" Width="80px" Text='<%#Eval("TargetDt")%>' runat="server"></asp:TextBox></td>

                                    <td>
                                        <asp:TextBox ID="txtEdActualDt" BackColor="Transparent" BorderStyle="None" Width="80px" Text='<%#Eval("ActualDt")%>' runat="server"></asp:TextBox></td>
                                    <td>
                                        <asp:DropDownList ID="ddlEdStatus" BackColor="Transparent" BorderStyle="None" Width="70px" runat="server"></asp:DropDownList>
                                        <asp:Label ID="lblEdStatus" Visible="false" Text='<%#Eval("Status")%>' runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtRemarks" BackColor="Transparent" BorderStyle="None" Width="70px" TextMode="MultiLine" Text='<%#Eval("Remarks")%>' runat="server"></asp:TextBox></td>

                                    <td>
                                        <asp:LinkButton ID="lbtnUpdate" Font-Underline="true" CommandName="Update" runat="server">Update</asp:LinkButton>
                                        <asp:LinkButton ID="lbtnCancel" Font-Underline="true" CommandName="cancel" runat="server">Cancel</asp:LinkButton>
                                    </td>
                                </tr>
                                </EditItemTemplate>
                        </asp:ListView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr align="center">
            <td colspan="2">
                <asp:ImageButton ID="imgBtnSubmit" runat="server"
                    ImageUrl="~/Images/Save2.png" OnClientClick="return Verify()" OnClick="imgBtnSubmit_Click" />&nbsp;
               
     <asp:ImageButton ID="imgBtnClear" runat="server"
         ImageUrl="~/Images/Clear2.png" OnClick="imgBtnClear_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
