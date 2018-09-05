<%@ Page Title="" Language="C#" MasterPageFile="~/Parent.master" AutoEventWireup="true" CodeFile="Default3.aspx.cs" Inherits="Default3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
    <link href="Styles/newEntry.css" rel="Stylesheet" type="text/css" />
<script src="Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
<script src="Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <style type="text/css">
        .style2
        {
            width: 503px;
        }
        .style3
        {
            width: 305px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <table align="center" class="neTable" cellpadding="12px" style="width:985px" cellspacing="10px">
      
      <tr><td class="style3">IDF NO:</td>
      <td><asp:DropDownList ID="drop1" runat="server"></asp:DropDownList></td></tr>
      
      
        <tr><td class="style3" >SL.No </td><td class="style2">Details</td>
        <td>Required</td>
        <td>Remarks</td>
        </tr>
  <tr><td class="style3">1.</td>
  <td class="style2"><font size="2">Search Report(As Per IITM Specification <br />[ First inventor approved version to be shared with us, including atleast 5 to 10 closest prior art patents with overlapping / anticipating,ovbivousness text highlighted; plus atleast 5 non literatues or publications ]</font></td>
  <td> Yes <asp:RadioButton ID=radio1 runat="server" GroupName="Req" />&nbsp;&nbsp;&nbsp; No <asp:RadioButton ID=radio2 runat="server" GroupName="Req" Checked="false"/></td>
  <td><asp:TextBox ID="text1" runat="server"></asp:TextBox></td></tr>
  <tr><td class="style3">2.</td>
  <td>Provisonal Filing</td>
  <td>Yes <asp:RadioButton ID=RadioButton1 runat="server" GroupName="Req1" />&nbsp;&nbsp;&nbsp; No <asp:RadioButton ID=RadioButton2 runat="server" GroupName="Req" Checked="false"/></td>
   <td><asp:TextBox ID="TextBox1" runat="server"></asp:TextBox></td></tr>
<tr><td class="style3">3.</td>
<td>Techno-Commercial Value Assessment<br /><font size="2">[An assessment of commercial potential of this invention on its own]</font></td>
<td> Yes <asp:RadioButton ID=RadioButton3 runat="server" GroupName="Req2" />&nbsp;&nbsp;&nbsp; No <asp:RadioButton ID=RadioButton4 runat="server" GroupName="Req" Checked="false"/></td><td><asp:TextBox ID="TextBox2" runat="server"></asp:TextBox></td></tr>
<tr><td class="style3">4.</td><td>Complete Filing Based on patentablitity Search Report Feedback<br />
    <font size="2">[Complete Filing after confirmation by inventor]<br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
    If after direct filing, patent application number and IDF</font></td>
    <td> Yes <asp:RadioButton ID=RadioButton5 runat="server" GroupName="Req3" />&nbsp;&nbsp;&nbsp; No <asp:RadioButton ID=RadioButton6 runat="server" GroupName="Req" Checked="false"/></td><td><asp:TextBox ID="TextBox3" runat="server"></asp:TextBox></td></tr>

<tr><td class="style3">5.</td>
<td>Request for Examination(RFE)<br />
Request for Early Publication<br />
Request for Early Examination</td>
<td> Yes <asp:RadioButton ID=RadioButton7 runat="server" GroupName="Req4" />&nbsp;&nbsp;&nbsp; No <asp:RadioButton ID=RadioButton8 runat="server" GroupName="Req" Checked="false"/></td><td><asp:TextBox ID="TextBox4" runat="server"></asp:TextBox></td></tr>
<tr><td class="style3">6.</td><td>Whether Patent of Addition <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
   <font size="2"> a) If Yes the earlier related Patent Application IDF No.<br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
    b) The relevant Patent Application No. and date<br /></font>
&nbsp;&nbsp;&nbsp; </td>
<td> Yes <asp:RadioButton ID=RadioButton9 runat="server" GroupName="Req5" />&nbsp;&nbsp;&nbsp; No <asp:RadioButton ID=RadioButton10 runat="server" GroupName="Req" Checked="false"/></td><td><asp:TextBox ID="TextBox5" runat="server"></asp:TextBox></td></tr>
<tr><td class="style3">7.</td>
<td>Cognate(COMBINED) Application<br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
    <font size="2">If Cognate, the earlier IDF no. related</font></td>
    <td> Yes <asp:RadioButton ID=RadioButton11 runat="server" GroupName="Req6" />&nbsp;&nbsp;&nbsp; No <asp:RadioButton ID=RadioButton12 runat="server" GroupName="Req" Checked="false"/></td><td><asp:TextBox ID="TextBox6" runat="server"></asp:TextBox></td>
    </tr>
    <tr><td class="style3">8.</td>
    <td>Value Proposition for Meeting<br /><font size="2">[Abriged with 100 words to explain the invention after filing]</font></td>
    <td> Yes <asp:RadioButton ID=RadioButton13 runat="server" GroupName="Req7" />&nbsp;&nbsp;&nbsp; No <asp:RadioButton ID=RadioButton14 runat="server" GroupName="Req" Checked="false"/></td><td><asp:TextBox ID="TextBox7" runat="server"></asp:TextBox></td>
    </tr>
    <tr><td class="style3">9.</td>
    <td>Techno Commercial evaluation (Prior to international filing)</td>
    <td> Yes <asp:RadioButton ID=RadioButton15 runat="server" GroupName="Req8" />&nbsp;&nbsp;&nbsp; No <asp:RadioButton ID=RadioButton16 runat="server" GroupName="Req" Checked="false"/></td><td><asp:TextBox ID="TextBox8" runat="server"></asp:TextBox></td>
    </tr>
    <tr><td class="style3">10.</td>
    <td>Inventor to be contacted for necessary action:</td>
    <td>Yes <asp:RadioButton ID=RadioButton17 runat="server" GroupName="Req9" />&nbsp;&nbsp;&nbsp; No <asp:RadioButton ID=RadioButton18 runat="server" GroupName="Req" Checked="false"/></td><td><asp:TextBox ID="TextBox9" runat="server"></asp:TextBox></td></td></tr>
        </table>
</asp:Content>

