<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AgeGraph.aspx.cs" Inherits="AgeGraph" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Chart ID="AgeChart" Width="600" Height="600" runat="server" >
        <Titles><asp:Title Font="Times new roman, 14pt, style=Bold" ForeColor="Brown"> 
        </asp:Title>
        </Titles>
            <Series>
                <asp:Series Name="AgeSeries1" Font="Arial, 9pt, style=Bold">
                </asp:Series>
                <asp:Series Name="AgeSeries2" Font="Arial, 9pt, style=Bold"></asp:Series>
            </Series>
            <ChartAreas>
                <asp:ChartArea Name="AgeChartArea1">
                <AxisX Interval="1" Title="Year" TitleFont="Arial, 8pt, style=Bold" TitleForeColor="Green"></AxisX>
                <AxisY Interval="10" Title="No. of Patents" TitleForeColor="Green"></AxisY>
                </asp:ChartArea>
            </ChartAreas>
            <Legends>
            <asp:Legend Alignment="Center" Font="Arial, 9.75pt, style=Bold" 
                    IsTextAutoFit="False"></asp:Legend>
            </Legends>
        </asp:Chart>
    </div>
    </form>
</body>
</html>
