<%@ Page Title="" Language="C#" MasterPageFile="~/Parent.master" AutoEventWireup="true" CodeFile="ContractModify.aspx.cs" Inherits="ContractModify" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<script runat="server">

 
</script>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
    <title></title>
<link href="Styles/ContractTabStyle.css" rel="Stylesheet" type="text/css" />
<script src="Scripts/jquery-1.9.1.js" type="text/javascript"></script>
<script src="Scripts/jquery-ui-1.10.3.custom.min.js" type="text/javascript"></script>
<link href="Styles/jquery-ui-1.10.3.custom.min.css" rel="stylesheet" type="text/css" />
<link href="Styles/TabStyle.css" rel="Stylesheet" type="text/css" />
<script type="text/javascript">
    $(document).ready(function() {
    $("#<%=txtParty.ClientID %>").autocomplete({
        source: function(request, response) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "Contract.aspx/GetCompanyName",
                data: "{'company':'" + $("#<%=txtParty.ClientID %>").val() + "'}",
                dataType: "json",
                success: function(data) {
                    response(data.d);
                },
                error: function(result) {
                    alert("Error");
                }
            });
        },
        minLength: 2
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
function Verify() {
    var r = confirm('Are you sure; Do you want to delete this record?');
    if (r == true) {
        return true;
    }
    else {
        return false;
    }
}
</script>
    <style type="text/css">
        .style1
        {
            height: 62px;
        }
        .style2
        {
            Font-Size: 14px;
            font-family: Arial;
            font-style: normal;
            font-weight: bold;
            color: #844904;
            height: 20px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>    
<table align="center" class="neTable" width="970px" cellpadding="0px" cellspacing="0px">
<tr style="height:30px;"><td colspan="4" align="center" valign="top" style="font-size:medium; color:#397249; text-decoration:underline;">
    CONTRACT MODIFICATION</td></tr>
<tr align="center">
    <td valign="bottom">Contract Number &nbsp;&nbsp;
    <asp:TextBox ID="txtContractNo" runat="server" Width="100px"></asp:TextBox>&nbsp;&nbsp;<asp:Button 
            ID="btnFind" runat="server" CssClass="submitButton" Text="Find" 
            onclick="btnFind_Click" />&nbsp;&nbsp;
        <asp:Button ID="btnClear" runat="server" CssClass="submitButton" Text="Clear" 
            onclick="btnClear_Click" /></td></tr>    
<tr><td>
    <asp:Button ID="Tab1" BorderStyle="None" runat="server" Text="Contract" CssClass="Initial" 
        onclick="Tab1_Click" />
    <asp:Button ID="Tab2" BorderStyle="None" runat="server" Text="Action" CssClass="Initial" 
        onclick="Tab2_Click" />
    <asp:Button ID="Tab3" BorderStyle="None" runat="server" Text="Files" CssClass="Initial" 
        onclick="Tab3_Click"/>
    </td></tr>
    <tr><td>
    <asp:MultiView ID="MainView" runat="server">
    <asp:View ID="View1" runat="server">
    <table style="width:100%;border-width:1px;border-color:#666;border-style:solid;" cellpadding="4px" class="MVStyle" cellspacing="10px">
    <tr>
    <td>Type of Agreement</td>
    <td><asp:DropDownList ID="ddlAgreeType" runat="server"></asp:DropDownList></td>
    <td colspan="2" align="right"><asp:Button ID="btnUpdate" Visible="false" runat="server" 
            Text="Enable Update" BackColor="Transparent" Font-Names="Arial" ForeColor="#9f5a0b" 
            Font-Bold="true" BorderStyle="None" Font-Underline="true" 
            onclick="btnUpdate_Click" /></td></tr>
      <tr>
        <td>
            Title</td>
        <td colspan="3">
            <asp:TextBox ID="txtTitle" runat="server" TextMode="MultiLine" Width="750px"></asp:TextBox>
        </td></tr>
    <tr><td>Scope of Contract</td>
    <td colspan="3"><asp:TextBox ID="txtScope" runat="server" TextMode="MultiLine" 
            Width="750px"></asp:TextBox></td></tr>
    <tr><td>Contract Party</td>
    <td><asp:TextBox ID="txtParty" runat="server" Width="250px"> </asp:TextBox></td>
        <td>
            Coordinating Person</td>
        <td>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:DropDownList ID="ddlDept" runat="server" AutoPostBack="true" 
                        onselectedindexchanged="ddlDept_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlCoor" runat="server" Width="250px">
                    </asp:DropDownList>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
        </tr>
    <tr><td>Effective Date</td>
    <td><asp:TextBox ID="txtEffectiveDt" runat="server" Width="100px"></asp:TextBox>&nbsp;<asp:Image 
            ID="imgEffective" runat="server" ImageUrl="~/Images/GreenCalendar.gif" />
        <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" 
            PopupButtonID="imgEffective" TargetControlID="txtEffectiveDt">
        </asp:CalendarExtender>
        </td>
    <td>Expiry Date</td>
    <td>
        <asp:TextBox ID="txtExpiryDt" runat="server" Width="100px"> </asp:TextBox>
        &nbsp;<asp:Image ID="imgExpiry" runat="server" 
            ImageUrl="~/Images/GreenCalendar.gif" />
        <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" 
            PopupButtonID="imgExpiry" TargetControlID="txtExpiryDt">
        </asp:CalendarExtender>
    </td></tr>
    <tr><td>Agreement No.</td>
    <td><asp:TextBox ID="txtAgmtNo" runat="server" Width="150px"></asp:TextBox></td>
    <td>Remarks</td>
    <td><asp:TextBox ID="txtRemark" runat="server" Width="250px"></asp:TextBox></td></tr>
    
    <tr><td>Tech Transfer A/C</td>
    <td><asp:TextBox ID="txttrans" runat="server" Width="150px"></asp:TextBox></td><td>Status</td>
    <td><asp:DropDownList ID="dropStatus" runat="server">
      <%--<asp:ListItem>Draft-Party </asp:ListItem>
        <asp:ListItem>Draft-Legal</asp:ListItem>
        <asp:ListItem>Draft-IP Cell</asp:ListItem>
        <asp:ListItem>Executed-Subsisting</asp:ListItem>
        <asp:ListItem>Executed-Expired</asp:ListItem>
        <asp:ListItem>Executed-Rescinded</asp:ListItem>
        <asp:ListItem>Executed-Suspended</asp:ListItem>
        <asp:ListItem>Renewal Due</asp:ListItem>
        <asp:ListItem>To be amended</asp:ListItem>
        <asp:ListItem>Closed</asp:ListItem>--%>
        </asp:DropDownList></td></tr>
        
    <tr><td align="center" colspan="4">
        <asp:Button ID="btnContract" runat="server" Visible="false" CssClass="submitButton" 
            onclick="btnContract_Click" Text="Update Contract" />
        </td>
    </tr>
    </table>
    </asp:View>
    <asp:View ID="View2" runat="server">
    <table style="width:100%;border-width:1px;border-color:#666;border-style:solid;" class="MVStyle" cellspacing="10px">
    <tr><td align="right"><asp:Button ID="btnNewAction" runat="server" 
            Text="New Action" BackColor="Transparent" Font-Names="Arial" ForeColor="#9f5a0b" 
            Font-Bold="true" BorderStyle="None" Font-Underline="true" 
            onclick="btnNewAction_Click"/></td></tr>
    <tr><td></table>
    <asp:Panel ID="panUpdate" CssClass="borderStyle" Visible="false" Width="100%" runat="server">
    <table width="100%"  align="center">
    <tr><td id="thTitle" colspan="6" align="center" runat="server" class="style2">New 
        Action</td></tr>
    <tr><td>Sl.No.</td><td><asp:Label ID="lblSlNo" runat="server"></asp:Label></td>
    <td>Type</td>
    <td><asp:DropDownList ID="ddl1" runat="server">
    <asp:ListItem></asp:ListItem>
        <asp:ListItem>Upfront Payment</asp:ListItem>
        <asp:ListItem>Annual</asp:ListItem>
        <asp:ListItem>Royalty</asp:ListItem>
        <asp:ListItem>Reimbursement</asp:ListItem>
        <asp:ListItem>Equity</asp:ListItem>
        <asp:ListItem>Part Payment</asp:ListItem>
        </asp:DropDownList>
    </td>

    <td>Term</td>
<td><asp:DropDownList ID="ddl2" runat="server">
    <asp:ListItem></asp:ListItem>
    <asp:ListItem>Annual</asp:ListItem>
    <asp:ListItem>Once</asp:ListItem>
    <asp:ListItem>Quarterly</asp:ListItem>
    </asp:DropDownList></td>

    </tr>
    <tr>
        <td>
            DueDate</td>
        <td>
            <asp:TextBox ID="txtexecDt" runat="server" Height="22px" Width="100px"></asp:TextBox>
            &nbsp;<asp:Image ID="Image2" runat="server" ImageUrl="~/Images/GreenCalendar.gif" />
            <asp:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd/MM/yyyy" 
                PopupButtonID="imgInvoice" TargetControlID="txtexecDt">
            </asp:CalendarExtender>
        </td>
        <td>
            Basis</td>
        <td>
            <asp:TextBox ID="txtBasis" runat="server" Width="170px"></asp:TextBox>
        </td>
        <td>
            DeclDueAmt</td>
        <td>
            <asp:TextBox ID="txtDeclAmt" runat="server" Width="170px"></asp:TextBox>
        </td>
        </tr>
    </tr designer:mapid="1185">
        <tr>
            <td class="style1">
                InvoiceNo</td>
            <td class="style1">
                <asp:TextBox ID="txtInvoiceNo" runat="server" Width="120px"></asp:TextBox>
            </td>
            <td class="style1">
                Invoice Date</td>
            <td class="style1">
                <asp:TextBox ID="txtInvoiceDt" runat="server" Width="90px"></asp:TextBox>
                &nbsp;<asp:Image ID="imgTarget" runat="server" 
                    ImageUrl="~/Images/GreenCalendar.gif" />
                <%--<asp:CalendarExtender ID="CalendarExtender8" runat="server" Format="dd/MM/yyyy" 
                PopupButtonID="imgTarget" TargetControlID="txtInvRaised">
            </asp:CalendarExtender>--%>
            </td>
          
            <td class="style1">
                Receipt No</td>
            <td class="style1">
                <asp:TextBox ID="txtReceiptNo" runat="server" Width="150px"></asp:TextBox>
            </td>
            <tr>
                <td>
                    Receipt Date</td>
                <td>
                    <asp:TextBox ID="txtRecieptDate" runat="server" Width="150px"></asp:TextBox>
                    &nbsp;<asp:Image ID="Image4" runat="server" ImageUrl="~/Images/GreenCalendar.gif" />
                    <asp:CalendarExtender ID="CalendarExtender6" runat="server" Format="dd/MM/yyyy" 
                        PopupButtonID="imgInvoice" TargetControlID="txtRecieptDate">
                    </asp:CalendarExtender>
                </td>
                <td>
                    Mode</td>
                <td>
                    <asp:DropDownList ID="ddlMode" runat="server" Width="150">
                        <asp:ListItem></asp:ListItem>
                        <asp:ListItem>Cheque</asp:ListItem>
                        <asp:ListItem>Wire Transfer</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    Amount In FC</td>
                <td>
                    <asp:TextBox ID="txtAmtInFC" runat="server" Width="120px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Conversion Factor</td>
                <td>
                    <asp:TextBox ID="txtCF" runat="server" Width="120px"></asp:TextBox>
                </td>
                <td class="style1">
                    Amount In Rs</td>
                <td>
                    <asp:TextBox ID="txtAmtInRs" runat="server" Width="150px">
                    </asp:TextBox>
                </td>
                <td>
                    Status</td>
                <td>
                    <asp:DropDownList ID="drop1" runat="server" Width="150">
                        <asp:ListItem></asp:ListItem>
                        <asp:ListItem>Received</asp:ListItem>
                        <asp:ListItem>Pending</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    Remarks</td>
                <td>
                    <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" Width="269px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    <asp:Button ID="btnActSave" runat="server" CssClass="submitButton" 
                        onclick="btnActSave_Click" Text="Save" />
                    &nbsp;&nbsp;
                    <asp:Button ID="btnActUpdate" runat="server" CssClass="submitButton" 
                        onclick="btnActUpdate_Click" Text="Update" />
                    &nbsp;&nbsp;
                    <asp:Button ID="btnClose" runat="server" CssClass="submitButton" 
                        onclick="btnClose_Click" Text="Close" />
                </td>
            </tr>
        </tr>
    </table>
    </asp:Panel>    
    </td></tr>
    <tr><td><div>List of Action Plans</div></td></tr>
    <tr><td>
    <asp:ListView ID="lvAction" runat="server" onitemdeleting="lvAction_ItemDeleting" onitemdatabound="lvAction_ItemDataBound" 
            onitemcommand="lvAction_ItemCommand" 
            onselectedindexchanged="lvAction_SelectedIndexChanged" >
    <LayoutTemplate>
    <table id="Table1" width="950px" align="center" runat="server"  style="text-align:left;border-color:#994c00; border-style:solid; border-width:1px;" cellpadding="5" cellspacing="0" border="1">
    <tr id="Tr1" runat="server" style="background-color:#d2691e;color:#fffff0">
   <%-- <th id="Th1" align="center" runat="server">Contract No</th>--%>
    <th id="Th2" align="center" runat="server">Sl.No.</th>
    <th id="Th3" align="center" runat="server">Event</th>
    <th id="Th4" align="center" runat="server">Frequency</th>
    <th id="Th5" align="center" runat="server">Execution Date</th>
    <th id="Th6" align="center" runat="server">Basis</th>
    <th id="Th7" align="center" runat="server">Invoice No</th>
    <th id="Th8" align="center" runat="server">Invoice Date</th>
    <th id="Th9" align="center" runat="server">Receipt No</th>
    <th id="Th10" align="center" runat="server">Receipt Date</th>
    <th id="Th11" align="center" runat="server">Amount in RS</th>
    <th id="Th12" align="center" runat="server">Status</th>
    <th id="Th13" runat="server">Remarks</th>
    <th id="Th14" runat="server"></th>
    </tr>
    <tr runat="server" id="itemPlaceholder" />
    </table>
    </LayoutTemplate>
    <ItemTemplate>
    <tr id="Tr2" runat="server" style="background-color:White">
    <%-- <td><asp:Label ID="lblContractNo" runat="server" BackColor="Transparent" BorderStyle="None" Width="20px" Text='<%#Eval("ContractNo")%>'></asp:Label></td>--%>
    <td><asp:Label ID="lblSlNo" runat="server" BackColor="Transparent" BorderStyle="None" Width="20px" Text='<%#Eval("SlNO")%>'></asp:Label></td>
    <td><asp:Label ID="lblRequest" runat="server" BackColor="Transparent" BorderStyle="None" Width="30px" Text='<%#Eval("Event")%>'></asp:Label></td>
    <td><asp:Label ID="lblFrequency" runat="server" BackColor="Transparent" BorderStyle="None" Width="40px" Text='<%#Eval("Frequency")%>'></asp:Label></td>
     <td><asp:Label ID="lblExecDt" runat="server" BackColor="Transparent" BorderStyle="None" Width="40px" Text='<%#Eval("ExecutionDate")%>'></asp:Label></td>
    <td><asp:Label ID="lblBasis" runat="server" BackColor="Transparent" BorderStyle="None" Width="90px" Text='<%#Eval("Basis")%>'></asp:Label></td>
    <td><asp:Label ID="lblInvoiceNo" runat="server" BackColor="Transparent" BorderStyle="None" Width="50px" Text='<%#Eval("InvoiceNo","{0:dd/MM/yyyy}")%>'></asp:Label></td>
    <td><asp:Label ID="lblInvoiceDate" runat="server" BackColor="Transparent" BorderStyle="None" Width="50px" Text='<%#Eval("InvoiceRaised")%>'></asp:Label></td>
    <td><asp:Label ID="lblRecieptNo" runat="server" BackColor="Transparent" BorderStyle="None" Width="50px" Text='<%#Eval("RecieptNo")%>'></asp:Label></td>
    <td><asp:Label ID="lblReceiptDate" runat="server" BackColor="Transparent" BorderStyle="None" Width="50px" Text='<%#Eval("RecieptDate")%>'></asp:Label></td>
    <td><asp:Label ID="lblAmtInRs" runat="server" BackColor="Transparent" BorderStyle="None" Width="50px" Text='<%#Eval("AmountInRs")%>'></asp:Label></td>
    <td><asp:Label ID="lblStatus" runat="server" BackColor="Transparent" BorderStyle="None" Width="50px" Text='<%#Eval("Status")%>'></asp:Label></td>
    <td><asp:Label ID="lblRemarks" runat="server" BackColor="Transparent" BorderStyle="None" Width="50px" Text='<%#Eval("Remarks")%>'></asp:Label></td>
    <td><asp:LinkButton ID="lbtnUpdate" Font-Underline="true" CommandName="Modify" runat="server">Update</asp:LinkButton>
    <asp:LinkButton ID="lbtnDelete" Font-Underline="true" OnClientClick="return DeleteVerify()" CommandName="delete" runat="server">Delete</asp:LinkButton>    
    </td>
    </tr>
</ItemTemplate>
<AlternatingItemTemplate>
    <tr id="Tr3" runat="server" style="background-color:#F7F7DE">
     <td><asp:Label ID="lblSlNo" runat="server" BackColor="Transparent" BorderStyle="None" Width="20px" Text='<%#Eval("SlNO")%>'></asp:Label></td>
  <td><asp:Label ID="lblRequest" runat="server" BackColor="Transparent" BorderStyle="None" Width="30px" Text='<%#Eval("Event")%>'></asp:Label></td>
    <td><asp:Label ID="lblFrequency" runat="server" BackColor="Transparent" BorderStyle="None" Width="40px" Text='<%#Eval("Frequency")%>'></asp:Label></td>
     <td><asp:Label ID="lblExecDt" runat="server" BackColor="Transparent" BorderStyle="None" Width="40px" Text='<%#Eval("ExecutionDate")%>'></asp:Label></td>
    <td><asp:Label ID="lblBasis" runat="server" BackColor="Transparent" BorderStyle="None" Width="90px" Text='<%#Eval("Basis")%>'></asp:Label></td>
    <td><asp:Label ID="lblInvoiceNo" runat="server" BackColor="Transparent" BorderStyle="None" Width="50px" Text='<%#Eval("InvoiceNo","{0:dd/MM/yyyy}")%>'></asp:Label></td>
    <td><asp:Label ID="lblInvoiceDate" runat="server" BackColor="Transparent" BorderStyle="None" Width="50px" Text='<%#Eval("InvoiceRaised")%>'></asp:Label></td>
    <td><asp:Label ID="lblRecieptNo" runat="server" BackColor="Transparent" BorderStyle="None" Width="50px" Text='<%#Eval("RecieptNo")%>'></asp:Label></td>
    <td><asp:Label ID="lblReceiptDate" runat="server" BackColor="Transparent" BorderStyle="None" Width="50px" Text='<%#Eval("RecieptDate")%>'></asp:Label></td>
    <td><asp:Label ID="lblAmtInRs" runat="server" BackColor="Transparent" BorderStyle="None" Width="50px" Text='<%#Eval("AmountInRs")%>'></asp:Label></td>
    <td><asp:Label ID="lblStatus" runat="server" BackColor="Transparent" BorderStyle="None" Width="50px" Text='<%#Eval("Status")%>'></asp:Label></td>
    <td><asp:Label ID="lblRemarks" runat="server" BackColor="Transparent" BorderStyle="None" Width="50px" Text='<%#Eval("Remarks")%>'></asp:Label></td>
 <td>
    <asp:LinkButton ID="lbtnUpdate" Font-Underline="true" CommandName="Modify" runat="server">Update</asp:LinkButton>
    <asp:LinkButton ID="lbtnDelete" Font-Underline="true" OnClientClick="return DeleteVerify()" CommandName="delete" runat="server">Delete</asp:LinkButton>
    </td>
    </tr>
</AlternatingItemTemplate>
</asp:ListView>
</td></tr>
</table>
</asp:View>--%&gt;
<asp:View ID="View3" runat="server">
    <table style="width:100%;border-width:1px;border-color:#666;border-style:solid;"  class="MVStyle" cellspacing="10px">
    <tr><td align="right"><asp:Button ID="btnUpload" runat="server" 
            Text="Upload File" BackColor="Transparent" Font-Names="Arial" ForeColor="#9f5a0b"
            Font-Bold="true" BorderStyle="None" Font-Underline="true" 
            onclick="btnUpload_Click" /></td></tr>
    <tr><td>
    <asp:Panel ID="panNewFile" CssClass="borderStyle" Visible="false" Width="100%" runat="server">
    <table width="100%"  align="center">
    <tr><td id="Td1" colspan="4" align="center" class="TitleStyle">New File Upload</td></tr>
    <tr> <td>Description of File</td><td><asp:TextBox ID="txtFileDesc" Width="300px" runat="server"></asp:TextBox></td>
    <td>Uploading File</td>
    <td><asp:FileUpload ID="FileUploadAgree" runat="server" /></td>
    </tr><tr>
    <td>Comments</td><td><asp:TextBox ID="txtComment" Width="300" runat="server"></asp:TextBox></td>
    <td colspan="2" align="center">
        <asp:Button ID="btnFileSave" runat="server" 
            CssClass="submitButton" Text="Save" onclick="btnFileSave_Click" />&nbsp;&nbsp;
    <asp:Button ID="btnFileClose" runat="server" CssClass="submitButton" Text="Close" 
            onclick="btnFileClose_Click"/>
    </td></tr>
    </table>
    </asp:Panel>    
    </td></tr>
    <tr><td><div>List of Contract Files</div></td></tr>
    <tr><td>
 <asp:GridView ID="gvFileDetails" AutoGenerateColumns="false" Width="950px"  
         CellPadding="5" CellSpacing="0" 
         Font-Names="Arial" BorderColor="#1b5c09" runat="server" 
         AllowSorting="true" AllowPaging="true" PageSize="25"
         onpageindexchanging="gvFileDetails_PageIndexChanging" 
         onselectedindexchanging="gvFileDetails_SelectedIndexChanging" 
         onsorting="gvFileDetails_Sorting" 
         onrowdatabound="gvFileDetails_RowDataBound" 
         onrowcommand="gvFileDetails_RowCommand" 
         onrowdeleting="gvFileDetails_RowDeleting">
 <Columns>
 <asp:BoundField DataField="EntryDt" HeaderStyle-Font-Underline="true" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Entry Dt." SortExpression="EntryDt"  />
 <asp:BoundField HeaderStyle-Font-Underline="true" DataField="ContractNo" HeaderText="File No." SortExpression="ContractNo"  />
 <asp:BoundField ItemStyle-Width="170" ItemStyle-Wrap="true" HeaderStyle-Font-Underline="true" DataField="FileDescription" HeaderText="File Description" SortExpression="FileDescription" />
 <asp:BoundField HeaderStyle-Font-Underline="true" DataFormatString="{0:dd/MM/yyyy}" DataField="ModifiedDt" SortExpression="ModifiedDt" HeaderText="Modified Dt." />
 <asp:BoundField ItemStyle-Width="120" ItemStyle-Wrap="true" HeaderStyle-Font-Underline="true" DataField="Comments" SortExpression="Comments" HeaderText="Comments" />
 <asp:TemplateField HeaderText="File Name">
 <ItemTemplate>
    <asp:LinkButton ID="lbtnEdit" Text='<%#Eval("FileName")%>' Font-Underline="true" CommandName="Select" runat="server"></asp:LinkButton> 
 </ItemTemplate> 
 </asp:TemplateField>
 <asp:TemplateField>
 <ItemTemplate>
    <asp:LinkButton ID="lbtnDelete" Text="Delete" OnClientClick="return Verify()" Font-Underline="true" CommandName="Delete" runat="server"></asp:LinkButton>
 </ItemTemplate>
 </asp:TemplateField>
 </Columns>
 <EmptyDataTemplate>
 <p style="text-align:center; background-color:#C8C8C8">
 <asp:Image ID="Image1" BorderStyle="None" BackColor="#C8C8C8" ImageUrl="~/Images/NoData.jpg" runat="server" />
 </p>
 </EmptyDataTemplate>
 <HeaderStyle CssClass="HeaderStyle" ForeColor="Ivory" HorizontalAlign="Center" VerticalAlign="Middle" Height="25px" />
 <RowStyle BackColor="White" HorizontalAlign="Left" Height="25px" Font-Size="12px" Wrap="true"/>
 <AlternatingRowStyle Wrap="true" HorizontalAlign="Left" BackColor="#d1e1cd" />
 <PagerSettings Mode="Numeric" PageButtonCount="9" Position="TopAndBottom"/>
 <PagerStyle Font-Underline="true" Font-Bold="true" Font-Italic="true" ForeColor="Red"/>
 </asp:GridView> 
 <i id="pageNumber" visible="false" style="font-weight:bold" runat="server"> You are 
        viewing page <%=gvFileDetails.PageIndex + 1%> of <%= gvFileDetails.PageCount%></i> 
 </td></tr>
 </table> 
 </td></tr>
</table>
</asp:View>
</asp:MultiView>
</td></tr>
<tr><td>&nbsp;</td></tr>
</table>
</asp:Content>

