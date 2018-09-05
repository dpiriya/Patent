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
using System.Web.Services;

public partial class Contract : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (User.IsInRole("Admin") || User.IsInRole("Super User") || User.IsInRole("Marketing"))
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
                con.Close();
                dsContract ds = new dsContract();
                imgBtnInsert.Visible = (!User.IsInRole("View"));
                txtContractNo.Text = AssignContractNo();
               filldata();
            }
        }
        else
        {
            Server.Transfer("Unautherized.aspx");
        }
        con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
        con.Open();
        //SqlCommand cmd11 = new SqlCommand("select max(SlNO) from Royality where ContractNo='" + txtContractNo.Text + "'", con);
      
        //int count=Convert.ToInt32(cmd11.ExecuteScalar());


        //int c = ++count;
        //txtslno.Text = c.ToString();

        SqlCommand cmd11 = new SqlCommand("select count(*) from Royality where ContractNo='" + txtContractNo.Text + "'", con);
        int count = Convert.ToInt32(cmd11.ExecuteScalar());

        if (count != 0)
        {
            int c = ++count;
            txtslno.Text = c.ToString();
        }
        else
        {
            int c = 1;
            txtslno.Text = c.ToString();
        }
        con.Close();
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
    protected void lvAction_ItemCreated(object sender, ListViewItemEventArgs e)
    {
        DropDownList ddlActionType = (DropDownList)e.Item.FindControl("ddlNewType");
        if (ddlActionType != null)
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
            SqlCommand cmd = new SqlCommand();
            string sql = "SELECT ITEMLIST FROM LISTITEMMASTER WHERE CATEGORY='ContractActionType' ORDER BY ITEMLIST";
            SqlDataReader dr;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            cmd.CommandText = sql;
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ddlActionType.Items.Add(dr["ITEMLIST"].ToString());
            }
            //ddlActionType.DataTextField = "itemList";
            //ddlActionType.DataValueField = "itemList";
            //ddlActionType.DataSource = dr;
            //ddlActionType.DataBind();
            ddlActionType.Items.Insert(0, new ListItem("", ""));
            con.Close();
            txtContractNo.Text = AssignContractNo();
        }
    }
    protected string AssignContractNo()
    {
        con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
        SqlCommand cmd3 = new SqlCommand();
        string sql3 = "Select  max(cast(Substring(Contractno,5,len(Contractno)-4) as int))+1 From Agreement";
        cmd3.CommandText = sql3;
        cmd3.Connection = con;
        cmd3.CommandType = CommandType.Text;
        con.Open();
        string ContractNo = Convert.ToString(cmd3.ExecuteScalar());
        if (string.IsNullOrEmpty(ContractNo)) ContractNo = "MDOC1"; else ContractNo = "MDOC" + ContractNo;
        con.Close();
        return ContractNo;
    }
    //protected void lvAction_ItemDeleting(object sender, ListViewDeleteEventArgs e)
    //{
    //    System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
    //    Label lbl = lvAction.Items[e.ItemIndex].FindControl("lblSlNo") as Label;
    //    DataTable dt = (DataTable)ViewState["ContractAction"];
    //    int cnt = 0;
    //    foreach (DataRow dr in dt.Rows)
    //    {
    //        cnt += 1;
    //        if (dr["SlNo"].ToString() == lbl.Text.Trim())
    //        {
    //            dr.Delete();
    //            break;
    //        }
    //    }
    //    for (int i = cnt - 1; i <= dt.Rows.Count - 1; i++)
    //    {
    //        dt.Rows[i][0] = i + 1;
    //    }
    //   // lvAction.DataSource = (DataTable)ViewState["ContractAction"];
    //    //lvAction.DataBind();

    //}
    //protected void lvAction_ItemCanceling(object sender, ListViewCancelEventArgs e)
    //{
    //    System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
    //    lvAction.EditIndex = -1;
    //    lvAction.DataSource = (DataTable)ViewState["ContractAction"];
    //    lvAction.DataBind();
    //}
    //protected void lvAction_ItemInserting(object sender, ListViewInsertEventArgs e)
    //{
    //    System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
    //    string slno = Convert.ToString(lvAction.Items.Count + 1);
    //    DropDownList ddlType = e.Item.FindControl("ddlNewType") as DropDownList;
    //    if (ddlType.SelectedIndex == -1 || ddlType.SelectedIndex == 0)
    //    {
    //        ClientScript.RegisterStartupScript(GetType(), "Contract Action", "<script>alert('Select Action Type')</script>");
    //        return;
    //    }
    //    TextBox tmpNarration = e.Item.FindControl("txtNarration") as TextBox;
    //    if (tmpNarration.Text == "")
    //    {
    //        ClientScript.RegisterStartupScript(GetType(), "Contract Action", "<script>alert('Enter Narration')</script>");
    //        return;
    //    }
    //    TextBox tmpClause = e.Item.FindControl("txtClauseRef") as TextBox;
    //    TextBox tmpResponse = e.Item.FindControl("txtResponsePerson") as TextBox;
    //    if (tmpResponse.Text == "")
    //    {
    //        ClientScript.RegisterStartupScript(GetType(), "Contract Action", "<script>alert('Enter Response Person')</script>");
    //        return;
    //    }
    //    TextBox tmpTarget = e.Item.FindControl("txtTargetDt") as TextBox;
    //    if (tmpTarget.Text.Trim() != "")
    //    {
    //        DateTime tmpTdate;
    //        if (!DateTime.TryParse(tmpTarget.Text, out tmpTdate))
    //        {
    //            ClientScript.RegisterStartupScript(GetType(), "Contract Action", "<script>alert('Enter Target Date')</script>");
    //            return;
    //        }
    //    }
    //    TextBox tmpRemark = e.Item.FindControl("txtRemark") as TextBox;
    //  DataTable dtCA = (DataTable)ViewState["ContractAction"];
    //    DataRow dr = dtCA.NewRow();
    //    dr["SlNo"] = slno;
    //    dr["ActionType"] = ddlType.SelectedItem.Text.Trim();
    //    dr["Narration"] = tmpNarration.Text.Trim();
    //    if (tmpClause.Text.Trim() == "") dr["ClauseRef"] = DBNull.Value; else dr["ClauseRef"] = tmpClause.Text.Trim();
    //    if (tmpResponse.Text.Trim() == "") dr["ResponsePerson"] = DBNull.Value; else dr["ResponsePerson"] = tmpResponse.Text.Trim();
    //    if (tmpTarget.Text.Trim() == "") dr["TargetDt"] = DBNull.Value; else dr["TargetDt"] = Convert.ToDateTime(tmpTarget.Text);
    //    dr["Remark"] = tmpRemark.Text.Trim();
    //    dtCA.Rows.Add(dr);
    //    lvAction.DataSource = (DataTable)ViewState["ContractAction"];
    //    lvAction.DataBind();
    //}
    protected void imgBtnInsert_Click(object sender, ImageClickEventArgs e)
    {
        if (string.IsNullOrEmpty(txtContractNo.Text))
        {
            ClientScript.RegisterStartupScript(GetType(), "Contract Number", "<script>alert('Agreement No Can not be Empty')</script>");
            return;
        }
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
        SqlCommand cmd1 = new SqlCommand();
        string sql1 = "select ContractNo from Agreement where ContractNo like '" + txtContractNo.Text.Trim() + "'";
        cmd1.CommandText = sql1;
        cmd1.CommandType = CommandType.Text;
        cmd1.Connection = con;
        con.Open();
        SqlDataReader dr;
        dr = cmd1.ExecuteReader();
        if (dr.Read())
        {
            ClientScript.RegisterStartupScript(GetType(), "Contract Number Exist", "<script>alert('This Agreement No. is exist in table')</script>");
            dr.Close();
            return;
        }
        dr.Close();
        //SqlTransaction Trans;
        //Trans = con.BeginTransaction();
        try
        {

            SqlCommand cmd = new SqlCommand();
            // cmd.Transaction = Trans;
            cmd.CommandText = "ContractNew";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlParameter pm1 = new SqlParameter();
            pm1.ParameterName = "@EntryDt";
            pm1.SourceColumn = "EntryDt";
            pm1.Value = DateTime.Now.ToShortDateString();
            pm1.DbType = DbType.String;
            pm1.Direction = ParameterDirection.Input;

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

            SqlParameter pm19 = new SqlParameter();
            pm19.ParameterName = "@TechTransfer";
            pm19.SourceColumn = "TechTransfer";
            if (txtTechTransfer.Text.Trim() == "") pm19.Value = DBNull.Value; else pm19.Value = txtTechTransfer.Text.Trim();
            pm19.DbType = DbType.String;
            pm19.Direction = ParameterDirection.Input;


            SqlParameter pm18 = new SqlParameter();
            pm18.ParameterName = "@Request";
            pm18.SourceColumn = "@Request";
            if (txtRequest.Text.Trim() == "")
                pm18.Value = DBNull.Value;
            else
            {
                DateTime Expiry;
                if (DateTime.TryParse(txtRequest.Text.Trim(), out Expiry))
                    pm18.Value = txtRequest.Text.Trim();
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "Request Date", "<script>alert('Verify Request Date')</script>");
                    return;
                }
            }
            pm18.DbType = DbType.String;
            pm18.Direction = ParameterDirection.Input;

            SqlParameter pm20 = new SqlParameter();
            pm20.ParameterName = "@Status";
            pm20.SourceColumn = "Status";
            if (dropstatus.SelectedItem.Text.Trim() == "") pm20.Value = DBNull.Value; else pm20.Value = dropstatus.SelectedItem.Text.Trim();
            pm20.DbType = DbType.String;
            pm20.Direction = ParameterDirection.Input;

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
            //cmd.Parameters.Add(pm15);
            //cmd.Parameters.Add(pm16);
            //cmd.Parameters.Add(pm17);
            cmd.Parameters.Add(pm18);
            cmd.Parameters.Add(pm19);
            cmd.Parameters.Add(pm20);
            cmd.ExecuteNonQuery();
            con.Close();
            // Trans.Commit();
            ClientScript.RegisterStartupScript(GetType(), "success", "<script>alert('Record Successfully Saved')</script>");

        }
        catch (Exception ex)
        {
            //Trans.Rollback();
            ClientScript.RegisterStartupScript(GetType(), "Error", "<script>alert('" + ex.Message.ToString() + "')</script>");
          
            SqlCommand cmd4 = new SqlCommand("Delete * from Royality where ContractNo='" + txtContractNo.Text + "'", con);
            cmd4.ExecuteNonQuery();
            return;
        }
        imgBtnClear_Click(sender, e);

    }
    void filldata()
    {
        con.Open();
        SqlDataAdapter da = new SqlDataAdapter("select SlNO,ContractNO,Event,Frequency,ExecutionDate,Basis,InvoiceNo,InvoiceRaised,AmountInRs,Status from Royality where ContractNo='" + txtContractNo.Text.Trim() + "'", con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        GV1.DataSource = dt;
        GV1.DataBind();
        con.Close();
    }

    protected void imgBtnClear_Click(object sender, ImageClickEventArgs e)
    {
        txtContractNo.Text = AssignContractNo();
        ddlAgreeType.Text = "";
        txtTitle.Text = "";
        txtScope.Text = "";
        txtParty.Text = "";
        ddlDept.Text = "";
        ddlCoor.Items.Clear();
        txtEffectiveDt.Text = "";
        txtExpiryDt.Text = "";
        txtAgmtNo.Text = "";
        txtRemark.Text = "";
        txtRequest.Text = "";
        txtTechTransfer.Text = "";
        ddl1.Text = "";
        txtBasis.Text = "";
        txtDeclAmt.Text = "";
        txtInvRaised.Text = "";
        txtAmtInCF.Text = "";
        txtCF.Text = "";
        txtAmtInRs.Text = "";
        txtexecDt.Text="";
        drop1.Text = "";
        txtRemark.Text = "";
        txtAmtInRs.Text = "";
        txtReceiptNo.Text = "";
        txtRecieptDate.Text = "";
        ddlMode.Text = "";
        GV1.Visible = false;
        txtInvoiceNo.Text = "";
        txtRemarks.Text = "";
        txtslno.Text = "";
       


       
        //  DataTable dtCA =(DataTable) ViewState["ContractAction"];
        //  dtCA.Clear();
        ////  lvAction.DataSource = dtCA;
        //lvAction.DataBind();
        //ViewState["ContractAction"] = dtCA;       
    }
    protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlCoor.Items.Clear();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
        SqlCommand cmd = new SqlCommand();
        string sql = "select CoorCode,CoorName from FacultyMaster where deptcode like '" + ddlDept.SelectedValue.Trim() + "' ORDER BY CoorName";
        SqlDataReader dr;
        cmd.CommandType = CommandType.Text;
        cmd.Connection = con;
        cmd.CommandText = sql;
        con.Open();
        dr = cmd.ExecuteReader();
        ddlCoor.DataTextField = "CoorName";
        ddlCoor.DataValueField = "CoorCode";
        ddlCoor.DataSource = dr;
        ddlCoor.DataBind();
        ddlCoor.Items.Insert(0, new ListItem("", ""));
        con.Close();
    }
    protected void ListView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void txtDeclAmt_TextChanged(object sender, EventArgs e)
    {

    }

    protected void GV1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
      //  con.Open();
      //  System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
      //  //int index = Convert.ToInt32(e.RowIndex);
      ////  var ID = (HiddenField)GV1.Rows[e.RowIndex].FindControl("SlNO");

      //  int ID = int.Parse(GV1.Rows[e.RowIndex].FindControl("SlNO").ToString());


      //  SqlCommand cmd6 = new SqlCommand("Delete from Royality where SlNO='" + ID.ToString() + "'", con);
      //  cmd6.ExecuteNonQuery();
      //  con.Close();
        con.Open();
        string a = GV1.Rows[e.RowIndex].Cells[1].Text.ToString();
        string contract = GV1.Rows[e.RowIndex].Cells[2].Text.ToString();
        SqlCommand cmd6 = new SqlCommand("Delete from Royality where SlNO='" + a.ToString() + "' and ContractNo='" + contract.ToString() + "'", con);
        cmd6.ExecuteNonQuery();
        con.Close();
    }

    protected void button1_Click(object sender, EventArgs e)
    {
        con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
        con.Open();
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
        apm3.Value = txtslno.Text.Trim();
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
        if (txtInvRaised.Text.Trim() == "")
            apm13.Value = DBNull.Value;
        else
        {
            DateTime Expiry;
            if (DateTime.TryParse(txtInvRaised.Text.Trim(), out Expiry))
                apm13.Value = txtInvRaised.Text.Trim();
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
        apm14.Value = txtAmtInCF.Text.Trim();
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

        SqlParameter apm7= new SqlParameter();
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
       // cmd3.Parameters.Add(apm4);
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
        filldata();

        txtRequest.Text = "";
      //  TxtTTACC.Text = "";
        //txtAgreeName.Text = "";
        //txtIDF.Text = "";
        //ddl1.Text = "";
        txtBasis.Text = "";
        txtDeclAmt.Text = "";
        txtInvRaised.Text = "";
        txtAmtInCF.Text = "";
        txtCF.Text = "";
        txtAmtInRs.Text = "";
        ddl2.Text = "";
       // txtStatus.Text = "";
        GV1.Columns.Clear();


        con.Open();
        //SqlCommand cmd11 = new SqlCommand("select count(*)from Royality where ContractNo='" + txtContractNo.Text + "'", con);

        //Int32 count = Convert.ToInt32(cmd11.ExecuteScalar());
        //txtslno.Text = count++.ToString();
        SqlCommand cmd11 = new SqlCommand("select count(*) from Royality where ContractNo='" + txtContractNo.Text + "'", con);
        int count = Convert.ToInt32(cmd11.ExecuteScalar());

        if (count != 0)
        {
            int c = ++count;
            txtslno.Text = c.ToString();
        }
        else
        {
            int c = 1;
            txtslno.Text = c.ToString();
        }
        con.Close();
    }
    protected void TxtTTACC_TextChanged(object sender, EventArgs e)
    {

    }
    protected void ddl1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
