using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Globalization;
using System.Data.SqlClient;

public partial class ReportSelection2 : System.Web.UI.Page
{
    string id;
    SqlConnection con = new SqlConnection();
    protected void Page_Load(object sender, EventArgs e)
    {
        id = Request.QueryString["id"];
        if (id == "FilingDt")
        {
            this.Title = "Filing Date Wise Report";
        }
        else if (id == "Renewal")
        {
            this.Title = "P&M ACTION LIST REPORT";
        }
        else if (id == "FileNoWise")
        {
            this.Title = "File Number Wise Report";
        }
        else if (id == "EntryDt")
        {
            this.Title = "Entry Date Wise Report";
        }
        else if (id == "PatDt")
        {
            this.Title = "Patent Date Wise Report";
        }
        else if (id == "IntlFilingDt")
        {
            this.Title = "Filing Date Wise Report";
        }
        else if (id == "IntlPatDt")
        {
            this.Title = "Patent Date Wise Report";
        }
        else if (id == "IntlEntryDt")
        {
            this.Title = "Entry Date Wise Report";
        }
        else if (id == "PayDtWise")
        {
            this.Title = "Payment Date Wise Report";
        }
        else if (id == "AgeAnalysis")
        {
            this.Title = "Age Analysis";
        }
        else if (id == "Commercial")
        {
            this.Title = "Commercialization Report Based On Filing Date";
        }
        else if (id == "CommercialStatus")
        {
            this.Title = "Commercialization Status";
        }
        
        if (!IsPostBack)
        {
            if (id == "Renewal")
            {
                lblTitle.InnerText = "P&M ACTION LIST FOR SELECTED MONTH";
                imgFromDt.Visible = false;
                imgToDt.Visible = false;
                lblInput1.InnerText = "Month (MM)";
                lblInput2.InnerText = "Year (yyyy)";
                CalendarExtender1.Enabled = false;
                CalendarExtender2.Enabled = false;
                Renewal.Visible = true;
                ddlRenewal.Items.Add("");
                ddlRenewal.Items.Add("Yes");
                ddlRenewal.Items.Add("No");
            }
            else if (id == "FileNoWise")
            {
                lblTitle.InnerText = "FILE NUMBER WISE REPORT";
                imgFromDt.Visible = false;
                imgToDt.Visible = false;
                lblInput1.InnerText = "Starting File Number";
                lblInput2.InnerText = "Ending File Number";
                lblDrop.InnerText = "Commercialization";
                CalendarExtender1.Enabled = false;
                CalendarExtender2.Enabled = false;
                Renewal.Visible = true;
                ddlRenewal.Items.Add("All");
                ddlRenewal.Items.Add("IV");
                ddlRenewal.Items.Add("IITM");
                ddlRenewal.Items.Add("Others");
            }
            else if (id == "AgeAnalysis")
            {
                lblTitle.InnerText = "AGE ANALYSIS";
                imgFromDt.Visible = false;
                imgToDt.Visible = false;
                lblInput1.InnerText = "Starting Year (YYYY)";
                lblInput2.InnerText = "Ending Year (YYYY)";
                CalendarExtender1.Enabled = false;
                CalendarExtender2.Enabled = false;
                imgBtnGraph.Visible = true;
            }
            else if (id == "EntryDt")
            {
                lblTitle.InnerText = "ENTRY DATE WISE REPORT";
                Renewal.Visible = true;
                lblDrop.InnerText = "IDF Status";
                ddlRenewal.Items.Add("All");
                ddlRenewal.Items.Add("Filed");
                ddlRenewal.Items.Add("Not Yet Filed");
                ddlRenewal.Items.Add("Closed - Internal");
                ddlRenewal.Items.Add("Closed - External");
                ddlRenewal.Items.Add("Granted");
                ddlRenewal.Items.Add("Not Yet Granted");
            }
            else if (id == "FilingDt")
            {
                lblTitle.InnerText = "FILING DATE WISE REPORT";
                Renewal.Visible = true;
                lblDrop.InnerText = "IDF Status";
                ddlRenewal.Items.Add("All");
                ddlRenewal.Items.Add("Granted");
                ddlRenewal.Items.Add("Not Yet Granted");
            }
            else if (id == "PatDt")
            {
                lblTitle.InnerText = "PATENT DATE WISE REPORT";
            }
            else if (id == "IntlFilingDt")
            {
                lblTitle.InnerText = "FILING DATE WISE REPORT";
            }
            else if (id == "IntlPatDt")
            {
                lblTitle.InnerText = "PATENT DATE WISE REPORT";
            }
            else if (id == "IntlEntryDt")
            {
                lblTitle.InnerText = "ENTRY DATE WISE REPORT";
            }
            else if (id == "PayDtWise")
            {
                lblTitle.InnerText = "PAYMENT DATE WISE REPORT";
            }
            else if (id == "Commercial")
            {
                lblTitle.InnerText = "COMMERCIALIZATION REPORT BASED ON FILING DATE";
                lblDrop.InnerText = "Commercialization";
                Renewal.Visible = true;
                con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
                string sql5 = "SELECT ITEMLIST FROM LISTITEMMASTER WHERE CATEGORY LIKE 'COMMERCIALIZED' ORDER BY ITEMLIST";
                con.Open();
                SqlCommand cmd = new SqlCommand(sql5, con);
                SqlDataReader dr;
                dr = cmd.ExecuteReader();
                ddlRenewal.DataTextField = "ItemList";
                ddlRenewal.DataValueField = "ItemList";
                ddlRenewal.DataSource = dr;
                ddlRenewal.DataBind();
                ddlRenewal.Items.Insert(0, new ListItem("", ""));
                con.Close();
            }
            else if (id == "CommercialStatus")
            {
                lblTitle.InnerText = "COMMERCIALIZATION STATUS";
                lblDrop.InnerText = "Industry";
                Renewal.Visible = true;
                ddlRenewal.Items.Add("");
                ddlRenewal.Items.Add("Industry I");
                ddlRenewal.Items.Add("Industry II");
            }
        }
    }
    protected void imgBtnReport_Click(object sender, ImageClickEventArgs e)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        if (id != "FileNoWise" && id != "Renewal" && id != "AgeAnalysis")
        {
            DateTime fromDate;
            DateTime toDate;
            if (DateTime.TryParse(txtFromDt.Text.Trim(), out fromDate))
            {}
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "Information", "<script>alert('Verify From Date')</script>");
                return;
            }
            if (DateTime.TryParse(txtToDt.Text.Trim(), out toDate))
            {}
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "Information", "<script>alert('Verify To Date')</script>");
                return;
            }
            TimeSpan ts = toDate - fromDate;
            if (ts.Days < 0)
            {
                ClientScript.RegisterStartupScript(GetType(), "Information", "<script>alert('To Date should be greater than From Date')</script>");
                return;
            }
        }
        if (id == "FilingDt")
        {
            if (txtFromDt.Text.Trim() != "" && txtToDt.Text.Trim() != "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "filingdt_window", String.Format("void(window.open('{0}','child_filingdt','menubar=0,toolbar=0,resizable=1,scrollbars=1'));", "Review.aspx?id=FilingDt&FromDt=" + txtFromDt.Text.Trim() + "&ToDt=" + txtToDt.Text.Trim()+ "&IDFStatus=" + ddlRenewal.SelectedItem.Text.Trim()), true);
            }
        
        }
        else if (id == "Renewal")
        {
            if (txtFromDt.Text.Trim() != "" && txtToDt.Text.Trim() != "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "renewal_window", String.Format("void(window.open('{0}','child_Renewal','menubar=0,toolbar=0,resizable=1,scrollbars=1'));", "Review.aspx?id=Renewal&FromDt=" + txtFromDt.Text.Trim() + "&ToDt=" + txtToDt.Text.Trim()+"&NotRenewal="+ ddlRenewal.SelectedItem.Text.Trim()), true);
            }
        }
        else if (id == "FileNoWise")
        {
            if (txtFromDt.Text.Trim() != "" && txtToDt.Text.Trim() != "")
            {
                int fromNumber, toNumber;
                if (int.TryParse(txtFromDt.Text.Trim(), out fromNumber) && int.TryParse(txtToDt.Text.Trim(), out toNumber))
                {
                    if (fromNumber > toNumber)
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Information", "<script>alert('First Number should be less than Second Number')</script>");
                        return;
                    }
                }
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "Information", "<script>alert('Please Verify input values')</script>");
                    return;
                }

                Page.ClientScript.RegisterStartupScript(this.GetType(), "filingdt_window", String.Format("void(window.open('{0}','child_fileNo','menubar=0,toolbar=0,resizable=1,scrollbars=1'));", "Review.aspx?id=FileNoWise&FromNo=" + txtFromDt.Text.Trim() + "&ToNo=" + txtToDt.Text.Trim() + "&Commercial=" + ddlRenewal.SelectedItem.Text.Trim()), true);
            }

        }
        else if (id == "EntryDt")
        {
            if (txtFromDt.Text.Trim() != "" && txtToDt.Text.Trim() != "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "entrydt_window", String.Format("void(window.open('{0}','child_entrydt','menubar=0,toolbar=0,resizable=1,scrollbars=1'));", "Review.aspx?id=EntryDt&FromDt=" + txtFromDt.Text.Trim() + "&ToDt=" + txtToDt.Text.Trim() + "&IDFStatus=" + ddlRenewal.SelectedItem.Text.Trim()), true);
            }

        }
        else if (id == "PatDt")
        {
            if (txtFromDt.Text.Trim() != "" && txtToDt.Text.Trim() != "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "patdt_window", String.Format("void(window.open('{0}','child_patdt','menubar=0,toolbar=0,resizable=1,scrollbars=1'));", "Review.aspx?id=PatDt&FromDt=" + txtFromDt.Text.Trim() + "&ToDt=" + txtToDt.Text.Trim()), true);
            }

        }
        else if (id == "IntlFilingDt")
        {
            if (txtFromDt.Text.Trim() != "" && txtToDt.Text.Trim() != "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Intlfilingdt_window", String.Format("void(window.open('{0}','child_Intlfilingdt','menubar=0,toolbar=0,resizable=1,scrollbars=1'));", "Review.aspx?id=IntlFilingDt&FromDt=" + txtFromDt.Text.Trim() + "&ToDt=" + txtToDt.Text.Trim()), true);
            }

        }
        else if (id == "IntlPatDt")
        {
            if (txtFromDt.Text.Trim() != "" && txtToDt.Text.Trim() != "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Intlpatdt_window", String.Format("void(window.open('{0}','child_Intlpatdt','menubar=0,toolbar=0,resizable=1,scrollbars=1'));", "Review.aspx?id=IntlPatDt&FromDt=" + txtFromDt.Text.Trim() + "&ToDt=" + txtToDt.Text.Trim()), true);
            }

        }
        else if (id == "IntlEntryDt")
        {
            if (txtFromDt.Text.Trim() != "" && txtToDt.Text.Trim() != "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "IntlEntrydt_window", String.Format("void(window.open('{0}','child_IntlEntrydt','menubar=0,toolbar=0,resizable=1,scrollbars=1'));", "Review.aspx?id=IntlEntryDt&FromDt=" + txtFromDt.Text.Trim() + "&ToDt=" + txtToDt.Text.Trim()), true);
            }

        }
        else if (id == "PayDtWise")
        {
            if (txtFromDt.Text.Trim() != "" && txtToDt.Text.Trim() != "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "PayDtWise_window", String.Format("void(window.open('{0}','child_PayDtWise','menubar=0,toolbar=0,resizable=1,scrollbars=1'));", "Review.aspx?id=PayDtWise&FromDt=" + txtFromDt.Text.Trim() + "&ToDt=" + txtToDt.Text.Trim()), true);
            }

        }
        else if (id == "AgeAnalysis")
        {
            if (txtFromDt.Text.Trim() != "" && txtToDt.Text.Trim() != "")
            {
                int fromNumber, toNumber;
                if (int.TryParse(txtFromDt.Text.Trim(), out fromNumber) && int.TryParse(txtToDt.Text.Trim(), out toNumber))
                {
                    if (fromNumber > toNumber)
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Information", "<script>alert('First Number should be less than Second Number')</script>");
                        return;
                    }
                }
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "Information", "<script>alert('Please Verify input values')</script>");
                    return;
                }
                if (fromNumber < 1979)
                {
                    ClientScript.RegisterStartupScript(GetType(), "Information", "<script>alert('Starting Year should be greater than or equal to 1979')</script>");
                    return;
                }
                if (toNumber > DateTime.Now.Year)
                {
                    ClientScript.RegisterStartupScript(GetType(), "Information", "<script>alert('Ending Year should be less than or equal to current year')</script>");
                    return;
                }
                Page.ClientScript.RegisterStartupScript(this.GetType(), "AgeAnalysis_window", String.Format("void(window.open('{0}','child_AgeAnalysis','menubar=0,toolbar=0,resizable=1,scrollbars=1'));", "Review.aspx?id=AgeAnalysis&FromYear=" + txtFromDt.Text.Trim() + "&ToYear=" + txtToDt.Text.Trim()), true);
            }
        }
        else if (id == "Commercial")
        {
            if (ddlRenewal.SelectedIndex == -1 || ddlRenewal.SelectedIndex == 0)
            {
                ClientScript.RegisterStartupScript(GetType(), "Information", "<script>alert('Select Commercialization Status')</script>");
                return;
            }
            if (txtFromDt.Text.Trim() != "" && txtToDt.Text.Trim() != "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Commercial_window", String.Format("void(window.open('{0}','child_commercial','menubar=0,toolbar=0,resizable=1,scrollbars=1'));", "Review.aspx?id=Commercial&FromDt=" + txtFromDt.Text.Trim() + "&ToDt=" + txtToDt.Text.Trim()+"&Status="+ ddlRenewal.SelectedItem.Text.Trim()), true);
            }
        }
        else if (id == "CommercialStatus")
        {
            if (ddlRenewal.SelectedIndex == -1 || ddlRenewal.SelectedIndex == 0)
            {
                ClientScript.RegisterStartupScript(GetType(), "Information", "<script>alert('Select Industry')</script>");
                return;
            }
            if (txtFromDt.Text.Trim() != "" && txtToDt.Text.Trim() != "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CommercialStatus_window", String.Format("void(window.open('{0}','child_commercialStatus','menubar=0,toolbar=0,resizable=1,scrollbars=1'));", "Review.aspx?id=CommercialStatus&FromDt=" + txtFromDt.Text.Trim() + "&ToDt=" + txtToDt.Text.Trim() + "&Status=" + ddlRenewal.SelectedItem.Text.Trim()), true);
            }
        }
    }
    protected void imgBtnClear_Click(object sender, ImageClickEventArgs e)
    {
        txtFromDt.Text = "";
        txtToDt.Text="";
    }
    protected void imgBtnGraph_Click(object sender, ImageClickEventArgs e)
    {
        if (txtFromDt.Text.Trim() != "" && txtToDt.Text.Trim() != "")
        {
            int fromNumber, toNumber;
            if (int.TryParse(txtFromDt.Text.Trim(), out fromNumber) && int.TryParse(txtToDt.Text.Trim(), out toNumber))
            {
                if (fromNumber > toNumber)
                {
                    ClientScript.RegisterStartupScript(GetType(), "Information", "<script>alert('First Number should be less than Second Number')</script>");
                    return;
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "Information", "<script>alert('Please Verify input values')</script>");
                return;
            }
            if (fromNumber < 1979)
            {
                ClientScript.RegisterStartupScript(GetType(), "Information", "<script>alert('Starting Year should be greater than or equal to 1979')</script>");
                return;
            }
            if (toNumber > DateTime.Now.Year)
            {
                ClientScript.RegisterStartupScript(GetType(), "Information", "<script>alert('Ending Year should be less than or equal to current year')</script>");
                return;
            }
            Page.ClientScript.RegisterStartupScript(this.GetType(), "AgeAnalysisG_window", String.Format("void(window.open('{0}','child_AgeAnalysisG','menubar=0,toolbar=0,resizable=1,scrollbars=1'));", "AgeGraph.aspx?FromYear=" + txtFromDt.Text.Trim() + "&ToYear=" + txtToDt.Text.Trim()), true);
        }
    }
}
