<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Parent.master" CodeFile="Home.aspx.cs" Inherits="_Home" Title="Home Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="HeaderContent" ContentPlaceHolderID="cphHead" runat="server">
<link href="Styles/Home.css" rel="Stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="cphBody" runat="server">
<table><tr><td valign="top" style="width:600px">
<h3><a href="IPR policy.pdf">IPR Policy</a></h3>
<p style="height:50px"></p>
<h3><a href="IP Cell Procedures.pdf">Patent Filing Procedure</a></h3>
<p style="height:50px"></p>        
<h3><a href="IPR Orientation Program.pdf">Training Presentation </a></h3>
<p style="height:50px"></p>        
</td>
<td valign="top" align="center">
<table width="400px" align="center" style="height:350px">
            <tr align="center">
                <td>
                    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
                    </asp:ToolkitScriptManager>
                    <asp:Label ID="lableImageDetail" runat="server" ForeColor="#000000" />
                </td>
            </tr>
            <tr align="center">
                <td>
                    <asp:Image runat="server" ID="image1" Height="400" Width="400" />
                </td>
            </tr>
            
        </table>
                <asp:SlideShowExtender ID="slideShowExtender1" runat="Server" TargetControlID="image1"
ImageDescriptionLabelID="lableImageDetail" Loop="true" AutoPlay="true" SlideShowServiceMethod="GetSlides"> </asp:SlideShowExtender>
</td></tr>
  <tr><td colspan="2">
  </td>
  </tr>  
    </table>
</asp:Content>
       
