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
using System.Data.SqlClient;

public partial class ReportSelection1 : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    string id;
    protected void Page_Load(object sender, EventArgs e)
    {
        id= Request.QueryString["id"];
        if (id == "Inventor" || id == "Inventor2" || id == "IntlInventor")
        {
            this.Title = "INVENTOR WISE REPORT";
        }
        else if (id == "Attorney" || id == "IntlAttorney")
        {
            this.Title = "ATTORNEY WISE REPORT";
        }
        else if (id == "IntlCountry")
        {
            this.Title = "COUNTRY WISE REPORT";
        }
        else if (id == "PayInventor")
        {
            this.Title = "PAYMENT DETAILS OF INVENTOR";
        }

        if (!IsPostBack)
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
            if (id == "Inventor" || id == "Inventor2" || id == "IntlInventor" || id == "PayInventor")
            {
                if (id == "Inventor")
                    lblTitle.InnerText = "INDIAN PATANT DETAILS OF INVENTOR";
                else if (id == "IntlInventor")
                    lblTitle.InnerText = "INTERNATIONAL PATANT DETAILS OF INVENTOR";
                else if (id == "PayInventor")
                    lblTitle.InnerText = "PAYMENT DETAILS OF INVENTOR";
                lblListItem.InnerText = "Inventor Name";
                string sql = "select upper(Inventor1) as inventor from patdetails where Inventor1 <>'' and Inventor1 is not null group by Inventor1 order by Inventor1";
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = sql;
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                con.Open();
                SqlDataReader dr;
                dr = cmd.ExecuteReader();
                ddlListItem.DataTextField = "inventor";
                ddlListItem.DataValueField = "inventor";
                ddlListItem.DataSource = dr;
                ddlListItem.DataBind();
                con.Close();
                lblListItem2.Visible = false;
            }
            else if (id == "Attorney")
            {
                lblTitle.InnerText = "ATTORNEY WISE REPORT";
                lblListItem.InnerText = "Attorney Name";
                string sql = "select Attorney as Attorney from patdetails where Attorney <>'' and Attorney is not null group by Attorney order by Attorney";
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = sql;
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                con.Open();
                SqlDataReader dr;
                dr = cmd.ExecuteReader();
                ddlListItem.DataTextField = "Attorney";
                ddlListItem.DataValueField = "Attorney";
                ddlListItem.DataSource = dr;
                ddlListItem.DataBind();
                dr.Close();
                lblListItem2.Visible = true;
                ddlListItem2.Items.Add("All");
                ddlListItem2.Items.Add("Granted");
                ddlListItem2.Items.Add("Not Yet Granted");
                con.Close();
            }
            else if (id == "IntlAttorney")
            {
                lblTitle.InnerText = "ATTORNEY WISE REPORT";
                lblListItem.InnerText = "Attorney Name";
                string sql = "select distinct(Attorney) from international where Attorney is not null order by Attorney";
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = sql;
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                con.Open();
                SqlDataReader dr;
                dr = cmd.ExecuteReader();
                ddlListItem.DataTextField = "Attorney";
                ddlListItem.DataValueField = "Attorney";
                ddlListItem.DataSource = dr;
                ddlListItem.DataBind();
                con.Close();
                lblListItem2.Visible = true;
                ddlListItem2.Items.Add("All");
                ddlListItem2.Items.Add("Granted");
                ddlListItem2.Items.Add("Not Yet Granted");
            }
            else if (id == "IntlCountry")
            {
                lblTitle.InnerText = "COUNTRY WISE REPORT";
                lblListItem.InnerText = "Country Name";
                string sql = "select distinct(country) from international where country is not null order by country";
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = sql;
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                con.Open();
                SqlDataReader dr;
                dr = cmd.ExecuteReader();
                ddlListItem.DataTextField = "Country";
                ddlListItem.DataValueField = "Country";
                ddlListItem.DataSource = dr;
                ddlListItem.DataBind();
                con.Close();
                lblListItem2.Visible = false;
            }
        }
    }
    protected void imgBtnClear_Click(object sender, ImageClickEventArgs e)
    {
        
    }
    protected void imgBtnReport_Click(object sender, ImageClickEventArgs e)
    {
        if (id == "Inventor")
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Inventor_window", string.Format("void(window.open('{0}','child_Inventor','menubar=0,toolbar=0,resizable=1,scrollbars=1'));", "Review.aspx?id=Inventor&name=" + ddlListItem.SelectedItem.Text.Trim()), true);
        }
        else if (id == "Inventor2")
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Inventor2_window", string.Format("void(window.open('{0}','child_Inventor2','menubar=0,toolbar=0,resizable=1,scrollbars=1'));", "Review.aspx?id=Inventor2&name=" + ddlListItem.SelectedItem.Text.Trim()), true);
        }
        
        else if (id == "Attorney")
        {
            string Ampersand = ddlListItem.SelectedItem.Text.Trim();
            string AttorneyName=Ampersand.Replace("&", "~");
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Attorney_window", string.Format("void(window.open('{0}','child_Attorney','menubar=0,toolbar=0,resizable=1,scrollbars=1'));", "Review.aspx?id=Attorney&name=" + AttorneyName +"&Filedstatus=" + ddlListItem2.Text.Trim()), true);
        }
        else if (id == "IntlInventor")
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "IntlInventor_window", string.Format("void(window.open('{0}','child_IntlInventor','menubar=0,toolbar=0,resizable=1,scrollbars=1'));", "Review.aspx?id=IntlInventor&name=" + ddlListItem.SelectedItem.Text.Trim()), true);
        }
        else if (id == "IntlAttorney")
        {
            string Ampersand = ddlListItem.SelectedItem.Text.Trim();
            string AttorneyName = Ampersand.Replace("&", "~");
            Page.ClientScript.RegisterStartupScript(this.GetType(), "IntlAttorney_window", string.Format("void(window.open('{0}','child_IntlAttorney','menubar=0,toolbar=0,resizable=1,scrollbars=1'));", "Review.aspx?id=IntlAttorney&name=" + AttorneyName + "&Filedstatus=" + ddlListItem2.Text.Trim()), true);
        }
        else if (id == "IntlCountry")
        {
            string Ampersand = ddlListItem.SelectedItem.Text.Trim();
            string CountryName = Ampersand.Replace("&", "~");
            Page.ClientScript.RegisterStartupScript(this.GetType(), "IntlCountry_window", string.Format("void(window.open('{0}','child_IntlCountry','menubar=0,toolbar=0,resizable=1,scrollbars=1'));", "Review.aspx?id=IntlCountry&name=" + CountryName), true);
        }
        else if (id == "PayInventor")
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "PayInventor_window", string.Format("void(window.open('{0}','child_PayInventor','menubar=0,toolbar=0,resizable=1,scrollbars=1'));", "Review.aspx?id=PayInventor&name=" + ddlListItem.SelectedItem.Text.Trim()), true);
        }
        
    }
}
