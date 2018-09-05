<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LetterGenerator.aspx.cs" Inherits="LetterGenerator" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 <title></title>
 <link href="Styles/LetterGenerator.css"  rel="stylesheet" type="text/css" media="screen" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div style="text-align:center"><h3>Technology Transer-IP Licensing, Collaboration</h3></div>
    <div style="padding-left:220px"><h4>IDF File Number</h4></div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
<asp:ListView ID="lvIdf" runat="server" InsertItemPosition="LastItem" 
            onitemcreated="lvIdf_ItemCreated" onitemdatabound="lvIdf_ItemDataBound" 
            onitemcanceling="lvIdf_ItemCanceling" onitemdeleting="lvIdf_ItemDeleting" 
            oniteminserting="lvIdf_ItemInserting">
<LayoutTemplate>
<table id="Table1" width="930px" align="center" runat="server"  style="text-align:left;border-color:#DEDFDE; border-style:solid; border-width:1px;" cellpadding="5" cellspacing="0" border="1">
    <tr id="Tr1" runat="server" style="background-color:#c8d59c">
    <th id="Th1" align="center" runat="server">Sl.No.</th>
    <th id="Th2" align="center" runat="server">IDF File Number</th>
    <th id="Th3" align="center" runat="server">IDF Title</th>
    <th id="Th4" align="center" runat="server">Inventor</th>
    <th id="Th7" align="center" runat="server">Application No</th>
    <th id="Th8" align="center" runat="server">Status</th>
    <th id="Th5" runat="server"></th>
    </tr>
    <tr runat="server" id="itemPlaceholder" />
    </table>
</LayoutTemplate>
<ItemTemplate>
    <tr id="Tr2" runat="server" style="background-color:White">
    <td><asp:Label ID="lblSlNo" Text='<%#Eval("SlNo")%>' runat="server"></asp:Label></td>
    <td><asp:Label ID="lblIdfNo" runat="server" BackColor="Transparent" BorderStyle="None" Width="50px" Text='<%#Eval("FileNo")%>'></asp:Label></td>
    <td><asp:TextBox ID="lblTitle" BackColor="Transparent" BorderStyle="None" Width="300px" TextMode="MultiLine" Wrap="true" ReadOnly="true" Text='<%#Eval("Title")%>' runat="server"></asp:TextBox></td>
    <td><asp:TextBox ID="lblInventor" BackColor="Transparent" BorderStyle="None" Width="150px" TextMode="MultiLine" Wrap="true" ReadOnly="true" Text='<%#Eval("Inventor1")%>' runat="server"></asp:TextBox></td>
    <td><asp:TextBox ID="lblApplcnNo" BackColor="Transparent" BorderStyle="None" Width="100px" TextMode="MultiLine" Wrap="true" ReadOnly="true" Text='<%#Eval("applcn_no")%>' runat="server"></asp:TextBox></td>
    <td><asp:TextBox ID="lblStatus" BackColor="Transparent" BorderStyle="None" Width="100px" TextMode="MultiLine" Wrap="true" ReadOnly="true" Text='<%#Eval("status")%>' runat="server"></asp:TextBox></td>
    <td><asp:LinkButton ID="lbtnDelete" Font-Underline="true" CommandName="delete" runat="server">Delete</asp:LinkButton>
    </td>
    </tr>
</ItemTemplate>
<AlternatingItemTemplate>
    <tr id="Tr2" runat="server" style="background-color:#F7F7DE">
    <td><asp:Label ID="lblSlNo" Text='<%#Eval("SlNo")%>' runat="server"></asp:Label></td>
    <td><asp:Label ID="lblIdfNo" runat="server" BackColor="Transparent" BorderStyle="None" Width="50px" Text='<%#Eval("FileNo")%>'></asp:Label></td>
    <td><asp:TextBox ID="lblTitle" BackColor="Transparent" BorderStyle="None" Width="300px" TextMode="MultiLine" Wrap="true" ReadOnly="true" Text='<%#Eval("Title")%>' runat="server"></asp:TextBox></td>
    <td><asp:TextBox ID="lblInventor" BackColor="Transparent" BorderStyle="None" Width="150px" TextMode="MultiLine" Wrap="true" ReadOnly="true" Text='<%#Eval("Inventor1")%>' runat="server"></asp:TextBox></td>
    <td><asp:TextBox ID="lblApplcnNo" BackColor="Transparent" BorderStyle="None" Width="100px" TextMode="MultiLine" Wrap="true" ReadOnly="true" Text='<%#Eval("applcn_no")%>' runat="server"></asp:TextBox></td>
    <td><asp:TextBox ID="lblStatus" BackColor="Transparent" BorderStyle="None" Width="100px" TextMode="MultiLine" Wrap="true" ReadOnly="true" Text='<%#Eval("status")%>' runat="server"></asp:TextBox></td>
    <td><asp:LinkButton ID="lbtnDelete" Font-Underline="true" CommandName="delete" runat="server">Delete</asp:LinkButton>
    </td>
    </tr>
</AlternatingItemTemplate>
<InsertItemTemplate>
<tr id="Tr4" runat="server">
    <td><asp:Label ID="lblNewSlNo" Text='<%#Eval("SlNo")%>' runat="server"></asp:Label></td>
    <td><asp:DropDownList ID="ddlNewIdfNo" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlNewIdfNo_SelectedIndexChanged"></asp:DropDownList></td>
    <td><asp:TextBox ID="txtTitle" BackColor="Transparent" ReadOnly="true" BorderStyle="None" Width="300px" TextMode="MultiLine" Wrap="true" Text='<%#Eval("Title")%>' runat="server"></asp:TextBox></td>
    <td><asp:TextBox ID="txtInventor" BackColor="Transparent" ReadOnly="true" BorderStyle="None" Width="150px" TextMode="MultiLine" Wrap="true" Text='<%#Eval("Inventor1")%>' runat="server"></asp:TextBox></td>
    <td><asp:TextBox ID="txtApplcnNo" BackColor="Transparent" ReadOnly="true" BorderStyle="None" Width="100px" TextMode="MultiLine" Wrap="true" Text='<%#Eval("applcn_no")%>' runat="server"></asp:TextBox></td>
    <td><asp:TextBox ID="txtStatus" BackColor="Transparent" ReadOnly="true" BorderStyle="None" Width="100px" TextMode="MultiLine" Wrap="true" Text='<%#Eval("status")%>' runat="server"></asp:TextBox></td>
    <td><asp:LinkButton ID="lbtnInsert" Font-Underline="true" CommandName="insert" runat="server">Insert</asp:LinkButton>
    <asp:LinkButton ID="lbtnCancel" Font-Underline="true" CommandName="cancel" runat="server">Cancel</asp:LinkButton>
    </td>
    </tr>
</InsertItemTemplate>
</asp:ListView>
</ContentTemplate>
</asp:UpdatePanel>
<div style="text-align:center; vertical-align:bottom;" ><br /><asp:Button ID="BtnSelectRecord" runat="server" 
        Text="Letter"  onclick="BtnSelectRecord_Click" CssClass="LetterButton" /></div>
    <div>
    <table align="center">
     <tr><td align="left" style=" font-family:Arial;font-size:14px;font-weight:bold; text-decoration:underline;">
         Search Results</td></tr>
     <tr><td >
     <asp:GridView ID="gvGenerator" AutoGenerateColumns="false" Width="930px" 
             CellPadding="3" CellSpacing="0"  
             Font-Names="Arial" BorderColor="#006633" runat="server" >
     <Columns>
     <asp:TemplateField HeaderText="Select">
     <ItemTemplate>
         <asp:CheckBox ID="chkSelected" runat="server" />
     </ItemTemplate>
     </asp:TemplateField>
     <asp:BoundField DataField="CompanyID" HeaderText="Company ID" ItemStyle-HorizontalAlign="Center" />
     <asp:BoundField DataField="CompanyName" HeaderText="Company Name" ItemStyle-HorizontalAlign="Left" SortExpression="CompanyName" ItemStyle-Wrap="true" />
     <asp:BoundField DataField="IndustryType" HeaderText="Industry Type" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="true" />
     <asp:BoundField DataField="ContactPersonName" HeaderText="Contact Person Name" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="true" />     
     <asp:BoundField DataField="Address1" HeaderText="Address 1" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="true" />     
     <asp:BoundField DataField="Address2" HeaderText="Address 2" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="true" />          
     </Columns>
     <EmptyDataTemplate>
     <p style="text-align:center; background-color:#C8C8C8">
     <asp:Image ID="Image1" BorderStyle="None" BackColor="#C8C8C8" ImageUrl="~/Images/NoData.jpg" runat="server" />
     </p>
     </EmptyDataTemplate>
     <HeaderStyle HorizontalAlign="Center" BackColor="#006633" VerticalAlign="Middle" Font-Size="14px" Height="35px" />
     <RowStyle BackColor="White" Height="25px" Font-Size="13px" Wrap="true"/>
     <AlternatingRowStyle Wrap="true" BackColor="#FFFFCC" />
     <PagerSettings Position="TopAndBottom" Mode="NextPreviousFirstLast" FirstPageText="First" PreviousPageText="Previous" NextPageText="Next" LastPageText="Last" />
     <PagerStyle ForeColor="Black" Font-Bold="true" Font-Italic="true"  Font-Underline="true" HorizontalAlign="Center" />
     </asp:GridView>   
     </td></tr></table>    
    </div>
    </form>
</body>
</html>
