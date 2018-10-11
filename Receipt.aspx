<%@ Page Language="C#" MasterPageFile="~/Parent.master" AutoEventWireup="true" CodeFile="Receipt.aspx.cs" Inherits="Receipt" Title="New Receipt Entry" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="Server">
    <link href="Styles/newEntry.css" rel="Stylesheet" type="text/css" />
     <link href="Styles/jquery-ui-1.10.3.custom.min.css" rel="stylesheet" />
   <%-- <script src="Scripts/jquery-1.9.1.js" type="text/javascript"></script>
    <script src="Scripts/jquery-ui-1.10.3.custom.min.js" type="text/javascript"></script>
    <link href="Styles/jquery-ui-1.10.3.custom.min.css" rel="stylesheet" type="text/css" />--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">
     <script src="packages/jQuery.3.3.1/Content/Scripts/jquery-3.3.1.js" type="text/javascript"></script>          
    <script src="Scripts/jquery-ui-1.10.3.custom.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            $(".date").datepicker({
                changeMonth:true,
                changeYear:true,
                inline:true
            });
        });
        </script>
    <table  align="right"><tr>
     <td >
         <asp:LinkButton OnClick="New_Click" Font-Underline="true" runat="server">New</asp:LinkButton>
     </td>
        <td></td>
     <td>
         <asp:LinkButton OnClick="Modify_Click" Font-Underline="true" runat="server">Modify</asp:LinkButton>
     </td>
        </tr>      
 </table>
    <table align="center" class="neTable" cellpadding="12px" width="970px" cellspacing="10px">
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
        <tr align="center">
            <td colspan="4" style="font-size: 18px; color: Maroon;">
                <asp:Label runat="server" ID="title"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label runat="server" Text="Receipt No"></asp:Label>
            </td>
            <td>
                <asp:TextBox runat="server" ID="txtrcpno"></asp:TextBox>
                <asp:DropDownList runat="server" ID="ddlrcpno" AutoPostBack="true" OnSelectedIndexChanged="ddlrcpno_SelectedIndexChanged"></asp:DropDownList>
            </td>
            <td>
                <asp:Label runat="server" Text="Receipt Date" ></asp:Label>
            </td>
            <td>
                <asp:TextBox runat="server" ID="txtrcpdt" class="date"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="4" style="background-color: lightgoldenrodyellow">
                <asp:Label runat="server">Receipt Details</asp:Label></td>
        </tr>
        <tr>
            <td>
                <asp:Label runat="server" Text="Source"></asp:Label>
            </td>
            <td>
                <asp:DropDownList runat="server" ID="ddlsrc" Style="width: 170px;" AutoPostBack="true" OnSelectedIndexChanged="ddlsrc_SelectedIndexChanged"></asp:DropDownList></td>
            <td>
                
            </td>
            <td>
                
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label runat="server" Text="Party Name"></asp:Label>
            </td>
            <td>
                <asp:DropDownList runat="server" ID="ddlparty" Style="width: 170px;"></asp:DropDownList>
            </td>
            <td>
                <asp:Label runat="server" Text="Party Ref No"></asp:Label>
            </td>
            <td>
                <asp:TextBox runat="server" ID="txtpartyref"></asp:TextBox>
            </td>

        </tr>
        <tr>
            <td>
                <asp:Label runat="server">Receipt Ref</asp:Label>
            </td>
            <td>
                <asp:TextBox runat="server" ID="txtrcpref"></asp:TextBox></td>
            <td>
                <asp:Label runat="server">Receipt Desc</asp:Label>
            </td>
            <td>
                <asp:TextBox runat="server" ID="txtrcpdesc"></asp:TextBox></td>
        </tr>
        <tr><td><asp:Label runat="server">Amount in INR</asp:Label></td>
            <td>
            <asp:TextBox runat="server" ID="txtamt"></asp:TextBox>
            </td>
            <td></td><td></td>
        </tr>
        <tr>
            <td colspan="4" style="background-color: lightgoldenrodyellow">
                <asp:Label runat="server">Background</asp:Label></td>
        </tr>
        <tr>
            <td>
                <asp:Label runat="server">Intimation Ref</asp:Label>
               </td>
            <td>
                <asp:TextBox runat="server" ID="txtintref"></asp:TextBox></td>
            <td>
                <asp:Label runat="server">Intimation Date</asp:Label></td>
            <td>
                <asp:TextBox runat="server" ID="txtintdt" class="date"></asp:TextBox></td>
        </tr>
        <tr>
            <td>
                <asp:Label runat="server">Transaction Comments</asp:Label>
            </td>
            <td>
                <asp:TextBox runat="server" ID="txtcmt"></asp:TextBox></td>
            <td></td>
            <td></td>
        </tr>
        <tr>
            <td colspan="4" style="background-color: lightgoldenrodyellow">
                <asp:Label runat="server">Transfer to IP Cell</asp:Label></td>
        </tr>
        <tr>
            <td><asp:Label runat="server">From A/c No</asp:Label></td>
            <td><asp:TextBox runat="server" ID="txtsaccno"></asp:TextBox>
                <asp:AutoCompleteExtender ServiceMethod="searchttaccno" Enabled="false" MinimumPrefixLength="4" EnableCaching="false" CompletionSetCount="10"
                    TargetControlID="txtsaccno" ID="autocompleteex1" FirstRowSelected="false" runat="server"></asp:AutoCompleteExtender>
            </td>
            <td><asp:Label runat="server">IP Cell A/c No</asp:Label></td>
            <td><asp:TextBox runat="server" ID="txtipaccno"></asp:TextBox></td>
        </tr>
        <tr>
            <td><asp:Label runat="server">Amount Transfer INR</asp:Label></td>
            <td><asp:TextBox runat="server" ID="txttransamt"></asp:TextBox></td>
            <td><asp:Label runat="server">Transfer Date</asp:Label></td>
            <td><asp:TextBox runat="server" ID="txttransdt" class="date"></asp:TextBox></td>
        </tr>
        <tr>
            <td colspan="4">
                <p style="background-color: lightgoldenrodyellow">Detailed Information:</p>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" width="550px" Visible="true">
                    <ContentTemplate>
                        <asp:ListView ID="lvIdf" runat="server" InsertItemPosition="LastItem" OnItemDataBound="lvIdf_ItemDataBound"
                            OnItemCreated="lvIdf_ItemCreated" OnItemCanceling="lvIdf_ItemCanceling"
                            OnItemDeleting="lvIdf_ItemDeleting" OnItemUpdating="lvIdf_ItemUpdating"
                            OnItemInserting="lvIdf_ItemInserting" OnItemEditing="lvIdf_ItemEditing">
                            <LayoutTemplate>
                                <table id="Table1" width="inherit" runat="server" 
                                    style="text-align: left; border-color: #DEDFDE; border-style: none; border-width: 1px;" cellpadding="5" cellspacing="0" border="1">
                                    <tr id="Tr1" runat="server" style="background-color: #c8d59c">
                                        <th id="Th1" runat="server">Sl.No.</th>
                                        <th id="Th2" runat="server">IDF</th>
                                        <th id="Th3" runat="server">Title</th>
                                        <th id="Th4" runat="server">Receipt Group</th>
                                        <th id="Th5" runat="server">Amt in INR</th>
                                        <th id="Th6" runat="server">Remarks</th>
                                        <th></th>
                                    </tr>
                                    <tr runat="server" id="itemPlaceholder"/>
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr id="Tr2" runat="server" style="background-color: white">
                                    <td>
                                        <asp:Label ID="lblSno" Text='<%#Eval("SlNo")%>' Width="50px" runat="server"></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblIdf" Text='<%#Eval("FileNo")%>' runat="server" ></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblTitle" Text='<%#Eval("Title")%>' runat="server"></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblrcpgrp" Text='<%#Eval("RGroup")%>' Width="100px" runat="server"></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblamt" Text='<%#Eval("SplitAmtINR")%>' Width="50px" runat="server"></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblrem" Text='<%#Eval("Remarks")%>' Width="100px" runat="server"></asp:Label></td>
                                    <td>
                                        <asp:LinkButton ID="lbEdit" Font-Underline="true" CommandName="edit" runat="server">Edit</asp:LinkButton>
                                        <asp:LinkButton ID="lbtnDelete" Font-Underline="true" CommandName="delete" runat="server">Delete</asp:LinkButton>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                <tr id="Tr3" runat="server" style="background-color: #F7F7DE">
                                    <td>
                                        <asp:Label ID="lblSno" Text='<%#Eval("Slno")%>' Width="50px" runat="server"></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblIdf" Text='<%#Eval("FileNo")%>' runat="server"></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblTitle" Text='<%#Eval("Title")%>' runat="server"></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblrcpgrp" Text='<%#Eval("RGroup")%>' Width="100px" runat="server"></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblamt" Text='<%#Eval("SplitAmtINR")%>' Width="50px" runat="server"></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblrem" Text='<%#Eval("Remarks")%>' Width="100px" runat="server"></asp:Label></td>
                                    <td>
                                        <asp:LinkButton ID="lbEdit" Font-Underline="true" CommandName="edit" runat="server">Edit</asp:LinkButton>

                                        <asp:LinkButton ID="lbtnDelete" Font-Underline="true" CommandName="delete" runat="server">Delete</asp:LinkButton>
                                    </td>
                                </tr>
                            </AlternatingItemTemplate>
                            <InsertItemTemplate>
                                <tr id="Tr4" runat="server">
                                    <td><asp:TextBox ID="txtsno" runat="server" Text='<%#Eval("Slno")%>' Width="50px"></asp:TextBox></td>
                                    <td><asp:DropDownList ID="ddlIdf" runat="server" OnSelectedIndexChanged="ddlIdf_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></td>
                                    <td><asp:Label ID="lbltitle" Text='<%#Eval("Title")%>' Width="250px" runat="server"></asp:Label></td>
                                    <td><asp:DropDownList ID="ddlgrp" runat="server" Width="180px"></asp:DropDownList></td>
                                    <td><asp:TextBox runat="server" ID="txtamt" Width="90px" Text='<%#Eval("SplitAmtInr")%>'></asp:TextBox></td>
                                    <td><asp:TextBox runat="server" ID="txtrem" TextMode="MultiLine" Text='<%#Eval("Remarks")%>'></asp:TextBox></td>
                                    <td>
                                        <asp:LinkButton ID="lbtnInsert" Font-Underline="true" CommandName="insert" runat="server">Insert</asp:LinkButton>
                                        <asp:LinkButton ID="lbtnCancel" Font-Underline="true" CommandName="cancel" runat="server">Cancel</asp:LinkButton>
                                    </td>
                                </tr>
                            </InsertItemTemplate>
                            <EditItemTemplate>
                                <tr id="Tr5" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtEdSno" runat="server" width="50px" Text='<%#Eval("Slno")%>'></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlEdIdf" runat="server" AutoPostBack="true" Width="50px" OnSelectedIndexChanged="ddlIdf_SelectedIndexChanged"></asp:DropDownList>
                                        <asp:Label Id="lblEdIdf" runat="server" Text='<%#Eval("FileNo")%>' Visible="false"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblEdtitle" runat="server" Text='<%#Eval("Title")%>' Width="200px"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlEdgrp" runat="server" Width="180px"></asp:DropDownList>
                                        <asp:Label ID="lblEdgrp" runat="server" Text='<%#Eval("Rgroup") %>' Visible="false"></asp:Label>
                                    </td>
                                    <td><asp:TextBox runat="server" ID="txtEdamt" Width="90px" Text='<%#Eval("SplitAmtInr") %>'></asp:TextBox></td>
                                    <td><asp:TextBox runat="server" ID="txtEdrem" TextMode="MultiLine" Text='<%#Eval("Remarks")%>'></asp:TextBox></td>
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
            <td colspan="4">
                <asp:ImageButton ID="imgBtnSubmit" runat="server"
                    ImageUrl="~/Images/Save2.png" OnClientClick="return Verify()" OnClick="imgBtnSubmit_Click" />&nbsp;               
     <asp:ImageButton ID="imgBtnClear" runat="server"
         ImageUrl="~/Images/Clear2.png" OnClick="imgBtnClear_Click" />
            </td>
        </tr>
    </table>
</asp:Content>

