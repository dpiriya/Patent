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
using System.Web.Services;
using System.Web.Script.Services;
using System.Collections.Generic;

public partial class CompanyModify : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    SqlTransaction trans;   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (User.Identity.IsAuthenticated)
        {
            if (User.IsInRole("Admin") || User.IsInRole("Super User") || User.IsInRole("Marketing") || !User.IsInRole("Intern"))
            {
                if (!IsPostBack)
                {
                    Company ds = new Company();
                    lvBusiness.DataSource = ds.Tables["BusinessAreaDetails"];
                    lvBusiness.DataBind();
                    ViewState["BATable"] = ds.Tables["BusinessAreaDetails"];
                    lvContact.DataSource = ds.Tables["ContactDetails"];
                    lvContact.DataBind();
                    ViewState["ContactTable"] = ds.Tables["ContactDetails"];
                    imgBtnSubmit.Visible = (!User.IsInRole("View"));
                }
            }
            else
            {
                Server.Transfer("Unauthorized.aspx");
            }
        }
    }

    [WebMethod]
    public static List<string> GetCompany(string company)
    {
        List<string> result = new List<string>();
        SqlConnection cnp = new SqlConnection();
        cnp.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
        SqlCommand cmd = new SqlCommand();
        string sql = "select companyID from companyMaster where companyID like '%'+ @SearchText +'%' order by companyName";
        SqlDataReader dr;
        cmd.CommandType = CommandType.Text;
        cmd.Parameters.AddWithValue("@SearchText", company);
        cmd.Connection = cnp;
        cmd.CommandText = sql;
        cnp.Open();
        dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            result.Add(dr["companyID"].ToString());
        }
        return result;
    }

    protected void imgBtnSubmit_Click(object sender, ImageClickEventArgs e)
    {
        if (txtCompanyName.Text.Trim() == "")
        {
            ClientScript.RegisterStartupScript(GetType(), "Success", "<script>alert('Please Enter Company Name')</script>");
            return;
        }
        try
        {
            SqlCommand cmd = new SqlCommand();
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
            con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
            string sql = "update companyMaster set CompanyName=@CompanyName,Address1=case when @Address1='' then null else @Address1 end," +
            "Address2=case when @Address2='' then null else @Address2 end,City=case when @City='' then null else @City end," +
            "Pincode=case when @Pincode='' then null else @Pincode end,Phone1=case when @Phone1='' then null else @Phone1 end," +
            "Phone2=case when @Phone2='' then null else @Phone2 end,FaxNo=case when @FaxNo='' then null else @FaxNo end," +
            "EmailID1=case when @EmailID1='' then null else @EmailID1 end,EmailID2=case when @EmailID2='' then null else @EmailID2 end," +
            "IndustryType=case when @IndustryType='' then null else @IndustryType end,State=case when @State='' then null else @State end,Country=case when @Country='' then null else @Country end where CompanyID like @CompanyID"; 
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            con.Open();
            trans = con.BeginTransaction();
            cmd.Transaction = trans;

            SqlParameter pm1 = new SqlParameter();
            pm1.ParameterName = "@CompanyID";
            pm1.SourceColumn = "CompanyID";
            pm1.Value = txtCompanyID.Text.Trim();
            pm1.DbType = DbType.String;
            pm1.Direction = ParameterDirection.Input;

            SqlParameter pm2 = new SqlParameter();
            pm2.ParameterName = "@CompanyName";
            pm2.SourceColumn = "CompanyName";
            pm2.Value = txtCompanyName.Text.Trim();
            pm2.DbType = DbType.String;
            pm2.Direction = ParameterDirection.Input;

            SqlParameter pm3 = new SqlParameter();
            pm3.ParameterName = "@Address1";
            pm3.SourceColumn = "Address1";
            pm3.Value = txtAddress1.Text.Trim();
            pm3.DbType = DbType.String;
            pm3.Direction = ParameterDirection.Input;

            SqlParameter pm4 = new SqlParameter();
            pm4.ParameterName = "@Address2";
            pm4.SourceColumn = "Address2";
            pm4.Value = txtAddress2.Text.Trim();
            pm4.DbType = DbType.String;
            pm4.Direction = ParameterDirection.Input;

            SqlParameter pm5 = new SqlParameter();
            pm5.ParameterName = "@City";
            pm5.SourceColumn = "City";
            pm5.Value = txtCity.Text.Trim();
            pm5.DbType = DbType.String;
            pm5.Direction = ParameterDirection.Input;

            SqlParameter pm6 = new SqlParameter();
            pm6.ParameterName = "@Pincode";
            pm6.SourceColumn = "Pincode";
            pm6.Value = txtPincode.Text.Trim();
            pm6.DbType = DbType.String;
            pm6.Direction = ParameterDirection.Input;

            SqlParameter pm7 = new SqlParameter();
            pm7.ParameterName = "@Phone1";
            pm7.SourceColumn = "Phone1";
            pm7.Value = txtPhone1.Text.Trim();
            pm7.DbType = DbType.String;
            pm7.Direction = ParameterDirection.Input;

            SqlParameter pm8 = new SqlParameter();
            pm8.ParameterName = "@Phone2";
            pm8.SourceColumn = "Phone2";
            pm8.Value = txtPhone2.Text.Trim();
            pm8.DbType = DbType.String;
            pm8.Direction = ParameterDirection.Input;

            SqlParameter pm9 = new SqlParameter();
            pm9.ParameterName = "@FaxNo";
            pm9.SourceColumn = "FaxNo";
            pm9.Value = txtFax.Text.Trim();
            pm9.DbType = DbType.String;
            pm9.Direction = ParameterDirection.Input;

            SqlParameter pm10 = new SqlParameter();
            pm10.ParameterName = "@EmailID1";
            pm10.SourceColumn = "EmailID1";
            pm10.Value = txtEmail1.Text.Trim();
            pm10.DbType = DbType.String;
            pm10.Direction = ParameterDirection.Input;

            SqlParameter pm11 = new SqlParameter();
            pm11.ParameterName = "@EmailID2";
            pm11.SourceColumn = "EmailID2";
            pm11.Value = txtEmail2.Text.Trim();
            pm11.DbType = DbType.String;
            pm11.Direction = ParameterDirection.Input;

            SqlParameter pm12 = new SqlParameter();
            pm12.ParameterName = "@IndustryType";
            pm12.SourceColumn = "IndustryType";
            pm12.Value = txtIndustry.Text.Trim();
            pm12.DbType = DbType.String;
            pm12.Direction = ParameterDirection.Input;

            SqlParameter pm13 = new SqlParameter();
            pm13.ParameterName = "@State";
            pm13.SourceColumn = "State";
            pm13.Value = txtState.Text.Trim();
            pm13.DbType = DbType.String;
            pm13.Direction = ParameterDirection.Input;

            SqlParameter pm14 = new SqlParameter();
            pm14.ParameterName = "@Country";
            pm14.SourceColumn = "Country";
            pm14.Value = txtCountry.Text.Trim();
            pm14.DbType = DbType.String;
            pm14.Direction = ParameterDirection.Input;

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
            cmd.ExecuteNonQuery();

            DataTable dt = (DataTable)ViewState["BATable"];
            DataTable baAdd = dt.GetChanges(DataRowState.Added);
            if (baAdd != null)
            {
                if (baAdd.Rows.Count > 0)
                {
                    foreach (DataRow dr in baAdd.Rows)
                    {
                        SqlCommand cmd2 = new SqlCommand();
                        cmd2.Transaction = trans;
                        string sql2 = "insert into CompanyBusinessArea (EntryDt,CompanyID,SlNo,BusinessArea,Products) values " +
                        "(convert(smalldatetime,@EntryDt,103),@CompanyID,@SlNo,case when @BusinessArea='' then null else @BusinessArea end,case when @Products='' then null else @Products end)";
                        cmd2.CommandText = sql2;
                        cmd2.CommandType = CommandType.Text;
                        cmd2.Connection = con;

                        SqlParameter bpm1 = new SqlParameter();
                        bpm1.ParameterName = "@CompanyID";
                        bpm1.SourceColumn = "CompanyID";
                        bpm1.Value = txtCompanyID.Text.Trim();
                        bpm1.DbType = DbType.String;
                        bpm1.Direction = ParameterDirection.Input;

                        SqlParameter bpm2 = new SqlParameter();
                        bpm2.ParameterName = "@SlNo";
                        bpm2.SourceColumn = "SlNo";
                        bpm2.Value = Convert.ToInt32(dr["SlNo"]);
                        bpm2.DbType = DbType.Int32;
                        bpm2.Direction = ParameterDirection.Input;

                        SqlParameter bpm3 = new SqlParameter();
                        bpm3.ParameterName = "@BusinessArea";
                        bpm3.SourceColumn = "BusinessArea";
                        bpm3.Value = dr["BusinessArea"].ToString();
                        bpm3.DbType = DbType.String;
                        bpm3.Direction = ParameterDirection.Input;

                        SqlParameter bpm4 = new SqlParameter();
                        bpm4.ParameterName = "@Products";
                        bpm4.SourceColumn = "Products";
                        bpm4.Value = dr["Products"].ToString();
                        bpm4.DbType = DbType.String;
                        bpm4.Direction = ParameterDirection.Input;

                        SqlParameter bpm5 = new SqlParameter();
                        bpm5.ParameterName = "@EntryDt";
                        bpm5.SourceColumn = "EntryDt";
                        bpm5.Value = DateTime.Now.ToShortDateString();
                        bpm5.DbType = DbType.String;
                        bpm5.Direction = ParameterDirection.Input;

                        cmd2.Parameters.Add(bpm1);
                        cmd2.Parameters.Add(bpm2);
                        cmd2.Parameters.Add(bpm3);
                        cmd2.Parameters.Add(bpm4);
                        cmd2.Parameters.Add(bpm5);

                        cmd2.ExecuteNonQuery();
                    }
                }
            }

            DataTable baMod = dt.GetChanges(DataRowState.Modified);
            if (baMod != null)
            {
                if (baMod.Rows.Count > 0)
                {
                    foreach (DataRow dr in baMod.Rows)
                    {
                        SqlCommand cmd4 = new SqlCommand();
                        cmd4.Transaction = trans;
                        string sql4 = "update CompanyBusinessArea set BusinessArea=@BusinessArea,Products=case when @Products='' then null else @Products end " +
                        "where CompanyID=@CompanyID and SlNo=@SlNo";
                        cmd4.CommandText = sql4;
                        cmd4.CommandType = CommandType.Text;
                        cmd4.Connection = con;

                        SqlParameter bpb1 = new SqlParameter();
                        bpb1.ParameterName = "@CompanyID";
                        bpb1.SourceColumn = "CompanyID";
                        bpb1.Value = txtCompanyID.Text.Trim();
                        bpb1.DbType = DbType.String;
                        bpb1.Direction = ParameterDirection.Input;

                        SqlParameter bpb2 = new SqlParameter();
                        bpb2.ParameterName = "@SlNo";
                        bpb2.SourceColumn = "SlNo";
                        bpb2.Value = Convert.ToInt32(dr["SlNo"]);
                        bpb2.DbType = DbType.Int32;
                        bpb2.Direction = ParameterDirection.Input;

                        SqlParameter bpb3 = new SqlParameter();
                        bpb3.ParameterName = "@BusinessArea";
                        bpb3.SourceColumn = "BusinessArea";
                        bpb3.Value = dr["BusinessArea"].ToString();
                        bpb3.DbType = DbType.String;
                        bpb3.Direction = ParameterDirection.Input;

                        SqlParameter bpb4 = new SqlParameter();
                        bpb4.ParameterName = "@Products";
                        bpb4.SourceColumn = "Products";
                        bpb4.Value = dr["Products"].ToString();
                        bpb4.DbType = DbType.String;
                        bpb4.Direction = ParameterDirection.Input;

                        cmd4.Parameters.Add(bpb1);
                        cmd4.Parameters.Add(bpb2);
                        cmd4.Parameters.Add(bpb3);
                        cmd4.Parameters.Add(bpb4);

                        cmd4.ExecuteNonQuery();
                    }
                }
            }
            DataTable baDel = dt.GetChanges(DataRowState.Deleted);
            if (baDel != null)
            {
                if (baDel.Rows.Count > 0)
                {
                    foreach (DataRow dr in baDel.Rows)
                    {
                        SqlCommand cmd5 = new SqlCommand();
                        cmd5.Transaction = trans;
                        string sql5 = "delete from CompanyBusinessArea where CompanyID=@CompanyID and SlNo=@SlNo";
                        cmd5.CommandText = sql5;
                        cmd5.CommandType = CommandType.Text;
                        cmd5.Connection = con;

                        SqlParameter bpd1 = new SqlParameter();
                        bpd1.ParameterName = "@CompanyID";
                        bpd1.SourceColumn = "CompanyID";
                        bpd1.Value = txtCompanyID.Text.Trim();
                        bpd1.DbType = DbType.String;
                        bpd1.Direction = ParameterDirection.Input;

                        SqlParameter bpd2 = new SqlParameter();
                        bpd2.ParameterName = "@SlNo";
                        bpd2.SourceColumn = "SlNo";
                        bpd2.Value = Convert.ToInt32(dr["SlNo",DataRowVersion.Original]);
                        bpd2.DbType = DbType.Int32;
                        bpd2.Direction = ParameterDirection.Input;

                        cmd5.Parameters.Add(bpd1);
                        cmd5.Parameters.Add(bpd2);
                       
                        cmd5.ExecuteNonQuery();
                    }
                }
            }

            DataTable ContactDT = (DataTable)ViewState["ContactTable"];
            DataTable cAdd = ContactDT.GetChanges(DataRowState.Added);
            if (cAdd != null)
            {
                if (cAdd.Rows.Count > 0)
                {
                    foreach (DataRow dr in cAdd.Rows)
                    {
                        SqlCommand cmd3 = new SqlCommand();
                        cmd3.Transaction = trans;
                        string sql3 = "insert into CompanyContactDetails(EntryDt,CompanyID,SlNo,ContactPersonName,BusinessArea,Address1,Address2,PhoneNo1,PhoneNo2,EmailID) values " +
                        "(convert(smalldatetime,@EntryDt,103),@CompanyID,@SlNo,@ContactPersonName,case when @BusinessArea='' then null else @BusinessArea end,case when @Address1='' then null else @Address1 end," +
                        "case when @Address2='' then null else @Address2 end,case when @PhoneNo1='' then null else @PhoneNo1 end,case when @PhoneNo2='' then null else @PhoneNo2 end,case when @EmailID='' then null else @EmailID end)";
                        cmd3.CommandText = sql3;
                        cmd3.CommandType = CommandType.Text;
                        cmd3.Connection = con;

                        SqlParameter cpm1 = new SqlParameter();
                        cpm1.ParameterName = "@CompanyID";
                        cpm1.SourceColumn = "CompanyID";
                        cpm1.Value = txtCompanyID.Text.Trim();
                        cpm1.DbType = DbType.String;
                        cpm1.Direction = ParameterDirection.Input;

                        SqlParameter cpm2 = new SqlParameter();
                        cpm2.ParameterName = "@SlNo";
                        cpm2.SourceColumn = "SlNo";
                        cpm2.Value = Convert.ToInt32(dr["SlNo"]);
                        cpm2.DbType = DbType.Int32;
                        cpm2.Direction = ParameterDirection.Input;

                        SqlParameter cpm3 = new SqlParameter();
                        cpm3.ParameterName = "@ContactPersonName";
                        cpm3.SourceColumn = "ContactPersonName";
                        cpm3.Value = dr["ContactPersonName"];
                        cpm3.DbType = DbType.String;
                        cpm3.Direction = ParameterDirection.Input;

                        SqlParameter cpm4 = new SqlParameter();
                        cpm4.ParameterName = "@BusinessArea";
                        cpm4.SourceColumn = "BusinessArea";
                        cpm4.Value = dr["BusinessArea"].ToString();
                        cpm4.DbType = DbType.String;
                        cpm4.Direction = ParameterDirection.Input;

                        SqlParameter cpm5 = new SqlParameter();
                        cpm5.ParameterName = "@Address1";
                        cpm5.SourceColumn = "Address1";
                        cpm5.Value = dr["Address1"].ToString();
                        cpm5.DbType = DbType.String;
                        cpm5.Direction = ParameterDirection.Input;

                        SqlParameter cpm6 = new SqlParameter();
                        cpm6.ParameterName = "@Address2";
                        cpm6.SourceColumn = "Address2";
                        cpm6.Value = dr["Address2"].ToString();
                        cpm6.DbType = DbType.String;
                        cpm6.Direction = ParameterDirection.Input;

                        SqlParameter cpm7 = new SqlParameter();
                        cpm7.ParameterName = "@PhoneNo1";
                        cpm7.SourceColumn = "PhoneNo1";
                        cpm7.Value = dr["PhoneNo1"].ToString();
                        cpm7.DbType = DbType.String;
                        cpm7.Direction = ParameterDirection.Input;

                        SqlParameter cpm8 = new SqlParameter();
                        cpm8.ParameterName = "@PhoneNo2";
                        cpm8.SourceColumn = "PhoneNo2";
                        cpm8.Value = dr["PhoneNo2"].ToString();
                        cpm8.DbType = DbType.String;
                        cpm8.Direction = ParameterDirection.Input;

                        SqlParameter cpm9 = new SqlParameter();
                        cpm9.ParameterName = "@EmailID";
                        cpm9.SourceColumn = "EmailID";
                        cpm9.Value = dr["EmailID"].ToString();
                        cpm9.DbType = DbType.String;
                        cpm9.Direction = ParameterDirection.Input;

                        SqlParameter cpm10 = new SqlParameter();
                        cpm10.ParameterName = "@EntryDt";
                        cpm10.SourceColumn = "EntryDt";
                        cpm10.Value = DateTime.Now.ToShortDateString();
                        cpm10.DbType = DbType.String;
                        cpm10.Direction = ParameterDirection.Input;

                        cmd3.Parameters.Add(cpm1);
                        cmd3.Parameters.Add(cpm2);
                        cmd3.Parameters.Add(cpm3);
                        cmd3.Parameters.Add(cpm4);
                        cmd3.Parameters.Add(cpm5);
                        cmd3.Parameters.Add(cpm6);
                        cmd3.Parameters.Add(cpm7);
                        cmd3.Parameters.Add(cpm8);
                        cmd3.Parameters.Add(cpm9);
                        cmd3.Parameters.Add(cpm10);

                        cmd3.ExecuteNonQuery();
                    }
                }
            }
            DataTable cMod = ContactDT.GetChanges(DataRowState.Modified);
            if (cMod != null)
            {
                if (cMod.Rows.Count > 0)
                {
                    foreach (DataRow dr in cMod.Rows)
                    {
                        SqlCommand cmd6 = new SqlCommand();
                        cmd6.Transaction = trans;
                        string sql6 = "update CompanyContactDetails set ContactPersonName=@ContactPersonName,BusinessArea=case when @BusinessArea='' then null else @BusinessArea end,Address1=case when @Address1='' then null else @Address1 end," +
                        "Address2=case when @Address2='' then null else @Address2 end,PhoneNo1=case when @PhoneNo1='' then null else @PhoneNo1 end," +
                        "PhoneNo2=case when @PhoneNo2='' then null else @PhoneNo2 end,EmailID=case when @EmailID='' then null else @EmailID end " +
                        "where CompanyID=@CompanyID and SlNo=@SlNo";
                        cmd6.CommandText = sql6;
                        cmd6.CommandType = CommandType.Text;
                        cmd6.Connection = con;

                        SqlParameter cpc1 = new SqlParameter();
                        cpc1.ParameterName = "@CompanyID";
                        cpc1.SourceColumn = "CompanyID";
                        cpc1.Value = txtCompanyID.Text.Trim();
                        cpc1.DbType = DbType.String;
                        cpc1.Direction = ParameterDirection.Input;

                        SqlParameter cpc2 = new SqlParameter();
                        cpc2.ParameterName = "@SlNo";
                        cpc2.SourceColumn = "SlNo";
                        cpc2.Value = Convert.ToInt32(dr["SlNo"]);
                        cpc2.DbType = DbType.Int32;
                        cpc2.Direction = ParameterDirection.Input;

                        SqlParameter cpc3 = new SqlParameter();
                        cpc3.ParameterName = "@ContactPersonName";
                        cpc3.SourceColumn = "ContactPersonName";
                        cpc3.Value = dr["ContactPersonName"];
                        cpc3.DbType = DbType.String;
                        cpc3.Direction = ParameterDirection.Input;

                        SqlParameter cpc4 = new SqlParameter();
                        cpc4.ParameterName = "@BusinessArea";
                        cpc4.SourceColumn = "BusinessArea";
                        cpc4.Value = dr["BusinessArea"].ToString();
                        cpc4.DbType = DbType.String;
                        cpc4.Direction = ParameterDirection.Input;

                        SqlParameter cpc5 = new SqlParameter();
                        cpc5.ParameterName = "@Address1";
                        cpc5.SourceColumn = "Address1";
                        cpc5.Value = dr["Address1"].ToString();
                        cpc5.DbType = DbType.String;
                        cpc5.Direction = ParameterDirection.Input;

                        SqlParameter cpc6 = new SqlParameter();
                        cpc6.ParameterName = "@Address2";
                        cpc6.SourceColumn = "Address2";
                        cpc6.Value = dr["Address2"].ToString();
                        cpc6.DbType = DbType.String;
                        cpc6.Direction = ParameterDirection.Input;

                        SqlParameter cpc7 = new SqlParameter();
                        cpc7.ParameterName = "@PhoneNo1";
                        cpc7.SourceColumn = "PhoneNo1";
                        cpc7.Value = dr["PhoneNo1"].ToString();
                        cpc7.DbType = DbType.String;
                        cpc7.Direction = ParameterDirection.Input;

                        SqlParameter cpc8 = new SqlParameter();
                        cpc8.ParameterName = "@PhoneNo2";
                        cpc8.SourceColumn = "PhoneNo2";
                        cpc8.Value = dr["PhoneNo2"].ToString();
                        cpc8.DbType = DbType.String;
                        cpc8.Direction = ParameterDirection.Input;

                        SqlParameter cpc9 = new SqlParameter();
                        cpc9.ParameterName = "@EmailID";
                        cpc9.SourceColumn = "EmailID";
                        cpc9.Value = dr["EmailID"].ToString();
                        cpc9.DbType = DbType.String;
                        cpc9.Direction = ParameterDirection.Input;


                        cmd6.Parameters.Add(cpc1);
                        cmd6.Parameters.Add(cpc2);
                        cmd6.Parameters.Add(cpc3);
                        cmd6.Parameters.Add(cpc4);
                        cmd6.Parameters.Add(cpc5);
                        cmd6.Parameters.Add(cpc6);
                        cmd6.Parameters.Add(cpc7);
                        cmd6.Parameters.Add(cpc8);
                        cmd6.Parameters.Add(cpc9);

                        cmd6.ExecuteNonQuery();
                    }
                }
            }
            DataTable cDel = ContactDT.GetChanges(DataRowState.Deleted);
            if (cDel != null)
            {
                if (cDel.Rows.Count > 0)
                {
                    foreach (DataRow dr in cDel.Rows)
                    {
                        SqlCommand cmd7 = new SqlCommand();
                        cmd7.Transaction = trans;
                        string sql7 = "delete from CompanyContactDetails where CompanyID=@CompanyID and SlNo=@SlNo";
                        cmd7.CommandText = sql7;
                        cmd7.CommandType = CommandType.Text;
                        cmd7.Connection = con;

                        SqlParameter cpd1 = new SqlParameter();
                        cpd1.ParameterName = "@CompanyID";
                        cpd1.SourceColumn = "CompanyID";
                        cpd1.Value = txtCompanyID.Text.Trim();
                        cpd1.DbType = DbType.String;
                        cpd1.Direction = ParameterDirection.Input;

                        SqlParameter cpd2 = new SqlParameter();
                        cpd2.ParameterName = "@SlNo";
                        cpd2.SourceColumn = "SlNo";
                        cpd2.Value = Convert.ToInt32(dr["SlNo",DataRowVersion.Original]);
                        cpd2.DbType = DbType.Int32;
                        cpd2.Direction = ParameterDirection.Input;

                        cmd7.Parameters.Add(cpd1);
                        cmd7.Parameters.Add(cpd2);
                        
                        cmd7.ExecuteNonQuery();
                    }
                }
            }

            trans.Commit();

            ClientScript.RegisterStartupScript(GetType(), "Success", "<script>alert('This record successfully modified')</script>");
            con.Close();
        }
        catch (Exception ex)
        {
            ErrorMsg.InnerText = ex.Message.ToString();
            //ClientScript.RegisterStartupScript(GetType(), "Error", "<script>alert('" + ex.Message.ToString() + "')</script>");
            trans.Rollback();
            con.Close();
            return;
        }
        imgBtnClear_Click(sender, e);
    }
    protected void imgBtnClear_Click(object sender, ImageClickEventArgs e)
    {
        txtCompanyID.Text = "";
        txtCompanyID.Enabled = true;
        txtCompanyName.Text = "";
        txtAddress1.Text = "";
        txtAddress2.Text = "";
        txtCity.Text = "";
        txtState.Text = "";
        txtCountry.Text = "";
        txtPincode.Text = "";
        txtPhone1.Text = "";
        txtPhone2.Text = "";
        txtFax.Text = "";
        txtEmail1.Text = "";
        txtEmail2.Text = "";
        txtIndustry.Text = "";
        ErrorMsg.InnerText = "";
        DataTable dt = (DataTable)ViewState["ContactTable"];
        dt.Clear();
        lvContact.DataSource = dt;
        lvContact.DataBind();
        DataTable dt1 = (DataTable)ViewState["BATable"];
        dt1.Clear();
        lvBusiness.DataSource = dt1;
        lvBusiness.DataBind();
    }
    protected void lvBusiness_ItemCanceling(object sender, ListViewCancelEventArgs e)
    {
        lvBusiness.EditIndex = -1;
        lvBusiness.DataSource = (DataTable)ViewState["BATable"];
        lvBusiness.DataBind();
    }
    protected void lvBusiness_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        LinkButton lb = e.Item.FindControl("lbtnInsert") as LinkButton;
        LinkButton lb1 = e.Item.FindControl("lbtnCancel") as LinkButton;
        if (lb != null)
        {
            ScriptManager.GetCurrent(Page).RegisterPostBackControl(lb);
        }
        if (lb1 != null)
        {
            ScriptManager.GetCurrent(Page).RegisterPostBackControl(lb1);
        }
    }
    protected void lvBusiness_ItemDeleting(object sender, ListViewDeleteEventArgs e)
    {
        int iDel = e.ItemIndex;
        DataTable dt = (DataTable)ViewState["BATable"];
        DataRow dr = dt.Rows[iDel];
        if (dr.RowState != DataRowState.Deleted)
        {
            dr.Delete();
        }
        lvBusiness.DataSource = (DataTable)ViewState["BATable"];
        lvBusiness.DataBind();
    }
    protected void lvBusiness_ItemEditing(object sender, ListViewEditEventArgs e)
    {
        lvBusiness.EditIndex = e.NewEditIndex;
        lvBusiness.DataSource = (DataTable)ViewState["BATable"];
        lvBusiness.DataBind();
    }
    protected void lvBusiness_ItemInserting(object sender, ListViewInsertEventArgs e)
    {
        string slno = "";
        string business = "";
        string product = "";
        TextBox txt;
        txt = e.Item.FindControl("txtNewBusiness") as TextBox;
        business = txt.Text;
        txt = e.Item.FindControl("txtNewProduct") as TextBox;
        product = txt.Text;
        DataTable dt = (DataTable)ViewState["BATable"];
        if (dt.Rows.Count > 0)
        {
            DataRow drSl = dt.Rows[dt.Rows.Count - 1];
            if (drSl.RowState == DataRowState.Deleted)
            {
                slno = (Convert.ToInt32(drSl["slno", DataRowVersion.Original]) + 1).ToString();
            }
            else
            {
                slno = (Convert.ToInt32(drSl["slno"]) + 1).ToString();
            }
        }
        else
        {
            slno = "1";
        }
        DataRow dr = dt.NewRow();
        dr["slno"] = slno;
        dr["BusinessArea"] = business;
        dr["Products"] = product;
        dt.Rows.Add(dr);
        lvBusiness.DataSource = (DataTable)ViewState["BATable"];
        lvBusiness.DataBind();
    }
    protected void lvBusiness_ItemUpdating(object sender, ListViewUpdateEventArgs e)
    {
        string slno = "";
        string business = "";
        string product = "";
        TextBox txt;
        Label lbl;
        lbl = lvBusiness.Items[e.ItemIndex].FindControl("lblSlNo") as Label;
        slno = lbl.Text;
        txt = lvBusiness.Items[e.ItemIndex].FindControl("txtEditBusiness") as TextBox;
        business = txt.Text;
        txt = lvBusiness.Items[e.ItemIndex].FindControl("txtEditProduct") as TextBox;
        product = txt.Text;
        DataTable dt = (DataTable)ViewState["BATable"];
        foreach (DataRow dr in dt.Rows)
        {
            if (dr["SlNo"].ToString() == slno)
            {
                dr["BusinessArea"] = business;
                dr["Products"] = product;
            }
        }
        lvBusiness.EditIndex = -1;
        lvBusiness.DataSource = (DataTable)ViewState["BATable"];
        lvBusiness.DataBind();  
    }
    protected void lvContact_ItemCanceling(object sender, ListViewCancelEventArgs e)
    {
        lvContact.EditIndex = -1;
        lvContact.DataSource = (DataTable)ViewState["ContactTable"];
        lvContact.DataBind();
    }
    protected void lvContact_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        LinkButton lb = e.Item.FindControl("btnInsert") as LinkButton;
        LinkButton lb1 = e.Item.FindControl("btnCancel") as LinkButton;
        if (lb != null)
        {
            ScriptManager.GetCurrent(Page).RegisterPostBackControl(lb);
        }
        if (lb1 != null)
        {
            ScriptManager.GetCurrent(Page).RegisterPostBackControl(lb1);
        }
    }
    protected void lvContact_ItemDeleting(object sender, ListViewDeleteEventArgs e)
    {
        int iDel = e.ItemIndex;
        DataTable dt = (DataTable)ViewState["ContactTable"];
        DataRow dr = dt.Rows[iDel];
        if (dr.RowState != DataRowState.Deleted)
        {
            dr.Delete();
        }
        lvContact.DataSource = (DataTable)ViewState["ContactTable"];
        lvContact.DataBind();
    }
    protected void lvContact_ItemEditing(object sender, ListViewEditEventArgs e)
    {
        lvContact.EditIndex = e.NewEditIndex;
        lvContact.DataSource = (DataTable)ViewState["ContactTable"];
        lvContact.DataBind();
    }
    protected void lvContact_ItemInserting(object sender, ListViewInsertEventArgs e)
    {
        string slno = "";
        string contact = "";
        string business = "";
        string address1 = "";
        string address2 = "";
        string phoneNo1 = "";
        string phoneNo2 = "";
        string emailID = "";
        TextBox txt;
        txt = e.Item.FindControl("txtNewContact") as TextBox;
        contact = txt.Text;
        txt = e.Item.FindControl("txtNewBusiness") as TextBox;
        business = txt.Text;
        txt = e.Item.FindControl("txtNewAddress1") as TextBox;
        address1 = txt.Text;
        txt = e.Item.FindControl("txtNewAddress2") as TextBox;
        address2 = txt.Text;
        txt = e.Item.FindControl("txtNewPhone1") as TextBox;
        phoneNo1 = txt.Text;
        txt = e.Item.FindControl("txtNewPhone2") as TextBox;
        phoneNo2 = txt.Text;
        txt = e.Item.FindControl("txtNewEmail") as TextBox;
        emailID = txt.Text;
        DataTable dt = (DataTable)ViewState["ContactTable"];
        if (dt.Rows.Count > 0)
        {
            DataRow drSl = dt.Rows[dt.Rows.Count - 1];
            if (drSl.RowState == DataRowState.Deleted)
            {
                slno = (Convert.ToInt32(drSl["slno", DataRowVersion.Original]) + 1).ToString();
            }
            else
            {
                slno = (Convert.ToInt32(drSl["slno"]) + 1).ToString();
            }
        }
        else
        {
            slno = "1";
        }
        DataRow dr = dt.NewRow();
        dr["slno"] = slno;
        dr["ContactPersonName"] = contact;
        dr["BusinessArea"] = business;
        dr["Address1"] = address1;
        dr["Address2"] = address2;
        dr["PhoneNo1"] = phoneNo1;
        dr["PhoneNo2"] = phoneNo2;
        dr["EmailID"] = emailID;
        dt.Rows.Add(dr);
        lvContact.DataSource = (DataTable)ViewState["ContactTable"];
        lvContact.DataBind();
    }
    protected void lvContact_ItemUpdating(object sender, ListViewUpdateEventArgs e)
    {
        string slno = "";
        string contact = "";
        string business = "";
        string address1 = "";
        string address2 = "";
        string phoneNo1 = "";
        string phoneNo2 = "";
        string emailID = "";
        Label lbl;
        TextBox txt;
        lbl = lvContact.Items[e.ItemIndex].FindControl("lblEditSlNo") as Label;
        slno = lbl.Text;
        txt = lvContact.Items[e.ItemIndex].FindControl("txtEditContact") as TextBox;
        contact = txt.Text;
        txt = lvContact.Items[e.ItemIndex].FindControl("txtEditBusiness") as TextBox;
        business = txt.Text;
        txt = lvContact.Items[e.ItemIndex].FindControl("txtEditAddress1") as TextBox;
        address1 = txt.Text;
        txt = lvContact.Items[e.ItemIndex].FindControl("txtEditAddress2") as TextBox;
        address2 = txt.Text;
        txt = lvContact.Items[e.ItemIndex].FindControl("txtPhone1") as TextBox;
        phoneNo1 = txt.Text;
        txt = lvContact.Items[e.ItemIndex].FindControl("txtPhone2") as TextBox;
        phoneNo2 = txt.Text;
        txt = lvContact.Items[e.ItemIndex].FindControl("txtEmail") as TextBox;
        emailID = txt.Text;
        DataTable dt = (DataTable)ViewState["ContactTable"];
        foreach (DataRow dr in dt.Rows)
        {
            if (dr["slno"].ToString() == slno)
            {
                dr["ContactPersonName"] = contact;
                dr["BusinessArea"] = business;
                dr["Address1"] = address1;
                dr["Address2"] = address2;
                dr["PhoneNo1"] = phoneNo1;
                dr["PhoneNo2"] = phoneNo2;
                dr["EmailID"] = emailID;               
            }
        }
        lvContact.EditIndex = -1;
        lvContact.DataSource = (DataTable)ViewState["ContactTable"];
        lvContact.DataBind();
    }
    protected void btnFind_Click(object sender, EventArgs e)
    {
        con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
        string sql = "select CompanyName,Address1,Address2,City,Pincode,Phone1,Phone2,FaxNo,EmailID1,EmailID2,IndustryType,State,Country from CompanyMaster where CompanyID like '" + txtCompanyID.Text.Trim() + "'";
        con.Open();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = sql;
        cmd.CommandType = CommandType.Text;
        cmd.Connection = con;
        SqlDataReader dr;
        dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            if (!dr.IsDBNull(0)) { txtCompanyName.Text = dr.GetString(0).ToString(); }
            else { txtCompanyName.Text = ""; }
            if (!dr.IsDBNull(1)) { txtAddress1.Text = dr.GetString(1).ToString(); }
            else { txtAddress1.Text = ""; }
            if (!dr.IsDBNull(2)) { txtAddress2.Text = dr.GetString(2).ToString(); }
            else { txtAddress2.Text = ""; }
            if (!dr.IsDBNull(3)) { txtCity.Text = dr.GetString(3).ToString(); }
            else { txtCity.Text = ""; }
            if (!dr.IsDBNull(4)) { txtPincode.Text = dr.GetString(4).ToString(); }
            else { txtPincode.Text = ""; }
            if (!dr.IsDBNull(5)) { txtPhone1.Text = dr.GetString(5).ToString(); }
            else { txtPhone1.Text = ""; }
            if (!dr.IsDBNull(6)) { txtPhone2.Text = dr.GetString(6).ToString(); }
            else { txtPhone2.Text = ""; }
            if (!dr.IsDBNull(7)) { txtFax.Text = dr.GetString(7).ToString(); }
            else { txtFax.Text = ""; }
            if (!dr.IsDBNull(8)) { txtEmail1.Text = dr.GetString(8).ToString(); }
            else { txtEmail1.Text = ""; }
            if (!dr.IsDBNull(9)) { txtEmail2.Text = dr.GetString(9).ToString(); }
            else { txtEmail2.Text = ""; }
            if (!dr.IsDBNull(10)) { txtIndustry.Text = dr.GetString(10).ToString(); }
            else { txtIndustry.Text = ""; }
            if (!dr.IsDBNull(11)) { txtState.Text = dr.GetString(11).ToString(); }
            else { txtState.Text = ""; }
            if (!dr.IsDBNull(12)) { txtCountry.Text = dr.GetString(12).ToString(); }
            else { txtCountry.Text = ""; }
            txtCompanyID.Enabled = false;
        }
        dr.Close();
        cmd.Cancel();
        DataTable dtBA= (DataTable) ViewState["BATable"];
        string sql2 = "select SlNo,BusinessArea,Products from companyBusinessArea where companyID like '" + txtCompanyID.Text.Trim() + "' order by slno";
        SqlDataAdapter sda = new SqlDataAdapter(sql2, con);
        sda.Fill(dtBA);
        dtBA.AcceptChanges();
        ViewState["BATable"] = dtBA;
        lvBusiness.DataSource = dtBA;
        lvBusiness.DataBind();
        sda.Dispose();

        DataTable dtCD = (DataTable)ViewState["ContactTable"];
        string sql3 = "select SlNo,ContactPersonName,BusinessArea,Address1,Address2,PhoneNo1,PhoneNo2,EmailID from companyContactDetails where companyID like '" +  txtCompanyID.Text.Trim() + "' order by slno";
        SqlDataAdapter sda1 = new SqlDataAdapter(sql3, con);
        sda1.Fill(dtCD);
        dtCD.AcceptChanges();
        ViewState["ContactTable"] = dtCD;
        lvContact.DataSource = dtCD;
        lvContact.DataBind();
        con.Close(); 
    }
}
