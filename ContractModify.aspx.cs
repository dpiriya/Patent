using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Security;
using System.IO;
using System.Web.Services;

public partial class ContractModify : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    protected void Page_Load(object sender, EventArgs e)
    
    {
        if (User.IsInRole("Admin") || User.IsInRole("Super User") || User.IsInRole("Marketing") || !User.IsInRole("Intern"))
        {
            if (!this.IsPostBack)
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
                string sql = "SELECT ITEMLIST,ITEMLIST + ' - ' + DESCRIPTION AS ITEM_DESC FROM LISTITEMMASTER WHERE CATEGORY='ContractAgreementType' ORDER BY ITEMLIST";
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = sql;
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                SqlDataReader dr;
                dr = cmd.ExecuteReader();
                ddlAgreeType.DataTextField = "ITEM_DESC";
                ddlAgreeType.DataValueField = "ITEMLIST";
                ddlAgreeType.DataSource = dr;
                ddlAgreeType.DataBind();
                ddlAgreeType.Items.Insert(0, new ListItem("", ""));
                dr.Close();
                sql = "select DeptCode from department order by DeptCode";
                cmd.CommandText = sql;
                dr = cmd.ExecuteReader();
                ddlDept.DataTextField = "DeptCode";
                ddlDept.DataValueField = "DeptCode";
                ddlDept.DataSource = dr;
                ddlDept.DataBind();
                ddlDept.Items.Insert(0, new ListItem("", ""));
                dr.Close();
                sql = "select ItemList from ListItemMaster where Category='Contract' and Grouping='Status'";
                cmd.CommandText = sql;
                dr = cmd.ExecuteReader();
                dropStatus.DataTextField = "ItemList";
                dropStatus.DataValueField = "ItemList";
                dropStatus.DataSource = dr;
                dropStatus.DataBind();
                dr.Close();
                
                //sql = "SELECT ITEMLIST FROM LISTITEMMASTER WHERE CATEGORY='ContractActionType' ORDER BY ITEMLIST";
                //cmd.CommandText = sql;
                //dr = cmd.ExecuteReader();
                //ddlAgreeType.DataTextField = "itemList";
                //ddlAgreeType.DataValueField = "itemList";
                //ddlAgreeType.DataSource = dr;
                //ddlAgreeType.DataBind();
                //ddlAgreeType.Items.Insert(0, new ListItem("", ""));
                //con.Close();
                //ddlActStatus.Items.Add("Progress");
                //ddlActStatus.Items.Add("Closed");
                lvAction.DataSource = null;
                lvAction.DataBind();
                Tab1.CssClass = "Clicked";
                MainView.ActiveViewIndex = 0;
                btnUpload.Visible = false;
                btnNewAction.Visible = false;
                con.Close();
            }
        }
        else
        {
            Server.Transfer("Unauthorized.aspx");
        }
    }
    [WebMethod]
    public static List<string> GetCompanyName(string company)
    {
        List<string> result = new List<string>();
        SqlConnection cnp = new SqlConnection();
        cnp.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
        SqlCommand cmd = new SqlCommand();
        string sql = "select companyName from companyMaster where companyName like '%'+ @SearchText +'%' group by companyName order by companyName";
        SqlDataReader dr;
        cmd.CommandType = CommandType.Text;
        cmd.Parameters.AddWithValue("@SearchText", company);
        cmd.Connection = cnp;
        cmd.CommandText = sql;
        cnp.Open();
        dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            result.Add(dr["companyName"].ToString());
        }
        return result;
    }
    protected void Tab1_Click(object sender, EventArgs e)
    {
        Tab1.CssClass = "Clicked";
        Tab2.CssClass = "Initial";
        Tab3.CssClass = "Initial";
        MainView.ActiveViewIndex = 0;
    }
    protected void Tab2_Click(object sender, EventArgs e)
    {
        Tab2.CssClass = "Clicked";
        Tab1.CssClass = "Initial";
        Tab3.CssClass = "Initial";
        MainView.ActiveViewIndex = 1;       
    }

    protected void Tab3_Click(object sender, EventArgs e)
    {
        Tab3.CssClass = "Clicked";
        Tab1.CssClass = "Initial";
        Tab2.CssClass = "Initial";
        MainView.ActiveViewIndex = 2;
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {

    }
    protected void btnModify_Click(object sender, EventArgs e)
    {

    }
    protected void btnFind_Click(object sender, EventArgs e)
    {
       

        txtContractNo.Text = txtContractNo.Text.Trim().ToUpper();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        string sql = "SELECT AgreementType,AgreementNo,Title,Scope,Party,CoordinatingPerson,CoorCode,Dept,EffectiveDt,ExpiryDt,Remark,Status,TechTransfer FROM AGREEMENT WHERE CONTRACTNO LIKE '" + txtContractNo.Text.Trim() + "'";
        con.Open();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = sql;
        cmd.CommandType = CommandType.Text;
        cmd.Connection = con;
        SqlDataReader dr;
        dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            if (!dr.IsDBNull(0)) ddlAgreeType.Text = dr.GetString(0); else ddlAgreeType.Text = "";
            if (!dr.IsDBNull(2)) txtTitle.Text = dr.GetString(2); else txtTitle.Text = "";
            if (!dr.IsDBNull(3)) txtScope.Text = dr.GetString(3); else txtScope.Text = "";
            if (!dr.IsDBNull(4)) txtParty.Text = dr.GetString(4); else txtParty.Text = "";
            if (!dr.IsDBNull(7)) ddlDept.SelectedValue = dr.GetString(7); else ddlDept.Text = "";
            ddlDept_SelectedIndexChanged(sender, e);
            if (!dr.IsDBNull(6)) ddlCoor.SelectedValue = dr.GetString(6); else ddlCoor.Text = "";
            if (!dr.IsDBNull(8)) txtEffectiveDt.Text = dr.GetDateTime(8).ToShortDateString(); else txtEffectiveDt.Text = "";
            if (!dr.IsDBNull(9)) txtExpiryDt.Text = dr.GetDateTime(9).ToShortDateString(); else txtExpiryDt.Text = "";
            if (!dr.IsDBNull(10)) txtRemark.Text = dr.GetString(10); else txtRemark.Text = "";
            if (!dr.IsDBNull(1)) txtAgmtNo.Text = dr.GetString(1); else txtAgmtNo.Text = "";
            if (!dr.IsDBNull(11)) dropStatus.SelectedValue = dr.GetString(11); else dropStatus.SelectedValue = "";
            if (!dr.IsDBNull(12)) txttrans.Text = dr.GetString(12); else txttrans.Text = "";
            dr.Close();
            txtContractNo.Enabled = false;
            btnUpdate.Visible = (!User.IsInRole("View"));
            btnNewAction.Visible = (!User.IsInRole("View"));
            btnUpload.Visible = (!User.IsInRole("View"));
            FetchAction(con, txtContractNo.Text.Trim());
            fetchFiles(con, txtContractNo.Text.Trim());
            con.Close();
            Tab1_Click(sender, e);
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "Contract Number Is Not Found", "<script>alert('Contract Number Is Not Found')</script>");
            con.Close();
            return;
        }
    }
    protected void FetchAction(SqlConnection con, string ContractNo)
    {
        DataTable dtAction = new DataTable();
        string sql2 = "select SlNo,Event,Frequency,ExecutionDate,Basis,DeclDueAmt,InvoiceNo,InvoiceRaised,RecieptNo,RecieptDate,AmountInRs,Status,Remarks from Royality where ContractNo ='" + txtContractNo.Text.Trim() + "' order by slno desc";
        SqlDataAdapter sda = new SqlDataAdapter(sql2, con);
        sda.Fill(dtAction);
        lvAction.DataSource = dtAction;
        lvAction.DataBind();
        sda.Dispose();      
    }
    protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlCoor.Items.Clear();
        SqlConnection cn = new SqlConnection();
        cn.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
        SqlCommand cmd = new SqlCommand();
        string sql = "select CoorCode,CoorName from FacultyMaster where deptcode like '" + ddlDept.SelectedValue.Trim() + "' ORDER BY CoorName";
        SqlDataReader dr;
        cmd.CommandType = CommandType.Text;
        cmd.Connection = cn;
        cmd.CommandText = sql;
        cn.Open();
        dr = cmd.ExecuteReader();
        ddlCoor.DataTextField = "CoorName";
        ddlCoor.DataValueField = "CoorCode";
        ddlCoor.DataSource = dr;
        ddlCoor.DataBind();
        ddlCoor.Items.Insert(0, new ListItem("", ""));
        cn.Close();
    }
    protected void lvAction_ItemDeleting(object sender, ListViewDeleteEventArgs e)
    {
        try
        {
            ListViewDataItem lvd = lvAction.Items[e.ItemIndex];
            Label tmplabel = lvd.FindControl("lblSlNo") as Label;
            int SlNo = Convert.ToInt32(tmplabel.Text.Trim());
            con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
            string sql = "delete from Royality where ContractNo ='" + txtContractNo.Text.Trim() + "' and Slno=" + SlNo+"";
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            cmd.ExecuteNonQuery();
            FetchAction(con, txtContractNo.Text.Trim());
            ClientScript.RegisterStartupScript(GetType(), "success", "<script>alert('This Record Successfully Deleted')</script>");
            con.Close();

        }
        catch(Exception ex)
        {
            ClientScript.RegisterStartupScript(GetType(), "Error", "<script>alert('" + ex.Message.ToString() + "')</script>");
            con.Close();
            return;
        }     
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        string sql = "SELECT AgreementType,AgreementNo,Title,Scope,Party,CoordinatingPerson,CoorCode,Dept,EffectiveDt,ExpiryDt,Remark,TechTransfer FROM AGREEMENT WHERE CONTRACTNO LIKE '" + txtContractNo.Text.Trim() + "'";
        con.Open();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = sql;
        cmd.CommandType = CommandType.Text;
        cmd.Connection = con;
        SqlDataReader dr;
        dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            if (!dr.IsDBNull(0)) ddlAgreeType.Text = dr.GetString(0); else ddlAgreeType.Text = "";
            if (!dr.IsDBNull(2)) txtTitle.Text = dr.GetString(2); else txtTitle.Text = "";
            if (!dr.IsDBNull(3)) txtScope.Text = dr.GetString(3); else txtScope.Text = "";
            if (!dr.IsDBNull(4)) txtParty.Text = dr.GetString(4); else txtParty.Text = "";
            if (!dr.IsDBNull(7)) ddlDept.SelectedValue = dr.GetString(7); else ddlDept.Text = "";
            ddlDept_SelectedIndexChanged(sender, e);
            if (!dr.IsDBNull(6)) ddlCoor.SelectedValue = dr.GetString(6); else ddlCoor.Text = "";
            if (!dr.IsDBNull(8)) txtEffectiveDt.Text = dr.GetDateTime(8).ToShortDateString(); else txtEffectiveDt.Text = "";
            if (!dr.IsDBNull(9)) txtExpiryDt.Text = dr.GetDateTime(9).ToShortDateString(); else txtExpiryDt.Text = "";
            if (!dr.IsDBNull(10)) txtRemark.Text = dr.GetString(10); else txtRemark.Text = "";
            if (!dr.IsDBNull(1)) txtAgmtNo.Text = dr.GetString(1); else txtAgmtNo.Text = "";
            if (!dr.IsDBNull(11)) txttrans.Text = dr.GetString(11); else txttrans.Text = "";
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "Contract Number Is Not Found", "<script>alert('Contract Number Is Not Found')</script>");
            dr.Close();
            con.Close();
            return;
        }
        dr.Close();
        con.Close();
        btnContract.Visible = true;
        btnUpdate.Visible = false;
    }
    protected void btnContract_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtContractNo.Text))
        {
            ClientScript.RegisterStartupScript(GetType(), "Contract Number", "<script>alert('Agreement No Can not be Empty')</script>");
            return;
        }
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
        con.Open();
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "ContractModify";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            SqlParameter pm2 = new SqlParameter();
            pm2.ParameterName = "@ContractNo";
            pm2.SourceColumn = "ContractNo";
            pm2.Value = txtContractNo.Text.Trim();
            pm2.DbType = DbType.String;
            pm2.Direction = ParameterDirection.Input;

            SqlParameter pm3 = new SqlParameter();
            pm3.ParameterName = "@AgreementType";
            pm3.SourceColumn = "AgreementType";
            if (string.IsNullOrEmpty(ddlAgreeType.SelectedValue)) pm3.Value = DBNull.Value; else pm3.Value = ddlAgreeType.SelectedValue;
            pm3.DbType = DbType.String;
            pm3.Direction = ParameterDirection.Input;

            SqlParameter pm4 = new SqlParameter();
            pm4.ParameterName = "@Title";
            pm4.SourceColumn = "Title";
            if (txtTitle.Text.Trim() == "") pm4.Value = DBNull.Value; else pm4.Value = txtTitle.Text.Trim();
            pm4.DbType = DbType.String;
            pm4.Direction = ParameterDirection.Input;

            SqlParameter pm5 = new SqlParameter();
            pm5.ParameterName = "@Scope";
            pm5.SourceColumn = "Scope";
            if (txtScope.Text.Trim() == "") pm5.Value = DBNull.Value; else pm5.Value = txtScope.Text.Trim();
            pm5.DbType = DbType.String;
            pm5.Direction = ParameterDirection.Input;

            SqlParameter pm6 = new SqlParameter();
            pm6.ParameterName = "@Party";
            pm6.SourceColumn = "Party";
            if (txtParty.Text.Trim() == "") pm6.Value = DBNull.Value; else pm6.Value = txtParty.Text.Trim();
            pm6.DbType = DbType.String;
            pm6.Direction = ParameterDirection.Input;

            SqlParameter pm7 = new SqlParameter();
            pm7.ParameterName = "@Dept";
            pm7.SourceColumn = "Dept";
            if (ddlDept.SelectedItem.Text.Trim() == "") pm7.Value = DBNull.Value; else pm7.Value = ddlDept.SelectedItem.Text.Trim();
            pm7.DbType = DbType.String;
            pm7.Direction = ParameterDirection.Input;

            SqlParameter pm8 = new SqlParameter();
            pm8.ParameterName = "@CoorCode";
            pm8.SourceColumn = "CoorCode";
            if (ddlCoor.SelectedValue.Trim() == "") pm8.Value = DBNull.Value; else pm8.Value = ddlCoor.SelectedValue.Trim();
            pm8.DbType = DbType.String;
            pm8.Direction = ParameterDirection.Input;

            SqlParameter pm9 = new SqlParameter();
            pm9.ParameterName = "@CoordinatingPerson";
            pm9.SourceColumn = "CoordinatingPerson";
            if (ddlCoor.SelectedItem.Text.Trim() == "") pm9.Value = DBNull.Value; else pm9.Value = ddlCoor.SelectedItem.Text.Trim();
            pm9.DbType = DbType.String;
            pm9.Direction = ParameterDirection.Input;

            SqlParameter pm10 = new SqlParameter();
            pm10.ParameterName = "@EffectiveDt";
            pm10.SourceColumn = "EffectiveDt";
            if (txtEffectiveDt.Text.Trim() == "")
                pm10.Value = DBNull.Value;
            else
            {
                DateTime Effective;
                if (DateTime.TryParse(txtEffectiveDt.Text.Trim(), out Effective))
                    pm10.Value = txtEffectiveDt.Text.Trim();
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "Effective Date", "<script>alert('Verify Effective Date')</script>");
                    return;
                }
            }
            pm10.DbType = DbType.String;
            pm10.Direction = ParameterDirection.Input;

            SqlParameter pm11 = new SqlParameter();
            pm11.ParameterName = "@ExpiryDt";
            pm11.SourceColumn = "ExpiryDt";
            if (txtExpiryDt.Text.Trim() == "")
                pm11.Value = DBNull.Value;
            else
            {
                DateTime Expiry;
                if (DateTime.TryParse(txtExpiryDt.Text.Trim(), out Expiry))
                    pm11.Value = txtExpiryDt.Text.Trim();
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "Expiry Date", "<script>alert('Verify Expiry Date')</script>");
                    return;
                }
            }
            pm11.DbType = DbType.String;
            pm11.Direction = ParameterDirection.Input;

            SqlParameter pm12 = new SqlParameter();
            pm12.ParameterName = "@Remark";
            pm12.SourceColumn = "Remark";
            if (txtRemark.Text.Trim() == "") pm12.Value = DBNull.Value; else pm12.Value = txtRemark.Text.Trim();
            pm12.DbType = DbType.String;
            pm12.Direction = ParameterDirection.Input;

            SqlParameter pm13 = new SqlParameter();
            pm13.ParameterName = "@UserName";
            pm13.SourceColumn = "UserName";
            pm13.Value = Membership.GetUser().UserName.ToString();
            pm13.DbType = DbType.String;
            pm13.Direction = ParameterDirection.Input;

            SqlParameter pm14 = new SqlParameter();
            pm14.ParameterName = "@AgreementNo";
            pm14.SourceColumn = "AgreementNo";
            if (txtAgmtNo.Text.Trim() == "") pm14.Value = DBNull.Value; else pm14.Value = txtAgmtNo.Text.Trim();
            pm14.DbType = DbType.String;
            pm14.Direction = ParameterDirection.Input;

            SqlParameter pm15 = new SqlParameter();
            pm15.ParameterName = "@status";
            pm15.SourceColumn = "status";
            if (dropStatus.SelectedItem.Text.Trim() == "") pm15.Value = DBNull.Value; else pm15.Value = dropStatus.SelectedItem.Text.Trim();
            pm5.DbType = DbType.String;
            pm5.Direction = ParameterDirection.Input;

            SqlParameter pm16 = new SqlParameter();
            pm16.ParameterName = "@TechTransfer";
            pm16.SourceColumn = "TechTransfer";
            if (txttrans.Text.Trim() == "") pm16.Value = DBNull.Value; else pm16.Value = txttrans.Text.Trim();
            pm16.DbType = DbType.String;
            pm16.Direction = ParameterDirection.Input;

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

            ClientScript.RegisterStartupScript(GetType(), "success", "<script>alert('This Record successfully Updated')</script>");
            con.Close();

            btnContract.Visible = false;
            btnUpdate.Visible = true;
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(GetType(), "Error", "<script>alert('" + ex.Message.ToString() + "')</script>");
            con.Close();
            return;
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtContractNo.Text = "";
        txtContractNo.Enabled = true;
        ddlAgreeType.Text = "";
        txtTitle.Text = "";
        txtScope.Text = "";
        txtParty.Text = "";
        ddlDept.Text = "";
        ddlCoor.Items.Clear();
        txtEffectiveDt.Text = "";
        txtExpiryDt.Text = "";
        txtRemark.Text = "";
        txtAgmtNo.Text = "";
        btnContract.Visible = false;
        btnUpdate.Visible = false;
        btnNewAction.Visible = false;
        ClearAction();
        panUpdate.Visible = false;
        panNewFile.Visible = false;
        btnUpload.Visible = false;
        ClearFileUpload();
        using (DataTable dt = new DataTable())
        {
           lvAction.DataSource = dt;
           lvAction.DataBind();
            gvFileDetails.DataSource = dt;
            gvFileDetails.DataBind();
        }
        Tab1_Click(sender, e);
    }
    
    protected void lvAction_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        LinkButton lb = e.Item.FindControl("lbtnDelete") as LinkButton;
        LinkButton lbUpdate = e.Item.FindControl("lbtnUpdate") as LinkButton;
        if (User.IsInRole("Admin") || User.IsInRole("Super User"))
        {
            lb.Visible = true;                
        }
        else
        {
            lb.Visible = false;
        }
        lbUpdate.Enabled = (!User.IsInRole("View"));
    }
    
    protected void btnNewAction_Click(object sender, EventArgs e)
    {
        panUpdate.Visible = true;
        thTitle.InnerText = "New Action";
        btnNewAction.Visible = false;
        btnActSave.Visible = true;
        btnActUpdate.Visible = false;
        ClearAction();
        int tmpSlNo;
        FindActionSlNO (txtContractNo.Text.Trim(), out tmpSlNo);
      lblSlNo.Text = tmpSlNo.ToString();
        //ddlActStatus.SelectedIndex = 0;       
    }
    protected void FindActionSlNO(string ContractNo,out int SlNo)
    {
        SqlConnection cn = new SqlConnection();
        cn.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
        cn.Open();
        SqlCommand cmd21 = new SqlCommand();
        cmd21.CommandText = "select max(slno) from Royality where contractNo ='" + ContractNo + "'";
        cmd21.CommandType = CommandType.Text;
        cmd21.Connection = cn;
        if (cmd21.ExecuteScalar() == DBNull.Value) SlNo = 1; else SlNo= Convert.ToInt32(cmd21.ExecuteScalar())+1;
        cn.Close();
    }
    protected void btnActSave_Click(object sender, EventArgs e)
    {
        
        if (string.IsNullOrEmpty(txtContractNo.Text))
        {
            ClientScript.RegisterStartupScript(GetType(), "Contract Number", "<script>alert('Agreement No Can not be Empty')</script>");
            return;
        }
        if (lblSlNo.Text.Trim() == "")
        {
            ClientScript.RegisterStartupScript(GetType(), "SlNo", "<script>alert('Sl.No. can not be empty')</script>");
            return;
        }
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
        con.Open();
        try
        {
          
            SqlCommand cmd3 = new SqlCommand();
            cmd3.CommandText = "ContractRoyality";
            cmd3.CommandType = CommandType.StoredProcedure;
            cmd3.Connection = con;

            SqlParameter apm1 = new SqlParameter();
            apm1.ParameterName = "@EntryDt";
            apm1.SourceColumn = "EntryDt";
            apm1.Value = DateTime.Now.ToShortDateString();
            apm1.DbType = DbType.String;
            apm1.Direction = ParameterDirection.Input;

            SqlParameter apm2 = new SqlParameter();
            apm2.ParameterName = "@ContractNo";
            apm2.SourceColumn = "ContractNo";
            apm2.Value = txtContractNo.Text.Trim();
            apm2.DbType = DbType.String;
            apm2.Direction = ParameterDirection.Input;

            SqlParameter apm3 = new SqlParameter();
            apm3.ParameterName = "@SlNo";
            apm3.SourceColumn = "SlNo";
            apm3.Value = lblSlNo.Text.Trim();
            apm3.DbType = DbType.Int32;
            apm3.Direction = ParameterDirection.Input;

            //SqlParameter apm4 = new SqlParameter();
            //apm4.ParameterName = "@Request";
            //apm4.SourceColumn = "Request";
            //apm4.Value = txtRequest.Text.Trim();
            //apm4.DbType = DbType.String;
            //apm4.Direction = ParameterDirection.Input;



            SqlParameter apm6 = new SqlParameter();
            apm6.ParameterName = "@Basis";
            apm6.SourceColumn = "Basis";
            apm6.Value = txtBasis.Text.Trim();
            apm6.DbType = DbType.String;
            apm6.Direction = ParameterDirection.Input;


            SqlParameter apm11 = new SqlParameter();
            apm11.ParameterName = "@DeclDueAmt";
            apm11.SourceColumn = "DeclDueAmt";
            apm11.Value = txtDeclAmt.Text.Trim();
            apm11.DbType = DbType.String;
            apm11.Direction = ParameterDirection.Input;


            SqlParameter apm12 = new SqlParameter();
            apm12.ParameterName = "@InvoiceNo";
            apm12.SourceColumn = "@InvoiceNo";
            apm12.Value = txtInvoiceNo.Text.Trim();
            apm12.DbType = DbType.String;
            apm12.Direction = ParameterDirection.Input;

            SqlParameter apm13 = new SqlParameter();
            apm13.ParameterName = "@InvoiceRaised";
            apm13.SourceColumn = "InvoiceRaised";
            if (txtInvoiceDt.Text.Trim() == "")
                apm13.Value = DBNull.Value;
            else
            {
                DateTime Expiry;
                if (DateTime.TryParse(txtInvoiceDt.Text.Trim(), out Expiry))
                    apm13.Value = txtInvoiceDt.Text.Trim();
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "Expiry Date", "<script>alert('Verify Invoice Date')</script>");
                    return;
                }
            }
            apm13.DbType = DbType.String;
            apm13.Direction = ParameterDirection.Input;


            SqlParameter apm14 = new SqlParameter();
            apm14.ParameterName = "@AmtInFC";
            apm14.SourceColumn = "AmtInFC";
            apm14.Value = txtAmtInFC.Text.Trim();
            apm14.DbType = DbType.String;
            apm14.Direction = ParameterDirection.Input;

            SqlParameter apm15 = new SqlParameter();
            apm15.ParameterName = "@ConvFactor";
            apm15.SourceColumn = "ConvFactor";
            apm15.Value = txtCF.Text.Trim();
            apm15.DbType = DbType.String;
            apm15.Direction = ParameterDirection.Input;

            SqlParameter apm16 = new SqlParameter();
            apm16.ParameterName = "@AmountInRs";
            apm16.SourceColumn = "AmountInRs";
            apm16.Value = txtAmtInRs.Text.Trim();
            apm16.DbType = DbType.String;
            apm16.Direction = ParameterDirection.Input;

            SqlParameter apm17 = new SqlParameter();
            apm17.ParameterName = "@Frequency";
            apm17.SourceColumn = "Frequency";
            apm17.Value = ddl2.SelectedValue.ToString();
            apm17.DbType = DbType.String;
            apm17.Direction = ParameterDirection.Input;


            SqlParameter apm5 = new SqlParameter();
            apm5.ParameterName = "@Event";
            apm5.SourceColumn = "Event";
            apm5.Value = ddl1.SelectedValue.ToString();
            apm5.DbType = DbType.String;
            apm5.Direction = ParameterDirection.Input;

            SqlParameter apm18 = new SqlParameter();
            apm18.ParameterName = "@Status";
            apm18.SourceColumn = "Status";
            apm18.Value = drop1.SelectedValue.ToString();
            apm18.DbType = DbType.String;
            apm18.Direction = ParameterDirection.Input;


            SqlParameter apm19 = new SqlParameter();
            apm19.ParameterName = "@UserName";
            apm19.SourceColumn = "UserName";
            apm19.Value = Membership.GetUser().UserName.ToString();
            apm19.DbType = DbType.String;
            apm19.Direction = ParameterDirection.Input;

            SqlParameter apm20 = new SqlParameter();
            apm20.ParameterName = "@ExecutionDate";
            apm20.SourceColumn = "ExecutionDate";
            apm20.Value = txtexecDt.Text.Trim();
            apm20.DbType = DbType.String;
            apm20.Direction = ParameterDirection.Input;

            SqlParameter apm7 = new SqlParameter();
            apm7.ParameterName = "@RecieptNo";
            apm7.SourceColumn = "RecieptNo";
            if (txtReceiptNo.Text.Trim() == "") apm7.Value = DBNull.Value; else apm7.Value = txtReceiptNo.Text.Trim();
            apm7.DbType = DbType.String;
            apm7.Direction = ParameterDirection.Input;

            SqlParameter apm8 = new SqlParameter();
            apm8.ParameterName = "@Mode";
            apm8.SourceColumn = "Mode";
            apm8.Value = ddlMode.SelectedValue.ToString();
            apm8.DbType = DbType.String;
            apm8.Direction = ParameterDirection.Input;

            SqlParameter apm9 = new SqlParameter();
            apm9.ParameterName = "@RecieptDate";
            apm9.SourceColumn = "RecieptDate";
            if (txtRecieptDate.Text.Trim() == "")
                apm9.Value = DBNull.Value;
            else
            {
                DateTime Expiry;
                if (DateTime.TryParse(txtRecieptDate.Text.Trim(), out Expiry))
                    apm9.Value = txtRecieptDate.Text.Trim();
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "Expiry Date", "<script>alert('Verify Receipt Date')</script>");
                    return;
                }
            }
            apm9.DbType = DbType.String;
            apm9.Direction = ParameterDirection.Input;

            SqlParameter apm10 = new SqlParameter();
            apm10.ParameterName = "@Remarks";
            apm10.SourceColumn = "Remarks";
            apm10.Value = txtRemarks.Text.Trim();
            apm10.DbType = DbType.String;
            apm10.Direction = ParameterDirection.Input;


            cmd3.Parameters.Add(apm1);
            cmd3.Parameters.Add(apm2);
            cmd3.Parameters.Add(apm3);
            //cmd3.Parameters.Add(apm4);
            cmd3.Parameters.Add(apm5);
            cmd3.Parameters.Add(apm6);
            cmd3.Parameters.Add(apm7);
            cmd3.Parameters.Add(apm8);
            cmd3.Parameters.Add(apm9);
            cmd3.Parameters.Add(apm10);
            cmd3.Parameters.Add(apm11);
            cmd3.Parameters.Add(apm12);
            cmd3.Parameters.Add(apm13);
            cmd3.Parameters.Add(apm14);
            cmd3.Parameters.Add(apm15);
            cmd3.Parameters.Add(apm16);
            cmd3.Parameters.Add(apm17);
            cmd3.Parameters.Add(apm18);
            cmd3.Parameters.Add(apm19);
            cmd3.Parameters.Add(apm20);
            cmd3.ExecuteNonQuery();
            con.Close();
           // filldata();

            ClientScript.RegisterStartupScript(GetType(), "success", "<script>alert('This Record successfully Added')</script>");
            FetchAction(con, txtContractNo.Text.Trim());
            con.Close();
            ClearAction();
            panUpdate.Visible = false;
            btnNewAction.Visible = true;
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(GetType(), "Error", "<script>alert('" + ex.Message.ToString() + "')</script>");
            con.Close();
            return;
        }
    }
    protected void ClearAction()
    {
    //    lblSlNo.Text = "";
    //    ddlType.Text = "";
    //    txtNarration.Text = "";
    //    //  txtClauseRef.Text = "";
    //    txtResponsePerson.Text = "";
    //    txtTargetDt.Text = "";
    //    txtActRemark.Text = "";
    }
    protected void btnActUpdate_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtContractNo.Text))
        {
            ClientScript.RegisterStartupScript(GetType(), "Contract Number", "<script>alert('Agreement No Can not be Empty')</script>");
            return;
        }
        if (lblSlNo.Text.Trim() == "")
        {
            ClientScript.RegisterStartupScript(GetType(), "SlNo", "<script>alert('Sl.No. can not be empty')</script>");
            return;
        }
       System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
        con.Open();
        try
        {
           SqlCommand cmd2 = new SqlCommand();
            cmd2.CommandText = "ContractRoyalityModify";
            cmd2.CommandType = CommandType.StoredProcedure;
            cmd2.Connection = con;

            SqlParameter bpm1 = new SqlParameter();
            bpm1.ParameterName = "@EntryDt";
            bpm1.SourceColumn = "EntryDt";
            bpm1.Value = DateTime.Now.ToShortDateString();
            bpm1.DbType = DbType.String;
            bpm1.Direction = ParameterDirection.Input;

            SqlParameter bpm2 = new SqlParameter();
            bpm2.ParameterName = "@SlNo";
            bpm2.SourceColumn = "SlNo";
            bpm2.Value = lblSlNo.Text.Trim();
            bpm2.DbType = DbType.Int32;
            bpm2.Direction = ParameterDirection.Input;

            SqlParameter bpm3 = new SqlParameter();
            bpm3.ParameterName = "@ContractNo";
            bpm3.SourceColumn = "ContractNo";
            bpm3.Value = txtContractNo.Text.Trim();
            bpm3.DbType = DbType.String;
            bpm3.Direction = ParameterDirection.Input;

            SqlParameter bpm4 = new SqlParameter();
            bpm4.ParameterName = "@Status";
            bpm4.SourceColumn = "Status";
            bpm4.Value = drop1.SelectedValue.ToString();
            bpm4.DbType = DbType.String;
            bpm4.Direction = ParameterDirection.Input;

            SqlParameter bpm5 = new SqlParameter();
            bpm5.ParameterName = "@DeclDueAmt";
            bpm5.SourceColumn = "DeclDueAmt";
            if (txtDeclAmt.Text.Trim() == "") bpm5.Value = DBNull.Value; else bpm5.Value = txtDeclAmt.Text.Trim();
            bpm5.DbType = DbType.String;
            bpm5.Direction = ParameterDirection.Input;

            SqlParameter bpm6 = new SqlParameter();
            bpm6.ParameterName = "@InvoiceNo";
            bpm6.SourceColumn = "InvoiceNo";
            if (txtInvoiceNo.Text.Trim() == "") bpm6.Value = DBNull.Value; else bpm6.Value = txtInvoiceNo.Text.Trim();
            bpm6.DbType = DbType.String;
            bpm6.Direction = ParameterDirection.Input;

            SqlParameter bpm7 = new SqlParameter();
            bpm7.ParameterName = "@InvoiceRaised";
            bpm7.SourceColumn = "InvoiceRaised";
            if (txtInvoiceDt.Text.Trim() == "") bpm7.Value = DBNull.Value; else bpm7.Value = txtInvoiceDt.Text.Trim();
            bpm7.DbType = DbType.String;
            bpm7.Direction = ParameterDirection.Input;

            SqlParameter bpm8 = new SqlParameter();
            bpm8.ParameterName = "@AmtInFC";
            bpm8.SourceColumn = "AmtInFC";
            if (txtAmtInFC.Text.Trim() == "") bpm8.Value = DBNull.Value; else bpm8.Value = txtAmtInFC.Text.Trim();
            bpm8.DbType = DbType.String;
            bpm8.Direction = ParameterDirection.Input;

            SqlParameter bpm9 = new SqlParameter();
            bpm9.ParameterName = "@ConvFactor";
            bpm9.SourceColumn = "ConvFactor";
            if (txtCF.Text.Trim() == "") bpm9.Value = DBNull.Value; else bpm9.Value = txtCF.Text.Trim();
            bpm9.DbType = DbType.String;
            bpm9.Direction = ParameterDirection.Input;

            SqlParameter bpm10 = new SqlParameter();
            bpm10.ParameterName = "@Frequency";
            bpm10.SourceColumn = "Frequency";
            bpm10.Value = ddl2.SelectedValue.ToString();
            bpm10.DbType = DbType.String;
            bpm10.Direction = ParameterDirection.Input;

            SqlParameter bpm11 = new SqlParameter();
            bpm11.ParameterName = "@Mode";
            bpm11.SourceColumn = "Mode";
            bpm11.Value = ddlMode.SelectedValue.ToString();
            bpm11.DbType = DbType.String;
            bpm11.Direction = ParameterDirection.Input;


            SqlParameter bpm12 = new SqlParameter();
            bpm12.ParameterName = "@AmountInRs";
            bpm12.SourceColumn = "AmountInRs";
            if (txtAmtInRs.Text.Trim() == "") bpm12.Value = DBNull.Value; else bpm12.Value = txtAmtInRs.Text.Trim();
            bpm12.DbType = DbType.String;
            bpm12.Direction = ParameterDirection.Input;

            SqlParameter bpm13 = new SqlParameter();
            bpm13.ParameterName = "@UserName";
            bpm13.SourceColumn = "UserName";
            bpm13.Value = Membership.GetUser().UserName.ToString();
            bpm13.DbType = DbType.String;
            bpm13.Direction = ParameterDirection.Input;

            SqlParameter bpm14 = new SqlParameter();
            bpm14.ParameterName = "@Basis";
            bpm14.SourceColumn = "Basis";
            if (txtBasis.Text.Trim() == "") bpm14.Value = DBNull.Value; else bpm14.Value = txtBasis.Text.Trim();
            bpm14.DbType = DbType.String;
            bpm14.Direction = ParameterDirection.Input;

            SqlParameter bpm15 = new SqlParameter();
            bpm15.ParameterName = "@ExecutionDate";
            bpm15.SourceColumn = "ExecutionDate";
            bpm15.Value = txtexecDt.Text.Trim();
            bpm15.DbType = DbType.String;
            bpm15.Direction = ParameterDirection.Input;

            SqlParameter bpm16 = new SqlParameter();
            bpm16.ParameterName = "@RecieptNo";
            bpm16.SourceColumn = "RecieptNo";
            if (txtReceiptNo.Text.Trim() == "") bpm16.Value = DBNull.Value; else bpm16.Value = txtReceiptNo.Text.Trim();
            bpm16.DbType = DbType.String;
            bpm16.Direction = ParameterDirection.Input;

            SqlParameter bpm17= new SqlParameter();
            bpm17.ParameterName = "@Remarks";
            bpm17.SourceColumn = "Remarks";
            if (txtRemarks.Text.Trim() == "") bpm17.Value = DBNull.Value; else bpm17.Value = txtRemarks.Text.Trim();
            bpm17.DbType = DbType.String;
            bpm17.Direction = ParameterDirection.Input;

            SqlParameter apm15 = new SqlParameter();
            apm15.ParameterName = "@RecieptDate";
            apm15.SourceColumn = "RecieptDate";
            if (txtRecieptDate.Text.Trim() == "")
                apm15.Value = DBNull.Value;
            else
            {
                DateTime Expiry;
                if (DateTime.TryParse(txtRecieptDate.Text.Trim(), out Expiry))
                    apm15.Value = txtRecieptDate.Text.Trim();
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "Expiry Date", "<script>alert('Verify Receipt Date')</script>");
                    return;
                }
            }
            apm15.DbType = DbType.String;
            apm15.Direction = ParameterDirection.Input;

            SqlParameter apm9 = new SqlParameter();
            apm9.ParameterName = "@Event";
            apm9.SourceColumn = "Event";
            apm9.Value = ddl1.SelectedValue.ToString();
            apm9.DbType = DbType.String;
            apm9.Direction = ParameterDirection.Input;


            cmd2.Parameters.Add(bpm1);
            cmd2.Parameters.Add(bpm2);
            cmd2.Parameters.Add(bpm3);
            cmd2.Parameters.Add(bpm4);
            cmd2.Parameters.Add(bpm5);
            cmd2.Parameters.Add(bpm6);
            cmd2.Parameters.Add(bpm7);
            cmd2.Parameters.Add(bpm8);
            cmd2.Parameters.Add(bpm9);
            cmd2.Parameters.Add(bpm10);
            cmd2.Parameters.Add(bpm11);
            cmd2.Parameters.Add(bpm12);
            cmd2.Parameters.Add(bpm13);
            cmd2.Parameters.Add(bpm14);
            cmd2.Parameters.Add(bpm15);
            cmd2.Parameters.Add(bpm16);
            cmd2.Parameters.Add(bpm17);
            cmd2.Parameters.Add(apm15);
            cmd2.Parameters.Add(apm9);
            cmd2.ExecuteNonQuery();

            ClientScript.RegisterStartupScript(GetType(), "success", "<script>alert('This Record successfully Updated')</script>");
            FetchAction(con, txtContractNo.Text.Trim());
            con.Close();
            ClearAction();
            panUpdate.Visible = false;
            btnNewAction.Visible = true;
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(GetType(), "Error", "<script>alert('" + ex.Message.ToString() + "')</script>");
            con.Close();
            return;
        }
    }
       
    protected void lvAction_ItemCommand(object sender, ListViewCommandEventArgs e)
    {                      
        Label tmpLabel = new Label();
        tmpLabel = (Label)e.Item.FindControl("lblSlNo");
        if (e.CommandName == "Modify")
        {
            btnNewAction.Visible = false;
            btnActSave.Visible = false;
            btnActUpdate.Visible = true;
            panUpdate.Visible = true;
            ClearAction();
            thTitle.InnerText = "Action Modification";
            BindAction(txtContractNo.Text.Trim(), Convert.ToInt32(tmpLabel.Text.Trim()));
        }
    }

    protected void BindAction(string ContractNo, int Slno)
    {
        con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        //string sql = "select SlNo,ActionType,Narration,ClauseRef,ResponsePerson,TargetDt,Remark,ActionStatus from agreementAction where ContractNo ='" + ContractNo + "' and Slno="+ Slno ;
        string sql = "select SlNO,Event,Frequency,ExecutionDate,Basis,DeclDueAmt,InvoiceNo,InvoiceRaised,RecieptNo,RecieptDate,Mode,AmtInFC,ConvFactor,AmountInRs,Status,Remarks from Royality where ContractNo='" + ContractNo + "'and Slno="+ Slno;
        con.Open();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = sql;
        cmd.CommandType = CommandType.Text;
        cmd.Connection = con;
        SqlDataReader dr;
        dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            if (!dr.IsDBNull(0)) lblSlNo.Text = dr.GetInt32(0).ToString(); else lblSlNo.Text = "";
            if (!dr.IsDBNull(1)) ddl1.SelectedValue = dr.GetString(1);else ddl1.Text="";
            if (!dr.IsDBNull(2)) ddl2.SelectedValue = dr.GetString(2); else ddl2.Text = "";
            if (!dr.IsDBNull(3)) txtexecDt.Text = dr.GetDateTime(3).ToShortDateString(); else txtexecDt.Text = "";
            if (!dr.IsDBNull(4)) txtBasis.Text = dr.GetString(4); else txtBasis.Text = "";
            if (!dr.IsDBNull(5)) txtDeclAmt.Text = dr.GetString(5); else txtDeclAmt.Text = "";
            if (!dr.IsDBNull(6)) txtInvoiceNo.Text = dr.GetString(6); else txtInvoiceNo.Text = "";
            if (!dr.IsDBNull(7)) txtInvoiceDt.Text = dr.GetDateTime(7).ToShortDateString(); else txtInvoiceDt.Text = "";
            if (!dr.IsDBNull(8)) txtReceiptNo.Text = dr.GetString(8); else txtReceiptNo.Text = "";
            if (!dr.IsDBNull(9)) txtRecieptDate.Text = dr.GetDateTime(9).ToShortDateString(); else txtRecieptDate.Text = "";
            if (!dr.IsDBNull(10)) ddlMode.SelectedValue = dr.GetString(10); else ddlMode.Text = "";
            if (!dr.IsDBNull(11)) txtAmtInFC.Text = dr.GetString(11); else txtAmtInFC.Text = "";
            if (!dr.IsDBNull(12)) txtCF.Text = dr.GetString(12); else txtCF.Text = "";
            if (!dr.IsDBNull(13)) txtAmtInRs.Text = dr.GetString(13); else txtAmtInRs.Text = "";
            if (!dr.IsDBNull(14)) drop1.SelectedValue = dr.GetString(14); else drop1.Text = "";
            if (!dr.IsDBNull(15)) txtRemarks.Text = dr.GetString(15); else txtRemarks.Text = "";

        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "Contract Number Is Not Found", "<script>alert('Contract Number Is Not Found')</script>");
            dr.Close();
            con.Close();
            return;
        }
        dr.Close();
        con.Close();
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        ClearAction();
        btnNewAction.Visible = true;
        panUpdate.Visible = false;       
    }

    protected void fetchFiles(SqlConnection con, string ContractNo)
    {
        gvFileDetails.DataSource = null;
        gvFileDetails.DataBind();
        SqlCommand cmdFF = new SqlCommand();
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        string sql5 = "select EntryDt,ContractNo,FileDescription,ModifiedDt,Comments,FileName from Agreementfiles where contractNo ='" + ContractNo.Trim() + "' order by EntryDt desc";
        cmdFF.CommandText = sql5;
        cmdFF.CommandType = CommandType.Text;
        cmdFF.Connection = con;
        SqlDataReader drFF;
        drFF = cmdFF.ExecuteReader();
        DataTable dt = new DataTable();
        dt.Load(drFF);
        if (dt.Rows.Count > 25)
        {
            pageNumber.Visible = true;
        }
        else
        {
            pageNumber.Visible = false;
        }
        Session["ContractView"] = dt;
        gvFileDetails.DataSource = dt;
        gvFileDetails.DataBind();
        drFF.Close();        
    }
    protected void gvFileDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvFileDetails.PageIndex = e.NewPageIndex;
        gvFileDetails.DataSource = Session["ContractView"];
        gvFileDetails.DataBind();
    }
    protected void gvFileDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void gvFileDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SqlTransaction trans;
        try
        {
            SqlCommand cmd = new SqlCommand();
            GridViewRow item = gvFileDetails.Rows[e.RowIndex];
            string ContractNo = item.Cells[1].Text.Trim();
            string fileDescription = item.Cells[2].Text.Trim();
            LinkButton lb = (LinkButton)item.FindControl("lbtnEdit");
            string fileName = lb.Text.Trim();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
            string sql = "delete from AgreementFiles where ContractNo = '" + ContractNo.Trim() + "' and fileDescription = '" + fileDescription.Trim() + "' and fileName = '" + fileName.Trim() + "'";
            con.Open();
            trans = con.BeginTransaction();
            cmd.Transaction = trans;
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            cmd.ExecuteNonQuery();
            try
            {
                string sourceFile = @"F:\PatentDocument\Contract Files\" + ContractNo.Trim() + @"\" + fileName.Trim();
                string targetPath = @"F:\PatentDocument\Contract Files\Delete Files\" + ContractNo.Trim() + @"\";
                if (!Directory.Exists(targetPath))
                {
                    Directory.CreateDirectory(targetPath);
                }
                if (!File.Exists(targetPath + fileName.Trim()))
                {
                    string targetFile = targetPath + fileName.Trim();
                    System.IO.File.Move(sourceFile, targetFile);
                }
                else
                {
                    string tmpFileName = Path.GetFileNameWithoutExtension(sourceFile);
                    string extension = Path.GetExtension(sourceFile);
                    tmpFileName = tmpFileName + DateTime.Now.ToString("dd-MM-yyyy-hh-mm");
                    tmpFileName = tmpFileName + extension;
                    string targetFile = targetPath + tmpFileName.Trim();
                    System.IO.File.Move(sourceFile, targetFile);
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "Error", "<script>alert('" + ex.Message.ToString() + "')</script>");
                trans.Rollback();
                con.Close();
                return;
            }
            trans.Commit();
            ClientScript.RegisterStartupScript(GetType(), "success", "<script>alert('This Record successfully Deleted')</script>");
            fetchFiles(con, ContractNo);
            con.Close();
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(GetType(), "Error", "<script>alert('" + ex.Message.ToString() + "')</script>");
            con.Close();
            return;
        }
    }
    protected void gvFileDetails_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        GridViewRow item = gvFileDetails.Rows[e.NewSelectedIndex];
        string ContractNo = item.Cells[1].Text;
        LinkButton lbFileName = gvFileDetails.Rows[e.NewSelectedIndex].FindControl("lbtnEdit") as LinkButton;

        string strPath = "";
        string strFileName = "";
        strPath = @"F:\PatentDocument\Contract Files\" + ContractNo.Trim() + @"\";
        strFileName = lbFileName.Text.Trim();
        string extension = Path.GetExtension(strPath + strFileName);
        string mimeType = "image/jpeg";
        if (extension == ".jpg")
            mimeType = "image/jpeg";
        else if (extension == ".xls")
            mimeType = "application/vnd.xls";
        else if (extension == ".xlsx")
            mimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        else if (extension == ".doc")
            mimeType = "application/msword";
        else if (extension == ".docx")
            mimeType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
        else if (extension == ".ppt")
            mimeType = "application/vnd.ms-powerpoint";
        else if (extension == ".pptx")
            mimeType = "application/vnd.openxmlformats-officedocument.presentationml.presentation";
        else if (extension == ".pdf")
            mimeType = "application/pdf";
        else if (extension == ".txt")
            mimeType = "plain/text";
        FileStream fs = new FileStream(strPath + strFileName, FileMode.Open);
        byte[] bytBytes = new byte[fs.Length];
        fs.Read(bytBytes, 0, (int)fs.Length);
        fs.Close();
        strFileName = strFileName.Replace(" ", "-");
        Response.AppendHeader("Content-Disposition", "attachment;filename=" + strFileName);
        Response.ContentType = mimeType;
        Response.BinaryWrite(bytBytes);
        Response.End();
    }
    private string getSortDirection(string column)
    {
        string sortDirection = "ASC";
        string sortExpression = ViewState["sortExpression"] as string;
        if (sortExpression != null)
        {
            if (sortExpression == column)
            {
                string lastDirection = ViewState["sortDirection"] as string;
                if ((lastDirection != null) && (lastDirection == "ASC"))
                {
                    sortDirection = "Desc";
                }
            }
        }
        ViewState["sortExpression"] = column;
        ViewState["sortDirection"] = sortDirection;

        return sortDirection;
    }
    protected void gvFileDetails_Sorting(object sender, GridViewSortEventArgs e)
    {
        DataTable dt = Session["ContractView"] as DataTable;
        if (dt != null)
        {
            dt.DefaultView.Sort = e.SortExpression + " " + getSortDirection(e.SortExpression);

            gvFileDetails.DataSource = dt;
            gvFileDetails.DataBind();
        }
    }
    protected void gvFileDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        LinkButton lb = e.Row.FindControl("lbtnDelete") as LinkButton;
        if (lb != null)
        {
            if (User.IsInRole("Admin") || User.IsInRole("Super User"))
            {
                lb.Visible = true;
            }
        }
        
        if(!User.IsInRole("Admin") && !User.IsInRole("Super User"))
        {
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
                e.Row.Cells[6].Visible = false;

        }
    }
    protected void ClearFileUpload()
    {
        txtFileDesc.Text = "";
        FileUploadAgree.Dispose();
        txtComment.Text="";
    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        panNewFile.Visible = true;
        btnUpload.Visible = false;
        ClearFileUpload();
    }
    protected void btnFileSave_Click(object sender, EventArgs e)
    {
        if (txtFileDesc.Text.Trim() == "")
        {
            ClientScript.RegisterStartupScript(GetType(), "File Description", "<script>alert('Description of File can not be empty')</script>");
            return;
        }
        if (!FileUploadAgree.HasFile)
        {
            ClientScript.RegisterStartupScript(GetType(), "Upload File", "<script>alert('Select Uploading File')</script>");
            return;
        }
        string fname = FileUploadAgree.FileName;
        using (SqlConnection cn = new SqlConnection())
        {
            cn.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
            try
            {
                using (SqlCommand cmd2 = new SqlCommand())
                {
                    cmd2.CommandText = "Select fileDescription,fileName from AgreementFiles where ContractNo='" + txtContractNo.Text.Trim() + "'";
                    cmd2.CommandType = CommandType.Text;
                    cmd2.Connection = cn;
                    cn.Open();
                    SqlDataReader sdr;
                    sdr = cmd2.ExecuteReader();
                    while (sdr.Read())
                    {
                        if (sdr.GetString(0) == txtFileDesc.Text.Trim() || sdr.GetString(1) == fname)
                        {
                            ClientScript.RegisterStartupScript(GetType(), "Error", "<script>alert('File Description or File Name already Exist')</script>");
                            cn.Close();
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "Error", "<script>alert('" + ex.Message.ToString() + "')</script>");
                cn.Close();
                return;
            }
        }
        SqlCommand cmd = new SqlCommand();
        SqlTransaction Trans;
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
        try
        {
            cmd.CommandText = "AgreeFileInsert";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            con.Open();
            Trans = con.BeginTransaction();
            cmd.Transaction = Trans;
            SqlParameter pm1 = new SqlParameter();
            pm1.ParameterName = "@EntryDt";
            pm1.SourceColumn = "EntryDt";
            pm1.Value = DateTime.Now.ToShortDateString();
            pm1.DbType = DbType.String;
            pm1.Direction = ParameterDirection.Input;

            SqlParameter pm2 = new SqlParameter();
            pm2.ParameterName = "@ContractNo";
            pm2.SourceColumn = "@ContractNo";
            pm2.Value = txtContractNo.Text.Trim();
            pm2.DbType = DbType.String;
            pm2.Direction = ParameterDirection.Input;

            SqlParameter pm3 = new SqlParameter();
            pm3.ParameterName = "@FileDescription";
            pm3.SourceColumn = "FileDescription";
            pm3.Value = txtFileDesc.Text.Trim();
            pm3.DbType = DbType.String;
            pm3.Direction = ParameterDirection.Input;

            SqlParameter pm4 = new SqlParameter();
            pm4.ParameterName = "@FileName";
            pm4.SourceColumn = "FileName";
            pm4.Value = fname;
            pm4.DbType = DbType.String;
            pm4.Direction = ParameterDirection.Input;

            SqlParameter pm5 = new SqlParameter();
            pm5.ParameterName = "@Comments";
            pm5.SourceColumn = "Comments";
            if (txtComment.Text.Trim() == "") pm5.Value = DBNull.Value; else pm5.Value = txtComment.Text.Trim();
            pm5.DbType = DbType.String;
            pm5.Direction = ParameterDirection.Input;

            SqlParameter pm6 = new SqlParameter();
            pm6.ParameterName = "@UserName";
            pm6.SourceColumn = "UserName";
            pm6.Value = Membership.GetUser().UserName.ToString();
            pm6.DbType = DbType.String;
            pm6.Direction = ParameterDirection.Input;

            cmd.Parameters.Add(pm1);
            cmd.Parameters.Add(pm2);
            cmd.Parameters.Add(pm3);
            cmd.Parameters.Add(pm4);
            cmd.Parameters.Add(pm5);
            cmd.Parameters.Add(pm6);

            cmd.ExecuteNonQuery();
            try
            {
                HttpPostedFile file1 = FileUploadAgree.PostedFile;
                Int32 fileLength = file1.ContentLength;
                string fileName = file1.FileName;
                byte[] buffer = new byte[fileLength];
                file1.InputStream.Read(buffer, 0, fileLength);
                FileStream newFile;
                string strPath = @"F:\PatentDocument\Contract Files\" + txtContractNo.Text.Trim() + @"\";
                if (!Directory.Exists(strPath))
                {
                    Directory.CreateDirectory(strPath);
                }
                if (!File.Exists(strPath + fileName))
                {
                    newFile = File.Open(strPath + fileName, FileMode.Create);
                    newFile.Write(buffer, 0, buffer.Length);
                    newFile.Close();
                }
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "Information", "<script>alert('This File exists in this Folder')</script>");
                    Trans.Rollback();
                    con.Close();
                    return;
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "Error", "<script>alert('" + ex.Message.ToString() + "')</script>");
                Trans.Rollback();
                con.Close();
                return;
            }

            ClientScript.RegisterStartupScript(GetType(), "Success", "<script>alert('This Record successfully added')</script>");
            Trans.Commit();
            fetchFiles(con, txtContractNo.Text);
            con.Close();
            btnFileClose_Click(sender, e);
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(GetType(), "Error", "<script>alert('" + ex.Message.ToString() + "')</script>");
            con.Close();
            return;
        }
    }
    protected void btnFileClose_Click(object sender, EventArgs e)
    {
        ClearFileUpload();
        panNewFile.Visible = false;
        btnUpload.Visible = true;        
    }
    protected void lvAction_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
