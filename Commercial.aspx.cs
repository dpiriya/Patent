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

public partial class Commercial : System.Web.UI.Page
{
    SqlConnection cnp = new SqlConnection();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
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
            ddlFileNo.DataTextField = "fileno";
            ddlFileNo.DataValueField = "fileno";
            ddlFileNo.DataSource = dr;
            ddlFileNo.DataBind();
            ddlFileNo.Items.Insert(0, "");
            dr.Close();

            sql = "SELECT ITEMLIST FROM LISTITEMMASTER WHERE CATEGORY LIKE 'COMMERCIAL' ORDER BY ITEMLIST";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnp;
            cmd.CommandText = sql;
            dr = cmd.ExecuteReader();
            ddlCommercial.Items.Add("");
            while (dr.Read())
            {
                ddlCommercial.Items.Add(dr.GetString(0));
            }
            dr.Close();

            sql = "SELECT ITEMLIST FROM LISTITEMMASTER WHERE CATEGORY LIKE 'INDUSTRY' ORDER BY ITEMLIST";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnp;
            cmd.CommandText = sql;
            dr = cmd.ExecuteReader();
            ddlIndustry.Items.Add("");
            while (dr.Read())
            {
                ddlIndustry.Items.Add(dr.GetString(0));
            }
            dr.Close();
            
            sql = "SELECT ITEMLIST FROM LISTITEMMASTER WHERE CATEGORY LIKE 'COMMERCIALIZED' ORDER BY ITEMLIST";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnp;
            cmd.CommandText = sql;
            dr = cmd.ExecuteReader();
            ddlRoyal.DataTextField = "ItemList";
            ddlRoyal.DataValueField = "ItemList";
            ddlRoyal.DataSource = dr;
            ddlRoyal.DataBind();
            ddlRoyal.Items.Insert(0, new ListItem("", ""));
            dr.Close();

            sql = "SELECT ITEMLIST FROM LISTITEMMASTER WHERE CATEGORY LIKE 'ABSTRACT' ORDER BY SLNO";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnp;
            cmd.CommandText = sql;
            dr = cmd.ExecuteReader();
            ddlAbstract.Items.Add("");
            while (dr.Read())
            {
                ddlAbstract.Items.Add(dr.GetString(0));
            }
            dr.Close();
            cnp.Close();
            imgBtnInsert.Visible = (!User.IsInRole("View"));
        }
    }
    protected void imgBtnInsert_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
            cnp.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
            SqlCommand cmd = new SqlCommand();
            /*string sql = "update patdetails set commercial= case when @commercial='' then null else @commercial end,InventionNo=case when @InventionNo='' then null else @InventionNo end, " +
            "validity_from_dt= case when @validity_from_dt='' then null else convert(smalldatetime,@validity_from_dt,103) end,validity_to_dt= case when @validity_to_dt='' then null else convert(smalldatetime,@validity_to_dt,103) end, " +
            "industry1= case when @industry='' then null else @industry end,industry2= case when @industry2='' then null else @industry2 end,abstract= case when @abstract='' then null else @abstract end, " +
            "commercialized = case when @commercialized='' then null else @commercialized end, patentLicense = case when @patentLicense='' then null else @patentLicense end, TechTransNo = case when @TechTransNo='' then null else @TechTransNo end, " +
            "remarks= case when @remarks='' then null else @remarks end,IPC_Code= case when @IPC_Code='' then null else @IPC_Code end,DevelopmentStatus= case when @DevelopmentStatus='' then null else @DevelopmentStatus end,UserName=@UserName " +
            "where fileno ='" + ddlFileNo.SelectedItem.Text.Trim() + "'";*/
            cmd.CommandText = "upd_commercial";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cnp;
            cnp.Open();

            SqlParameter pm1 = new SqlParameter();
            pm1.ParameterName = "@commercial";
            pm1.SourceColumn = "commercial";
            if (txtCommercial.Text.Trim() == "") pm1.Value = DBNull.Value; else pm1.Value = txtCommercial.Text.Trim();
            pm1.DbType = DbType.String;
            pm1.Direction = ParameterDirection.Input;

            SqlParameter pm2 = new SqlParameter();
            pm2.ParameterName = "@InventionNo";
            pm2.SourceColumn = "InventionNo";
            if (txtInventionNo.Text.Trim()=="") pm2.Value = DBNull.Value; else pm2.Value = txtInventionNo.Text.Trim();
            pm2.DbType = DbType.String;
            pm2.Direction = ParameterDirection.Input;

            SqlParameter pm3 = new SqlParameter();
            pm3.ParameterName = "@validity_from_dt";
            pm3.SourceColumn = "validity_from_dt";
            if (txtVFromDt.Text.Trim() == "")
                pm3.Value = DBNull.Value;
            else
            {
                DateTime fromDt;
                if (DateTime.TryParse(txtVFromDt.Text.Trim(), out fromDt))
                    pm3.Value = txtVFromDt.Text.Trim();
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "Information", "<script>alert('Verify Validity From Date')</script>");
                    return;
                }
            }
            pm3.DbType = DbType.String;
            pm3.Direction = ParameterDirection.Input;

            SqlParameter pm4 = new SqlParameter();
            pm4.ParameterName = "@validity_to_dt";
            pm4.SourceColumn = "validity_to_dt";
            if (txtVToDt.Text.Trim() == "")
                pm4.Value = DBNull.Value;
            else
            {
                DateTime toDt;
                if (DateTime.TryParse(txtVToDt.Text.Trim(), out toDt))
                    pm4.Value = txtVToDt.Text.Trim();
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "Information", "<script>alert('Verify Validity To Date')</script>");
                    return;
                }
            }
            pm4.DbType = DbType.String;
            pm4.Direction = ParameterDirection.Input;

            SqlParameter pm5 = new SqlParameter();
            pm5.ParameterName = "@industry";
            pm5.SourceColumn = "industry1";
            if (ddlIndustry.SelectedItem.Text.Trim() == "") pm5.Value = DBNull.Value; else pm5.Value = ddlIndustry.SelectedItem.Text.Trim();
            pm5.DbType = DbType.String;
            pm5.Direction = ParameterDirection.Input;

            SqlParameter pm6 = new SqlParameter();
            pm6.ParameterName = "@industry2";
            pm6.SourceColumn = "industry2";
            if (txtIndustry2.Text.Trim() == "") pm6.Value = DBNull.Value; else pm6.Value = txtIndustry2.Text.Trim();
            pm6.DbType = DbType.String;
            pm6.Direction = ParameterDirection.Input;

            SqlParameter pm7 = new SqlParameter();
            pm7.ParameterName = "@abstract";
            pm7.SourceColumn = "abstract";
            if(ddlAbstract.SelectedItem.Text.Trim()=="") pm7.Value = DBNull.Value; else pm7.Value = ddlAbstract.SelectedItem.Text.Trim();
            pm7.DbType = DbType.String;
            pm7.Direction = ParameterDirection.Input;

            SqlParameter pm8 = new SqlParameter();
            pm8.ParameterName = "@commercialized";
            pm8.SourceColumn = "commercialized";
            if (ddlRoyal.SelectedItem.Text.Trim() == "") pm8.Value = DBNull.Value; else pm8.Value = ddlRoyal.SelectedItem.Text.Trim();
            pm8.DbType = DbType.String;
            pm8.Direction = ParameterDirection.Input;

            if (ddlRoyal.SelectedItem.Text.Trim() != "Yes")
            {
                txtLicense.Text = "";
                txtTechTransNo.Text = "";
            }

            SqlParameter pm9 = new SqlParameter();
            pm9.ParameterName = "@patentLicense";
            pm9.SourceColumn = "PatentLicense";
            if (txtLicense.Text.Trim() == "") pm9.Value = DBNull.Value; else pm9.Value = txtLicense.Text.Trim();
            pm9.DbType = DbType.String;
            pm9.Direction = ParameterDirection.Input;

            SqlParameter pm10 = new SqlParameter();
            pm10.ParameterName = "@TechTransNo";
            pm10.SourceColumn = "TechTransNo";
            if (txtTechTransNo.Text.Trim() == "") pm10.Value = DBNull.Value; else pm10.Value = txtTechTransNo.Text.Trim();
            pm10.DbType = DbType.String;
            pm10.Direction = ParameterDirection.Input;

            SqlParameter pm11 = new SqlParameter();
            pm11.ParameterName = "@remarks";
            pm11.SourceColumn = "remarks";
            if (txtRemark.Text.Trim() == "") pm11.Value = DBNull.Value; else pm11.Value = txtRemark.Text.Trim();
            pm11.DbType = DbType.String;
            pm11.Direction = ParameterDirection.Input;

            SqlParameter pm12 = new SqlParameter();
            pm12.ParameterName = "@IPC_Code";
            pm12.SourceColumn = "IPC_Code";
            if (txtIPC.Text.Trim() == "") pm12.Value = DBNull.Value; else pm12.Value = txtIPC.Text.Trim();
            pm12.DbType = DbType.String;
            pm12.Direction = ParameterDirection.Input;

            SqlParameter pm13 = new SqlParameter();
            pm13.ParameterName = "@DevelopmentStatus";
            pm13.SourceColumn = "DevelopmentStatus";
            if (txtDevelop.Text.Trim() == "") pm13.Value = DBNull.Value; else pm13.Value = txtDevelop.Text.Trim();
            pm13.DbType = DbType.String;
            pm13.Direction = ParameterDirection.Input;

            SqlParameter pm14 = new SqlParameter();
            pm14.ParameterName = "@UserName";
            pm14.SourceColumn = "UserName";
            pm14.Value = Membership.GetUser().UserName.ToString();
            pm14.DbType = DbType.String;
            pm14.Direction = ParameterDirection.Input;

            SqlParameter pm15 = new SqlParameter();
            pm15.ParameterName = "@fileNo";
            pm15.SourceColumn = "fileNo";
            pm15.Value = ddlFileNo.SelectedItem.Text.Trim();
            pm15.DbType = DbType.String;
            pm15.Direction = ParameterDirection.Input;

            SqlParameter pm16 = new SqlParameter();
            pm16.ParameterName = "@industry3";
            pm16.SourceColumn = "industry3";
            if (txtIndustry3.Text.Trim() == "") pm16.Value = DBNull.Value; else pm16.Value = txtIndustry3.Text.Trim();
            pm16.DbType = DbType.String;
            pm16.Direction = ParameterDirection.Input;

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

            cmd.ExecuteNonQuery();
            ClientScript.RegisterStartupScript(GetType(), "Success", "<script>alert('This Record successfully Updated')</script>");
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
        ddlFileNo.Enabled = true;
        ddlFileNo.Text = "";
        ddlCommercial.Text = "";
        txtCommercial.Text = "";
        txtInventionNo.Text = ""; 
        txtVFromDt.Text = "";
        txtVToDt.Text = "";
        ddlIndustry.Text = "";
        txtIndustry2.Text = "";
        txtIndustry3.Text = "";
        txtIPC.Text = "";
        ddlAbstract.Text = "";
        txtDevelop.Text = "";
        ddlRoyal.Text = "";
        txtLicense.Text = "";
        txtTechTransNo.Text = "";
        txtRemark.Text = "";        
    }
    protected void ddlFileNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlCommercial.Text = "";
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        cnp.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
        SqlCommand cmd = new SqlCommand();
        string sql = "select commercial,InventionNo,validity_from_dt,validity_to_dt,industry1,industry2,abstract,commercialized,patentLicense,techtransno,remarks," +
        "IPC_Code,DevelopmentStatus,filing_dt,industry3 from patdetails where fileno='" + ddlFileNo.SelectedItem.Text + "'";
        SqlDataReader dr;
        cmd.CommandType = CommandType.Text;
        cmd.Connection = cnp;
        cmd.CommandText = sql;
        cnp.Open();
        dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            if (!dr.IsDBNull(0))
            { txtCommercial.Text = dr.GetString(0); }
            else
            {
                ddlCommercial.Text = "IITM";
                txtCommercial.Text = "IITM";
            }
            if (!dr.IsDBNull(1))
            { txtInventionNo.Text = dr.GetString(1); }
            else { txtInventionNo.Text = ""; }
            if (!dr.IsDBNull(2))
            { 
                txtVFromDt.Text = dr.GetDateTime(2).ToShortDateString(); 
            }
            else if (!dr.IsDBNull(13))
            {
                txtVFromDt.Text = dr.GetDateTime(13).ToShortDateString(); 
            }
            else
            {
                txtVFromDt.Text ="";
            }
            if (!dr.IsDBNull(3))
            { txtVToDt.Text = dr.GetDateTime(3).ToShortDateString(); }
            else { txtVToDt.Text = ""; }
            if (!dr.IsDBNull(4))
            { ddlIndustry.Text = dr.GetString(4); }
            else { ddlIndustry.Text = ""; }
            if (!dr.IsDBNull(5))
            { txtIndustry2.Text = dr.GetString(5); }
            else { txtIndustry2.Text = ""; }
            if (!dr.IsDBNull(6))
            { ddlAbstract.Text = dr.GetString(6); }
            else { ddlAbstract.Text = ""; }
            if (!dr.IsDBNull(7))
            { ddlRoyal.Text = dr.GetString(7); }
            else { ddlRoyal.Text = ""; }
            if (!dr.IsDBNull(8))
            { txtLicense.Text = dr.GetString(8); }
            else { txtLicense.Text = ""; }
            if (!dr.IsDBNull(9))
            { txtTechTransNo.Text = dr.GetString(9); }
            else { txtTechTransNo.Text = ""; }
            if (!dr.IsDBNull(10))
            { txtRemark.Text = dr.GetString(10); }
            else { txtRemark.Text = ""; }
            if (!dr.IsDBNull(11))
            { txtIPC.Text = dr.GetString(11); }
            else { txtIPC.Text = ""; }
            if (!dr.IsDBNull(12))
            { txtDevelop.Text = dr.GetString(12); }
            else { txtDevelop.Text = ""; }
            if (!dr.IsDBNull(14))
            { txtIndustry3.Text = dr.GetString(14); }
            else { txtIndustry3.Text = ""; }           
        }
        dr.Close();
        cnp.Close();
        ddlFileNo.Enabled = false;
    }
    protected void ddlCommercial_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtCommercial.Text = ddlCommercial.SelectedItem.Text;
    }
}
