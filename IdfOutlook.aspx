<%@ Page Language="C#" MasterPageFile="~/Parent.master" AutoEventWireup="true" CodeFile="IdfOutlook.aspx.cs" Inherits="IdfOutlook" Title="IDF Overview" %>
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
});
function Verify()
{
    if (document.getElementById('<%=ddlFileNo.ClientID%>').value== "")
    {
        alert('File No. can not be empty');
        return false;
    }
    return true;
}
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <table align="center" class="neTable" cellpadding="12px" style="width:980px" cellspacing="10px">
    <tr><td id="lbltitle" runat="server" align="center" valign="middle" style="font-size:medium; width:95%; text-decoration:underline;">
        IDF Overview </td><td style="width:5%;"><asp:ImageButton ID="ibtnPrinter" 
                                    ImageUrl="~/Images/Printer.gif" runat="server" OnClientClick="return Verify()" onclick="ibtnPrinter_Click" 
                                    style="height: 24px"/></td></tr>
    <tr><td colspan="2" align="center">File Number &nbsp;&nbsp;&nbsp;<asp:DropDownList ID="ddlFileNo" runat="server" AutoPostBack="true" EnableViewState="true"  
                onselectedindexchanged="ddlFileNo_SelectedIndexChanged">
            </asp:DropDownList>
        </td></tr>
    <tr><td colspan="2" >
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <asp:LinkButton ID="lbtnIDF" runat="server" onclick="lbtnIDF_Click">IDF Details</asp:LinkButton>
       <div align="center">
        <asp:DetailsView ID="dvIDF" runat="server" AutoGenerateRows="False" Width="800px" 
                CellPadding="4" ForeColor="Black" GridLines="Both" BackColor="White" 
                BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px">
            <RowStyle BackColor="#F7F7DE"/>
        <Fields>
        <asp:BoundField HeaderStyle-Width="170px" HeaderText="Title" 
                ItemStyle-HorizontalAlign="Left" DataField="title" ReadOnly="true">
            </asp:BoundField>
        <asp:BoundField HeaderText="IDF Type" ItemStyle-HorizontalAlign="Left" 
                DataField="type" ReadOnly="true">
            </asp:BoundField>
        <asp:BoundField HeaderText="Initial Filing" ItemStyle-HorizontalAlign="Left" 
                DataField="InitialFiling" ReadOnly="true">
            </asp:BoundField>
        <asp:BoundField HeaderText="First Applicant" ItemStyle-HorizontalAlign="Left" 
                DataField="firstApplicant" ReadOnly="true">
            </asp:BoundField>
        <asp:BoundField HeaderText="Second Applicant" ItemStyle-HorizontalAlign="Left" 
                DataField="secondApplicant" ReadOnly="true">
            </asp:BoundField>
        <asp:BoundField HeaderText="Request Date" ItemStyle-HorizontalAlign="Left" 
                DataField="request_dt" DataFormatString="{0:dd/MM/yyyy}" ReadOnly="true">        
            </asp:BoundField>
         <asp:BoundField HeaderText="Write Up" ItemStyle-HorizontalAlign="Left" 
                DataField="Specification" ReadOnly="true">        
            </asp:BoundField>
        </Fields>
            <AlternatingRowStyle BackColor="White" />
        </asp:DetailsView>
        </div>       
    </ContentTemplate>
    </asp:UpdatePanel>
    </td></tr>
    <tr style="background-color:#f2d7d7"><td colspan="2">
        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
        <ContentTemplate> 
        <asp:LinkButton ID="lbtnInventor" runat="server" onclick="lbtnInventor_Click">Inventor 
            Details</asp:LinkButton>
        <div align="center">
            <asp:ListView ID="lvInventor" runat="server" >
            <LayoutTemplate>
            <table id="Table1" width="800px" align="center" runat="server"  style="text-align:left;Border-Color:#DEDFDE; Border-Style:None; Border-Width:1px;" cellpadding="5" cellspacing="0" border="1" >
            <tr id="Tr1" runat="server" style="background-color:Teal;" >
            <th id="Th1" align="center" runat="server">Sl. No.</th>
            <th id="Th2" align="center" runat="server">Inventor Name</th>
            <th id="Th3" align="center" runat="server">Inventor Type</th>
            <th id="Th9" align="center" runat="server">Inventor ID</th>
            <th id="Th4" align="center" runat="server">Department / Organization</th>
            </tr>
            <tr runat="server" id="itemPlaceholder" />
            </table>
            </LayoutTemplate>
            <ItemTemplate>            
            <tr id="Tr2" style="background-color:#F7F7DE;" runat="server">
                <td><asp:Label ID="lblSlNo" BackColor="Transparent" BorderStyle="None" Text='<%#Eval("SlNo")%>' runat="server"></asp:Label></td>
                <td><asp:Label ID="lblName" BackColor="Transparent" BorderStyle="None" Text='<%#Eval("InventorName")%>' runat="server"></asp:Label></td>
                <td><asp:Label ID="Label1" BackColor="Transparent" BorderStyle="None" Text='<%#Eval("InventorType")%>' runat="server"></asp:Label></td>
                <td><asp:Label ID="Label3" BackColor="Transparent" BorderStyle="None" Text='<%#Eval("InventorID")%>' runat="server"></asp:Label></td>
                <td><asp:Label ID="Label2" BackColor="Transparent" BorderStyle="None" Text='<%#Eval("Dept")%>' runat="server"></asp:Label></td>
            </tr>
            </ItemTemplate>
            <AlternatingItemTemplate>
            <tr id="Tr3" style="background-color:#ffffff;" runat="server">
                <td><asp:Label ID="lblSlNo" BackColor="Transparent" BorderStyle="None" Text='<%#Eval("SlNo")%>' runat="server"></asp:Label></td>
                <td><asp:Label ID="lblName" BackColor="Transparent" BorderStyle="None" Text='<%#Eval("InventorName")%>' runat="server"></asp:Label></td>
                <td><asp:Label ID="Label1" BackColor="Transparent" BorderStyle="None" Text='<%#Eval("InventorType")%>' runat="server"></asp:Label></td>
                <td><asp:Label ID="Label3" BackColor="Transparent" BorderStyle="None" Text='<%#Eval("InventorID")%>' runat="server"></asp:Label></td>
                <td><asp:Label ID="Label2" BackColor="Transparent" BorderStyle="None" Text='<%#Eval("Dept")%>' runat="server"></asp:Label></td>
            </tr>
            </AlternatingItemTemplate>
            </asp:ListView>
            </div>
        </ContentTemplate>
        </asp:UpdatePanel>
    </td></tr>
    <tr><td colspan="2">
        <asp:UpdatePanel ID="UpdatePanel7" runat="server">
        <ContentTemplate> 
        <asp:LinkButton ID="lbtnSource" runat="server" onclick="lbtnSource_Click">Source of Invention</asp:LinkButton>
        <div align="center">
            <asp:ListView ID="LvInventionSource" runat="server" >
            <LayoutTemplate>
            <table id="Table1" width="800px" align="center" runat="server"  style="text-align:left;Border-Color:#DEDFDE; Border-Style:None; Border-Width:1px;" cellpadding="5" cellspacing="0" border="1" >
            <tr id="Tr1" runat="server" style="background-color:Teal;" >
            <th id="Th1" align="center" runat="server">Sl. No.</th>
            <th id="Th2" align="center" runat="server">Project No</th>
            <th id="Th3" align="center" runat="server">Project Type</th>
            <th id="Th9" align="center" runat="server">Funding Agency</th>
            <th id="Th4" align="center" runat="server">Project Title</th>
            </tr>
            <tr runat="server" id="itemPlaceholder" />
            </table>
            </LayoutTemplate>
            <ItemTemplate>            
            <tr id="Tr2" style="background-color:#F7F7DE;" runat="server">
                <td><asp:Label ID="lblSlNo" BackColor="Transparent" BorderStyle="None" Text='<%#Eval("SlNo")%>' runat="server"></asp:Label></td>
                <td><asp:Label ID="lblName" BackColor="Transparent" BorderStyle="None" Text='<%#Eval("ProjectNumber")%>' runat="server"></asp:Label></td>
                <td><asp:Label ID="Label1" BackColor="Transparent" BorderStyle="None" Text='<%#Eval("ProjectType")%>' runat="server"></asp:Label></td>
                <td><asp:Label ID="Label3" BackColor="Transparent" BorderStyle="None" Text='<%#Eval("FundingAgency")%>' runat="server"></asp:Label></td>
                <td><asp:Label ID="Label2" BackColor="Transparent" BorderStyle="None" Text='<%#Eval("ProjectTitle")%>' runat="server"></asp:Label></td>
            </tr>
            </ItemTemplate>
            <AlternatingItemTemplate>
            <tr id="Tr3" style="background-color:#ffffff;" runat="server">
                <td><asp:Label ID="lblSlNo" BackColor="Transparent" BorderStyle="None" Text='<%#Eval("SlNo")%>' runat="server"></asp:Label></td>
                <td><asp:Label ID="lblName" BackColor="Transparent" BorderStyle="None" Text='<%#Eval("ProjectNumber")%>' runat="server"></asp:Label></td>
                <td><asp:Label ID="Label1" BackColor="Transparent" BorderStyle="None" Text='<%#Eval("ProjectType")%>' runat="server"></asp:Label></td>
                <td><asp:Label ID="Label3" BackColor="Transparent" BorderStyle="None" Text='<%#Eval("FundingAgency")%>' runat="server"></asp:Label></td>
                <td><asp:Label ID="Label2" BackColor="Transparent" BorderStyle="None" Text='<%#Eval("ProjectTitle")%>' runat="server"></asp:Label></td>
            </tr>
            </AlternatingItemTemplate>
            </asp:ListView>
            </div>
        </ContentTemplate>
        </asp:UpdatePanel>
    </td></tr>
    <tr style="background-color:#f2d7d7"><td colspan="2">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
        <asp:LinkButton ID="lbtnIndian" runat="server" onclick="lbtnIndian_Click">Indian 
            Patent Status</asp:LinkButton>
        <div align="center">
        <asp:DetailsView ID="dvIndian" runat="server" AutoGenerateRows="False" Width="800px" 
                CellPadding="4" ForeColor="Black" GridLines="Both" BackColor="White" 
                BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px">
            <RowStyle BackColor="#F7F7DE"/>
        <Fields>
            <asp:BoundField HeaderStyle-Width="170px" HeaderText="Attorney" 
                ItemStyle-HorizontalAlign="Left" DataField="attorney" ReadOnly="true">
            </asp:BoundField>
            <asp:BoundField HeaderText="Application Number" 
                ItemStyle-HorizontalAlign="Left" DataField="applcn_no" ReadOnly="true">
            </asp:BoundField>
            <asp:BoundField HeaderText="Filing Date" 
                ItemStyle-HorizontalAlign="Left" DataField="filing_dt" DataFormatString="{0:dd/MM/yyyy}" ReadOnly="true">
            </asp:BoundField>
            <asp:BoundField HeaderText="Examination" 
                ItemStyle-HorizontalAlign="Left" DataField="examination" ReadOnly="true">
            </asp:BoundField>
            <asp:BoundField HeaderText="Examination Request Date" 
                ItemStyle-HorizontalAlign="Left" DataField="exam_dt" DataFormatString="{0:dd/MM/yyyy}" ReadOnly="true">
            </asp:BoundField>
            <asp:BoundField HeaderText="Publication" 
                ItemStyle-HorizontalAlign="Left" DataField="publication" ReadOnly="true">
            </asp:BoundField>
            <asp:BoundField HeaderText="Publication Date" 
                ItemStyle-HorizontalAlign="Left" DataField="pub_dt" DataFormatString="{0:dd/MM/yyyy}" ReadOnly="true">
            </asp:BoundField>
            <asp:BoundField HeaderText="Patent Status" 
                ItemStyle-HorizontalAlign="Left" DataField="status" ReadOnly="true">
            </asp:BoundField>
            <asp:BoundField HeaderText="Patent Sub Status" 
                ItemStyle-HorizontalAlign="Left" DataField="sub_status" ReadOnly="true">
            </asp:BoundField>
            <asp:BoundField HeaderText="Patent Grant Number" 
                ItemStyle-HorizontalAlign="Left" DataField="pat_no" ReadOnly="true">
            </asp:BoundField>
            <asp:BoundField HeaderText="Patent Issue Date" 
                ItemStyle-HorizontalAlign="Left" DataField="pat_dt" DataFormatString="{0:dd/MM/yyyy}%"   ReadOnly="true">
            </asp:BoundField>            
        </Fields>
         <AlternatingRowStyle BackColor="White" />
        </asp:DetailsView></div>
        </ContentTemplate>
        </asp:UpdatePanel>
    </td></tr>
    <tr><td colspan="2">
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
        <asp:LinkButton ID="lbtnCommercial" runat="server" 
            onclick="lbtnCommercial_Click1">Commercialization</asp:LinkButton>     
        <div align="center">
            <asp:DetailsView ID="dvCommercial" runat="server" AutoGenerateRows="False" Width="800px" 
                CellPadding="4" ForeColor="Black" GridLines="Both" BackColor="White" 
                BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px">
            <RowStyle BackColor="#F7F7DE"/>
        <Fields>
            <asp:BoundField HeaderStyle-Width="170px" HeaderText="Commercialization Responsibility" 
                ItemStyle-HorizontalAlign="Left" DataField="commercial" ReadOnly="true">
            </asp:BoundField>
            <asp:BoundField HeaderText="Partner Reference No." 
                ItemStyle-HorizontalAlign="Left" DataField="InventionNo" ReadOnly="true">
            </asp:BoundField>
            <asp:BoundField HeaderText="Validity From Date" 
                ItemStyle-HorizontalAlign="Left" DataField="Validity_from_dt" DataFormatString="{0:dd/MM/yyyy}" ReadOnly="true">
            </asp:BoundField>
            <asp:BoundField HeaderText="Validity Till Date" 
                ItemStyle-HorizontalAlign="Left" DataField="Validity_to_dt" DataFormatString="{0:dd/MM/yyyy}" ReadOnly="true">
            </asp:BoundField> 
            <asp:BoundField HeaderText="Industry" 
                ItemStyle-HorizontalAlign="Left" DataField="Industry1" ReadOnly="true">
            </asp:BoundField> 
            <asp:BoundField HeaderText="Industry 2" 
                ItemStyle-HorizontalAlign="Left" DataField="Industry2" ReadOnly="true">
            </asp:BoundField> 
            <asp:BoundField HeaderText="Usage Area" 
                ItemStyle-HorizontalAlign="Left" DataField="Industry3" ReadOnly="true">
            </asp:BoundField> 
            <asp:BoundField HeaderText="International Patent Classification(IPC) Code" 
                ItemStyle-HorizontalAlign="Left" DataField="IPC_Code" ReadOnly="true">
            </asp:BoundField> 
            <asp:BoundField HeaderText="Abstract (Value Proposition)" 
                ItemStyle-HorizontalAlign="Left" DataField="abstract" ReadOnly="true">
            </asp:BoundField> 
            <asp:BoundField HeaderText="Development Status" 
                ItemStyle-HorizontalAlign="Left" DataField="developmentStatus" ReadOnly="true">
            </asp:BoundField> 
            <asp:BoundField HeaderText="Commercialization Status" 
                ItemStyle-HorizontalAlign="Left" DataField="Commercialized" ReadOnly="true">
            </asp:BoundField> 
            <asp:BoundField HeaderText="Partner Agreement" 
                ItemStyle-HorizontalAlign="Left" DataField="PatentLicense" ReadOnly="true">
            </asp:BoundField> 
            <asp:BoundField HeaderText="Technology Transfer No. / Marketing No." 
                ItemStyle-HorizontalAlign="Left" DataField="TechTransNo" ReadOnly="true">
            </asp:BoundField> 
            <asp:BoundField HeaderText="Remark" 
                ItemStyle-HorizontalAlign="Left" DataField="Remarks" ReadOnly="true">
            </asp:BoundField>             
        </Fields>
        <AlternatingRowStyle BackColor="White" />
        </asp:DetailsView>
        </div>
        </ContentTemplate>
        </asp:UpdatePanel>
    </td></tr>   
    <tr style="background-color:#f2d7d7"><td colspan="2">
        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
        <ContentTemplate> 
        <asp:LinkButton ID="lbtnIntl" runat="server" onclick="lbtnIntl_Click">International 
            Patent Status</asp:LinkButton>
        <div align="center">
            <asp:ListView ID="lvIntl" runat="server" 
                onselectedindexchanging="lvIntl_SelectedIndexChanging" >
            <LayoutTemplate>
            <table id="Table1" width="800px" align="center" runat="server"  style="text-align:left;Border-Color:#DEDFDE; Border-Style:None; Border-Width:1px;" cellpadding="5" cellspacing="0" border="1" >
            <tr id="Tr1" runat="server" style="background-color:Teal;" >
            <th id="Th1" align="center" runat="server">Sub File No</th>
            <th id="Th2" align="center" runat="server">Country</th>
            <th id="Th3" align="center" runat="server">Partner</th>
            <th id="Th4" align="center" runat="server">Partner Number</th>
            <th id="Th5" align="center" runat="server">Attorney</th>
            <th id="Th6" align="center" runat="server">Status</th>
            <th id="Th7" align="center" runat="server">Sub Status</th>
            <th id="Th8" align="center" runat="server"></th>
            </tr>
            <tr runat="server" id="itemPlaceholder" />
            </table>
            </LayoutTemplate>
            <ItemTemplate>            
            <tr id="Tr2" style="background-color:#F7F7DE;" runat="server">
                <td><asp:Label ID="lblSubFileNo" BackColor="Transparent" BorderStyle="None" Text='<%#Eval("subFileNo")%>' runat="server"></asp:Label></td>
                <td><asp:Label ID="lblCountry" BackColor="Transparent" BorderStyle="None" Text='<%#Eval("country")%>' Width="50px" runat="server"></asp:Label></td>
                <td><asp:Label ID="lblpartner" BackColor="Transparent" BorderStyle="None" Width="100px"  Text='<%#Eval("partner")%>' runat="server"></asp:Label></td>
                <td><asp:Label ID="lblPartnerNo" BackColor="Transparent" BorderStyle="None" Width="50px"  Text='<%#Eval("partnerNo")%>' runat="server"></asp:Label></td>
                <td><asp:Label ID="lblAttorney" BackColor="Transparent" BorderStyle="None" Width="100px"  Text='<%#Eval("attorney")%>' runat="server"></asp:Label></td>
                <td><asp:Label ID="lblStatus" BackColor="Transparent" BorderStyle="None" Width="100px"  Text='<%#Eval("status")%>' runat="server"></asp:Label></td>
                <td><asp:Label ID="lblSubStatus" BackColor="Transparent" BorderStyle="None" Width="100px" Text='<%#Eval("substatus")%>' runat="server"></asp:Label></td>
                <td><asp:LinkButton ID="lbtnEdit" Font-Underline="true" CommandName="Select" runat="server">View</asp:LinkButton></td>
            </tr>
            </ItemTemplate>
            <AlternatingItemTemplate>
            <tr id="Tr3" style="background-color:#ffffff;" runat="server">
                <td><asp:Label ID="lblSubFileNo1" BackColor="Transparent" BorderStyle="None" Text='<%#Eval("subFileNo")%>' runat="server"></asp:Label></td>
                <td><asp:Label ID="lblCountry1" BackColor="Transparent" BorderStyle="None" Text='<%#Eval("country")%>' Width="50px" runat="server"></asp:Label></td>
                <td><asp:Label ID="lblpartner1" BackColor="Transparent" BorderStyle="None" Width="100px"  Text='<%#Eval("partner")%>' runat="server"></asp:Label></td>
                <td><asp:Label ID="lblPartnerNo1" BackColor="Transparent" BorderStyle="None" Width="50px"  Text='<%#Eval("partnerNo")%>' runat="server"></asp:Label></td>
                <td><asp:Label ID="lblAttorney1" BackColor="Transparent" BorderStyle="None" Width="100px"  Text='<%#Eval("attorney")%>' runat="server"></asp:Label></td>
                <td><asp:Label ID="lblStatus1" BackColor="Transparent" BorderStyle="None" Width="100px"  Text='<%#Eval("status")%>' runat="server"></asp:Label></td>
                <td><asp:Label ID="lblSubStatus1" BackColor="Transparent" BorderStyle="None" Width="100px" Text='<%#Eval("substatus")%>' runat="server"></asp:Label></td>
                <td><asp:LinkButton ID="lbtnEdit1" Font-Underline="true" CommandName="Select" runat="server">View</asp:LinkButton></td>
            </tr>
            </AlternatingItemTemplate>
            </asp:ListView>
        <br /><br /> 
        <asp:DetailsView ID="dvIntl" runat="server" AutoGenerateRows="False" Width="800px" 
                CellPadding="4" ForeColor="Black" GridLines="Both" BackColor="White" 
                BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px">
        <RowStyle BackColor="#F7F7DE"/>
        <Fields>
        <asp:BoundField HeaderText="Sub File Number" HeaderStyle-Width="170px"  
                ItemStyle-HorizontalAlign="Left" DataField="subFileNo" ReadOnly="true">
        </asp:BoundField> 
        <asp:BoundField HeaderText="Request Date" 
                ItemStyle-HorizontalAlign="Left" DataField="RequestDt" DataFormatString="{0:dd/MM/yyyy}" ReadOnly="true">
        </asp:BoundField>
        <asp:BoundField HeaderText="Country" 
                ItemStyle-HorizontalAlign="Left" DataField="Country" ReadOnly="true">
        </asp:BoundField>
        <asp:BoundField HeaderText="Partner" 
                ItemStyle-HorizontalAlign="Left" DataField="Partner" ReadOnly="true">
        </asp:BoundField>
        <asp:BoundField HeaderText="Partner Number" 
                ItemStyle-HorizontalAlign="Left" DataField="PartnerNo" ReadOnly="true">
        </asp:BoundField>
        <asp:BoundField HeaderText="Invention Type" 
                ItemStyle-HorizontalAlign="Left" DataField="Type" ReadOnly="true">
        </asp:BoundField>
        <asp:BoundField HeaderText="Attorney" 
                ItemStyle-HorizontalAlign="Left" DataField="Attorney" ReadOnly="true">
        </asp:BoundField>
        <asp:BoundField HeaderText="Application Number" 
                ItemStyle-HorizontalAlign="Left" DataField="ApplicationNo" ReadOnly="true">
        </asp:BoundField>
        <asp:BoundField HeaderText="Filing Date" 
                ItemStyle-HorizontalAlign="Left" DataField="FilingDt" DataFormatString="{0:dd/MM/yyyy}" ReadOnly="true">
        </asp:BoundField>
        <asp:BoundField HeaderText="Publication Number" 
                ItemStyle-HorizontalAlign="Left" DataField="PublicationNo" ReadOnly="true">
        </asp:BoundField>
        <asp:BoundField HeaderText="Publication Date" 
                ItemStyle-HorizontalAlign="Left" DataField="PublicationDt" DataFormatString="{0:dd/MM/yyyy}" ReadOnly="true">
        </asp:BoundField>
        <asp:BoundField HeaderText="Patent Status" 
                ItemStyle-HorizontalAlign="Left" DataField="Status" ReadOnly="true">
        </asp:BoundField>
        <asp:BoundField HeaderText="Patent Sub Status" 
                ItemStyle-HorizontalAlign="Left" DataField="SubStatus" ReadOnly="true">
        </asp:BoundField>
        <asp:BoundField HeaderText="Patent Grant Number" 
                ItemStyle-HorizontalAlign="Left" DataField="PatentNo" ReadOnly="true">
        </asp:BoundField>
        <asp:BoundField HeaderText="Patent Grant Date" 
                ItemStyle-HorizontalAlign="Left" DataField="PatentDt" DataFormatString="{0:dd/MM/yyyy}" ReadOnly="true">
        </asp:BoundField>
        <asp:BoundField HeaderText="Remark" 
                ItemStyle-HorizontalAlign="Left" DataField="Remark" ReadOnly="true">
        </asp:BoundField>
        </Fields>
        <AlternatingRowStyle BackColor="White" />
        </asp:DetailsView>
        </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    </td></tr>
    <tr><td colspan="2">
    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
        <ContentTemplate> 
        <asp:LinkButton ID="lbtnDocument" runat="server" onclick="lbtnDocument_Click">Supporting Documents</asp:LinkButton>     
        <div align="center">
            <asp:GridView ID="GVSD" AutoGenerateColumns="false" Width="700px"  
         CellPadding="5" CellSpacing="0" Font-Names="Arial" BorderColor="#DEBA84" 
                runat="server" onselectedindexchanging="GVSD_SelectedIndexChanging" 
                onrowdatabound="GVSD_RowDataBound">
            <Columns>
            <asp:BoundField DataField="FileDescription" HeaderText="File Description" />
            <asp:TemplateField HeaderText="File Name">
            <ItemTemplate>
                <asp:LinkButton ID="lbtnEdit" Text='<%#Eval("FileName")%>' Font-Underline="true" CommandName="Select" runat="server"></asp:LinkButton> 
            </ItemTemplate>
            </asp:TemplateField>
            </Columns>
             <HeaderStyle ForeColor="#dffaa4" HorizontalAlign="Center" BackColor="#A55129" BorderStyle="Solid" BorderWidth="2" BorderColor="#A55129" VerticalAlign="Middle" Font-Size="13px" Height="30px" />
             <RowStyle BackColor="White" HorizontalAlign="Left" Height="25px" Font-Size="12px" Wrap="true"/>
             <AlternatingRowStyle Wrap="true" HorizontalAlign="Left" BackColor="#E9CEA4" />
            </asp:GridView>
        </div>  
        </ContentTemplate>
        </asp:UpdatePanel>      
    </td></tr>
    <tr><td colspan="2" align="center">
    <asp:ImageButton ID="imgBtnClear" runat="server" 
              ImageUrl="~/Images/Clear2.png" Visible="false" onclick="imgBtnClear_Click" />
    </td></tr>    
    </table>
</asp:Content>

