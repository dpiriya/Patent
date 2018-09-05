<%@ Page Title="" Language="C#" MasterPageFile="~/Parent.master" AutoEventWireup="true"
    CodeFile="FilingConfirmation.aspx.cs" Inherits="Forms_FilingComfirmation" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692FBEA5521E1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="Server">
    <title>Filing Confirmation</title>
    <link href="Styles/Site.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript">
        function Verify()
        {
            var SelFileNo = document.getElementById('<%=ddlFileNo.ClientID%>');
            var SelVal = SelFileNo.options[SelFileNo.selectedIndex].value;
            if (SelVal == "")
            {
                alert('File No. can not be empty');
                return false;
            }
            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">
    <table style="text-align: center" width="980px">
        <tr>
            <th style="font-size: medium; font-family: Times New Roman MS Sans Serif; color: #994c00;
                text-decoration: underline;">
                Filing Confirmation
            </th>
        </tr>
        <tr>
            <td style="font-family: Times New Roman; font-size: 14px">
                File Number &nbsp;&nbsp;&nbsp;<asp:DropDownList ID="ddlFileNo" runat="server"></asp:DropDownList>&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnReport" CssClass="submitButtonStyle" OnClientClick="return Verify()"
                    runat="server" Text="Report" OnClick="btnReport_Click" />
            </td>
        </tr>
        <tr>
            <td>
                <div>
                    <CR:CrystalReportViewer ID="CrystalReportViewer1" HasCrystalLogo="False" HasToggleGroupTreeButton="False"
                        runat="server" />
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
