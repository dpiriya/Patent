﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default4.aspx.cs" Inherits="Default4" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:GridView ID="Grid1" runat="server" AutoGenerateColumns="false" Width="600px">
    <Columns>
    <asp:TemplateField HeaderText="Select">
    <ItemTemplate><asp:CheckBox ID="cbSelect" runat="server" /></ItemTemplate></asp:TemplateField>
<asp:BoundField DataField="ID" HeaderText="ID" />
<asp:BoundField DataField="Details" HeaderText="Details" />
    
    </Columns></asp:GridView>
    
    </div>
    </form>
</body>
</html>
