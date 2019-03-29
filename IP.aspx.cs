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

public partial class IP : System.Web.UI.Page
{
    SqlConnection cnp = new SqlConnection();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!User.IsInRole("Intern"))
        {
            if (!this.IsPostBack)
            {
                cnp.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                string sql = "select fileno from patdetails order by cast(fileno as int) desc";
                SqlDataReader dr;
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cnp;
                cmd.CommandText = sql;
                cnp.Open();
                dr = cmd.ExecuteReader();
                ddlFileNo.Items.Clear();
                ddlFileNo.Items.Add("");
                while (dr.Read())
                {
                    ddlFileNo.Items.Add(dr.GetString(0));
                }
                dr.Close();

                sql = "select iprname from ipr_category order by iprname";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cnp;
                cmd.CommandText = sql;
                dr = cmd.ExecuteReader();
                ddlType.Items.Clear();
                ddlType.DataValueField = "iprname";
                ddlType.DataTextField = "iprname";
                ddlType.DataSource = dr;
                ddlType.DataBind();
                ddlType.Items.Insert(0, "");
                dr.Close();

                sql = "select country from ipCountry where country is not null order by country";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cnp;
                cmd.CommandText = sql;
                dr = cmd.ExecuteReader();
                ddlCountry.Items.Clear();
                ddlCountry.DataValueField = "country";
                ddlCountry.DataTextField = "country";
                ddlCountry.DataSource = dr;
                ddlCountry.DataBind();
                ddlCountry.Items.Insert(0, "");
                dr.Close();

                sql = "select attorneyName from (select attorneyName from Attorney union select attorneyName from ipAttorney) as t order by t.attorneyName";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cnp;
                cmd.CommandText = sql;
                dr = cmd.ExecuteReader();
                ddlAttorney.Items.Clear();
                ddlAttorney.DataValueField = "attorneyname";
                ddlAttorney.DataTextField = "attorneyname";
                ddlAttorney.DataSource = dr;
                ddlAttorney.DataBind();
                ddlAttorney.Items.Insert(0, "");
                dr.Close();
                sql = "SELECT ITEMLIST FROM LISTITEMMASTER WHERE CATEGORY LIKE 'STATUS' AND (GROUPING IS NULL OR GROUPING  LIKE 'Non Patent' OR GROUPING  LIKE 'INTERNATIONAL') ORDER BY ITEMLIST";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cnp;
                cmd.CommandText = sql;
                dr = cmd.ExecuteReader();
                ddlStatus.Items.Clear();
                ddlStatus.Items.Add("");
                while (dr.Read())
                {
                    ddlStatus.Items.Add(dr.GetString(0));
                }
                dr.Close();
                cnp.Close();
                imgBtnInsert.Visible = (!User.IsInRole("View"));
            }
        }
        else
        {
            Server.Transfer("Unauthorized.aspx");
        }
    }
    
    protected void Clear()
    {
        ddlFileNo.Enabled = true;
        txtSFileNo.Enabled = true;
        ddlFileNo.Text = "";
        txtSFileNo.Text = "";
        txtRequestDt.Text = "";
        ddlCountry.Text = "";
        txtPartName.Text = "";
        txtPartRefNo.Text = "";
        ddlType.Text = "";
        ddlAttorney.Text = "";
        txtAppNo.Text = "";
        txtDtFiling.Text = "";
        txtPubNo.Text = "";
        txtPubDt.Text="";
        ddlStatus.Text = "";
        ddlSubStatus.Text="";
        txtPatNo.Text = "";
        txtPatDt.Text = "";
        txtRemark.Text = "";
    }
    protected void ddlFileNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        cnp.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
        int totalFileno;
        SqlCommand cmd1 = new SqlCommand();
        string sql1 = "select count(*) from INTERNATIONAL where fileno='" + ddlFileNo.SelectedItem.Value.Trim() + "'";
        cmd1.CommandText = sql1;
        cmd1.CommandType = CommandType.Text;
        cmd1.Connection = cnp;
        cnp.Open();
        totalFileno = Convert.ToInt32(cmd1.ExecuteScalar());
        if (totalFileno.Equals(null))
        {
            totalFileno = 0;
        }
        char[] subFile = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z' };
        string subFileno = ddlFileNo.SelectedItem.Value.Trim() + "-" + subFile[totalFileno];
        txtSFileNo.Text = subFileno;
        cnp.Close();
        ddlFileNo.Enabled = false;
        txtSFileNo.Enabled = false;
    }
    protected void imgBtnInsert_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            SqlCommand cmd = new SqlCommand();
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
            cnp.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
            cmd.CommandText = "spInsertIntl";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cnp;
            cnp.Open();
            SqlParameter pm1 = new SqlParameter();
            pm1.ParameterName = "@inputDt";
            pm1.SourceColumn = "InputDt";
            pm1.Value = DateTime.Now.ToShortDateString();
            pm1.DbType = DbType.String;
            pm1.Direction = ParameterDirection.Input;

            SqlParameter pm2 = new SqlParameter();
            pm2.ParameterName = "@fileno";
            pm2.SourceColumn = "fileno";
            pm2.Value = ddlFileNo.Text.Trim();
            pm2.DbType = DbType.String;
            pm2.Direction = ParameterDirection.Input;

            SqlParameter pm3 = new SqlParameter();
            pm3.ParameterName = "@subFileNo";
            pm3.SourceColumn = "subFileNo";
            pm3.Value = txtSFileNo.Text.Trim();
            pm3.DbType = DbType.String;
            pm3.Direction = ParameterDirection.Input;

            SqlParameter pm4 = new SqlParameter();
            pm4.ParameterName = "@requestDt";
            pm4.SourceColumn = "requestDt";
            if (txtRequestDt.Text.Trim() == "")
                pm4.Value = DBNull.Value;
            else
            {
                DateTime dtRequest;
                if (DateTime.TryParse(txtRequestDt.Text.Trim(), out dtRequest))
                    pm4.Value = txtRequestDt.Text.Trim();
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "Information", "<script>alert('Verify Request Date')</script>");
                    return;
                }
                
            }
            pm4.DbType = DbType.String;
            pm4.Direction = ParameterDirection.Input;

            SqlParameter pm5 = new SqlParameter();
            pm5.ParameterName = "@country";
            pm5.SourceColumn = "country";
            if (ddlCountry.Text.Trim() == "") pm5.Value = DBNull.Value; else pm5.Value = ddlCountry.Text.Trim();
            pm5.DbType = DbType.String;
            pm5.Direction = ParameterDirection.Input;

            SqlParameter pm6 = new SqlParameter();
            pm6.ParameterName = "@partner";
            pm6.SourceColumn = "partner";
            if (txtPartName.Text.Trim() == "") pm6.Value = DBNull.Value; else pm6.Value = txtPartName.Text.Trim();
            pm6.DbType = DbType.String;
            pm6.Direction = ParameterDirection.Input;

            SqlParameter pm7 = new SqlParameter();
            pm7.ParameterName = "@partnerNo";
            pm7.SourceColumn = "partnerNo";
            if (txtPartRefNo.Text.Trim() == "") pm7.Value = DBNull.Value; else pm7.Value = txtPartRefNo.Text.Trim();
            pm7.DbType = DbType.String;
            pm7.Direction = ParameterDirection.Input;

            SqlParameter pm8 = new SqlParameter();
            pm8.ParameterName = "@type";
            pm8.SourceColumn = "type";
            if (ddlType.SelectedItem.Text.Trim() == "") pm8.Value = DBNull.Value; else pm8.Value = ddlType.SelectedItem.Text.Trim();
            pm8.DbType = DbType.String;
            pm8.Direction = ParameterDirection.Input;

            SqlParameter pm9 = new SqlParameter();
            pm9.ParameterName = "@attorney";
            pm9.SourceColumn = "attorney";
            if (ddlAttorney.Text.Trim() == "") pm9.Value = DBNull.Value; else pm9.Value = ddlAttorney.Text.Trim();
            pm9.DbType = DbType.String;
            pm9.Direction = ParameterDirection.Input;

            SqlParameter pm10 = new SqlParameter();
            pm10.ParameterName = "@applicationNo";
            pm10.SourceColumn = "applicationno";
            if (txtAppNo.Text.Trim() == "") pm10.Value = DBNull.Value; else pm10.Value = txtAppNo.Text.Trim();
            pm10.DbType = DbType.String;
            pm10.Direction = ParameterDirection.Input;

            SqlParameter pm11 = new SqlParameter();
            pm11.ParameterName = "@filingDt";
            pm11.SourceColumn = "filingDt";
            if (txtDtFiling.Text.Trim() == "")
                pm11.Value = DBNull.Value;
            else
            {
                DateTime dtFiling;
                if (DateTime.TryParse(txtDtFiling.Text.Trim(), out dtFiling))
                    pm11.Value = txtDtFiling.Text.Trim();
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "Information", "<script>alert('Verify Filing Date')</script>");
                    return;
                }                
            }
            pm11.DbType = DbType.String;
            pm11.Direction = ParameterDirection.Input;

            SqlParameter pm12 = new SqlParameter();
            pm12.ParameterName = "@publicationNo";
            pm12.SourceColumn = "publicationNo";
            if (txtPubNo.Text.Trim() == "") pm12.Value = DBNull.Value; else pm12.Value = txtPubNo.Text.Trim();
            pm12.DbType = DbType.String;
            pm12.Direction = ParameterDirection.Input;

            SqlParameter pm13 = new SqlParameter();
            pm13.ParameterName = "@publicationDt";
            pm13.SourceColumn = "publicationDt";
            if (txtPubDt.Text.Trim() == "")
                pm13.Value = DBNull.Value;
            else
            {
                DateTime publishDt;
                if (DateTime.TryParse(txtPubDt.Text.Trim(), out publishDt))
                    pm13.Value = txtPubDt.Text.Trim();
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "Information", "<script>alert('Verify Publication Date')</script>");
                    return;
                }
            }
            pm13.DbType = DbType.String;
            pm13.Direction = ParameterDirection.Input;

            SqlParameter pm14 = new SqlParameter();
            pm14.ParameterName = "@status";
            pm14.SourceColumn = "status";
            if (ddlStatus.Text.Trim() == "") pm14.Value = DBNull.Value; else pm14.Value = ddlStatus.Text.Trim();
            pm14.DbType = DbType.String;
            pm14.Direction = ParameterDirection.Input;

            SqlParameter pm15 = new SqlParameter();
            pm15.ParameterName = "@subStatus";
            pm15.SourceColumn = "subStatus";
            if (ddlSubStatus.Text.Trim() == "") pm15.Value = DBNull.Value; else pm15.Value = ddlSubStatus.Text.Trim();
            pm15.DbType = DbType.String;
            pm15.Direction = ParameterDirection.Input;

            SqlParameter pm16 = new SqlParameter();
            pm16.ParameterName = "@patentNo";
            pm16.SourceColumn = "patentNo";
            if (txtPatNo.Text.Trim() == "") pm16.Value = DBNull.Value; else pm16.Value = txtPatNo.Text.Trim();
            pm16.DbType = DbType.String;
            pm16.Direction = ParameterDirection.Input;

            SqlParameter pm17 = new SqlParameter();
            pm17.ParameterName = "@patentDt";
            pm17.SourceColumn = "patentDt";
            if (txtPatDt.Text.Trim() == "")
                pm17.Value = DBNull.Value;
            else
            {
                DateTime grandDt;
                if (DateTime.TryParse(txtPatDt.Text.Trim(), out grandDt))
                    pm17.Value = txtPatDt.Text.Trim();
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "Information", "<script>alert('Verify Patent Date')</script>");
                    return;
                }
            }
            pm17.DbType = DbType.String;
            pm17.Direction = ParameterDirection.Input;

            SqlParameter pm18 = new SqlParameter();
            pm18.ParameterName = "@remark";
            pm18.SourceColumn = "remark";
            if (txtRemark.Text.Trim() == "") pm18.Value = DBNull.Value; else pm18.Value = txtRemark.Text.Trim();
            pm18.DbType = DbType.String;
            pm18.Direction = ParameterDirection.Input;

            SqlParameter pm19 = new SqlParameter();
            pm19.ParameterName = "@UserName";
            pm19.SourceColumn = "UserName";
            pm19.Value = Membership.GetUser().UserName.ToString();
            pm19.DbType = DbType.String;
            pm19.Direction = ParameterDirection.Input;

            cmd.Parameters.Add(pm1);
            cmd.Parameters.Add(pm2);
            cmd.Parameters.Add(pm3);
            cmd.Parameters.Add(pm4);
            cmd.Parameters.Add(pm5);
            cmd.Parameters.Add(pm6);
            cmd.Parameters.Add(pm7);
            cmd.Parameters.Add(pm8);
            cmd.Parameters.Add(pm9);
            cmd.Parameters.Add(pm10);
            cmd.Parameters.Add(pm11);
            cmd.Parameters.Add(pm12);
            cmd.Parameters.Add(pm13);
            cmd.Parameters.Add(pm14);
            cmd.Parameters.Add(pm15);
            cmd.Parameters.Add(pm16);
            cmd.Parameters.Add(pm17);
            cmd.Parameters.Add(pm18);
            cmd.Parameters.Add(pm19);
            cmd.ExecuteNonQuery();
            ClientScript.RegisterStartupScript(GetType(), "Success", "<script>alert('This Record successfully Added')</script>");
            cnp.Close();
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(GetType(), "Error", "<script>alert('" + ex.Message.ToString() + "')</script>");
            cnp.Close();
            return;
        }
        imgBtnClear_Click(sender, e);
    }
    protected void imgBtnClear_Click(object sender, ImageClickEventArgs e)
    {
        Clear();
    }
    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
        con.Open();
        SqlDataReader dr;
        SqlCommand cmd = new SqlCommand();
        string sql = "SELECT ITEMLIST FROM LISTITEMMASTER WHERE CATEGORY LIKE 'SUB STATUS' AND GROUPING LIKE '" + ddlStatus.SelectedItem.Text.Trim() + "'";
        cmd.CommandType = CommandType.Text;
        cmd.Connection = con;
        cmd.CommandText = sql;
        dr = cmd.ExecuteReader();
        ddlSubStatus.Items.Clear();
        ddlSubStatus.Items.Add("");
        while (dr.Read())
        {
            ddlSubStatus.Items.Add(dr.GetString(0));
        }
        dr.Close();
        con.Close();
    }
}
