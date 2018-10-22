<%@ Page Language="C#" MasterPageFile="~/Parent.master" AutoEventWireup="true" CodeFile="duediligence.aspx.cs" Inherits="duediligence" %>

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
    </script>
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
                <asp:Label runat="server" Text="File No"></asp:Label>
            </td>
            <td>
                <asp:DropDownList runat="server" ID="ddlidfno"></asp:DropDownList>
            </td>
            <td>
                <asp:Label runat="server" Text="Entry Date"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtentrydt" runat="server" class="date"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                 <asp:Label runat="server">SNo</asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtsno" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Label runat="server">Request Date</asp:Label></td>
            <td>
                <asp:TextBox ID="txtrequestdt" runat="server" class="date"></asp:TextBox></td>
        </tr>
        <tr>
            <td>
                <asp:Label runat="server">Service Request</asp:Label></td>
            <td>
                <asp:TextBox runat="server" ID="txtsrno"></asp:TextBox></td>
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
        <tr>
            <td>
                <asp:Label runat="server">File Upload</asp:Label></td>
            <td>
                <asp:FileUpload ID="fp1" runat="server" /></td>
        </tr>
        <tr align="center">
            <td colspan="4">
                <asp:ImageButton ID="imgBtnSubmit" runat="server"
                    ImageUrl="~/Images/Save2.png" OnClientClick="return Verify()" OnClick="imgBtnSubmit_Click" />&nbsp;               
     <asp:ImageButton ID="imgBtnClear" runat="server"
         ImageUrl="~/Images/Clear2.png" OnClick="imgBtnClear_Click" /></td>
        </tr>
    </table>
</asp:Content>
