<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IdfPrint.aspx.cs" Inherits="IdfPrint" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Make PDF</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table>
    <tr><th>Select Print Items from following list</th></tr>
    <tr><td>
        <asp:CheckBox ID="chkboxIdf" Text="IDF Details" runat="server" /><br /><br />
        <asp:CheckBox ID="chkboxIndian" Text="Indian Patent Details" runat="server" /><br /><br />
        <asp:CheckBox ID="chkboxComm" Text="Commercialization Details" runat="server" /><br /><br />
        <asp:CheckBox ID="chkboxInter" Text="International Patent Details" runat="server" /><br /><br />
    </td></tr>
    <tr><td style="text-align:center;">
        <asp:Button ID="btnPrint" runat="server" Text="Print" 
            onclick="btnPrint_Click" /> &nbsp;
        <asp:Button ID="btnClose" runat="server" Text="Close" 
            onclick="btnClose_Click" />
    </td></tr>
    </table>           
    </div>
    </form>
</body>
</html>
