using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Globalization;

public partial class FindFiles : System.Web.UI.Page
{
    string id = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        id = Request.QueryString["id"];
        if (id == "Provisional")
        {
            this.Title= "Provisional Application List";
            trDate.Visible = true;
        }
        else if (id == "Examination")
        {
            this.Title = "Examination & Publication";
        }
        else if (id == "PCT_Filing")
        {
            this.Title="PCT Filing";
            trDate.Visible = true;
        }
        else if (id == "Foreign")
        {
            this.Title = "Foreign Filing Review (Excluding Direct International)";
        }
        if (!this.IsPostBack)
        {
            if (id == "Provisional")
            {
                tdTitle.InnerText = "Provisional Application List";
                ddlMonth.Items.Add("");
                ddlMonth.Items.Add(new ListItem("6 Months", "6"));
                ddlMonth.Items.Add(new ListItem("9 Months", "9"));
                ddlMonth.Items.Add(new ListItem("10 Months", "10"));
                ddlMonth.Items.Add(new ListItem("11 Months", "11"));
                ddlMonth.Items.Add(new ListItem("12 Months", "12"));
                txtDate.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Now);
            }
            else if (id == "Examination")
            {
                tdTitle.InnerText = "Examination & Publication";
                ddlMonth.Items.Add("");
                ddlMonth.Items.Add(new ListItem("40 Months", "40"));
            }
            else if (id == "PCT_Filing")
            {
                tdTitle.InnerText = "PCT Filing";
                ddlMonth.Items.Add("");
                ddlMonth.Items.Add(new ListItem("6 Months", "6"));
                ddlMonth.Items.Add(new ListItem("9 Months", "9"));
                ddlMonth.Items.Add(new ListItem("10 Months", "10"));
                ddlMonth.Items.Add(new ListItem("11 Months", "11"));
                ddlMonth.Items.Add(new ListItem("12 Months", "12"));
                txtDate.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Now);
            }
            else if (id == "Foreign")
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString);
                string sql = "select Country from IPCountry order by Country";
                ddlMonth.Width = 150;                
                using (SqlDataAdapter sda = new SqlDataAdapter(sql, con))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    ddlMonth.DataTextField = "Country";
                    ddlMonth.DataValueField = "Country";
                    ddlMonth.DataSource = dt;
                    ddlMonth.DataBind();
                    ddlMonth.Items.Insert(0, new ListItem("All", "All"));
                }
                tdTitle.InnerText = "Foreign Filing Review (Excluding Direct International)";
                tdDynamic4.InnerText = "Country";
                trDynamic1.Visible = true;
                tdDynamic1.InnerText = "International Patent Status ";
                ddlDynamic1.Width = 150;
                string sql1 = "select ItemList from ListItemMaster where Category like 'status' and [Grouping] is null order by ItemList";
                using (SqlDataAdapter sda = new SqlDataAdapter(sql1, con))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    ddlDynamic1.DataTextField = "ItemList";
                    ddlDynamic1.DataValueField = "ItemList";
                    ddlDynamic1.DataSource = dt;
                    ddlDynamic1.DataBind();
                    ddlDynamic1.Items.Insert(0,new ListItem("All","All"));
                }                
                trDynamic2.Visible = true;
                tdDynamic2.InnerText = "Commercialization Responsibility";
                string sql2 = "select Commercial from International group by Commercial";
                ddlDynamic2.Width = 150;
                using (SqlDataAdapter sda = new SqlDataAdapter(sql2, con))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    ddlDynamic2.DataTextField = "Commercial";
                    ddlDynamic2.DataValueField = "Commercial";
                    ddlDynamic2.DataSource = dt;
                    ddlDynamic2.DataBind();
                    ddlDynamic2.Items.Insert(0, new ListItem("All", "All"));
                }
                trDynamic3.Visible = true;                
            }

        }
    }
    protected void imgBtnSubmit_Click(object sender, ImageClickEventArgs e)
    {

        if (id == "Provisional")
        {
            if (ddlMonth.SelectedIndex != 0) 
            {
                try
                {
                    Response.Redirect("Review.aspx?id=ProvisionalToComplete&selMonth=" + ddlMonth.SelectedItem.Value + "&tillDate=" + txtDate.Text.Trim());
                }
                catch
                {
                    ClientScript.RegisterStartupScript(GetType(), "Information", "<script>alert('Verify To Date')</script>");
                }
            }
        }
        else if (id == "Examination")
        {
            if (ddlMonth.SelectedIndex != 0)
            {
                Response.Redirect("Review.aspx?id=Examination&selMonth=" + ddlMonth.SelectedItem.Value);
            }
        }
        else if (id == "PCT_Filing")
        {
            if (ddlMonth.SelectedIndex != 0)
            {
                Response.Redirect("Review.aspx?id=PCT_Filing&selMonth=" + ddlMonth.SelectedItem.Value + "&tillDate=" + txtDate.Text.Trim());
            }
        }
        else if (id == "Foreign")
        {
            if (string.IsNullOrEmpty(txtDynamic3.Text) && string.IsNullOrEmpty(txtDynamic4.Text))
            {
                ClientScript.RegisterStartupScript(GetType(), "Information", "<script>alert('Verify No of Months')</script>");
            }
            if (Convert.ToInt32(txtDynamic3.Text) > Convert.ToInt32(txtDynamic4.Text))
            {
                ClientScript.RegisterStartupScript(GetType(), "Information", "<script>alert('Verify No of Months')</script>");
            }
            Response.Redirect("Review.aspx?id=Foreign&Country=" + ddlMonth.SelectedItem.Value + "&Status=" + ddlDynamic1.SelectedItem.Value + "&Commercial=" + ddlDynamic2.SelectedItem.Value + "&FromMonth=" + txtDynamic3.Text.Trim() + "&ToMonth=" + txtDynamic4.Text.Trim());           
        }
    }
    protected void imgBtnClear_Click(object sender, ImageClickEventArgs e)
    {
        if (id != "Foreign") ddlMonth.Text = "";
        if (id == "Provisional")
        {
            txtDate.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Now);
        }
    }
}
