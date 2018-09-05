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


public partial class MarketingCompany : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    SqlTransaction trans;            
    protected void Page_Load(object sender, EventArgs e)
    {
        if (User.Identity.IsAuthenticated)
        {
            if (User.IsInRole("Admin") || User.IsInRole("Super User") || User.IsInRole("Marketing"))
            {
                if (!this.IsPostBack)
                {
                    con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
                    SqlCommand cmd1 = new SqlCommand();
                    string sql1 = "SELECT MAX(CONVERT(BIGINT,SUBSTRING(COMPANYID,2,LEN(COMPANYID))))+1 FROM COMPANYMASTER";
                    cmd1.CommandText = sql1;
                    cmd1.CommandType = CommandType.Text;
                    cmd1.Connection = con;
                    con.Open();
                    txtCompanyID.Text = "M" + Convert.ToString(cmd1.ExecuteScalar());
                    con.Close();

                    Company ds = new Company();
                    lvBusiness.DataSource = ds.Tables["BusinessAreaDetails"];
                    lvBusiness.DataBind();
                    ViewState["BATable"] = ds.Tables["BusinessAreaDetails"];
                    lvContact.DataSource = ds.Tables["ContactDetails"];
                    lvContact.DataBind();
                    ViewState["ContactTable"] = ds.Tables["ContactDetails"];
                    aBusinessArea.ServerClick += new EventHandler(EnableBusiness);
                    aContact.ServerClick += new EventHandler(aContact_ServerClick);
                    imgBtnSubmit.Visible = (!User.IsInRole("View"));
                }
            }
            else
            {
                Server.Transfer("Unautherized.aspx");
            }
        }
    }

    protected void lvBusiness_ItemEditing(object sender, ListViewEditEventArgs e)
    {
        lvBusiness.EditIndex = e.NewEditIndex;
        lvBusiness.DataSource = (DataTable) ViewState["BATable"];
        lvBusiness.DataBind();
    }

    protected void EnableBusiness(object sender,EventArgs e)
    {
            divBusiness.Visible = true;
    }

    protected void aContact_ServerClick(object sender, EventArgs e)
    {
        divContact.Visible = true;
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
            if (dr["SlNo"].ToString()==slno)
            {
                dr["BusinessArea"] = business;
                dr["Products"] = product;
                dr.AcceptChanges();
            }
        }
        lvBusiness.EditIndex = -1;
        lvBusiness.DataSource = (DataTable)ViewState["BATable"];
        lvBusiness.DataBind();       
    }
    protected void lvBusiness_ItemDeleting(object sender, ListViewDeleteEventArgs e)
    {
        Label lbl = lvBusiness.Items[e.ItemIndex].FindControl("lblSlNo") as Label;
        DataTable dt = (DataTable)ViewState["BATable"];
        int cnt = 0;
        foreach (DataRow dr in dt.Rows)
        {
            cnt += 1;
            if (dr["SlNo"].ToString() == lbl.Text.Trim())
            {
                dr.Delete();
                break;
            }
        }
        for (int i = cnt - 1; i <= dt.Rows.Count - 1; i++)
        {
            dt.Rows[i][0] = i+1;
        }
        lvBusiness.DataSource = (DataTable)ViewState["BATable"];
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
    protected void lvContact_ItemDeleting(object sender, ListViewDeleteEventArgs e)
    {
        Label lbl= lvContact.Items[e.ItemIndex].FindControl("lblSlNo") as Label;
        DataTable dt = (DataTable)ViewState["ContactTable"];
        int cnt = 0;
        foreach (DataRow dr in dt.Rows)
        {
            cnt += 1;
            if (dr["slno"].ToString() == lbl.Text.Trim())
            {
                dr.Delete();
                break;
            }
        }
        for (int i = cnt-1; i <= dt.Rows.Count-1; i++)
        {
            dt.Rows[i][0] = i+1;
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
        dr["EmailID"] =emailID;
        dt.Rows.Add(dr);
        lvContact.DataSource = (DataTable)ViewState["ContactTable"];
        lvContact.DataBind();
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
                dr.AcceptChanges();
            }
        }
        lvContact.EditIndex = -1;       
        lvContact.DataSource = (DataTable)ViewState["ContactTable"];
        lvContact.DataBind();
    }
    protected void imgBtnSubmit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (txtCompanyName.Text.Trim() == "")
            {
                ClientScript.RegisterStartupScript(GetType(), "Success", "<script>alert('Please Enter Company Name')</script>");
                return;
            }

            SqlCommand cmd = new SqlCommand();
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
            con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
            string sql = "insert into companyMaster (EntryDt,CompanyID,CompanyName,Address1,Address2,City,Pincode,Phone1,Phone2,FaxNo,EmailID1,EmailID2,IndustryType,State,Country) values" +
            "(convert(smalldatetime,@EntryDt,103),@CompanyID,@CompanyName,case when @Address1='' then null else @Address1 end,case when @Address2='' then null else @Address2 end,case when @City='' then null else @City end," +
            "case when @Pincode='' then null else @Pincode end,case when @Phone1='' then null else @Phone1 end,case when @Phone2='' then null else @Phone2 end,case when @FaxNo='' then null else @FaxNo end,"+
            "case when @EmailID1='' then null else @EmailID1 end,case when @EmailID2='' then null else @EmailID2 end,case when @IndustryType='' then null else @IndustryType end,case when @State='' then null else @State end,case when @Country='' then null else @Country end)";
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            con.Open();
            trans = con.BeginTransaction();
            cmd.Transaction = trans;

            SqlConnection con1 = new SqlConnection();
            con1.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
            SqlCommand cmd1 = new SqlCommand();
            string sql1 = "SELECT MAX(CONVERT(BIGINT,SUBSTRING(COMPANYID,2,LEN(COMPANYID))))+1 FROM COMPANYMASTER";
            cmd1.CommandText = sql1;
            cmd1.CommandType = CommandType.Text;
            cmd1.Connection = con1;
            con1.Open();
            string custID = "M" + Convert.ToString(cmd1.ExecuteScalar());
            con1.Close();

            SqlParameter pm1 = new SqlParameter();
            pm1.ParameterName = "@CompanyID";
            pm1.SourceColumn = "CompanyID";
            pm1.Value = custID;
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
            pm13.ParameterName = "@EntryDt";
            pm13.SourceColumn = "EntryDt";
            pm13.Value = DateTime.Now.ToShortDateString();
            pm13.DbType = DbType.String;
            pm13.Direction = ParameterDirection.Input;

            SqlParameter pm14 = new SqlParameter();
            pm14.ParameterName = "@State";
            pm14.SourceColumn = "State";
            pm14.Value = txtState.Text.Trim();
            pm14.DbType = DbType.String;
            pm14.Direction = ParameterDirection.Input;

            SqlParameter pm15 = new SqlParameter();
            pm15.ParameterName = "@Country";
            pm15.SourceColumn = "Country";
            pm15.Value = txtCountry.Text.Trim();
            pm15.DbType = DbType.String;
            pm15.Direction = ParameterDirection.Input;

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

            cmd.ExecuteNonQuery();

            DataTable dt = (DataTable)ViewState["BATable"];
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
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
                    bpm1.Value = custID;
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
            DataTable ContactDT = (DataTable)ViewState["ContactTable"];
            if (ContactDT.Rows.Count > 0)
            {
                foreach (DataRow dr in ContactDT.Rows)
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
                    cpm1.Value = custID;
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

            trans.Commit();

            string str2 = "This Record successfully Added, Your Customer ID is : "+custID;
            ClientScript.RegisterStartupScript(GetType(), "Success", "<script>alert('" + str2 + "')</script>");
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

        con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
        SqlCommand cmd1 = new SqlCommand();
        string sql1 = "SELECT MAX(CONVERT(BIGINT,SUBSTRING(COMPANYID,2,LEN(COMPANYID))))+1 FROM COMPANYMASTER";
        cmd1.CommandText = sql1;
        cmd1.CommandType = CommandType.Text;
        cmd1.Connection = con;
        con.Open();
        txtCompanyID.Text = "M" + Convert.ToString(cmd1.ExecuteScalar());
        con.Close();
    }
}
