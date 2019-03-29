<%@ Page Language="C#" MasterPageFile="~/Parent.master" AutoEventWireup="true" CodeFile="Dispute.aspx.cs" Inherits="Dispute" Title="New Dispute Entry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="Server">
    <link href="Styles/newEntry.css" rel="Stylesheet" type="text/css" />
    <script src="Scripts/jquery-1.9.1.js" type="text/javascript"></script>
    <script src="Scripts/jquery-ui-1.10.3.custom.min.js" type="text/javascript"></script>
    <link href="Styles/jquery-ui-1.10.3.custom.min.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        $(document).ready(function () {
            $("table.neTable tr:even").addClass("tdTable");
            $("table.neTable tr:odd").addClass("tdTable1");

            $("#<%=atxtParty.ClientID %>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "Marketing.aspx/GetCompanyData",
                        data: "{'company':'" + $("#<%=atxtParty.ClientID %>").val() + "'}",
                        dataType: "json",
                        success: function (data) {
                            response(data.d);
                        },
                        error: function (result) {
                            alert("Error");
                        }
                    });
                },
                minLength: 2
            });
        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">
    <table align="center" class="neTable" cellpadding="12px" width="950px" cellspacing="10px">
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
        <tr>
            <td colspan="4" align="center" valign="middle" style="font-size: 18px; color: Maroon;">Dispute Database</td>
        </tr>
        <tr>
            <td></td>
            <td></td>
            <td>Date Created</td>
            <td>
                <asp:TextBox ID="txtcreatedDate" runat="server"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td>Reference Number</td>
            <td>
                <asp:TextBox ID="txtRefNo" Enabled="false" runat="server"></asp:TextBox>
            </td>
            <td>Dispute Group</td>
            <td>
                <asp:DropDownList ID="ddlGroup" runat="server"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>Title</td>
            <td>
                <asp:TextBox ID="txtTitle" TextMode="MultiLine" Wrap="true" Width="300px" runat="server"></asp:TextBox>
            </td>
            <td>Estimated Value</td>
            <td>
                <asp:TextBox ID="txtValue" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Party Name</td>
            <td>
                <asp:TextBox ID="atxtParty" Width="300px" runat="server"></asp:TextBox>
            </td>
            <td>Value Realization</td>
            <td>
                <asp:TextBox ID="txtRealization" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Internal Coordinator</td>
            <td>
                <asp:TextBox ID="atxtCoor" runat="server"></asp:TextBox>
            </td>
            <td>Status</td>
            <td>
                <asp:DropDownList ID="ddlStatus" runat="server"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>Remarks</td>
            <td>
                <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine"></asp:TextBox>
            </td>
            <td></td>
            <td></td>
        </tr>
        <tr>
            <td colspan="4">
                <p>List of MDOC Ref</p>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:ListView ID="LvMDoc" runat="server" InsertItemPosition="LastItem"
                            OnItemCreated="LvMDoc_ItemCreated" OnItemDataBound="LvMDoc_ItemDataBound" OnItemCanceling="LvMDoc_ItemCanceling"
                            OnItemDeleting="LvMDoc_ItemDeleting" OnItemInserting="LvMDoc_ItemInserting">
                            <LayoutTemplate>
                                <table id="tblMdoc" width="800px" align="center" runat="server" style="text-align: left; border-color: #DEDFDE; border-style: solid; border-width: 1px;" cellpadding="5" cellspacing="0" border="1">

                                    <tr id="Tr1" runat="server" style="background-color: #c8d59c">
                                        <th id="Th1" align="center" runat="server">Sl.No.</th>
                                        <th id="Th2" align="center" runat="server">MDOC</th>
                                        <th id="Th3" align="center" runat="server">Title</th>
                                        <th id="Th4" align="center" runat="server">Owner</th>
                                        <th id="Th8" align="center" runat="server">Status</th>
                                        <th id="Th5" runat="server"></th>
                                    </tr>
                                    <tr runat="server" id="itemPlaceHolder"></tr>
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr id="Tr2" runat="server" style="background-color: White">

                                    <td>
                                        <asp:Label ID="lblSlNo" Width="10px" Text='<%#Eval("SNO")%>' runat="server"></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblMDocNo" runat="server" BackColor="Transparent" BorderStyle="None" Width="50px" Text='<%#Eval("ContractNo")%>'></asp:Label></td>
                                    <td>
                                        <asp:TextBox ID="lblTitle" BackColor="Transparent" BorderStyle="None" Width="300px" TextMode="MultiLine" Wrap="true" ReadOnly="true" Text='<%#Eval("Title")%>' runat="server"></asp:TextBox></td>
                                    <td>
                                        <asp:TextBox ID="lblOwner" BackColor="Transparent" BorderStyle="None" Width="150px" TextMode="MultiLine" Wrap="true" ReadOnly="true" Text='<%#Eval("CoordinatingPerson")%>' runat="server"></asp:TextBox></td>
                                    <td>
                                        <asp:TextBox ID="lblStatus" BackColor="Transparent" BorderStyle="None" Width="100px" TextMode="MultiLine" Wrap="true" ReadOnly="true" Text='<%#Eval("Status")%>' runat="server"></asp:TextBox></td>
                                    <td>
                                        <asp:LinkButton ID="lbtnDelete" Font-Underline="true" CommandName="delete" runat="server">Delete</asp:LinkButton>
                                    </td>

                                </tr>
                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                <tr id="Tr2" runat="server" style="background-color: #F7F7DE">
                                    <td>
                                        <asp:Label ID="lblSlNo" Text='<%#Eval("SNO")%>' runat="server"></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblMDocNo" runat="server" BackColor="Transparent" BorderStyle="None" Width="50px" Text='<%#Eval("ContractNo")%>'></asp:Label></td>
                                    <td>
                                        <asp:TextBox ID="lblTitle" BackColor="Transparent" BorderStyle="None" Width="300px" TextMode="MultiLine" Wrap="true" ReadOnly="true" Text='<%#Eval("Title")%>' runat="server"></asp:TextBox></td>
                                    <td>
                                        <asp:TextBox ID="lblOwner" BackColor="Transparent" BorderStyle="None" Width="150px" TextMode="MultiLine" Wrap="true" ReadOnly="true" Text='<%#Eval("CoordinatingPerson")%>' runat="server"></asp:TextBox></td>
                                    <td>
                                        <asp:TextBox ID="lblStatus" BackColor="Transparent" BorderStyle="None" Width="100px" TextMode="MultiLine" Wrap="true" ReadOnly="true" Text='<%#Eval("Status")%>' runat="server"></asp:TextBox></td>
                                    <td>
                                        <asp:LinkButton ID="lbtnDelete" Font-Underline="true" CommandName="delete" runat="server">Delete</asp:LinkButton>
                                    </td>
                                </tr>
                            </AlternatingItemTemplate>
                            <InsertItemTemplate>
                                <tr id="Tr4" runat="server">
                                    <td>
                                        <asp:Label ID="lblNewSlNo" Text='<%#Eval("SNO")%>' runat="server"></asp:Label></td>
                                    <td>
                                        <asp:DropDownList ID="ddlNewMdocNo" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlNewMdocNo_SelectedIndexChanged"></asp:DropDownList></td>
                                    <td>
                                        <asp:TextBox ID="txtTitle" BackColor="Transparent" ReadOnly="true" BorderStyle="None" Width="300px" TextMode="MultiLine" Wrap="true" Text='<%#Eval("Title")%>' runat="server"></asp:TextBox></td>
                                    <td>
                                        <asp:TextBox ID="txtOwner" BackColor="Transparent" ReadOnly="true" BorderStyle="None" Width="150px" TextMode="MultiLine" Wrap="true" Text='<%#Eval("CoordinatingPerson")%>' runat="server"></asp:TextBox></td>

                                    <td>
                                        <asp:TextBox ID="txtStatus" BackColor="Transparent" ReadOnly="true" BorderStyle="None" Width="100px" TextMode="MultiLine" Wrap="true" Text='<%#Eval("Status")%>' runat="server"></asp:TextBox></td>
                                    <td>
                                        <asp:LinkButton ID="lbtnInsert" Font-Underline="true" CommandName="insert" runat="server">Insert</asp:LinkButton>
                                        <asp:LinkButton ID="lbtnCancel" Font-Underline="true" CommandName="cancel" runat="server">Cancel</asp:LinkButton>
                                    </td>
                                </tr>
                            </InsertItemTemplate>
                        </asp:ListView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>

        <tr>
            <td colspan="4">
                <p>List of IDF File Numbers</p>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:ListView ID="lvIdf" runat="server" InsertItemPosition="LastItem"
                            OnItemCreated="lvIdf_ItemCreated" OnItemDataBound="lvIdf_ItemDataBound"
                            OnItemCanceling="lvIdf_ItemCanceling" OnItemDeleting="lvIdf_ItemDeleting"
                            OnItemInserting="lvIdf_ItemInserting">
                            <LayoutTemplate>
                                <table id="Table1" width="800px" align="center" runat="server" style="text-align: left; border-color: #DEDFDE; border-style: solid; border-width: 1px;" cellpadding="5" cellspacing="0" border="1">
                                    <tr id="Tr1" runat="server" style="background-color: #c8d59c">
                                        <th id="Th1" align="center" runat="server">Sl.No.</th>
                                        <th id="Th2" align="center" runat="server">IDF No</th>
                                        <th id="Th3" align="center" runat="server">Title</th>
                                        <th id="Th4" align="center" runat="server">Inventor</th>
                                        <th id="Th8" align="center" runat="server">Application No</th>
                                        <th id="Th7" align="center" runat="server">Status</th>
                                        <th id="Th5" runat="server"></th>
                                    </tr>
                                    <tr runat="server" id="itemPlaceholder" />
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr id="Tr2" runat="server" style="background-color: White">
                                    <td>
                                        <asp:Label ID="lblSlNo" Text='<%#Eval("SlNo")%>' runat="server"></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblIdfNo" runat="server" BackColor="Transparent" BorderStyle="None" Width="50px" Text='<%#Eval("FileNo")%>'></asp:Label></td>
                                    <td>
                                        <asp:TextBox ID="lblTitle" BackColor="Transparent" BorderStyle="None" Width="300px" TextMode="MultiLine" Wrap="true" ReadOnly="true" Text='<%#Eval("Title")%>' runat="server"></asp:TextBox></td>
                                    <td>
                                        <asp:TextBox ID="lblInventor" BackColor="Transparent" BorderStyle="None" Width="150px" TextMode="MultiLine" Wrap="true" ReadOnly="true" Text='<%#Eval("Inventor1")%>' runat="server"></asp:TextBox></td>
                                    <td>
                                        <asp:TextBox ID="lblApplcnNo" BackColor="Transparent" BorderStyle="None" Width="100px" TextMode="MultiLine" Wrap="true" ReadOnly="true" Text='<%#Eval("Applcn_No")%>' runat="server"></asp:TextBox></td>
                                    <td>
                                        <asp:TextBox ID="lblStatus" BackColor="Transparent" BorderStyle="None" Width="100px" TextMode="MultiLine" Wrap="true" ReadOnly="true" Text='<%#Eval("Status")%>' runat="server"></asp:TextBox></td>
                                    <td>
                                        <asp:LinkButton ID="lbtnDelete" Font-Underline="true" CommandName="delete" runat="server">Delete</asp:LinkButton>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                <tr id="Tr2" runat="server" style="background-color: #F7F7DE">
                                    <td>
                                        <asp:Label ID="lblSlNo" Text='<%#Eval("SlNo")%>' runat="server"></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblIdfNo" runat="server" BackColor="Transparent" BorderStyle="None" Width="50px" Text='<%#Eval("FileNo")%>'></asp:Label></td>
                                    <td>
                                        <asp:TextBox ID="lblTitle" BackColor="Transparent" BorderStyle="None" Width="300px" TextMode="MultiLine" Wrap="true" ReadOnly="true" Text='<%#Eval("Title")%>' runat="server"></asp:TextBox></td>
                                    <td>
                                        <asp:TextBox ID="lblInventor" BackColor="Transparent" BorderStyle="None" Width="150px" TextMode="MultiLine" Wrap="true" ReadOnly="true" Text='<%#Eval("Inventor1")%>' runat="server"></asp:TextBox></td>
                                    <td>
                                        <asp:TextBox ID="lblApplcnNo" BackColor="Transparent" BorderStyle="None" Width="100px" TextMode="MultiLine" Wrap="true" ReadOnly="true" Text='<%#Eval("Applcn_No")%>' runat="server"></asp:TextBox></td>
                                    <td>
                                        <asp:TextBox ID="lblStatus" BackColor="Transparent" BorderStyle="None" Width="100px" TextMode="MultiLine" Wrap="true" ReadOnly="true" Text='<%#Eval("Status")%>' runat="server"></asp:TextBox></td>
                                    <td>
                                        <asp:LinkButton ID="lbtnDelete" Font-Underline="true" CommandName="delete" runat="server">Delete</asp:LinkButton>
                                    </td>
                                </tr>
                            </AlternatingItemTemplate>
                            <InsertItemTemplate>
                                <tr id="Tr4" runat="server">
                                    <td>
                                        <asp:Label ID="lblNewSlNo" Text='<%#Eval("SlNo")%>' runat="server"></asp:Label></td>
                                    <td>
                                        <asp:DropDownList ID="ddlNewIdfNo" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlNewIdfNo_SelectedIndexChanged"></asp:DropDownList></td>
                                    <td>
                                        <asp:TextBox ID="txtTitle" BackColor="Transparent" ReadOnly="true" BorderStyle="None" Width="300px" TextMode="MultiLine" Wrap="true" Text='<%#Eval("Title")%>' runat="server"></asp:TextBox></td>
                                    <td>
                                        <asp:TextBox ID="txtInventor" BackColor="Transparent" ReadOnly="true" BorderStyle="None" Width="150px" TextMode="MultiLine" Wrap="true" Text='<%#Eval("Inventor1")%>' runat="server"></asp:TextBox></td>
                                    <td>
                                        <asp:TextBox ID="txtApplcnNo" BackColor="Transparent" ReadOnly="true" BorderStyle="None" Width="100px" TextMode="MultiLine" Wrap="true" Text='<%#Eval("Applcn_No")%>' runat="server"></asp:TextBox></td>
                                    <td>
                                        <asp:TextBox ID="txtStatus" BackColor="Transparent" ReadOnly="true" BorderStyle="None" Width="100px" TextMode="MultiLine" Wrap="true" Text='<%#Eval("Status")%>' runat="server"></asp:TextBox></td>
                                    <td>
                                        <asp:LinkButton ID="lbtnInsert" Font-Underline="true" CommandName="insert" runat="server">Insert</asp:LinkButton>
                                        <asp:LinkButton ID="lbtnCancel" Font-Underline="true" CommandName="cancel" runat="server">Cancel</asp:LinkButton>
                                    </td>
                                </tr>
                            </InsertItemTemplate>
                        </asp:ListView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <p>List of Activities</p>
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <asp:ListView ID="lvActivity" runat="server" InsertItemPosition="LastItem"
                            OnItemCanceling="lvActivity_ItemCanceling"
                            OnItemDeleting="lvActivity_ItemDeleting" 
                            OnItemInserting="lvActivity_ItemInserting" OnItemCommand="lvActivity_ItemCommand" OnItemDataBound="lvActivity_ItemDataBound">
                            <LayoutTemplate>
                                <table id="Table1" width="800px" align="center" runat="server" style="text-align: left; border-color: #DEDFDE; border-style: solid; border-width: 1px;" cellpadding="5" cellspacing="0" border="1">
                                    <tr id="Tr1" runat="server" style="background-color: #c8d59c">
                                        <th id="Th1" align="center" runat="server">Sl.No.</th>
                                        <th id="Th2" align="center" runat="server">Activity Date</th>
                                        <th id="Th3" align="center" runat="server">Forum</th>
                                        <th id="Th4" align="center" runat="server">Activity Type</th>
                                        <th id="Th5" align="center" runat="server">Remarks</th>                                        
                                        <th id="Th6" runat="server">Doc upload</th>
                                        <th id="Th7" runat="server">Upload/Delete</th>
                                        <th id="Th8" runat="server"></th>
                                    </tr>
                                    <tr runat="server" id="itemPlaceholder" />
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr id="Tr2" runat="server" style="background-color: White">
                                    <td>
                                        <asp:Label ID="lblSlNo" Text='<%#Eval("SlNo")%>' runat="server"></asp:Label></td>
                                    <td>
                                        <asp:TextBox ID="lblActivityDt" BackColor="Transparent" BorderStyle="None" Width="100px" ReadOnly="true" Text='<%#Eval("ActivityDt")%>' runat="server"></asp:TextBox></td>
                                    <td>
                                        <asp:TextBox ID="lblChannel" BackColor="Transparent" BorderStyle="None" Width="100px" TextMode="MultiLine" Wrap="true" ReadOnly="true" Text='<%#Eval("Forum")%>' runat="server"></asp:TextBox></td>
                                    <td>
                                        <asp:TextBox ID="lblActivityType" BackColor="Transparent" BorderStyle="None" Width="150px" TextMode="MultiLine" Wrap="true" ReadOnly="true" Text='<%#Eval("ActivityType")%>' runat="server"></asp:TextBox></td>
                                    <td>
                                        <asp:TextBox ID="lblActivityDetails" BackColor="Transparent" BorderStyle="None" Width="150px" TextMode="MultiLine" Wrap="true" ReadOnly="true" Text='<%#Eval("Remarks")%>' runat="server"></asp:TextBox></td>
                                    <td>
                                        <asp:LinkButton ID="lbtnFileName" runat="server" BackColor="Transparent" BorderStyle="None" Width="20px" Text='<%#Eval("FileName")%>' ></asp:LinkButton>                                        
                                        <asp:FileUpload runat="server" ID="fp1"  />
                                    </td>                                   
                                       <td>
                                    <asp:ImageButton runat="server" ID="lbtnup" ImageUrl="~/Images/tick.png" CommandName="Upload"></asp:ImageButton>
                                    <asp:ImageButton runat="server" ID="lbtndel" ImageUrl="~/Images/delete.png" CommandName="DelFile"></asp:ImageButton>                               
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="lbtnEdit" Font-Underline="true" CommandName="edit" runat="server">Edit</asp:LinkButton>
                                        <asp:LinkButton ID="lbtnDelete" Font-Underline="true" CommandName="delete" runat="server">Delete</asp:LinkButton>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                <tr id="Tr5" runat="server" style="background-color: #F7F7DE">
                                    <td>
                                        <asp:Label ID="lblSlNo" Text='<%#Eval("SlNo")%>' runat="server"></asp:Label></td>
                                    <td>
                                        <asp:TextBox ID="lblActivityDt" BackColor="Transparent" BorderStyle="None" Width="100px" ReadOnly="true" Text='<%#Eval("ActivityDt")%>' runat="server"></asp:TextBox></td>
                                    <td>
                                        <asp:TextBox ID="lblChannel" BackColor="Transparent" BorderStyle="None" Width="100px" TextMode="MultiLine" Wrap="true" ReadOnly="true" Text='<%#Eval("Forum")%>' runat="server"></asp:TextBox></td>
                                    <td>
                                        <asp:TextBox ID="lblActivityType" BackColor="Transparent" BorderStyle="None" Width="150px" TextMode="MultiLine" Wrap="true" ReadOnly="true" Text='<%#Eval("ActivityType")%>' runat="server"></asp:TextBox></td>
                                    <td>
                                        <asp:TextBox ID="lblActivityDetails" BackColor="Transparent" BorderStyle="None" Width="150px" TextMode="MultiLine" Wrap="true" ReadOnly="true" Text='<%#Eval("Remarks")%>' runat="server"></asp:TextBox></td>
                                   <td> <asp:LinkButton ID="lbtnFileName" runat="server" BackColor="Transparent" BorderStyle="None" Width="20px" Text='<%#Eval("FileName")%>'></asp:LinkButton>
                                       <asp:FileUpload runat="server" ID="fp1"  />
                                   </td>
                                    <td>
                                    <asp:ImageButton runat="server" ID="lbtnup"  ImageUrl="~/Images/tick.png" CommandName="Upload"></asp:ImageButton>
                                    <asp:ImageButton runat="server" ID="lbtndel" ImageUrl="~/Images/delete.png" CommandName="DelFile"></asp:ImageButton>
                                </td>
                                    <td>
                                        <asp:LinkButton ID="lbtnEdit" Font-Underline="true" CommandName="edit" runat="server">Edit</asp:LinkButton>
                                        <asp:LinkButton ID="lbtnDelete" Font-Underline="true" CommandName="delete" runat="server">Delete</asp:LinkButton>
                                    </td>
                                </tr>
                            </AlternatingItemTemplate>                            
                            <InsertItemTemplate>
                                <tr id="Tr4" runat="server">
                                    <td>
                                        <asp:Label ID="lblNewSlNo" Text='<%#Eval("SlNo")%>' runat="server"></asp:Label></td>
                                    <td>
                                        <asp:TextBox ID="txtActivityDt" BorderStyle="None" Width="100px" Text='<%#Eval("ActivityDt")%>' runat="server"></asp:TextBox>
                                        <br />
                                        (dd/mm/yyyy) </td>
                                    <td>
                                        <asp:TextBox ID="txtChannel" BorderStyle="None" Width="100px" TextMode="MultiLine" Wrap="true" Text='<%#Eval("Forum")%>' runat="server"></asp:TextBox></td>
                                    <td>
                                        <asp:TextBox ID="txtActivityType" BorderStyle="None" Width="150px" TextMode="MultiLine" Wrap="true" Text='<%#Eval("ActivityType")%>' runat="server"></asp:TextBox></td>
                                    <td>
                                        <asp:TextBox ID="txtActivityDetails" BorderStyle="None" Width="150px" TextMode="MultiLine" Wrap="true" Text='<%#Eval("Remarks")%>' runat="server"></asp:TextBox></td>
                                   <td><asp:FileUpload runat="server" ID="fp1" /></td>
                                    <td>
                                    <asp:ImageButton runat="server" ID="lbtnup"  ImageUrl="~/Images/tick.png" CommandName="Upload"></asp:ImageButton>
                                    <asp:ImageButton runat="server" ID="lbtndel"  ImageUrl="~/Images/delete.png" CommandName="DelFile"></asp:ImageButton>
                                </td>
                                    <td>
                                        <asp:LinkButton ID="lbtnInsert" Font-Underline="true" CommandName="insert" runat="server">Insert</asp:LinkButton>
                                        <asp:LinkButton ID="lbtnCancel" Font-Underline="true" CommandName="cancel" runat="server">Cancel</asp:LinkButton>
                                    </td>
                                </tr>
                            </InsertItemTemplate>
                        </asp:ListView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>



