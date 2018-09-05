<%@ Page Language="C#" MasterPageFile="~/Parent.master" AutoEventWireup="true" CodeFile="ReportPage.aspx.cs" Inherits="ReportPage" Title="Reports Page" ClientTarget="Uplevel" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692FBEA5521E1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="Server">
    <link href="Styles/RptStyle.css" rel="Stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">

    <table id="tblReport">
        <tr>
            <td>
                <p class="BoxTitle">Overview</p>
                <p><a href="IdfOutlook.aspx">R001 - IDF Overview</a></p>
                <p><a href="ReportSelection2.aspx?id=AgeAnalysis">R002 - Age Analysis</a></p>
               <p class="BoxTitle">Prosecution and Maintenance</p>
                <p><a href="ReportSelection2.aspx?id=Renewal">R107 - Renewal List</a></p>
                <%--<p><a href="RenewalDateWiseReport.aspx?">R107-A - Action Plan Template</a></p>--%>
                <p><a href="RenewalActionPlanReport.aspx?">R107-A - IDF Wise</a></p>
                <p><a href="RenewalDateWiseReport.aspx?">R107-B - Period Wise</a></p>
                <p class="BoxTitle">Marketing</p>
                <p><a href="MarketingSearch.aspx">R601 - Marketing Project Search</a></p>
                <p><a href="CompanySearch.aspx">R602 - Company Search</a></p>
                <p><a href="CompanySearch2.aspx">R602-A - Letters to company</a></p>
                <p><a href="ReportSelection2.aspx?id=Commercial">R603 - Commercialization</a></p>
                <p><a href="ReportSelection2.aspx?id=CommercialStatus">R605 - Commercialization Status</a></p>
            </td>
            <td>
                <p class="BoxTitle">Indian Patent</p>
                <p><a href="ReportSelection2.aspx?id=FileNoWise">R101 - File Number Wise</a></p>
                <p><a href="ReportSelection1.aspx?id=Inventor">R102 - Inventor Wise I</a></p>
                <p><a href="ReportSelection1.aspx?id=Inventor2">R102A - Inventor Wise II</a></p>
                <p><a href="ReportSelection1.aspx?id=Attorney">R103 - Attorney Wise</a></p>
                <p><a href="ReportSelection2.aspx?id=EntryDt">R104 - Entry Date Wise</a></p>
                <p><a href="ReportSelection2.aspx?id=FilingDt">R105 - Filing Date Wise</a></p>
                <p><a href="ReportSelection2.aspx?id=PatDt">R106 - Patent Date Wise</a></p>
                <p><a href="FindFiles.aspx?id=Provisional">R108 - Provisional Application</p>
                <p><a href="FindFiles.aspx?id=Examination">R109 - Examination & Publication</a></p>
            </td>
            <td>
                <p class="BoxTitle">Int'l Patent</p>
                <p><a href="ReportSelection.aspx?id=IPReport">R201 - Int'l Patent List</a></p>
                <p><a href="ReportSelection1.aspx?id=IntlInventor">R202 - Inventor Wise</a></p>
                <p><a href="ReportSelection1.aspx?id=IntlAttorney">R203 - Attorney Wise</a></p>
                <p><a href="ReportSelection2.aspx?id=IntlEntryDt">R204 - Entry Date Wise</a></p>
                <p><a href="ReportSelection2.aspx?id=IntlFilingDt">R205 - Filing Date Wise</a></p>
                <p><a href="ReportSelection2.aspx?id=IntlPatDt">R206 - Patent Date Wise</a></p>
                <p><a href="ReportSelection1.aspx?id=IntlCountry">R207 - Country Wise</a></p>
                <p><a href="FindFiles.aspx?id=PCT_Filing">R208 - PCT/National Face Filing</a></p>
                <p style="text-align: left"><a href="FindFiles.aspx?id=Foreign">R209 - Foreign Filing Review(Excluding Direct International)</a></p>
                <p style="text-align: left"><a href="#">R210 - Foreign Filing Review(Direct International)</a></p>
            </td>
            <td>
                <p class="BoxTitle">Service Request & Payment</p>
                <p><a href="Rpt_SR.aspx">R301 - Service Request</a></p>
                <p><a href="ReportSelection.aspx?id=PaymentReport">R302 - File Number Wise</a></p>
                <p><a href="ReportSelection1.aspx?id=PayInventor">R303 - Inventor Wise</a></p>
                <p><a href="ReportSelection2.aspx?id=PayDtWise">R304 -Payment Date Wise</a></p>
                <p class="BoxTitle">Receipt</p>
                <p><a href="ReportSelection.aspx?id=ReceiptReport">R401 - File Number Wise</a></p>
                <p class="BoxTitle">Contract</p>
                <p><a href="ReportSelection4.aspx?id=Contract">R604 - Contract</a></p>
                <p><a href="ContractActionDate.aspx?">R604-A - Contract Action Plan</a></p>
                <p><a href="AgreementTrack.aspx?">R604-B - Agreement Tracker</a></p>
                 <p class="BoxTitle">Miscellaneous</p>
                <p><a href="AuditDetails.aspx">R501 - User Audit</a></p>            
                
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <CR:CrystalReportViewer ID="CrystalReportViewer1" Visible="false" runat="server" AutoDataBind="true" />
            </td>
        </tr>
    </table>
</asp:Content>

