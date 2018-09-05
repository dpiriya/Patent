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

public partial class IP_Modify : System.Web.UI.Page
{
    SqlConnection cnp = new SqlConnection();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            cnp.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
            SqlCommand cmd = new SqlCommand();
            string sql = "SELECT FILENO FROM INTERNATIONAL GROUP BY FILENO ORDER BY CAST(FILENO AS INT) DESC";
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
            sql = "select attorneyname from (select attorneyname from attorney union select attorneyname from ipattorney) as t  order by t.attorneyname";
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

    protected void Clear()
    {
        ddlFileNo.Enabled = true;
        ddlFileNo.Text = "";
        ddlSubFileNo.Enabled = true;
        ddlSubFileNo.Items.Clear();
        txtRequestDt.Text = "";
        ddlCountry.Text = "";
        txtPartName.Text = "";
        txtPartRefNo.Text = "";
        ddlType.Text = "";
        ddlAttorney.Text = "";
        txtAppNo.Text = "";
        txtDtFiling.Text = "";
        txtPubNo.Text = "";
        txtPubDt.Text = "";
        ddlStatus.Text = "";
        ddlSubStatus.Text = "";
        txtPatNo.Text = "";
        txtPatDt.Text = "";
        txtRemark.Text = "";
        ddlSubStatus.Items.Clear();
    }

    protected void ddlFileNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlSubFileNo.Items.Clear();
        cnp.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
        SqlCommand cmd1 = new SqlCommand();
        string sql1 = "select subFileNo from INTERNATIONAL where fileno='" + ddlFileNo.SelectedItem.Value.Trim() + "'";
        cmd1.CommandText = sql1;
        cmd1.CommandType = CommandType.Text;
        cmd1.Connection = cnp;
        cnp.Open();
        SqlDataReader sdr;
        sdr = cmd1.ExecuteReader();
        ddlSubFileNo.Items.Add("");
        while (sdr.Read())
        {
            ddlSubFileNo.Items.Add(new ListItem(sdr.GetString(0), sdr.GetString(0)));
        }
        ddlSubFileNo.SelectedIndex = 0;
        cnp.Close();
        ddlFileNo.Enabled = false;
    }
    protected void ddlSubFileNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        cnp.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
        SqlCommand cmd1 = new SqlCommand();
        string sql1 = "select RequestDt,Country,Partner,PartnerNo,Type,Attorney,ApplicationNo,FilingDt,PublicationNo,PublicationDt,Status,SubStatus,PatentNo,PatentDt,Remark from INTERNATIONAL where fileno='" + ddlFileNo.SelectedItem.Value.Trim() + "' and subFileNo='" + ddlSubFileNo.SelectedItem.Text.Trim() + "'" ;
        cmd1.CommandText = sql1;
        cmd1.CommandType = CommandType.Text;
        cmd1.Connection = cnp;
        cnp.Open();
        SqlDataReader sdr;
        sdr = cmd1.ExecuteReader();
        if (sdr.Read())
        {
            if (!sdr.IsDBNull(0))
            { txtRequestDt.Text = sdr.GetDateTime(0).ToShortDateString(); }
            else { txtRequestDt.Text = ""; }
            if (!sdr.IsDBNull(1))
            { ddlCountry.Text = sdr.GetString(1).ToString().Trim(); }
            else { ddlCountry.Text = ""; }
            if (!sdr.IsDBNull(2))
            { txtPartName.Text = sdr.GetString(2).ToString(); }
            else { txtPartName.Text = ""; }
            if (!sdr.IsDBNull(3))
            { txtPartRefNo.Text = sdr.GetString(3).ToString(); }
            else { txtPartRefNo.Text = ""; }
            if (!sdr.IsDBNull(4))
            { ddlType.Text = Convert.ToString(sdr.GetString(4)); }
            else { ddlType.Text = ""; }
            if (!sdr.IsDBNull(5))
            { ddlAttorney.Text = sdr.GetString(5).ToString(); }
            else { ddlAttorney.Text = ""; }
            if (!sdr.IsDBNull(6))
            { txtAppNo.Text = sdr.GetString(6).ToString(); }
            else { txtAppNo.Text = ""; }
            if (!sdr.IsDBNull(7))
            { txtDtFiling.Text = sdr.GetDateTime(7).ToShortDateString(); }
            else { txtDtFiling.Text = ""; }
            if (!sdr.IsDBNull(8))
            { txtPubNo.Text = sdr.GetString(8).ToString(); }
            else { txtPubNo.Text = ""; }
            if (!sdr.IsDBNull(9))
            { txtPubDt.Text = sdr.GetDateTime(9).ToShortDateString(); }
            else { txtPubDt.Text = ""; }
            if (!sdr.IsDBNull(10))
            { 
                ddlStatus.Text = sdr.GetString(10).ToString(); 
                ddlStatus_SelectedIndexChanged(sender, e);
            }
            else 
            { 
                ddlStatus.Text = ""; 
            }
            if (!sdr.IsDBNull(11))
            { ddlSubStatus.Text = sdr.GetString(11).ToString(); }
            else { ddlSubStatus.Text = ""; }
            if (!sdr.IsDBNull(12))
            { txtPatNo.Text = sdr.GetString(12).ToString(); }
            else { txtPatNo.Text = ""; }
            if (!sdr.IsDBNull(13))
            { txtPatDt.Text = sdr.GetDateTime(13).ToShortDateString(); }
            else { txtPatDt.Text = ""; }
            if (!sdr.IsDBNull(14))
            { txtRemark.Text = sdr.GetString(14).ToString(); }
            else { txtRemark.Text = ""; }
            cnp.Close();
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "Information", "<script>alert('Sub File No. Can't be empty')</script>");
            cnp.Close();
        }
        ddlSubFileNo.Enabled = false;
    }
    protected void imgBtnClear_Click(object sender, ImageClickEventArgs e)
    {
        Clear();
    }
    protected void imgBtnInsert_Click(object sender, ImageClickEventArgs e)
    {
        if ((ddlFileNo.SelectedIndex == -1) || (ddlSubFileNo.SelectedIndex == 0))
        {
            Clear();
            return;
        }

        try
        {
            SqlCommand cmd = new SqlCommand();
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
            cnp.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
            cmd.CommandText = "spUpdateIntl";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cnp;
            cnp.Open();
            
            SqlParameter pm1 = new SqlParameter();
            pm1.ParameterName = "@requestDt";
            pm1.SourceColumn = "requestDt";
            if (txtRequestDt.Text.Trim() == "")
                pm1.Value = DBNull.Value;
            else
            {
                DateTime mRequestDt;
                if (DateTime.TryParse(txtRequestDt.Text.Trim(), out mRequestDt))
                    pm1.Value = txtRequestDt.Text.Trim();
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "Information", "<script>alert('Verify Request Date')</script>");
                    return;
                }
            }
            pm1.DbType = DbType.String;
            pm1.Direction = ParameterDirection.Input;

            SqlParameter pm2 = new SqlParameter();
            pm2.ParameterName = "@country";
            pm2.SourceColumn = "country";
            if (ddlCountry.Text.Trim() == "") pm2.Value = DBNull.Value; else pm2.Value = ddlCountry.Text.Trim();
            pm2.DbType = DbType.String;
            pm2.Direction = ParameterDirection.Input;

            SqlParameter pm3 = new SqlParameter();
            pm3.ParameterName = "@partner";
            pm3.SourceColumn = "partner";
            if (txtPartName.Text.Trim() == "") pm3.Value = DBNull.Value; else pm3.Value = txtPartName.Text.Trim();
            pm3.DbType = DbType.String;
            pm3.Direction = ParameterDirection.Input;

            SqlParameter pm4 = new SqlParameter();
            pm4.ParameterName = "@partnerNo";
            pm4.SourceColumn = "partnerNo";
            if (txtPartRefNo.Text.Trim() == "") pm4.Value = DBNull.Value; else pm4.Value = txtPartRefNo.Text.Trim();
            pm4.DbType = DbType.String;
            pm4.Direction = ParameterDirection.Input;

            SqlParameter pm5 = new SqlParameter();
            pm5.ParameterName = "@type";
            pm5.SourceColumn = "type";
            if (ddlType.SelectedItem.Text.Trim() == "") pm5.Value = DBNull.Value; else pm5.Value = ddlType.SelectedItem.Text.Trim();
            pm5.DbType = DbType.String;
            pm5.Direction = ParameterDirection.Input;

            SqlParameter pm6 = new SqlParameter();
            pm6.ParameterName = "@attorney";
            pm6.SourceColumn = "attorney";
            if (ddlAttorney.Text.Trim() == "") pm6.Value = DBNull.Value; else pm6.Value = ddlAttorney.Text.Trim();
            pm6.DbType = DbType.String;
            pm6.Direction = ParameterDirection.Input;

            SqlParameter pm7 = new SqlParameter();
            pm7.ParameterName = "@applicationNo";
            pm7.SourceColumn = "applicationno";
            if (txtAppNo.Text.Trim() == "") pm7.Value = DBNull.Value; else pm7.Value = txtAppNo.Text.Trim();
            pm7.DbType = DbType.String;
            pm7.Direction = ParameterDirection.Input;

            SqlParameter pm8 = new SqlParameter();
            pm8.ParameterName = "@filingDt";
            pm8.SourceColumn = "filingDt";
            if (txtDtFiling.Text.Trim() == "")
                pm8.Value = DBNull.Value;
            else
            {
                DateTime DtFiling;
                if (DateTime.TryParse(txtDtFiling.Text.Trim(), out DtFiling))
                {
                    pm8.Value = txtDtFiling.Text.Trim();
                    try
                    {
                        SqlCommand cmd1 = new SqlCommand("select count(*) from RenewalFollowup where FileNo = '" + ddlSubFileNo.Text.Trim() + "' and phase = 'Prosecution'", cnp);
                        int check =Convert.ToInt16(cmd1.ExecuteScalar());
                        if(check==0)
                        {
                            SqlCommand cmd2=new SqlCommand("insert into RenewalFollowup(FileNo, SlNo, Description, DueDate, Responsibility, Phase)(select '" + ddlSubFileNo.Text.Trim() + "',[s no], description,[Due Date], responsibility, 'Prosecution' from tbl_mst_prosecution where Indian_Foreign = 'US')", cnp);
                            int inserted=Convert.ToInt16(cmd2.ExecuteNonQuery());                            
                            DateTime duedate_pa = Convert.ToDateTime(txtDtFiling.Text).AddMonths(18);
                            DateTime duedate_fer = Convert.ToDateTime(txtDtFiling.Text).AddMonths(24);
                            DateTime duedate_oa1 = Convert.ToDateTime(txtDtFiling.Text).AddMonths(30);
                            DateTime duedate_oa2 = Convert.ToDateTime(txtDtFiling.Text).AddMonths(36);
                            DateTime duedate_oa3 = Convert.ToDateTime(txtDtFiling.Text).AddMonths(36);
                            DateTime duedate_pregrant = Convert.ToDateTime(txtDtFiling.Text).AddMonths(36);
                            DateTime duedate_grant = Convert.ToDateTime(txtDtFiling.Text).AddMonths(36);
                            //DateTime duedate_pogrant = Convert.ToDateTime(txtDtFiling.Text).AddMonths(36);
                            SqlCommand cmd3 = new SqlCommand("update RenewalFollowup set DueDate= case SlNo when 1 then convert(date,'" + duedate_pa + "',103) when 2 then convert(date,'" + duedate_fer + "',103) when 3 then convert(date,'" + duedate_oa1 + "',103) when 4 then convert(date,'" + duedate_oa2 + "',103) when 5 then convert(date,'" + duedate_oa3 + "',103) when 6 then convert(date,'" + duedate_pregrant + "',103) when 7 then convert(date,'" + duedate_grant + "',103) end where fileno='" + ddlSubFileNo.Text.Trim() + "' and phase='prosecution'", cnp);
                            int updated_rows = Convert.ToInt16(cmd3.ExecuteNonQuery());
                            if (inserted==8 && updated_rows == 8)
                            {
                                // ClientScript.RegisterStartupScript(GetType(), "Information", "<script>alert('Failed to update renewal schedule. Kindly update it through P&M action plan')</script>", true);
                                pminfo.Text = "Renewal followup for Fileno '" + ddlSubFileNo.Text + "' is successfully updated with Duedates";
                            }   
                            else if(inserted==8 && updated_rows!=8)
                            {
                                pminfo.Text = "Renewal followup for Fileno '" + ddlSubFileNo.Text + "' is successfully updated. But unable to update Duedates";
                            }
                            else 
                            {
                                pminfo.Text = "Failed to update renewal schedule. Kindly update it through P&M action plan";
                            }
                    }
                        else
                        {
                            pminfo.Text = "Renewal followup for Fileno '" + ddlSubFileNo.Text + "' is already exists";
                        }
                    }
                    catch(Exception ex)
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Information", "<script>alert('"+ex+"')</script>");
                    }
                }
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "Information", "<script>alert('Verify Filing Date')</script>");
                    return;
                }
            }
            pm8.DbType = DbType.String;
            pm8.Direction = ParameterDirection.Input;

            SqlParameter pm9 = new SqlParameter();
            pm9.ParameterName = "@publicationNo";
            pm9.SourceColumn = "publicationNo";
            if (txtPubNo.Text.Trim() == "") pm9.Value = DBNull.Value; else pm9.Value = txtPubNo.Text.Trim();
            pm9.DbType = DbType.String;
            pm9.Direction = ParameterDirection.Input;

            SqlParameter pm10 = new SqlParameter();
            pm10.ParameterName = "@publicationDt";
            pm10.SourceColumn = "publicationDt";
            if (txtPubDt.Text.Trim() == "")
                pm10.Value = DBNull.Value;
            else
            {
                DateTime DtPub;
                if (DateTime.TryParse(txtPubDt.Text.Trim(), out DtPub))
                    pm10.Value = txtPubDt.Text.Trim();
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "Information", "<script>alert('Verify Publication Date')</script>");
                    return;
                }
            }
            pm10.DbType = DbType.String;
            pm10.Direction = ParameterDirection.Input;

            SqlParameter pm11 = new SqlParameter();
            pm11.ParameterName = "@status";
            pm11.SourceColumn = "status";
            if (ddlStatus.Text.Trim() == "") pm11.Value = DBNull.Value; else pm11.Value = ddlStatus.Text.Trim();
            pm11.DbType = DbType.String;
            pm11.Direction = ParameterDirection.Input;

            SqlParameter pm12 = new SqlParameter();
            pm12.ParameterName = "@subStatus";
            pm12.SourceColumn = "subStatus";
            if (ddlSubStatus.Text.Trim() == "") pm12.Value = DBNull.Value; else pm12.Value = ddlSubStatus.Text.Trim();
            pm12.DbType = DbType.String;
            pm12.Direction = ParameterDirection.Input;

            SqlParameter pm13 = new SqlParameter();
            pm13.ParameterName = "@patentNo";
            pm13.SourceColumn = "patentNo";
            if (txtPatNo.Text.Trim() == "") pm13.Value = DBNull.Value; else pm13.Value = txtPatNo.Text.Trim();
            pm13.DbType = DbType.String;
            pm13.Direction = ParameterDirection.Input;

            SqlParameter pm14 = new SqlParameter();
            pm14.ParameterName = "@patentDt";
            pm14.SourceColumn = "patentDt";
            if (txtPatDt.Text.Trim() == "")
                pm14.Value = DBNull.Value;
            else
            {
                DateTime dtPat;
                if (DateTime.TryParse(txtPatDt.Text.Trim(), out dtPat))
                    pm14.Value = txtPatDt.Text.Trim();
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "Information", "<script>alert('Verify Patent Date')</script>");
                    return;
                }
            }
            pm14.DbType = DbType.String;
            pm14.Direction = ParameterDirection.Input;

            SqlParameter pm15 = new SqlParameter();
            pm15.ParameterName = "@remark";
            pm15.SourceColumn = "remark";
            if (txtRemark.Text.Trim() == "") pm15.Value = DBNull.Value; else pm15.Value = txtRemark.Text.Trim();
            pm15.DbType = DbType.String;
            pm15.Direction = ParameterDirection.Input;

            SqlParameter pm16 = new SqlParameter();
            pm16.ParameterName = "@UserName";
            pm16.SourceColumn = "UserName";
            pm16.Value = Membership.GetUser().UserName.ToString();
            pm16.DbType = DbType.String;
            pm16.Direction = ParameterDirection.Input;

            SqlParameter pm17 = new SqlParameter();
            pm17.ParameterName = "@fileno";
            pm17.SourceColumn = "fileno";
            pm17.Value = ddlFileNo.SelectedItem.Text.Trim();
            pm17.DbType = DbType.String;
            pm17.Direction = ParameterDirection.Input;

            SqlParameter pm18 = new SqlParameter();
            pm18.ParameterName = "@subFileNo";
            pm18.SourceColumn = "subFileNo";
            pm18.Value = ddlSubFileNo.SelectedItem.Text.Trim();
            pm18.DbType = DbType.String;
            pm18.Direction = ParameterDirection.Input;

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
            cmd.ExecuteNonQuery();
            ClientScript.RegisterStartupScript(GetType(), "Success", "<script>alert('This Record successfully Modified')</script>");
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
