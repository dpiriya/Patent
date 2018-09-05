<%@ Page Language="C#" MasterPageFile="~/Parent.master" AutoEventWireup="true" CodeFile="FileCreation.aspx.cs" Inherits="FileCreation" Title="File Creation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="Server">
    <title>File Creation</title>
    <link href="Styles/newEntry.css" rel="Stylesheet" type="text/css" />
    <script src="Scripts/jquery-1.9.1.js" type="text/javascript"></script>
    <script src="Scripts/jquery-ui-1.10.3.custom.min.js" type="text/javascript"></script>
    <link href="Styles/jquery-ui-1.10.3.custom.min.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function () {
            $("table.neTable tr:even").addClass("tdTable");
            $("table.neTable tr:odd").addClass("tdTable1");
        });
        function Verify() {
            var r = confirm('Are you sure; Do you want to insert this record?');
            if (r == true) {
                if (document.getElementById('<%=txtFileNo.ClientID%>').value == "") {
                    alert('File No. can not be empty');
                    return false;
                }
                if (document.getElementById('<%=txtTitle.ClientID%>').value == "") {
                    alert('Title can not be empty');
                    return false;
                }
                if (document.getElementById('<%=ddlIPR.ClientID%>').selectedIndex == 0 || document.getElementById('<%=ddlIPR.ClientID%>').selectedIndex == -1) {
                    alert('IPR Category can not be empty');
                    return false;
                }
                if (document.getElementById('<%=ddlType1.ClientID%>').selectedIndex == 0 || document.getElementById('<%=ddlType1.ClientID%>').selectedIndex == -1) {
                    alert('Select First Inventor Type');
                    return false;
                }

                if (document.getElementById('<%=ddlType1.ClientID%>').selectedIndex != 4) {
                    if (document.getElementById('<%=ddlDept1.ClientID%>').selectedIndex == 0 || document.getElementById('<%=ddlDept1.ClientID%>').selectedIndex == -1) {
                        alert('Select First Inventor Department');
                        return false;
                    }
                    if (document.getElementById('<%=ddlType1.ClientID%>').selectedIndex == 1) {
                        if (document.getElementById('<%=ddlCoor1.ClientID%>').selectedIndex == 0 || document.getElementById('<%=ddlCoor1.ClientID%>').selectedIndex == -1) {
                            alert('Select First Inventor Name');
                            return false;
                        }
                    }
                    else if (document.getElementById('<%=ddlType1.ClientID%>').selectedIndex == 2 || document.getElementById('<%=ddlType1.ClientID%>').selectedIndex == 3) {
                        if (document.getElementById('<%=txtCoor1.ClientID%>').value == "") {
                            alert('Enter First Inventor Name');
                            return false;
                        }
                    }
                }
                else {
                    if (document.getElementById('<%=txtDept1.ClientID%>').value == "") {
                        alert('Organization Name can not be empty');
                        return false;
                    }
                    if (document.getElementById('<%=txtCoor1.ClientID%>').value == "") {
                        alert('First Inventor Name can not be empty');
                        return false;
                    }
                }

                return true;
            }
            else {
                return false;
            }
        }
    </script>

    <style type="text/css">
        .style3 {
            width: 260px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">
    &nbsp;<table align="center" class="neTable" cellpadding="10px" width="970px" cellspacing="8px">
        <asp:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server">
        </asp:ToolkitScriptManager>
        <tr>
            <td colspan="4" align="center" valign="middle" style="font-size: medium; text-decoration: underline;">PATENT FILE CREATION</td>
        </tr>
        <tr>
            <td class="style3">File Number</td>
            <td>
                <asp:TextBox ID="txtFileNo" Enabled="false" runat="server"></asp:TextBox></td>
            <td>Title</td>
            <td>
                <asp:TextBox ID="txtTitle" runat="server" TextMode="MultiLine" Width="275px"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="style3">IPR Category</td>
            <td>
                <asp:DropDownList ID="ddlIPR" runat="server">
                </asp:DropDownList>
            </td>
            <td>Initial Filing</td>
            <td>
                <asp:DropDownList ID="ddlIFiling" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="style3">First Applicant</td>
            <td>
                <asp:TextBox ID="txtFApp" Width="275px" runat="server"></asp:TextBox>
            </td>
            <td>Second Applicant</td>
            <td>
                <asp:TextBox ID="txtSApp" Width="275px" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="style3">First Inventor Type.</td>
            <td>
                <asp:DropDownList ID="ddlType1" runat="server" AutoPostBack="true"
                    OnSelectedIndexChanged="ddlType1_SelectedIndexChanged">
                </asp:DropDownList></td>
            <td id="tdFirstInvent" runat="server">First Inventor Dept.</td>
            <td>
                <asp:DropDownList ID="ddlDept1" Width="70px"
                    runat="server" OnSelectedIndexChanged="ddlDept1_SelectedIndexChanged" AutoPostBack="true">
                </asp:DropDownList>
                <asp:TextBox ID="txtDept1" Width="200px" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style3">First Inventor Name</td>
            <td>
                <asp:DropDownList ID="ddlCoor1" Width="250px" runat="server"
                    OnSelectedIndexChanged="ddlCoor1_SelectedIndexChanged" AutoPostBack="true">
                </asp:DropDownList>
                <asp:TextBox ID="txtCoor1" Visible="false" Width="250px" runat="server"></asp:TextBox>
                <asp:TextBox ID="txtCoorID" Width="75px" runat="server"></asp:TextBox>
            </td>
            <td>Date of Request</td>
            <td>
                <asp:TextBox ID="txtRequest" runat="server"></asp:TextBox>&nbsp;<asp:Image ID="imgRequest" ImageUrl="~/Images/RedCalendar.gif" runat="server" />
                <asp:CalendarExtender ID="CalendarExtender1" TargetControlID="txtRequest" PopupButtonID="imgRequest" runat="server" Format="dd/MM/yyyy">
                </asp:CalendarExtender>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <div style="text-decoration: underline;">List of Co Inventors</div>
                <br />
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:ListView ID="lvInventor" runat="server" InsertItemPosition="LastItem"
                            OnItemCreated="lvInventor_ItemCreated" OnItemInserting="lvInventor_ItemInserting"
                            OnItemCanceling="lvInventor_ItemCanceling" OnItemDeleting="lvInventor_ItemDeleting"
                            OnItemDataBound="lvInventor_ItemDataBound">
                            <LayoutTemplate>
                                <table id="Table1" width="950px" align="center" runat="server" style="text-align: left; border-color: #DEDFDE; border-style: solid; border-width: 1px;" cellpadding="5" cellspacing="0" border="1">
                                    <tr id="Tr1" runat="server" style="background-color: #f9cfaa; padding-top: 0px; padding-bottom: 0px; height: 25px;">
                                        <th id="Th1" align="center" runat="server">Sl.No.</th>
                                        <th id="Th2" align="center" runat="server">Inventor Type</th>
                                        <th id="Th6" align="center" runat="server">Department Code</th>
                                        <th id="Th3" align="center" runat="server">Department/Organization</th>
                                        <th id="Th4" align="center" runat="server">Inventor ID</th>
                                        <th id="Th7" align="center" runat="server">Inventor Name</th>
                                        <th id="Th5" runat="server">
                                            <asp:ImageButton ID="btnInventorAdd" Width="25px" Height="25px" ImageUrl="~/Images/Plus.gif" runat="server" />
                                            <asp:ImageButton ID="btnInventorClear" Width="25px" Height="25px" ImageUrl="~/Images/Minus.gif" runat="server" />
                                        </th>
                                    </tr>
                                    <tr id="itemPlaceholder" runat="server"></tr>

                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr id="Tr2" runat="server" style="background-color: White">
                                    <td>
                                        <asp:Label ID="lblSlNo" Text='<%#Eval("SlNo") %>' runat="server"></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblType" runat="server" BackColor="Transparent" BorderStyle="None" Width="50px" Text='<%#Eval("InventorType")%>'></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblDeptCode" runat="server" BackColor="Transparent" BorderStyle="None" Width="70px" Text='<%#Eval("DeptCode")%>'></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblDept" runat="server" BackColor="Transparent" BorderStyle="None" Width="250px" Text='<%#Eval("DeptOrgName")%>'></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblID" runat="server" BackColor="Transparent" BorderStyle="None" Width="90px" Text='<%#Eval("InventorID")%>'></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblName" runat="server" BackColor="Transparent" BorderStyle="None" Width="250px" Text='<%#Eval("InventorName")%>'></asp:Label></td>
                                    <td>
                                        <asp:LinkButton ID="lbtnDelete" Font-Underline="true" CommandName="delete" runat="server">Delete</asp:LinkButton></td>
                                </tr>
                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                <tr id="Tr3" runat="server" style="background-color: #F7F7DE">
                                    <td>
                                        <asp:Label ID="lblSlNo" Text='<%#Eval("SlNo")%>' runat="server"></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblType" runat="server" BackColor="Transparent" BorderStyle="None" Width="50px" Text='<%#Eval("InventorType")%>'></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblDeptCode" runat="server" BackColor="Transparent" BorderStyle="None" Width="70px" Text='<%#Eval("DeptCode")%>'></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblDept" runat="server" BackColor="Transparent" BorderStyle="None" Width="250px" Text='<%#Eval("DeptOrgName")%>'></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblID" runat="server" BackColor="Transparent" BorderStyle="None" Width="90px" Text='<%#Eval("InventorID")%>'></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblName" runat="server" BackColor="Transparent" BorderStyle="None" Width="250px" Text='<%#Eval("InventorName")%>'></asp:Label></td>
                                    <td>
                                        <asp:LinkButton ID="lbtnDelete" Font-Underline="true" CommandName="delete" runat="server">Delete</asp:LinkButton>
                                    </td>
                                </tr>
                            </AlternatingItemTemplate>
                            <InsertItemTemplate>
                                <tr id="Tr4" runat="server">
                                    <td>
                                        <asp:Label ID="lblNewSlNo" runat="server"></asp:Label></td>
                                    <td>
                                        <asp:DropDownList ID="ddlNewType" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlType_SelectedIndexChanged"></asp:DropDownList></td>
                                    <td>
                                        <asp:DropDownList ID="ddlNewDeptCode" Width="60px" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged"></asp:DropDownList></td>
                                    <td>
                                        <asp:TextBox ID="txtDept" Width="250px" TextMode="MultiLine" Wrap="true" runat="server"></asp:TextBox></td>
                                    <td>
                                        <asp:TextBox ID="txtID" Width="75px" runat="server"></asp:TextBox></td>
                                    <td>
                                        <asp:DropDownList ID="ddlNewName" Width="250px" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlCoor_SelectedIndexChanged"></asp:DropDownList>
                                        <asp:TextBox ID="txtName" Visible="false" Width="250px" TextMode="MultiLine" Wrap="true" runat="server"></asp:TextBox></td>
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
            <td colspan="4">Source of Invention from Research Projects&nbsp;
                <asp:DropDownList ID="ddlSource" Width="50px" runat="server" AutoPostBack="true"
                    OnSelectedIndexChanged="ddlSource_SelectedIndexChanged">
                </asp:DropDownList>
                &nbsp;&nbsp;Direct Strategy&nbsp;
                <asp:DropDownList ID="dropstrategy" runat="server">
                    <asp:ListItem>No</asp:ListItem>
                    <asp:ListItem>Yes</asp:ListItem>
                </asp:DropDownList>
                &nbsp; Application Post Dated&nbsp;&nbsp;<asp:DropDownList ID="DropPost" runat="server">
                    <asp:ListItem>No</asp:ListItem>
                    <asp:ListItem>Yes</asp:ListItem>
                </asp:DropDownList>
                &nbsp; Post Dated Date
                <asp:TextBox ID="txtPost" runat="server"></asp:TextBox>&nbsp;<asp:Image ID="imagecal" ImageUrl="~/Images/RedCalendar.gif" runat="server" /><asp:CalendarExtender ID="CalendarExtender2" TargetControlID="txtPost" PopupButtonID="imagecal" runat="server" Format="dd/MM/yyyy">
                </asp:CalendarExtender>
            </td>
        </tr>
        <%--<td>Source of Invention from Research Projects <asp:DropDownList ID="ddlSource" runat="server" AutoPostBack="true" onselectedindexchanged="ddlSource_SelectedIndexChanged" ></asp:DropDownList></td>
<td>Direct Strategy&nbsp;&nbsp;&nbsp;<asp:DropDownList ID="dropstrategy" runat="server">
        <asp:ListItem>Yes</asp:ListItem>
        <asp:ListItem>No</asp:ListItem>
    </asp:DropDownList></td>   
<td>Application Post Dated&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:DropDownList ID="DropPost" runat="server"><asp:ListItem>Yes</asp:ListItem>
        <asp:ListItem>No</asp:ListItem></asp:DropDownList></td>
   <td>Post Dated Date <asp:TextBox ID="txtPost" runat="server"></asp:TextBox>&nbsp;<asp:Image ID="imagecal" ImageUrl="~/Images/RedCalendar.gif" runat="server" /><asp:CalendarExtender ID="CalendarExtender2" TargetControlID="txtPost" PopupButtonID="imagecal" runat="server" Format="dd/MM/yyyy">
    </asp:CalendarExtender></td>--%>
        <tr id="ViewProjectList" visible="false" runat="server">
            <td colspan="4">
                <div style="text-decoration: underline;">Research Projects List</div>
                <br />
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <asp:ListView ID="lvProjectList" runat="server" InsertItemPosition="LastItem"
                            OnItemCanceling="lvProjectList_ItemCanceling"
                            OnItemCreated="lvProjectList_ItemCreated"
                            OnItemDataBound="lvProjectList_ItemDataBound"
                            OnItemDeleting="lvProjectList_ItemDeleting"
                            OnItemInserting="lvProjectList_ItemInserting"
                            OnItemUpdating="lvProjectList_ItemUpdating"
                            OnSelectedIndexChanged="lvProjectList_SelectedIndexChanged">
                            <LayoutTemplate>
                                <table id="Table1" width="950px" align="center" runat="server" style="text-align: left; border-color: #DEDFDE; border-style: solid; border-width: 1px;" cellpadding="5" cellspacing="0" border="1">
                                    <tr id="Tr1" runat="server" style="background-color: #f9cfaa; padding-top: 0px; padding-bottom: 0px; height: 25px;">
                                        <th id="Th1" align="center" runat="server">Sl.No.</th>
                                        <th id="Th2" align="center" runat="server">Project Type</th>
                                        <th id="Th8" align="center" runat="server">Financial Year</th>
                                        <th id="Th6" align="center" runat="server">Project Number</th>
                                        <th id="Th3" align="center" runat="server">Funding Agency</th>
                                        <th id="Th4" align="center" runat="server">Title of Project</th>
                                        <th id="Th5" runat="server"></th>
                                    </tr>
                                    <tr runat="server" id="itemPlaceholder" />
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr id="Tr2" runat="server" style="background-color: White">
                                    <td>
                                        <asp:Label ID="lblProjectSlNo" Text='<%#Eval("SlNo")%>' runat="server"></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblProjectType" runat="server" BackColor="Transparent" BorderStyle="None" Text='<%#Eval("ProjectType")%>'></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblFinYear" runat="server" BackColor="Transparent" BorderStyle="None" Text='<%#Eval("FinancialYear")%>'></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblProjectNo" runat="server" BackColor="Transparent" BorderStyle="None" Text='<%#Eval("ProjectNumber")%>'></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblFundAgency" runat="server" BackColor="Transparent" BorderStyle="None" Width="175px" Text='<%#Eval("FundingAgency")%>'></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblTitle" runat="server" BackColor="Transparent" BorderStyle="None" Width="250px" Text='<%#Eval("ProjectTitle")%>'></asp:Label></td>
                                    <td>
                                        <asp:LinkButton ID="lbtnDelete" Font-Underline="true" CommandName="delete" runat="server">Delete</asp:LinkButton>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                <tr id="Tr3" runat="server" style="background-color: #F7F7DE">
                                    <td>
                                        <asp:Label ID="lblProjectSlNo" Text='<%#Eval("SlNo")%>' runat="server"></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblProjectType" runat="server" BackColor="Transparent" BorderStyle="None" Text='<%#Eval("ProjectType")%>'></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblFinYear" runat="server" BackColor="Transparent" BorderStyle="None" Text='<%#Eval("FinancialYear")%>'></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblProjectNo" runat="server" BackColor="Transparent" BorderStyle="None" Text='<%#Eval("ProjectNumber")%>'></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblFundAgency" runat="server" BackColor="Transparent" BorderStyle="None" Width="175px" Text='<%#Eval("FundingAgency")%>'></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblTitle" runat="server" BackColor="Transparent" BorderStyle="None" Width="250px" Text='<%#Eval("ProjectTitle")%>'></asp:Label></td>
                                    <td>
                                        <asp:LinkButton ID="lbtnDelete" Font-Underline="true" CommandName="delete" runat="server">Delete</asp:LinkButton>
                                    </td>
                                </tr>
                            </AlternatingItemTemplate>
                            <InsertItemTemplate>
                                <tr id="Tr4" runat="server">
                                    <td>
                                        <asp:Label ID="lblProjectSlNo" runat="server"></asp:Label></td>
                                    <td>
                                        <asp:DropDownList ID="ddlNewProjectType" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlNewProjectType_SelectedIndexChanged"></asp:DropDownList></td>
                                    <td>
                                        <asp:DropDownList ID="ddlFinYear" Width="90px" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlFinYear_SelectedIndexChanged"></asp:DropDownList></td>
                                    <td>
                                        <asp:DropDownList ID="ddlNewProjectNo" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlNewProjectNo_SelectedIndexChanged"></asp:DropDownList></td>
                                    <td>
                                        <asp:TextBox ID="txtAgency" Width="175px" TextMode="MultiLine" Wrap="true" runat="server"></asp:TextBox></td>
                                    <td>
                                        <asp:TextBox ID="txtProjectTitle" Width="220px" TextMode="MultiLine" runat="server"></asp:TextBox></td>
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
            <td class="style3">Write Up from IDF</td>
            <td>
                <asp:TextBox ID="txtWriteUp" runat="server" TextMode="MultiLine" Width="275px" onkeydown="return(event.keyCode !=13);"></asp:TextBox></td>
            <td>Remarks</td>
            <td>
                <asp:TextBox ID="txtComments" runat="server" TextMode="MultiLine" Width="275px"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Write Up for Tech Transfer</td>
            <td>
                <asp:TextBox ID="txtAbstract" runat="server" TextMode="MultiLine" Width="275px"></asp:TextBox></td>
            <%--<td>Designs</td> --%>
            <td>
                <asp:FileUpload ID="FileUpload1" runat="server" />
                <asp:Label Text="Image size should not exceed 30MB" ID="lblupload" Visible="false" runat="server"></asp:Label>
            </td>
            <td></td>
        </tr>
        <%--<tr><td>Software</td><td><asp:TextBox ID="TxtSoftware" runat="server" TextMode="MultiLine"></asp:TextBox></td>
        <td>Other IP</td><td><asp:FileUpload ID="FileUpload2" runat="server" /></td></tr>
        <tr><td>Solution Packages</td>
        <td><asp:FileUpload ID="FileUpload3" runat="server" /></td></tr>
        --%>
        <tr>
            <td colspan="4" align="center">

                <asp:ImageButton ID="imgBtnInsert" ImageUrl="~/Images/Save3.png" runat="server"
                    OnClientClick="return Verify()" OnClick="imgBtnInsert_Click" />&nbsp;
  <asp:ImageButton ID="imgBtnClear" runat="server"
      ImageUrl="~/Images/Clear3.png" OnClick="imgBtnClear_Click"
      Style="width: 74px" />
            </td>
        </tr>
    </table>
</asp:Content>

