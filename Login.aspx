<%@ Page Language="C#" MasterPageFile="~/Parent.master" AutoEventWireup="true"
    CodeFile="Login.aspx.cs" Inherits="Account_Login" %>    
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="cphHead">
    <link href="./Styles/Site.css" rel="Stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="cphBody">
<table><tr><td style="width:500px;vertical-align:middle;height:500px" align="center">
    <asp:Login ID="LoginUser" runat="server" EnableViewState="false" RenderOuterTable="false">
        <LayoutTemplate>
            <div class="accountInfo">
                <fieldset class="login" >                     
                    <table cellpadding="7px">
                    <tr>
                    <td><asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName" CssClass="inline">Username</asp:Label></td>
                    <td><asp:TextBox ID="UserName" runat="server" CssClass="textEntry"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" 
                             CssClass="failureNotification" ErrorMessage="User Name is required." ToolTip="User Name is required." 
                             ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr><td><asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password" CssClass="inline">Password</asp:Label></td>
                    <td><asp:TextBox ID="Password" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password" 
                             CssClass="failureNotification" ErrorMessage="Password is required." ToolTip="Password is required." 
                             ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr><td colspan="2" align="center"><span class="failureNotification">
                        <asp:Literal ID="FailureText" runat="server"></asp:Literal>
                    </span>
                    <asp:ValidationSummary ID="LoginUserValidationSummary" runat="server" CssClass="failureNotification" 
                         ValidationGroup="LoginUserValidationGroup"/>
                    </td></tr>
                    <tr><td colspan="2" align="center"><asp:CheckBox ID="RememberMe" runat="server"/>
                        <asp:Label ID="RememberMeLabel" runat="server" AssociatedControlID ="RememberMe" CssClass="inline">Keep me logged in</asp:Label></td></tr>
                        
                    <tr><td colspan="2" align="right"><asp:Button ID="LoginButton" runat="server" CommandName="Login" Text="Log In" 
                        ValidationGroup="LoginUserValidationGroup" CssClass="submitButtonStyle" /></td></tr>
                    </table>                   
                </fieldset>                
            </div>
        </LayoutTemplate>
    </asp:Login>
    </td>
    <td style="width:500px;vertical-align:middle;" align="center">    
<table width="400px" style="height:400px;border-style:ridge;border-width:2px;border-color:#efdb96;text-align:center;">
            <tr align="center">
                <td>
                    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
                    </asp:ToolkitScriptManager>
                    <asp:Label ID="lableImageDetail" runat="server" ForeColor="#000000" />
                </td>
            </tr>
            <tr align="center">
                <td>
                    <asp:Image runat="server" ID="image1" ImageUrl="~/Slide/images1.jpg" Height="400" Width="400" />
                </td>
            </tr>            
        </table>
              
</td></tr></table>
</asp:Content>
