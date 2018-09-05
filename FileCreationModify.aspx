<%@ Page Language="C#" MasterPageFile="~/Parent.master" AutoEventWireup="true" CodeFile="FileCreationModify.aspx.cs" Inherits="FileCreationModify" Title="File Creation Modification" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
<link href="Styles/newEntry.css" rel="Stylesheet" type="text/css" />
<script src="Scripts/jquery-1.9.1.js" type="text/javascript"></script>
<script src="Scripts/jquery-ui-1.10.3.custom.min.js" type="text/javascript"></script>
<link href="Styles/jquery-ui-1.10.3.custom.min.css" rel="stylesheet" type="text/css" />
<script type="text/javascript">
$(document).ready(function(){
  
    $("table.neTable tr:even").addClass("tdTable");
    $("table.neTable tr:odd").addClass("tdTable1");
    
    $("#<%=txtFileNo.ClientID %>").autocomplete({
    source: function(request, response) {
    $.ajax({
    type: "POST",
    contentType: "application/json; charset=utf-8",
    url: "FileCreationModify.aspx/GetAutoCompleteData",
    data: "{'fileno':'" + $("#<%=txtFileNo.ClientID %>").val() + "'}",
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
function Verify()
{
    var r= confirm('Are you sure; Do you want to insert this record?');
    if (r==true)
    {
        if (document.getElementById('<%=txtFileNo.ClientID%>').value== "")
        {
            alert('File No. can not be empty');
            return false;
        }
        if (document.getElementById('<%=txtTitle.ClientID%>').value == "")
        {
            alert('Title can not be empty');
            return false;
        }
        if (document.getElementById('<%=ddlIPR.ClientID%>').selectedIndex== 0 || document.getElementById('<%=ddlIPR.ClientID%>').selectedIndex== -1)
        {
            alert('IPR Category can not be empty');
            return false;
        }
        if ((document.getElementById('<%=ddlType1.ClientID%>').selectedIndex== 0 || document.getElementById('<%=ddlType1.ClientID%>').selectedIndex== -1) && document.getElementById('<%=hfType.ClientID%>').value !="")
        {
            alert('Select First Inventor Type');
            return false;
        }
        
        if (document.getElementById('<%=ddlType1.ClientID%>').selectedIndex != 4 && document.getElementById('<%=ddlType1.ClientID%>').selectedIndex != 0 && document.getElementById('<%=ddlType1.ClientID%>').selectedIndex != -1)
        {
            if (document.getElementById('<%=ddlDept1.ClientID%>').selectedIndex== 0 || document.getElementById('<%=ddlDept1.ClientID%>').selectedIndex== -1)
            {
                alert('Select First Inventor Department');
                return false;
            }
            if (document.getElementById('<%=ddlType1.ClientID%>').selectedIndex== 1)
            {
                if (document.getElementById('<%=ddlCoor1.ClientID%>').selectedIndex== 0 || document.getElementById('<%=ddlCoor1.ClientID%>').selectedIndex== -1)
                {
                    alert('Select First Inventor Name');
                    return false;
                }
            }
            else  if (document.getElementById('<%=ddlType1.ClientID%>').selectedIndex== 2 || document.getElementById('<%=ddlType1.ClientID%>').selectedIndex== 3)
            {
                if (document.getElementById('<%=txtCoor1.ClientID%>').value== "")
                {
                    alert('Enter First Inventor Name');
                    return false;
                }
            }
        }
        else
        {
            if (document.getElementById('<%=txtDept1.ClientID%>').value == "")
            {
                alert('Department/Organization Name can not be empty');
                return false;
            }
            if (document.getElementById('<%=txtCoor1.ClientID%>').value == "")
            {
                alert('First Inventor Name can not be empty');
                return false;
            }            
        }
        
        return true;
    }
    else
    {
        return false;
    }
}
function DeleteVerify()
{
   var r= confirm('Are you sure; Do you want to delete this record?');
   if (r==true)
   {
        return true;
   }
   else
   {
        return false;
   }
} 
</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
<table align="center" class="neTable"  cellpadding="12px" width="970px" cellspacing="10px">
        <asp:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server">
        </asp:ToolkitScriptManager>
    <tr><td colspan="4" align="center" valign="middle" style="font-size:medium; text-decoration:underline;">
        PATENT FILE MODIFICATION</td></tr>
    <tr> 
        <td>File Number</td>
        <td> <asp:TextBox ID="txtFileNo" Width="80px" runat="server"></asp:TextBox>
        <asp:Button ID="btnFind" runat="server" Text="Find" BorderColor="Brown" 
            BorderStyle="Outset" BackColor="SandyBrown" onclick="btnFind_Click"/>
        </td>
        <td>Title</td>
        <td><asp:TextBox ID="txtTitle" runat="server" TextMode="MultiLine" Width="240px"></asp:TextBox></td></tr>
      <tr> 
        <td>IPR Category</td> 
        <td>
            <asp:DropDownList ID="ddlIPR" runat="server">
            </asp:DropDownList>
        </td>
        <td>Initial Filing</td>
        <td>
            <asp:DropDownList ID="ddlIFiling" runat="server">
            </asp:DropDownList> 
          </td></tr>
      <tr> 
        <td>First Applicant</td>
        <td>
            <asp:TextBox ID="txtFApp" Width="250px" runat="server"></asp:TextBox>
        </td>
        <td>Second Applicant</td>
        <td><asp:TextBox ID="txtSApp" Width="250px" runat="server"></asp:TextBox></td></tr>
      <tr>
      <td>First Inventor Type.</td> 
      <td><asp:DropDownList ID="ddlType1" runat="server" AutoPostBack="true" 
              onselectedindexchanged="ddlType1_SelectedIndexChanged">
            </asp:DropDownList>
          <asp:HiddenField ID="hfType" runat="server" />
      </td>
      <td id="tdFirstInvent" runat="server">First Inventor Dept.</td><td><asp:DropDownList ID="ddlDept1" Width="70px" 
              runat="server" onselectedindexchanged="ddlDept1_SelectedIndexChanged" AutoPostBack="true">
            </asp:DropDownList>
            <asp:TextBox ID="txtDept1" Width="175px" runat="server"></asp:TextBox> </td></tr>
        <tr>
        <td>First Inventor Name</td>
        <td>
            <asp:DropDownList ID="ddlCoor1" Width="250px" runat="server" 
                onselectedindexchanged="ddlCoor1_SelectedIndexChanged" AutoPostBack="true" >     
            </asp:DropDownList>
            <asp:TextBox ID="txtCoor1" Width="250px" Visible="false" runat="server"></asp:TextBox> 
            <asp:TextBox ID="txtCoorID" Width="75px" runat="server"></asp:TextBox> </td>
        <td>Date of Request</td>
    <td>
    <asp:TextBox ID="txtRequest" runat="server"></asp:TextBox>&nbsp;<asp:Image ID="imgRequest" ImageUrl="~/Images/RedCalendar.gif" runat="server" />
    <asp:CalendarExtender ID="CalendarExtender1" TargetControlID="txtRequest" PopupButtonID="imgRequest" runat="server" Format="dd/MM/yyyy">
    </asp:CalendarExtender>
    </td></tr>    
<tr><td colspan="4">
<div>List of Co Inventors</div><br />
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
<asp:ListView ID="lvInventor" runat="server" InsertItemPosition="LastItem" 
                                    onitemcreated="lvInventor_ItemCreated" 
                                    oniteminserting="lvInventor_ItemInserting" 
            onitemcanceling="lvInventor_ItemCanceling" 
            onitemdeleting="lvInventor_ItemDeleting" 
        onitemdatabound="lvInventor_ItemDataBound" 
        onitemediting="lvInventor_ItemEditing" 
        onitemupdating="lvInventor_ItemUpdating" >
<LayoutTemplate>
<table id="Table1" width="950px" align="center" runat="server"  style="text-align:left; border-color:#DEDFDE; border-style:solid; border-width:1px;" cellpadding="5" cellspacing="0" border="1">
    <tr id="Tr1" runat="server" style="background-color:#f9cfaa">
    <th id="Th1" align="center" runat="server">Sl.No.</th>
    <th id="Th2" align="center" runat="server">Inventor Type</th>
    <th id="Th6" align="center" runat="server">Department Code</th>
    <th id="Th3" align="center" runat="server">Department/Organization</th>
    <th id="Th4" align="center" runat="server">Inventor ID</th>
    <th id="Th7" align="center" runat="server">Inventor Name</th>
    <th id="Th5" runat="server"></th>
    </tr>
    <tr runat="server" id="itemPlaceholder" />
    </table>
</LayoutTemplate>
<ItemTemplate>
    <tr id="Tr2" runat="server" style="background-color:White">
    <td><asp:Label ID="lblSlNo" Text='<%#Eval("SlNo")%>' runat="server"></asp:Label></td>
    <td><asp:Label ID="lblType" runat="server" BackColor="Transparent" BorderStyle="None" Width="50px" Text='<%#Eval("InventorType")%>'></asp:Label></td>
    <td><asp:Label ID="lblDeptCode" runat="server" BackColor="Transparent" BorderStyle="None" Width="70px" Text='<%#Eval("DeptCode")%>'></asp:Label></td>
    <td><asp:Label ID="lblDept" runat="server" BackColor="Transparent" BorderStyle="None" Width="200px" Text='<%#Eval("DeptOrgName")%>'></asp:Label></td>
    <td><asp:Label ID="lblID" runat="server" BackColor="Transparent" BorderStyle="None" Width="90px" Text='<%#Eval("InventorID")%>'></asp:Label></td>
    <td><asp:Label ID="lblName" runat="server" BackColor="Transparent" BorderStyle="None" Width="200px" Text='<%#Eval("InventorName")%>'></asp:Label></td>
    <td>
    <asp:LinkButton ID="lbtnEdit" Font-Underline="true" CommandName="edit" runat="server">Edit</asp:LinkButton>
    <asp:LinkButton ID="lbtnDelete" Font-Underline="true" OnClientClick="return DeleteVerify()" Visible="false" CommandName="delete" runat="server">Delete</asp:LinkButton>
    </td>
    </tr>
</ItemTemplate>
<AlternatingItemTemplate>
    <tr id="Tr3" runat="server" style="background-color:#F7F7DE">
    <td><asp:Label ID="lblSlNo" Text='<%#Eval("SlNo")%>' runat="server"></asp:Label></td>
    <td><asp:Label ID="lblType" runat="server" BackColor="Transparent"  BorderStyle="None" Width="50px" Text='<%#Eval("InventorType")%>'></asp:Label></td>
    <td><asp:Label ID="lblDeptCode" runat="server" BackColor="Transparent" BorderStyle="None" Width="70px" Text='<%#Eval("DeptCode")%>'></asp:Label></td>
    <td><asp:Label ID="lblDept" runat="server" BackColor="Transparent" BorderStyle="None" Width="200px"  Text='<%#Eval("DeptOrgName")%>'></asp:Label></td>
    <td><asp:Label ID="lblID" runat="server" BackColor="Transparent" BorderStyle="None" Width="90px" Text='<%#Eval("InventorID")%>'></asp:Label></td>
    <td><asp:Label ID="lblName" runat="server" BackColor="Transparent" BorderStyle="None" Width="200px" Text='<%#Eval("InventorName")%>'></asp:Label></td>
    <td>
    <asp:LinkButton ID="lbtnEdit" Font-Underline="true" CommandName="edit" runat="server">Edit</asp:LinkButton>
    <asp:LinkButton ID="lbtnDelete" Font-Underline="true" OnClientClick="return DeleteVerify()" Visible="false" CommandName="delete" runat="server">Delete</asp:LinkButton>
    </td>
    </tr>
</AlternatingItemTemplate>
<EditItemTemplate>
<tr id="Tr5" runat="server">
    <td><asp:Label ID="lblEditSlNo" Text='<%#Eval("SlNo")%>'  runat="server"></asp:Label></td>
    <td><asp:DropDownList ID="ddlEditType" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlEditType_SelectedIndexChanged"></asp:DropDownList>
    <asp:Label ID="lblEditType" runat="server" Text='<%#Eval("InventorType")%>' Visible="false"></asp:Label></td>
    <td><asp:DropDownList ID="ddlEditDeptCode" Width="60px" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlEditDept_SelectedIndexChanged"></asp:DropDownList>
    <asp:Label ID="lblEditDeptCode" runat="server" Text='<%#Eval("DeptCode")%>' Visible="false"></asp:Label></td>
    <td><asp:TextBox ID="txtEditDept" Width="250px" TextMode="MultiLine" Wrap="true" runat="server" Text='<%#Eval("DeptOrgName")%>'></asp:TextBox></td>
    <td><asp:TextBox ID="txtEditID" Width="75px" runat="server" Text='<%#Eval("InventorID")%>'></asp:TextBox></td>
    <td><asp:DropDownList ID="ddlEditName" Width="250px" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlEditCoor_SelectedIndexChanged"></asp:DropDownList>
    <asp:TextBox ID="txtEditName" Visible="false" Width="250px" TextMode="MultiLine" Wrap="true" runat="server" Text='<%#Eval("InventorName")%>'></asp:TextBox></td>
    <td><asp:LinkButton ID="lbtnUpdate" Font-Underline="true" CommandName="update" runat="server">Update</asp:LinkButton>
    <asp:LinkButton ID="lbtnCancel" Font-Underline="true" CommandName="cancel" runat="server">Cancel</asp:LinkButton>
    </td>
    </tr>
</EditItemTemplate>
<InsertItemTemplate>
<tr id="Tr4" runat="server">
    <td><asp:Label ID="lblNewSlNo" runat="server"></asp:Label></td>
    <td><asp:DropDownList ID="ddlNewType" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlType_SelectedIndexChanged"></asp:DropDownList></td>
    <td><asp:DropDownList ID="ddlNewDeptCode" Width="60px" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged"></asp:DropDownList></td>
    <td><asp:TextBox ID="txtDept" Width="250px" TextMode="MultiLine" Wrap="true" runat="server"></asp:TextBox></td>
    <td><asp:TextBox ID="txtID" Width="75px" runat="server"></asp:TextBox></td>
    <td><asp:DropDownList ID="ddlNewName" Width="250px" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlCoor_SelectedIndexChanged"></asp:DropDownList>
    <asp:TextBox ID="txtName" Visible="false" Width="250px" TextMode="MultiLine" Wrap="true" runat="server"></asp:TextBox></td>
    <td><asp:LinkButton ID="lbtnInsert" Font-Underline="true" CommandName="insert" runat="server">Insert</asp:LinkButton>
    <asp:LinkButton ID="lbtnCancel" Font-Underline="true" CommandName="cancel" runat="server">Cancel</asp:LinkButton>
    </td>
    </tr>
</InsertItemTemplate>
</asp:ListView>
</ContentTemplate></asp:UpdatePanel>
</td></tr>
<tr>
<td colspan="4">Source of Invention from Research Projects&nbsp;
<asp:DropDownList ID="ddlSource" Width="50px" runat="server" AutoPostBack="true" 
        onselectedindexchanged="ddlSource_SelectedIndexChanged" ></asp:DropDownList>&nbsp;&nbsp;Direct Strategy&nbsp; <asp:DropDownList ID="dropstrategy" runat="server">
        <asp:ListItem>Yes</asp:ListItem>
        <asp:ListItem>No</asp:ListItem>
    </asp:DropDownList>
&nbsp; Application Post Dated&nbsp;&nbsp;<asp:DropDownList ID="DropPost" runat="server"><asp:ListItem>Yes</asp:ListItem>
        <asp:ListItem>No</asp:ListItem></asp:DropDownList>
   &nbsp; Post Dated Date <asp:TextBox ID="txtPost" runat="server"></asp:TextBox>&nbsp;<asp:Image ID="imagecal" ImageUrl="~/Images/RedCalendar.gif" runat="server" /><asp:CalendarExtender ID="CalendarExtender2" TargetControlID="txtPost" PopupButtonID="imagecal" runat="server" Format="dd/MM/yyyy">
    </asp:CalendarExtender></td></tr>
<%--<td colspan="2">Source of Invention from Research Projects&nbsp;&nbsp;&nbsp;&nbsp;
<asp:DropDownList ID="ddlSource" Width="50px" runat="server" AutoPostBack="true" 
        onselectedindexchanged="ddlSource_SelectedIndexChanged" ></asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;Direct Strategy&nbsp;&nbsp;&nbsp;&nbsp;<asp:DropDownList ID="dropstrategy" runat="server">
        <asp:ListItem>Yes</asp:ListItem>
        <asp:ListItem>No</asp:ListItem>
    </asp:DropDownList></td>
</tr>

<tr><td>Application Post Dated&nbsp;&nbsp;&nbsp;<asp:DropDownList ID="DropPost" runat="server"><asp:ListItem>Yes</asp:ListItem>
        <asp:ListItem>No</asp:ListItem></asp:DropDownList></td>
        <td>Post Dated Date <asp:TextBox ID="txtPost" runat="server"></asp:TextBox>&nbsp;<asp:Image ID="imagecal" ImageUrl="~/Images/RedCalendar.gif" runat="server" /><asp:CalendarExtender ID="CalendarExtender2" TargetControlID="txtRequest" PopupButtonID="imgRequest" runat="server" Format="dd/MM/yyyy">
    </asp:CalendarExtender></td>--%>
    
<tr id="ViewProjectList" visible="false" runat="server"><td colspan="4">
<div style="text-decoration:underline;">Research Projects List</div><br />
<asp:UpdatePanel ID="UpdatePanel3" runat="server">
<ContentTemplate>
<asp:ListView ID="lvProjectList" runat="server" InsertItemPosition="LastItem" 
        onitemcanceling="lvProjectList_ItemCanceling" 
        onitemcreated="lvProjectList_ItemCreated" 
        onitemdatabound="lvProjectList_ItemDataBound" 
        onitemdeleting="lvProjectList_ItemDeleting" 
        oniteminserting="lvProjectList_ItemInserting" 
        onitemupdating="lvProjectList_ItemUpdating" 
        onitemediting="lvProjectList_ItemEditing">
<LayoutTemplate>
<table id="Table1" width="950px" align="center" runat="server"  style="text-align:left;border-color:#DEDFDE; border-style:solid; border-width:1px;" cellpadding="5" cellspacing="0" border="1">
    <tr id="Tr1" runat="server" style="background-color:#f9cfaa;padding-top:0px;padding-bottom:0px;height:25px;">
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
    <tr id="Tr2" runat="server" style="background-color:White">
    <td><asp:Label ID="lblProjectSlNo" Text='<%#Eval("SlNo")%>' runat="server"></asp:Label></td>
    <td><asp:Label ID="lblProjectType" runat="server" BackColor="Transparent" BorderStyle="None" Text='<%#Eval("ProjectType")%>'></asp:Label></td>
    <td><asp:Label ID="lblFinYear" runat="server" BackColor="Transparent" BorderStyle="None" Text='<%#Eval("FinancialYear")%>'></asp:Label></td>
    <td><asp:Label ID="lblProjectNo" runat="server" BackColor="Transparent" BorderStyle="None" Text='<%#Eval("ProjectNumber")%>'></asp:Label></td>
    <td><asp:Label ID="lblFundAgency" runat="server" BackColor="Transparent" BorderStyle="None" Width="175px" Text='<%#Eval("FundingAgency")%>'></asp:Label></td>    
    <td><asp:Label ID="lblTitle" runat="server" BackColor="Transparent" BorderStyle="None" Width="250px" Text='<%#Eval("ProjectTitle")%>'></asp:Label></td>
    <td>
    <asp:LinkButton ID="lbtnEdit" Font-Underline="true" CommandName="edit" runat="server">Edit</asp:LinkButton>
    <asp:LinkButton ID="lbtnDelete" Font-Underline="true" CommandName="delete" runat="server">Delete</asp:LinkButton>
    </td>
    </tr>
</ItemTemplate>
<AlternatingItemTemplate>
    <tr id="Tr3" runat="server" style="background-color:#F7F7DE">
    <td><asp:Label ID="lblProjectSlNo" Text='<%#Eval("SlNo")%>' runat="server"></asp:Label></td>
    <td><asp:Label ID="lblProjectType" runat="server" BackColor="Transparent" BorderStyle="None"  Text='<%#Eval("ProjectType")%>'></asp:Label></td>
    <td><asp:Label ID="lblFinYear" runat="server" BackColor="Transparent" BorderStyle="None"  Text='<%#Eval("FinancialYear")%>'></asp:Label></td>
    <td><asp:Label ID="lblProjectNo" runat="server" BackColor="Transparent" BorderStyle="None" Text='<%#Eval("ProjectNumber")%>'></asp:Label></td>
    <td><asp:Label ID="lblFundAgency" runat="server" BackColor="Transparent" BorderStyle="None" Width="175px"  Text='<%#Eval("FundingAgency")%>'></asp:Label></td>
    <td><asp:Label ID="lblTitle" runat="server" BackColor="Transparent" BorderStyle="None" Width="250px" Text='<%#Eval("ProjectTitle")%>'></asp:Label></td>
    <td>
    <asp:LinkButton ID="lbtnEdit" Font-Underline="true" CommandName="edit" runat="server">Edit</asp:LinkButton>
    <asp:LinkButton ID="lbtnDelete" Font-Underline="true" CommandName="delete" runat="server">Delete</asp:LinkButton>
    </td>
    </tr>
</AlternatingItemTemplate>
<InsertItemTemplate>
<tr id="Tr4" runat="server">
    <td><asp:Label ID="lblProjectSlNo" runat="server"></asp:Label></td>
    <td><asp:DropDownList ID="ddlNewProjectType" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlNewProjectType_SelectedIndexChanged"></asp:DropDownList></td>
    <td><asp:DropDownList ID="ddlFinYear" Width="90px" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlFinYear_SelectedIndexChanged"></asp:DropDownList></td>
    <td><asp:DropDownList ID="ddlNewProjectNo" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlNewProjectNo_SelectedIndexChanged" ></asp:DropDownList></td>
    <td><asp:TextBox ID="txtAgency" Width="175px" TextMode="MultiLine" Wrap="true"  runat="server"></asp:TextBox></td>
    <td><asp:TextBox ID="txtProjectTitle" Width="220px" TextMode="MultiLine" runat="server"></asp:TextBox></td>
    <td><asp:LinkButton ID="lbtnInsert" Font-Underline="true" CommandName="insert" runat="server">Insert</asp:LinkButton>
    <asp:LinkButton ID="lbtnCancel" Font-Underline="true" CommandName="cancel" runat="server">Cancel</asp:LinkButton>
    </td>
    </tr>
</InsertItemTemplate>
<EditItemTemplate>
<tr id="Tr4" runat="server">
    <td><asp:Label ID="lblEditProjectSlNo" Text='<%#Eval("SlNo")%>' runat="server"></asp:Label></td>
    <td><asp:DropDownList ID="ddlEditProjectType" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlEditProjectType_SelectedIndexChanged"></asp:DropDownList>
    <asp:HiddenField ID="hfProjectType" Value='<%#Eval("ProjectType")%>' runat="server" /></td>    
    <td><asp:DropDownList ID="ddlEditFinYear" Width="90px" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlEditFinYear_SelectedIndexChanged"></asp:DropDownList>
    <asp:HiddenField ID="hfFinYear" Value='<%#Eval("FinancialYear")%>'  runat="server" /></td>    
    <td><asp:DropDownList ID="ddlEditProjectNo" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlEditProjectNo_SelectedIndexChanged" ></asp:DropDownList>
    <asp:HiddenField ID="hfProjectNo" Value='<%#Eval("ProjectNumber")%>' runat="server" /></td>
    <td><asp:TextBox ID="txtEditAgency" Width="175px"  Text='<%#Eval("FundingAgency")%>' TextMode="MultiLine" Wrap="true"  runat="server"></asp:TextBox></td>
    <td><asp:TextBox ID="txtEditProjectTitle" Width="220px" Text='<%#Eval("ProjectTitle")%>'  TextMode="MultiLine" runat="server"></asp:TextBox></td>
    <td><asp:LinkButton ID="lbtnUpdate" Font-Underline="true" CommandName="Update" runat="server">Update</asp:LinkButton>
    <asp:LinkButton ID="lbtnCancel" Font-Underline="true" CommandName="cancel" runat="server">Cancel</asp:LinkButton>
    </td>
    </tr>
</EditItemTemplate>
</asp:ListView>
</ContentTemplate>
</asp:UpdatePanel>
</td></tr>
<tr>
<td>Write Up</td><td><asp:TextBox ID="txtWriteUp" runat="server" TextMode="MultiLine" Width="275px" onkeydown="return(event.keyCode !=13);" ></asp:TextBox></td>
<td>Remarks</td><td><asp:TextBox ID="txtComments" runat="server" TextMode="MultiLine" Width="275px" ></asp:TextBox></td>
</tr>
<tr><td>Write Up for Tech Transfer</td><td><asp:TextBox ID="txtAbstract" runat="server" TextMode="MultiLine" Width="275px"></asp:TextBox></td>
        <td>
            <%--<asp:FileUpload ID="FileUpload1" runat="server" />
       <asp:Label Text="Image size should not exceed 30MB" ID="lblupload" Visible="false" runat="server"></asp:Label>
        </td>--%><td></td></tr>
       


<tr>
<td colspan="4" align="center">
  <asp:ImageButton ID="imgBtnInsert" ImageUrl="~/Images/Save3.png" runat="server" 
      OnClientClick="return Verify()" onclick="imgBtnInsert_Click" />&nbsp;
  <asp:ImageButton ID="imgBtnClear" runat="server" 
      ImageUrl="~/Images/Clear3.png" onclick="imgBtnClear_Click" />         
</td>
</tr>
</table>
</asp:Content>
