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

public partial class IndianPatent : System.Web.UI.Page
{
    SqlConnection cnp = new SqlConnection();
    //string PreviousStatus = "";
    protected void Page_Load(object sender, EventArgs e)
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
            ddlFileNo.DataTextField = "fileno";
            ddlFileNo.DataValueField = "fileno";
            ddlFileNo.DataSource = dr;
            ddlFileNo.DataBind();
            ddlFileNo.Items.Insert(0, "");
            dr.Close();

            sql = "select attorneyname from attorney order by attorneyname";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnp;
            cmd.CommandText = sql;
            dr = cmd.ExecuteReader();
            ddlAttorney.Items.Clear();
            ddlAttorney.Items.Add("");
            while (dr.Read())
            {
                ddlAttorney.Items.Add(dr.GetString(0));
            }
            dr.Close();

            sql = "SELECT ITEMLIST FROM LISTITEMMASTER WHERE CATEGORY LIKE 'STATUS' ORDER BY ITEMLIST";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnp;
            cmd.CommandText = sql;
            dr = cmd.ExecuteReader();
            ddlStatus.Items.Clear();
            ddlStatus.DataTextField = "ITEMLIST";
            ddlStatus.DataSource = dr;
            ddlStatus.DataBind();
            ddlStatus.Items.Insert(0, "");
            dr.Close();


            /* sql = "SELECT ITEMLIST FROM LISTITEMMASTER WHERE CATEGORY LIKE 'SUB STATUS' ORDER BY GROUPING";
             cmd.CommandType = CommandType.Text;
             cmd.Connection = cnp;
             cmd.CommandText = sql;
             dr = cmd.ExecuteReader();
             ddlSubStatus.Items.Clear();
             ddlSubStatus.Items.Add("");

             while (dr.Read())
             {
                 ddlSubStatus.Items.Add(dr.GetString(0));
             }
             dr.Close();*/

            ddlExam.Items.Add("");
            ddlExam.Items.Add(new ListItem("Yes", "Yes"));
            ddlExam.Items.Add(new ListItem("No", "No"));

            sql = "select Country from IPCountry order by Country";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnp;
            cmd.CommandText = sql;
            dr = cmd.ExecuteReader();
            ddlPSearch.Items.Clear();
            ddlPSearch.Items.Add("");
            while (dr.Read())
            {
                ddlPSearch.Items.Add(dr.GetString(0));
            }
            dr.Close();
            //ddlPSearch.Items.Add("");
            //ddlPSearch.Items.Add(new ListItem("TSI1 - Tech search - Int1", "TSI1"));
            //ddlPSearch.Items.Add(new ListItem("TSI2 - Tech search - Int2", "TSI2"));
            //ddlPSearch.Items.Add(new ListItem("TSI3 - Tech search - Int3", "TSI3"));
            //ddlPSearch.Items.Add(new ListItem("TCEKO - KPMG Phase 0", "TCEK0"));
            //ddlPSearch.Items.Add(new ListItem("TCEK1 - KPMG Phase 1", "TCEK1"));
            //ddlPSearch.Items.Add(new ListItem("TCEK2 - KPMG Phase 2", "TCEK2"));
            //ddlPSearch.Items.Add(new ListItem("TCEK3 - KPMG Phase 3", "TCEK3"));
            //ddlPSearch.Items.Add(new ListItem("TCEKP - KPMG TCE Pre-PCT", "TCEKP"));
            //ddlPSearch.Items.Add(new ListItem("TSAP - Tech search - Attroney(Provisional)", "TSAP"));
            //ddlPSearch.Items.Add(new ListItem("TSAC - Tech search - Attroney(Complete)", "TSAC"));
            //ddlPSearch.Items.Add(new ListItem("TSAP + TSI2", "TSAP + TSI2"));
            //ddlPSearch.Items.Add(new ListItem("TSAC + TCEK0", "TSAC + TCEK0"));
            //ddlPSearch.Items.Add(new ListItem("BCIL TCE", "TCEBL"));
            //ddlPSearch.Items.Add(new ListItem("NRDC TCE", "TCEND"));
            //ddlPSearch.Items.Add(new ListItem("IP Matrix TCE", "TCEIP"));

            //ddlPSearch.Items.Add(new ListItem("TSI2+TSI3", "TSI2+TSI3"));
            //ddlPSearch.Items.Add(new ListItem("TSI2+TSAP+TSI3", "TSI2+TSAP+TSI3"));
            //ddlPSearch.Items.Add(new ListItem("TSI2+TSAC+TSI3", "TSI2+TSAC+TSI3"));

            //ddlPSearch.Items.Add(new ListItem("TSI3+TCEKP", "TSI3+TCEKP"));
            //ddlPSearch.Items.Add(new ListItem("TSAP+TSI3+TCEKP", "TSAP+TSI3+TCEKP"));
            //ddlPSearch.Items.Add(new ListItem("TSI2+TSAP+TSI3+TCEKP", "TSI2+TSAP+TSI3+TCEKP"));
            //ddlPSearch.Items.Add(new ListItem("TSI2+TSI3+TCEKP", "TSI2+TSI3+TCEKP"));

            //ddlPSearch.Items.Add(new ListItem("TSI3+BCIL TCE", "TSI3+BCIL TCE"));
            //ddlPSearch.Items.Add(new ListItem("TSAP+TSI3+BCIL TCE", "TSAP+TSI3+BCIL TCE"));
            //ddlPSearch.Items.Add(new ListItem("TSI2+TSAP+TSI3+BCIL TCE", "TSI2+TSAP+TSI3+BCIL TCE"));
            //ddlPSearch.Items.Add(new ListItem("TSI2+TSI3+BCIL TCE", "TSI2+TSI3+BCIL TCE"));

            //ddlPSearch.Items.Add(new ListItem("TSI3+NRDC TCE", "TSI3+NRDC TCE"));
            //ddlPSearch.Items.Add(new ListItem("TSAP+TSI3+NRDC TCE", "TSAP+TSI3+NRDC TCE"));
            //ddlPSearch.Items.Add(new ListItem("TSI2+TSAP+TSI3+NRDC TCE", "TSI2+TSAP+TSI3+NRDC TCE"));
            //ddlPSearch.Items.Add(new ListItem("TSI2+TSI3+NRDC TCE", "TSI2+TSI3+NRDC TCE"));

            //ddlPSearch.Items.Add(new ListItem("TSA", "TSA"));

            //ddlPSearch.Items.Add(new ListItem("No", "No"));

            cnp.Close();
            imgBtnInsert.Visible = (!User.IsInRole("View"));
        }
    }

    protected void Clear()
    {
        ddlFileNo.Enabled = true;
        ddlFileNo.Text = "";
        ddlAttorney.Text = "";
        txtAppNo.Text = "";
        txtDtFiling.Text = "";
        txtDtFiling.Enabled = true;
        txtCompleteDt.Text = "";
        ddlPSearch.Text = "";
        ddlExam.Text = "";
        txtExamDt.Text = "";
        txtPub.Text = "";
        txtPubDt.Text = "";
        ddlStatus.Text = "";
        txtSubStatus.Text = "";
        ddlSubStatus.Items.Clear();
        txtPatNo.Text = "";
        txtPatDt.Text = "";
        hfType.Value = "";
        hfPreviousStatus.Value = "";
    }

    protected void ddlFileNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlAttorney.Text = "";
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        cnp.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
        SqlCommand cmd = new SqlCommand();
        string sql = "select Attorney,applcn_no,filing_dt,publication,pub_dt,status,sub_status,pat_no,pat_dt," +
        "examination,exam_dt,PatentSearch,type,CompleteDt,Patent_searchDt from patdetails where fileno='" + ddlFileNo.SelectedItem.Text + "'";
        SqlDataReader dr;
        cmd.CommandType = CommandType.Text;
        cmd.Connection = cnp;
        cmd.CommandText = sql;
        cnp.Open();
        dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            if (!dr.IsDBNull(0))
            { ddlAttorney.Text = dr.GetString(0); }
            else { ddlAttorney.Text = ""; }
            if (!dr.IsDBNull(1))
            { txtAppNo.Text = dr.GetString(1); }
            else { txtAppNo.Text = ""; }
            if (!dr.IsDBNull(2))
            {
                txtDtFiling.Text = dr.GetDateTime(2).ToShortDateString();
                if (User.IsInRole("Admin") || User.IsInRole("super user"))
                {
                    txtDtFiling.Enabled = true;
                }
                else
                {
                    txtDtFiling.Enabled = false;
                }
            }
            else { txtDtFiling.Text = ""; }
            if (!dr.IsDBNull(3))
            { txtPub.Text = dr.GetString(3); }
            else { txtPub.Text = ""; }
            if (!dr.IsDBNull(4))
            { txtPubDt.Text = dr.GetDateTime(4).ToShortDateString(); }
            else { txtPubDt.Text = ""; }
            if (!dr.IsDBNull(5))
            {
                ddlStatus.Text = dr.GetString(5);
                ddlStatus_SelectedIndexChanged(sender, e);
            }
            else
            {
                ddlStatus.Text = "Pending - Internal";
                ddlStatus_SelectedIndexChanged(sender, e);
            }
            hfPreviousStatus.Value = ddlStatus.Text.Trim();
            if (!dr.IsDBNull(6))
            { txtSubStatus.Text = dr.GetString(6); }
            else { txtSubStatus.Text = ""; }
            if (!dr.IsDBNull(7))
            { txtPatNo.Text = dr.GetString(7); }
            else { txtPatNo.Text = ""; }
            if (!dr.IsDBNull(8))
            { txtPatDt.Text = dr.GetDateTime(8).ToShortDateString(); }
            else { txtPatDt.Text = ""; }
            if (!dr.IsDBNull(9))
            { ddlExam.Text = dr.GetString(9); }
            else { ddlExam.Text = ""; }
            if (!dr.IsDBNull(10))
            {
                txtExamDt.Text = dr.GetDateTime(10).ToShortDateString();
                lbFER.Visible = true;
                if (User.IsInRole("Admin") || User.IsInRole("super user"))
                    txtExamDt.Enabled = true;
                else
                    txtExamDt.Enabled = false;
            }
            else
                txtExamDt.Text = "";
            if (!dr.IsDBNull(11))
            { ddlPSearch.Text = dr.GetString(11); }
            else { ddlPSearch.Text = ""; }
            if (!dr.IsDBNull(12))
            { hfType.Value = dr.GetString(12); }
            else { hfType.Value = ""; }
            if (!dr.IsDBNull(13))
            {
                txtCompleteDt.Text = dr.GetDateTime(13).ToShortDateString();
                if (User.IsInRole("Admin") || User.IsInRole("super user"))
                {
                    txtCompleteDt.Enabled = true;
                }
                else
                {
                    txtCompleteDt.Enabled = false;
                }
            }
            else { txtCompleteDt.Text = ""; }
            if(!dr.IsDBNull(14))
            {
                txtpatsearchDt.Text = dr.GetDateTime(14).ToShortDateString();
                
            }
        }
        dr.Close();
        cnp.Close();
        ddlFileNo.Enabled = false;
    }
    protected void ddlAttorney_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
        con.Open();
        SqlCommand cmd1 = new SqlCommand();
        string sql1 = "SELECT GROUPING FROM LISTITEMMASTER WHERE CATEGORY LIKE 'STATUS' AND ITEMLIST LIKE '" + ddlStatus.SelectedItem.Text.Trim() + "'";
        cmd1.CommandType = CommandType.Text;
        cmd1.Connection = con;
        cmd1.CommandText = sql1;
        SqlDataReader dr;
        dr = cmd1.ExecuteReader();
        string typeFlag = "";
        if (dr.Read())
        {
            if (dr.IsDBNull(0)) typeFlag = ""; else typeFlag = dr.GetString(0);
        }
        if ((typeFlag == "Non Patent" && hfType.Value == "PATENT"))
        {
            ddlStatus.Text = hfPreviousStatus.Value;
            ClientScript.RegisterStartupScript(GetType(), "Information", "<script>alert('This List item only for Copyright,Trademark and Design Patent')</script>");
            return;
        }
        dr.Close();
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
        txtSubStatus.Text = "";
    }
    protected void ddlSubStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtSubStatus.Text = ddlSubStatus.SelectedItem.Text;
    }
    protected void imgBtnInsert_Click(object sender, ImageClickEventArgs e)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        if (txtCompleteDt.Text.Trim() != "")
        {
            if (txtDtFiling.Text.Trim() != "")
            {
                DateTime FilingDt, completeDt;

                if (DateTime.TryParse(txtDtFiling.Text.Trim(), out FilingDt))
                {
                    if (DateTime.TryParse(txtCompleteDt.Text.Trim(), out completeDt))
                    {
                        if (completeDt < FilingDt)
                        {
                            ClientScript.RegisterStartupScript(GetType(), "Information", "<script>alert('Complete Date should be greater than fileing date')</script>");
                            return;
                        }
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Information", "<script>alert('Verify Complete Date')</script>");
                        return;
                    }
                }
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "Information", "<script>alert('Verify Filing Date')</script>");
                    return;
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "Information", "<script>alert('Filing Date can not be empty')</script>");
                return;
            }
        }
        try
        {
            cnp.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "IndianPatent";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cnp;
            cnp.Open();
            SqlParameter pm1 = new SqlParameter();
            pm1.ParameterName = "@attorney";
            pm1.SourceColumn = "Attorney";
            if (ddlAttorney.SelectedItem.Text.Trim() == "") pm1.Value = DBNull.Value; else pm1.Value = ddlAttorney.SelectedItem.Text.Trim();
            pm1.DbType = DbType.String;
            pm1.Direction = ParameterDirection.Input;

            SqlParameter pm2 = new SqlParameter();
            pm2.ParameterName = "@applcn_no";
            pm2.SourceColumn = "applcn_no";
            if (txtAppNo.Text.Trim() == "") pm2.Value = DBNull.Value; else pm2.Value = txtAppNo.Text.Trim();
            pm2.DbType = DbType.String;
            pm2.Direction = ParameterDirection.Input;

            SqlParameter pm3 = new SqlParameter();
            pm3.ParameterName = "@filing_dt";
            pm3.SourceColumn = "filing_dt";
            if (txtDtFiling.Text.Trim() == "")
                pm3.Value = DBNull.Value;
            else
            {
                DateTime dtFiling;
                if (DateTime.TryParse(txtDtFiling.Text.Trim(), out dtFiling))
                    pm3.Value = txtDtFiling.Text.Trim();
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "Information", "<script>alert('Verify Filing Date')</script>");
                    return;
                }

            }
            pm3.DbType = DbType.String;
            pm3.Direction = ParameterDirection.Input;

            SqlParameter pm4 = new SqlParameter();
            pm4.ParameterName = "@publication";
            pm4.SourceColumn = "publication";
            if (txtPub.Text.Trim() == "") pm4.Value = DBNull.Value; else pm4.Value = txtPub.Text.Trim();
            pm4.DbType = DbType.String;
            pm4.Direction = ParameterDirection.Input;

            SqlParameter pm5 = new SqlParameter();
            pm5.ParameterName = "@pub_dt";
            pm5.SourceColumn = "pub_dt";
            if (txtPubDt.Text.Trim() == "")
                pm5.Value = DBNull.Value;
            else
            {
                DateTime pubDt;
                if (DateTime.TryParse(txtPubDt.Text.Trim(), out pubDt))
                    pm5.Value = txtPubDt.Text.Trim();
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "Information", "<script>alert('Verify Publication Date')</script>");
                    return;
                }
            }
            pm5.DbType = DbType.String;
            pm5.Direction = ParameterDirection.Input;

            SqlParameter pm6 = new SqlParameter();
            pm6.ParameterName = "@status";
            pm6.SourceColumn = "status";
            if (ddlStatus.SelectedItem.Value.Trim() == "") pm6.Value = DBNull.Value; else pm6.Value = ddlStatus.SelectedItem.Text.Trim();
            pm6.DbType = DbType.String;
            pm6.Direction = ParameterDirection.Input;

            SqlParameter pm7 = new SqlParameter();
            pm7.ParameterName = "@sub_status";
            pm7.SourceColumn = "sub_status";
            if (txtSubStatus.Text.Trim() == "") pm7.Value = DBNull.Value; else pm7.Value = txtSubStatus.Text.Trim();
            pm7.DbType = DbType.String;
            pm7.Direction = ParameterDirection.Input;

            SqlParameter pm8 = new SqlParameter();
            pm8.ParameterName = "@pat_no";
            pm8.SourceColumn = "pat_no";
            if (txtPatNo.Text.Trim() == "") pm8.Value = DBNull.Value; else pm8.Value = txtPatNo.Text.Trim();
            pm8.DbType = DbType.String;
            pm8.Direction = ParameterDirection.Input;

            SqlParameter pm9 = new SqlParameter();
            pm9.ParameterName = "@pat_dt";
            pm9.SourceColumn = "pat_dt";
            if (txtPatDt.Text.Trim() == "")
                pm9.Value = DBNull.Value;
            else
            {
                DateTime patdt;
                if (DateTime.TryParse(txtPatDt.Text.Trim(), out patdt))
                {
                    pm9.Value = txtPatDt.Text.Trim();
                    //added by priya for auto maintenence action plans 
                    if (ddlStatus.Text.Trim() == "Granted" && !string.IsNullOrEmpty(txtPatNo.Text) && !string.IsNullOrEmpty(txtPatDt.Text))
                    {
                        SqlCommand cmd1 = new SqlCommand("select count(*) from RenewalFollowup where FileNo = '" + ddlFileNo.Text.Trim() + "' and phase = 'Maintenance'", cnp);
                        int check = Convert.ToInt32(cmd1.ExecuteScalar());
                        if (check == 0)
                        {
                            DateTime flyr = Convert.ToDateTime(txtDtFiling.Text);
                            SqlCommand cmd2 = new SqlCommand("insert into RenewalFollowup (FileNo,SlNo,Description,AmtInFC,AmtInINR,Amt_InCond,Responsibility,Sharing_Party,Percentage,Status,Phase) (select '" + ddlFileNo.Text.Trim() + "',[s no],description,[Amt FC],[Amt INR],[Addn Amt ( on condonation)],responsibility,[Sharing party],Percentage,Status,'Maintenance' from Renewal_Schedule where Indian_Foreign='Indian')", cnp);
                            cmd2.ExecuteNonQuery();
                            SqlCommand cmd3 = new SqlCommand("update RenewalFollowup set DueDate= case SlNo when 1 then convert(date,'" + flyr.AddYears(2) + "',103) when 2 then convert(date,'" + flyr.AddYears(3) + "',103)  when 3 then convert(date,'" + flyr.AddYears(4) + "',103)  when 4 then convert(date,'" + flyr.AddYears(5) + "',103)  when 5 then convert(date,'" + flyr.AddYears(6) + "',103)  when 6 then convert(date,'" + flyr.AddYears(7) + "',103)  when 7 then convert(date,'" + flyr.AddYears(8) + "',103)  when 8 then convert(date,'" + flyr.AddYears(9) + "',103)  when 9 then convert(date,'" + flyr.AddYears(10) + "',103)  when 10 then convert(date,'" + flyr.AddYears(11) + "',103)  when 11 then convert(date,'" + flyr.AddYears(12) + "',103)  when 12 then convert(date,'" + flyr.AddYears(13) + "',103)  when 13 then convert(date,'" + flyr.AddYears(14) + "',103)  when 14 then convert(date,'" + flyr.AddYears(15) + "',103)  when 15 then convert(date,'" + flyr.AddYears(16) + "',103)  when  16 then convert(date,'" + flyr.AddYears(17) + "',103)  when 17 then convert(date,'" + flyr.AddYears(18) + "',103)  when 18 then convert(date,'" + flyr.AddYears(19) + "',103)  when 19 then convert(date,'" + flyr.AddYears(20) + "',103)  when 20 then convert(date,'" + flyr.AddYears(21) + "',103) end where  fileno='" + ddlFileNo.Text.Trim() + "' and phase='Maintenance'", cnp);
                            cmd3.ExecuteNonQuery();
                            DateTime gyr = Convert.ToDateTime(txtPatDt.Text);
                            int gyear = gyr.Year;
                            int fyear = flyr.Year;
                            int dyr = gyear - fyear - 1;
                            DateTime yr1 = gyr.AddMonths(3);
                            SqlCommand cmd4 = new SqlCommand("update RenewalFollowup set Status='Pending', DueDate= case when (SlNo <='" + dyr + "') then convert(date,'" + yr1 + "',103) end where fileno='" + ddlFileNo.Text.Trim() + "' and phase='Maintenance' and SlNo<='" + dyr + "'", cnp);
                            int updated_rows = Convert.ToInt32(cmd4.ExecuteNonQuery());
                            if (updated_rows != dyr)
                            {
                                ClientScript.RegisterStartupScript(GetType(), "Information", "<script>alert('Failed to update renewal schedule. Kindly update it through P&M action plan')</script>", true);
                                pminfo.Text = "Failed to update renewal schedule. Kindly update it through P&M action plan";
                            }
                            else
                            {
                                pminfo.Text = "Renewal followup for Fileno '" + ddlFileNo.Text + "' is successfully updated";
                            }
                        }
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Information", "<script>alert('Verify Patent Date')</script>");
                        return;
                    }
                }
            }
                pm9.DbType = DbType.String;
                pm9.Direction = ParameterDirection.Input;



                SqlParameter pm10 = new SqlParameter();
                pm10.ParameterName = "@Examination";
                pm10.SourceColumn = "Examination";
                if (ddlExam.SelectedItem.Text == "") pm10.Value = DBNull.Value; else pm10.Value = ddlExam.SelectedItem.Text;
                pm10.DbType = DbType.String;
                pm10.Direction = ParameterDirection.Input;

                SqlParameter pm11 = new SqlParameter();
                pm11.ParameterName = "@Exam_Dt";
                pm11.SourceColumn = "Exam_Dt";
                if (txtExamDt.Text == "")
                    pm11.Value = DBNull.Value;
                else
                {
                    DateTime examDt;
                    if (DateTime.TryParse(txtExamDt.Text.Trim(), out examDt))
                        pm11.Value = txtExamDt.Text.Trim();
                    else
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Information", "<script>alert('Verify Examination Date')</script>");
                        return;
                    }
                }
                pm11.DbType = DbType.String;
                pm11.Direction = ParameterDirection.Input;

                SqlParameter pm12 = new SqlParameter();
                pm12.ParameterName = "@UserName";
                pm12.SourceColumn = "UserName";
                pm12.Value = Membership.GetUser().UserName.ToString();
                pm12.DbType = DbType.String;
                pm12.Direction = ParameterDirection.Input;

                SqlParameter pm13 = new SqlParameter();
                pm13.ParameterName = "@fileNo";
                pm13.SourceColumn = "fileNo";
                pm13.Value = ddlFileNo.SelectedItem.Text.Trim();
                pm13.DbType = DbType.String;
                pm13.Direction = ParameterDirection.Input;

                SqlParameter pm14 = new SqlParameter();
                pm14.ParameterName = "@PatentSearch";
                pm14.SourceColumn = "PatentSearch";
                if (ddlPSearch.SelectedItem.Text == "") pm14.Value = DBNull.Value; else pm14.Value = ddlPSearch.SelectedItem.Value;
                pm14.DbType = DbType.String;
                pm14.Direction = ParameterDirection.Input;

                SqlParameter pm15 = new SqlParameter();
                pm15.ParameterName = "@CompleteDt";
                pm15.SourceColumn = "CompleteDt";
                if (txtCompleteDt.Text.Trim() == "")
                    pm15.Value = DBNull.Value;
                if (txtCompleteDt.Text.Trim() != "")
                {
                    DateTime CompleteDt;
                    if (DateTime.TryParse(txtCompleteDt.Text.Trim(), out CompleteDt))
                    {
                        pm15.Value = txtCompleteDt.Text.Trim();
                        try
                        {
                            SqlCommand cmd1 = new SqlCommand("select count(*) from RenewalFollowup where FileNo = '" + ddlFileNo.Text.Trim() + "' and phase = 'Prosecution'", cnp);
                            int check = Convert.ToInt32(cmd1.ExecuteScalar());
                            if (check == 0)
                            {
                                SqlCommand cmd2 = new SqlCommand("insert into RenewalFollowup (FileNo,SlNo,Description,Responsibility,Phase) (select '" + ddlFileNo.Text.Trim() + "',[s no],description,responsibility,'Prosecution' from tbl_mst_prosecution where Indian_Foreign='Indian')", cnp);
                                cmd2.ExecuteNonQuery();
                                DateTime duedate_ep = Convert.ToDateTime(txtCompleteDt.Text).AddDays(1);
                                DateTime duedate_pa = Convert.ToDateTime(txtDtFiling.Text).AddMonths(18);
                                DateTime duedate_fer = Convert.ToDateTime(txtDtFiling.Text).AddMonths(24);
                                DateTime duedate_oa1 = duedate_fer.AddMonths(1);
                                DateTime duedate_oa2 = duedate_oa1.AddMonths(3);
                                DateTime duedate_oa3 = duedate_oa2.AddMonths(5);
                                DateTime duedate_grant = Convert.ToDateTime(txtDtFiling.Text).AddMonths(36);
                                SqlCommand cmd3 = new SqlCommand("update RenewalFollowup set set Status='Pending', DueDate= case SlNo when 1 then  convert(date,'" + duedate_ep + "',103) when 2 then convert(date,'" + duedate_pa + "',103) when 3 then convert(date,'" + duedate_fer + "',103) when 4 then convert(date,'" + duedate_oa1 + "',103) when 5 then convert(date,'" + duedate_oa2 + "',103) when 6 then convert(date,'" + duedate_oa3 + "',103) when 8 then convert(date,'" + duedate_grant + "',103) end where fileno='" + ddlFileNo.Text.Trim() + "' and phase='prosecution'", cnp);
                                int updated_rows = Convert.ToInt32(cmd3.ExecuteNonQuery());
                                if (updated_rows != 8)
                                {
                                    ClientScript.RegisterStartupScript(GetType(), "Information", "<script>alert('Failed to update renewal schedule. Kindly update it through P&M action plan')</script>", true);
                                    pminfo.Text = "Failed to update renewal schedule. Kindly update it through P&M action plan";
                                }
                                else
                                {
                                    pminfo.Text = "Renewal followup for Fileno '" + ddlFileNo.Text + "' is successfully updated";
                                }
                            }
                            else
                            {
                                ClientScript.RegisterStartupScript(GetType(), "Information", "<script>alert('Renewal followup for Fileno'" + ddlFileNo.Text + "' is already exists. Complete filing date cannot be altered')</script>");
                                pminfo.Text = "Renewal followup for Fileno '" + ddlFileNo.Text + "' is already exists. Complete filing date cannot be altered";
                            }
                        }
                        catch (Exception ex)
                        {
                            ClientScript.RegisterStartupScript(GetType(), "Information", "<script>alert('" + ex + "')</script>");
                            return;
                        }
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Information", "<script>alert('Verify Complete Date')</script>");
                        return;
                    }
                }
                pm15.DbType = DbType.String;
                pm15.Direction = ParameterDirection.Input;

                SqlParameter pm16 = new SqlParameter();
                pm16.ParameterName = "@Patent_searchDt";
                pm16.SourceColumn = "Patent_searchDt";
                if (txtpatsearchDt.Text.Trim() == "")
                    pm16.Value = DBNull.Value;
                else
                {
                    DateTime Patent_searchDt;
                    if (DateTime.TryParse(txtpatsearchDt.Text.Trim(), out Patent_searchDt))
                        pm16.Value = txtpatsearchDt.Text.Trim();
                    else
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Information", "<script>alert('Verify Patent Search Date')</script>");
                        return;
                    }
                }
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
                txtpatsearchDt.Text = "";
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
    protected void lbFER_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime examDt;
        if (DateTime.TryParse(txtExamDt.Text.Trim(), out examDt))
        {
            if (!string.IsNullOrEmpty(ddlFileNo.Text.Trim()) && ddlExam.Text.Trim() == "Yes") Server.Transfer("ExaminationReport.aspx");
        }
    }

}
